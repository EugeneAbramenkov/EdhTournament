using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerTables
{
    public class MainVm : INotifyPropertyChanged
    {
        Algo _algo = new Algo();

        public List<Player> Players
        {
            get;
            set;
        }

        public List<List<Player>> _lastTables;

        public List<Table> Tables
        {
            get
            {
                if (Players == null)
                    return new List<Table>();

                var bs = _algo.BestSeating();

                var tbl = new List<Table>();

                int indexT = 1;
                foreach (var pt in  bs)
                {
                    tbl.Add(new Table() { TableName = $"Table №{indexT++}", Players = pt });
                }

                _lastTables = bs;

                return tbl;
            }
        } 

        public void RefreshTable()
        {
            Players.ForEach(p => p.IsWinner = false);
            NotifyPropertyChanged("Tables");
        }

        public bool NextTable()
        {
            foreach (var tab in _lastTables) 
            {
                int winners = tab.Count(p => p.IsWinner);
                if (winners == 1 || winners == tab.Count)
                {
                    
                }
                else
                {
                    return false;
                }
            }

            foreach (var tab in _lastTables)
            {
                int winners = tab.Count(p => p.IsWinner);

                if (winners == 1)
                {
                    var pl = tab.First(p => p.IsWinner);
                    pl.Wins++;
                }
                else if (winners == tab.Count)
                {
                    tab.ForEach(p => p.Draw++);
                }

                tab.ForEach(p => p.IsWinner = false);
            }

            _algo.UpdateRelations(_lastTables);

            NotifyPropertyChanged("Tables");
            NotifyPropertyChanged("Players");
            NotifyPropertyChanged("Players.Wins");

            return true;
        }

        public void AddNewPlayer(Player player)
        {
            Players.Add(player);
            _algo.AddNewPlayer(player);
        }

        public void Init()
        {
            InitFromFile();
            _algo.Init(Players);
            NotifyPropertyChanged("Tables");
        }

        void CreateTables()
        {

        }

        string _playersFile = "players.txt";

        string _imgFolder = "img";

        public void InitFromFile()
        {
            Players = new List<Player>();

            using (var io = new StreamReader(_playersFile))
            {
                var info = io.ReadToEnd();

                var infos = info.Split('=');

                foreach (var pl in infos)
                {
                    var plStrs = pl.Replace("\r", "").Split('\n');
                    if(plStrs.Length >= 2)
                    {
                        var player = new Player();
                        player.Name = plStrs[1];
                        player.Capitan = plStrs[2];
                        Players.Add(player);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
