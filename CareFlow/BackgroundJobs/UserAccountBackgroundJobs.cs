using CareFlow.BackgroundJobs.Interfaces;
using CareFlow.Data;
using CareFlow.Models;
using CareFlow.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace CareFlow.BackgroundJobs;

public class UserAccountBackgroundJobs(
    ApplicationDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    ILogger<UserAccountBackgroundJobs> logger) : IUserAccountBackgroundJobs
{
    public async Task CreateDoctorUserAccountAsync(int doctorId)
    {
		try
		{
            var doctor = await dbContext.Doctors.FindAsync(doctorId);
            if (doctor == null)
            {
                logger.LogError("Doctor with ID {doctorId} not found.", doctorId);
                return;
            }
            if (doctor.HasUserAccount)
            {
                logger.LogError("Doctor with ID {doctorId} already has a user account.", doctorId);
                return;
            }

            var user = await userManager.FindByEmailAsync(doctor.Email);
            if (user != null)
            {
                logger.LogError("User with email {email} already exists.", user.Email);
                return;
            }

            user = new ApplicationUser
            {
                UserName = doctor.Email,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                EmailConfirmed = false
            };

            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                logger.LogError("User creation failed", result.Errors.ToArray());
                return;
            }

            await userManager.AddToRoleAsync(user, "Doctor");

            doctor.UserId = user.Id;
            await dbContext.SaveChangesAsync();

            // TODO: Send welcome mail
        }
		catch (Exception ex)
		{
            logger.LogError(ex, "User account creation for doctor with ID {doctorId} failed", doctorId);
			throw;
		}
    }

    public async Task CreatePatientUserAccountAsync(int patientId)
    {
        try
        {
            var patient = await dbContext.Patients.FindAsync(patientId);
            if (patient == null)
            {
                logger.LogError("Patient with ID {patientId} not found.", patientId);
                return;
            }
            if (patient.HasUserAccount)
            {
                logger.LogError("Patient with ID {patientId} already has a user account.", patientId);
                return;
            }

            var user = await userManager.FindByEmailAsync(patient.Email);
            if (user != null)
            {
                logger.LogError("User with email {email} already exists.", user.Email);
                return;
            }

            user = new ApplicationUser
            {
                UserName = patient.Email,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                EmailConfirmed = false
            };

            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                logger.LogError("User creation failed", result.Errors.ToArray());
                return;
            }

            await userManager.AddToRoleAsync(user, "Patient");

            patient.UserId = user.Id;
            await dbContext.SaveChangesAsync();

            // TODO: Send welcome mail
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "User account creation for patient with ID {patientId} failed", patientId);
            throw;
        }
    }
}
