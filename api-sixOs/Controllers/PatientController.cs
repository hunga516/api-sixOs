using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_sixOs.Models;
using api_sixOs.Data;

namespace api_sixOs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ApiContext _context;

        public PatientController(ApiContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public JsonResult Create([FromForm] Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();

            return new JsonResult(Ok(patient));
        }

        //Read
        [HttpGet]
        public JsonResult Read()
        {
            var patients = _context.Patients.ToList();
            return new JsonResult(Ok(patients));
        }

        //Update
        [HttpPut("{id}")]
        public JsonResult Update(int id, [FromBody] Patient patient)
        {
            var existingPatient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (existingPatient == null)
            {
                return new JsonResult(NotFound($"Patient with Id {id} not found"));
            }

            // Update properties
            existingPatient.Name = patient.Name ?? existingPatient.Name;
            
            existingPatient.Address = patient.Address ?? existingPatient.Address;
            
            existingPatient.Dob = patient.Dob != default ? patient.Dob : existingPatient.Dob;

            _context.SaveChanges();

            return new JsonResult(Ok(existingPatient));
        }

        //Delete
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return new JsonResult(NotFound($"Patient with Id {id} not found"));
            }

            _context.Patients.Remove(patient);
            _context.SaveChanges();

            return new JsonResult(Ok($"Patient with Id {id} deleted successfully"));
        }
    }
}
