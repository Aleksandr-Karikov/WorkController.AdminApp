using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkController.Common.Http.Helper;
using WorkController.Common.Http.Helper.ApiHelper;
using WorkControllerAdmin.Commands;
using WorkControllerAdmin.Http.RequstModels;
using WorkControllerAdmin.ViewModels.BaseViewModels;
using WorkControllerAdmin.Views;

namespace WorkControllerAdmin.ViewModels
{
    internal class RegisterViewModel:BaseViewModel
    {
        #region ctors
        public RegisterViewModel(IHttpClientFactory _factory)
        {
            factory = _factory;
            LoginCommand = new LamdaCommand(OnLoginCommandExecute, CanLoginCommandExecute);
            RegisterCommand = new LamdaCommand(OnRegisterCommandExecute, CanRegisterCommandExecute);
        }
        #endregion
        #region Fields
        IHttpClientFactory factory;
        private string email;
        private string password;
        private string firstName;
        private string lastName;
        #endregion
        #region Props

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        #endregion
        #region Commands
        public ICommand RegisterCommand { get; }
        private bool CanRegisterCommandExecute(object p)
        {
            return true;
        }
        private async void OnRegisterCommandExecute(object p)
        {
            #region Валидность полей
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Одно из полей пустое");
                return;
            }
            if (password.Length < 6)
            {
                MessageBox.Show("Пароль слишком короткий");
                return;
            }
            if (!RequestHelper.IsValidEmail(Email))
            {
                MessageBox.Show("Email не корректен");
                return;
            }
            #endregion
            try
            {
                var response = await RequestHelper.SendPostRequest(ApiHelperUri.RegisterUri, factory, new RegisterModel()
                {
                    Email = email,
                    Password = password,
                    LastName =lastName,
                    FirstName = firstName
                });
                MessageBox.Show(await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Отсутствует подключение к серверу");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        public ICommand LoginCommand { get; }
        private bool CanLoginCommandExecute(object p) => true;
        private void OnLoginCommandExecute(object p)
        {
            var a = new LoginView(factory);
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.Close();
            a.Show();

        }
        #endregion
    }
}
