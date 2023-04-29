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
    public class MainViewModel:ViewModelBase
    {
        public MainViewModel(Client client, ProductService productService, CategoryService categoryService, ServiceService serviceService, ProductOrderService productOrderService, ServiceOrderService serviceOrderService)
        {
            
        }
    }
}
