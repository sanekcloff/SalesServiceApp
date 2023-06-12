﻿using AppData.Entities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientApp.Views
{
    /// <summary>
    /// Логика взаимодействия для EmpNewsPage.xaml
    /// </summary>
    public partial class EmpNewsPage : UserControl
    {
        public EmpNewsPage(Employee employee, NewsService newsService)
        {
            InitializeComponent();
            DataContext = new EmpNewsViewModel(employee, newsService);
        }
    }
}
