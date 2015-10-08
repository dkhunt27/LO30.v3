using LO30.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class ScoreSheetEntryRoster
  {
    public int GameId { get; set; }

    public bool HomeTeam { get; set; }

    public GameTeam GameTeam { get; set; }

    public List<GameRoster> GameRoster { get; set; }

    public List<TeamRoster> TeamRoster { get; set; }
  }
}