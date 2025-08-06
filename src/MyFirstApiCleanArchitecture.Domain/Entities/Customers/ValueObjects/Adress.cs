using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstApiCleanArchitecture.Domain.Entities.Customers.ValueObjects
{
    public record Adress(
        string FirstLineAdress, 
        string? SecondLineAdress,
        string Postcode,
        string City,
        string Country);
    

    
}
