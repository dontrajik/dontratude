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
using DataGrid = System.Windows.Controls.DataGrid;

namespace DataGridTest
{
    class TxtExporter
    {
        public static void saveToDesktop(DataGrid XAMLDataGrid)
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
    }
}
