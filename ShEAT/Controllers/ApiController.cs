using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShEAT.Controllers
{
    public class ApiController : Controller
    {
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="email">почта</param>
        /// <param name="password">пароль</param>
        /// <returns></returns>
        public JsonResult Login(string email, string password)
        {
            return null;
        }


        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="email">почта</param>
        /// <param name="password">пароль</param>
        /// <returns></returns>
        public JsonResult Registration(string email, string password)
        {
            return null;
        }
        //test commit

        /// <summary>
        ///  Статистика
        /// </summary>
        /// <param name="email">почта</param>
        /// <param name="days">Кол-во дней</param>
        /// <returns></returns>
        public JsonResult Statistic(string email, int days)
        {
            return null;
        }

        /// <summary>
        /// Получение инфы по картинке
        /// </summary>
        /// <param name="email">почта</param>
        /// <param name="image">картинка</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCalories(string email, byte[] image)
        {
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
            return null;
        }

        /// <summary>
        /// Получение истории (по умолчанию  последние 30 )
        /// </summary>
        /// <param name="email">почта</param>
        /// <param name="count">колличество </param>
        /// <returns></returns>
        public JsonResult GetHistory(string email, int count = 30)
        {
            return null;
        }
    }
}