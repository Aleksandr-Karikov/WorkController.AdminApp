using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkController.Admin.Models;
using WorkController.Admin.Views;
using WorkControllerAdmin.Commands;
using WorkControllerAdmin.Models;
using WorkControllerAdmin.ViewModels.BaseViewModels;

namespace WorkController.Admin.ViewModels
{
    public class EmployeeViewModel:BaseViewModel
    {
        public Employee Employee { get;private set; }
        public User User { get; private set; }
        private DateTime selectedDate = DateTime.Now;
        private DateTime dateStart = DateTime.Now;
        private DateTime dateEnd = DateTime.Now;
        private int period;
        private bool radioButtonFilter;
        private int moneyPerHour;
        private Time currentTime;
        public Time CurrentTime
        {
            get => currentTime;
            set
            {
                currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }
        public bool RadioButtonFilter
        {
            get => radioButtonFilter;
            set
            {
                if (radioButtonFilter)
                    radioButtonFilter = false;
                else radioButtonFilter = true;
                OnPropertyChanged(nameof(RadioButtonFilter));
                UpdateCurent();
            }
        }
        public int MoneyPerHour
        {
            get => moneyPerHour;
            set
            {

                if (value != 0)
                {
                    moneyPerHour = value;
                    OnPropertyChanged(nameof(MoneyPerHour));
                }
                
            }
        }
        public int Period
        {
            get => period;
            set
            {

                if (value != 0)
                {
                    period = value;
                    OnPropertyChanged(nameof(Period));
                }

            }
        }
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        public DateTime DateStart
        {
            get => dateStart;
            set
            {
                dateStart = value;
                OnPropertyChanged(nameof(DateStart));
            }
        }
        public DateTime DateEnd
        {
            get => dateEnd;
            set
            {
                dateEnd = value;
                OnPropertyChanged(nameof(DateEnd));
            }
        }
        public EmployeeViewModel(Employee employee, User user)
        {
            this.Employee = employee;
            this.User = user;
            SetMoneyCommand = new LamdaCommand(OnSetMoneyCommandExecute, CanSetMoneyCommandExecute);
            SetPeriodCommand = new LamdaCommand(OnSetPeriodCommandExecute, CanSetPeriodCommandExecute);
            CountCommand = new LamdaCommand(OnCountCommandExecute, CanCountCommandExecute);
            DoubleClickCommand = new LamdaCommand(OnDoubleClickCommandExecute, CanDoubleClickCommandExecute);
            if (employee.MoneyPerHour != 0)
            {
                MoneyPerHour = employee.MoneyPerHour;
            }
            if (employee.ScreenShotPeriod != 0)
            {
                Period = employee.ScreenShotPeriod;
            }
            Update();
        }

        public ObservableCollection<Time> Times { get; set; } =
            new ObservableCollection<Time> { };
        public ObservableCollection<Time> CurentTimes { get; set; } =
            new ObservableCollection<Time> { };
        public async void Update()
        {
            Times.Clear();
            var times = await Employee.GetTimes(User.Factory, User.Token);
            if (times != null)
                foreach (var time in times)
                {
                    Times.Add(time);
                }

            UpdateCurent();
        }
        private void UpdateCurent()
        {
            CurentTimes.Clear();
            if (!RadioButtonFilter)
            {
                foreach (var time in Times)
                {
                    CurentTimes.Add(time);
                }
                return;
            }
            foreach (var time in Times)
            {
                if (SelectedDate.Date == time.DateTime.Date )
                {
                    CurentTimes.Add(time);
                }
            }
        }
        private string title;
        public string Title
        {
            get => Employee.LastName + " " + Employee.FirstName;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }


        public ICommand SetMoneyCommand { get; }
        private bool CanSetMoneyCommandExecute(object p) => true;
        private async void OnSetMoneyCommandExecute(object p)
        {
            if (MoneyPerHour > int.MaxValue || MoneyPerHour < 0)
            {
                MessageBox.Show("Некорректная зарплата");
                return;
            }
            try
            {
                await Employee.SetMoney(User.Factory, User.Token, MoneyPerHour);
                Employee.MoneyPerHour = MoneyPerHour;
                MessageBox.Show("Зарплата за час установлена");
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Отсутствует подключение к серверу");
            }
        }
        public ICommand SetPeriodCommand { get; }
        private bool CanSetPeriodCommandExecute(object p) => true;
        private async void OnSetPeriodCommandExecute(object p)
        {
            if (MoneyPerHour > int.MaxValue || MoneyPerHour < 0)
            {
                MessageBox.Show("Некорректный период");
                return;
            }
            try
            {
                await Employee.SetPeriod(User.Factory, User.Token, Period);
                Employee.MoneyPerHour = MoneyPerHour;
                MessageBox.Show("Период установлена");
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Отсутствует подключение к серверу");
            }
        }
        public ICommand CountCommand { get; }
        private bool CanCountCommandExecute(object p) => true;
        private void OnCountCommandExecute(object p)
        {
            var sum = 0;
            foreach (var time in Times)
            {
                if (time.DateTime.Date >= DateStart.Date && time.DateTime.Date<=dateEnd.Date)
                {
                    sum += time.Milleseconds;
                }
            }
            TimeSpan rez = TimeSpan.FromMilliseconds(sum);
            MessageBox.Show((Math.Round(rez.TotalHours * MoneyPerHour).ToString()) + " заработал даннный сотрудник\n за выбранный перод времени");
        }

        public ICommand DoubleClickCommand { get; }
        private bool CanDoubleClickCommandExecute(object p) => true;
        private void OnDoubleClickCommandExecute(object p)
        {
            new ScreenShots(this,CurrentTime.DateTime.Date).Show();
        }
    }
}
