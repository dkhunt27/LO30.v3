﻿using LO30.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace LO30.Data.Extensions
{
  public static partial class ExtensionIncludeAll
  {
    public static IQueryable<Season> IncludeAll(this IQueryable<Season> query)
    {
      return query
        ;
    }
  }
}
