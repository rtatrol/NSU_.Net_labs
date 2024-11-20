using System.Runtime.CompilerServices;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Moq;
namespace DreamTeam.Tests;

[TestFixture]
public class HackatonTests
{

    private ServiceProvider _serviceProvider;
    private Hackaton _hackaton;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var mockWishListStrategy = new Mock<IWishlistGenStrategy>();
        mockWishListStrategy.Setup(s => s.MakePrefer(It.IsAny<List<Junior>>(), It.IsAny<List<TeamLead>>()));

        var services = new ServiceCollection();
        services.AddTransient<Hackaton>();
        services.AddTransient<HRManager>();
        services.AddTransient<HRDirector>();
        services.AddTransient<IWishlistGenStrategy>(ServiceProvider => mockWishListStrategy.Object);
        services.AddTransient<ITeamsGenStrategy, FirstPriorityStrategy>();
        _serviceProvider = services.BuildServiceProvider();
        _hackaton = _serviceProvider.GetService<Hackaton>()!;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _serviceProvider?.Dispose();
    }

    [Test]
    public void CalculateHarmony_ShouldReturnDefinedScore()
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
        var harmony = (decimal)_hackaton.Run(juniors, teamLeads);
        Assert.That(harmony, Is.EqualTo(3));
    }
}
