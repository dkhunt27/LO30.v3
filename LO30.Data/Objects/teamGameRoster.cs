using LO30.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class TeamGameRoster
  {
    public bool RosteredPlayed { get; set; }

    public bool RosteredWasSubbedFor { get; set; }

    public TeamRoster Rostered { get; set; }

    public List<GameRoster> SubbedForRostered { get; set; }

    public TeamGameRoster()
    {
      this.RosteredPlayed = true;
      this.RosteredWasSubbedFor = false;
    }
  }
}