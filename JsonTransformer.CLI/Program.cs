using System.Text.Json;
using JsonTransformer.Core.Mappers;
using JsonTransformer.Core.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a path to JSON file");
            return;
        }

        var jsonFilePath = args[0];
        var outputPath = args.Length > 1 ? args[1] : "output.json";

        var jsonContent = File.ReadAllText(jsonFilePath);
        var dto = JsonSerializer.Deserialize<AssumptionsModelDto>(jsonContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); // Gotcha: PropertyNameCaseInsensitive

        var domainModel = AssumptionMapper.ToAssumptionsModel(dto);

        var dictionary = AssumptionMapper.ToAssumptionsDictionary(domainModel);

        var outputJson = JsonSerializer.Serialize(dictionary, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(outputPath, outputJson);

        Console.WriteLine($"Processed assumptions and wrote dictionary to {outputPath}");
    }
}