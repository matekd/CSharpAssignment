﻿namespace Business.Models;

public class ContactRegistrationForm
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}
