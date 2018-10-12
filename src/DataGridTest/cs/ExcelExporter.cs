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
    class ExcelExporter
    {
        public static void SaveAs(DataGrid XAMLDataGrid)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string saveFilePath = string.Empty;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                saveFilePath = saveFileDialog.FileName;
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
                workbook.SaveAs(saveFilePath);
                workbook.Close();
            }
            /*
            else if(saveFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            */
            System.Windows.Forms.MessageBox.Show("Mentve!");
            XAMLDataGrid.Focus();
        }

        public static void LoadFromExcel(DataGrid XAMLDataGrid)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string LoadedFile = ofd.FileName;
            
            Workbook workbook = excel.Workbooks.Open(LoadedFile);
            Worksheet sheet = (Worksheet)workbook.Sheets[1];

            
            int db = 0;
            while (sheet.Cells[db + 2, 1].value != null)
            {
                db++;
            }
            for (int i = 0; i < db; i++)
            {
                Player temp = new Player
                {
                    PlayerID = (int)sheet.Cells[i + 2, 1].value,
                    PlayerName = (string)sheet.Cells[i+2,2].value,
                    PlayerPoint = (int)sheet.Cells[i+2,3].value
                };
                XAMLDataGrid.Items.Add(temp);
            }
            excel.Application.Quit();
            System.Windows.Forms.MessageBox.Show("Adatok Betöltve!");
        }
    }
}
