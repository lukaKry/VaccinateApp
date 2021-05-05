using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VaccinateRegistration.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VaccinateRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationsController : ControllerBase
    {
        private readonly VaccinateDbContext context;
        public VaccinationsController(VaccinateDbContext context) => this.context = context;

        // This class is NOT COMPLETE.
        // Todo: Complete the class according to the requirements

        [HttpPost]
        public async Task<Vaccination> StoreVaccination([FromBody] StoreVaccination vaccination)
        {
            await context.StoreVaccination(vaccination);
            return await context.Vaccinations.FirstOrDefaultAsync(q => q.RegistrationId == vaccination.RegistrationId);
        }
    }
}
