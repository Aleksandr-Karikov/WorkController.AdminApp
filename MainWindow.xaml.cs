using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkControllerAdmin.Http;
using WorkControllerAdmin.Http.Helper;
using WorkControllerAdmin.Http.Helper.ApiHelper;
using WorkControllerAdmin.Http.RequstModels;

namespace WorkControllerAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IHttpClientFactory clientFactory)
        {
            InitializeComponent();
            this._clientFactory = clientFactory;
        }
        private readonly IHttpClientFactory _clientFactory;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var response = await RequestHelper.SendPostRequest(ApiHelperUri.RegisterUri, _clientFactory,new RegisterModel()
            {
                Email = "test1@gmail.com",
                FirstName = "Andrey",
                LastName ="starkov",
                Password = "12345"
            });
            var receiveStream = await response.Content.ReadAsStreamAsync();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            MessageBox.Show(readStream.ReadToEnd());
        }
    }
}
