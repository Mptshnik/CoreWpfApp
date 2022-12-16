using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreWpfApp.Models
{
    public class Employee : IDataErrorInfo
    {
        public string this[string columnName] 
        {
            get
            {
                string error = string.Empty;
                switch(columnName)
                {
                    case "FirstName":
                        if (!ValidateFirstName())
                            error = "Имя должно состоять из букв русского или латинского алфавита";
                        break;
                    case "LastName":
                        if (!ValidateLastName())
                            error = "Фамилия должна состоять из букв русского или латинского алфавита";
                        break;
                    case "MiddleName":
                        if (!ValidateMiddleName())
                            error = "Отчество должно состоять из букв русского или латинского алфавита";
                        break;
                    case "Login":
                        if (!ValidateLogin())
                            error = "Логин должнен состоять из букв латинского алфавита и цифр 0-9";
                        if (!LoginIsUnique())
                            error = "Логин должен быть уникальным";
                        break;
                    case "Password":
                        if (!ValidatePassword())
                            error = "Пароль должнен состоять латинского алфавита и цифр 0-9. Минимальная длина пароля - 6 символов";
                        break;

                }

                return error;
            }
        }

        public bool IsValid()
        {
            return ValidateFirstName() && ValidateLastName() 
                && ValidateMiddleName() && ValidatePassword() && ValidateLogin() && LoginIsUnique();
        }

        private bool ValidateFirstName()
        {          
            string pattern = "[a-zA-Zа-яА-Я]+";
                if (FirstName == null)
                    return false;
                if (!Regex.IsMatch(FirstName, pattern))
                {
                    return false;
                }

            return true;
        }

        private bool ValidateLastName()
        {
            string pattern = "[a-zA-Zа-яА-Я]+";
            if (LastName == null)
                return false;

                if (!Regex.IsMatch(LastName, pattern))
                {
                    return false;
                }

            return true;
        }

        private bool ValidateMiddleName()
        {
            string pattern = "[a-zA-Zа-яА-Я]+";
            if (MiddleName != null && MiddleName != "")
            {              
                if (!Regex.IsMatch(MiddleName, pattern))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidateLogin()
        {
            string pattern = "[a-zA-Z0-9]+";
            if (Login != null && Login != "")
            {
                if (!Regex.IsMatch(Login, pattern))
                {
                    return false;
                }
            }

            return true;
        }

        private bool LoginIsUnique()
        {
            bool isUnique = false;
            if (Login == null)
                return true;
            using(DatabaseContext databaseContext = new DatabaseContext())
            {
                Employee employee = databaseContext.Employees.Where(e => e.Login == Login && e.ID != ID).FirstOrDefault();
                if(employee == null)
                {
                    isUnique = true;
                }
            }

            return isUnique;
        }

        private bool ValidatePassword()
        {
            string pattern = "[a-zA-Z0-9]+";
            if (Password != null && Password != "")
            {

                if (!Regex.IsMatch(Password, pattern) || Password.Length < 6)
                {
                    return false;
                }
            }

            return true;
        }

        [Key] public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int? Role { get; set; }
        public int? PostID { get; set; }
        public Post Post { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public string Error => throw new NotImplementedException();
    }
}
