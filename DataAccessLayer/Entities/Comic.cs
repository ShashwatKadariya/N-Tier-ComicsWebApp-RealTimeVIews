using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DataAccess.Entities;

public class Comic
{
    [Key]
    public Guid Id { get; set; }
    
    
    [DisplayName("Comic Name")]
    [
        Required, 
        MinLength(1, ErrorMessage="At-least 3 characters Required"), 
        MaxLength(256,  ErrorMessage="Max Length should not exceed 30 characters")
    ]
    public string? Title { get; set; }
    
    [DisplayName("Comic Description")]
    [
        Required, 
        MinLength(1, ErrorMessage="At-least 3 characters Required"), 
        MaxLength(1024,  ErrorMessage="Max Length should not exceed 30 characters")
    ]
    public String? Description { get; set; }
    
    
    [DisplayName("Comics in stock")]
    [
        Required, 
        Range(1, 10000, ErrorMessage = "in stock range should be between 1 - 10000")
    ]
    public int inStock { get; set; }
    
    [
        DisplayName("Comic Price"),
        Required,
        Range(1, 10000, ErrorMessage = "price should be between 1 - 10000")
    ]
    public double Price{get;set;}
    
    public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category Category { get; set; }
    
    [ValidateNever]
    public string ImageUrl { get; set; }
    
    
    [DefaultValue(0)]
    public int ViewCount { get; set; }
}



