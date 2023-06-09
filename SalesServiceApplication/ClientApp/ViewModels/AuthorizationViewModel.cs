﻿using AppData.Commands;
using AppData.DataBaseData;
using AppData.Entities;
using AppData.Services;
using AppData.ViewModel;
using ClientApp.Views;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ClientApp.ViewModels
{
    public class AuthorizationViewModel:ViewModelBase
    {
        public AuthorizationViewModel()
        {
            _ctx = new();
            _clientService = new(_ctx);
            _employeeService = new(_ctx);
        }
        #region Context
        private ApplicationDbContext _ctx;
        #endregion
        #region Services
        private ClientService _clientService;
        private EmployeeService _employeeService;
        #endregion
        #region Fields & Properties
        private string _login;
        private string _password;

        public string Login { get => _login; set => Set(ref _login, value, nameof(Login)); }
        public string Password { get => _password; set => Set(ref _password, value, nameof(Password)); }
        #endregion
        #region Methods
        private bool ClientIsExits()
        {
            var isExist = false;
            try
            {
                isExist = _clientService.GetClients().Any(c => c.Login == Login && c.Password == Password);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            return isExist;
        }
        private Employee? GetEmployee() => _employeeService.GetEmployees().SingleOrDefault(e=> e.Password == Password && e.Login == Login);

        private bool PropertiesIsNull() => (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Login)) ? true : false;
        private void OpenClientWindow()
        {
            var employee = GetEmployee();
            if (PropertiesIsNull())
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (ClientIsExits())
            {
                var ClientWindow = new ClientWindow(_ctx, _clientService.GetClients().Single(c => c.Password == Password && c.Login == Login), _clientService);
                var CurrentWindow = Application.Current.MainWindow;
                ClientWindow.Show();
                Application.Current.MainWindow = ClientWindow;
                CurrentWindow.Close();
            }
            else if (employee != null)
            {
                var EmployeeWindow = new EmployeeWindow(_ctx, employee, _employeeService);
                var CurrentWindow = Application.Current.MainWindow;
                EmployeeWindow.Show();
                Application.Current.MainWindow = EmployeeWindow;
                CurrentWindow.Close();
            }
            else
                MessageBox.Show("Учётная запись не найдена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void OpenRegistrationtWindow()
        {
            var RegistrationWindow = new RegistrationWindow(_ctx);
            var CurrentWindow = Application.Current.MainWindow;
            RegistrationWindow.Show();
            Application.Current.MainWindow = RegistrationWindow;
            CurrentWindow.Close();
        }
        #endregion
        #region Commands
        public ICommand AuthorizationButton => new Command(authorization => OpenClientWindow());
        public ICommand RegistrationButton => new Command(registrtion => OpenRegistrationtWindow());
        #endregion
    }
}
