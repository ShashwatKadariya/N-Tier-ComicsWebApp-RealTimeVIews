using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DataAccess.ViewModels;

public class ComicVM
{
    public Comic Comic { get; set; }
    
    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList { get; set; }
}