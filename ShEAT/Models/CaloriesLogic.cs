using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.IO;

namespace ShEAT.Models
{
    public class CaloriesLogic
    {
        private string productUrl = "https://www.imageidentify.com/objects/user-26a7681f-4b48-4f71-8f9f-93030898d70d/prd/urlapi?image={0}";
        private string calories = "https://api.nutritionix.com/v1_1/search/{0}?fields=nf_calories%2Cnf_total_fat&appId=4c195a81&appKey=3f70599afceafe3fc3aaf86647f7c2d6";

        public string GetProduct(string url)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync(string.Format(productUrl, HttpUtility.UrlEncode(url)));
            result.Wait();
            var result2 = result.Result.Content.ReadAsStringAsync();
            result2.Wait();
            var product = new JavaScriptSerializer().Deserialize<Product>(result2.Result);
            return product.identify.title;
        }

        public double GetCalories(string name)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync(string.Format(calories, HttpUtility.UrlEncode(name)));
            result.Wait();
            var result2 = result.Result.Content.ReadAsStringAsync();
            result2.Wait();
            var calor = new JavaScriptSerializer().Deserialize<Calories>(result2.Result);
            var res = calor.hits.FirstOrDefault();
            if (res != null)
                return res.fields.nf_calories;

            return 0; 
        }
    }

    #region Product
    public class Product
    {
        public string id { get; set; }
        public string image { get; set; }
        public bool classify { get; set; }
        public Identity identify { get; set; }
    }

    public class Identity
    {
        public string title { get; set; }
        public string entity { get; set; }
    }
    #endregion


    #region calories
    public class Calories
    {
        public string total_hits { get; set; }
        public double max_score { get; set; }
        public Hits[] hits { get; set; }
    }

    public class Hits
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public double _score { get; set; }
        public Fields fields { get; set; }
    }

    public class Fields
    {
        public double nf_calories { get; set; }
        public double nf_total_fat { get; set; }
        public double nf_serving_size_qty { get; set; }
        public string nf_serving_size_unit { get; set; }
    }
    #endregion
}