namespace Infrastructure.Model;

public class AddressModel
{
    public int Id { get; set; }

    public string AddressLine1 { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}
