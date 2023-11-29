using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AutoComplete.xaml
    /// </summary>
    public partial class AutoComplete : UserControl
    {

        public ObservableCollection<string> Items
        {
            get { return (ObservableCollection<string>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<string>), typeof(AutoComplete), new PropertyMetadata(new ObservableCollection<string>()));


        public AutoComplete()
        {
            InitializeComponent();
        }


        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = sender as ListBoxItem;
            this.txt.Text = listBoxItem.Content.ToString();
            this.pop.IsOpen = false;
        }

        private void txt_KeyUp(object sender, KeyEventArgs e)
        {
            string keyword = this.txt.Text.Trim();
            if (keyword.Length > 0)
            {
                cc.Content = Items.Where(x => x.StartsWith(keyword));
                //lst.ItemsSource = Items.Where(x => x.StartsWith(keyword));
                this.pop.IsOpen = true;
            }
            else
            {
                this.pop.IsOpen = false;
            }

            ListBox listbox = FindChildElement((FrameworkElement)this.pop.Child, "lst") as ListBox;

            if (listbox != null && e.Key == Key.Down && this.pop.IsOpen)
            {
                int selectedIndex = listbox.SelectedIndex;
                listbox.SelectedIndex = (selectedIndex + 1) % listbox.Items.Count;
            }
            else if (listbox != null && e.Key == Key.Up && this.pop.IsOpen)
            {
                int selectedIndex = listbox.SelectedIndex;
                listbox.SelectedIndex = Math.Abs((selectedIndex - 1) % listbox.Items.Count);
            }
        }

        private FrameworkElement FindChildElement(FrameworkElement element, string name)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                var objChild = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
                if (objChild != null && objChild.Name.Equals(name))
                {
                    return objChild;
                }
                else
                {
                    return FindChildElement(objChild, name);
                }
            }
            return null;
        }


        private void ListBoxItem_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (pop.IsOpen && e.Key == Key.Enter)
            {
                ListBox listbox = FindChildElement((FrameworkElement)this.pop.Child, "lst") as ListBox;
                if (listbox != null && listbox.SelectedItem != null)
                {
                    string v = listbox.SelectedItem.ToString();
                    this.txt.Text = v;
                    this.pop.IsOpen = false;
                    this.btn.Focus();
                }
            }

        }
    }


}
