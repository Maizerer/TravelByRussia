using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    {
        public ToursPage()
        {
            InitializeComponent();
            Manager.fraimTitle = "Список туров";
            
            var allTypes = TourFirmEntities.GetContext().Types.ToList();
            allTypes.Insert(0, new Type {
                Name = "Все типы"
            });
            sumPrice.Text = "2000 РУБ";
            selectTour.ItemsSource = allTypes;
            selectTour.SelectedIndex = 0;
            checkIsActual.IsChecked = true;

            UpdateTours();
        }
        private void UpdateTours()
        {
            var currentTours = TourFirmEntities.GetContext().Tours.ToList();

            if(selectTour.SelectedIndex > 0)
            {
                currentTours = currentTours.Where(p => p.Types.Contains(selectTour.SelectedItem as Type)).ToList();
            }

            currentTours = currentTours.Where(p => p.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            if(checkIsActual != null)
            {
                if (checkIsActual.IsChecked.Value)
                {
                    currentTours = currentTours.Where(p => p.IsActual).ToList();
                }
            }
            

            if (sortComboBox.SelectedIndex == 0)
            {
                currentTours = currentTours.OrderBy(p => p.Price).ToList();
            }
            else
            {
                currentTours = currentTours.OrderByDescending(p => p.Price).ToList();
            }
            decimal tourPricesum = 0;

            foreach(Tour tour in currentTours)
            {
                decimal tourPrice = tour.Price * Convert.ToDecimal(tour.TicketCount);

                tourPricesum += tourPrice;
            }

            sumPrice.Text = tourPricesum.ToString("N2") + " РУБ";
            if(ToursListView != null)
            {
                ToursListView.ItemsSource = currentTours;
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTours();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }
    }
}
