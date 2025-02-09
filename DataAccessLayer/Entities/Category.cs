using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DataAccess.Entities;

public class Category
{
    [Key] public Guid Id { get; set; }

    [DisplayName("Category Name")]
    [
        Required, 
        MinLength(3, ErrorMessage="At-least 3 characters Required"), 
        MaxLength(30,  ErrorMessage="Max Length should not exceed 124 characters")
    ]
    public string? Name { get; set; }
    
    [DisplayName("Category Description")]
    [
        Required, 
        MaxLength(124, ErrorMessage="Max Length should not exceed 124 characters"), 
        MinLength(5, ErrorMessage="At-least 5 characters Required")
    ]
    public string? Description { get; set; }
    
}