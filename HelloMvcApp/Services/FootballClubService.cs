using Microsoft.AspNetCore.Mvc;
using WOD.Domain.Models;
using WOD.WebUI.Data;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Dom;
using Microsoft.EntityFrameworkCore;
using WOD.DAL.Repositories;
namespace WOD.WebUI.Services
{
	public class FootballClubService
    {
        private readonly Dictionary<string, string> rusFootballClubNames = new Dictionary<string, string>
        {
            {"Ливерпуль","Liverpool" },
            {"Арсенал" , "Arsenal" },
            {"Челси" , "Chelsea" },
            {"Брайтон" , "Brighton & Hove Albion" },
            {"Манчестер Сити" , "Manchester City" },
            {"Ноттингем Форест" , "Nottingham Forest" },
            {"Тоттенхэм" , "Tottenham Hotspur" },
            {"Брентфорд" , "Brentford" },
            {"Манчестер Юнайтед" , "Manchester United" },
            {"Фулхэм" , "Fulham" },
            {"Ньюкасл" , "Newcastle United" },
            {"Астон Вилла" , "Aston Villa" },
            {"Борнмут" , "AFC Bournemouth" },
            {"Вест Хэм" , "West Ham United" },
            {"Эвертон" , "Everton" },
            {"Лестер Сити" , "Leicester City" },
            {"Кристал Пэлас" , "Crystal Palace" },
            {"Вулверхэмптон" , "Wolverhampton Wanderers" },
            {"Ипсвич Таун" , "Ipswich Town" },
            {"Саутгемптон" , "Southampton" },
			{"Шеффилд Юнайтед", "Sheffield United" },
			{"Лидс Юнайтед","Leeds United" },
			{"Сандерленд","Sunderland"},
			{"Бернли","Burnley"},
			{"Мидлсбро","Middlesbrough"},
			{"Блэкберн","Blackburn Rovers"},
			{"Вест Бромвич","West Bromwich Albion"},
			{"Уотфорд","Watford"},
			{"Куинз Парк Рейнджерс","Queens Park Rangers"},
			{"Бристоль Сити","Bristol City"},
			{"Шеффилд Уэнсдей","Sheffield Wednesday"},
			{"Норвич Сити","Norwich City"},
			{"Ковентри Сити","Coventry City"},
			{"Оксфорд Юнайтед","Oxford United"},
			{"Суонси","Swansea City"},
			{"Престон Норт Энд","Preston North End"},
			{"Миллуол","Millwall"},
			{"Сток Сити","Stoke City"},
			{"Кардифф Сити","Cardiff City"},
			{"Дерби Каунти","Derby County"},
			{"Халл Сити","Hull City"},
			{"Портсмут","Portsmouth"},
			{"Лутон Таун","Luton Town"},
			{"Плимут Аргайл","Plymouth Argyle"},
		};

        private readonly FootballClubRepository _repository;

        private static readonly HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://soccer365.ru"),
        };

        public FootballClubService(FootballClubRepository repository)
        {
			_repository = repository;
        }

		public FootballClub Create(FootballClub footballCLub)
		{
			_repository.Create(footballCLub);

			return footballCLub;
		}

		public async Task Delete(int id) => await _repository.Delete(id);
		

		public FootballClub Update(FootballClub footballCLub)
		{
			_repository.Update(footballCLub);

			return footballCLub;
		}

		public List<FootballClub> GetListFootballClubs() => _repository.GetList();

		public async Task SaveAsync() => await _repository.SaveAsync();
		
		public async Task ClearData() => await _repository.ClearData().ConfigureAwait(false);

		public async Task<FootballClub> GetFootballClub(int id) => await _repository.Get(id);
		public async Task<FootballClub> GetFootballClub(string name) => await _repository.Get(name);

		public async Task<IHtmlDocument> GetHtmlDocumentAsync()
		{
			using var response = await sharedClient.GetAsync("competitions/12/").ConfigureAwait(false);

			var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

		    var browsingContext = new BrowsingContext();

			var document = await browsingContext.OpenAsync(x => x.Content(content)).ConfigureAwait(false);

			return (IHtmlDocument)document;
		}
		
		public async Task GetNewListEPLClubs(IHtmlDocument document)
		{
			await ClearData();

			var section = document.QuerySelector<IHtmlElement>("table.tablesorter tbody");

			var footballClubsInfo = section.QuerySelectorAll<IHtmlElement>("tr");

			var newFootballCLub = new FootballClub();

			foreach (var row in footballClubsInfo)
			{
				var logo = row.QuerySelector("img")?.GetAttribute("src") ?? "No found";
				var rusName = row.QuerySelector("a").TextContent;

				newFootballCLub.Name = rusFootballClubNames.GetValueOrDefault(rusName) ?? rusName;
				newFootballCLub.Logo = logo;

				Create(newFootballCLub);
			}
			await SaveAsync();
		}

		public async Task UpdateFootballClubsList(IHtmlDocument document)
		{
			var section = document.QuerySelector<IHtmlElement>("table.tablesorter tbody");

			var footballClubsInfo = section.QuerySelectorAll<IHtmlElement>("tr");

			foreach (var row in footballClubsInfo)
			{
				var rusName = row.QuerySelector("a").TextContent;
				
				string name = rusFootballClubNames.GetValueOrDefault(rusName) ?? rusName;

				var footballClub = await GetFootballClub(name);

				footballClub.Rank = Byte.Parse(row.QuerySelector("td").TextContent);
				footballClub.Logo = row.QuerySelector("img")?.GetAttribute("src") ?? "No found";
				footballClub.Games = Byte.Parse(row.Children[2].TextContent);
				footballClub.Wins = Byte.Parse(row.Children[3].TextContent);
				footballClub.Ties = Byte.Parse(row.Children[4].TextContent);
				footballClub.Losses = Byte.Parse(row.Children[5].TextContent);
				footballClub.GoalsFor = Byte.Parse(row.Children[6].TextContent);
				footballClub.GoalsAgainst = Byte.Parse(row.Children[7].TextContent);
				footballClub.GoalsDifference = (sbyte)int.Parse(row.Children[8].TextContent);
				footballClub.Points = Byte.Parse(row.Children[9].TextContent);

				Update(footballClub);
			}
			await SaveAsync();
		}

        public async Task<FootballClub> DefineFootballClub(string url)
        {
            using var response = await sharedClient.GetAsync(url).ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var browsingContext = new BrowsingContext();

            var document = await browsingContext.OpenAsync(x => x.Content(content)).ConfigureAwait(false);

			string footballClubsName = document.QuerySelector("h1.team-header span").TextContent;

			return await GetFootballClub(footballClubsName);
        }
    }
}

