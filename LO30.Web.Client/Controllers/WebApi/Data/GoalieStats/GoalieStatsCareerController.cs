using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatsCareerController : ApiController
  {
    public GoalieStatsCareerController()
    {
    }

    public List<PlayerStatCareer> GetGoalieStatsCareer()
    {
      throw new NotImplementedException();
    }

    public List<PlayerStatCareer> GetGoalieStatsCareerByPlayerId(int playerId)
    {
      throw new NotImplementedException();
    }
  }
}
