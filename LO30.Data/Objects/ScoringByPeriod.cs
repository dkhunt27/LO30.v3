using LO30.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class ScoringByPeriod
  {
    public int GameId { get; set; }

    public DateTime GameDateTime { get; set; }

    public int TeamId { get; set; }

    public string TeamCode { get; set; }

    public string TeamNameShort { get; set; }

    public string TeamNameLong { get; set; }

    public bool HomeTeam { get; set; }

    public string Outcome { get; set; }

    public int Period1 { get; set; }

    public int Period2 { get; set; }

    public int Period3 { get; set; }

    public int Period4 { get; set; }

    public int Final { get; set; }
  }
}