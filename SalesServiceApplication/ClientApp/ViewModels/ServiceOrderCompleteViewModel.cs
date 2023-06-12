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
    public class ServiceOrderCompleteViewModel : ViewModelBase
    {
        public ServiceOrderCompleteViewModel(ServiceOrder serviceOrder, ServiceOrderService serviceOrderService, Window window)
        {
            _serviceOrder = serviceOrder;
            if (serviceOrder.Service.IsNegotiable)
                Cost = serviceOrder.Service.CostPerHour;
            _serviceOrderService = serviceOrderService;
            _window = window;
        }
        #region Elements
        private Window _window;
        #endregion
        #region Services
        private ServiceOrderService _serviceOrderService;
        #endregion
        #region Fields & Properties
        private ServiceOrder _serviceOrder;

        private int _hours;
        private decimal _cost;

        public int Hours { get => _hours; set => Set(ref _hours, value, nameof(Hours)); }
        public decimal Cost { get => _cost; set => Set(ref _cost, value, nameof(Cost)); }
        #endregion
        #region Methods
        
        private void UpdateOrder()
        {
            _serviceOrder.PaymentAmount = (decimal)Cost * Hours;
            _serviceOrderService.Update(_serviceOrder);
            MessageBox.Show($"Статус обновлён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            _window.Close();
        }
        #endregion
        #region Commands
        public ICommand UpdateOrderButton => new Command(updateorder => UpdateOrder());
        #endregion
    }
}
