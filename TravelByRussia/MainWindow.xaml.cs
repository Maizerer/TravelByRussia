using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace TravelByRussia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.fraim = MainFrame;
            MainFrame.Navigate(new IntroPage());
            this.DataContext = new { Title = Manager.fraimTitle };
        }
        private void importTours()
        {
            var fileData = File.ReadAllLines(@"C:\Users\maize\Desktop\ДЭ\import\Туры.txt");

            var images = Directory.GetFiles(@"C:\Users\maize\Desktop\ДЭ\import\Туры фото");

            foreach (var line in fileData)
            {
                var data = line.Split('\t');

                Tour newTour = new Tour
                {
                    Name = data[0],
                    TicketCount = int.Parse(data[2]),
                    Price = int.Parse(data[3]),
                    IsActual = (int.Parse(data[4]) == 1) ? true : false
                };

                foreach (var tourType in data[5].Split(new String[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var currentType = TourFirmEntities.GetContext().Types.ToList().FirstOrDefault(p => p.Name == tourType);

                    if(currentType != null)
                    {
                        newTour.Types.Add(currentType);
                    }
                }
                try
                {
                    newTour.ImagePreview = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(newTour.Name)));
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                TourFirmEntities.GetContext().Tours.Add(newTour);
                TourFirmEntities.GetContext().SaveChanges();
            }
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            this.DataContext = new { Title = Manager.fraimTitle };
            if (MainFrame.CanGoBack)
            {
                GoBack.Visibility = Visibility.Visible;
            }
            else
            {
                Manager.fraimTitle = "Главная страница";
                this.DataContext = new { Title = Manager.fraimTitle };
                GoBack.Visibility = Visibility.Hidden;
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            string temp = Manager.lastTitle;
            Manager.lastTitle = Manager.fraimTitle;
            Manager.fraimTitle = temp;
            this.DataContext = new { Title = Manager.fraimTitle };
            MainFrame.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.lastTitle = Manager.fraimTitle;
            MainFrame.Navigate(new Hotels());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Manager.lastTitle = Manager.fraimTitle;
            MainFrame.Navigate(new AddEditHotel("Добавление отеля", null));
        }

    }
}
