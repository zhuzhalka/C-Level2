using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MyCompany_WPF
{
    class Employee : INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int DepartmentID { get; set; }
          
        public Employee(string firstName, string lastName, int age, int DepID)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            DepartmentID = DepID;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string FirstName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new
                PropertyChangedEventArgs(FirstName));
        }

        public override string ToString()
        {
            return $"{FirstName, 10} {LastName, 10} {Age, 3} {DepartmentID}";
        }
    }
}
