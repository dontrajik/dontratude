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
using MessageBox = System.Windows.MessageBox;

namespace DataGridTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NewPlayerName_TB.Focus();
        }
        int ID = 1;
        public int ID1 { get => ID; set => ID = value; }

        private void NewPlayer_BN_Click(object sender, RoutedEventArgs e)
        {
            Player temp = new Player
            {
                PlayerID = ID,
                PlayerName = NewPlayerName_TB.Text,
                PlayerPoint = int.Parse(NewPlayerPoint_TB.Text)
            };

            ID++;
            XAMLDataGrid.Items.Add(temp);
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
            string Path = @"C:\Users\molnar.mark\Desktop\datagridTest.txt";
            File.WriteAllText(Path, string.Empty);
            foreach (var item in XAMLDataGrid.Items)
            {
                Player player = (Player)item;
                string playerInfo = string.Format("{0} {1} {2} \r\n", player.PlayerID, player.PlayerName, player.PlayerPoint);
                File.AppendAllText(Path, playerInfo);
            }
            if (XAMLDataGrid.Items.Count == 0)
                MessageBox.Show("Üres dokumentumot nemlehet menteni! \r\nElőször adj hozzá Játékosokat!");
            else
                MessageBox.Show("Dokumentum mentve!");
        }

        private void Btn_click_deleteData(object sender, RoutedEventArgs e)
        {
            XAMLDataGrid.Items.Clear();
        }

        private void Btn_click_deletePlayer(object sender, RoutedEventArgs e)
        {
            //XAMLDataGrid.CurrentCell.Item.ToString();
        }
    }
}
