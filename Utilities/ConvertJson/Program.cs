using GlobExpressions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Text;

Console.WriteLine("Processing Json Language Resource files");

var config = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional:true, reloadOnChange: true)
       .Build();

var currentFolder = Environment.CurrentDirectory;
Console.WriteLine("Creating .restext files for all json files matching pattern.");
Console.WriteLine($"Current Folder: {currentFolder}");

string[] matchingFiles = Glob.Files(currentFolder, "**/Languages/*.json").ToArray();

var lineWidth = 40;
try
{
    lineWidth = Console.WindowWidth;
}
catch
{
    // fallback is 40, above;
}

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

    Console.WriteLine($"Parsing {Path.GetFileName(matchingFile)} -> {Path.GetFileName(targetPath)}");

    using var outputFile = new StreamWriter(targetPath, false, Encoding.UTF8);

    foreach (var item in jObject.DescendantsAndSelf().OfType<JValue>())
    {
        outputFile.WriteLine($"{item.Path}={item.Value}");
    }
}
