using BusinessLogicLayer.Service.IService;
using ComicsWeb.Hubs;
using ComicsWeb.Utility;
using DataAccess.Entities;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;

namespace ComicsWeb.Areas.Admin.Controllers;


[Authorize(Roles = SD.RoleAdmin)]
[Area("Admin")]
public class ComicController: Controller
{
    private readonly IComicService _comicService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ICategoryService _categoryService;

    public ComicController(
        IComicService comicService, 
        ICategoryService categoryService, 
        IWebHostEnvironment webHostEnvironment) 
    {
        _comicService = comicService;
        _categoryService = categoryService;
        _webHostEnvironment = webHostEnvironment;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<Comic> allComicList = _comicService.GetAll(includeProperties: "Category").ToList();
        return View(allComicList);
    }

    [HttpGet]
    public IActionResult Upsert(Guid? id)
    {
        ComicVM comicVm = new()
        {
            Comic = new Comic(),
            CategoryList = _categoryService.GetAll()
                .Select(
                    obj => new SelectListItem(
                            obj.Name,
                            obj.Id.ToString()
                        )
                    )
        };
        // create
        if (id == null || id == Guid.Empty)
        {
            return View(comicVm);
        }
        //update
        else
        {
            comicVm.Comic = _comicService.GetById(id.Value);
            return View(comicVm);
        }
    }

    [HttpPost]
    public IActionResult Upsert(ComicVM comicVm, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);  
                string comicPath = Path.Combine(wwwRootPath, "images", "comics");

                if (!string.IsNullOrEmpty(comicVm.Comic.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, comicVm.Comic.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                
                using (var fileStream = new FileStream(Path.Combine(comicPath, filename), FileMode.Create)) 
                {
                    file.CopyTo(fileStream);
                }

                comicVm.Comic.ImageUrl = @"/images/comics/" + filename;
            }

            // create
            if (comicVm.Comic.Id == Guid.Empty)
            {
                _comicService.Add(comicVm.Comic);
            }
            // update
            else
            {
                _comicService.Update(comicVm.Comic);
            }
            TempData["success"] = "Comic Created";
            return RedirectToAction("Index");
        }
        return View(comicVm);
    }
    
    
    #region API CALLS
    
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Comic> allComicList = _comicService.GetAll(includeProperties:"Category").ToList();
        return Json(new
        {
            data= allComicList
        });
    }
    
    [HttpDelete]
    public IActionResult Delete(Guid? id)
    {
        var comicToBeDeleted = _comicService.Get(x =>x.Id == id);
        if (comicToBeDeleted == null)
        {
            return Json( 
                new { success = false, message = "Error While Deleting" }
                );
        }
        
        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, comicToBeDeleted.ImageUrl.TrimStart('/'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }
        
        _comicService.Delete(comicToBeDeleted);
        
        return Json(new
        {
            success = true,
            message = "Comic Deleted"
        });
    }
    
    #endregion
        
}