using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WOD.Domain.Models;
public class FootballClub : IEquatable<FootballClub>, IComparable<FootballClub>
{
    [Key]
    public int Id { get; set; }

    public string Name { get;   set; }

    public string Logo { get; set; }   
    
    public byte Games {get; set; }

    public byte Wins { get; set; }

    public byte Losses { get; set; }

    public byte Ties { get; set; }

    public byte GoalsFor { get; set; }

    public byte GoalsAgainst { get; set; }   
    
    public byte Rank { get; set; }

    public byte Points {  get; set; } 

    public sbyte GoalsDifference {  get; set; }

    public ICollection<Player> Players { get; set; }

    public ICollection<Match> Matches { get; set; }

    public FootballClub(string fullName, string logo)
    {           
        Name = fullName;
        Logo = logo;
        Players = new HashSet<Player>();
        Matches = new HashSet<Match>();
    }       

    public FootballClub() { }
	public int CompareTo(FootballClub compareFootballClub)
	{
		// A null value means that this object is greater.
		if (compareFootballClub == null)
			return 1;

		else
			return compareFootballClub.Points.CompareTo(this.Points);
	}
	public override int GetHashCode()
	{
		return Points;
	}
	public bool Equals(FootballClub other)
	{
		if (other == null) return false;
		return other.Points.Equals(this.Points);
	}
}