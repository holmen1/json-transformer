namespace JsonTransformer.Core.Models;

public enum Level1Type { A, B }
public enum Level2Type { HR, LR }
public enum Level3Type { Investment, Cash }
    
public class Assumption
{
    public Level1Type Level1 { get; set; }
    public Level2Type Level2 { get; set; }
    public Level3Type Level3 { get; set; }
    public double Value { get; set; }
}
    
public class AssumptionsModel
{
    public List<Assumption> Assumptions { get; set; }
}