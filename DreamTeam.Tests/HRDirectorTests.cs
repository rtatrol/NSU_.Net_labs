using System.Runtime.CompilerServices;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
namespace DreamTeam.Tests;

[TestFixture]
public class HRDirectorTests
{
    private HRDirector _hrDirector;

    [SetUp]
    public void SetUp()
    {
        _hrDirector = new HRDirector();
    }

    [TestCaseSource(nameof(TestCases_ForIdenticalScores))]
    public void CalculateTotalValue_ShouldReturnCorrectForIdenticalScores(List<double> values, double expected)
    {
        var harmony = _hrDirector.CalculateTotalValue(values);
        Assert.That(harmony, Is.EqualTo(expected));
    }
    private static IEnumerable<object[]> TestCases_ForIdenticalScores => new[]
    {
        new object[] { new List<double> {1, 1, 1, 1}, 1},
        new object[] { new List<double> {3, 3, 3, 3}, 3},
        new object[] { new List<double> {12, 12, 12, 12}, 12}
    };


    [TestCaseSource(nameof(TestCases_ForCorrectValue))]
    public void CalculateTotalValue_ShouldReturnCorrectValue(List<double> values, double expected)
    {
        var harmony = _hrDirector.CalculateTotalValue(values);
        Assert.That(harmony, Is.EqualTo(expected));
    }
    private static IEnumerable<object[]> TestCases_ForCorrectValue => new[]
    {
        new object[] {new List<double>{2, 6}, 3},
        new object[] {new List<double>{2, 1, 2, 1}, 4 / (double) 3},
        new object[] {new List<double>{6, 3, 2, 6, 3, 2}, 3}
    };

    [Test]
    public void CalculateHarmony_ShouldReturnDefinedValue()
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
        var Teams = new List<(Junior, TeamLead)>
        {
            (juniors[0], teamLeads[0]),
            (juniors[1], teamLeads[1]),
            (juniors[2], teamLeads[2])
        };
        var harmony = _hrDirector.CalculateHarmony(Teams);
        Assert.That((decimal)harmony, Is.EqualTo(3));
    }
}
