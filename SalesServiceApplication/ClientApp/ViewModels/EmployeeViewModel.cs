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
    public class EmployeeViewModel : ViewModelBase
    {
        public EmployeeViewModel(ApplicationDbContext ctx, Employee employee, EmployeeService employeeService, UserControl EmployeePage)
        {
            _productService = new(ctx);
            _categoryService = new(ctx);
            _serviceService = new(ctx);
            _productOrderService = new(ctx);
            _serviceOrderService = new(ctx);
            _newsService = new(ctx);
            _reviewService = new(ctx);
            _questionService = new(ctx);
            _clientService = new(ctx);

            _employeeService = employeeService;
            _employee = employee;
            _employeePage = EmployeePage;

            OpenPurchasesPage();
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
        private ClientService _clientService;
        private EmployeeService _employeeService;
        #endregion

        #region Fields & Properties
        private Employee _employee;
        private UserControl _employeePage;

        public UserControl EmployeePage { get => _employeePage; set => Set(ref _employeePage, value, nameof(EmployeePage)); }
        #endregion

        #region Methods
        private void OpenDiscountPage() => EmployeePage.Content = new EmpDiscountPage(_employee, _productService, _categoryService, _serviceService);
        private void OpenPurchasesPage() => EmployeePage.Content = new EmpPurchasesPage(_employee, _productService, _categoryService, _serviceService, _productOrderService, _serviceOrderService);
        private void OpenQuestionsPage() => EmployeePage.Content = new EmpQuestionsPage(_employee, _questionService);
        private void OpenNewsPage() => EmployeePage.Content = new EmpNewsPage(_employee, _newsService);
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
        public ICommand RequestsPageButton => new Command(mainpage => OpenPurchasesPage());
        public ICommand DiscountsPageButton => new Command(mainpage => OpenDiscountPage());
        public ICommand QuestionsPageButton => new Command(employeepage => OpenQuestionsPage());
        public ICommand NewsPageButton => new Command(newspage => OpenNewsPage());
        public ICommand LogOutButton => new Command(logout => LogOut());
        #endregion
    }
}
