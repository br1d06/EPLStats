using WOD.DAL.Repositories;
using WOD.Domain.Models;

namespace WOD.WebUI.Services
{
	public class MatchService
	{
		private readonly MatchReposiroty _repository;

		public MatchService(MatchReposiroty repository)
		{
			_repository = repository;
		}

		public MatchService() { }

		public async Task<Match> Get(int matchId) => await _repository.Get(matchId);

		public List<Match> GetList() => _repository.GetList();

		public async Task<Match> Create(Match match)
		{
			_repository.Create(match);

			await _repository.SaveAsync();

			return match;
		}

		public async Task Delete(int matchId)
		{
			await _repository.Delete(matchId);

			await _repository.SaveAsync();
		}

		public async Task<Match> Update(Match match)
		{
			_repository.Update(match);

			await _repository.SaveAsync();

			return match;
		}
	}
}
