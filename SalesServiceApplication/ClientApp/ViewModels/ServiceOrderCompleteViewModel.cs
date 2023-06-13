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
            if (!serviceOrder.Service.IsNegotiable)
                Cost = serviceOrder.Service.CostPerHour.ToString();
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

        private string _hours;
        private string _cost;

        public string Hours
        {
            get => _hours; set
            {
                if (!string.IsNullOrEmpty(value))
                    value = Convert.ToInt32(value).ToString();
                Set(ref _hours, value, nameof(Hours));
            }
        }
        public string Cost
        {
            get => _cost; set
            {
                if (!string.IsNullOrEmpty(value))
                    value = Convert.ToDecimal(value).ToString();
                Set(ref _cost, value, nameof(Cost));
            }
        }
        #endregion

        #region Methods
        private void UpdateOrder()
        {
            _serviceOrder.PaymentAmount = Convert.ToDecimal(Cost) * Convert.ToInt32(Hours);
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
