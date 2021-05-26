using Npgsql;
using System;
using System.IO;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace Kursovaya
{
    class ExcelReport
    {
        static string Folder = "excel";
        string FileName { get; set; }
        string[] Columns { get; set; }
        public ExcelReport(string filename, string[] columns)
        {
            FileName = filename;
            Columns = columns;
        }
        public void Parse(string query)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workBook = excelApp.Workbooks.Add();
                Excel.Worksheet workSheet = workBook.ActiveSheet;
                using (NpgsqlConnection connect = SQL.GetConnection())
                {
                    for (int i = 0; i < Columns.Length; i++)
                        workSheet.Cells[1, 1 + i] = Columns[i];
                    connect.Open();
                    NpgsqlCommand command = new NpgsqlCommand(query, connect);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    int row = 2;
                    while (reader.Read())
                    {
                        for(int i = 0; i < Columns.Length; i++)
                            workSheet.Cells[row, 1 + i] = reader.GetValue(i);
                        row++;
                    }
                    connect.Close();
                }
                workBook.Close(true, Directory.GetCurrentDirectory() + "\\" + Folder + "\\" + FileName);
                excelApp.Quit();
            }catch(Exception ex) { Message.Warning(ex.Message); }
        }
    }
}
