namespace TestProjectLibrary.Db.Entities;

public class Point
{
    // can be sorted by date minimum 
    public Guid Id { get; set; }
    
    public string Origin { get; set; }
    
    public string Destination { get; set; }
    
    public DateTime Date {get; set; }
    
    public Guid RouteId { get; set; }
}