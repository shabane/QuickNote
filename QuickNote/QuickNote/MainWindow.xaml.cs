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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace QuickNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        System.Windows.Forms.NotifyIcon TryIcon = new System.Windows.Forms.NotifyIcon();

        private void btnClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TryIcon.Icon = new System.Drawing.Icon("note.ico");
            TryIcon.Visible = true;
            TryIcon.MouseDoubleClick += TryIcon_MouseDoubleClick;
            this.Hide();
        }

        private void TryIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Show();
            TryIcon.Visible = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowInTaskbar = false;

            double screenwith = SystemParameters.PrimaryScreenWidth;
            double screenhight = SystemParameters.PrimaryScreenHeight;

            this.Top = screenhight / 2;
            this.Left = screenwith - screenwith / 3;
            if (File.Exists("QuickNote.txt") != true)
            {
                File.AppendAllText("QiuckNote.txt", "");
            }
            string txt = File.ReadAllText("QiuckNote.txt");
            txtprim.AppendText(txt.Trim());

            string Currentdir = Directory.GetCurrentDirectory() + "QuickNote.exe";
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            registryKey.SetValue("ApplicationName", Currentdir);

            TryIcon.Icon = new System.Drawing.Icon("note.ico");
            TryIcon.Visible = true;
            TryIcon.MouseDoubleClick += TryIcon_MouseDoubleClick;
            this.Hide();
        }

        private void txtprim_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txtRange = new TextRange(txtprim.Document.ContentStart, txtprim.Document.ContentEnd);
            File.WriteAllText("QiuckNote.txt", txtRange.Text.Trim());
        }
    }
}
