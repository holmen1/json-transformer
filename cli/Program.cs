using json_transformer.Mappers;
using json_transformer.Models;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a path to JSON file");
            return;
        }

        string jsonFilePath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "output.json";
        
        string jsonContent = File.ReadAllText(jsonFilePath);
        var dto = JsonSerializer.Deserialize<AssumptionsModelDto>(jsonContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); // Gotcha: PropertyNameCaseInsensitive
        
        var domainModel = AssumptionMapper.ToAssumptionsModel(dto);
        
        var dictionary = AssumptionMapper.ToAssumptionsDictionary(domainModel);
        
        string outputJson = JsonSerializer.Serialize(dictionary, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        
        File.WriteAllText(outputPath, outputJson);
        
        Console.WriteLine($"Processed assumptions and wrote dictionary to {outputPath}");
    }
}