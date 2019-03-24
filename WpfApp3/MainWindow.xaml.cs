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
using Xceed.Wpf.Toolkit;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CheckComboBoxViewModel _vm;
        public MainWindow()
        {
            InitializeComponent();

            DataContext = _vm = new CheckComboBoxViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(CheckComboBoxPanel.Children.Count == 0)
            {
                CheckComboBox ccb = new CheckComboBox();
                CheckComboBoxPanel.Children.Add(ccb);

                ccb.SetBinding(CheckComboBox.ItemsSourceProperty,
                    new Binding("AllItems") { Source = DataContext });

                ccb.SetBinding(CheckComboBox.SelectedItemsOverrideProperty,
                    new Binding("SelectedItems") { Source = DataContext, Mode=BindingMode.TwoWay });
            }
            else
            {
                CheckComboBoxPanel.Children.RemoveAt(0);
            }
        }
    }
}
