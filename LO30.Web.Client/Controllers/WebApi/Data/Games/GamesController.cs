using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GamesController : ApiController
  {
    public GamesController()
    {
    }

    public List<Game> GetGames()
    {

      var results = new List<Game>();

      using (var context = new LO30Context())
      {
        results = context.Games.ToList();
      }
      return results.OrderByDescending(x => x.GameId)
                    .ToList();
    }

    public Game GetGamesByGameId(int gameId)
    {
      var results = new Game();

      using (var context = new LO30Context())
      {
        results = context.Games.Where(x => x.GameId == gameId).FirstOrDefault();
      }
      return results;
    }
  }
}
