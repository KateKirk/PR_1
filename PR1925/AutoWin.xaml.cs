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
using System.Windows.Threading;

namespace PR1925
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt;
        public int startBlockTime = 11;
        public int BlockTime = 11;
        UsersEntities usersEntities = new UsersEntities();
        int tryies;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            CheckData();
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.ShowDialog();
        }

        void CheckData()
        {

            var check= usersEntities.users.FirstOrDefault(x => x.login == loginTextBox.Text && x.password == passwordTextBox.Password);
            if (check == null)
            {
                tryies++;
                MessageBox.Show("Неверно введен логин или пароль");
                if (tryies == 3)
                {
                    signUpButton.IsEnabled = false;
                    regButton.IsEnabled = false;
                    dt.Start();
                }
                if (tryies > 3)
                {
                    startBlockTime += 5;
                    BlockTime = startBlockTime;
                    signUpButton.IsEnabled = false;
                    regButton.IsEnabled = false;
                    dt.Start();
                }
            }
            else
            {
                UsersWindow usersWindow = new UsersWindow(check);
                usersWindow.Show();
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += Dt_Tick;
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            //BlockTime = startBlockTime;
            BlockTime--;
            signUpButton.Content = BlockTime.ToString();
            if (BlockTime == 0)
            {
                dt.Stop();
                signUpButton.Content = "Войти";
                BlockTime = startBlockTime;
                signUpButton.IsEnabled = true;
                regButton.IsEnabled = true;
            }
        }
    }
}
