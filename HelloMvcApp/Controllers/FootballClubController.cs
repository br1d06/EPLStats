using System;
using Microsoft.AspNetCore.Mvc;
using WOD.Domain.Models;
using WOD.WebUI.Services;
using WOD.WebUI.ViewModels;
using Microsoft.EntityFrameworkCore;
using WOD.WebUI.Data;

namespace HelloMvcApp.Controllers
{
	public class FootballClubController : Controller
	{		
		private readonly ILogger<FootballClubController> _logger;
		private readonly FootballClubService _footballClubService;
		private readonly NewsService _newsService;
        
    public FootballClubController(ILogger<FootballClubController> logger, FootballClubService footballClubService, NewsService newsService)
		{		
			_logger = logger;
			_footballClubService = footballClubService;
			_newsService = newsService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var homeViewModel = new HomeViewModel(_footballClubService.GetListFootballClubs(), _newsService.GetList());

			return View("~/Views/FootballClub/Index.cshtml", homeViewModel);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var footballClub = await GetFootballClub(id);

			if (footballClub != null)
				return View("~/Views/FootballClub/Details.cshtml", footballClub);
			
			return NotFound();
		}
		
		[HttpGet]
		public async Task<FootballClub> GetFootballClub(int id) => await _footballClubService.GetFootballClub(id);

		[HttpGet]
		public List<FootballClub> GetFootballClubsList() => _footballClubService.GetListFootballClubs();

		[HttpPost]
		public async Task<FootballClub> CreateFootballClub(FootballClub footballClub)
		{
			_footballClubService.Create(footballClub);

			await _footballClubService.SaveAsync();

			return footballClub;
		}

		[HttpDelete]
		public async Task DeleteFootballCLub(int id)
		{
			await _footballClubService.Delete(id);

			await _footballClubService.SaveAsync();
		}

		[HttpPut]
		public async Task<FootballClub> UpdateFootballClub(FootballClub footballClub)
		{
			_footballClubService.Update(footballClub);

			await _footballClubService.SaveAsync();

			return footballClub;
		}
	}
}
