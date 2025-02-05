using System.Reflection;
using WOD.DAL.Repositories;
using WOD.Domain.Models;

namespace WOD.WebUI.Services;

public class NewsService
{
	public static readonly News DefaultNews = 
		new ("Error 2505: The news not found.", 
		"Sorry, we're already working on a fix.", 
		"errorNews.png");

	private readonly NewsRepository _repository;

	public NewsService(NewsRepository repository)
	{
		_repository = repository;
	}

	public NewsService() { }

	public async Task<News> Create(News news)
	{
		_repository.Create(news);
		await _repository.SaveAsync();

		return news;
	}

	public async Task Delete(int id)
	{
		await _repository.Delete(id);
		await _repository.SaveAsync();
	}

	public async Task<News> Update(News news)
	{
		_repository.Update(news);
		await _repository.SaveAsync();

		return news;
	}

	public List<News> GetList() => _repository.GetList();
		

	public async Task<News> Get(int id) => await _repository.Get(id) ?? DefaultNews;

	public static List<News> GetContainerNews(List<News> allNews)
	{
		if (allNews.Count >= 10)			
			return allNews.GetRange(allNews.Count - 10, 10);
			
		else
		return allNews;
	}
}

