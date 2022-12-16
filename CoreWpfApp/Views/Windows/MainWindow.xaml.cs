using CoreWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoreWpfApp
{
    public partial class MainWindow : Window
    {
        public static Employee CurrentEmployee;
        private List<TabItem> _tabItems;

        public MainWindow()
        {
            InitializeComponent();
            Language = XmlLanguage.GetLanguage("ru-RU");
        }

        public void AuthenticateEmployee(Employee employee)
        {
            _tabItems = new List<TabItem>();
            foreach (var item in tabControl.Items)
                _tabItems.Add((TabItem)item);

            CurrentEmployee = employee;
            if(employee.Role != null)
            {
                if(employee.Role == 0)
                {

                }
                else if(employee.Role == 1)
                {
                    AddTabs(new List<string>() { "Cheques" });
                }
                else if (employee.Role == 2)
                {
                    AddTabs(new List<string>() { "Events", "Areas" });
                }
                else if (employee.Role == 3)
                {
                    AddTabs(new List<string>() { "Employees", "Posts" });
                }
                else if (employee.Role == 4)
                {
                    AddTabs(new List<string>() { "Equipment" });
                }
                else if (employee.Role == 5)
                {
                    AddTabs(new List<string>() { "Weapons" });
                }
                Show();
            }
        }

        private void AddTabs(List<string> tabNames)
        {
            tabControl.Items.Clear();
            foreach(string tabName in tabNames)
            {
                TabItem tab = _tabItems.First(c => c.Name == tabName);               
                tabControl.Items.Add(tab);
            }
        }
    }
}
