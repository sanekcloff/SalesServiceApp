using SalesServiceApplication.Commands;
using SalesServiceApplication.Data;
using SalesServiceApplication.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SalesServiceApplication.ViewModels.Windows
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            _ctx = new ApplicationDbContext();
        }
        #region Context
        private ApplicationDbContext _ctx;
        #endregion
        #region Fields
        #endregion
        #region Properties
        #endregion
        #region Methods
        private void OpenAuthoriztionWindow() => new AuthoriztionWindow(_ctx).ShowDialog();
        #endregion
        #region Commands
        public ICommand ManagementButton => new Command(mb=>OpenAuthoriztionWindow());
        #endregion
    }
}
