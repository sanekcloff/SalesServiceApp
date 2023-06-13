using AppData.Converter;
using AppData.Entities;
using AppData.Services;
using AppData.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using AppData.Commands;
using AppData.Storage.Enums;
using System.Windows.Documents;
using ClientApp.Views;

namespace ClientApp.ViewModels
{
    public class EmpPurchasesViewModel : ViewModelBase
    {
        public EmpPurchasesViewModel(Employee employee, ProductService productService, CategoryService categoryService, ServiceService serviceService, ProductOrderService productOrderService, ServiceOrderService serviceOrderService)
        {
            _employee = employee;
            _productOrderService = productOrderService;
            _serviceOrderService = serviceOrderService;

            ProductFilthers = new List<string> { "Все статутсы", $"{Statuses.Ожидание}", $"{Statuses.Выполнение}", $"{Statuses.Завершён}" };
            ServiceFilthers = new List<string> { "Все статусы", $"{Statuses.Ожидание}", $"{Statuses.Выполнение}", $"{Statuses.Завершён}" };
            ProductSorts = new List<string> { "Не выбрана", "По стоимости(убыв.)", "По стоимости(возр.)", "Сначала новые", "Сначала старые" };
            ServiceSorts = new List<string> { "Не выбрана", "По стоимости(убыв.)", "По стоимости(возр.)", "Сначала новые", "Сначала старые" };
            SelectedProductFilther = ProductFilthers[0];
            SelectedServiceFilther = ServiceFilthers[0];
            SelectedProductSort = ProductSorts[0];
            SelectedServiceSort = ServiceSorts[0];
        }
        #region Services
        private ProductOrderService _productOrderService;
        private ServiceOrderService _serviceOrderService;
        #endregion
        #region Fields & Properties
        private Employee _employee;

        private ProductOrder _selectedProductOrder;
        private List<ProductOrder> _productOrders;
        private ServiceOrder _selectedServiceOrder;
        private List<ServiceOrder> _serviceOrders;
        private string _searchProductValue;
        private string _searchServiceValue;
        private string _selectedProductFilther;
        private string _selectedServiceFilther;
        private string _selectedProductSort;
        private string _selectedServiceSort;
        private string _productsCount;
        private string _servicesCount;

