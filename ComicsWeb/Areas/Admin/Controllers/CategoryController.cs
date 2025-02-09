using BusinessLogicLayer.Service.IService;
using ComicsWeb.Hubs;
using DataAccess.Entities;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ComicsWeb.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class CategoryController: Controller
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        _logger.LogInformation("Category Index");
        List<Category> categories = _categoryService.GetAll().ToList();
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryService.Add(category);
            TempData["success"] = "Category Created";
            return RedirectToAction("Index");
        }

        return View();
    }

    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        if (id == Guid.Empty) return NotFound(); 
        Category? foundCategory = _categoryService.Get(x => x.Id == id);
        if (foundCategory == null) return NotFound();
        
        return View(foundCategory);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryService.Update(category);
            TempData["success"] = "Category Updated";
            return RedirectToAction("Index");
        }  
        return View();
    }

    [HttpGet]
    public IActionResult Delete(Guid id)
    {
        if (id == Guid.Empty) return NotFound(); 
        Category? foundCategory = _categoryService.Get(x => x.Id == id);
        if (foundCategory == null) return NotFound();
        return View(foundCategory);   
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(Category category)
    {
        Guid id = category.Id;
        if (ModelState.IsValid)
        {
            TempData["success"] = "Category Deleted";
            _categoryService.Delete(category);
        }
        return RedirectToAction("Index");
    }
}