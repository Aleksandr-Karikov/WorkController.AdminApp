using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkController.Admin.Http.RequstModels;
using WorkController.Admin.Models;
using WorkController.Admin.Views;
using WorkController.Common.Http.Helper;
using WorkControllerAdmin.Commands;
using WorkControllerAdmin.Models;
using WorkControllerAdmin.ViewModels.BaseViewModels;

namespace WorkControllerAdmin.ViewModels
{
    internal class MainWindowViewModel: BaseViewModel
    {
        public  MainWindowViewModel(User user)
        {
            this.user = user;
            AddCommand = new LamdaCommand(OnAddCommandExecute, CanAddCommandExecute);
            CloseCommand = new LamdaCommand(OnCloseCommandExecute, CanCloseCommandExecute);
            HistoryCommand = new LamdaCommand(OnHistoryCommandExecute, CanHistoryCommandExecute);
            Update();
            
        }
        private Employee curentEmployee;

        public Employee CurentEmployee
        {
            get => curentEmployee;
            set
            {
                curentEmployee = value;
                Selected = curentEmployee.LastName + " " + curentEmployee.FirstName;
                OnPropertyChanged(nameof(CurentEmployee));
            }
        }
        private string selected;
        public string Selected
        {
            get => selected;
            set
            {
                selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        private string find;
        public string Find
        {
            get => find;
            set
            {
                find = value;
                OnPropertyChanged(nameof(Find));
                UpdateCurent();
            }
        }
        private string addEmail;
        public string AddEmail
        {
            get => addEmail;
            set
            {
                addEmail = value;
                OnPropertyChanged(nameof(AddEmail));
            }
        }
        private User user;
        public ObservableCollection<Employee> Employees { get; set; } =
            new ObservableCollection<Employee> { };
        public ObservableCollection<Employee> CurentEmployees { get; set; } =
            new ObservableCollection<Employee> { };
        public async void Update()
        {
            Employees.Clear();
            var emps = await user.GetEmployees();
            if (emps != null)
                foreach(var emp in emps)
                {
                    Employees.Add(emp);
                }

            UpdateCurent();
        }
        private void UpdateCurent()
        {
            CurentEmployees.Clear();
            if (string.IsNullOrEmpty(Find))
            {
                foreach (var emp in Employees)
                {
                    CurentEmployees.Add(emp);
                }
                return;
            }
            foreach (var emp in Employees)
            {
                if (string.IsNullOrEmpty(emp.LastName) && string.IsNullOrEmpty(emp.FirstName))
                    continue;
                if (emp.Email.ToUpper().Contains(Find.ToUpper())  || emp.LastName.ToUpper().Contains(Find.ToUpper()) || emp.FirstName.ToUpper().Contains(Find.ToUpper()))
                {
                    CurentEmployees.Add(emp);
                }
            }
        }
        public ICommand AddCommand { get; }
        private bool CanAddCommandExecute(object p) => true;
        private async void OnAddCommandExecute(object p)
        {
            if (string.IsNullOrEmpty(AddEmail))
            {
                MessageBox.Show("Поле почта пустое");
                return;
            }
            if (!RequestHelper.IsValidEmail(AddEmail))
            {
                MessageBox.Show("Email не корректен");
                return;
            }
            await user.SetNewWorker(new SetEmployee()
            {
                Email = addEmail,
                ChiefId = user.ID
            });
            Update();
        }

        public ICommand CloseCommand { get; }
        private bool CanCloseCommandExecute(object p)
        {
            return true;
        }
        private void OnCloseCommandExecute(object p)
        {
            Application.Current.Shutdown();
        }

        public ICommand HistoryCommand { get; }
        private bool CanHistoryCommandExecute(object p)
        {
            return true;
        }
        private void OnHistoryCommandExecute(object p)
        {
            if (CurentEmployee == null)
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }
            if (string.IsNullOrEmpty(CurentEmployee.FirstName))
            {
                MessageBox.Show("Пользователь не зарегестрирован");
                return;
            }
            new EmployeeView(CurentEmployee,user).Show();
        }
    }
}
