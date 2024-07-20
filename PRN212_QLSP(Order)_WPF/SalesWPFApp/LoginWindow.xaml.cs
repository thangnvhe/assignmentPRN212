using DataAccess.Repository;
using System.Windows;

namespace SalesWPFApp
{
    public partial class LoginWindow : Window
    {
        IMemberRepository memberRepository;
        public LoginWindow(IMemberRepository repository)
        {
            InitializeComponent();
            memberRepository = repository;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool isAdmin = memberRepository.IsAdmin(new() { Email = txtEmail.Text, Password = txtPassword.Password }); ;

            if (isAdmin)
            {
                var main = new MainWindow(true);
                main.Show();
                Close();
            }
            else
            {
                var member = memberRepository.VerifyMember(new() { Email = txtEmail.Text, Password = txtPassword.Password });
                if (member != null)
                {
                    var main = new MainWindow();
                    main.Show();
                    Close();
                }
                else
                {
                    lbError.Content = "Wrong email or password";
                }
            }
        }
    }
}
