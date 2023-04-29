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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientApp.Views
{
    /// <summary>
    /// Логика взаимодействия для QuestionsPage.xaml
    /// </summary>
    public partial class QuestionsPage : UserControl
    {
        public QuestionsPage(Client client, QuestionService questionService)
        {
            InitializeComponent();
            DataContext = new QuestionViewModel(client, questionService);
        }
    }
}
