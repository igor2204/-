using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string path = "C:\\Users\\igorc\\OneDrive\\Рабочий стол\\Новый текстовый документ.txt";
        string inputCode = File.ReadAllText(path);

        // Удаление лишних комментариев
        string codenotcoment = Regex.Replace(inputCode, @"(//.*?$)|(/\*.*?\*/)", string.Empty, RegexOptions.Multiline);
        
        // Замена имени класса 
        inputCode = Regex.Replace(inputCode, @"(?<=\bclass\s+)\w+(?=\s*\{)", "New_name_of_class");

        // Замена имени файла
        string newFileName = "obfuskator.cs";
        inputCode = Regex.Replace(inputCode, @"(?<=\bpublic\s+class\s+)\w+(?=\s*\{)", newFileName);

        // Замена имени конструктора
        inputCode = Regex.Replace(inputCode, @"public\s\w+\s*\(", "public NewConstructorName(");

        // Удаление лишних пробелов и символов перехода на новую строку
        inputCode = Regex.Replace(codenotcoment, @"\s+", " ");

        // Нахождение всех идентификаторов
        var identifiers = Regex.Matches(inputCode, @"\b\w+\b")
                               .OfType<Match>()
                               .Select(m => m.Value)
                               .Distinct()
                               .ToArray();

        // Замена идентификаторов на односимвольные или двухсимвольные имена 
        for (int i = 0; i < identifiers.Length; i++)
        {
            string replacement = i < 26 ? ((char)('a' + i)).ToString() : "var" + (i - 26 + 1);
            inputCode = Regex.Replace(inputCode, $@"\b{identifiers[i]}\b", replacement);
        }
        Console.WriteLine("Измененный код:");
        Console.WriteLine(inputCode);
    }
}