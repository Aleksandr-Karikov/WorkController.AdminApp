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
using WorkController.Admin.Models;
using Newtonsoft.Json;
using WorkController.Admin.Http.RequstModels;
using WorkController.Common.Http.Helper;
using WorkController.Common.Http.Helper.ApiHelper;

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
        private int id;
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

        public int ID
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(ID));
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


        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            
            var response = await RequestHelper.SendPostAuthRequest(ApiHelperUri.GetEmployeesUri + $"?ID={ID}",factory,Token);
            var content = await response.Content.ReadAsStringAsync();
            var c = JsonConvert.DeserializeObject<List<Employee>>(content);
            return c;
        }
        public async Task SetNewWorker(SetEmployee employee)
        {
            var response = await RequestHelper.SendPostAuthRequest(ApiHelperUri.SetEmployeeUri, factory, Token, employee);
            var content = await response.Content.ReadAsStringAsync();
            MessageBox.Show(content);
        }
    }
}
