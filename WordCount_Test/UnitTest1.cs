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
        public void SmallTestFile()
        {
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.Parent.Parent.FullName;

            string path = projectDirectory + "\\testfiles\\test";
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
    }
}