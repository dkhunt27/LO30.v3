using LO30.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace LO30.Data.Extensions
{
  public static partial class ExtensionIncludeAll
  {
    public static IQueryable<GameTeam> IncludeAll(this IQueryable<GameTeam> query)
    {
      return query
        .Include("Season")
        .Include("Team")
        .Include("Team.Season")
        .Include("Team.Coach")
        .Include("Team.Sponsor")
        .Include("Team.Division")
        .Include("Game")
        .Include("Game.Season")
        .Include("OpponentTeam")
        .Include("OpponentTeam.Season")
        .Include("OpponentTeam.Coach")
        .Include("OpponentTeam.Sponsor")
        .Include("OpponentTeam.Division")
        ;
    }
  }
}
