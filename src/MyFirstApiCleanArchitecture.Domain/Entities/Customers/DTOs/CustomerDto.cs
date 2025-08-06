namespace MyFirstApiCleanArchitecture.Domain.Entities.Customers.DTOs;

public abstract class BaseCustomerDto
{
    public string Title { get; set; } = null!;

    public string FirstLineAdress { get; set; } = null!;

    public string? SecondLineAdress { get; set; } = null!;

    public string Postcode { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;
}

public class CreateCustomerDto : BaseCustomerDto;

public class UpdateCustomerDto : BaseCustomerDto
{
    public Guid CustomerID { get; set; }
}