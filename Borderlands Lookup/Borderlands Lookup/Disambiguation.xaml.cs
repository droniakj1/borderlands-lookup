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
using System.Windows.Shapes;

namespace Borderlands_Lookup
{
    /// <summary>
    /// Interaction logic for Disambiguation.xaml
    /// </summary>
    public partial class Disambiguation : Window
    {

        public static readonly DependencyProperty DisambiguationDataProperty = DependencyProperty.Register("DisambiguationData", typeof(string),
            typeof(Disambiguation));
        public string DisambiguationData
        {
            get { return (string)GetValue(DisambiguationDataProperty); }
            set { SetValue(DisambiguationDataProperty, value); }
        }

        public Disambiguation()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
