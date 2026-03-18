using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace WpfControlsDemo
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<SimpleItem> ListViewItems { get; } = new ObservableCollection<SimpleItem>();
        public ObservableCollection<GridItem> GridItems { get; } = new ObservableCollection<GridItem>();

        public MainWindow()
        {
            InitializeComponent();
            // populate ListView
            ListViewItems.Add(new SimpleItem { Name = "Alpha", Value = "1" });
            ListViewItems.Add(new SimpleItem { Name = "Bravo", Value = "2" });
            ListViewItems.Add(new SimpleItem { Name = "Charlie", Value = "3" });
            ListViewSimple.ItemsSource = ListViewItems;

            // populate DataGrid
            GridItems.Add(new GridItem { Id = 1, Description = "First row", IsActive = true });
            GridItems.Add(new GridItem { Id = 2, Description = "Second row", IsActive = false });
            GridItems.Add(new GridItem { Id = 3, Description = "Third row", IsActive = true });
            DataGridSample.ItemsSource = GridItems;
        }

        private void BtnSimple_Click(object sender, RoutedEventArgs e)
        {
            LblStatus.Content = "Status: Button clicked";
        }

        private void MenuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open clicked", "Menu");
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuHelpAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("WPF Controls Demo\nVersion 1.0", "About");
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void BtnShowDialog_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Window
            {
                Title = "Modal Dialog",
                Width = 300,
                Height = 180,
                Content = new TextBlock { Text = "This is a dialog", Margin = new Thickness(12) }
            };
            dlg.ShowDialog();
        }
    }

    public class SimpleItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class GridItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
