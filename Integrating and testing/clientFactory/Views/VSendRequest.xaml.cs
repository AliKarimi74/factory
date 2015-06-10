﻿using System;
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

namespace clientFactory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VSendRequest : Window
    {
        private CRequestController _controller;

        public VSendRequest()
        {
            InitializeComponent();
        }

        public void SetController(CRequestController con)
        {
            _controller = con;
        }

        private void BackEvent_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            _controller.SendRequestEvent();
        }

    }
}
