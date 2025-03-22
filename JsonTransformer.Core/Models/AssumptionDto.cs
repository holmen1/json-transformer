namespace JsonTransformer.Core.Models;

public class AssumptionDto
{
    public string Level1 { get; set; }
    public string Level2 { get; set; }
    public string Level3 { get; set; }
    public double Value { get; set; }
}

public class AssumptionsModelDto
{
    public List<AssumptionDto> Assumptions { get; set; }
}