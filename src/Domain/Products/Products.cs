namespace IWantApp.Domain.Products;

public class Products : Entity
{
    public string Name { get; set; }
    public Category Description { get; set; }
    public bool HasStock { get; set; }
    
}
