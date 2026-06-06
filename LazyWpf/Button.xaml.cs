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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LazyWpf {
    /// <summary>
    /// Button.xaml 的交互逻辑
    /// </summary>
    public partial class Button : UserControl {
        // 属性 "Text"
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Button));
        public string Text {
            get { return (string)GetValue(TextProperty); }
            set {
                SetValue(TextProperty, value);
                SetMargin();
            }
        }

        // 属性 "Icon"
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(Button));
        public string Icon {
            get { return (string)GetValue(IconProperty); }
            set {
                SetValue(IconProperty, value);
                SetMargin();
            }
        }

        // 属性 "IsAccented"
        public static readonly DependencyProperty IsAccentedProperty =
            DependencyProperty.Register("IsAccented", typeof(bool), typeof(Button));
        public bool IsAccented {
            get { return (bool)GetValue(IsAccentedProperty); }
            set {
                SetValue(IsAccentedProperty, value);
                if (value) {
                    btn.SetResourceReference(StyleProperty, "AccentButtonStyle");
                } else {
                    btn.SetResourceReference(StyleProperty, "DefaultButtonStyle");
                }
            }
        }

        // 事件 "Clicked"
        public static readonly RoutedEvent ClickEvent =
            ButtonBase.ClickEvent.AddOwner(typeof(Button));
        public event RoutedEventHandler Click {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        public Button() {
            InitializeComponent();
        }

        private void root_Loaded(object sender, RoutedEventArgs e) {
            IsAccented = IsAccented; // 写法有点迷惑，但是简洁有用
            SetMargin();
        }

        private void SetMargin() {
            if (Icon == null || Text == null) return;
            if (Icon.Length > 0 && Text.Length > 0) {
                TbIcon.Margin = Tck;
            } else {
                TbIcon.Margin = TckNone;
            }
        }
        private static readonly Thickness Tck = new(0, 0, 10, 0);
        private static readonly Thickness TckNone = new(0, 0, 0, 0);
    }
}
