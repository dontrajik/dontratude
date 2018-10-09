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
using System.Windows.Forms;
using System.IO;

namespace DataGridTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NewPlayerName_TB.Focus();
        }
        int ID = 1;
        
        public int ID1 { get => ID; set => ID = value; }

        public class Player
        {
            public int PlayerID { get; set; }
            public string PlayerName { get; set; }
            public string PlayerPoint { get; set; }
        }

        private void NewPlayer_BN_Click(object sender, RoutedEventArgs e)
        {
            Player temp = new Player
            {
                PlayerID = ID,
                PlayerName = NewPlayerName_TB.Text,
                PlayerPoint = NewPlayerPoint_TB.Text
            };
            ID++;
            DataGridXAML.Items.Add(temp);
            NewPlayerPoint_TB.Text = "";
            NewPlayerName_TB.Text = "";
            NewPlayerName_TB.Focus();
            
        }



        private void NewPlayerPoint_TB_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NewPlayer_BN_Click(this, e);
            }
        
        }

        private void Save_BN_Click(object sender, RoutedEventArgs e)
        {
            string Path = @"C:\Users\hercs\Desktop\save.txt";
            string myString = "asdElMagad";
            File.AppendAllText(Path, myString); 
            System.Windows.MessageBox.Show("ANYÉD");
            //DataGridXAML.
            
        }
    }
}
