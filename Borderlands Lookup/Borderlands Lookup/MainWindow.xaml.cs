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
using Borderlands_Lookup.Classes;
using System.ComponentModel;

namespace Borderlands_Lookup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// TODO: Search Suggestions
    public partial class MainWindow : Window
    {
        _Stack stkRecent = new _Stack(); // my own simple version of a stack... the default C# stack annoys me
        ItemDataParser current = new ItemDataParser();
        List<string> _LegendaryWeapons = new List<string>();
        BackgroundWorker bwLegendaries = new BackgroundWorker();
        /* 
         * I did this project before I had knowledge of threading... and unfortunately I stopped working on it before I could fully implement the
         * background worker
        */
        public MainWindow()
        {
            InitializeComponent();
            bwLegendaries.DoWork += bwLegendaries_DoWork;
            bwLegendaries.WorkerReportsProgress = true;
            bwLegendaries.ProgressChanged += bwLegendaries_ProgressChanged;
        }

        void bwLegendaries_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        void bwLegendaries_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ItemDataParser item = new ItemDataParser();
            item.GetItemData("Hellfire_(Borderlands_2)");
            current = item;
        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMaximize_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = System.Windows.WindowState.Normal;
            else
                this.WindowState = System.Windows.WindowState.Maximized;
        }

        private void btnMinimize_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            LegendaryDataParser _legendaries = new LegendaryDataParser();
            string _error = "This was supposed to display a list of all legendary items in both Borderlands 1 & 2... but it is not working at the moment due to my lack of updating this program for about two years.";
            lstMain.Items.Add(_error);
            // Uncomment these lines to return the program to its bugged satte. You should get an error that basically means the program requested
            // a page from the Borderlands Wiki that does not exist. This is due to the changes that have been made to the Wiki and me not updating
            // the program for about two years or so.
            // _legendaries.ParseLegendaryItems();
            // _legendaries.GetAllLegendaryData();
            // this.lstMain.ItemsSource = _legendaries.LegendaryItemList;
        }
    }
}
