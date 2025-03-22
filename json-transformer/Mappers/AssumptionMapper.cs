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
}