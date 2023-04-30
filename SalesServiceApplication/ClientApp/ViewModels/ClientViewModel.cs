using AppData.Commands;
using AppData.DataBaseData;
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
using System.Windows.Controls;
using System.Windows.Input;

namespace ClientApp.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        public ClientViewModel(ApplicationDbContext ctx, Client client, UserControl clientPage)
        {
            _productService = new(ctx);
            _categoryService = new(ctx);
            _serviceService = new(ctx);
            _productOrderService = new(ctx);
            _serviceOrderService = new(ctx);
            _newsService = new(ctx);
            _reviewService = new(ctx);
            _questionService = new(ctx);

            _client = client;
            ClientPage = clientPage;

            OpenMainPage();
        }
        #region Services
        private ProductService _productService;
        private CategoryService _categoryService;
        private ServiceService _serviceService;
        private ProductOrderService _productOrderService;
        private ServiceOrderService _serviceOrderService;
        private QuestionService _questionService;
        private NewsService _newsService;
        private ReviewService _reviewService;
        #endregion
        #region Fields & Properties
        private Client _client;
        private UserControl _clientPage;
        
        public UserControl ClientPage { get => _clientPage; set => Set(ref _clientPage, value, nameof(ClientPage)); }
        #endregion
        #region Methods
        private void OpenMainPage() => ClientPage.Content = new MainPage(_client,_productService,_categoryService,_serviceService,_productOrderService,_serviceOrderService);
        private void OpenOrdersPage() => ClientPage.Content = new OrdersPage(_client,_categoryService,_productOrderService,_serviceOrderService);
        private void OpenHistoryPage() => ClientPage.Content = new HistoryPage(_client,_categoryService,_productOrderService,_serviceOrderService);
        private void OpenQuestionsPage() => ClientPage.Content = new QuestionsPage(_client, _questionService);
        private void OpenNewsPage() => ClientPage.Content = new NewsPage(_newsService);
        private void OpenAboutUsPage() => ClientPage.Content = new AboutUsPage();
        private void OpenReviewsPage() => ClientPage.Content = new ReviewsPage(_client,_reviewService);
        private void LogOut()
        {
            var AuthorizationWindow = new AuthorizationWindow();
            var CurrentWindow = Application.Current.MainWindow;
            AuthorizationWindow.Show();
            Application.Current.MainWindow = AuthorizationWindow;
            CurrentWindow.Close();
        }
        #endregion
        #region Commands
        public ICommand MainPageButton =>new Command(mainpage => OpenMainPage());
        public ICommand OrdersPageButton =>new Command(orderspage => OpenOrdersPage());
        public ICommand HistoryPageButton =>new Command(historypage => OpenHistoryPage());
        public ICommand QuestionsPageButton => new Command(employeepage => OpenQuestionsPage());
        public ICommand NewsPageButton =>new Command(newspage => OpenNewsPage());
        public ICommand AboutUsPageButton =>new Command(aboutuspage => OpenAboutUsPage());
        public ICommand ReviewsPageButton => new Command(reviewspage => OpenReviewsPage());
        public ICommand LogOutButton =>new Command(logout => LogOut());
        #endregion
    }
}
