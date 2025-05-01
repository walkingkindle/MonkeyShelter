using Application.Shared.Models;
using CSharpFunctionalExtensions;
using Domain.Entities;

namespace MonkeyShelter.Test.Unit
{
    public class AdmissionTests
    {
        [Fact]

        public void Create_Admission_Should_Succeeed()
        {
            Result<Admission> admissionRequest = Admission.Create(1, DateTime.Today);
            
             Assert.True(admissionRequest.IsSuccess);
        }

        [Fact]
        public void Create_Admission_Should_FailInvalid_Data_Id()
        {
            Assert.True(Admission.Create(-6,DateTime.Today).IsFailure);
        }

        [Fact]
        public void Create_Admission_Should_Suceed_Datetime_NotToday()
        {
            Assert.True(Admission.Create(2,DateTime.Today.AddDays(-5)).IsSuccess);
        }
    }
}
