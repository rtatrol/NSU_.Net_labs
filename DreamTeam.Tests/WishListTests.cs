using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
namespace DreamTeam.Tests;

[TestFixture]
public class WishlistGenStrategyTests
{
    private IWishlistGenStrategy? _wishlistGenStrategy;
    private ServiceProvider? _serviceProvider;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var services = new ServiceCollection();
        services.AddTransient<IWishlistGenStrategy, RandomGenStrategy>();

        _serviceProvider = services.BuildServiceProvider();
        _wishlistGenStrategy = _serviceProvider.GetService<IWishlistGenStrategy>()!;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _serviceProvider?.Dispose();
    }

    [Test]
    public void Wishlist_Size_ShouldMatchParticipantCount()
    {
        var juniors = new List<Junior> { new("1;Юдин Адам"), new("2;Яшина Яна"), new("3;Никитина Вероника") };
        var teamLeads = new List<TeamLead> { new("1;Филиппова Ульяна"), new("2;Николаев Григорий"), new("3;Андреева Вероника") };

        _wishlistGenStrategy!.MakePrefer(juniors, teamLeads);

        Assert.That(juniors, Has.All.Matches<Junior>(j => j.preferences.Count == teamLeads.Count));
        Assert.That(teamLeads, Has.All.Matches<TeamLead>(t => t.preferences.Count == juniors.Count));
    }

    [Test]
    public void Wishlist_ShouldContainSpecificParticipant()
    {
        var juniors = new List<Junior> { new("1;Юдин Адам"), new("2;Яшина Яна"), new("3;Никитина Вероника") };
        var teamLeads = new List<TeamLead> { new("1;Филиппова Ульяна"), new("2;Николаев Григорий"), new("3;Андреева Вероника") };

        _wishlistGenStrategy!.MakePrefer(juniors, teamLeads);

        var specificLead = teamLeads[1];
        var specificJunior = juniors[2];

        Assert.That(juniors, Has.All.Matches<Junior>(j => j.preferences.Contains(specificLead)));
        Assert.That(teamLeads, Has.All.Matches<TeamLead>(t => t.preferences.Contains(specificJunior)));
    }
}
