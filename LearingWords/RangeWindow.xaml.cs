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
        private void Enable(bool flag)
        {
            label.IsEnabled = flag;
            label1.IsEnabled = flag;
            textBox.IsEnabled = flag;
            textBox1.IsEnabled = flag;
            if(!flag)
            {
                textBox.Text = string.Empty;
                textBox1.Text = string.Empty;
            }
        }
        private MainWindow window;
        public RangeWindow(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            if(window.RangeUsingFlag)
            {
                radioButton1.IsChecked = true;
                textBox.Text = Convert.ToString(window.CurrentMin);
                textBox1.Text = Convert.ToString(window.CurrentMax);
            }
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            Enable(true);
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            if(radioButton1.IsChecked ?? false)
            {
                window.RangeUsingFlag = true;
                try
                {
                    if (int.Parse(textBox.Text) > int.Parse(textBox1.Text) || int.Parse(textBox.Text) < 0 || int.Parse(textBox1.Text) < 0)
                        throw new ArgumentException();
                    window.ChangeRange(int.Parse(textBox.Text), int.Parse(textBox1.Text));

                }
                catch (Exception)
                {

                    MessageBox.Show("Вы ввели некорректный диапазон!");
                    radioButton.IsChecked = true;
                    window.RangeUsingFlag = false;
                }
            }
            else
            {
                window.RangeUsingFlag = false;
            }
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            Enable(false);
        }
    }
}
