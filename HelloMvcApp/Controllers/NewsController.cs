using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WOD.Domain.Models;
using WOD.WebUI.Services;
using WOD.WebUI.ViewModels;

namespace HelloMvcApp.Controllers;

public class NewsController : Controller
{
	private readonly FootballClubService _footballClubService;
	private readonly ILogger<NewsController> _logger;
	private readonly NewsService _newsService;
	private readonly IWebHostEnvironment _environment;

	public NewsController(ILogger<NewsController> logger, NewsService newsService, FootballClubService footballClubService, IWebHostEnvironment environment)
	{
		_logger = logger;
		_newsService = newsService;
		_environment = environment;
	}
	public IActionResult Index()
	{			
		var newsViewModel = new NewsViewModel(_newsService.GetList());

		return View("~/Views/News/Index.cshtml", newsViewModel);
	}

	[HttpGet]
	public IActionResult GetTranslayedNews(int index)
	{
		var homeViewModel = new HomeViewModel(_footballClubService.GetListFootballClubs(), _newsService.GetList(), index);

		return View("/Views/Home/Index.cshtml", homeViewModel); 
	}
	public IActionResult Create() 
	{
		return View("~/Views/News/Create.cshtml");
	}
    [HttpPost]
    public async Task<IActionResult> Create(News news, IFormFile imageFile)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "news");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    
                    news.Image = Path.Combine("images", "news", uniqueFileName);
                }
                
                news.DateAdded = DateTime.Now;
                news.DateUpdated = news.DateAdded;
                
                await _newsService.Create(news);

                _logger.LogInformation("News item successfully created: {Title}", news.Title);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating news item.");
                ModelState.AddModelError("", "An error occurred while processing your request.");
            }
        }

        // Если модель не валидна, возвращаем представление с ошибками
        return View(news);
    }

    public async Task<IActionResult> Edit(int id)
	{
		if (ModelState.IsValid)
		{
			var news = await _newsService.Get(id);

			if(news!=null) 
				return View(news);
		}

		return NotFound();
    }

	[HttpPut]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(News news)
	{
		if (ModelState.IsValid)
		{
			await _newsService.Update(news);
			_logger.LogInformation("News item edited successfully: {Title}", news.Title);
			return RedirectToAction("Index");
		}
		return View(news);
	}

	public IActionResult IndexAdmin()
	{
        var newsViewModel = new NewsViewModel(_newsService.GetList());

        return View("~/Views/News/IndexAdmin.cshtml", newsViewModel);
	}
       
    [HttpDelete]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
		if (ModelState.IsValid)
		{               
			await _newsService.Delete(id);
			_logger.LogInformation("News item successfully deleted");
			return RedirectToAction("Delete");
		}
        return NotFound();
    }
	[HttpGet]
	[ValidateAntiForgeryToken]
    public async Task<IActionResult> Details(int id)
	{
		var news = await _newsService.Get(id);
	
		if (news == null)
		{
			return NotFound();
		}

		return View(news);
	}
		
}

