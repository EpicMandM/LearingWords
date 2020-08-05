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

namespace LearingWords
{
    /// <summary>
    /// Логика взаимодействия для RangeWindow.xaml
    /// </summary>
    public partial class RangeWindow : Window
    {
        private MainWindow window;
        public void Enable(bool flag)
        {
            label.IsEnabled = flag;
            label1.IsEnabled = flag;
            textBox.IsEnabled = flag;
            textBox1.IsEnabled = flag;
        }
        public RangeWindow(MainWindow window)
        {
            this.window = window;
            InitializeComponent();

        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            Enable(true);
            window.RWFlag = true;
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            Enable(false);
            window.RWFlag = false;
            textBox.Text = null;
            textBox1.Text = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((textBox1.Text != null && textBox.Text != null) && (textBox1.Text != string.Empty && textBox.Text != string.Empty))
                window.ChangeRange(int.Parse(textBox.Text), int.Parse(textBox1.Text));
        }
    }
}
