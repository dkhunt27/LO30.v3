using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LO30.Data.Models
{
  public class ScoreSheetEntryProcessedSub
  {
    [Required, Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ScoreSheetEntrySubId { get; set; }

    [Required, Index("PK2", 1, IsUnique = true)]
    public int SeasonId { get; set; }

    [Required, Index("PK2", 2, IsUnique = true)]
    public int TeamId { get; set; }

    [Required, Index("PK2", 3, IsUnique = true)]
    public int GameId { get; set; }

    [Required, Index("PK2", 4, IsUnique = true)]
    public int SubPlayerId { get; set; }

    [Required, Index("PK2", 5, IsUnique = true)]
    public int SubbingForPlayerId { get; set; }

    [Required]
    public bool HomeTeam { get; set; }

    [Required, MaxLength(5)]
    public string JerseyNumber { get; set; }

    [Required]
    public DateTime UpdatedOn { get; set; }

    // virtual, foreign keys
    [ForeignKey("SeasonId")]
    public virtual Season Season { get; set; }

    [ForeignKey("TeamId")]
    public virtual Team Team { get; set; }

    [ForeignKey("GameId")]
    public virtual Game Game { get; set; }

    [ForeignKey("SubPlayerId")]
    public virtual Player SubPlayer { get; set; }

    [ForeignKey("SubbingForPlayerId")]
    public virtual Player SubbingForPlayer { get; set; }

    public ScoreSheetEntryProcessedSub()
    {
    }

    public ScoreSheetEntryProcessedSub(int ssesid, int gid, bool ht, int tid, int sid, string jer, int spid, int sfpid, DateTime upd)
    {
      this.ScoreSheetEntrySubId = ssesid;

      this.GameId = gid;
      this.HomeTeam = ht;
      this.TeamId = tid;
      this.SeasonId = sid;
      this.JerseyNumber = jer;
      this.SubPlayerId = spid;
      this.SubbingForPlayerId = sfpid;

      this.UpdatedOn = upd;

      Validate();
    }


    private void Validate()
    {
      var locationKey = string.Format("ssesid: {0}, gid: {1}",
                            this.ScoreSheetEntrySubId,
                            this.GameId);
    }
  }
}