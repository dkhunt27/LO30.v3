using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using LO30.Data.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace LO30.Data.Views
{
  public class PlayerActiveSub
  {
    [Key]
    public int PlayerId { get; set; }

    [Required, MaxLength(35)]
    public string FirstName { get; set; }

    [Required, MaxLength(35)]
    public string LastName { get; set; }

    [MaxLength(5)]
    public string Suffix { get; set; }

    [Required, MaxLength(1)]
    public string PreferredPosition { get; set; }

    [MaxLength(35)]
    public string TeamNameShort { get; set; }
  }

  internal partial class PlayerActiveSubConfiguration : EntityTypeConfiguration<PlayerActiveSub>
  {
    public PlayerActiveSubConfiguration(string schema = "dbo")
    {
      ToTable(schema + ".PlayersActiveSubs");
      HasKey(x => x.PlayerId);

      Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().IsUnicode(false).HasMaxLength(35);
      Property(x => x.LastName).HasColumnName("LastName").IsRequired().IsUnicode(false).HasMaxLength(35);
      Property(x => x.Suffix).HasColumnName("Suffix").IsOptional().IsUnicode(false).HasMaxLength(5);
      Property(x => x.PreferredPosition).HasColumnName("PreferredPosition").IsRequired().IsUnicode(false).HasMaxLength(1);
      Property(x => x.TeamNameShort).HasColumnName("TeamNameShort").IsOptional().IsUnicode(false).HasMaxLength(15);

      InitializePartial();
    }
    partial void InitializePartial();
  }
}