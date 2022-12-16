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
    public class Post : IDataErrorInfo
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
                            error = "Наименование должно состоять из букв русского или латинского алфавита";
                        break;
                }

                return error;
            } 
        }

        public bool IsValid()
        {
            return ValidateName();
        }

        private bool ValidateName()
        {
            string pattern = @"[a-zA-Zа-яА-Я\s]+";
            if (Name == null)
                return false;
            if (!Regex.IsMatch(Name, pattern))
            {
                return false;
            }

            return true;
        }

        [Key] public int ID { get; set; }
        public string Name { get; set; }

        public string Error => throw new NotImplementedException();
    }
}
