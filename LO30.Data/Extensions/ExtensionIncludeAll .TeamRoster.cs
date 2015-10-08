using LO30.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace LO30.Data.Extensions
{
  public static partial class ExtensionIncludeAll
  {
    public static IQueryable<TeamRoster> IncludeAll(this IQueryable<TeamRoster> query)
    {
      return query
        .Include("Season")
        .Include("Team")
        .Include("Team.Season")
        .Include("Team.Coach")
        .Include("Team.Sponsor")
        .Include("Team.Division")
        .Include("Player")
        ;
    }
  }
}
