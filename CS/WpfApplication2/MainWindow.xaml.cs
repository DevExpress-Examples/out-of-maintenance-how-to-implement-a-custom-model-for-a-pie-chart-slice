using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Charts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WpfApplication2 {
    public partial class MainWindow : Window {
        public class Item {
            public string Name { get; set; }
        }

        public List<Item> data;
        public List<Item> Data {
            get {
                return data;
            }
        }

        public MainWindow() {
            InitializeComponent();
            chart.DataContext = this;
            using (StreamReader r = new StreamReader(@"..\..\Data\votes.txt")) {
                string json = r.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<Item>>(json);
            }
        }
    }
}