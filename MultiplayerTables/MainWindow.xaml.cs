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

namespace MultiplayerTables
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainVm _mainVm = new MainVm();

        public MainWindow()
        {
            InitializeComponent();

            _mainVm.Init();
            this.DataContext = _mainVm;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (!_mainVm.NextTable())
            {
                MessageBox.Show("Ошибка перехода к следующему раунду");
            }
            else
            {
                full.Items.Refresh();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _mainVm.RefreshTable();
        }
    }
}
