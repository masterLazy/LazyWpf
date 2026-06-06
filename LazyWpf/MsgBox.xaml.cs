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
using System.Media;
using System.Windows;
using System.Windows.Media;

namespace LazyWpf {
    /// <summary>
    /// MsgBox.xaml 的交互逻辑
    /// </summary>


    public enum MbOpt {
        OK,
        OKCancel,
        AbortRetryIgnore,
        YesNoCancel,
        YesNo,
        RetryCancel,
        RetryCancelContinue
    }

    public enum MbBtn { None, Yes, No, Abort, Retry, Continue, Ignore, OK, Cancel }

    public enum MbIco { None, Error, Warning, Info, Success }

    public partial class MsgBox : Window {
        public MbBtn Result { get; private set; } = MbBtn.None;

        public string Text { get; set; }
        public MbOpt Option { get; set; } = MbOpt.OK;
        public MbBtn AccentButton { get; set; } = MbBtn.None;
        public new MbIco Icon { get; set; } = MbIco.None;

        // <构造函数>

        public MsgBox(string text, string caption) {
            InitializeComponent();
            Text = text;
            Title = caption;
        }

        public MsgBox(string text, string caption, MbOpt option) {
            InitializeComponent();
            Text = text;
            Title = caption;
            Option = option;
        }

        public MsgBox(string text, string caption, MbOpt option, MbBtn accentButton) {
            InitializeComponent();
            Text = text;
            Title = caption;
            Option = option;
            AccentButton = accentButton;
        }

        public MsgBox(string text, string caption, MbOpt option, MbIco icon) {
            InitializeComponent();
            Text = text;
            Title = caption;
            Option = option;
            Icon = icon;
        }

        public MsgBox(string text, string caption, MbOpt option, MbBtn accentButton, MbIco icon) {
            InitializeComponent();
            Text = text;
            Title = caption;
            Option = option;
            AccentButton = accentButton;
            Icon = icon;
        }

        // </构造函数>

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            SetBtnVisibility();
            GetBtnByEnum(AccentButton)?.IsAccented = true;
            SetIcon();
            MbText.Text = Text;
        }

        private void SetBtnVisibility() {
            BtnAbort.Visibility =
            BtnCancel.Visibility =
            BtnContinue.Visibility =
            BtnIgnore.Visibility =
            BtnNo.Visibility =
            BtnOK.Visibility =
            BtnRetry.Visibility =
            BtnYes.Visibility = Visibility.Collapsed;
            switch (Option) {
                case MbOpt.OK:
                    BtnOK.Visibility = Visibility.Visible;
                    break;
                case MbOpt.OKCancel:
                    BtnCancel.Visibility = Visibility.Visible;
                    BtnOK.Visibility = Visibility.Visible;
                    break;
                case MbOpt.AbortRetryIgnore:
                    BtnAbort.Visibility = Visibility.Visible;
                    BtnRetry.Visibility = Visibility.Visible;
                    BtnIgnore.Visibility = Visibility.Visible;
                    break;
                case MbOpt.YesNoCancel:
                    BtnYes.Visibility = Visibility.Visible;
                    BtnNo.Visibility = Visibility.Visible;
                    BtnCancel.Visibility = Visibility.Visible;
                    break;
                case MbOpt.YesNo:
                    BtnYes.Visibility = Visibility.Visible;
                    BtnNo.Visibility = Visibility.Visible;
                    break;
                case MbOpt.RetryCancel:
                    BtnRetry.Visibility = Visibility.Visible;
                    BtnCancel.Visibility = Visibility.Visible;
                    break;
                case MbOpt.RetryCancelContinue:
                    BtnCancel.Visibility = Visibility.Visible;
                    BtnRetry.Visibility = Visibility.Visible;
                    BtnContinue.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void SetIcon() {
            switch (Icon) {
                case MbIco.None:
                    MbIcon.Text = "";
                    break;
                case MbIco.Error:
                    MbIcon.Text = "\xEB90";
                    MbIcon.Foreground = FindResource("BhCritical") as SolidColorBrush;
                    SystemSounds.Hand.Play();
                    break;
                case MbIco.Warning:
                    MbIcon.Text = "\xE814";
                    MbIcon.Foreground = FindResource("BhCaution") as SolidColorBrush;
                    SystemSounds.Asterisk.Play();
                    break;
                case MbIco.Info:
                    MbIcon.Text = "\xF167";
                    MbIcon.Foreground = FindResource("BhAttention") as SolidColorBrush;
                    SystemSounds.Asterisk.Play();
                    break;
                case MbIco.Success:
                    MbIcon.Text = "\xEC61";
                    MbIcon.Foreground = FindResource("BhSuccess") as SolidColorBrush;
                    SystemSounds.Asterisk.Play();
                    break;
            }
        }

        private Button? GetBtnByEnum(MbBtn btn) {
            return btn switch {
                MbBtn.Yes => BtnYes,
                MbBtn.No => BtnNo,
                MbBtn.Abort => BtnAbort,
                MbBtn.Retry => BtnRetry,
                MbBtn.Continue => BtnContinue,
                MbBtn.Ignore => BtnIgnore,
                MbBtn.OK => BtnOK,
                MbBtn.Cancel => BtnCancel,
                _ => null,
            };
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            foreach (MbBtn btn in Enum.GetValues(typeof(MbBtn))) {
                if (sender == GetBtnByEnum(btn)) {
                    Result = btn;
                    Close();
                    return;
                }
            }
            Result = MbBtn.None;
        }
    }
}
