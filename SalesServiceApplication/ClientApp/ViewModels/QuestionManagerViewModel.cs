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
    public class QuestionManagerViewModel : ViewModelBase
    {
        public QuestionManagerViewModel(Client client, Question? question, QuestionService questionService)
        {
            if (question == null)
            {
                _isNew = true;
                Question = new() { DateOfAdd = DateTime.Now, Client = client };
            }
            else
            {
                Topic= question.Topic;
                Text = question.Text;
                Question = question;
            }
            _questionService = questionService;
        }
        #region Services
        private QuestionService _questionService;
        #endregion
        #region Fields & Properties
        private bool _isNew = false;

        private Question? _question;
        private string _topic;
        private string _text;

        public Question? Question { get => _question; set => Set(ref _question, value, nameof(Question)); }
        public string Topic { get => _topic; set => Set(ref _topic, value, nameof(Topic)); }
        public string Text { get => _text; set => Set(ref _text, value, nameof(Text)); }
        #endregion
        #region Methods
        private bool FieldsIsNull() => (string.IsNullOrEmpty(Topic) || string.IsNullOrEmpty(Text));
        private void AddQuestion()
        {
            if (!FieldsIsNull())
            {
                Question.Topic = Topic;
                Question.Text = Text;
                _questionService.Insert(Question);
                MessageBox.Show($"Вопрос на тему \"{Topic}\" создан!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Заполните поля!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void EditQuestion()
        {
            if (!FieldsIsNull())
            {
                Question.Topic= Topic;
                Question.Text= Text;
                _questionService.Update(Question);
                MessageBox.Show($"Вопрос обновлён!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Заполните поля!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
        #region
        public ICommand AddQuestionButton => new Command(addquestion => AddQuestion());
        public ICommand EditQuestionButton => new Command(editquestion => EditQuestion());
        #endregion
    }
}
