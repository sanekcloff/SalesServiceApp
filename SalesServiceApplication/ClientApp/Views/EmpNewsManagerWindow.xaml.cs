using AppData.Entities;
using AppData.Services;
using ClientApp.ViewModels;
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
using System.Windows.Shapes;

namespace ClientApp.Views
{
    /// <summary>
    /// Логика взаимодействия для EmpNewsManagerWindow.xaml
    /// </summary>
    public partial class EmpNewsManagerWindow : Window
    {
        public EmpNewsManagerWindow(Employee employee, New? @new, NewsService newsService)
        {
            InitializeComponent();
            var viewModel = new EmpNewsManagerViewModel(employee, @new, newsService, this);
            if (@new == null)
            {
                Title = "Публикация новости";
                ActionButton.Content = "Опубликовать";
                ActionButton.Command = viewModel.AddNewButton;
            }
            else
            {
                Title = "Редактирование новости";
                ActionButton.Content = "Сохранить";
                ActionButton.Command = viewModel.EditNewButton;
            }
            DataContext = viewModel;
        }
    }
}
