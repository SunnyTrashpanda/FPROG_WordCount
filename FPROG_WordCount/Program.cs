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
            foreach (var c in wordlist)
            {
                Console.WriteLine($"{c.Key}  {c.Value}");
            }

        }

        public static Func<Tuple<string, string>> GetPathAndFile = () =>
        {
            Tuple<string, string> pathAndFile = new Tuple<string, string>(Environment.GetCommandLineArgs()[1], Environment.GetCommandLineArgs()[2]);
            return pathAndFile;
        };

        public static Func<char[]> GetDelimiters = () =>
        {
            return Enumerable.Range(0, 256).Select(i => (char)i).Where(c => Char.IsWhiteSpace(c) || Char.IsPunctuation(c)).ToArray();
        };

        
        //empty entrys ignorieren 

        //magic numbers 


        //DISCLAIMER: muss noch alles überprüft werden ob funktional ! 
        static void Main(string[] args)
        {

            if (Environment.GetCommandLineArgs().Length > 2)
            {

                var pathAndFile = GetPathAndFile();

                var files = GetFilesListFromDirectory(pathAndFile.Item1, pathAndFile.Item2);

                var delimiters = GetDelimiters();

                var counts = MapReduce(files, //source
                                        path => File.ReadLines(path).SelectMany(line => line.Split(delimiters)), //map 
                                        word => word, //key
                                        group => new[] { new KeyValuePair<string, int>(group.Key, group.Count()) }); ; //reduce

                var orderedCounts = counts.OrderByDescending(x => x.Value);

                printWords(orderedCounts);
            }

        }
    }
}