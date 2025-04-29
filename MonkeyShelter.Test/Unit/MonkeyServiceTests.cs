using Application.Contracts;
using Application.Implementations;
using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Contracts;
using Moq;

namespace MonkeyShelter.Test.Unit
{
    public class MonkeyServiceTests
    {
        private readonly IMonkeyService _service;
        private readonly Mock<IAdmissionTracker> _admissionTracker;
        private readonly Mock<IMonkeyRepository> _monkeyRepository;
        private readonly Mock<IDepartureService> _departureService;

        public MonkeyServiceTests()
        {

            _admissionTracker = new Mock<IAdmissionTracker>();
            _monkeyRepository = new Mock<IMonkeyRepository>();
            _departureService = new Mock<IDepartureService>();
            _service = new MonkeyService(_admissionTracker.Object, _monkeyRepository.Object, _departureService.Object);
        }


        [Fact]
        public async Task AddMonkey_ValidRequest_AddsMonkey()
        {
            var request = new MonkeyEntryRequest
            {
                Species = MonkeySpecies.Capuchin,
                Name = "Bobo",
                Weight = 12.5
            };
            int newMonkeyId = 0;

            _admissionTracker.Setup(p => p.CanMonkeyBeAdmitted()).Returns(true);

            var result = await _service.AddMonkey(request);

            var monkey = Monkey.CreateMonkey(request);


            Assert.True(monkey.IsSuccess);

            Assert.True(result.IsSuccess);
            var monkeySaveResult = _monkeyRepository.Setup(repo => repo.AddMonkeyToShelter(monkey.Value))
                .Callback<Monkey>(m => newMonkeyId = m.Id)
                .Returns(Task.FromResult(Result.Success(newMonkeyId)));

            _monkeyRepository.Verify(repo => repo.AddMonkeyToShelter(It.Is<Monkey>(m =>
             m.Name == request.Name &&
             m.Species == request.Species &&
             m.Weight == request.Weight)));
        }


    [Fact]
    public async Task AddMonkey_ValidRequest_CreatesAndAdmitsMonkey()
    {
        // Arrange
        var request = new MonkeyEntryRequest
        {
            Species = MonkeySpecies.Capuchin,
            Name = "Bobo",
            Weight = 12.3
        };

        var monkey = Monkey.CreateMonkey(Maybe.From(request)).Value;



          _admissionTracker.Setup(t => t.CanMonkeyBeAdmitted())
                            .Returns(true);

           Maybe<int> admittedMonkeyId = -1;

          _monkeyRepository.Setup(r => r.AddMonkeyToShelter(It.IsAny<Monkey>()))
                            .ReturnsAsync(monkey.Id);
    
            _admissionTracker
            .Setup(t => t.Admit(It.IsAny<Maybe<int>>()))
            .Callback<Maybe<int>>(id => admittedMonkeyId = id)
            .Returns(Task.FromResult(Result.Success()));

        // Act
        var result = await _service.AddMonkey(Maybe.From(request));

        // Assert
        Assert.True(result.IsSuccess);

        _admissionTracker.Verify(t => t.CanMonkeyBeAdmitted(), Times.Once);
        _monkeyRepository.Verify(r => r.AddMonkeyToShelter(It.Is<Monkey>(m =>
            m.Name == request.Name &&
            m.Species == request.Species &&
            m.Weight == request.Weight
        )), Times.Once);

            _admissionTracker.Verify(t => t.Admit(admittedMonkeyId), Times.Once);
            Assert.True(admittedMonkeyId.Value > 0);
        }



    }

}
