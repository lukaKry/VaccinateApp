using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

namespace VaccinateRegistration.Data
{
    public record GetRegistrationResult(int Id, long Ssn, string FirstName, string LastName);

    public record StoreVaccination(int RegistrationId, DateTime Datetime);

    public class VaccinateDbContext : DbContext
    {
        public VaccinateDbContext(DbContextOptions<VaccinateDbContext> options) : base(options) { }

        // This class is NOT COMPLETE.
        // Todo: Complete the class according to the requirements

        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }




        /// <summary>
        /// Import registrations from JSON file
        /// </summary>
        /// <param name="registrationsFileName">Name of the file to import</param>
        /// <returns>
        /// Collection of all imported registrations
        /// </returns>
        public async Task<IEnumerable<Registration>> ImportRegistrations(string registrationsFileName)
        {
            using var transaction = await Database.BeginTransactionAsync();
            var fileContent = await File.ReadAllTextAsync("registrations.json");
            var registrations = JsonSerializer.Deserialize<Registration[]>(fileContent);
            await Registrations.AddRangeAsync(registrations);
            await SaveChangesAsync();
            await transaction.CommitAsync();
            return registrations;
        }

        /// <summary>
        /// Delete everything (registrations, vaccinations)
        /// </summary>
        public async Task DeleteEverything()
        {
            using var transaction = await Database.BeginTransactionAsync();
            await Database.ExecuteSqlRawAsync("DELETE FROM Vaccinations");
            await Database.ExecuteSqlRawAsync("DELETE FROM Registrations");
            await transaction.CommitAsync();
        }

        /// <summary>
        /// Get registration by social security number (SSN) and PIN
        /// </summary>
        /// <param name="ssn">Social Security Number</param>
        /// <param name="pin">PIN code</param>
        /// <returns>
        /// Registration result or null if no registration with given SSN and PIN was found.
        /// </returns>
        public async Task<GetRegistrationResult?> GetRegistration(long ssn, int pin)
        {
            var foundResult = await Registrations.FirstOrDefaultAsync(q => q.SocialSecurityNumber == ssn && q.PinCode == pin);
            if (foundResult is null) return null;

            return new GetRegistrationResult(
                foundResult.Id,
                foundResult.SocialSecurityNumber,
                foundResult.FirstName,
                foundResult.LastName);

        }

        /// <summary>
        /// Get available time slots on the given date
        /// </summary>
        /// <param name="date">Date (without time, i.e. time is 00:00:00)</param>
        /// <returns>
        /// Collection of all available time slots
        /// </returns>
        public async Task<IEnumerable<DateTime>> GetTimeslots(DateTime date)
        {
            var vaccinations = await Vaccinations.ToArrayAsync();
            var vaccinationsAtDay = vaccinations
                    .Where(q => q.VaccinationDate.Date == date.Date)
                    .Select(q => q.VaccinationDate);
            List<DateTime> timeSlots = new();
            DateTime startingHour = date.Date.AddHours(8);

            for (int i = 0; i < 180; i += 15)
            {
                DateTime slot = startingHour.AddMinutes(i);
                if (vaccinationsAtDay.Contains(slot))
                {
                    continue;
                }
                else
                {
                    timeSlots.Add(slot);
                }
            }
            return timeSlots.ToArray();
        }

        /// <summary>
        /// Store a vaccination
        /// </summary>
        /// <param name="vaccination">Vaccination to store</param>
        /// <returns>
        /// Stored vaccination after it has been written to the database.
        /// </returns>
        /// <remarks>
        /// If a vaccination with the given vaccination.RegistrationID already exists,
        /// overwrite it. Otherwise, insert a new vaccination.
        /// </remarks>
        public async Task<Vaccination> StoreVaccination(StoreVaccination vaccination)
        {
            var vac = await Vaccinations.FirstOrDefaultAsync(q => q.RegistrationId == vaccination.RegistrationId);
            if(vac is null)
            {
                Vaccination newVac = new Vaccination
                {
                    RegistrationId = vaccination.RegistrationId,
                    VaccinationDate = vaccination.Datetime
                };
                await Vaccinations.AddAsync(newVac);
                await SaveChangesAsync();
                return newVac;
            }
            else
            {
                vac.VaccinationDate = vaccination.Datetime;
                await SaveChangesAsync();
                return vac;
            }
        }
    }
}
