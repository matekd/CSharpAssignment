namespace Business.Models;

public class Contact
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string PostalCode { get; set; } = null!;

    public override string ToString()
    {
        return $"{FirstName} {LastName} {Email} {Phone} {Address} {Region} {PostalCode}";
    }
}
