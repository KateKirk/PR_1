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
    /// Логика взаимодействия для ChangeWindow.xaml
    /// </summary>
    public partial class ChangeWindow : Window
    {
        UsersEntities context;
        users _podopini;
        public ChangeWindow(users checc)
        {
            InitializeComponent();
            _podopini = checc;
            this.DataContext = _podopini;
            context = new UsersEntities();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {

            if (CheckData())
            {
                users _newuserdata = new users();
                _newuserdata = context.users.FirstOrDefault(x => x.id == _podopini.id);
                _newuserdata.full_name = fullNameTextBox.Text;
                _newuserdata.password = passwordTextBox.Password;
                _newuserdata.email = emailTextBox.Text;
                _newuserdata.login = loginTextBox.Text;
                _newuserdata.last_change = DateTime.UtcNow;
                _podopini = _newuserdata;
                context.SaveChanges();
                UsersWindow usersWin = new UsersWindow(_podopini);
                usersWin.Show();
                this.Close();
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
