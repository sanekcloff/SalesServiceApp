using AppData.Commands;
using AppData.DataBaseData;
using AppData.Entities;
using AppData.Services;
using AppData.Validations;
using AppData.ViewModel;
using ClientApp.Views;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ClientApp.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        public RegistrationViewModel(ApplicationDbContext ctx)
        {
            _clientService = new(ctx);
            _employeeService = new(ctx);
        }
        #region Service
        private ClientService _clientService;
        private EmployeeService _employeeService;
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

        public string LastName
        {
            get => _lastName; set
            {

                Set(ref _lastName, FirstLetterToUpper(value), nameof(LastName));
            }
        }
        public string FirstName { get => _firstName; set => Set(ref _firstName, FirstLetterToUpper(value), nameof(FirstName)); }
        public string MiddleName { get => _middleName; set => Set(ref _middleName, FirstLetterToUpper(value), nameof(MiddleName)); }
        public string Organization { get => _organization; set => Set(ref _organization, value, nameof(Organization)); }
        public string Email
        {
            get => _email; set
            {
                Set(ref _email, value, nameof(Email));
            }
        }
        public string Phone
        {
            get => _phone; set
            {
                Set(ref _phone, value, nameof(Phone));
            }
        }
        public string Login { get => _login; set => Set(ref _login, value, nameof(Login)); }
        public string Password { get => _password; set => Set(ref _password, value, nameof(Password)); }
        #endregion
        #region Methods
        private string FirstLetterToUpper(string value)
        {
            var newString = new StringBuilder();
            if (!string.IsNullOrEmpty(value))
            {
                if (value[0] != value.ToUpper()[0])
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (i == 0)
                            newString.Append(value.ToUpper()[i]);
                        else
                            newString.Append(value[i]);
                    }
                    value = newString.ToString();
                }
            }
                return value;
        }
        private bool UserIsExist() => _clientService.GetClients().Any(c => c.Password == Password && c.Login == Login) || _employeeService.GetEmployees().Any(e => e.Login == Login && e.Password==Password);
        private bool PasswordAlredyInUse() => _clientService.GetClients().Any(c=>c.Password==Password) || _employeeService.GetEmployees().Any(e=>e.Password==Password);
        private bool LoginAlredyInUse() => _clientService.GetClients().Any(c => c.Login == Login) || _employeeService.GetEmployees().Any(e=>e.Login==Login);
        private bool EnailAlredyInUse() => _clientService.GetClients().Any(c => c.Email == Email);
        private bool PhoneAlredyInUse() => _clientService.GetClients().Any(c => c.Phone == Phone);
        private bool PropertiesIsNull() => (string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(MiddleName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Organization));
        // проверка на корректность данных
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
        private void Registration()
        {
            if (PropertiesIsNull())
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (DataIsCorrect())
            {
                if (UserIsExist())
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
                    _clientService.Insert(new Client { Login = Login, Password = Password, FirstName = FirstName, LastName = LastName, MiddleName = MiddleName, Email = Email, Organization = Organization, Phone = $"8{Phone}" });
                    MessageBox.Show("Учётная запись зарегистрирована!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenAuthorizationWindow();
                }
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
