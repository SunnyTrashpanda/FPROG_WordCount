using System;
using System.Text.RegularExpressions;

namespace FPROG_WordCount   
{
    internal class Program
    {


        static void Main(string[] args)
        {
            //pfad angeben, auslesen
            //dateiendung auslesen
            string dirPath = "help";
            string fileExtension = "help";

            //auslagern dann!
            var files = Directory.EnumerateFiles(dirPath, "*" + fileExtension, SearchOption.AllDirectories);
            IEnumerable<string>  fileList = Directory.EnumerateFiles(dirPath, "*" + fileExtension, SearchOption.AllDirectories);

            Console.WriteLine("Sewas!");



        }


        // kopiert von stack overflow --> ' and "
        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex(
                          "(?:[^a-zA-Z0-9 ]|(?<=['\"])s)",
                          RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }
    }
}