using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkController.Admin.Models;
using WorkControllerAdmin.Models;
using WorkControllerAdmin.ViewModels.BaseViewModels;

namespace WorkControllerAdmin.ViewModels
{
    internal class MainWindowViewModel: BaseViewModel
    {
        public MainWindowViewModel(User user)
        {
            this.user = user;
            Update();
        }
        private User user;
        public ObservableCollection<Employee> Employees { get; set; } =
            new ObservableCollection<Employee> { };
        public void Update()
        {
            Employees.Add(new Employee()
            {
                LastName = "test",
                FirstName = "test",
                Email = "test"
            });
            Employees.Add(new Employee()
            {
                LastName = "test1",
                FirstName = "test1",
                Email = "test1"
            });
            Employees.Add(new Employee()
            {
                LastName = "test2",
                FirstName = "test2",
                Email = "test2"
            });
       
        }

    }
}
