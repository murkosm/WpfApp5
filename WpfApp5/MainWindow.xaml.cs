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

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {

        public MainWindow()
        {

            InitializeComponent();
           
         
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            Window1.Window1 add = new Window1.Window1();
            add.Show();
        }

        private void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
            Window1.EditWindow edit = new Window1.EditWindow();
            edit.Show();
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
