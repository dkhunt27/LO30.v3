using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameOutcomeController : ApiController
  {
    
    public GameOutcomeController()
    {
      
    }

    public GameOutcome GetGameOutcomeByGameIdAndHomeTeam(int gameId, bool homeTeam, bool fullDetail = true)
    {
      var results = new GameOutcome();

      using (var context = new LO30Context())
      {
        results = context.GameOutcomes.Where(x => x.GameId == gameId && x.HomeTeam == homeTeam).FirstOrDefault();
      }
      return results;
    }
  }
}
