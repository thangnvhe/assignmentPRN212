using System.ComponentModel;
using System.Windows;

namespace SalesWPFApp
{
    public partial class MainWindow : Window
    {

        private bool _isAdmin;
        public bool isAdmin
        {
            get { return _isAdmin; }
            set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                }
            }
        }
        public MainWindow(bool isAdmin = false)
        {
            InitializeComponent();
            this.isAdmin = isAdmin;
            this.DataContext = this;
        }

        private void ManagementControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
