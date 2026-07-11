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
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LazyWpf {
    /// <summary>
    /// ProgBar.xaml 的交互逻辑
    /// </summary>
    public partial class ProgBar : UserControl {
        // 属性 "Minimum"
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(ProgBar));
        public int Minimum {
            get => (int)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        // 属性 "Minimum"
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(ProgBar));
        public int Maximum {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        // 属性 "Value"
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(ProgBar),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValuePropertyChanged));
        public int Value {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        // 属性 "IsIndeterminate"
        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool), typeof(ProgBar));
        public bool IsIndeterminate {
            get => (bool)GetValue(IsIndeterminateProperty);
            set => SetValue(IsIndeterminateProperty, value);
        }

        // 属性 "Background"
        public static new readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(ProgBar));
        public new Brush Background {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        // 属性 "FadeOutWhenComplete"
        public static new readonly DependencyProperty FadeOutWhenCompleteProperty =
            DependencyProperty.Register("FadeOutWhenComplete", typeof(bool), typeof(ProgBar));
        public bool FadeOutWhenComplete {
            get => (bool)GetValue(FadeOutWhenCompleteProperty);
            set => SetValue(FadeOutWhenCompleteProperty, value);
        }

        public ProgBar() {
            InitializeComponent();
        }

        // 动画
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var control = (ProgBar)d;
            control.AnimateToValue((int)e.NewValue);

            if (!control.IsIndeterminate && control.FadeOutWhenComplete) {
                if ((int)e.NewValue >= control.Maximum)
                    control.AnimateOpacityTo(0.0, 300);
                else
                    control.AnimateOpacityTo(1.0, 0);
            }
        }

        // 进度动画
        private void AnimateToValue(double targetValue) {
            ProgressBar.BeginAnimation(ProgressBar.ValueProperty, null);
            var animation = new DoubleAnimation {
                From = ProgressBar.Value,
                To = targetValue,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase()
            };
            ProgressBar.BeginAnimation(ProgressBar.ValueProperty, animation);
        }

        // 渐隐动画
        private void AnimateOpacityTo(double targetOpacity, long delay) {
            ProgressBar.BeginAnimation(ProgressBar.OpacityProperty, null);
            var animation = new DoubleAnimation {
                To = targetOpacity,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase(),
                BeginTime = TimeSpan.FromMilliseconds(delay)
            };
            ProgressBar.BeginAnimation(ProgressBar.OpacityProperty, animation);
        }

        private void root_Loaded(object sender, RoutedEventArgs e) {
            if (!IsIndeterminate && FadeOutWhenComplete && Value >= Maximum) {
                ProgressBar.Opacity = 0;
            }
        }
    }
}
