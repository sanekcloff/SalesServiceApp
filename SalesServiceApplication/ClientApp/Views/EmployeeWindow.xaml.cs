using AppData.DataBaseData;
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
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow(ApplicationDbContext context, Employee employee, EmployeeService employeeService)
        {
            InitializeComponent();
            Title = $"Учётная запись: {employee.FullName} ({employee.Department.Title})";
            DataContext = new EmployeeViewModel(context, employee, employeeService, EmployeePage);
        }
    }
}
