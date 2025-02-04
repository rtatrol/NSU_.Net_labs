using Microsoft.EntityFrameworkCore;

using NUnit.Framework;


namespace DreamTeam.Data;

[TestFixture]
class HackatonDbContextTest
{
    private HackatonDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<HackatonDbContext>()
        .UseSqlite("Data Source = :memory:")
        .Options;
        var context = new HackatonDbContext(options);

        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        return context;
    }

    [Test]
    public void CanInsertTest()
    {
        using (var context = GetInMemoryDbContext())
        {

            var hackaton = new Hackaton() { Id = 1, AverageHarmony = 3 };
            context.Hackatons.Add(hackaton);
            context.SaveChanges();

            Assert.AreEqual(context.Hackatons.Count(), 1);
        }
    }

    [Test]
    public void CanReciveTest()
    {
        using (var context = GetInMemoryDbContext())
        {
            var hackaton = new Hackaton() { Id = 2, AverageHarmony = 3 };
            context.Hackatons.Add(hackaton);
            context.SaveChanges();

            var recvHackaton = context.Hackatons.FirstOrDefault(h => h.Id == 2);
            Assert.That(recvHackaton != null);
            Assert.AreEqual(hackaton, recvHackaton!);
        }
    }

    [Test]
    public void CorrectMeanHarmonyTest()
    {
        using (var context = GetInMemoryDbContext())
        {
            Console.WriteLine(context.Hackatons.Count());
            var hackaton1 = new Hackaton() { Id = 3, AverageHarmony = 2 };
            var hackaton2 = new Hackaton() { Id = 4, AverageHarmony = 6 };
            context.Hackatons.AddRange(hackaton1, hackaton2);
            context.SaveChanges();

            Assert.AreEqual(context.Hackatons.Average(h => h.AverageHarmony), 3);
        }
    }
}
