using json_transformer.Models;

namespace json_transformer.Mappers;

public static class AssumptionMapper
{
    public static AssumptionsModel ToAssumptionsModel(AssumptionsModelDto dto)
    {
        return new AssumptionsModel
        {
            Assumptions = dto.Assumptions.Select(a => new Assumption
            {
                Level1 = Enum.Parse<Level1Type>(a.Level1),
                Level2 = Enum.Parse<Level2Type>(a.Level2),
                Level3 = Enum.Parse<Level3Type>(a.Level3, true),
                Value = a.Value
            }).ToList()
        };
    }

    // Optional reverse mapping method
    public static AssumptionsModelDto ToAssumptionsModelDto(AssumptionsModel model)
    {
        return new AssumptionsModelDto
        {
            Assumptions = model.Assumptions.Select(a => new AssumptionDto
            {
                Level1 = a.Level1.ToString(),
                Level2 = a.Level2.ToString(),
                Level3 = a.Level3.ToString(),
                Value = a.Value
            }).ToList()
        };
    }
    
    public static Dictionary<Level1Type, Dictionary<Level2Type, Dictionary<Level3Type, double>>> ToAssumptionsDictionary(AssumptionsModel model)
    {
        var result = new Dictionary<Level1Type, Dictionary<Level2Type, Dictionary<Level3Type, double>>>();
    
        foreach (var assumption in model.Assumptions)
        {
            // Ensure the Level1 dictionary exists
            if (!result.ContainsKey(assumption.Level1))
            {
                result[assumption.Level1] = new Dictionary<Level2Type, Dictionary<Level3Type, double>>();
            }
        
            // Ensure the Level2 dictionary exists
            if (!result[assumption.Level1].ContainsKey(assumption.Level2))
            {
                result[assumption.Level1][assumption.Level2] = new Dictionary<Level3Type, double>();
            }
        
            // Set the value at Level3
            result[assumption.Level1][assumption.Level2][assumption.Level3] = assumption.Value;
        }
    
        return result;
    }
}