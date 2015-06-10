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

namespace clientFactory
{
    /// <summary>
    /// Interaction logic for RequestList.xaml
    /// </summary>
    public partial class VRequestList : Window
    {
        public CRequestController controller;

        public List<RequestModel> inbox_item;
        public List<RequestModel> outbox_item;

        public VRequestList()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Inbox_DG.ItemsSource = inbox_item;
            Outbox_DG.ItemsSource = outbox_item;
            Show();
        }

        private void BackEvent_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowEvent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
