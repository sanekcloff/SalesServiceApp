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
    public class EmpNewsManagerViewModel : ViewModelBase
    {
        public EmpNewsManagerViewModel(Employee employee, New? @new, NewsService newsService, Window window)
        {
            if (@new == null)
            {
                New = new() { Employee = employee, PublicationDate = DateTime.Now };
            }
            else
            {
                Header = @new.Header;
                Text = @new.Text;
                New = @new;
            }
            _newsService = newsService;
            _window = window;
        }
        #region Elements
        private Window _window;
        #endregion

        #region Services
        private NewsService _newsService;
        #endregion

        #region Fields & Properties
        private New? _new;
        private string _header;
        private string _text;

        public New? New { get => _new; set => Set(ref _new, value, nameof(New)); }
        public string Header { get => _header; set => Set(ref _header, value, nameof(Header)); }
        public string Text { get => _text; set => Set(ref _text, value, nameof(Text)); }
        #endregion

        #region Methods
        private bool FieldsIsNull() => (string.IsNullOrEmpty(Header) || string.IsNullOrEmpty(Text));
        private void AddNew()
        {
            if (!FieldsIsNull())
            {
                New.Header = Header;
                New.Text = Text;
                _newsService.Insert(New);
                MessageBox.Show($"Новость на тему \"{Header}\" опубликована!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                _window.Close();
            }
            else
                MessageBox.Show("Заполните поля!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void EditNew()
        {
            if (!FieldsIsNull())
            {
                New.Header = Header;
                New.Text = Text;
                _newsService.Update(New);
                MessageBox.Show($"Новость обновлена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                _window.Close();
            }
            else
                MessageBox.Show("Заполните поля!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region
        public ICommand AddNewButton => new Command(addnew => AddNew());
        public ICommand EditNewButton => new Command(editnew => EditNew());
        #endregion
    }
}
