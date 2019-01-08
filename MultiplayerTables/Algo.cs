using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerTables
{
    public class Algo
    {
        const int MaxPlayer = 16;
        public List<Player> _players;
        List<Relation> _relations = new List<Relation>();
        int _countTables = 0;
        Random _rand = new Random(DateTime.Now.Millisecond);

        public void Init(List<Player> players)
        {
            _players = new List<Player>(players);
            _relations = CreateBaseRelations(players);
        }

        public void AddNewPlayer(Player player)
        {
            _players.Add(player);

            for (int i = 0; i < _players.Count - 1; i++)
            {
                Player p2 = _players[i];

                _relations.Add(new Relation()
                {
                    Player1 = player,
                    Player2 = p2
                });
            }
        } 

        List<Relation> CreateBaseRelations(List<Player> players)
        {
            List<Relation> relations = new List<Relation>();

            for (int i = 0; i < _players.Count - 1; i++)
            {
                Player p1 = _players[i];

                for (int j = i + 1; j < _players.Count; j++)
                {
                    Player p2 = _players[j];

                    relations.Add(new Relation()
                    {
                        Player1 = p1, 
                        Player2 = p2
                    });
                }
            }

            return relations;
        }

        List<int> GetPattern(List<Player> players)
        {
            if (players.Count < 1 || players.Count > MaxPlayer)
            {
                throw new Exception("limit players");
            }

            var pattern = new List<int>();

            switch (players.Count)
            {
                case 1: pattern = new List<int>() { 1 }; break;
                case 2: pattern = new List<int>() { 2 }; break;
                case 3: pattern = new List<int>() { 3 }; break;
                case 4: pattern = new List<int>() { 4 }; break;
                case 5: pattern = new List<int>() { 5 }; break;
                case 6: pattern = new List<int>() { 3, 3 }; break;
                case 7: pattern = new List<int>() { 4, 3 }; break;
                case 8: pattern = new List<int>() { 4, 4 }; break;
                case 9: pattern = new List<int>() { 3, 3, 3 }; break;
                case 10: pattern = new List<int>() { 4, 3, 3 }; break;
                case 11: pattern = new List<int>() { 4, 4, 3 }; break;
                case 12: pattern = new List<int>() { 4 ,4, 4 }; break;
                case 13: pattern = new List<int>() { 4, 3, 3, 3 }; break;
                case 14: pattern = new List<int>() { 4, 4, 3, 3 }; break;
                case 15: pattern = new List<int>() { 4, 4, 4, 3 }; break;
                case 16: pattern = new List<int>() { 4, 4, 4, 4 }; break;
            }

            return pattern;
        }

        public List<List<Player>> BestSeating()
        {
            List<List<Player>> tables = null;

            int? minRelation = null; 

            for (int i = 0; i < 100; i++)
            {
                var tmp = CreateNewTables();
                var cr = CountRelationWithPast(tmp);

                if (!minRelation.HasValue || cr < minRelation.Value)
                {
                    minRelation = cr;
                    tables = tmp;
                }
            }

            return tables;
        }

        public void UpdateRelations(List<List<Player>> tables)
        {
            IterForRelations(tables, (rel) => rel.CountRelation++);
        }

        int CountRelationWithPast(List<List<Player>> tables)
        {
            int counter = 0;
            IterForRelations(tables, (rel) => counter += rel.CountRelation);
            return counter;
        }

        void IterForRelations(List<List<Player>> tables, Action<Relation> forRelation)
        {
            foreach (var table in tables)
            {
                for (int i = 0; i < table.Count - 1; i++)
                {
                    Player p1 = table[i];

                    for (int j = i + 1; j < table.Count; j++)
                    {
                        Player p2 = table[j];

                        var rel = _relations.FirstOrDefault(
                            r => (r.Player1 == p1 && r.Player2 == p2) || (r.Player1 == p2 && r.Player2 == p1)
                        );

                        if (rel != null)
                        {
                            if (forRelation != null)
                                forRelation(rel);
                        }
                    }
                }
            }
        }

        List<List<Player>> CreateNewTables()
        {
            var plForSort = new List<Player>(_players);

            var tables = new List<List<Player>>();

            foreach (var patt in GetPattern(_players))
            {
                var table = new List<Player>();

                for (int i = 0; i < patt; i++)
                {
                    int index = _rand.Next() % plForSort.Count;
                    table.Add(plForSort[index]);
                    plForSort.RemoveAt(index);
                }

                tables.Add(table);
            }

            return tables;
        }
        
    }
}
