using ShEAT.Models;
using ShEAT.Models.DTO;
using ShEAT.Models.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShEAT.Controllers
{
    public class ApiController : Controller
    {

        CaloriesContext db = new CaloriesContext();
        LogManager logs = new LogManager();
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="email">почта</param>
        /// <param name="password">пароль</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            var result = false;
            if (user != null)
                result = true;

            return Json(new LoginResponse { isLoginSuccessful = result }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="email">почта</param>
        /// <param name="password">пароль</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Registration(string email, string password)
        {
            if (db.Users.Where(x => x.Email == email).Any())
            {
                return Json(new RegistrationResponse
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.Users.Add(new User
                {
                    Email = email,
                    Password = password
                });

                db.SaveChanges();
                return Json(new RegistrationResponse
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        ///  Статистика
        /// </summary>
        /// <param name="email">почта</param>
        /// <param name="days">Кол-во дней</param>
        /// <returns></returns>
        public JsonResult Statistic(string email, int days)
        {
            logs.WriteLog("Statistic", email);
            logs.WriteLog("Statistic", days.ToString());
            var dateFrom = DateTime.Now.AddDays(-days);

            var user = db.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                logs.WriteLog("Statistic", user.Email);
                var history = db.Histories.Where(x => x.UserID == user.ID && x.UpdateTime >= dateFrom).ToList();
                var dates = history.Select(x => x.UpdateTime).Distinct();

                var calor = new List<double>();

                foreach (var date in dates)
                {
                    var inThisDay = history.Where(x => x.UpdateTime == date);
                    calor.Add(inThisDay.Sum(x => x.Calories));
                }
                logs.WriteLog("Statistic", "Success");
                return Json(new StatisticResponse
                {
                    FullListCalories = calor,
                    FullStatistic = dates.ToList()
                }, JsonRequestBehavior.AllowGet);
            }


            logs.WriteLog("Statistic", "Fail");
            return Json(new StatisticResponse
            {
                FullListCalories = new List<double>(),
                FullStatistic = new List<DateTime>()
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Получение инфы по картинке
        /// </summary>
        /// <param name="email">почта</param>
        /// <param name="image">картинка</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCalories(string email, string url)
        {


            logs.WriteLog("GetCalories: url", email);
            logs.WriteLog("GetCalories: url", url);
           
            var user = db.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                var test = new CaloriesLogic();
                var name = test.GetProduct(url);
                var colories = test.GetCalories(name);

                var history = new History
                {
                    Calories = colories,
                    ImageURL = url,
                    IsVisible = false,
                    Name = name,
                    UpdateTime = DateTime.Now,
                    UserID = user.ID
                };

                db.Histories.Add(history);
                db.SaveChanges();


                logs.WriteLog("GetCalories", history.ID.ToString());
                return Json(new CaloriesResponse
                {
                    ID = history.ID,
                    Caloricity = history.Calories,
                    ImageURL = history.ImageURL,
                    NameImageObject = history.Name
                }, JsonRequestBehavior.AllowGet);

            }




            return Json(new CaloriesResponse (), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string GetCalories2(HttpPostedFileBase file)
        {
            logs.WriteLog("GetCalories2", "Start control");
            if (file != null)
            {
                try
                {
                    logs.WriteLog("GetCalories2", Path.GetFileName(file.FileName));
                    string pic = Path.GetFileName(file.FileName);
                    string path = Path.Combine(
                                           Server.MapPath("~/UploadedFiles/"), pic);
                    // file is uploaded
                    file.SaveAs(path);

                    return "http://u0350169.plsk.regruhosting.ru/caloriesfit.info/UploadedFiles/" + pic;
                    
                }
                catch (Exception e)
                {
                    logs.WriteLog("GetCalories2", e.Message);
                } 
            }
            logs.WriteLog("GetCalories2", "Finish control");
            return null;
        }

        /// <summary>
        /// Сохранение в историю 
        /// </summary>
        /// <param name="email">почта</param>
        /// <param name="id">id записи</param>
        /// <returns></returns>
        public JsonResult Save(string email, int id)
        {
            logs.WriteLog("Save", email);
            logs.WriteLog("Save", id.ToString());
            var item = db.Histories.FirstOrDefault(x => x.ID == id);
            logs.WriteLog("Save", item.UserID + " " + item.ID);
            if (item != null)
            {
                logs.WriteLog("Save", email);
                item.IsVisible = true;
                db.SaveChanges();
                return Json(new SaveResponse { isSaveSuccessful = true}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new SaveResponse { isSaveSuccessful = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Получение истории (по умолчанию  последние 30 )
        /// </summary>
        /// <param name="email">почта</param>
        /// <param name="count">колличество </param>
        /// <returns></returns>
        public JsonResult GetHistory(string email, int count = 30)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                return Json(new HistoryResponse
                {
                    History = db.Histories.Where(x => x.UserID == user.ID)
                    .Select(x => new CaloriesResponse
                    {
                        Caloricity = x.Calories,
                        ImageURL = x.ImageURL,
                        NameImageObject = x.Name
                    }).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
                
            return Json(new HistoryResponse
            {
                History = new List<CaloriesResponse>()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}