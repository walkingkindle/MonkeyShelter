using Application.Contracts;
using Application.Implementations;
using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace MonkeyShelter.Test.Unit
{
    public class MonkeyServiceTests
    {
        private readonly IMonkeyService _service;
        private readonly Mock<IAdmissionTracker> _admissionTracker;
        private readonly Mock<IMonkeyRepository> _monkeyRepository;
        private readonly Mock<IDepartureService> _departureService;
        private readonly Mock<IMemoryCache> _mockMemoryCache;

        public MonkeyServiceTests()
        {

            _admissionTracker = new Mock<IAdmissionTracker>();
            _monkeyRepository = new Mock<IMonkeyRepository>();
            _departureService = new Mock<IDepartureService>();
            _mockMemoryCache = new Mock<IMemoryCache>();
            _service = new MonkeyService(_admissionTracker.Object, _monkeyRepository.Object, _departureService.Object, _mockMemoryCache.Object);
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
            Assert.True(admittedMonkeyId.Value >= 0);
        }


        [Fact]
        public async Task DepartMonkey_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var request = new MonkeyDepartureRequest { MonkeyId = 1 };
            var monkey = Monkey.CreateMonkey(new MonkeyEntryRequest { Name = "Bobo", Species = MonkeySpecies.Capuchin, Weight = 22.2 }).Value;

            _monkeyRepository.Setup(r => r.GetMonkeyById(request.MonkeyId))
                             .ReturnsAsync(Result.Success(monkey));

            _departureService.Setup(s => s.CanMonkeyDepart(monkey.Species))
                             .Returns(true);

            _monkeyRepository.Setup(r => r.RemoveMonkeyFromShelter(monkey))
                             .Returns(Task.CompletedTask);

            _departureService.Setup(s => s.Depart(request.MonkeyId))
                             .ReturnsAsync(Result.Success());

            // Act
            var result = await _service.DepartMonkey(Maybe.From(request));

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DepartMonkey_MonkeyNotFound_ReturnsFailure()
        {
            // Arrange
            var request = new MonkeyDepartureRequest { MonkeyId = 1 };

            _monkeyRepository.Setup(r => r.GetMonkeyById(request.MonkeyId))
                             .ReturnsAsync(Result.Failure<Monkey>("Monkey not found"));

            // Act
            var result = await _service.DepartMonkey(Maybe.From(request));

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Monkey not found", result.Error);
        }


        [Fact]
        public async Task GetMonkeyBySpecies_ValidSpecies_ReturnsMonkeys()
        {
            // Arrange
            var species = MonkeySpecies.Capuchin;
            var expectedMonkeys = new List<MonkeyReportResponse>
            {
                new MonkeyReportResponse { Name = "Bobo", Species = species },
                new MonkeyReportResponse { Name = "Lulu", Species = species }
            };

            _monkeyRepository.Setup(r => r.GetMonkeysBySpecies(species))
                             .ReturnsAsync(expectedMonkeys);

            // Act
            var result = await _service.GetMonkeyBySpecies(Maybe.From(species));

            // Assert
            Assert.Equal(expectedMonkeys.Count, result.Value.Count);
            Assert.All(result.Value, m => Assert.Equal(species, m.Species));
            _monkeyRepository.Verify(r => r.GetMonkeysBySpecies(species), Times.Once);
        }


        [Fact]
        public async Task GetMonkeysByDate_ValidRange_ReturnsMonkeys()
        {
            var from = new DateTime(2024, 1, 1);
            var to = new DateTime(2024, 12, 31);

            var expectedMonkeys = new List<MonkeyReportResponse>
            {
                new MonkeyReportResponse { Name = "Bobo" },
                new MonkeyReportResponse { Name = "Mimi" }
            };

            _monkeyRepository.Setup(r => r.GetMonkeysByDate(from, to))
                             .ReturnsAsync(expectedMonkeys);

            var result = await _service.GetMonkeysByDate(new MonkeyDateRequest { DateFrom = from, DateTo = to});

            Assert.Equal(expectedMonkeys.Count, result.Value.Count);
            _monkeyRepository.Verify(r => r.GetMonkeysByDate(from, to), Times.Once);
        }

        [Fact]
        public async Task GetMonkeyBySpecies_NoMonkeysFound_ReturnsEmptyList()
        {
            var species = MonkeySpecies.Mandrill;

            _monkeyRepository.Setup(r => r.GetMonkeysBySpecies(species))
                             .ReturnsAsync(new List<MonkeyReportResponse>());

            var result = await _service.GetMonkeyBySpecies(Maybe.From(species));

            Assert.Empty(result.Value);
            _monkeyRepository.Verify(r => r.GetMonkeysBySpecies(species), Times.Once);
        }


        [Fact]
        public async Task GetMonkeysByDate_NoMonkeysFound_ReturnsEmptyList()
        {
            // Arrange
            var from = new DateTime(1999, 1, 1);
            var to = new DateTime(1999, 12, 31);

            _monkeyRepository.Setup(r => r.GetMonkeysByDate(from, to))
                             .ReturnsAsync(new List<MonkeyReportResponse>());

            // Act
            var result = await _service.GetMonkeysByDate(new MonkeyDateRequest { DateFrom = from, DateTo = to });

            // Assert
            Assert.Empty(result.Value);
            _monkeyRepository.Verify(r => r.GetMonkeysByDate(from, to), Times.Once);
        }

        [Fact]
        public async Task GetMonkeyBySpecies_InvalidInput_Results_Failure()
        {
            // Arrange
            var species = Maybe<MonkeySpecies>.None;

            var call = await _service.GetMonkeyBySpecies(species);
            // Act & Assert
            Assert.True(call.IsFailure);

            _monkeyRepository.Verify(r => r.GetMonkeysBySpecies(It.IsAny<MonkeySpecies>()), Times.Never);
        }

        [Fact]
        public async Task GetMonkeysByDate_InvalidDateRange_Failure()
{
        // Arrange
            var from = new DateTime(2025, 12, 1);
            var to = new DateTime(2025, 1, 1);

            var act =  await _service.GetMonkeysByDate(new MonkeyDateRequest { DateFrom = from, DateTo = to });

            Assert.True(act.IsFailure);

            _monkeyRepository.Verify(r => r.GetMonkeysByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Never);
        }


        [Fact]
        public async Task UpdateMonkeyWeight_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var request = new MonkeyWeightRequest
            {
                MonkeyId = 1,
                NewMonkeyWeight = 15.5
            };

            _monkeyRepository.Setup(r => r.UpdateMonkey(request))
                             .Returns(Task.FromResult(Result.Success()));

            // Act
            var result = await _service.UpdateMonkeyWeight(Maybe.From(request));

            // Assert
            Assert.True(result.IsSuccess);
            _monkeyRepository.Verify(r => r.UpdateMonkey(request), Times.Once);
        }

        public async Task UpdateMonkeyWeight_InvalidWeight_ReturnsFailure()
        {
            // Arrange
            var invalidWeight = -2.3;
            var request = new MonkeyWeightRequest
            {
                MonkeyId = 1,
                NewMonkeyWeight = invalidWeight
            };

            // Act
            var result = await _service.UpdateMonkeyWeight(Maybe.From(request));

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Monkey must have a valid weight", result.Error);
            _monkeyRepository.Verify(r => r.UpdateMonkey(It.IsAny<MonkeyWeightRequest>()), Times.Never);
        }











    }

}
