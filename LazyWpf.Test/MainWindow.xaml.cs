/**
   Copyright 2026 masterLazy

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */
using System.ComponentModel;
using System.Windows;

namespace LazyWpf.Test {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow() {
            InitializeComponent();
        }

        private string _msgBoxResult = "Result: --";
        public string MsgBoxResult {
            get => _msgBoxResult;
            set {
                if (value == _msgBoxResult) return;
                _msgBoxResult = value;
                OnPropertyChanged(nameof(MsgBoxResult));
            } 
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            ((Button)sender).IsAccented = !((Button)sender).IsAccented;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            var msgBox = new MsgBox("Do you want a dessert?", "Question", MbOpt.YesNoCancel, MbBtn.Yes, MbIco.Info) { Owner = this };
            msgBox.ShowDialog();
            MsgBoxResult = $"Result: {msgBox.Result}";
        }
    }
}