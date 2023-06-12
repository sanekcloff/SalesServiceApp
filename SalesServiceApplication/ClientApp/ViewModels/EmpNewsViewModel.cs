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
    public class EmpNewsViewModel : ViewModelBase
    {
        public EmpNewsViewModel(Employee employee, NewsService newsService)
        {
            _newsService = newsService;
            _employee = employee;

            NewsSorts = new List<string> { "Сначала новые", "Сначала старые" };
            SelectedSortValue = NewsSorts[0];
            UpdateLists();
        }
        #region Services
        private NewsService _newsService;
        #endregion
        #region Properties
        private Employee _employee;

        private New _selectedNew;
        private List<New> _news;
        private string _searchNewsValue;
        private string _selectedSortValue;


        public List<New> News { get => _news; set => Set(ref _news, value, nameof(News)); }
        public string SearchNewsValue
        {
            get => _searchNewsValue; set
            {
                if (Set(ref _searchNewsValue, value, nameof(SearchNewsValue)))
                    UpdateLists();
            }
        }
        public string SelectedSortValue
        {
            get => _selectedSortValue; set
            {
                if (Set(ref _selectedSortValue, value, nameof(SelectedSortValue)))
                    UpdateLists();
            }
        }
        public List<string> NewsSorts { get; set; }
        public New SelectedNew { get => _selectedNew; set => Set(ref _selectedNew, value, nameof(SelectedNew)); }
        #endregion
        #region Methods
        private ICollection<New> GetNews() => SortNews(SearchNews(_newsService.GetNews().OrderByDescending(n => n.PublicationDate).ToList()));
        private void UpdateLists()
        {
            News = new List<New>(GetNews());
        }
        private ICollection<New> SearchNews(ICollection<New> news)
        {
            if (!string.IsNullOrEmpty(SearchNewsValue))
                return news.Where(n => n.Header.ToLower().Contains(SearchNewsValue.ToLower()) || n.Text.ToLower().Contains(SearchNewsValue.ToLower())).ToList();
            else
                return news;
        }
        private ICollection<New> SortNews(ICollection<New> news)
        {
            if (SelectedSortValue == NewsSorts[0])
                return news.OrderByDescending(n => n.PublicationDate).ToList();
            else
                return news.OrderBy(n => n.PublicationDate).ToList();
        }
        private bool IsSelectedNewNull() => SelectedNew == null;
        private bool IsEmployeeOwnerNew() => SelectedNew.Employee == _employee;
        private void OpenAddNewManagerWindow()
        {
            new EmpNewsManagerWindow(_employee, null, _newsService).ShowDialog();
            UpdateLists();
        }
        private void OpenEditNewManagerWindow()
        {
            if (IsSelectedNewNull())
                MessageBox.Show("Выберите новость!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (!IsEmployeeOwnerNew())
                MessageBox.Show("Нельзя редактировать чужую публикацию!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                new EmpNewsManagerWindow(_employee, SelectedNew, _newsService).ShowDialog();
                UpdateLists();
            }
                
        }
        private void DeleteNew()
        {
            if (IsSelectedNewNull())
                MessageBox.Show("Выберите вопрос!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (IsEmployeeOwnerNew())
                MessageBox.Show("Нельзя редактировать чужую публикацию!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                var result = MessageBox.Show($"Удалить новость на тему \"{SelectedNew.Header}\"?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _newsService.Delete(SelectedNew);
                    MessageBox.Show($"Новость на тему \"{SelectedNew.Header}\" успешно удалёна!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateLists();
                }
            }
                
        }
        #endregion

        #region Commands
        public ICommand OpenAddManagerWindow => new Command(addnew => OpenAddNewManagerWindow());
        public ICommand OpenEditManagerWindow => new Command(editnew => OpenEditNewManagerWindow());
        public ICommand DeleteNewButton => new Command(deletenew => DeleteNew());
        #endregion
    }
}
