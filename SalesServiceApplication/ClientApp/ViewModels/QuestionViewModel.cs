using AppData.Commands;
using AppData.Entities;
using AppData.Services;
using AppData.ViewModel;
using ClientApp.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ClientApp.ViewModels
{
    public class QuestionViewModel : ViewModelBase
    {
        public QuestionViewModel(Client client, QuestionService questionService)
        {
            _client = client;
            _questionService = questionService;

            QuestionSorts = new List<string> { "Сначала новые", "Сначала старые" };
            SelectedQuestionSort = QuestionSorts[0];
            SelectedClientQuestionSort = QuestionSorts[0];
            UpdateLists();
        }
        #region Services
        private QuestionService _questionService;
        #endregion
        #region Fields & Properties
        private Client _client;

        
        private List<Question> _questions;
        private string _searchQuestionValue;
        private string _selectedQuestionSort;
        private string _questionCount;
        private string _clientQuestionCount;

        private Question _selectedQuestion;
        private List<Question> _clientQuestions;
        private string _searchClientQuestionValue;
        private string _selectedClientQuestionSort;

        public Question SelectedQuestion { get => _selectedQuestion; set => Set(ref _selectedQuestion, value, nameof(SelectedQuestion)); }
        public List<Question> Questions { get => _questions; set => Set(ref _questions, value, nameof(Questions)); }
        public string SearchQuestionValue
        {
            get => _searchQuestionValue; set
            {
                if (Set(ref _searchQuestionValue, value, nameof(SearchQuestionValue)))
                    UpdateLists();
            }
        }
        public string SelectedQuestionSort
        {
            get => _selectedQuestionSort; set
            {
                if (Set(ref _selectedQuestionSort, value, nameof(SelectedQuestionSort)))
                    UpdateLists();
            }
        }
        public List<string> QuestionSorts { get; set; }
        public string QuestionCount { get => _questionCount; set => Set(ref _questionCount, value, nameof(QuestionCount)); }
        public string ClientQuestionCount { get => _clientQuestionCount; set => Set(ref _clientQuestionCount, value, nameof(ClientQuestionCount)); }
        public List<Question> ClientQuestions { get => _clientQuestions; set => Set(ref _clientQuestions, value, nameof(ClientQuestions)); }
        public string SearchClientQuestionValue
        {
            get => _searchClientQuestionValue; set
            {
                if (Set(ref _searchClientQuestionValue, value, nameof(SearchClientQuestionValue)))
                    UpdateLists();
            }
        }
        public string SelectedClientQuestionSort
        {
            get => _selectedClientQuestionSort; set
            {
                if (Set(ref _selectedClientQuestionSort, value, nameof(SelectedClientQuestionSort)))
                    UpdateLists();
            }
        }
        #endregion
        #region Methods
        private ICollection<Question> GetQuestions() => SortQuestion(SearchQuestions(_questionService.GetQuestions().OrderByDescending(q => q.DateOfAdd).ToList()));
        private ICollection<Question> GetClientQuestions() => SortClientQuestion(SearchClientQuestions(_questionService.GetQuestions().OrderByDescending(q => q.DateOfAdd).Where(q=>q.Client==_client).ToList()));
        private void UpdateLists()
        {
            Questions = new List<Question>(GetQuestions());
            ClientQuestions = new List<Question>(GetClientQuestions());
            
            QuestionCount = $"{Questions.Count}/{_questionService.GetQuestions().Count}";
            ClientQuestionCount = $"{ClientQuestions.Count}/{_questionService.GetQuestions().Where(cqc=>cqc.Client==_client).ToList().Count}";
        }
        private ICollection<Question> SearchQuestions(ICollection<Question> questions)
        {
            if (!string.IsNullOrEmpty(SearchQuestionValue))
                return questions.Where(q => q.Topic.ToLower().Contains(SearchQuestionValue.ToLower()) || q.Text.ToLower().Contains(SearchQuestionValue.ToLower())).ToList();
            else
                return questions;
        }
        private ICollection<Question> SortQuestion(ICollection<Question> questions)
        {
            if (SelectedQuestionSort == QuestionSorts[0])
                return questions.OrderByDescending(q => q.DateOfAdd).ToList();
            else
                return questions.OrderBy(q => q.DateOfAdd).ToList();
        }
        private ICollection<Question> SearchClientQuestions(ICollection<Question> questions)
        {
            if (!string.IsNullOrEmpty(SearchClientQuestionValue))
                return questions.Where(q => q.Topic.ToLower().Contains(SearchClientQuestionValue.ToLower()) || q.Text.ToLower().Contains(SearchClientQuestionValue.ToLower())).ToList();
            else
                return questions;
        }
        private ICollection<Question> SortClientQuestion(ICollection<Question> questions)
        {
            if (SelectedClientQuestionSort == QuestionSorts[0])
                return questions.OrderByDescending(q => q.DateOfAdd).ToList();
            else
                return questions.OrderBy(q => q.DateOfAdd).ToList();
        }
        private void OpenAddQuestionManagerWindow()
        {
            new QuestionManagerWindow(_client, null, _questionService).ShowDialog();
            UpdateLists();
        }
        private bool SelectedQuestionIsNull() => SelectedQuestion == null;
        private void OpenEditQuestionManagerWindow()
        {
            if (!SelectedQuestionIsNull())
            {
                new QuestionManagerWindow(_client, SelectedQuestion, _questionService).ShowDialog();
                UpdateLists();
            }
            else
                MessageBox.Show("Выберите вопрос!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void DeleteQuestion()
        {
            if (!SelectedQuestionIsNull())
            {
                var result = MessageBox.Show($"Удалить вопрос на тему \"{SelectedQuestion.Topic}\"?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _questionService.Delete(SelectedQuestion);
                    MessageBox.Show($"Вопрос на тему \"{SelectedQuestion.Topic}\" успешно удалён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateLists();
                }
            }
            else
                MessageBox.Show("Выберите вопрос!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
        #region Comands
        public ICommand OpenAddManagerWindow => new Command(addquestion => OpenAddQuestionManagerWindow());
        public ICommand OpenEditManagerWindow => new Command(editquestion => OpenEditQuestionManagerWindow());
        public ICommand DeleteQustionButton => new Command(deletequestion => DeleteQuestion());
        #endregion
    }
}
