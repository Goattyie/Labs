using System.Collections.Generic;

namespace Operations_Table
{
    class Table
    {
        private Dictionary<string, List<Position>> table;

        public Table()
        {
            table = new Dictionary<string, List<Position>>();
        }

        public bool AddKey(string key)
        {
            if (!table.ContainsKey(key))
            {
                table.Add(key, new List<Position>());
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddAttribute(string key, Position attribute)
        {
            if (table.ContainsKey(key))
            {
                if (table[key].Find(value => value.Line == attribute.Line && value.Column == attribute.Column) == null)
                {
                    table[key].Add(attribute);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public List<string> GetKeys()
        {
            return new List<string>(table.Keys);
        }

        public List<Position> GetAttributes(string key)
        {
            if (table.ContainsKey(key))
            {
                return table[key];
            }
            else
            {
                return null;
            }
        }

        public void Reorganize()
        {
            List<string> keys = new List<string>(table.Keys);
            keys = new List<string>(table.Keys);
            foreach (string key in keys)
            {
                if (table[key].Count <= 0)
                {
                    table.Remove(key);
                }
            }
        }
    }

    class Position
    {
        public int Line;
        public int Column;

        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return "[" + Line + ", " + Column + "]";
        }
    }
}
