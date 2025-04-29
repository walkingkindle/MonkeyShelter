using Application.Contracts;
using Application.Implementations;
using Domain.Entities;
using Infrastructure.Contracts;
using Moq;

namespace MonkeyShelter.Test.Unit
{
    public class DepartureTrackerTests
    {
        private readonly DepartureService _departureService;
        private readonly Mock<IAdmissionsRepository> _admissionsRepository;
        private readonly Mock<IDeparturesRepository> _departuresRepository;

        public DepartureTrackerTests()
        {
            _admissionsRepository = new Mock<IAdmissionsRepository>();
            _departuresRepository = new Mock<IDeparturesRepository>();
            _departureService = new DepartureService(_departuresRepository.Object, _admissionsRepository.Object);
        }



        [Fact]
        public void Can_Monkey_Depart_Returns_True()
        {
            int departureAmount = 3;

            int amountOfSingularSpecies = 1;

            int todayAdmittanceAmount = 0;

            _departuresRepository.Setup(r => r.GetTodayDeparturesAmount())
               .Returns(departureAmount);

            _admissionsRepository.Setup(r => r.GetMonkeysAmountBySpecies(MonkeySpecies.Capuchin))
                .Returns(amountOfSingularSpecies);
             
            _admissionsRepository.Setup(r => r.GetTodayAdmittanceAmount())
            .Returns(todayAdmittanceAmount);

            Assert.True(_departureService.CanMonkeyDepart(MonkeySpecies.Capuchin));
        }

        [Fact]
        public void Can_Monkey_Depart_Returns_True_2()
        {
            int departureAmount = 3;

            int amountOfSingularSpecies = 1;

            int todayAdmittanceAmount = 0;


            _departuresRepository.Setup(r => r.GetTodayDeparturesAmount())
               .Returns(departureAmount);

            _admissionsRepository.Setup(r => r.GetMonkeysAmountBySpecies(MonkeySpecies.Capuchin))
                .Returns(amountOfSingularSpecies);

            _admissionsRepository.Setup(r => r.GetTodayAdmittanceAmount())
                .Returns(todayAdmittanceAmount);

            Assert.True(_departureService.CanMonkeyDepart(MonkeySpecies.Capuchin));
        }

        [Fact]
        public void Can_Monkey_Depart_Returns_True_3()
        {
            int departureAmount = 3;

            int amountOfSingularSpecies = 1;

            int todayAdmittanceAmount = 2;


            _departuresRepository.Setup(r => r.GetTodayDeparturesAmount())
               .Returns(departureAmount);

            _admissionsRepository.Setup(r => r.GetMonkeysAmountBySpecies(MonkeySpecies.Capuchin))
                .Returns(amountOfSingularSpecies);

            _admissionsRepository.Setup(r => r.GetTodayAdmittanceAmount())
                .Returns(todayAdmittanceAmount);

            Assert.True(_departureService.CanMonkeyDepart(MonkeySpecies.Capuchin));
        }

        [Fact]
        public void Can_Monkey_Depart_Returns_True_4()
        {
            int departureAmount = 3;

            int amountOfSingularSpecies = 22;

            int todayAdmittanceAmount = 2;


            _departuresRepository.Setup(r => r.GetTodayDeparturesAmount())
               .Returns(departureAmount);

            _admissionsRepository.Setup(r => r.GetMonkeysAmountBySpecies(MonkeySpecies.Capuchin))
                .Returns(amountOfSingularSpecies);

            _admissionsRepository.Setup(r => r.GetTodayAdmittanceAmount())
                .Returns(todayAdmittanceAmount);

            Assert.True(_departureService.CanMonkeyDepart(MonkeySpecies.Capuchin));
        }


