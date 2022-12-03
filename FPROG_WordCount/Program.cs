using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace FPROG_WordCount   
{
    internal static class Program
    {

        //TODO
        //tests
        //zeitmessung
        //lessons learned (LINQ is beste)
        //

        //lambda with 2 arguments
        public static Func<string, string, IEnumerable<string>> GetFilesListFromDirectory = (dirPath, fileExtension) =>
         {  
             //maybe side-effects
             return Directory.EnumerateFiles(dirPath, "*" + fileExtension, SearchOption.AllDirectories);
         };

        //lambda with 2 arguments
        public static Func<int,int,Tuple<string, string>> GetPathAndFile = (pathIndex,fileIndex) => 
        {
            Tuple<string, string> pathAndFile = new Tuple<string, string>(Environment.GetCommandLineArgs()[pathIndex], Environment.GetCommandLineArgs()[fileIndex]);
            return pathAndFile;
        };

        //lambda without arguments
        public static Func<char[]> GetDelimiters = () =>
        {
            return Enumerable.Range(0, 256).Select(i => (char)i).Where(c => Char.IsWhiteSpace(c) || Char.IsPunctuation(c) ||Char.IsSymbol(c)).ToArray();
        };

        //High Order Function 
        //has multiple functions as arguments
        public static IEnumerable<TResult> MapReduce<TSource, TMapped, TKey, TResult>(
         this IEnumerable<TSource> source,
         Func<TSource, IEnumerable<TMapped>> map,
         Func<TMapped, TKey> keySelector,
         Func<IGrouping<TKey, TMapped>, IEnumerable<TResult>> reduce)
        {
            return source.SelectMany(map).GroupBy(keySelector).SelectMany(reduce);
        }

        // NON-FUNCTIONAL METHOD
        public static void printWords(IEnumerable<KeyValuePair<string, int>> wordlist)
        {
            foreach (var c in wordlist)
            {
                Console.WriteLine($"{c.Key}  {c.Value}");
            }
        }

        static void Main(string[] args)
        {
            if (Environment.GetCommandLineArgs().Length > 2)
            {
                var pathIndexFromProgrammArgs = 1;
                var fileIndexFromProgrammArgs = 2;

                var pathAndFile = GetPathAndFile(pathIndexFromProgrammArgs, fileIndexFromProgrammArgs);

                var files = GetFilesListFromDirectory(pathAndFile.Item1, pathAndFile.Item2);

                var delimiters = GetDelimiters();

                var counts = MapReduce(files, //source
                                        path => File.ReadLines(path).SelectMany(line => line.Split(delimiters)).Where(line => !string.IsNullOrWhiteSpace(line)), //map  
                                        word => word, //key 
                                        group => new[] { new KeyValuePair<string, int>(group.Key, group.Count()) });  //reduce

                var orderedCounts = counts.OrderByDescending(x => x.Value).ThenByDescending(x => x.Key);

                //non functional part 
                printWords(orderedCounts);
            }
        }
    }
}