        public ProductOrder SelectedProductOrder { get => _selectedProductOrder; set => Set(ref _selectedProductOrder, value, nameof(SelectedProductOrder)); }
        public List<ProductOrder> ProductOrders { get => _productOrders; set => Set(ref _productOrders, value, nameof(ProductOrders)); }
        public ServiceOrder SelectedServiceOrder { get => _selectedServiceOrder; set => Set(ref _selectedServiceOrder, value, nameof(SelectedServiceOrder)); }
        public List<ServiceOrder> ServiceOrders { get => _serviceOrders; set => Set(ref _serviceOrders, value, nameof(ServiceOrders)); }
        public string SearchProductValue
        {
            get => _searchProductValue; set
            {
                if (Set(ref _searchProductValue, value, nameof(SearchProductValue)))
                    UpdateLists();
            }
        }
        public string SearchServiceValue
        {
            get => _searchServiceValue; set
            {
                if (Set(ref _searchServiceValue, value, nameof(SearchServiceValue)))
                    UpdateLists();
            }
        }
        public string SelectedProductFilther
        {
            get => _selectedProductFilther; set
            {
                if (Set(ref _selectedProductFilther, value, nameof(SelectedProductFilther)))
                    UpdateLists();
            }
        }
        public string SelectedProductSort
        {
            get => _selectedProductSort; set
            {
                if (Set(ref _selectedProductSort, value, nameof(SelectedProductSort)))
                    UpdateLists();
            }
        }
        public string SelectedServiceSort
        {
            get => _selectedServiceSort; set
            {
                if (Set(ref _selectedServiceSort, value, nameof(SelectedServiceSort)))
                    UpdateLists();
            }
        }
        public string SelectedServiceFilther
        {
            get => _selectedServiceFilther; set
            {
                if (Set(ref _selectedServiceFilther, value, nameof(SelectedServiceFilther)))
                    UpdateLists();
            }
        }
        public List<string> ProductFilthers { get; set; } = null!;
        public List<string> ServiceFilthers { get; set; } = null!;
        public List<string> ProductSorts { get; set; } = null!;
        public List<string> ServiceSorts { get; set; } = null!;
        public string ProductsCount { get => _productsCount; set => Set(ref _productsCount, value, nameof(ProductsCount)); }
        public string ServicesCount { get => _servicesCount; set => Set(ref _servicesCount, value, nameof(ServicesCount)); }
        #endregion
        #region Methods
        private ICollection<ProductOrder> GetProductOrders() => SortProducts(SearchProducts(FiltherProducts(_productOrderService.GetProductOrders().ToList())));
        private ICollection<ServiceOrder> GetServiceOrders() => SortServices(SearchServices(FiltherServices(_serviceOrderService.GetServiceOrders().ToList())));
        private void UpdateLists()
        {
            ProductOrders = new List<ProductOrder>(GetProductOrders());
            ServiceOrders = new List<ServiceOrder>(GetServiceOrders());
            ProductsCount = $"{ProductOrders.Count}/{_productOrderService.GetProductOrders().ToList().Count}";
            ServicesCount = $"{ServiceOrders.Count}/{_serviceOrderService.GetServiceOrders().ToList().Count}";
        }
        private ICollection<ProductOrder> SearchProducts(ICollection<ProductOrder> productOrders)
        {
            if (!string.IsNullOrEmpty(SearchProductValue))
                return productOrders.Where(p => p.Product.Title.ToLower().Contains(SearchProductValue.ToLower()) || p.Product.Description.ToLower().Contains(SearchProductValue.ToLower())).ToList();
            else
                return productOrders;
        }
        private ICollection<ProductOrder> FiltherProducts(ICollection<ProductOrder> productOrders)
        {
            if (!(SelectedProductFilther == ProductFilthers[0]))
                return productOrders.Where(p => p.Status==SelectedProductFilther).ToList();
            else
                return productOrders;
        }
        private ICollection<ProductOrder> SortProducts(ICollection<ProductOrder> productOrders)
        {
            if (SelectedProductSort == ProductSorts[1])
                return productOrders.OrderByDescending(p => p.PaymentAmount).ToList();
            else if (SelectedProductSort == ProductSorts[2])
                return productOrders.OrderBy(p => p.PaymentAmount).ToList();
            else if (SelectedProductSort == ProductSorts[3])
                return productOrders.OrderByDescending(p => p.DateOfAdd).ToList();
            else if (SelectedProductSort == ProductSorts[4])
                return productOrders.OrderBy(p => p.DateOfAdd).ToList();
            else
                return productOrders;
        }
        private ICollection<ServiceOrder> SearchServices(ICollection<ServiceOrder> serviceOrders)
        {
            if (!string.IsNullOrEmpty(SearchServiceValue))
                return serviceOrders.Where(p => p.Service.Title.ToLower().Contains(SearchServiceValue.ToLower()) || p.Service.Description.ToLower().Contains(SearchServiceValue.ToLower())).ToList();
            else
                return serviceOrders;
        }
        private ICollection<ServiceOrder> SortServices(ICollection<ServiceOrder> serviceOrders)
        {
            if (SelectedServiceSort == ServiceSorts[1])
                return serviceOrders.OrderByDescending(p => p.Service.CostPerHour).ToList();
            else if (SelectedServiceSort == ServiceSorts[2])
                return serviceOrders.OrderBy(p => p.Service.CostPerHour).ToList();
            else if (SelectedServiceSort == ServiceSorts[3])
                return serviceOrders.OrderByDescending(p => p.DateOfAdd).ToList();
            else if (SelectedServiceSort == ServiceSorts[4])
                return serviceOrders.OrderBy(p => p.DateOfAdd).ToList();
            else
                return serviceOrders;
        }
        private ICollection<ServiceOrder> FiltherServices(ICollection<ServiceOrder> serviceOrders)
        {
            if (!(SelectedServiceFilther == ServiceFilthers[0]))
                return serviceOrders.Where(p => p.Status == SelectedServiceFilther).ToList();
            else
                return serviceOrders;
        }
        private bool IsProductOrderEmployeeExist() => SelectedProductOrder.Employee != null && SelectedProductOrder.Employee!=_employee;
        private bool IsSelectedProductOrderNull() => SelectedProductOrder == null;
        private void CompleteProductOrder()
        {
            if (IsSelectedProductOrderNull())
                MessageBox.Show($"Выберите заказ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (IsProductOrderEmployeeExist())
                MessageBox.Show($"Этим заказом занимается {SelectedProductOrder.Employee.FullName}!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                SelectedProductOrder.Status = Statuses.Завершён.ToString();
                SelectedProductOrder.Employee = _employee;
                SelectedProductOrder.DateOfComplete = DateTime.Now;
                _productOrderService.Update(SelectedProductOrder);
                MessageBox.Show($"Статус обновлён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
        }
        private void InProgressProductOrder()
        {
            if (IsSelectedProductOrderNull())
                MessageBox.Show($"Выберите заказ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (IsProductOrderEmployeeExist())
                MessageBox.Show($"Этим заказом занимается {SelectedProductOrder.Employee.FullName}!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                SelectedProductOrder.Status = Statuses.Выполнение.ToString();
                SelectedProductOrder.Employee = _employee;
                SelectedProductOrder.DateOfComplete = null;
                _productOrderService.Update(SelectedProductOrder);
                MessageBox.Show($"Статус обновлён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
        }
        private void WaitingProductOrder()
        {
            if (IsSelectedProductOrderNull())
                MessageBox.Show($"Выберите заказ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (IsProductOrderEmployeeExist())
                MessageBox.Show($"Этим заказом занимается {SelectedProductOrder.Employee.FullName}!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                SelectedProductOrder.Status = Statuses.Ожидание.ToString();
                SelectedProductOrder.Employee = null;
                SelectedProductOrder.DateOfComplete = null;
                _productOrderService.Update(SelectedProductOrder);
                MessageBox.Show($"Статус обновлён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
        }
        private bool IsServiceOrderEmployeeExist() => SelectedServiceOrder.Employee != null && SelectedServiceOrder.Employee != _employee;
        private bool IsSelectedServiceOrderNull() => SelectedServiceOrder == null;
        private void CompleteServiceOrder()
        {
            if (IsSelectedServiceOrderNull())
                MessageBox.Show($"Выберите заказ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (IsServiceOrderEmployeeExist())
                MessageBox.Show($"Этим заказом занимается {SelectedServiceOrder.Employee.FullName}!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                SelectedServiceOrder.Status = Statuses.Завершён.ToString();
                SelectedServiceOrder.Employee = _employee;
                SelectedServiceOrder.DateOfComplete = DateTime.Now;
                new ServiceOrderCompleteWindow(SelectedServiceOrder,_serviceOrderService).ShowDialog();
                UpdateLists();
            }
        }
        private void InProgressServiceOrder()
        {
            if (IsSelectedServiceOrderNull())
                MessageBox.Show($"Выберите заказ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (IsServiceOrderEmployeeExist())
                MessageBox.Show($"Этим заказом занимается {SelectedServiceOrder.Employee.FullName}!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                SelectedServiceOrder.Status = Statuses.Выполнение.ToString();
                SelectedServiceOrder.Employee = _employee;
                SelectedServiceOrder.DateOfComplete = null;
                SelectedServiceOrder.PaymentAmount = null;
                _serviceOrderService.Update(SelectedServiceOrder);
                MessageBox.Show($"Статус обновлён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
        }
        private void WaitingServiceOrder()
        {
            if (IsSelectedServiceOrderNull())
                MessageBox.Show($"Выберите заказ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (IsServiceOrderEmployeeExist())
                MessageBox.Show($"Этим заказом занимается {SelectedServiceOrder.Employee.FullName}!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                SelectedServiceOrder.Status = Statuses.Ожидание.ToString();
                SelectedServiceOrder.Employee = null;
                SelectedServiceOrder.DateOfComplete = null;
                SelectedServiceOrder.PaymentAmount = null;
                _serviceOrderService.Update(SelectedServiceOrder);
                MessageBox.Show($"Статус обновлён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
        }
        private void ClientInfoProduct()
        {
            if (IsSelectedProductOrderNull())
                MessageBox.Show($"Выберите заказ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                new ClientInfoWindow(SelectedProductOrder).ShowDialog();
        }
        private void ClientInfoService()
        {
            if (IsSelectedServiceOrderNull())
                MessageBox.Show($"Выберите заказ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                new ClientInfoWindow(SelectedServiceOrder).ShowDialog();
        }
        #endregion
        #region Command
        public ICommand CompleteProductOrderButton => new Command(completeorder => CompleteProductOrder());
        public ICommand InProgressProductOrderButton => new Command(inprogressorder => InProgressProductOrder());
        public ICommand WaitingProductOrderButton => new Command(waitingorder => WaitingProductOrder());
        public ICommand CompleteServiceOrderButton => new Command(completeorder => CompleteServiceOrder());
        public ICommand InProgressServiceOrderButton => new Command(inprogressorder => InProgressServiceOrder());
        public ICommand WaitingServiceOrderButton => new Command(waitingorder => WaitingServiceOrder());
        public ICommand ClientInfoProductButton => new Command(clientinfoproduct => ClientInfoProduct());
        public ICommand ClientInfoServiceButton => new Command(clientinfoService => ClientInfoService());
        #endregion}
    }
}
