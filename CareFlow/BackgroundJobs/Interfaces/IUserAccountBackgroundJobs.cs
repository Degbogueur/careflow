namespace CareFlow.BackgroundJobs.Interfaces;

public interface IUserAccountBackgroundJobs
{
    Task CreateDoctorUserAccountAsync(int doctorId);
    Task CreatePatientUserAccountAsync(int patientId);
}
