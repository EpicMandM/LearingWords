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
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using OfficeOpenXml;
namespace LearingWords
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    ///
    //TODO: Сделать чтоб брались слова из разных диапазонов
    //TODO: Сделать рандомизацию
    enum Positions
    {
        Infinitive = 1, 
        PastTense = 2,
        PastParticiple = 3,
        Value = 4
    }
    public partial class MainWindow : Window
    {
        int index = 2;
        ExcelWorksheet ws;
        public bool RangeUsingFlag { get; set; }
        public int CurrentMax { get; set; }
        public int CurrentMin { get; set; }
        public bool IsRandomized { get; set; }
        public MainWindow()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            InitializeComponent();
            CurrentMax = 277;
            UpdateWord();
        }
        private void UpdateWord()
        {


            //Get the Worksheet created in the previous codesample. 
            ////                    элемент/столбик
            ///lvar fi = new FileInfo(@"f:\words.xlsx");
            if(IsRandomized)
            {
                var fi = new FileInfo(@".\words.xlsx");
                var p = new ExcelPackage(fi);
                //Get the Worksheet created in the previous codesample. 
                ws = p.Workbook.Worksheets["MainList"];
                index = new Random().Next(CurrentMin, CurrentMax);
                label1.Content = ws.Cells[index, 1].Value;
                //The style object is used to access most cells formatting and styles.
                //ws.Cells[2, 1].Style.Font.Bold = true;
                //Save and close the package.
            }
            else
            {
                var fi = new FileInfo(@".\words.xlsx");
                var p = new ExcelPackage(fi);
                //Get the Worksheet created in the previous codesample. 
                ws = p.Workbook.Worksheets["MainList"];
                label1.Content = ws.Cells[index, 1].Value;
                //The style object is used to access most cells formatting and styles.
                //ws.Cells[2, 1].Style.Font.Bold = true;
                //Save and close the package.

            }

        }
        public void ChangeRange(int min, int max)
        {
            index = min;
            CurrentMin = min;
            CurrentMax = max;
            UpdateWord();
        }
        public void SetDefault()
        {
            CurrentMin = 0;
            CurrentMax = 277;
        }
        private void Clear()
        {
            textBox.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                bool flag;
                if(Convert.ToString(ws.Cells[index, (int)Positions.PastTense].Value).Contains(textBox.Text) && textBox.Text != "")
                {
                    flag = true;
                }
                else
                {
                    labelerror1.Content = Int32.Parse(labelerror1.Content.ToString()) + 1;
                    flag = false;
                }
                if (Convert.ToString(ws.Cells[index, (int)Positions.PastParticiple].Value).Contains(textBox2.Text) && textBox2.Text != "")
                {
                    flag = flag && true;
                }
                else
                {
                    labelerror2.Content = Int32.Parse(labelerror2.Content.ToString()) + 1;
                    flag = false;
                }
                if (Convert.ToString(ws.Cells[index, (int)Positions.Value].Value).Contains(textBox3.Text) && textBox3.Text != "")
                {
                    flag = flag && true;
                }
                else
                {
                    labelerror3.Content = Int32.Parse(labelerror3.Content.ToString()) + 1;
                    flag = false;
                }
                if(flag)
                {
                    labelerrormain.Content = Int32.Parse(labelerrormain.Content.ToString()) + 1;
                }
                if(!IsRandomized)
                    index++;
                UpdateWord();
                Clear();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new RangeWindow(this).Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The program was created by Vladyslav Zhukov for simple learning Irregular Verbs. ", "About", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IsRandomized = true;
            UpdateWord();
        }
    }
}
