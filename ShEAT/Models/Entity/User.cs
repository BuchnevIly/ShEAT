using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShEAT.Models.Entity
{
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}