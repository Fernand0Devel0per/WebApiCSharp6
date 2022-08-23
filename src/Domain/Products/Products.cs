namespace IWantApp.Domain.Products;

public class Products
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Category Description { get; set; }
    public bool HasStock { get; set; }
    public string CreateBy { get; set; }
    public DateTime CreateOn { get; set; }
    public string EditedBy { get; set; }
    public DateTime EditedOn { get; set; }
}
