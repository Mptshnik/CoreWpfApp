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
    public class Equipment : IDataErrorInfo
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
                    case "Amount":
                        if (!ValidateName())
                            error = "Количество должно быть больше 0";
                        break;
                }

                return error;
            }
            
        }

        public bool IsValid()
        {
            return ValidateAmount() && ValidateName();
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

        private bool ValidateAmount()
        {
            if(Amount <= 0)
            {
                return false;
            }

            return true;
        }

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public virtual ICollection<ShootingArea> ShootingAreas { get; set; } = new List<ShootingArea>();

        public string Error => throw new NotImplementedException();
    }
}
