using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data.Models
{
  public class ScoreSheetEntryProcessedGame
  {
    [Required, Key, Column(Order = 1), DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int GameId { get; set; }

    [Required]
    public DateTime UpdatedOn { get; set; }

    // virtual, foreign keys
    [ForeignKey("GameId")]
    public virtual Game Game { get; set; }
  }
}