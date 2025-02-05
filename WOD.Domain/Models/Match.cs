using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WOD.Domain.Models;
public class Match
{
	[Key]
	public int Id { get; set; }
	public ICollection<FootballClub> MatchParticipants { get;  set; } = new List<FootballClub>(2);
	public byte HomeFootballClubGoals { get;  set; }
	public byte AwayFootballClubGoals { get;  set; }
	public enum Result
	{
		FootballClub1Win,
		FootballClub2Win,
		Draw
	}
	public Result MatchResult { get; set; }

	public Match(FootballClub homeFootballClub, FootballClub awayFootballClub,byte footballClub1Goals,byte footballClub2Goals) 
	{
		MatchParticipants = new List<FootballClub>
		{
			[0] = homeFootballClub,
			[1] = awayFootballClub
		};

		HomeFootballClubGoals = footballClub1Goals;
		AwayFootballClubGoals = footballClub2Goals;

		if (footballClub1Goals == footballClub2Goals)
			MatchResult = Result.Draw;
        else
			MatchResult = footballClub1Goals > footballClub2Goals ? Result.FootballClub1Win : Result.FootballClub2Win;
    }
	public Match() { }
}

