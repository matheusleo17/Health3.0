using Care3._0.Data;
using Care3._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Care3._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        //private readonly Medicament _medicament;
        
        public MedicamentController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public ActionResult<List<Medicament>> GetMedicament()
        {
            var medicaments = _appDbContext.Medicaments.ToList();
            return Ok(medicaments);
        }

        [HttpPost]
        public ActionResult<Medicament> InsertMedicament(Medicament medicament)
        {
            _appDbContext.Add(medicament);
            _appDbContext.SaveChanges(); 
            return Ok(medicament);
        }
        [HttpPost("{id}")]
        public ActionResult <Medicament> UpdateMedicament(int id, string name)
        {
            var update = _appDbContext.Medicaments.FirstOrDefault(_ => _.Id == id);

            if (update != null)
            {
                update.Name = name;
                _appDbContext.SaveChanges();

            }
            else
            {
                return BadRequest("Erro ao atualizar");

            }
            return Ok(update);
        }
    }
}
