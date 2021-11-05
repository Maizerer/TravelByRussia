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
        /// <summary>
        /// Инициализация и направление на Главную страницу
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Manager.fraim = MainFrame;
            MainFrame.Navigate(new IntroPage());
            this.DataContext = new { Title = Manager.fraimTitle };
        }
        /// <summary>
        /// Событие Рендера контента в Фрейме
        /// Кнопка назад появлется, если навигация фрейма может идти назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// По нажатию на кнопку назад возвращаемся на предыдующую страницу и изменяем заголовок фрейма
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            string temp = Manager.lastTitle;
            Manager.lastTitle = Manager.fraimTitle;
            Manager.fraimTitle = temp;
            this.DataContext = new { Title = Manager.fraimTitle };
            MainFrame.GoBack();
        }
        /// <summary>
        ///  Переход к странице "Список отелей"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.lastTitle = Manager.fraimTitle;
            MainFrame.Navigate(new Hotels());
            
        }
        /// <summary>
        /// Переход к странице "Список Туров"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.lastTitle = Manager.fraimTitle;
            MainFrame.Navigate(new ToursPage());
        }
        /// <summary>
        /// Переход к старнице "Добавление отеля"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Manager.lastTitle = Manager.fraimTitle;
            MainFrame.Navigate(new AddEditHotel("Добавление отеля", null));
        }

    }
}
