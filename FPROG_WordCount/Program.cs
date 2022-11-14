using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace FPROG_WordCount   
{
    internal class Program
    {


       
        //https://stackoverflow.com/questions/428798/map-and-reduce-in-net

        //create higher order function for mapping 

        //aus reflecting functional power point 

       //MAPPING single values to many
        static IEnumerable<TResult> SelectMany<TSource, TResult>(this<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            var files = new List<TResult>();
            //select many
            return File.ReadLines(source).SelectMany(line => line.Split(delimiters));

            return files;
        }

        //GROUP elements by some key
        static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource,TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {

        }

        //reduce 

       


        //First class function
        Func<int, int> square = x =>
        {
            return x * x;
        };



        static void Main(string[] args)
        {
            //pfad angeben, auslesen
            //dateiendung auslesen
            string dirPath = "help";
            string fileExtension = "help";

            //auslagern dann!
            var files = Directory.EnumerateFiles(dirPath, "*" + fileExtension, SearchOption.AllDirectories);

            //
            var counts = files.MapReduce(Path => File.ReadLines(path).SelectMany(line => line.Split(delimiters)),
                                         word => word,
                                         group => new[] { new KeyValuePair<string, int>(group.key, group.Count()) });

            
           



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