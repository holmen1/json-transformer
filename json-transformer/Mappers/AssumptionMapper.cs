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
    
    // Generic method to convert a list of objects to a 3 level nested dictionary
    public static Dictionary<TKey1, Dictionary<TKey2, Dictionary<TKey3, TValue>>> ToNestedDictionary<TKey1, TKey2, TKey3, TValue, TSource>(
        IEnumerable<TSource> source,
        Func<TSource, TKey1> key1Selector,
        Func<TSource, TKey2> key2Selector,
        Func<TSource, TKey3> key3Selector,
        Func<TSource, TValue> valueSelector)
    {
        var result = new Dictionary<TKey1, Dictionary<TKey2, Dictionary<TKey3, TValue>>>();

        foreach (var item in source)
        {
            var key1 = key1Selector(item);
            var key2 = key2Selector(item);
            var key3 = key3Selector(item);
            var value = valueSelector(item);

            if (!result.ContainsKey(key1))
            {
                result[key1] = new Dictionary<TKey2, Dictionary<TKey3, TValue>>();
            }

            if (!result[key1].ContainsKey(key2))
            {
                result[key1][key2] = new Dictionary<TKey3, TValue>();
            }

            result[key1][key2][key3] = value;
        }

        return result;
    }
    
    public static Dictionary<Level1Type, Dictionary<Level2Type, Dictionary<Level3Type, double>>> ToAssumptionsDictionary(AssumptionsModel model)
    {
        return ToNestedDictionary<Level1Type, Level2Type, Level3Type, double, Assumption>(
            model.Assumptions,
            a => a.Level1,
            a => a.Level2,
            a => a.Level3,
            a => a.Value);
    }
}