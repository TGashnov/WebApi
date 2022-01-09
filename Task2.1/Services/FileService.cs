using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2._1.Services
{
    public class FileService
    {
        public string FilePath { get; }

        public FileService()
        {
            FilePath = Path.Combine(Directory.GetCurrentDirectory(), "text.txt");
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
            }
        }

        public string GetAllText()
        {
            return File.ReadAllText(FilePath);
        }

        public string GetTextFromLine(int row)
        {
            InputValidation("Неверно введен номер строки", row);
            return File.ReadLines(FilePath).ElementAtOrDefault(row - 1);
        }

        public string GetTextRange(int start, int end)
        {
            InputValidation("Неверно введены номера строк", start, end);
            var sb = new StringBuilder();
            var text = File.ReadLines(FilePath).ToList();
            for (int i = start; i <= end; i++)
            {
                sb.Append(text[i] + "\n");
            }
            return sb.ToString();
        }

        public int PostLine(string inputText, bool force)
        {
            void AppendLine() => File.AppendAllText(FilePath, inputText + "\n");

            if (force == true)
            {
                AppendLine();
            }
            else
            {
                var text = File.ReadLines(FilePath).ToList();
                if (text.Contains(inputText))
                {
                    return text.FindIndex(str => str == inputText) + 1;
                }
                else
                {
                    AppendLine();
                }
            }
            return 0;
        }

        public void PutLine(string inputText, int row)
        {
            InputValidation("Неверно введен номер строки", row);
            var text = File.ReadLines(FilePath).ToList();
            text[row - 1] = inputText;
            File.WriteAllLines(FilePath, text);

        }

        public void DeleteLine(int row)
        {
            InputValidation("Неверно введен номер строки", row);
            var text = File.ReadAllLines(FilePath).ToList();
            text.RemoveAt(row - 1);
            File.WriteAllLines(FilePath, text);

        }

        public void InputValidation(string mes, int row)
        {
            if (row < 1 || row > File.ReadAllLines(FilePath).Length)
            {
                throw new ArgumentOutOfRangeException(mes);
            }
        }

        public void InputValidation(string mes, int start, int end)
        {
            if (start < 1 || end > File.ReadAllLines(FilePath).Length)
            {
                throw new ArgumentOutOfRangeException(mes);
            }
        }
    }
}
