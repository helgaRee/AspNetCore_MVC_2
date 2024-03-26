namespace Infrastructure.Entities;

//En entitetsklass som representerar en kurs i databasen.
public class CourseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string? ImageName { get; set; } 
    public string? Author { get; set; }
    public bool IsBestSeller { get; set; } = false;
    public int Hours { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal LikesInNumbers { get; set; }
    public decimal LikesInProcent { get; set; }

}
