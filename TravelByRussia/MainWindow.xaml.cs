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
            MainFrame.Navigate(new Hotels());
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
    }
}
