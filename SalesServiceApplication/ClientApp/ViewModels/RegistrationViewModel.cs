using AppData.Commands;
using AppData.DataBaseData;
using AppData.Entities;
using AppData.Services;
using AppData.ViewModel;
using ClientApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClientApp.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        public RegistrationViewModel(ApplicationDbContext ctx)
        {
            _clientService = new(ctx);
        }
        #region Service
        private ClientService _clientService;
        #endregion
        #region Fields & Properties
        private string _lastName;
        private string _firstName;
        private string _middleName;
        private string _organization;
        private string _email;
        private string _phone;
        private string _login;
        private string _password;

        public string LastName { get => _lastName; set => Set(ref _lastName, value, nameof(LastName)); }
        public string FirstName { get => _firstName; set => Set(ref _firstName, value, nameof(FirstName)); }
        public string MiddleName { get => _middleName; set => Set(ref _middleName, value, nameof(MiddleName)); }
        public string Organization { get => _organization; set => Set(ref _organization, value, nameof(Organization)); }
        public string Email { get => _email; set => Set(ref _email, value, nameof(Email)); }
        public string Phone { get => _phone; set => Set(ref _phone, value, nameof(Phone)); }
        public string Login { get => _login; set => Set(ref _login, value, nameof(Login)); }
        public string Password { get => _password; set => Set(ref _password, value, nameof(Password)); }
        #endregion
        #region Methods
        private bool ClientIsExist() => _clientService.GetClients().Any(c => c.Password == Password && c.Login == Login);
        private bool PasswordAlredyInUse() => _clientService.GetClients().Any(c=>c.Password==Password);
        private bool LoginAlredyInUse() => _clientService.GetClients().Any(c => c.Login == Login);
        private bool EnailAlredyInUse() => _clientService.GetClients().Any(c => c.Email == Email);
        private bool PhoneAlredyInUse() => _clientService.GetClients().Any(c => c.Phone == Phone);
        private bool PropertiesIsNull() => (string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(MiddleName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Organization));
        private void Registration()
        {
            if (PropertiesIsNull())
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (ClientIsExist())
                MessageBox.Show("Используйте другой логин и пароль!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (EnailAlredyInUse())
                MessageBox.Show("Используйте другой Email!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (PhoneAlredyInUse())
                MessageBox.Show("Используйте другой телефон!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (PasswordAlredyInUse())
                MessageBox.Show("Используйте другой пароль!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (LoginAlredyInUse())
                MessageBox.Show("Используйте другой логин!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                _clientService.Insert(new Client { Login = Login, Password = Password, FirstName = FirstName, LastName = LastName, MiddleName = MiddleName, Email = Email, Organization = Organization, Phone = Phone });
                MessageBox.Show("Учётная запись зарегистрирована!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                OpenAuthorizationWindow();
            }               
        }
        private void OpenAuthorizationWindow()
        {
            var AuthorizationWindow =new AuthorizationWindow();
            var CurrentWindow = Application.Current.MainWindow;
            AuthorizationWindow.Show();
            Application.Current.MainWindow = AuthorizationWindow;
            CurrentWindow.Close();
        }
        #endregion
        #region Commands
        public ICommand Registrationbutton => new Command(registration => Registration());
        public ICommand ExitButton => new Command(exit => OpenAuthorizationWindow());
        #endregion
    }
}
