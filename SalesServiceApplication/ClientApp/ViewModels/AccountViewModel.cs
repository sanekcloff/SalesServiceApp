using AppData.Commands;
using AppData.Entities;
using AppData.Services;
using AppData.Validations;
using AppData.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ClientApp.ViewModels
{
    public class AccountViewModel:ViewModelBase
    {
        public AccountViewModel(Client client, ClientService clientService)
        {
            _client = client;
            _clientService = clientService;
            PhoneIndexes = new List<string>
            {
                "8",
                "+7"
            };
            SelectedPhoneIndex = PhoneIndexes[0];
            RefreshFields();
        }
        #region Services
        private ClientService _clientService;
        #endregion
        #region Fields & Properties
        private Client _client;

        private string _firstName;
        private string _lastName;
        private string _middleName;
        private string _email;
        private string _phone;
        private string _organization;
        private string _login;
        private string _password;
        private string _selectedPhoneIndex;

        public string FirstName { get => _firstName; set => Set(ref _firstName, value, nameof(FirstName)); }
        public string LastName { get => _lastName; set => Set(ref _lastName, value, nameof(LastName)); }
        public string MiddleName { get => _middleName; set => Set(ref _middleName, value, nameof(MiddleName)); }
        public string Email { get => _email; set => Set(ref _email, value, nameof(Email)); }
        public string Phone { get => _phone; set => Set(ref _phone, value, nameof(Phone)); }
        public string Organization { get => _organization; set => Set(ref _organization, value, nameof(Organization)); }
        public string Login { get => _login; set => Set(ref _login, value, nameof(Login)); }
        public string Password { get => _password; set => Set(ref _password, value, nameof(Password)); }
        public string SelectedPhoneIndex { get => _selectedPhoneIndex; set => Set(ref _selectedPhoneIndex, value, nameof(SelectedPhoneIndex)); }
        public List<string> PhoneIndexes { get; set; }
        #endregion
        #region Methods
        private void RefreshFields()
        {
            FirstName = _client.FirstName;
            LastName = _client.LastName;
            MiddleName = _client.MiddleName;
            Password = _client.Password;
            Login = _client.Login;
            Email = _client.Email;
            Phone = _client.Phone.Remove(0,1);
            Organization = _client.Organization;
        }
        private bool FieldsIsNullOrEmpty() => string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) 
            || string.IsNullOrEmpty(MiddleName) || string.IsNullOrEmpty(Login) 
            || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Phone) 
            || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Organization);
        private bool ClientIsExist() => _clientService.GetClients().Any(c => c.Password == Password && c.Login == Login) && (_client.Password!=Password && _client.Login!=Login);
        private bool PasswordAlredyInUse() => _clientService.GetClients().Any(c => c.Password == Password) && _client.Password!=Password;
        private bool LoginAlredyInUse() => _clientService.GetClients().Any(c => c.Login == Login) && _client.Login!=Login;
        private bool EnailAlredyInUse() => _clientService.GetClients().Any(c => c.Email == Email) && _client.Email != Email;
        private bool PhoneAlredyInUse() => _clientService.GetClients().Any(c => c.Phone == Phone) && _client.Phone != Phone;

        private bool DataIsCorrect()
        {
            if (!ClientValidation.IsValidName(LastName))
            {
                MessageBox.Show("Фамилия введена не корректно!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!ClientValidation.IsValidName(FirstName))
            {
                MessageBox.Show("Имя введено не корректно!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!ClientValidation.IsValidName(MiddleName))
            {
                MessageBox.Show("Отчество введено не корректно!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!ClientValidation.IsValidEmail(Email))
            {
                MessageBox.Show("Email введён не корректно!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!ClientValidation.IsValidPhoneNumber(Phone))
            {
                MessageBox.Show("Телефон введён не корректно!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!ClientValidation.IsValidUserData(Login))
            {
                MessageBox.Show("Логин введён не корректно!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!ClientValidation.IsValidUserData(Password))
            {
                MessageBox.Show("Пароль введён не корректно!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!ClientValidation.IsValidOrganization(Organization))
            {
                MessageBox.Show("Организация введена не корректно!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        private void UpdateAccountData()
        {
            if (FieldsIsNullOrEmpty())
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (DataIsCorrect())
            {
                if (ClientIsExist())
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
                    _client.FirstName = FirstName;
                    _client.LastName = LastName;
                    _client.MiddleName = MiddleName;
                    _client.Password = Password;
                    _client.Login = Login;
                    _client.Email = Email;
                    _client.Phone = "8" + Phone;
                    _client.Organization = Organization;
                    _clientService.Update(_client);
                    MessageBox.Show("Данные учётной записи успешно обновлены!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        #endregion
        #region Commands
        public ICommand RefreshFieldsButton => new Command(refreshfields => RefreshFields());
        public ICommand UpdateAccountButton => new Command(updateaccount => UpdateAccountData());
        #endregion
    }
}
