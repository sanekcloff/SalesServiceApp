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
    /// Логика взаимодействия для EmpQuestionManagerWindow.xaml
    /// </summary>
    public partial class EmpQuestionManagerWindow : Window
    {
        public EmpQuestionManagerWindow(Employee employee, Question question, QuestionService questionService)
        {
            InitializeComponent();
            DataContext = new EmpQuestionManagerViewModel(employee, question, questionService,this);
        }
    }
}
