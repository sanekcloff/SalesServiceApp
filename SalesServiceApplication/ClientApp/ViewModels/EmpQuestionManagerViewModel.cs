using AppData.Commands;
using AppData.Entities;
using AppData.Services;
using AppData.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClientApp.ViewModels
{
    public class EmpQuestionManagerViewModel : ViewModelBase
    {
        public EmpQuestionManagerViewModel(Employee employee, Question question, QuestionService questionService, Window window)
        {
            _window = window;
            _questionService = questionService;
            _employee = employee;
            _question = question;
            _text = question.Answer;
        }

        #region Elements
        private Window _window;
        #endregion

        #region Services
        private QuestionService _questionService;
        #endregion

        #region Fields & Properties
        private Employee _employee;
        private Question _question;

        private string _text;

        public string Text { get => _text; set => Set(ref _text, value, nameof(Text)); }
        #endregion

        #region Methods
        private bool IsFieldsEmpty() => string.IsNullOrEmpty(Text) || string.IsNullOrWhiteSpace(Text);
        private void AddAnswer()
        {
            if (IsFieldsEmpty())
                MessageBox.Show("Поле для ответа должно быть заполнено!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                _question.Answer = Text;
                _question.Employee = _employee;
                _questionService.Update(_question);
                _window.Close();
            }
        }
        #endregion

        #region Commmands
        public ICommand AddAnswerButton => new Command(answer => AddAnswer());
        #endregion
    }
}
