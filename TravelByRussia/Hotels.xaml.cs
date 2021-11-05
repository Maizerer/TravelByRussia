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
    /// Логика взаимодействия для Hotels.xaml
    /// </summary>
    public partial class Hotels : Page
    {
        /// <summary>
        /// Инициализация
        /// Добавление отелеий из БД в DataGrid
        /// Установка заголовка фрейму
        /// </summary>
        public Hotels()
        {
            InitializeComponent();
            hotelGrid.ItemsSource = TourFirmEntities.GetContext().Hotels.ToList();
            Manager.fraimTitle = "Список доступных отелей";
        }
        /// <summary>
        /// Функция обновления отелей
        /// </summary>
        private void RefreshHotels()
        {
            hotelGrid.ItemsSource = TourFirmEntities.GetContext().Hotels.ToList();
        }
        /// <summary>
        /// Открывает страницу редактирования отеля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.fraim.Navigate(new AddEditHotel("Редактирование отеля", (sender as Button).DataContext as Hotel));
        }
        /// <summary>
        /// Функция удаления отеля, перед тем как удалить спрашивает выбор пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Hotel selectedHotel = (sender as Button).DataContext as Hotel;
            string hotelName = selectedHotel.Name;
            var result = MessageBox.Show($"Вы уверены, хотите удалить отель {hotelName} ?", "Удаление отеля", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    TourFirmEntities.GetContext().Hotels.Remove(selectedHotel);
                    TourFirmEntities.GetContext().SaveChanges();
                    MessageBox.Show("Отель удалён", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshHotels();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
