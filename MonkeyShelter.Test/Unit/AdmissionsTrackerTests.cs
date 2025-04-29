using Application.Contracts;
using Application.Implementations;
using Domain.Entities;
using Infrastructure.Contracts;
using Moq;

namespace MonkeyShelter.Test.Unit
{
    public class AdmissionsTrackerTests
    {
        private readonly Mock<IAdmissionsRepository> _admissionsRepository;

        private readonly Mock<IDeparturesRepository> _departuresRepository;

        private readonly IAdmissionTracker _tracker;

        public AdmissionsTrackerTests()
        {
            _admissionsRepository = new Mock<IAdmissionsRepository>();
            _departuresRepository = new Mock<IDeparturesRepository>();
            _tracker = new AdmissionsTracker(_admissionsRepository.Object, _departuresRepository.Object);
        }


        [Fact]
        public void Can_Monkey_Be_Admitted_Returns_True()
        {
            int admittanceAmount = 3;

            _admissionsRepository.Setup(r => r.GetTodayAdmittanceAmount())
                .Returns(admittanceAmount);

             Assert.True(_tracker.CanMonkeyBeAdmitted());
        }

        [Fact]
        public void Can_Monkey_Be_Admitted_Returns_False()
        {
            int admittanceAmount = 8;

            _admissionsRepository.Setup(r => r.GetTodayAdmittanceAmount())
                .Returns(admittanceAmount);

            Assert.False(_tracker.CanMonkeyBeAdmitted());
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
