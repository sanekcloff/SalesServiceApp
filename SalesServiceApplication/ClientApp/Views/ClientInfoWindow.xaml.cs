using AppData.Entities;
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
    /// Логика взаимодействия для ClientInfoWindow.xaml
    /// </summary>
    public partial class ClientInfoWindow : Window
    {
        public ClientInfoWindow(ProductOrder productOrder)
        {
            InitializeComponent();
            FullNameTextBlock.Text += productOrder.Client.FullName;
            OrganizationTextBlock.Text += productOrder.Client.Organization;
            EmailTextBlock.Text += productOrder.Client.Email;
            PhoneTextBlock.Text += productOrder.Client.Phone;
        }
        public ClientInfoWindow(ServiceOrder serviceOrder)
        {
            InitializeComponent();
            FullNameTextBlock.Text += serviceOrder.Client.FullName;
            OrganizationTextBlock.Text += serviceOrder.Client.Organization;
            EmailTextBlock.Text += serviceOrder.Client.Email;
            PhoneTextBlock.Text += serviceOrder.Client.Phone;
        }
    }
}
