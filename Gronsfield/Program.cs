using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите фразу для шифровки:");
            var phrase = Console.ReadLine();
            Console.Write("Введите ключ для шифровки(длинна ключа не больше длины строки):");
            var str_key = Console.ReadLine();
            if (phrase.Length < str_key.Length)
                throw new Exception("Длина ключа больше длины строки.");
            int int_key;
            Gronsfield.ShowTable();
            if (int.TryParse(str_key, out int_key))
            {
                Gronsfield.Key = int_key;
                var _encstr = Gronsfield.GetEncryptedString(phrase);
                Console.WriteLine($"Шифрованная строка: {_encstr}");
                Console.Write("Введите ключ для дешифровки(длинна ключа не больше длины строки):");
                str_key = Console.ReadLine();
                if (phrase.Length < str_key.Length)
                    throw new Exception("Длина ключа больше длины строки.");
                if(int.TryParse(str_key,out int_key))
                {
                    if (int_key != Gronsfield.Key)
                        Console.WriteLine("Этот ключ не подходит.");
                    Gronsfield.Key = int_key;
                    var _decstr = Gronsfield.GetDecryptedString(_encstr);
                    Console.WriteLine($"Дешифровка строки:{_decstr}");
                }
            }

            Console.ReadKey();
        }


       

    }
}
