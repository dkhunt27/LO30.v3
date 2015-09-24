using System.Data.Entity.Migrations;

namespace LO30.Data.Contexts
{
  public class LO30MigrationsConfiguration
    : DbMigrationsConfiguration<LO30Context>
  {


    public LO30MigrationsConfiguration()
    {
      this.AutomaticMigrationDataLossAllowed = true;
      this.AutomaticMigrationsEnabled = true;
    }

    protected override void Seed(LO30Context context)
    {
      base.Seed(context);
      LO30ContextSeed seeder = new LO30ContextSeed(context);
      seeder.Seed();
    }
  }
}
