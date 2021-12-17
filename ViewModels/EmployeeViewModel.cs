using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorkController.Admin.Models;
using WorkControllerAdmin.Models;
using WorkControllerAdmin.ViewModels.BaseViewModels;

namespace WorkController.Admin.ViewModels
{
    internal class EmployeeViewModel:BaseViewModel
    {
        private Employee employee;
        private User user;
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                UpdateCurent();
            }
        }
        public EmployeeViewModel(Employee employee, User user)
        {
            this.employee = employee;
            this.user = user;
            Update();
        }

        public ObservableCollection<Time> Times { get; set; } =
            new ObservableCollection<Time> { };
        public ObservableCollection<Time> CurentTimes { get; set; } =
            new ObservableCollection<Time> { };
        public async void Update()
        {
            Times.Clear();
            var time = await employee.GetTimes(user.Factory,user.Token);
            if (time != null)
                foreach (var emp in time)
                {
                    Times.Add(emp);
                }

            UpdateCurent();
        }
        private void UpdateCurent()
        {
            CurentTimes.Clear();
            if (SelectedDate == DateTime.MinValue )
            {
                foreach (var time in Times)
                {
                    CurentTimes.Add(time);
                }
                return;
            }
            foreach (var time in Times)
            {
                if (SelectedDate == time.DateTime )
                {
                    CurentTimes.Add(time);
                }
            }
        }
        private string title;
        public string Title
        {
            get => employee.LastName + " " + employee.FirstName;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
    }
}
