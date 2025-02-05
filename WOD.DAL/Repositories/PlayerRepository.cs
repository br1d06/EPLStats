using Microsoft.EntityFrameworkCore;
using WOD.Domain.Models;
using WOD.WebUI.Data;

namespace WOD.DAL.Repositories;

public class PlayerRepository : IRepository<Player>
{
    private readonly PostgresContext _context;

    public PlayerRepository(PostgresContext context)
    {
        _context = context;
    }

    public List<Player> GetList() => _context.Players.ToList();


    public async Task<Player> Get(int playerId)
    {
        var player = await _context.Players.FindAsync(playerId);

        return player ?? throw new NullReferenceException("Player not found");
    }

    public Player Create(Player player)
    {
        _context.Players.Add(player);

        return player;
    }

    public Player Update(Player player)
    {
        _context.Players.Update(player);

        return player;
    }

    public async Task Delete(int playerId)
    {
        var player = await _context.Players.FindAsync(playerId);

        if (player != null)
            _context.Players.Remove(player);
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();

    public async Task ClearData() => await _context.Players.ExecuteDeleteAsync();

}