using Application.Contracts;
using Application.Contracts.Repositories;
using Application.Implementations;
using Application.Shared.Models;
using Moq;

namespace MonkeyShelter.Test.Unit
{
    public class CheckupServiceTests
    {
        private readonly ICheckupService _mockCheckupService;

        private readonly Mock<IAdmissionsRepository> _admissionsRepository;

        public CheckupServiceTests()
        {
            _admissionsRepository = new Mock<IAdmissionsRepository>();
            _mockCheckupService = new CheckupService(_admissionsRepository.Object);
        }

        [Fact]
        public async Task RetrieveMonkeysWithUpcomingVetCheckup_Suceeds()
        {
            var listNext30 = new List<MonkeyCheckupResponse>
            {
                new MonkeyCheckupResponse { Id = 22, Name = "Bobo" }
            };

            var listMoreThan30 = new List<MonkeyCheckupResponse>
            {
                new MonkeyCheckupResponse { Id = 23, Name = "Lola" }
            };

            _admissionsRepository.Setup(r => r.GetMonkeysByCheckupDate(DateTime.Today, DateTime.Today.AddDays(30)))
                          .ReturnsAsync(listNext30);

            _admissionsRepository.Setup(r => r.GetMonkeysByCheckupDate(DateTime.Today, DateTime.Today.AddMonths(9)))
                          .ReturnsAsync(listMoreThan30);

            var result = await _mockCheckupService.RetreiveMonkeysWithUpcomingVeteranaryCheckup();

            Assert.NotNull(result);
            Assert.Equal(listNext30, result.ScheduledInLessThan30Days);
            Assert.Equal(listMoreThan30, result.ScheduledInMoreThan30Days);

            _admissionsRepository.Verify(r => r.GetMonkeysByCheckupDate(DateTime.Today, DateTime.Today.AddDays(30)), Times.Once);
            _admissionsRepository.Verify(r => r.GetMonkeysByCheckupDate(DateTime.Today, DateTime.Today.AddMonths(9)), Times.Once);
        }
    }
}
