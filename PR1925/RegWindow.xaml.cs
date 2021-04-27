using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        UsersEntities context = new UsersEntities();
        public RegWindow()
        {
            InitializeComponent();
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {

            if (CheckData())
            {
                var profile = new users { login = loginTextBox.Text, password = passwordTextBox.Password, email = emailTextBox.Text, full_name = fullNameTextBox.Text, type = 2, last_change = DateTime.UtcNow };
                context.users.Add(profile);
                context.SaveChanges();
                MessageBoxResult result = new MessageBoxResult();
                result = MessageBox.Show("Вы успешно зарегистрированы", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    this.Close();
                } 
            }

           
        }
        public bool CheckData()
        {
            string regexpass = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$";
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            string password = passwordTextBox.Password;
            bool capital = password.Any(Char.IsUpper);
            bool c = password.Any(Char.IsDigit);
            string result = "";
            if (password.Length < 6)
            {
                result += "Пароль меньше 6 символов\n";
            }
            if (capital == false)
            {
                result += "Отсутствует хотя бы 1 прописная буква\n";
            }
            if (c == false)
            {
                result += "Отсутствует хотя бы 1 цифра\n";
            }
            if (loginTextBox.Text == "")
            {
                result += "Отстутствует логин\n";
            }
                string email = emailTextBox.Text;
                if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
                {
                    result += "Email не подтвержден\n";
                }
            if (!Regex.IsMatch(password, regexpass))
            {
                result += "Отсутствует хотя бы один специальный символ\n";
            }
            
            if (result == "")
            {
                return true;
            }
            else
            {
                MessageBox.Show(result);
                return false;
            }
            
        }
            
    }
}
