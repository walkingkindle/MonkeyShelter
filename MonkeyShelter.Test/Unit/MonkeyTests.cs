using Application.Contracts;
using Application.Implementations;
using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Contracts;
using Moq;

namespace MonkeyShelter.Test.Unit
{
    public class MonkeyTests
    {

        [Fact]
        public void AddMonkey_Fails_Invalid_Data_Weight()
        {
            var request = new MonkeyEntryRequest
            {
                Species = MonkeySpecies.Capuchin,
                Name = "Bobo",
                Weight = -3.2
            };
            var monkeyResult = Monkey.CreateMonkey(Maybe.From(request));

            Assert.True(monkeyResult.IsFailure);
        }

        [Fact]
        public void AddMonkey_Fails_Invalid_Data_Species()
        {

            var invalidSpecies = (MonkeySpecies)999; 
            var request = new MonkeyEntryRequest
            {
                Species = invalidSpecies,
                Name = "Bobo",
                Weight = -3.2
            };
            var monkeyResult = Monkey.CreateMonkey(Maybe.From(request));

            Assert.True(monkeyResult.IsFailure);
        }

        [Fact]
        public void AddMonkey_Fails_Invalid_Data_Name()
        {
            var request = new MonkeyEntryRequest
            {
                Species = MonkeySpecies.Capuchin,
                Name = "",
                Weight = 3.2
            };
            var monkeyResult = Monkey.CreateMonkey(Maybe.From(request));

            Assert.True(monkeyResult.IsFailure);
        }

    }

}