using AppData.Entities;
using AppData.Services;
using AppData.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        public NewsViewModel(NewsService newsService)
        {
            _newsService=newsService;

            NewsSorts = new List<string> { "Сначала новые", "Сначала старые" };
            SelectedSortValue = NewsSorts[0];
            UpdateLists();
        }
        #region Services
        private NewsService _newsService;
        #endregion
        #region Properties
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
        public List<string> NewsSorts { get;  set; }
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
                return news.OrderByDescending(n=>n.PublicationDate).ToList();
            else 
                return news.OrderBy(n => n.PublicationDate).ToList();
        }
        #endregion
    }
}
