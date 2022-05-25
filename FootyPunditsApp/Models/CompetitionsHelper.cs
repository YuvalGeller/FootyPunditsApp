using FootballDataApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FootyPunditsApp.Models
{
    public class CompetitionsHelper
    {
        public string CompetitionName { get; set; }
        public int Id { get; set; }
        public ObservableCollection<Match> Matches { get; set; }
    }
}
