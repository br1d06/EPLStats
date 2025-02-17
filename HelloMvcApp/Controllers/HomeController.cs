using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WOD.Domain.Models;
using WOD.WebUI.Data;
using WOD.WebUI.Services;
using WOD.WebUI.ViewModels;

namespace HelloMvcApp.Controllers;

public class HomeController : Controller
{       
    private readonly ILogger<HomeController> _logger;
    private readonly FootballClubService _footballClubService;
    private readonly NewsService _newsService;

    public HomeController(ILogger<HomeController> logger, FootballClubService footballClubService, NewsService newsService)
    {
        _logger = logger;
        _footballClubService = footballClubService;
        _newsService = newsService;
    }

    public async Task<IActionResult> Index()
    {
        var homeViewModel = new HomeViewModel(_footballClubService.GetListFootballClubs(), _newsService.GetList());

        var htmlDocument = await _footballClubService.GetHtmlDocumentAsync();

		await _footballClubService.UpdateFootballClubsList(htmlDocument);

		return View("~/Views/Home/Index.cshtml", homeViewModel);

    }
    public IActionResult Error()
    {
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }        

    public IActionResult Privacy()
    {           
        return View(new PrivacyViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

    public IActionResult EPLTable()
    {
        var homeViewModel = new HomeViewModel(_footballClubService.GetListFootballClubs(), _newsService.GetList());

        return View("~/Views/Home/Table.cshtml", homeViewModel);
    }
}
