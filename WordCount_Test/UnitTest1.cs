using FPROG_WordCount;
using static System.Net.Mime.MediaTypeNames;

namespace WordCount_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void SmallTestFile_test1()
        {
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.Parent.Parent.FullName;

            string path = projectDirectory + "\\unittest_files\\test1";
            string fileExtension = ".txt";

            var files = Program.GetFilesListFromDirectory(path, fileExtension);

            var delimiters = Program.GetDelimiters();

            var counts = Program.MapReduce(files,
                                    path => File.ReadLines(path).SelectMany(line => line.Split(delimiters)).Where(line => !string.IsNullOrWhiteSpace(line)),  
                                    word => word, 
                                    group => new[] { new KeyValuePair<string, int>(group.Key, group.Count()) }); 

            var orderedCounts = counts.OrderByDescending(x => x.Value).ThenByDescending(x => x.Key);

            IEnumerable<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>> { 
                new KeyValuePair<string, int> ("hallo",3),
                new KeyValuePair<string, int> ("hop",2),
                new KeyValuePair<string, int> ("hi",1),
                new KeyValuePair<string, int> ("help",1),
            };

            Assert.That(result, Is.EqualTo(orderedCounts));
            
        }

        [Test]
        public void SmallTestFile_Numbers_test2()
        {
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.Parent.Parent.FullName;

            string path = projectDirectory + "\\unittest_files\\test2";
            string fileExtension = ".txt";

            var files = Program.GetFilesListFromDirectory(path, fileExtension);

            var delimiters = Program.GetDelimiters();

            var counts = Program.MapReduce(files,
                                    path => File.ReadLines(path).SelectMany(line => line.Split(delimiters)).Where(line => !string.IsNullOrWhiteSpace(line)),
                                    word => word,
                                    group => new[] { new KeyValuePair<string, int>(group.Key, group.Count()) });

            var orderedCounts = counts.OrderByDescending(x => x.Value).ThenByDescending(x => x.Key);

            IEnumerable<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>> {
                new KeyValuePair<string, int> ("1",7),
            };

            Assert.That(result, Is.EqualTo(orderedCounts));

        }

        [Test]
        public void SmallTestFile_OrderDescending_test3()
        {
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.Parent.Parent.FullName;

            string path = projectDirectory + "\\unittest_files\\test3";
            string fileExtension = ".txt";

            var files = Program.GetFilesListFromDirectory(path, fileExtension);

            var delimiters = Program.GetDelimiters();

            var counts = Program.MapReduce(files,
                                    path => File.ReadLines(path).SelectMany(line => line.Split(delimiters)).Where(line => !string.IsNullOrWhiteSpace(line)),
                                    word => word,
                                    group => new[] { new KeyValuePair<string, int>(group.Key, group.Count()) });

            var orderedCounts = counts.OrderByDescending(x => x.Value).ThenByDescending(x => x.Key);

            IEnumerable<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>> {
                new KeyValuePair<string, int> ("aa",2),
                new KeyValuePair<string, int> ("dd",1),
                new KeyValuePair<string, int> ("cc",1),
                new KeyValuePair<string, int> ("bee",1),
            };

            Assert.That(result, Is.EqualTo(orderedCounts));

        }
    }
}