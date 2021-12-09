using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkControllerAdmin.Http.Helper;
using WorkControllerAdmin.Http.Helper.ApiHelper;
using WorkControllerAdmin.Http.RequstModels;

namespace WorkControllerAdmin.Models
{
    internal class User : INotifyPropertyChanged
    {
        User(IHttpClientFactory _factory)
        {
            factory = _factory;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #region Fields
        private IHttpClientFactory factory;
        private string email;
        private string firstName;
        private string lastName;
        private string token;
        #endregion
        #region Properties
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
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
        public string Token
        {
            get => token;
            set
            {
                token = value;
                OnPropertyChanged(nameof(Token));
            }
        }
        #endregion


        public async Task Login(string password,string Email)
        {
            var response = await RequestHelper.SendPostRequest(ApiHelperUri.LoginUri, factory, new LoginModel()
            {
                Email = Email,
                Password = password
            }) ;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
