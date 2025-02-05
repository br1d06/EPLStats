using WOD.Domain.Models;
using WOD.WebUI.Data;

namespace WOD.DAL.Repositories;

public class NewsRepository : IRepository<News>
{
    private readonly PostgresContext _context;

    public NewsRepository(PostgresContext context)
    {
        _context = context;
    }

    public List<News> GetList() => _context.News.ToList();


    public async Task<News> Get(int newsId)
    {
        var news = await _context.News.FindAsync(newsId);

        return news;
    }

    public News Create(News news)
    {
        _context.News.Add(news);

        return news;
    }

    public News Update(News news)
    {
        _context.News.Update(news);

        return news;
    }

    public async Task Delete(int newsId)
    {
        var news = await _context.News.FindAsync(newsId);

        if (news != null)
            _context.News.Remove(news);
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();

}