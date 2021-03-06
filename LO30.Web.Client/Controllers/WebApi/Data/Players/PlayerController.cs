﻿using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.Players
{
  public class PlayerController : ApiController
  {
    public PlayerController()
    {
    }


    public Player GetPlayerByPlayerId(int playerId)
    {
      var results = new Player();

      using (var context = new LO30Context())
      {
        results = context.Players
                          .Where(x => x.PlayerId == playerId)
                          .IncludeAll()
                          .SingleOrDefault();
      }
      return results;
    }
  }
}
