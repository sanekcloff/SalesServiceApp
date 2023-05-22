using AppData.Entities;
using AppData.Services;
using AppData.ViewModel;
using ClientApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using AppData.Commands;

namespace ClientApp.ViewModels
{
    public class ReviewViewModel : ViewModelBase
    {
        public ReviewViewModel(Client client, ReviewService reviewService)
        {
            _client = client;
            _reviewService = reviewService;

            ReviewSorts = new List<string> { "Сначала новые", "Сначала старые" };
            SelectedReviewSort = ReviewSorts[0];
            SelectedClientReviewSort = ReviewSorts[0];
            UpdateLists();
        }
        #region Services
        private ReviewService _reviewService;
        #endregion
        #region Fields & Properties
        private Client _client;

        private Review _selectedReview;
        private List<Review> _reviews;
        private List<Review> _clientReviews;
        private string _searchReviewValue;
        private string _searchClientReviewValue;
        private string _selectedReviewSort;
        private string _selectedClientReviewSort;
        private string _reviewCount;
        private string _clientReviewCount;

        public Review SelectedReview { get => _selectedReview; set => Set(ref _selectedReview, value, nameof(SelectedReview)); }
        public List<Review> Reviews { get => _reviews; set => Set(ref _reviews, value, nameof(Reviews)); }
        public string SearchReviewValue
        {
            get => _searchReviewValue; set
            {
                if (Set(ref _searchReviewValue, value, nameof(SearchReviewValue)))
                    UpdateLists();
            }
        }
        public string SelectedReviewSort
        {
            get => _selectedReviewSort; set
            {
                if (Set(ref _selectedReviewSort, value, nameof(SelectedReviewSort)))
                    UpdateLists();
            }
        }
        public List<string> ReviewSorts { get; set; }
        public string ReviewCount { get => _reviewCount; set => Set(ref _reviewCount, value, nameof(ReviewCount)); }
        public List<Review> ClientReviews { get => _clientReviews; set => Set(ref _clientReviews, value, nameof(ClientReviews)); }
        public string SearchClientReviewValue
        {
            get => _searchClientReviewValue; set
            {
                if (Set(ref _searchClientReviewValue, value, nameof(SearchClientReviewValue)))
                    UpdateLists();
            }
        }
        public string SelectedClientReviewSort
        {
            get => _selectedClientReviewSort; set
            {
                if (Set(ref _selectedClientReviewSort, value, nameof(SelectedClientReviewSort)))
                    UpdateLists();
            }
        }
        public string ClientReviewCount { get => _clientReviewCount; set => Set(ref _clientReviewCount, value, nameof(ClientReviewCount)); }
        #endregion
        #region Methods
        private ICollection<Review> GetReviews() => SortReviews(SearchReviews(_reviewService.GetReviews().OrderByDescending(q => q.DateOfAdd).ToList()));
        private ICollection<Review> GetClientReviews() => SortClientReviews(SearchClientReviews(_reviewService.GetReviews().OrderByDescending(q => q.DateOfAdd).Where(q=>q.Client==_client).ToList()));
        private void UpdateLists()
        {
            Reviews = new List<Review>(GetReviews());
            ClientReviews = new List<Review>(GetClientReviews());

            ReviewCount = $"{Reviews.Count}/{_reviewService.GetReviews().Count}";
            ClientReviewCount = $"{ClientReviews.Count}/{_reviewService.GetReviews().Where(q=>q.Client==_client).ToList().Count}";
        }
        private ICollection<Review> SearchReviews(ICollection<Review> reviews)
        {
            if (!string.IsNullOrEmpty(SearchReviewValue))
                return reviews.Where(q => q.Text.ToLower().Contains(SearchReviewValue.ToLower())).ToList();
            else
                return reviews;
        }
        private ICollection<Review> SortReviews(ICollection<Review> reviews)
        {
            if (SelectedReviewSort == ReviewSorts[0])
                return reviews.OrderByDescending(q => q.DateOfAdd).ToList();
            else
                return reviews.OrderBy(q => q.DateOfAdd).ToList();
        }
        private ICollection<Review> SearchClientReviews(ICollection<Review> reviews)
        {
            if (!string.IsNullOrEmpty(SearchClientReviewValue))
                return reviews.Where(q => q.Text.ToLower().Contains(SearchClientReviewValue.ToLower())).ToList();
            else
                return reviews;
        }
        private ICollection<Review> SortClientReviews(ICollection<Review> reviews)
        {
            if (SelectedClientReviewSort == ReviewSorts[0])
                return reviews.OrderByDescending(q => q.DateOfAdd).ToList();
            else
                return reviews.OrderBy(q => q.DateOfAdd).ToList();
        }
        private void OpenAddReviewManagerWindow()
        {
            if (GetClientReviews().Count>0)
                MessageBox.Show("Нельзя оставить больше 1-го отзыва!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                new ReviewManagerWindow(_client,null, _reviewService).ShowDialog();
            UpdateLists();
        }
        private bool SelectedReviewIsNull() => SelectedReview == null;
        private void OpenEditReviewManagerWindow()
        {
            if (!SelectedReviewIsNull())
            {
                new ReviewManagerWindow(_client, SelectedReview, _reviewService).ShowDialog();
                UpdateLists();
            }
            else
                MessageBox.Show("Выберите отзыв!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void DeleteQuestion()
        {
            if (!SelectedReviewIsNull())
            {
                var result = MessageBox.Show($"Удалить отзыв?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _reviewService.Delete(SelectedReview);
                    MessageBox.Show($"Отзыв успешно удалён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateLists();
                }
            }
            else
                MessageBox.Show("Выберите отзыв!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
        #region Comands
        public ICommand OpenAddManagerWindow => new Command(addReview => OpenAddReviewManagerWindow());
        public ICommand OpenEditManagerWindow => new Command(editReview => OpenEditReviewManagerWindow());
        public ICommand DeleteReviewButton => new Command(deleteReview => DeleteQuestion());
        #endregion
    }
}
