using System.ComponentModel.DataAnnotations;

namespace WOD.Domain.Models;

public class News
{
	[Key]
	public int Id { get;  set; }

	[StringLength(60, MinimumLength = 3)]
	[Required]
	public string? Title { get; set; }
    [Required]
    public string Text { get;  set; }
    [Required]
    public string Image { get;  set; }
    [Required]
    public DateTime DateAdded { get; set; }
	public DateTime DateUpdated { get; set; }

	public News(string title, string text, string image) 
	{
		Title=title; 
		Text=text; 
		Image=image;
		DateAdded = DateTime.Now;
		DateUpdated = DateTime.Now;
	}

	public News() { }
}

