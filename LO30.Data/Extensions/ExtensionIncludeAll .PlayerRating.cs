using LO30.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace LO30.Data.Extensions
{
  public static partial class ExtensionIncludeAll
  {
    public static IQueryable<PlayerRating> IncludeAll(this IQueryable<PlayerRating> query)
    {
      return query
        .Include("Season")
        .Include("Player")
        ;
    }
  }
}
