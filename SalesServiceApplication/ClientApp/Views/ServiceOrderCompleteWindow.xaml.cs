using AppData.Entities;
using AppData.Services;
using ClientApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ServiceOrderCompleteWindow.xaml
    /// </summary>
    public partial class ServiceOrderCompleteWindow : Window
    {
        public ServiceOrderCompleteWindow(ServiceOrder serviceOrder, ServiceOrderService serviceOrderService)
        {
            InitializeComponent();
            if (serviceOrder.Service.IsNegotiable)
                Title = "Укажите кол-во часов и стоимость за час";
            else
            {
                Title = "Укажите кол-во часов";
                CostTextBox.Visibility=Visibility.Collapsed;
            }
            DataContext = new ServiceOrderCompleteViewModel(serviceOrder, serviceOrderService, this);
        }

        private void HoursTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CostTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ",")
               && (!CostTextBox.Text.Contains(",")
               && CostTextBox.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }
    }
}
