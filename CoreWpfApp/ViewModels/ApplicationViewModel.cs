using CoreWpfApp.Commands;
using CoreWpfApp.Models;
using CoreWpfApp.Views.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Syncfusion.Pdf;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.Pdf.Graphics;
using Syncfusion.UI.Xaml.Grid;

namespace CoreWpfApp.ViewModels
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        private DatabaseContext _databaseContext;

        #region Collections
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Post> Posts { get; set; }
        public ObservableCollection<Equipment> Equipment { get; set; }
        public ObservableCollection<ShootingArea> ShootingAreas { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<Gun> Guns { get; set; }
        public ObservableCollection<Cheque> Cheques { get; set; }
        public ObservableCollection<SportEvent> SportEvents { get; set; }

        #endregion

        #region Commands
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ExitCommand { get; set; }
        public RelayCommand AddShootingAreaToEquipmentCommand { get; set; }
        public RelayCommand AddCourseToChequeCommand { get; set; }
        public RelayCommand AddEmployeeCommand { get; }
        public RelayCommand EditEmployeeCommand { get; }
        public RelayCommand DeleteEmployeeCommand { get; }
        public RelayCommand AddPostCommand { get; }
        public RelayCommand EditPostCommand { get; }
        public RelayCommand DeletePostCommand { get; }
        public RelayCommand AddEquipmentCommand { get; }
        public RelayCommand EditEquipmentCommand { get; }
        public RelayCommand DeleteEquipmentCommand { get; }
        public RelayCommand AddGunCommand { get; }
        public RelayCommand EditGunCommand { get; }
        public RelayCommand DeleteGunCommand { get; }
        public RelayCommand AddCourseCommand { get; }
        public RelayCommand EditCourseCommand { get; }
        public RelayCommand DeleteCourseCommand { get; }
        public RelayCommand AddAreaCommand { get; }
        public RelayCommand EditAreaCommand { get; }
        public RelayCommand DeleteAreaCommand { get; }
        public RelayCommand AddSportEventCommand { get; }
        public RelayCommand EditSportEventCommand { get; }
        public RelayCommand DeleteSportEventCommand { get; }
        public RelayCommand AddChequeCommand { get; }
        public RelayCommand EditChequeCommand { get; }
        public RelayCommand DeleteChequeCommand { get; }
        #endregion

        #region Properties

        private Cheque _selectedCheque;
        public Cheque SelectedCheque
        {
            get
            {
                return _selectedCheque;
            }
            set
            {
                _selectedCheque = value;
                OnPropertyChanged("SelectedCheque");
            }
        }

        private SportEvent _selectedSportEvent;
        public SportEvent SelectedSportEvent
        {
            get
            {
                return _selectedSportEvent;
            }
            set
            {
                _selectedSportEvent = value;
                OnPropertyChanged("SelectedSportEvent");
            }
        }
        private ShootingArea _selectedArea;
        public ShootingArea SelectedArea 
        {
            get
            {
                return _selectedArea;
            }
            set
            {
                _selectedArea = value;
                OnPropertyChanged("SelectedArea");
            }
        }

        private Course _selectedCourse;
        public Course SelectedCourse 
        {
            get 
            {
                return _selectedCourse;
            }
            set 
            {
                _selectedCourse = value;
                OnPropertyChanged("SelectedCourse");
            }
        }

        private Gun _selectedGun;
        public Gun SelectedGun
        {
            get
            {
                return _selectedGun;
            }
            set 
            {
                _selectedGun = value;
                OnPropertyChanged("SelectedGun");
            }
        }

        private Equipment _selectedEquipment;
        public Equipment SelectedEquipment
        {
            get
            {
                return _selectedEquipment;
            }
            set
            {
                _selectedEquipment = value;
                OnPropertyChanged("SelectedEquipment");
            }
        }

        private Post _selectedPost;
        public Post SelectedPost
        {
            get
            {
                return _selectedPost;
            }
            set
            {
                _selectedPost = value;
                OnPropertyChanged("SelectedPost");
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public ApplicationViewModel()
        {
            _databaseContext = new DatabaseContext();

            SelectedEmployee = new Employee();
            SelectedPost = new Post();
            SelectedGun = new Gun();
            SelectedEquipment = new Equipment();
            SelectedCourse = new Course();
            SelectedArea = new ShootingArea();
            SelectedSportEvent = new SportEvent();
            SelectedCheque = new Cheque();

            //Employee em = new Employee() { Login="admin", Password="123456", FirstName="FDSF", LastName="sad"};
            //_databaseContext.Employees.Add(em);
            //_databaseContext.SaveChanges();


            Employees = new ObservableCollection<Employee>(_databaseContext.Employees.ToList());
            Posts = new ObservableCollection<Post>(_databaseContext.Posts.ToList());
            Equipment = new ObservableCollection<Equipment>(_databaseContext.Equipment.Include(c => c.ShootingAreas));
            Guns = new ObservableCollection<Gun>(_databaseContext.Guns.ToList());
            Courses = new ObservableCollection<Course>(_databaseContext.Courses.Include(c => c.Gun));
            ShootingAreas = new ObservableCollection<ShootingArea>(_databaseContext.ShootingAreas.Include(c => c.Equipment));
            SportEvents = new ObservableCollection<SportEvent>(_databaseContext.SportEvents.Include(c => c.ShootingArea).Include(c => c.Employee));
            Cheques = new ObservableCollection<Cheque>(_databaseContext.Cheques.Include(c => c.Courses).Include(c => c.Employee));

            AddShootingAreaToEquipmentCommand = new RelayCommand(ExecuteAddShootingAreaToEquipmentCommand,
                CanExecuteAddShootingAreaToEquipmentCommand);
            AddCourseToChequeCommand = new RelayCommand(ExecuteAddCourseToChequeCommand,
                CanExecuteAddCourseToChequeCommand);

            ExportCommand = new RelayCommand(ExecuteExportCommand, CanExecuteExportCommand);

            ExitCommand = new RelayCommand(ExecuteExitCommand);

            AddEmployeeCommand = new RelayCommand(ExecuteAddEmployeeCommand, CanExecuteAddEmployeeCommand);
            EditEmployeeCommand = new RelayCommand(ExecuteEditEmployeeCommand, CanExecuteEditEmployeeCommand);
            DeleteEmployeeCommand = new RelayCommand(ExecuteDeleteEmployeeCommand, CanExecuteDeleteEmployeeCommand);

            AddPostCommand = new RelayCommand(ExecuteAddPostCommand, CanExecuteAddPostCommand);
            EditPostCommand = new RelayCommand(ExecuteEditPostCommand, CanExecuteEditPostCommand);
            DeletePostCommand = new RelayCommand(ExecuteDeletePostCommand, CanExecuteDeletePostCommand);

            AddEquipmentCommand = new RelayCommand(ExecuteAddEquipmentCommand, CanExecuteAddEquipmentCommand);
            EditEquipmentCommand = new RelayCommand(ExecuteEditEquipmentCommand, CanExecuteEditEquipmentCommand);
            DeleteEquipmentCommand = new RelayCommand(ExecuteDeleteEquipmentCommand, CanExecuteDeleteEquipmentCommand);

            AddGunCommand = new RelayCommand(ExecuteAddGunCommand, CanExecuteAddGunCommand);
            EditGunCommand = new RelayCommand(ExecuteEditGunCommand, CanExecuteEditGunCommand);
            DeleteGunCommand = new RelayCommand(ExecuteDeleteGunCommand, CanExecuteDeleteGunCommand);

            AddCourseCommand = new RelayCommand(ExecuteAddCourseCommand, CanExecuteAddCourseCommand);
            EditCourseCommand = new RelayCommand(ExecuteEditCourseCommand, CanExecuteEditCourseCommand);
            DeleteCourseCommand = new RelayCommand(ExecuteDeleteCourseCommand, CanExecuteDeleteCourseCommand);

            AddAreaCommand = new RelayCommand(ExecuteAddAreaCommand, CanExecuteAddAreaCommand);
            EditAreaCommand = new RelayCommand(ExecuteEditAreaCommand, CanExecuteEditAreaCommand);
            DeleteAreaCommand = new RelayCommand(ExecuteDeleteAreaCommand, CanExecuteDeleteAreaCommand);

            AddSportEventCommand = new RelayCommand(ExecuteAddSportEventCommand, CanExecuteAddSportEventCommand);
            EditSportEventCommand = new RelayCommand(ExecuteEditSportEventCommand, CanExecuteEditSportEventCommand);
            DeleteSportEventCommand = new RelayCommand(ExecuteDeleteSportEventCommand, CanExecuteDeleteSportEventCommand);

            AddChequeCommand = new RelayCommand(ExecuteAddChequeCommand, CanExecuteAddChequeCommand);
            EditChequeCommand = new RelayCommand(ExecuteEditChequeCommand, CanExecuteEditChequeCommand);
            DeleteChequeCommand = new RelayCommand(ExecuteDeleteChequeCommand, CanExecuteDeleteChequeCommand);
        }

        #region Additional commands

        private void PdfHeaderFooterEventHandler(object sender, PdfHeaderFooterEventArgs e)
        {
            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 20f, PdfFontStyle.Bold);
            var width = e.PdfPage.GetClientSize().Width;
            PdfPageTemplateElement header = new PdfPageTemplateElement(width, 38);
            header.Graphics.DrawString("Курсы", font, PdfPens.Black, 70, 3);
            e.PdfDocumentTemplate.Top = header;
        }

        private void ExecuteExportCommand(object obj)
        {
            SfDataGrid dataGrid = (SfDataGrid)obj;
            PdfExportingOptions options = new PdfExportingOptions();
            options.PageHeaderFooterEventHandler = PdfHeaderFooterEventHandler;
            var document = dataGrid.ExportToPdf(options);
            string time = DateTime.Now.ToString().Replace(":", "_");
            document.Save(time + " Курсы.pdf");
            MessageBox.Show("Данные успешно экспортированы");
          

        }
        public void ExecuteExitCommand(object obj)
        {
            AuthorizationWindow window = new AuthorizationWindow();
            window.Show();
            MainWindow main = (MainWindow)obj;
            main.Close();
        }

        public void ExecuteAddShootingAreaToEquipmentCommand(object obj)
        {
            try 
            {
                if(SelectedEquipment != null)
                {
                    SelectedEquipment.ShootingAreas.Add((ShootingArea)obj);
                    MessageBox.Show("Добавлено");
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool CanExecuteAddShootingAreaToEquipmentCommand(object obj)
        {
            return ShootingAreas.Count > 0 && obj != null;
        }

        public void ExecuteAddCourseToChequeCommand(object obj)
        {
            try
            {
                if (SelectedCheque != null)
                {
                    SelectedCheque.Courses.Add((Course)obj);
                    MessageBox.Show("Добавлено в чек");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool CanExecuteAddCourseToChequeCommand(object obj)
        {
            return Courses.Count > 0;
        }
        #endregion

        #region Employee commands
        public void ExecuteAddEmployeeCommand(object obj)
        {
            _databaseContext.Employees.Add(SelectedEmployee);
            _databaseContext.SaveChanges();
            Employees.Add(SelectedEmployee);
            SelectedEmployee = new Employee();
        }

        public void ExecuteEditEmployeeCommand(object obj)
        {
            try
            {

                _databaseContext.Employees.Update(SelectedEmployee);
                _databaseContext.SaveChanges();
                SelectedEmployee = new Employee();
                MessageBox.Show("Данные успешно изменены");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ExecuteDeleteEmployeeCommand(object obj)
        {
            _databaseContext.Employees.Remove(SelectedEmployee);
            _databaseContext.SaveChanges();
            Employees.Remove(SelectedEmployee);
            SelectedEmployee = new Employee();
        }

        public bool CanExecuteExportCommand(object obj)
        {
            return Courses.Count > 0;
        }

        public bool CanExecuteDeleteEmployeeCommand(object obj)
        {
            if(SelectedEmployee != null)
            {
                return Employees.Count > 0 && SelectedEmployee.ID != 0;
            }

            return false;
        }

        public bool CanExecuteEditEmployeeCommand(object obj)
        {
            return Employees.Count > 0 && SelectedEmployee != null && SelectedEmployee.IsValid()
                && SelectedEmployee.ID != 0;
        }

        public bool CanExecuteAddEmployeeCommand(object obj)
        {
            if(SelectedEmployee != null)
                return SelectedEmployee.IsValid() && SelectedEmployee.ID == 0;
            return true;
        }
        #endregion

        #region Post commands
        public void ExecuteAddPostCommand(object obj)
        {
            _databaseContext.Posts.Add(SelectedPost);
            _databaseContext.SaveChanges();
            Posts.Add(SelectedPost);
            SelectedPost = new Post();
        }

        public void ExecuteEditPostCommand(object obj)
        {
            try
            {
                _databaseContext.Posts.Update(SelectedPost);
                _databaseContext.SaveChanges();
                MessageBox.Show("Данные успешно изменены");
                SelectedPost = new Post();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ExecuteDeletePostCommand(object obj)
        {
            _databaseContext.Posts.Remove(SelectedPost);
            _databaseContext.SaveChanges();
            Posts.Remove(SelectedPost);
            SelectedPost = new Post();
        }

        public bool CanExecuteDeletePostCommand(object obj)
        {
            if (SelectedPost != null)
            {
                return Posts.Count > 0 && SelectedPost.ID != 0;
            }

            return false;
        }

        public bool CanExecuteEditPostCommand(object obj)
        {
            return Posts.Count > 0 && SelectedPost != null && 
                SelectedPost.IsValid() && SelectedPost.ID != 0;
        }

        public bool CanExecuteAddPostCommand(object obj)
        {
            if (SelectedPost != null)
                return SelectedPost.IsValid() && SelectedPost.ID == 0;
            return true;
        }
        #endregion

        #region Equipment commands
        public void ExecuteAddEquipmentCommand(object obj)
        {
            _databaseContext.Equipment.Add(SelectedEquipment);
            _databaseContext.SaveChanges();
            Equipment.Add(SelectedEquipment);
            SelectedEquipment = new Equipment();
        }

        public void ExecuteEditEquipmentCommand(object obj)
        {
            try
            {
                _databaseContext.Equipment.Update(SelectedEquipment);
                _databaseContext.SaveChanges();
                MessageBox.Show("Данные успешно изменены");
                SelectedEquipment = new Equipment();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ExecuteDeleteEquipmentCommand(object obj)
        {
            _databaseContext.Equipment.Remove(SelectedEquipment);
            _databaseContext.SaveChanges();
            Equipment.Remove(SelectedEquipment);
            SelectedEquipment = new Equipment();
        }

        public bool CanExecuteDeleteEquipmentCommand(object obj)
        {
            if (SelectedEquipment != null)
            {
                return Equipment.Count > 0 && SelectedEquipment.ID != 0;
            }

            return false;
        }

        public bool CanExecuteEditEquipmentCommand(object obj)
        {
            return Equipment.Count > 0 && SelectedEquipment != null && SelectedEquipment.IsValid();
        }

        public bool CanExecuteAddEquipmentCommand(object obj)
        {
            if (SelectedEquipment != null)
                return SelectedEquipment.IsValid() && SelectedEquipment.ID == 0;
            return true;
        }
        #endregion

        #region Gun commands
        public void ExecuteAddGunCommand(object obj)
        {
            _databaseContext.Guns.Add(SelectedGun);
            _databaseContext.SaveChanges();
            Guns.Add(SelectedGun);
            SelectedGun = new Gun();
        }

        public void ExecuteEditGunCommand(object obj)
        {
            try
            {
                _databaseContext.Guns.Update(SelectedGun);
                _databaseContext.SaveChanges();
                MessageBox.Show("Данные успешно изменены");
                SelectedGun = new Gun();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ExecuteDeleteGunCommand(object obj)
        {
            _databaseContext.Guns.Remove(SelectedGun);
            _databaseContext.SaveChanges();
            Guns.Remove(SelectedGun);
            SelectedGun = new Gun();
        }

        public bool CanExecuteDeleteGunCommand(object obj)
        {
            if (SelectedGun != null)
            {
                return Guns.Count > 0 && SelectedGun.ID != 0;
            }

            return false;
        }

        public bool CanExecuteEditGunCommand(object obj)
        {
            return Guns.Count > 0 && SelectedGun != null && SelectedGun.IsValid();
        }

        public bool CanExecuteAddGunCommand(object obj)
        {
            if (SelectedGun != null)
                return SelectedGun.IsValid() && SelectedGun.ID == 0;
            return true;
        }
        #endregion

        #region Course commands
        public void ExecuteAddCourseCommand(object obj)
        {
            _databaseContext.Courses.Add(SelectedCourse);
            _databaseContext.SaveChanges();
            Courses.Add(SelectedCourse);
            SelectedCourse = new Course();
        }

        public void ExecuteEditCourseCommand(object obj)
        {
            try
            {
                _databaseContext.Courses.Update(SelectedCourse);
                _databaseContext.SaveChanges();
                MessageBox.Show("Данные успешно изменены");
                SelectedCourse = new Course();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ExecuteDeleteCourseCommand(object obj)
        {
            _databaseContext.Courses.Remove(SelectedCourse);
            _databaseContext.SaveChanges();
            Courses.Remove(SelectedCourse);
            SelectedCourse = new Course();
        }

        public bool CanExecuteDeleteCourseCommand(object obj)
        {
            if (SelectedCourse != null)
            {
                return Courses.Count > 0 && SelectedCourse.ID != 0;
            }

            return false;
        }

        public bool CanExecuteEditCourseCommand(object obj)
        {
            return Courses.Count > 0 && SelectedCourse != null && SelectedCourse.IsValid();
        }

        public bool CanExecuteAddCourseCommand(object obj)
        {
            if (SelectedCourse != null)
                return SelectedCourse.IsValid() && SelectedCourse.ID == 0;
            return true;
        }
        #endregion

        #region Area commands
        public void ExecuteAddAreaCommand(object obj)
        {
            _databaseContext.ShootingAreas.Add(SelectedArea);
            _databaseContext.SaveChanges();
            ShootingAreas.Add(SelectedArea);
            SelectedArea = new ShootingArea();
        }

        public void ExecuteEditAreaCommand(object obj)
        {
            try
            {
                _databaseContext.ShootingAreas.Update(SelectedArea);
                _databaseContext.SaveChanges();
                MessageBox.Show("Данные успешно изменены");
                SelectedArea = new ShootingArea();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ExecuteDeleteAreaCommand(object obj)
        {
            _databaseContext.ShootingAreas.Remove(SelectedArea);
            _databaseContext.SaveChanges();
            ShootingAreas.Remove(SelectedArea);
            SelectedArea = new ShootingArea();
        }

        public bool CanExecuteDeleteAreaCommand(object obj)
        {
            if (SelectedArea != null)
            {
                return ShootingAreas.Count > 0 && SelectedArea.ID != 0;
            }

            return false;
        }

        public bool CanExecuteEditAreaCommand(object obj)
        {
            return ShootingAreas.Count > 0 && SelectedArea != null && SelectedArea.IsValid();
        }

        public bool CanExecuteAddAreaCommand(object obj)
        {
            if (SelectedArea != null)
                return SelectedArea.IsValid() && SelectedArea.ID == 0;
            return true;
        }
        #endregion

        #region SportEvent commands
        public void ExecuteAddSportEventCommand(object obj)
        {

            SelectedSportEvent.Date = SelectedSportEvent.Date.Replace("00:00:00", "");
            _databaseContext.SportEvents.Add(SelectedSportEvent);
            _databaseContext.SaveChanges();
            SportEvents.Add(SelectedSportEvent);
            SelectedSportEvent = new SportEvent();
        }

        public void ExecuteEditSportEventCommand(object obj)
        {
            try
            {
                SelectedSportEvent.Date = SelectedSportEvent.Date.Replace("00:00:00", "");
                _databaseContext.SportEvents.Update(SelectedSportEvent);
                _databaseContext.SaveChanges();
                MessageBox.Show("Данные успешно изменены");
                SelectedSportEvent = new SportEvent();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ExecuteDeleteSportEventCommand(object obj)
        {
            _databaseContext.SportEvents.Remove(SelectedSportEvent);
            _databaseContext.SaveChanges();
            SportEvents.Remove(SelectedSportEvent);
            SelectedSportEvent = new SportEvent();
        }

        public bool CanExecuteDeleteSportEventCommand(object obj)
        {
            if (SelectedSportEvent != null)
            {
                return SportEvents.Count > 0 && SelectedSportEvent.ID != 0;
            }

            return false;
        }

        public bool CanExecuteEditSportEventCommand(object obj)
        {
            return SportEvents.Count > 0 && SelectedSportEvent != null && SelectedSportEvent.IsValid();
        }

        public bool CanExecuteAddSportEventCommand(object obj)
        {
            if (SelectedSportEvent != null)
                return SelectedSportEvent.IsValid() && SelectedSportEvent.ID == 0;
            return true;
        }
        #endregion

        #region Cheque commands
        public void ExecuteAddChequeCommand(object obj)
        {
            SelectedCheque.Date = DateTime.Now.Date.ToShortDateString();
            if(MainWindow.CurrentEmployee.Role == 1)
            {
                SelectedCheque.EmployeeID = MainWindow.CurrentEmployee.ID;
            }
            _databaseContext.Cheques.Add(SelectedCheque);
            _databaseContext.SaveChanges();
            Cheques.Add(SelectedCheque);
            SelectedCheque = new Cheque();
        }

        public void ExecuteEditChequeCommand(object obj)
        {
            try
            {
                if (MainWindow.CurrentEmployee.Role == 1)
                {
                    SelectedCheque.Employee = MainWindow.CurrentEmployee;
                }
                _databaseContext.Cheques.Update(SelectedCheque);
                _databaseContext.SaveChanges();
                MessageBox.Show("Данные успешно изменены");
                SelectedCheque = new Cheque();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ExecuteDeleteChequeCommand(object obj)
        {
            _databaseContext.Cheques.Remove(SelectedCheque);
            _databaseContext.SaveChanges();
            Cheques.Remove(SelectedCheque);
            SelectedCheque = new Cheque();
        }

        public bool CanExecuteDeleteChequeCommand(object obj)
        {
            if (SelectedCheque != null)
            {
                return Cheques.Count > 0 && SelectedCheque.ID != 0;
            }

            return false;
        }

        public bool CanExecuteEditChequeCommand(object obj)
        {
            return Cheques.Count > 0 && SelectedCheque != null && SelectedCheque.ID != 0;
        }

        public bool CanExecuteAddChequeCommand(object obj)
        {
            return SelectedCheque.ID == 0;
        }
        #endregion

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
