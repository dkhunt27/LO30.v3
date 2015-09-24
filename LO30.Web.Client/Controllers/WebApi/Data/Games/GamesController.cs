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

    //public List<Game> GetGames()
    //{
    //  var results = _repo.GetGames();
    //  return results.OrderByDescending(x => x.GameId).ToList();
    //}

    //public Game GetGamesByGameId(int gameId)
    //{
    //  var result = _repo.GetGameByGameId(gameId);
    //  return result;
    //}
  }
}
