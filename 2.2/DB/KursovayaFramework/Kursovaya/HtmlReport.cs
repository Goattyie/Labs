using Npgsql;
using System.IO;

namespace Kursovaya
{
    class HtmlReport
    {
        static string Folder = "html/";
        string FileName { get; set; }
        string[] Columns { get; set; }
        public HtmlReport(string filename, string[] columns)
        {
            FileName = filename;
            Columns = columns;
        }
        public void Parse(string query)
        {
            using(StreamWriter sw = new StreamWriter(Folder + FileName))
            {
                sw.Write("<style>table{margin: auto;}</style><table bordercolor=\"Black\"><tr>");
                foreach (string colname in Columns)
                    sw.Write($"<td>{colname}</td>");
                sw.Write("</tr>");
                using (NpgsqlConnection connect = SQL.GetConnection())
                {
                    try
                    {
                        connect.Open();
                        NpgsqlCommand command = new NpgsqlCommand(query, connect);
                        NpgsqlDataReader reader = command.ExecuteReader();
                        int i = Columns.Length;
                        while (reader.Read())
                        {
                            sw.Write("<tr>");
                            for(int index = 0; index < Columns.Length; index++)
                                sw.Write($"<td>{reader.GetValue(index)}</td>");
                            sw.Write("</tr>"); 
                        }
                        sw.Write("</tr>");
                        connect.Close();
                    }
                    catch { }
                }
                sw.Write("</table>");
            }
        }
    }
}
