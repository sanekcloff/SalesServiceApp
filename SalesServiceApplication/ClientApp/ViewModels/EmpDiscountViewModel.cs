using AppData.Entities;
using AppData.Services;
using AppData.Storage.Enums;
using AppData.ViewModel;
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
    public class EmpDiscountViewModel : ViewModelBase
    {
        public EmpDiscountViewModel(Employee employee, ProductService productService, CategoryService categoryService, ServiceService serviceService)
        {
            _employee = employee;
            _productService = productService;
            _categoryService = categoryService;

            ProductFilthers = new List<string> { "Не выбрана" };
            ProductSorts = new List<string> { "Не выбрана", "По стоимости(убыв.)", "По стоимости(возр.)", "По новизне(убыв.)", "По новизне(возр.)" };
            ProductFilthers.AddRange(_categoryService.GetCategories().Select(c => c.Title));
            SelectedProductFilther = ProductFilthers[0];
            SelectedProductSort = ProductSorts[0];
            DiscountValue = 0;
            UpdateLists();
        }
        #region Services
        private ProductService _productService;
        private CategoryService _categoryService;
        #endregion
        #region Fields & Properties
        private Employee _employee;

        private Product _selectedProduct;
        private List<Product> _products;
        private string _searchProductValue;
        private string _selectedProductFilther;
        private string _selectedProductSort;
        private string _productsCount;
        private int _discountValue;

        public Product SelectedProduct
        {
            get => _selectedProduct; set
            {
                if (Set(ref _selectedProduct, value, nameof(SelectedProduct)))
                    if (value != null)
                        DiscountValue = (int)value.Discount;
                    else
                        DiscountValue = 0;
            }
        }
        public List<Product> Products { get => _products; set => Set(ref _products, value, nameof(Products)); }
        public string SearchProductValue
        {
            get => _searchProductValue; set
            {
                if (Set(ref _searchProductValue, value, nameof(SearchProductValue)))
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
        public List<string> ProductFilthers { get; set; } = null!;
        public List<string> ProductSorts { get; set; } = null!;
        public string ProductsCount { get => _productsCount; set => Set(ref _productsCount, value, nameof(ProductsCount)); }
        public int DiscountValue { get => _discountValue; set => Set(ref _discountValue, value, nameof(DiscountValue)); }
        #endregion
        #region Methods
        private ICollection<Product> GetProducts() => SortProducts(SearchProducts(FiltherProducts(_productService.GetProducts())));
        private void UpdateLists()
        {
            Products = new List<Product>(GetProducts());
            ProductsCount = $"{Products.Count}/{_productService.GetProducts().Count}";
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
        private bool SelectedProductIsNull() => SelectedProduct == null;
        private void AddDiscount()
        {
            if (SelectedProductIsNull())
                MessageBox.Show("Выберите продукт!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                SelectedProduct.Discount = DiscountValue;
                _productService.Update(SelectedProduct);
                MessageBox.Show("Скидка обновлена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
        }
        #endregion

        #region Commands
        public ICommand DiscountButton => new Command(product => AddDiscount());
        #endregion
    }
}