        [Fact]
        public void Can_Monkey_Depart_Returns_True_5()
        {
            int departureAmount = 5;

            int amountOfSingularSpecies = 22;

            int todayAdmittanceAmount = 2;


            _departuresRepository.Setup(r => r.GetTodayDeparturesAmount())
               .Returns(departureAmount);

            _admissionsRepository.Setup(r => r.GetMonkeysAmountBySpecies(MonkeySpecies.Capuchin))
                .Returns(amountOfSingularSpecies);

            _admissionsRepository.Setup(r => r.GetTodayAdmittanceAmount())
                .Returns(todayAdmittanceAmount);

            Assert.True(_departureService.CanMonkeyDepart(MonkeySpecies.Capuchin));
        }

        [Fact]
        public void Can_Monkey_Depart_Returns_False()
        {
            int departureAmount = 6;

            int amountOfSingularSpecies = 22;

            int todayAdmittanceAmount = 2;


            _departuresRepository.Setup(r => r.GetTodayDeparturesAmount())
               .Returns(departureAmount);

            _admissionsRepository.Setup(r => r.GetMonkeysAmountBySpecies(MonkeySpecies.Capuchin))
                .Returns(amountOfSingularSpecies);

            _admissionsRepository.Setup(r => r.GetTodayAdmittanceAmount())
                .Returns(todayAdmittanceAmount);

            Assert.False(_departureService.CanMonkeyDepart(MonkeySpecies.Capuchin));
        }
        
        [Fact]
        public void Can_Monkey_Depart_Returns_False_2()
        {
            int departureAmount = 1;

            int amountOfSingularSpecies = 22;

            int todayAdmittanceAmount = 3;


            _departuresRepository.Setup(r => r.GetTodayDeparturesAmount())
               .Returns(departureAmount);

            _admissionsRepository.Setup(r => r.GetMonkeysAmountBySpecies(MonkeySpecies.Capuchin))
                .Returns(amountOfSingularSpecies);

            _admissionsRepository.Setup(r => r.GetTodayAdmittanceAmount())
                .Returns(todayAdmittanceAmount);

            Assert.False(_departureService.CanMonkeyDepart(MonkeySpecies.Capuchin));
        }


        [Fact]
        public void Can_Monkey_Depart_Returns_False_3()
        {
            int departureAmount = 1;

            int amountOfSingularSpecies = 0;

            int todayAdmittanceAmount = 1;


            _departuresRepository.Setup(r => r.GetTodayDeparturesAmount())
               .Returns(departureAmount);

            _admissionsRepository.Setup(r => r.GetMonkeysAmountBySpecies(MonkeySpecies.Capuchin))
                .Returns(amountOfSingularSpecies);

            _admissionsRepository.Setup(r => r.GetTodayAdmittanceAmount())
                .Returns(todayAdmittanceAmount);

            Assert.False(_departureService.CanMonkeyDepart(MonkeySpecies.Capuchin));
        }


        [Fact]
        public void Can_Monkey_Depart_Returns_False_4()
        {
            int departureAmount = 0;

            int amountOfSingularSpecies = 0;

            int todayAdmittanceAmount = 0;


            _departuresRepository.Setup(r => r.GetTodayDeparturesAmount())
               .Returns(departureAmount);

            _admissionsRepository.Setup(r => r.GetMonkeysAmountBySpecies(MonkeySpecies.Capuchin))
                .Returns(amountOfSingularSpecies);

            _admissionsRepository.Setup(r => r.GetTodayAdmittanceAmount())
                .Returns(todayAdmittanceAmount);

            Assert.False(_departureService.CanMonkeyDepart(MonkeySpecies.Capuchin));
        }

        [Fact]
        public async Task Depart_Calls_DepartureRepository_With_Correct_Id()
        {
            var monkeyId = 42;

            var result = await _departureService.Depart(monkeyId);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Depart_Calls_DepartureRepository_With_InCorrect_Id()
        {
            var monkeyId = -42;

            var result = await _departureService.Depart(monkeyId);

            Assert.True(result.IsFailure);
        }


    }
}
