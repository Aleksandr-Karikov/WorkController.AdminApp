using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkControllerAdmin.Models;
using WorkControllerAdmin.ViewModels.BaseViewModels;

namespace WorkControllerAdmin.ViewModels
{
    internal class MainWindowViewModel: BaseViewModel
    {
        private User user;
        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        
    }
}
