using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using WorkControllerAdmin.Commands;
using WorkControllerAdmin.Http.Helper;
using WorkControllerAdmin.Http.Helper.ApiHelper;
using WorkControllerAdmin.Http.RequstModels;
using WorkControllerAdmin.Models;
using WorkControllerAdmin.ViewModels.BaseViewModels;
using WorkControllerAdmin.Views;



namespace WorkControllerAdmin.ViewModels
{
    internal class LoginViewModel:BaseViewModel
    {
        IHttpClientFactory factory;
        private string email;
        private string password;

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
        public ICommand LoginCommand { get; }
        private bool CanLoginCommandExecute(object p) => true;
        private async void OnLoginCommandExecute(object p)
        {
            try
            {
                var response = await RequestHelper.SendPostRequest(ApiHelperUri.LoginUri, factory, new LoginModel()
                {
                    Email = this.Email,
                    Password = this.Password
                });
                MessageBox.Show(await response.Content.ReadAsStringAsync());
            }
            catch(Exception)
            {
                MessageBox.Show("Введенные вами данные не корректны");
            }
            
        }


        public LoginViewModel(IHttpClientFactory _factory)
        {
            factory = _factory;
            LoginCommand = new LamdaCommand(OnLoginCommandExecute, CanLoginCommandExecute);
            RegisterCommand = new LamdaCommand(OnRegisterCommandExecute, CanRegisterCommandExecute);
        }

        public ICommand RegisterCommand { get; }
        private bool CanRegisterCommandExecute(object p) => true;
        private void OnRegisterCommandExecute(object p)
        {
            var a = new RegisterView();
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.Close();
            a.Show();

        }


    }
}
