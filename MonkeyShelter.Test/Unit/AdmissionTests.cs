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
            Result<Admission> admissionRequest = Admission.CreateAdmission(1, DateTime.Today);
            
             Assert.True(admissionRequest.IsSuccess);
        }

        [Fact]
        public void Create_Admission_Should_FailInvalid_Data_Id()
        {
            Assert.True(Admission.CreateAdmission(-6,DateTime.Today).IsFailure);
        }

        [Fact]
        public void Create_Admission_Should_Fail_Invalid_Data_Date()
        {
            Assert.True(Admission.CreateAdmission(2,DateTime.Today.AddDays(-5)).IsFailure);
        }
    }
}
