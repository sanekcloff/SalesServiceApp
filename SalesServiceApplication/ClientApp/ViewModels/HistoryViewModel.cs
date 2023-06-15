using AppData.Entities;
using AppData.Services;
using AppData.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using AppData.Commands;
using AppData.Converter;


namespace ClientApp.ViewModels
{
    public class HistoryViewModel : ViewModelBase
    {
        public HistoryViewModel(Client client, CategoryService categoryService, ProductOrderService productOrderService, ServiceOrderService serviceOrderService)
        {
            _client = client;
            _categoryService = categoryService;
            _productOrderService = productOrderService;
            _serviceOrderService = serviceOrderService;

            ProductFilthers = new List<string> { "Не выбрана" };
            ProductSorts = new List<string> { "Не выбрана", "По стоимости(убыв.)", "По стоимости(возр.)", "По новизне(убыв.)", "По новизне(возр.)" };
            ServiceSorts = new List<string> { "Не выбрана", "По стоимости(убыв.)", "По стоимости(возр.)", "По новизне(убыв.)", "По новизне(возр.)" };
            ProductFilthers.AddRange(_categoryService.GetCategories().Select(c => c.Title));
            SelectedProductFilther = ProductFilthers[0];
            SelectedProductSort = ProductSorts[0];
            SelectedServiceSort = ServiceSorts[0];
        }
        #region Services
        private CategoryService _categoryService;
        private ProductOrderService _productOrderService;
        private ServiceOrderService _serviceOrderService;
        #endregion
        #region Fields & Properties
        private Client _client;

        private ProductOrder _selectedProductOrder;
        private List<ProductOrder> _productOrders;
        private ServiceOrder _selectedServiceOrder;
        private List<ServiceOrder> _serviceOrders;
        private string _searchProductValue;
        private string _searchServiceValue;
        private string _selectedProductFilther;
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
        public List<string> ProductFilthers { get; set; } = null!;
        public List<string> ProductSorts { get; set; } = null!;
        public List<string> ServiceSorts { get; set; } = null!;
        public string ProductsCount { get => _productsCount; set => Set(ref _productsCount, value, nameof(ProductsCount)); }
        public string ServicesCount { get => _servicesCount; set => Set(ref _servicesCount, value, nameof(ServicesCount)); }
        #endregion
        #region Methods
        private ICollection<ProductOrder> GetProductOrders() => SortProducts(SearchProducts(FiltherProducts(_productOrderService.GetProductOrders().Where(po => po.ClientId == _client.Id && po.IsCompleted == true).ToList())));
        private ICollection<ServiceOrder> GetServiceOrders() => SortServices(SearchServices(_serviceOrderService.GetServiceOrders().Where(so => so.ClientId == _client.Id && so.IsCompleted == true).ToList()));
        private void UpdateLists()
        {
            ProductOrders = new List<ProductOrder>(GetProductOrders());
            ServiceOrders = new List<ServiceOrder>(GetServiceOrders());
            ProductsCount = $"{ProductOrders.Count}/{_productOrderService.GetProductOrders().Where(po=>po.IsCompleted==true && po.ClientId == _client.Id).ToList().Count}";
            ServicesCount = $"{ServiceOrders.Count}/{_serviceOrderService.GetServiceOrders().Where(so=>so.IsCompleted==true && so.ClientId == _client.Id).ToList().Count}";
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
                return productOrders.Where(p => p.Product.ProductCategories.Any(pc => pc.Category.Title == SelectedProductFilther)).ToList();
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
        private void CreateProductPdf()
        {
            if (SelectedProductOrder!=null)
                PDF.CreateProductOrderCheck(SelectedProductOrder);
            else
                MessageBox.Show("Выберите заказ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CreateServicePdf()
        {
            if (SelectedServiceOrder != null)
                PDF.CreateServiceOrderCheck(SelectedServiceOrder);
            else
                MessageBox.Show("Выберите заказ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
        #region Command
        public ICommand CreateProductPDFButton => new Command(createpdf => CreateProductPdf());
        public ICommand CreateServicePDFButton => new Command(createpdf => CreateServicePdf());
        #endregion
    }
}
