using LO30.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace LO30.Data.Extensions
{
  public static partial class ExtensionIncludeAll
  {
    public static IQueryable<Game> IncludeAll(this IQueryable<Game> query)
    {
      return query
        .Include("Season")
        ;
    }
  }
}
