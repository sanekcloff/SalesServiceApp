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
    public class HistoryViewModel : ViewModelBase
    {
        public HistoryViewModel(Client client, CategoryService categoryService, ProductOrderService productOrderService, ServiceOrderService serviceOrderService)
        {
            
        }
    }
}
