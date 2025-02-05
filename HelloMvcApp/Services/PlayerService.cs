using AngleSharp.Html.Dom;
using AngleSharp;
using WOD.DAL.Repositories;
using WOD.Domain.Models;
using AngleSharp.Dom;

namespace WOD.WebUI.Services
{
	public class PlayerService
	{
        private static readonly HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://1xbet.whoscored.com"),
        };
        private readonly PlayerRepository _repository;
		private readonly FootballClubService _footballClubService;

		public PlayerService(PlayerRepository repository, FootballClubService footballClubService)
		{
			_repository = repository;
			_footballClubService = footballClubService;

		}

		public PlayerService() { }

		public async Task<Player> Get(int id) => await _repository.Get(id);

		public List<Player> GetList() => _repository.GetList();

		public Player Create(Player player)
		{
			_repository.Create(player);

			return player;
		}

		public async Task Delete(int id) => await _repository.Delete(id);
		

		public async Task<Player> Update(Player player)
		{
			_repository.Update(player);

			return player;
		}

		public async Task SaveAsync() => await _repository.SaveAsync();

		public async Task ClearData() => await _repository.ClearData();

		public async Task<IHtmlDocument> GetHtmlDocumentAsync()
		{
            using var response = await sharedClient.GetAsync("regions/252/tournaments/2/seasons/9075/stages/20934/playerstatistics/england-premier-league-2022-2023").ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var browsingContext = new BrowsingContext();

            var document = await browsingContext.OpenAsync(x => x.Content(content)).ConfigureAwait(false);

            return (IHtmlDocument)document;
        }

		

		public async Task GetNewListEPLPlayers(IHtmlDocument document)
		{
            await ClearData();

            var section = document.QuerySelector<IHtmlElement>("table.player-table-statistics-body tbody");

            var rows = section.QuerySelectorAll<IHtmlElement>("tr");

            var newPlayer = new Player();

            foreach (var row in rows)
            {
                var name = row.QuerySelector("span")?.TextContent ?? "No found";
                var age = Byte.Parse(row.QuerySelector("span.player-meta-data").TextContent);
				var footballClub = await _footballClubService.DefineFootballClub(row.QuerySelector("a.player-meta-data").GetAttribute("href"));

				newPlayer.UpdatePlayerInfo(name, age, footballClub);

                Create(newPlayer);
            }
            await SaveAsync();
        }
	}
}
