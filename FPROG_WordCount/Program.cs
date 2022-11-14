using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace FPROG_WordCount   
{
    internal static class Program
    {



        //https://stackoverflow.com/questions/428798/map-and-reduce-in-net

        //create higher order function for mapping 

        //aus reflecting functional power point 

        //https://weblogs.asp.net/dixin/functional-csharp-higher-order-function-currying-and-first-class-function
        //https://www.codeproject.com/Articles/375166/Functional-Programming-in-Csharp#FunctionTypes



        public static IEnumerable<TResult> MapReduce<TSource, TMapped, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TMapped>> map,
            Func<TMapped, TKey> keySelector,
            Func<IGrouping<TKey, TMapped>, IEnumerable<TResult>> reduce)
        {
            return source.SelectMany(map).GroupBy(keySelector).SelectMany(reduce);
        }

        public static Func<string, string, IEnumerable<string>> GetFilesListFromDirectory = (dirPath, fileExtension) =>
         {
             return Directory.EnumerateFiles(dirPath, "*" + fileExtension, SearchOption.AllDirectories);
         };

        public static void printWords(IEnumerable<KeyValuePair<string, int>> wordlist)
        {
            foreach (var c in wordlist.OrderByDescending(x => x.Value))
            {
                Console.Write($"{c.Key} -> {c.Value}____");
            }

        }

        

        
        //DISCLAIMER: muss noch alles überprüft werden ob funktional ! 
        static void Main(string[] args)
        {
            //string dirPath = @"C:\Users\Marlies\source\repos\SunnyTrashpanda\FPROG_WordCount\testfiles";
            //string fileExtension = ".txt";


            if (Environment.GetCommandLineArgs().Length > 2)
            {
                char[] delimiters = Enumerable.Range(0, 256).Select(i => (char)i).Where(c => Char.IsWhiteSpace(c) || Char.IsPunctuation(c)).ToArray();

                string dirPath = Environment.GetCommandLineArgs()[1];
                //TODO wenn keine file extension mitgegeben wird
                string fileExtension = Environment.GetCommandLineArgs()[2];

                var files = GetFilesListFromDirectory(dirPath, fileExtension);

                var counts = MapReduce(files, //source
                                        path => File.ReadLines(path).SelectMany(line => line.Split(delimiters)), //map 
                                        word => word, //key
                                                      //word => RemoveSpecialChars(word), //key
                                        group => new[] { new KeyValuePair<string, int>(group.Key, group.Count()) }); ; //reduce

                printWords(counts);
            }

        }


        //NOT NEEDED
        // kopiert von stack overflow --> ' and "
        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex(
                          "(?:[^a-zA-Z0-9 ]|(?<=['\"])s)",
                          RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }

        //braucht man eigentlich nicht, durch die delimiters schon erledigt 
        public static Func<string, string> RemoveSpecialChars = (input) =>
        {
            Regex r = new Regex(
                          "(?:[^a-zA-Z0-9 ]|(?<=['\"])s)",
                          RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        };


    }
}