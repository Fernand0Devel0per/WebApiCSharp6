namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; private set; }

    public bool Active { get; private set; } 

    public Category(string name, string createBy, string editedBy)
    {
        Active = true;
        Name = name;
        CreateBy = createBy;
        EditedBy = editedBy;
        CreateOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();

    }

    private void Validate()
    {
        var contract = new Contract<Category>().
                    IsNotNullOrEmpty(Name, "Name")
                    .IsGreaterOrEqualsThan(Name, 3, "Name")
                    .IsNotNullOrEmpty(CreateBy, "CreateBy")
                    .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active, string editedBy)
    {
        Active = active;
        Name = name;
        EditedBy = editedBy;
        Validate();
    }
   
            
}
