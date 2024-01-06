namespace DoroTech.BookStore.Domain.Aggregates;

public class User : Entity
{
    // public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] Hash { get; set; } = null!;
    public byte[] Salt { get; set; } = null!;

}
