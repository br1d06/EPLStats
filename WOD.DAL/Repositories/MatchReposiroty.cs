using WOD.Domain.Models;
using WOD.WebUI.Data;

namespace WOD.DAL.Repositories;

public class MatchReposiroty : IRepository<Match>
{
    private readonly PostgresContext _context;

    public MatchReposiroty(PostgresContext context)
    {
        _context = context;
    }

    public List<Match> GetList() => _context.Matches.ToList();

    public async Task<Match> Get(int matchId)
    {
        var match = await _context.Matches.FindAsync(matchId);

        return match ?? throw new NullReferenceException();
    }

    public Match Create(Match match)
    {
        _context.Matches.Add(match);

        return match;
    }

    public Match Update(Match match)
    {
        _context.Matches.Update(match);

        return match;
    }

    public async Task Delete(int matchId)
    {
        var match = await _context.Matches.FindAsync(matchId);

        if (match != null)
            _context.Matches.Remove(match);
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();

}