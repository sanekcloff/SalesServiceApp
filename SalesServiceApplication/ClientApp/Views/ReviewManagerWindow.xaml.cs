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
    /// Логика взаимодействия для ReviewManagerWindow.xaml
    /// </summary>
    public partial class ReviewManagerWindow : Window
    {
        public ReviewManagerWindow(Client client, Review? review, ReviewService reviewService)
        {
            InitializeComponent();
            var viewModel = new ReviewManagerViewModel(client, review, reviewService, this);
            if (review == null)
            {
                Title = "Создание отызва";
                ActionButton.Content = "Создать";
                ActionButton.Command = viewModel.AddReviewButton;
            }
            else
            {
                Title = "Редактирование отзыва";
                ActionButton.Content = "Сохранить";
                ActionButton.Command = viewModel.EditReviewButton;
            }
            DataContext = viewModel;
        }
    }
}
