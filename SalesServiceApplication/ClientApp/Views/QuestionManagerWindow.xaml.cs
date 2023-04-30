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
    /// Логика взаимодействия для QuestionManagerWindow.xaml
    /// </summary>
    public partial class QuestionManagerWindow : Window
    {
        public QuestionManagerWindow(Client client, Question? question, QuestionService questionService)
        {
            InitializeComponent();
            var viewModel = new QuestionManagerViewModel(client, question, questionService);
            if (question == null)
            {
                Title = "Создание вопроса";
                ActionButton.Content = "Создать";
                ActionButton.Command = viewModel.AddQuestionButton;
            }
            else
            {
                Title = "Редактирование вопроса";
                ActionButton.Content = "Сохранить";
                ActionButton.Command = viewModel.EditQuestionButton;
            }
            DataContext = viewModel;
        }
    }
}
