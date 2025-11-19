using FluentAssertions;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Tests.Entities;

public class PersonTests
{
    [Fact]
    public void Constructor_WithValidName_CreatesPerson()
    {
        var person = new Person("John");

        person.Name.Should().Be("John");
        person.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsException()
    {
        Action act = () => new Person("");

        act.Should().Throw<DomainException>()
            .WithMessage("Name is required.");
    }

    [Fact]
    public void UpdateName_WithValidName_Updates()
    {
        var person = new Person("Old");

        person.UpdateName("New");

        person.Name.Should().Be("New");
    }

    [Fact]
    public void UpdateName_WithEmptyName_ThrowsException()
    {
        var person = new Person("Valid");

        Action act = () => person.UpdateName("");

        act.Should().Throw<DomainException>()
            .WithMessage("Name is required.");
    }
}
