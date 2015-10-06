using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatGameController : ApiController
  {
    public PlayerStatGameController()
    {
    }

    public PlayerStatGame GetPlayerStatGameByPlayerIdGameId(int playerId, int gameId)
    {
      var results = new PlayerStatGame();

      using (var context = new LO30Context())
      {
        results = context.PlayerStatGames
                          .Where(x => x.PlayerId == playerId && x.GameId == gameId)
                          .IncludeAll()
                          .SingleOrDefault();
      }
      return results;
    }
  }
}
