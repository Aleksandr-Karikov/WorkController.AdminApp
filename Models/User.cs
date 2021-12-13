using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkControllerAdmin.Http.RequstModels;
using WorkController.Common;
using WorkControllerAdmin.Http.Helper.ApiHelper;
using WorkControllerAdmin.Http.Helper;

namespace WorkControllerAdmin.Models
{
    public  class User : INotifyPropertyChanged
    {
        public User(IHttpClientFactory _factory)
        {
            factory = _factory;
        }
        public User(){}
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

    }
}
