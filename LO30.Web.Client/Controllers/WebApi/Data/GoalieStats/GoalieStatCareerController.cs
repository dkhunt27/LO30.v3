using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LO30.Data.Extensions;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatCareerController : ApiController
  {
    public GoalieStatCareerController()
    {
    }

    public PlayerStatCareer GetGoalieStatCareerByPlayerIdSub(int playerId, bool sub)
    {
      throw new NotImplementedException();
    }
  }
}
