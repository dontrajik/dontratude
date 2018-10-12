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
using Microsoft.Office.Interop.Excel;
using MessageBox = System.Windows.MessageBox;

/*
TODO:
Save as... button for XLSX
Save as... button for txt
Load data from XLSX
Load data from txt

*/


namespace DataGridTest
{
    public partial class MainWindow : System.Windows.Window
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
            if (NewPlayerPoint_TB.Text != string.Empty && NewPlayerName_TB.Text != string.Empty)
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
            else
                MessageBox.Show("Nem adtál meg játékosnevet és pontszámot!");
        }

        private void NewPlayerPoint_TB_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NewPlayer_BN_Click(this, e);
            }
        }
        
        private void Btn_click_deleteData(object sender, RoutedEventArgs e)
        {
            XAMLDataGrid.Items.Clear();
        }

        private void Btn_click_deletePlayer(object sender, RoutedEventArgs e)
        {
            Player playerData = (Player)XAMLDataGrid.SelectedValue;
            XAMLDataGrid.Items.Remove(playerData);
        }

        private void Btn_savexls_Click(object sender, RoutedEventArgs e)
        {
            ExcelExporter.SaveAs(XAMLDataGrid);
        }

        private void btn_loadxls_Click(object sender, RoutedEventArgs e)
        {
            ExcelExporter.LoadFromExcel(XAMLDataGrid);
        }
    }
}
