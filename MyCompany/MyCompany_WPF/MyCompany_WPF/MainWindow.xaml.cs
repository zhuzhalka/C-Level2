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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace MyCompany_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Employee> EmployeeDB = new ObservableCollection<Employee>();
        ObservableCollection<Department> DepartmentDB = new ObservableCollection<Department>();
        Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            CreateDep();
        }
        void CreateDep()
        {
            for (int i = 0; i < 5; i++)
            {
                DepartmentDB.Add(new Department("Department" + i, i));
                for (int j = 0; j < 10; j++)
                {
                    EmployeeDB.Add(new Employee("Name" + i + j, "Surname" + i + j, rnd.Next(18, 65), i));
                }
            }
            //DepartmentLW.ItemsSource = DepartmentDB;
            EmployeeLW.ItemsSource = EmployeeDB;
            DepartmentLV.ItemsSource = DepartmentDB;

        }

        private void DepartmentLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EmployeeLW.ItemsSource = EmployeeDB.Where(
                w => w.DepartmentID == (DepartmentLV.SelectedValue as Department)?.DepartmentID
                );
            AddDepBtn.Content = "Изменить имя";
            //(DepartmentLV.SelectedItem as Department).DepartmentName = AddDepertmentTxtBox.Text;
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(AddEmployeeBtn.Content) == "Изменить данные")
            {
                (EmployeeLW.SelectedItem as Employee).FirstName = AddEmloyeeFNameTxtBlk.Text;
                (EmployeeLW.SelectedItem as Employee).LastName = AddEmloyeeLNameTxtBlk.Text;
                (EmployeeLW.SelectedItem as Employee).Age = Convert.ToInt32(AddEmloyeeAgeTxtBlk.Text);
            }
            else
            EmployeeDB.Add(new Employee(AddEmloyeeFNameTxtBlk.Text, AddEmloyeeLNameTxtBlk.Text, Convert.ToInt32(AddEmloyeeAgeTxtBlk.Text),
                (DepartmentLV.SelectedValue as Department).DepartmentID));
           
        }

        private void AddDepBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(AddDepBtn.Content) == "Изменить имя")
            {
                (DepartmentLV.SelectedItem as Department).NotifyPropertyChanged(AddDepertmentTxtBox.Text);
                DepartmentLV.UpdateLayout();
            }
            else
                DepartmentDB.Add(new Department(AddDepertmentTxtBox.Text, DepartmentDB.Last().DepartmentID + 1));

            //AddDepWindow addDepWindow = new AddDepWindow();
            //addDepWindow.Owner = this;
            //addDepWindow.Show();
        }

        private void EmployeeLW_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddEmloyeeFNameTxtBlk.Text = (EmployeeLW.SelectedItem as Employee)?.FirstName;
            AddEmloyeeLNameTxtBlk.Text = (EmployeeLW.SelectedItem as Employee)?.LastName;
            AddEmloyeeAgeTxtBlk.Text = Convert.ToString((EmployeeLW.SelectedItem as Employee)?.Age);
            AddEmployeeBtn.Content = "Изменить данные";
        }

        private void BtnDelDepartment_Click(object sender, RoutedEventArgs e)
        {
            DepartmentDB.RemoveAt(DepartmentDB.IndexOf((DepartmentLV.SelectedItem as Department)));
            foreach (Employee w in EmployeeDB)
            { 
                if (w.DepartmentID == (DepartmentLV.SelectedItem as Department)?.DepartmentID)
                    EmployeeDB.Remove(w);
            }
            EmployeeLW.ItemsSource = EmployeeDB;
        }
    }
}

