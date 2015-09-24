using System.Data.Entity;

namespace LO30.Data.Contexts
{
  //public class LO30ContextSeedInitializer : DropCreateDatabaseIfModelChanges<LO30Context>
  public class LO30ContextSeedInitializer : DropCreateDatabaseAlways<LO30Context>
  {

    protected override void Seed(LO30Context context)
    {
      base.Seed(context);
      LO30ContextSeed seeder = new LO30ContextSeed(context);
      seeder.Seed();
    }
  }
}
