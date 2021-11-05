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
    /// Логика взаимодействия для AddEditHotel.xaml
    /// </summary>
    public partial class AddEditHotel : Page
    {
        private Hotel _currentHotel = new Hotel();
        /// <summary>
        /// Если в параметр передан отель, то он добавляется в контекст данных
        /// и поля формы заполняются
        /// В комбобокс выбора страны загружаются страны из БД
        /// </summary>
        /// <param name="title"></param>
        /// <param name="hotel"></param>
        public AddEditHotel(string title, Hotel hotel)
        {
            InitializeComponent();
            if(hotel != null)
            {
                _currentHotel = hotel;
            }
            DataContext = _currentHotel;
            Manager.fraimTitle = title;
            countryCombo.ItemsSource = TourFirmEntities.GetContext().Countries.ToList();
        }
        /// <summary>
        /// Валидация введеных данных с формы
        /// и реадктирование или создание отеля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentHotel.Name))
            {
                errors.AppendLine("• Укажите название отеля");
            }
            if (_currentHotel.CountOfStars < 1 || _currentHotel.CountOfStars > 5)
            {
                errors.AppendLine("• Количество звёзд от 1 до 5");
            }
            if (_currentHotel.Country == null)
            {
                errors.AppendLine("• Выберите страну");
            }

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_currentHotel.Id == 0)
                TourFirmEntities.GetContext().Hotels.Add(_currentHotel);

            try
            {
                TourFirmEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.fraim.Navigate(new Hotels());
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Запрет ввода букв и других символов в текстбокс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
    }
}
