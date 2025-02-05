using Microsoft.AspNetCore.Mvc;
using WOD.Domain.Models;
using WOD.WebUI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HelloMvcApp.Controllers;

namespace WOD.WebUI.ViewModels;

public class HomeViewModel
{
	public List<FootballClub> FootballClubs { get; set; }
    public List<News> ContainerNews { get; set; }
    public News DefaultNews { get; }
    public News TranslayedNews { get; set; } 

	public HomeViewModel(List<FootballClub> footballClubs,List<News> allNews, int index=0)
    {
        footballClubs.Sort();
        FootballClubs = footballClubs;
        ContainerNews = NewsService.GetContainerNews(allNews);
        DefaultNews= NewsService.DefaultNews;
        if(ContainerNews != null && ContainerNews.Count != 0)
            TranslayedNews = ContainerNews[index];
        else
            TranslayedNews= DefaultNews;
	}

	public HomeViewModel(List<FootballClub> footballClubs) 
    {
		FootballClubs = footballClubs;
	}
    
    public HomeViewModel() 
    {   
    }
}
	
		
