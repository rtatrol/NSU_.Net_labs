using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Moq;
namespace DreamTeam.Tests;

[TestFixture]
public class HRManagerTests
{
    private ServiceProvider? _serviceProvider;
    private IWishlistGenStrategy _wishlistGenStrategy;

    private HRManager _hrManager;


    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var services = new ServiceCollection();
        services.AddTransient<ITeamsGenStrategy, FirstPriorityStrategy>();
        services.AddTransient<IWishlistGenStrategy, RandomGenStrategy>();
        services.AddTransient<HRManager>();
        _serviceProvider = services.BuildServiceProvider();
        _hrManager = _serviceProvider.GetService<HRManager>()!;
        _wishlistGenStrategy = _serviceProvider.GetService<IWishlistGenStrategy>()!;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _serviceProvider?.Dispose();
    }

    [Test]
    public void StrategyRun_ShouldReturnCorrectNumberOfTeams()
    {
        var juniors = new List<Junior> { new("1;Юдин Адам"), new("2;Яшина Яна"), new("3;Никитина Вероника") };
        var teamLeads = new List<TeamLead> { new("1;Филиппова Ульяна"), new("2;Николаев Григорий"), new("3;Андреева Вероника") };
        _wishlistGenStrategy.MakePrefer(juniors, teamLeads);
        var teams = _hrManager.StrategyRun(juniors, teamLeads);

        Assert.That(teams.Count, Is.EqualTo(juniors.Count));
    }

    [Test]
    public void StrategyRun_ShouldReturnExpectedDistribution()
    {

        var juniors = new List<Junior> { new("1;Юдин Адам"), new("2;Яшина Яна"), new("3;Никитина Вероника") };
        var teamLeads = new List<TeamLead> { new("1;Филиппова Ульяна"), new("2;Николаев Григорий"), new("3;Андреева Вероника") };

        int pairCount = juniors.Count;
        for (int i = 0; i < pairCount; i++)
        {
            for (int j = 0; j < pairCount; j++)
            {
                juniors[j].preferences.Add(teamLeads[(j + i) % pairCount]);
                teamLeads[j].preferences.Add(juniors[(j + i) % pairCount]);
            }
        }

        var teams = _hrManager.StrategyRun(juniors, teamLeads);
        var expectedTeams = new List<(Junior, TeamLead)>
        {
            (juniors[0], teamLeads[0]),
            (juniors[1], teamLeads[1]),
            (juniors[2], teamLeads[2])
        };

        Assert.That(teams, Is.EquivalentTo(expectedTeams).Using<Tuple<Junior, TeamLead>>(
            (x, y) => x.Item1 == y.Item1 && x.Item2 == y.Item2));
    }

    [Test]
    public void StrategyRun_ShouldCallStrategyOnce()
    {
        var mockStrategy = new Mock<ITeamsGenStrategy>();
        mockStrategy
            .Setup(s => s.MakeTeams(It.IsAny<List<Junior>>(), It.IsAny<List<TeamLead>>()))
            .Returns(new List<(Junior, TeamLead)>());

        var manager = new HRManager(mockStrategy.Object);
        var juniors = new List<Junior>();
        var teamLeads = new List<TeamLead>();

        manager.StrategyRun(juniors, teamLeads);

        mockStrategy.Verify(s => s.MakeTeams(It.IsAny<List<Junior>>(), It.IsAny<List<TeamLead>>()), Times.Once);
    }

}
