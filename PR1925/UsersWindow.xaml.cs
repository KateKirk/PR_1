﻿using System;
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

namespace PR1925
{
    /// <summary>
    /// Логика взаимодействия для UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
       
        users user1;
        public UsersWindow(users users)
        {
            InitializeComponent();
            user1 = users;
            fullNameLabel.Content = $"{user1.full_name}\n{user1.types.type}";
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow changeWindow = new ChangeWindow(user1);
            changeWindow.Show();
            this.Close();
        }
    }
}
