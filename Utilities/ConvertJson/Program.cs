using GlobExpressions;
using Newtonsoft.Json.Linq;
using System.Text;

var currentFolder = Environment.CurrentDirectory;
Console.WriteLine("Creating .restext files for all json files in current folder.");
Console.WriteLine($"Current Folder: {currentFolder}");

string[] matchingFiles = Glob.Files(currentFolder, "**/*.json").ToArray();

foreach (var matchingFile in matchingFiles.Select(mf => Path.GetFullPath(mf, currentFolder)))
{
    var folder = Path.GetDirectoryName(matchingFile);
    var targetFileName = Path.GetFileNameWithoutExtension(matchingFile);
    var targetPath = Path.GetFullPath($"{folder}\\{targetFileName}.restext");

    if (File.Exists(targetPath))
    {
        File.Delete(targetPath);
    }

    var content = File.ReadAllText(matchingFile);
    var jObject = JObject.Parse(content);

    Console.WriteLine($"Parsing {matchingFile} ::");

    using (var outputFile = new StreamWriter(targetPath, false, Encoding.UTF8))
    {
        foreach (var item in jObject.DescendantsAndSelf().OfType<JValue>())
        {
            outputFile.WriteLine($"{item.Path}={item.Value}");
        }
    }

    Console.WriteLine(new string('-', Console.WindowWidth));
}
