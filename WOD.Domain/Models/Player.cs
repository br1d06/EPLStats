using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOD.Domain.Models
{
    public class Player
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public byte Age { get; private set; }
        public FootballClub FootballClub { get; private set; }
        public byte Goals { get; private set; }
        public byte Assists { get; private set; }
        public byte MatchesAppearances { get; private set; }
        public byte YellowCards { get; private set; }
        public byte RedCards { get; private set; }

        public Player(string name, byte age, FootballClub footballClub) 
        { 
            Name= name;
            FootballClub= footballClub;
            Age= age;
        }
        public Player() { }

        public void UpdatePlayerInfo(string name, byte age, FootballClub footballClub)
        {
            Name = name;
            Age = (byte)age;
            FootballClub= footballClub;
        }
    }
}
