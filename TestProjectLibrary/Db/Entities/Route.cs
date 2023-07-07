using System.ComponentModel.DataAnnotations;

namespace TestProjectLibrary.Db.Entities;

public class Route
{
    // Mandatory
    // Identifier of the whole route
    public Guid Id { get; set; }

    // Mandatory
    // Start point of route
    [MaxLength(100)] 
    public string Origin { get; set; }

    // Mandatory
    // End point of route
    [MaxLength(100)]
    public string Destination { get; set; }

    // Mandatory
    // Start date of route
    public DateTime OriginDateTime { get; set; }

    // Mandatory
    // End date of route
    public DateTime DestinationDateTime { get; set; }

    // Mandatory
    // Price of route
    public decimal Price { get; set; }

    // Mandatory
    // Timelimit. After it expires, route became not actual
    public DateTime TimeLimit { get; set; }

    public IList<Point> Points { get; set; }

    [MaxLength(400)]
    
    public string Hash { get; set; }
}