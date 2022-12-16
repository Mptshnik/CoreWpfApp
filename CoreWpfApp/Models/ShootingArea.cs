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
    public class ShootingArea : IDataErrorInfo
    {
        public string this[string columnName] 
        { 
            get 
            {
                string error = string.Empty;

                switch(columnName)
                {
                    case "RentPerHour":
                        if (!ValidateRentPerHour())
                            error = "Стоимость аренды в час должна быть больше 0";
                        break;
                    case "Square":
                        if (!ValidateSquare())
                            error = "Площадь должна быть больше 0";
                        break;
                    case "Name":
                        if (!ValidateName())
                            error = "Наименование должно содержать буквы русского или латинского алфвавита или цифры 0-9";
                        break;
                }

                return error;
            } 
        }

        public bool IsValid()
        {
            return ValidateRentPerHour() && ValidateSquare();
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


        private bool ValidateSquare()
        {
            if(Square <=0)
            {
                return false;
            }

            return true;
        }



        private bool ValidateRentPerHour()
        {
            if(RentPerHour <= 0)
            {
                return false;
            }

            return true;
        }

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public double RentPerHour { get; set; }
        public string AreaType { get; set; }
        public string LevelOfDifficulty { get; set; }
        public double Square { get; set; }
        public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

        public string Error => throw new NotImplementedException();
    }
}
