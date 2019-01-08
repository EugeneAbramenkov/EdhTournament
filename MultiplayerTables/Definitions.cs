using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerTables
{
    public class Player
    {
        public string Name { get; set; }

        public string Capitan { get; set; }

        public int Wins { get; set; }

        public int Draw { get; set; }

        public int Point
        {
            get
            {
                return Wins * 3 + Draw * 1;
            }
            set
            {

            }
        }

        public int PowerPoint { get; set; }

        public bool IsWinner { get; set; }

        public string Img
        {
            get
            {
                return $"img\\${Capitan}";
            }
        }
    }

    public class Table
    {
        public string TableName { get; set; }

        public float TableRate { get; set; }

        public string TableInfo
        {
            get
            {
                return $"{TableName} ({TableRate})";
            }
        }

        public List<Player> Players { get; set; } = new List<Player>();
    }

    public class Relation
    {
        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public int CountRelation { get; set; }
    }
}
