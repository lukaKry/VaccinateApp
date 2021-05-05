using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaccinateRegistration.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VaccinateRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        // This class is NOT COMPLETE.
        // Todo: Complete the class according to the requirements
        private readonly VaccinateDbContext context;
        public RegistrationsController(VaccinateDbContext context) => this.context = context;



        [HttpGet]
        public async Task<GetRegistrationResult?> GetRegistration([FromQuery] long ssn, [FromQuery] int pin)
        {
            return await context.GetRegistration(ssn, pin);
        }

        [HttpGet]
        [Route("timeSlots")]
        public async Task<IEnumerable<DateTime>> GetTimeslots([FromQuery] DateTime date)
        {
            return await context.GetTimeslots(date);
        }
    }
}
