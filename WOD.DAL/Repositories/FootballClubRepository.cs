using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WOD.Domain.Models;
using WOD.WebUI.Data;

namespace WOD.DAL.Repositories;

public class FootballClubRepository : IRepository<FootballClub>
{
    private readonly PostgresContext _context;

    public FootballClubRepository(PostgresContext context)
    {
        _context = context;
    }

    public List<FootballClub> GetList() => _context.FootballClubs.ToList();


    public async Task<FootballClub> Get(int footballClubId)
    {
        var footballClub = await _context.FootballClubs.FindAsync(footballClubId);

        return footballClub ?? throw new ObjectNotFoundException("Football club not found");
    }

    public async Task<FootballClub> Get(string footballClubName)
    {
        var footballClub = await _context.FootballClubs.FirstOrDefaultAsync((f) => f.Name == footballClubName);

        return footballClub ?? Create(new FootballClub()
        {
            Name = footballClubName
        });
    }

    public FootballClub Create(FootballClub footballClub)
    {
        _context.FootballClubs.Add(footballClub);

        return footballClub;
    }

    public FootballClub Update(FootballClub footballClub)
    {
        _context.FootballClubs.Update(footballClub);

        return footballClub;
    }

    public async Task Delete(int footballClubId)
    {
        var footballClub = await _context.FootballClubs.FindAsync(footballClubId);

        if (footballClub != null)
            _context.FootballClubs.Remove(footballClub);
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();

    public async Task ClearData() => await _context.FootballClubs.ExecuteDeleteAsync();
}

