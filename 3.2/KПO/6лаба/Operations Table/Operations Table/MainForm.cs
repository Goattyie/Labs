using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Operations_Table
{
    public partial class MainForm : Form
    {
        Table table = null;
        string[] operations = { "head", "body", "title", "div", "a", "span", "form", "h1", "html", "section" };


        public MainForm()
        {
            InitializeComponent();
            OperationsList.SelectedText = "";
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            if (OpenSource.ShowDialog() == DialogResult.OK)
            {
                FilePath.Text = OpenSource.FileName;

                char plus = '+';

                table = new Table();
                OperationsList.Items.Clear();
                LinesList.Items.Clear();

                StreamReader reader = new StreamReader(OpenSource.FileName);
                string line;
                int lineNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    ++lineNumber;
                    for (int i = 0; i < operations.Length; ++i)
                    {
                        MatchCollection collection = Regex.Matches(line, Regex.Escape(operations[i]));
                        if (collection.Count > 0)
                        {
                            table.AddKey(operations[i]);
                            for (int j = 0; j < collection.Count; ++j)
                            {
                                bool isSingleOperator = operations[i].Length == 1;
                                bool left = collection[j].Index > 0 && (line[collection[j].Index - 1] == operations[i][0]);
                                bool right = collection[j].Index < line.Length - 1 && ((line[collection[j].Index + 1] == operations[i][0]) || (line[collection[j].Index + 1] == '='));
                                bool isChar = (collection[j].Index < line.Length - 1 && line[collection[j].Index + 1] == '\'') && (collection[j].Index > 0 && line[collection[j].Index - 1] == '\'');
                                //проверка единичного оператора или символа
                                if (!(isSingleOperator && (left || right || isChar)))
                                {
                                    //проверка того, что оператор в строке
                                    left = false;
                                    int count = 0;
                                    int p = collection[j].Index;
                                    while (p > 0 && !left)
                                    {
                                        if (line[--p] == '\"')
                                        {
                                            ++count;
                                        }
                                        if (p == 0 && count % 2 != 0)
                                        {
                                            left = true;
                                        }
                                    }
                                    right = false;
                                    count = 0;
                                    p = collection[j].Index;
                                    while (p < line.Length - 1 && !right)
                                    {
                                        if (line[++p] == '\"')
                                        {
                                            ++count;
                                        }
                                        if (p == line.Length - 1 && count % 2 != 0)
                                        {
                                            right = true;
                                        }
                                    }
                                    if (!left && !right)
                                    {
                                        table.AddAttribute(operations[i], new Position(lineNumber, collection[j].Index + 1));
                                    }
                                }
                            }
                        }
                    }
                }

                reader.Close();

                table.Reorganize();
                string[] selectedOperations = table.GetKeys().ToArray();
                OperationsList.Items.Add("");
                OperationsList.Items.AddRange(selectedOperations);
                OperationsList.SelectedIndex = 0;
            }
        }

        private void OperationsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LinesList.Items.Clear();
            if (OperationsList.SelectedIndex > -1)
            {
                if (OperationsList.SelectedItem.ToString().Equals(""))
                {
                    LinesList.Items.Clear();
                    for (int i = 0; i < operations.Length; ++i)
                    {
                        string key = operations[i];
                        string item = (key.Length < 2 ? " " + key : key) + " : ";
                        List<Position> attributes = table.GetAttributes(key);
                        if (attributes != null)
                        {
                            item += string.Join(", ", attributes);
                        }
                        LinesList.Items.Add(item);
                    }
                }
                else
                {
                    List<Position> positions = table.GetAttributes(OperationsList.SelectedItem.ToString());
                    foreach (Position position in positions)
                    {
                        LinesList.Items.Add("[" + position.Line + ", " + position.Column + "]");
                    }
                }
            }
        }
    }
}
