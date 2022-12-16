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
    public class SportEvent : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get 
            {
                string error = string.Empty;

                switch(columnName)
                {
                    case "Name":
                        if (!ValidateName())
                            error = "Наименование должно содержать буквы русского или латинского алфавита или цифры 0-9";
                        break;
                    case "Date":
                        if (!ValidateDate())
                            error = "Дата должна быть больше текущей. Дата обязательна.";
                        break;
                    case "Description":
                        if (!ValidateDescription())
                            error = "Описание обязательно";
                        break;
                    case "BeginningTime":
                        if (!ValidateBeginningTime())
                            error = "Время начала должно соответствовать маске ввода ЧЧ:ММ";
                        break;
                }

                return error;
            }
        }

        public bool IsValid()
        {
            return ValidateBeginningTime() && ValidateDate() && ValidateName()
                && ValidateDescription();
        }

        private bool ValidateName()
        {
            string pattern = @"[a-zA-Zа-яА-Я0-9\s]+";
            if (Name == null)
                return false;
            if (!Regex.IsMatch(Name, pattern))
            {
                return false;
            }

            return true;
        }

        private bool ValidateDate()
        {
           
             if (Date == null)
                return false;
            if (DateTime.TryParse(Date, out DateTime dateTime))
            {
                if (dateTime.Date < DateTime.Now.Date)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private bool ValidateDescription()
        {
            if (Description == null)
                return false;

            return true;
        }

        private bool ValidateBeginningTime()
        {

            string pattern = @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";
            if (BeginningTime == null)
                return false;
            if (!Regex.IsMatch(BeginningTime, pattern))
            {
                return false;
            }

            return true;
        }

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string BeginningTime { get; set; }
        public string Description { get; set; }
        public int? ShootingAreaID { get; set; }
        public ShootingArea ShootingArea { get; set; }
        public int? EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public string Error => throw new NotImplementedException();
    }
}
