using CoreWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CoreWpfApp.Views.Windows
{
    public partial class AuthorizationWindow : Window
    {
        private DatabaseContext _context;
        public AuthorizationWindow()
        {
            InitializeComponent();

            _context = new DatabaseContext();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = textLogin.Text;
            string password = textPassword.Password;

            Employee employee = _context.Employees.Where(c => c.Login == login && c.Password == password).FirstOrDefault();
            if(employee != null)
            {

                MainWindow mainWindow = new MainWindow();
                mainWindow.AuthenticateEmployee(employee);
                _context.Dispose();
                Close();

            }
            else
            {
                MessageBox.Show("Неверно введен логин или пароль");
            }

        }

      
    }
}
