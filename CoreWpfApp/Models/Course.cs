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
    public class Course : IDataErrorInfo
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
                            error = "Наименование должно содержать буквы русского или латинского алфвавита или цифры 0-9";
                        break;
                    case "Price":
                        if (!ValidatePrice())
                            error = "Стоимость должна быть больше 0";
                        break;
                    case "Description":
                        if (!ValidateDescription())
                            error = "Описание обязательно";
                        break;
                }

                return error;
            }
        }

        public bool IsValid()
        {
            return ValidateName() && ValidateDescription() && ValidatePrice();
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

        private bool ValidatePrice()
        {
            if(Price < 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateDescription()
        {
            if(Description == null)
            {
                return false;
            }

            return true;
        }

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int? GunID { get; set; }
        public Gun Gun { get; set; }
        public virtual ICollection<Cheque> Cheques { get; set; } = new List<Cheque>();
        public string Error => throw new NotImplementedException();
    }
}
