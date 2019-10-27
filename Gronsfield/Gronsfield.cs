using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class Gronsfield
    {
        private static char[,] GronsfeldTable;
        public static int Key { get; set; }
        private static void InitTable()
        {
            char Pointer = (char)1072;
            GronsfeldTable = new char[9, 32];
            for (int i = 0; i <= GronsfeldTable.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= GronsfeldTable.GetUpperBound(1); j++)
                {
                    GronsfeldTable[i, j] = (char)(Pointer + ((i + j) % 32));
                }
            }
        }

        public static List<int> KeyVector()
        {
            List<int> _keyVector = new List<int>();
            int tmp_key = Key;
            while (tmp_key > 0)
            {
                int digit = tmp_key % 10;
                tmp_key /= 10;
                _keyVector.Add(digit);
            }
            return _keyVector;
        }

        public static List<int> FindSpaces(string str)
        {
            List<int> spaces_idx = new List<int>();
            var idx = 0;
            while (idx < str.Length)
            {
                idx = str.IndexOf(' ');
                spaces_idx.Add(idx);
                str = str.Remove(idx, 1);
            }

            return spaces_idx;
        }
        private static int[] InitExtendetKey(string str, string [] split_str)
        {
            Console.WriteLine("Расширение ключа");
            var _keyVector = KeyVector();
            _keyVector.Reverse();
            int[] extended_key = new int[str.Length - split_str.GetUpperBound(0)];
            for (int i = 0; i < extended_key.Length; i++)
            {
                extended_key[i] = _keyVector[i % _keyVector.Count];
                Console.Write($"{extended_key[i]}");
            }
            Console.Write("\n");
            return extended_key;

        }
        private static List<int> GetUpperIndexes(string str)
        {
            List<int> _indexes = new List<int>();
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.ToUpper(str[i]) == str[i])
                    _indexes.Add(i);
            }
            return _indexes;
        }
        public static string GetEncryptedString(string str)
        {
            var _indexes = GetUpperIndexes(str);
            str = str.ToLower();
            var split_str = str.Split(' ');
            var extended_key = InitExtendetKey(str, split_str);
            string _encstr = null;
            Console.WriteLine("Замена по таблице гронсфельда:");
            for (int i = 0; i < split_str.Length; i++)
            {

                for (int j = 0; j < split_str[i].Length; j++)
                {
                    var idx = FindIdxInTable(i, j, split_str);
                    _encstr += GronsfeldTable[extended_key[i + j], idx];
                    Console.Write($"[{split_str[i][j]}->{_encstr[i + j]}]");
                }
                _encstr += " ";
            }
            Console.Write("\n");
            _encstr = _encstr.TrimEnd(' ');
            _encstr = ReturnToUpper(_encstr, _indexes);

            return _encstr;
        }
        private static string ReturnToUpper(string str, List<int> _indexes)
        {
            var strChar = str.ToCharArray();
            foreach (var el in _indexes)
            {
                strChar[el] = Char.ToUpper(strChar[el]);
            }
            return new string(strChar);
        }
        private static int FindIdxInTable(int i, int j, string[] split_str, int[] extended_key)
        {
            int idx = 0;
            while (idx <= GronsfeldTable.GetUpperBound(1))
            {
                if (split_str[i][j] == GronsfeldTable[extended_key[i + j], idx])
                    break;
                else idx++;
            }
            return idx;
        }
        private static int FindIdxInTable(int i, int j,string [] split_str)
        {
            int idx = 0;
            while (idx <= GronsfeldTable.GetUpperBound(1))
            {
                if (split_str[i][j] == GronsfeldTable[0, idx])
                    break;
                else idx++;
            }
            return idx;
        }
        public static string GetDecryptedString(string _encstr)
        {
            var _indexes = GetUpperIndexes(_encstr);
            _encstr = _encstr.ToLower();
            string _decstr = null;
            var split_str = _encstr.Split(' ');
            var extended_key = InitExtendetKey(_encstr, split_str);

            Console.WriteLine("Обратная замена по таблице гронсфельда");
            for (int i = 0; i < split_str.Length; i++)
            {
                for (int j = 0; j < split_str[i].Length; j++)
                {
                    var idx = FindIdxInTable(i, j, split_str,extended_key);
                    _decstr += GronsfeldTable[0, idx];
                    Console.Write($"[{split_str[i][j]}->{_decstr[i + j]}]");
                }
                _decstr += " ";
            }
            Console.Write("\n");
            _decstr = _decstr.TrimEnd(' ');
            return ReturnToUpper(_decstr, _indexes);
        }
        public static void ShowTable()
        {
            string output = null;
            Console.WriteLine("Таблица Гронсфельда:");
            Console.WriteLine("===============================================================");
            for (int i = 0; i <= GronsfeldTable.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= GronsfeldTable.GetUpperBound(1); j++)
                {
                    output += GronsfeldTable[i, j]+ " ";
                }
                Console.WriteLine(output);
                output = null;
            }
            Console.WriteLine("===============================================================");
        }
        static Gronsfield()
        {
            InitTable();
        }
    }
}
