using AppData.Commands;
using AppData.Entities;
using AppData.Services;
using AppData.ViewModel;
using ClientApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClientApp.ViewModels
{
    public class EmpQuestionViewModel : ViewModelBase
    {
        public EmpQuestionViewModel(Employee employee, QuestionService questionService)
        {
            _employee = employee;
            _questionService = questionService;

            QuestionSorts = new List<string> { "Сначала новые", "Сначала старые" };
            QuestionAnswerSorts = new List<string> { "Все вопросы", "Отвеченные", "Неотвеченные" };
            SelectedQuestionSort = QuestionSorts[0];
            SelectedQuestionAnswerSort = QuestionAnswerSorts[0];
        }
        #region Services
        private QuestionService _questionService;
        #endregion
        #region Fields & Properties
        private Employee _employee;


        private List<Question> _questions;
        private Question _selectedQuestion;

        private string _searchQuestionValue;
        private string _selectedQuestionSort;
        private string _selectedQuestionAnswerSort;
        private string _questionCount;

        public List<string> QuestionSorts { get; set; }
        public List<string> QuestionAnswerSorts { get; set; }
        public List<Question> Questions { get => _questions; set => Set(ref _questions, value, nameof(Questions)); }
        public Question SelectedQuestion { get => _selectedQuestion; set => Set( ref _selectedQuestion, value, nameof(SelectedQuestion)); }
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
        public string SelectedQuestionAnswerSort
        {
            get => _selectedQuestionAnswerSort; set
            {
                if (Set(ref _selectedQuestionAnswerSort, value, nameof(SelectedQuestionAnswerSort)))
                    UpdateLists();
            }
        }
        public string QuestionCount { get => _questionCount; set => Set(ref _questionCount, value, nameof(QuestionCount)); }
        #endregion

        #region Methods
        private ICollection<Question> GetQuestions() => SortQuestion(SearchQuestions(SortQuestionAnswer(_questionService.GetQuestions().OrderByDescending(q => q.DateOfAdd).ToList())));

        private void UpdateLists()
        {
            Questions = new List<Question>(GetQuestions());

            QuestionCount = $"{Questions.Count}/{_questionService.GetQuestions().Count}";
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
        private ICollection<Question> SortQuestionAnswer(ICollection<Question> questions)
        {
            if (SelectedQuestionAnswerSort == QuestionAnswerSorts[1])
                return questions.Where(q=>q.IsAnswered==true).ToList();
            else if (SelectedQuestionAnswerSort == QuestionAnswerSorts[2])
                return questions.Where(q => q.IsAnswered == false).ToList();
            else
                return questions.OrderBy(q => q.DateOfAdd).ToList();
        }
        private bool IsSelectedQuestionNull() => SelectedQuestion == null;
        private bool IsQuestionAnswered() => SelectedQuestion.Employee != null;
        private bool IsEmployeAnswerOwner() => SelectedQuestion.Employee == _employee;
        private bool IsEmptyAnswer() => SelectedQuestion.IsAnswered==false;
        private void OpenEditQuestionManagerWindow()
        {
            if (IsSelectedQuestionNull())
                MessageBox.Show("Выберите вопрос!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (IsQuestionAnswered() && SelectedQuestion.Employee!=_employee)
                MessageBox.Show($"{SelectedQuestion.Employee.FullName} уже ответил(а) на этот вопрос!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                new EmpQuestionManagerWindow(_employee, SelectedQuestion, _questionService).ShowDialog();
                UpdateLists();
            }

        }
        private void DeleteAnswer()
        {
            if (IsSelectedQuestionNull())
                MessageBox.Show("Выберите вопрос!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (IsEmptyAnswer())
                MessageBox.Show("Ответ отсутсвует!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (!IsEmployeAnswerOwner())
                MessageBox.Show("Нельзя удалить чужой ответ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                var result = MessageBox.Show($"Удалить ответ на тему \"{SelectedQuestion.Topic}\"?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SelectedQuestion.Answer = null;
                    SelectedQuestion.Employee = null;
                    _questionService.Update(SelectedQuestion);
                    MessageBox.Show($"Ответ на тему \"{SelectedQuestion.Topic}\" успешно удалён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateLists();
                }
            }

        }
        #endregion

        #region Commands
        public ICommand OpenAnswerWindow => new Command(editquestion => OpenEditQuestionManagerWindow());
        public ICommand DeleteAnswerButton => new Command(deletequestion => DeleteAnswer());
        #endregion
    }
}
