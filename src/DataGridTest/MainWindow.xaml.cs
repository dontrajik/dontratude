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

        private void Save_BN_Click(object sender, RoutedEventArgs e)
        {
            string userDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = "DontraTudeSavingFile.txt";
            string Path = string.Format(@"{0}\\{1}", userDesktop, fileName);

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
            Player playerData = (Player)XAMLDataGrid.SelectedValue;
            XAMLDataGrid.Items.Remove(playerData);
        }

        private void Btn_savexls_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Range range;
            Range myrange;
            for (int i = 0; i < XAMLDataGrid.Columns.Count; i++)
            {
                range = (Range)sheet1.Cells[1, i + 1];
                sheet1.Cells[1, 1 + 1].Font.Bold = true;
                range.Value = XAMLDataGrid.Columns[i].Header;

                for (int j = 0; j < XAMLDataGrid.Items.Count; j++)
                {
                    TextBlock b = XAMLDataGrid.Columns[i].GetCellContent(XAMLDataGrid.Items[j]) as TextBlock;
                    myrange = sheet1.Cells[j + 2, i + 1];
                    myrange.Value = b.Text;
                }
            }

            workbook.SaveAs("DataGridTest");
            workbook.Close();
        }
    }
}
