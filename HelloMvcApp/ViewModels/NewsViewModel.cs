using WOD.Domain.Models;
using WOD.WebUI.Services;
using Microsoft.EntityFrameworkCore;

namespace WOD.WebUI.ViewModels;

public class NewsViewModel
{
    public List<News> AllNews { get; private set; }
	public News DefaultNews { get; }

	public NewsViewModel(List<News> allNews)
    {
        DefaultNews = NewsService.DefaultNews;
        AllNews = allNews;
    }

    public NewsViewModel() 
    {  
    }      
}

