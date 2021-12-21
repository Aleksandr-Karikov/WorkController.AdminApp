using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using WorkControllerAdmin.ViewModels.BaseViewModels;

namespace WorkController.Admin.ViewModels
{
    internal class ScreenShotsViewModel:BaseViewModel
    {
        private int width;
        public int Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        private int height;
        public int Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        private EmployeeViewModel EmployeeVM;
        private BitmapImage selectedImage;
        public ScreenShotsViewModel(EmployeeViewModel employeeVM,DateTime date)
        {
            this.EmployeeVM = employeeVM;
            Width = (int)SystemParameters.FullPrimaryScreenWidth; 
            Height = (int)SystemParameters.FullPrimaryScreenHeight;
            Load(date);
        }
        private async void Load(DateTime date)
        {
            var screens = await EmployeeVM.Employee.GetScreenshots(EmployeeVM.User.Factory, EmployeeVM.User.Token, date.Date);
            if (screens == null || screens.Count()<=0)
            {
                MessageBox.Show("Нет данных");
                return;
            }
            foreach (var screen in screens)
            {
                ImagesSource.Add(ConvertBinaryToImage(screen));
            }

            SelectedImage = ImagesSource[0];
        }
        private static BitmapImage ConvertBinaryToImage(byte[] date)
        {
            MemoryStream stream = new MemoryStream(date);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
        public ObservableCollection<BitmapImage> ImagesSource { get; set; } =
            new ObservableCollection<BitmapImage> { };

        public BitmapImage SelectedImage
        {
            get => selectedImage;
            set
            {
                selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }
    }
}
