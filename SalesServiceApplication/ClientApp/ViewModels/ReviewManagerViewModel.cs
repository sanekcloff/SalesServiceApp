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
    public class ReviewManagerViewModel : ViewModelBase
    {
        public ReviewManagerViewModel(Client client, ReviewService reviewService)
        {
            _reviewService= reviewService;
            Review = new Review() { Client=client, DateOfAdd=DateTime.Now};
            Grade = 1;
        }
        #region Services
        private ReviewService _reviewService;
        #endregion
        #region Fields & Properties
        private Review _review;
        private string _text;
        private int _grade;

        public Review Review { get => _review; set => Set(ref _review, value, nameof(Review)); }
        public string Text { get => _text; set => Set(ref _text, value, nameof(Text)); }
        public int Grade { get => _grade; set => Set(ref _grade, value, nameof(Grade)); }
        #endregion
        #region Merthods
        private bool FieldsIsNull() => (string.IsNullOrEmpty(Text));
        private void AddReview()
        {
            if (!FieldsIsNull())
            {
                Review.Text = Text;
                Review.Grade = Grade;
                _reviewService.Imsert(Review);
                MessageBox.Show($"Отзыв с оценкой {Grade} создан!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Отзыв должен содержать текст!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
        #region Commands
        public ICommand AddReviewButton => new Command(addreview => AddReview());
        #endregion
    }
}
