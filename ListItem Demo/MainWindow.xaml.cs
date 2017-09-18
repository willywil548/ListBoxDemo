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
using System.Collections.ObjectModel;

namespace ListItem_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //populate listbox1 with four items
            addListBoxItems();
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            //Add items from one ListBox to another
            var selectedItems = listBox1.SelectedItems;
                foreach(string i in selectedItems)
            {
                listBox2.Items.Add(i);
            }
            for (int x = selectedItems.Count-1; x > -1; x--)
                listBox1.Items.Remove(selectedItems[x]);
        }

        private void lsb1_Selection(object sender, SelectionChangedEventArgs e)
        {
            btnTransfer.IsEnabled = true;
        }
        //Reset the listbox to start positions
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            //Remove reamining items from listbox1 and listbox2
            for (int x = listBox1.Items.Count - 1; x > -1; x--)
                listBox1.Items.RemoveAt(x);
            for (int x = listBox2.Items.Count - 1; x > -1; x--)
                listBox2.Items.RemoveAt(x);
            //Require both listboxes to refresh
            listBox1.Items.Refresh();
            listBox2.Items.Refresh();
            //Add the original items back to listbox1
            addListBoxItems();
        }
        private void addListBoxItems()
        {
            //Build out listbox1 with some data
            ListDemo newItem = new ListDemo();
            List<ListDemo> items = new List<ListDemo>();
            ObservableCollection<ListDemo> dataList = new ObservableCollection<ListDemo>(newItem.returnItems("first", items));
            dataList = new ObservableCollection<ListDemo>(newItem.returnItems("second", items));
            dataList = new ObservableCollection<ListDemo>(newItem.returnItems("third", items));
            dataList = new ObservableCollection<ListDemo>(newItem.returnItems("fourth", items));
            //write the dataList to the 
            foreach (ListDemo i in dataList)
            {
                listBox1.Items.Add(i.aName.ToString());
            }
        }

        private void btnTransBack_Click(object sender, RoutedEventArgs e)
        {
            //transfer items back from one ListBox to another
            //Gather the selected items from the listbox on the right
            var selectedItems = listBox2.SelectedItems;
            //Remove all items from the box on the left
            for (int x = listBox1.Items.Count - 1; x > -1; x--)
                listBox1.Items.RemoveAt(x);
            listBox1.Items.Refresh();
            //Add all original items back to the box on the right, makes sure the items are in order
            addListBoxItems();
            // removes the selected items from the box on the right
            for (int x = selectedItems.Count - 1; x > -1; x--)
            {
                listBox2.Items.Remove(selectedItems[x]);
            }
            //removes the remaining item in the box on the right from the box on the left
            for (int x = listBox2.Items.Count - 1; x > -1; x--)
            {
                listBox1.Items.Remove(listBox2.Items[x]);
            }
        }
    }
    public class ListDemo
    {
        public string aName { get; set; }

        public ListDemo() { }

        public List<ListDemo> returnItems(string a, List<ListDemo> items)
        {
            items.Add(new ListDemo()
            {
                aName = a
            });
            return items;
        }
    }

}
