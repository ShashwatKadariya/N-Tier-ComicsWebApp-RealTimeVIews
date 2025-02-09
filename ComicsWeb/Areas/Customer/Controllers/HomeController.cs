using System.Diagnostics;
using BusinessLogicLayer.Service.IService;
using ComicsWeb.Hubs;
using Microsoft.AspNetCore.Mvc;
using ComicsWeb.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.SignalR;

namespace ComicsWeb.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IComicService _comicService;
    private IHubContext<NotificationHub> _notificationHubContext;

    public HomeController(
        ILogger<HomeController> logger,
        IComicService comicService,
        IHubContext<NotificationHub> notificationHubContext)
    {
        _logger = logger;
        _comicService = comicService;
        _notificationHubContext = notificationHubContext;
    }

    public IActionResult Index()
    {
        IEnumerable<Comic> comics = _comicService.GetAll(includeProperties:"Category").ToList();
        return View(comics);
    }
     
    public async Task<IActionResult> Details(Guid id)
    {
        Comic? comic = _comicService.Get(x => x.Id == id ,includeProperties: "Category");
        
        if(comic == null) return NotFound();
        comic.ViewCount++;
        _comicService.Update(comic);
        await _notificationHubContext.Clients.All.SendAsync("ViewCountUpdated", comic.Id, comic.ViewCount);
        
        
        return View(comic);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}