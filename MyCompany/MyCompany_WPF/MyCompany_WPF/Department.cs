using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MyCompany_WPF
{
    class Department : INotifyPropertyChanged
    {
        public string DepartmentName{ get; set; }
        public int DepartmentID { get; set; }
       

        public Department(string DepName, int DepID)
        {
            DepartmentName = DepName;
            DepartmentID = DepID;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string DepartmentName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new
                PropertyChangedEventArgs(DepartmentName));
        }
        public override string ToString()
        {
            return $"{DepartmentName,10} {DepartmentID,3}";
        }
    }
}
