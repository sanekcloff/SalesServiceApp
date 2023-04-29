using AppData.Entities;
using AppData.Services;
using AppData.ViewModel;
using AppData.Storage.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AppData.Commands;

namespace ClientApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(Client client, ProductService productService, CategoryService categoryService, ServiceService serviceService, ProductOrderService productOrderService, ServiceOrderService serviceOrderService)
        {
            _client = client;
            _productService = productService;
            _categoryService = categoryService;
            _serviceService = serviceService;
            _productOrderService = productOrderService;
            _serviceOrderService = serviceOrderService;

            ProductFilthers = new List<string> { "Не выбрана" };
            ProductSorts = new List<string> { "Не выбрана", "По стоимости(убыв.)", "По стоимости(возр.)", "По новизне(убыв.)", "По новизне(возр.)" };
            ServiceSorts = new List<string> { "Не выбрана", "По стоимости(убыв.)", "По стоимости(возр.)", "По новизне(убыв.)", "По новизне(возр.)" };
            ProductFilthers.AddRange(_categoryService.GetCategories().Select(c => c.Title));
            SelectedProductFilther = ProductFilthers[0];
            SelectedProductSort = ProductSorts[0];
            SelectedServiceSort = ServiceSorts[0];
            UpdateLists();
        }
        #region Services
        private ProductService _productService;
        private CategoryService _categoryService;
        private ServiceService _serviceService;
        private ProductOrderService _productOrderService;
        private ServiceOrderService _serviceOrderService;
        #endregion
        #region Fields & Properties
        private Client _client;

        private Product _selectedProduct;
        private List<Product> _products;
        private Service _selectedService;
        private List<Service> _services;
        private string _searchProductValue;
        private string _searchServiceValue;
        private string _selectedProductFilther;
        private string _selectedProductSort;
        private string _selectedServiceSort;
        private string _productsCount;
        private string _servicesCount;

        public Product SelectedProduct { get => _selectedProduct; set => Set(ref _selectedProduct, value, nameof(SelectedProduct)); }
        public List<Product> Products { get => _products; set => Set(ref _products, value, nameof(Products)); }
        public Service SelectedService { get => _selectedService; set => Set(ref _selectedService, value, nameof(SelectedService)); }
        public List<Service> Services { get => _services; set => Set(ref _services, value, nameof(Services)); }
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
        public List<string> ProductFilthers { get; set; } = null!;
        public List<string> ProductSorts { get; set; } = null!;
        public List<string> ServiceSorts { get; set; } = null!;
        public string ProductsCount { get => _productsCount; set => Set(ref _productsCount, value, nameof(ProductsCount)); }
        public string ServicesCount { get => _servicesCount; set => Set(ref _servicesCount, value, nameof(ServicesCount)); }
        #endregion
        #region Methods
        private ICollection<Product> GetProducts() => SortProducts(SearchProducts(FiltherProducts(_productService.GetProducts())));
        private ICollection<Service> GetServices() => SortServices(SearchServices(_serviceService.GetServices()));
        private void UpdateLists()
        {
            Products = new List<Product>(GetProducts());
            Services = new List<Service>(GetServices());
            ProductsCount = $"{Products.Count}/{_productService.GetProducts().Count}";
            ServicesCount = $"{Services.Count}/{_serviceService.GetServices().Count}";
        }
        private ICollection<Product> SearchProducts(ICollection<Product> products)
        {
            if (!string.IsNullOrEmpty(SearchProductValue))
                return products.Where(p => p.Title.ToLower().Contains(SearchProductValue.ToLower()) || p.Description.ToLower().Contains(SearchProductValue.ToLower())).ToList();
            else
                return products;
        }
        private ICollection<Product> FiltherProducts(ICollection<Product> products)
        {
            if (!(SelectedProductFilther == ProductFilthers[0]))
                return products.Where(p => p.ProductCategories.Any(pc => pc.Category.Title == SelectedProductFilther)).ToList();
            else
                return products;
        }
        private ICollection<Product> SortProducts(ICollection<Product> products)
        {
            if (SelectedProductSort == ProductSorts[1])
                return products.OrderByDescending(p => p.CorrectCost).ToList();
            else if (SelectedProductSort == ProductSorts[2])
                return products.OrderBy(p => p.CorrectCost).ToList();
            else if (SelectedProductSort == ProductSorts[3])
                return products.OrderByDescending(p => p.DateOfAdd).ToList();
            else if (SelectedProductSort == ProductSorts[4])
                return products.OrderBy(p => p.DateOfAdd).ToList();
            else
                return products;
        }
        private ICollection<Service> SearchServices(ICollection<Service> services)
        {
            if (!string.IsNullOrEmpty(SearchServiceValue))
                return services.Where(p => p.Title.ToLower().Contains(SearchServiceValue.ToLower()) || p.Description.ToLower().Contains(SearchServiceValue.ToLower())).ToList();
            else
                return services;
        }
        private ICollection<Service> SortServices(ICollection<Service> services)
        {
            if (SelectedServiceSort == ServiceSorts[1])
                return services.OrderByDescending(p => p.CostPerHour).ToList();
            else if (SelectedServiceSort == ServiceSorts[2])
                return services.OrderBy(p => p.CostPerHour).ToList();
            else if (SelectedServiceSort == ServiceSorts[3])
                return services.OrderByDescending(p => p.DateOfAdd).ToList();
            else if (SelectedServiceSort == ServiceSorts[4])
                return services.OrderBy(p => p.DateOfAdd).ToList();
            else
                return services;
        }
        private void PurchaseProduct()
        {
            if (SelectedProduct != null)
            {
                _productOrderService.Insert(new ProductOrder { DateOfAdd = DateTime.Now, Client = _client, Status = Statuses.Ожидание.ToString(), Product = SelectedProduct, PaymentAmount=SelectedProduct.CorrectCost });
                MessageBox.Show($"Заказ на товар \"{SelectedProduct.Title}\" успешно оформлен, ожидайте обратной связи.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
            else
                MessageBox.Show("Выберите продукт!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void PurchaseService()
        {
            if (SelectedService != null)
            {
                _serviceOrderService.Insert(new ServiceOrder { DateOfAdd = DateTime.Now, Client = _client, Status = Statuses.Ожидание.ToString(), Service = SelectedService });
                MessageBox.Show($"Заказ на услугу \"{SelectedService.Title}\" успешно оформлен, ожидайте обратной связи.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
            else
                MessageBox.Show("Выберите услугу!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
        #region Commands
        public ICommand PurchaseProductButton => new Command(product => PurchaseProduct());
        public ICommand PurchaseServiceButton => new Command(service => PurchaseService());
        #endregion
    }
}
