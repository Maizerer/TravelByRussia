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
        /// <summary>
        /// Установка заголовка Фрейму
        /// Получаем типы туров из БД
        /// Добавляем в типы туров "Все типы"
        /// Присваиваем типы полученные из БД комбобоксу
        /// Устанавливаем как выбранный элемент "Все типы"
        /// Устанавливаем Чекбокс в checked значение
        /// Выводим туры с учетом фильтров
        /// </summary>
        public ToursPage()
        {
            InitializeComponent();
            Manager.fraimTitle = "Список туров";

            var allTypes = TourFirmEntities.GetContext().Types.ToList();
            allTypes.Insert(0, new Type {
                Name = "Все типы"
            });

            selectTour.ItemsSource = allTypes;
            selectTour.SelectedIndex = 0;
            checkIsActual.IsChecked = true;
            UpdateTours();
        }
        /// <summary>
        /// Получаем туры из БД
        /// Если выбран элемент не "Все типы", а какой-то определеенный то выводим туры только этого типа, иначе выводятся все
        /// Сравниваем имена полученных туров с текстом в строке поиска, если есть сопадения со строкой поиска то оставляем только их.
        /// Если установлен чекбокс, то выводим только актуальные туры
        ///  Если у комбобоксе сортировки установлен первый элемент, то сортируем по возрастанию цены
        ///  Иначе выводим в порядке убывания цены
        ///  Считаем полную стоимость всех выбранных туров с учетом кол-ва билетов
        ///  У каждого отеля умножаем Цену на Кол-во билетов и прибавляем в общую переменную tourPricesum
        ///  Выводим в TextBlock сумму всех туров с 2 знаками после запятой
        ///  Добавление туров в ListView
        /// </summary>
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
        /// <summary>
        /// Обновление туров по событию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTours();
        }
        /// <summary>
        /// Обновление туров по событию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }
        /// <summary>
        /// Обновление туров по событию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }
        /// <summary>
        /// Обновление туров по событию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }
        /// <summary>
        /// Обновление туров по событию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }
    }
}
