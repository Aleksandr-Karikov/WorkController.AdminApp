using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WorkControllerAdmin.ViewModels;

namespace WorkControllerAdmin.Views
{
    /// <summary>
    /// Логика взаимодействия для RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterView(IHttpClientFactory factory)
        {
            InitializeComponent();
            DataContext = new RegisterViewModel(factory);
        }
    }
}
