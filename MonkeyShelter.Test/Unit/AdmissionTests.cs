using Domain.Entities;
using Domain.Models;

namespace MonkeyShelter.Test.Unit
{
    public class AdmissionTests
    {
        [Fact]

        public void Create_Admission_Should_Succeeed()
        {
            AdmissionRequest admissionRequest = new AdmissionRequest
            {
                AdmittanceDate = DateTime.Today,
                MonkeyId = 52
            };
            Assert.True(Admission.CreateAdmission(admissionRequest).IsSuccess);
        }

        [Fact]
        public void Create_Admission_Should_FailInvalid_Data_Id()
        {
            AdmissionRequest admissionRequest = new AdmissionRequest
            {
                AdmittanceDate = DateTime.Today,
                MonkeyId = -6
            };
            Assert.True(Admission.CreateAdmission(admissionRequest).IsFailure);
            
        }

        [Fact]
        public void Create_Admission_Should_Fail_Invalid_Data_Date()
        {
            AdmissionRequest admissionRequest = new AdmissionRequest
            {
                AdmittanceDate = DateTime.Today.AddDays(-30),
                MonkeyId = 52
            };
            Assert.True(Admission.CreateAdmission(admissionRequest).IsSuccess);
        }
    }
}
