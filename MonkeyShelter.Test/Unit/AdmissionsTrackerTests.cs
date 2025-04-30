using Application.Contracts;
using Application.Contracts.Business;
using Application.Contracts.Repositories;
using Application.Implementations;
using Infrastructure.Implementations;
using Moq;

namespace MonkeyShelter.Test.Unit
{
    public class AdmissionsTrackerTests
    {
        private readonly Mock<AdmissionsRepository> _admissionsRepository;

        private readonly IAdmissionTracker _tracker;

        public AdmissionsTrackerTests()
        {
            _admissionsRepository = new Mock<AdmissionsRepository>();
            _tracker = new AdmissionsTracker(_admissionsRepository.Object);
        }

        [Fact]
        public async Task Admit_Suceeds()
        {
            int monkeyId = 6;

            var result = await _tracker.Admit(monkeyId);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Admit_Fails()
        {
            int monkeyId = -6;

            var result = await _tracker.Admit(monkeyId);

            Assert.True(result.IsFailure);
        }

    }
}
