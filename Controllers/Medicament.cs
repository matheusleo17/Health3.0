using Care3._0.Data;
using Care3._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Care3._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MedicamentController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        
        public MedicamentController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("Search")]
        public ActionResult<List<Medicament>> GetMedicament()
        {
            var medicaments = _appDbContext.Medicaments.ToList();
            return Ok(medicaments);
        }

        [HttpPost]
        [Route("Insert")]

        public ActionResult<Medicament> InsertMedicament(Medicament medicament)
        {
            _appDbContext.Add(medicament);
            _appDbContext.SaveChanges(); 
            return Ok(medicament);
        }
        [HttpPost]
        [Route("Update")]
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
        [HttpPost]
        [Route("Delete")]
        public ActionResult DeleteMedicament(int id)
        {
            
            var delete = _appDbContext.Medicaments.Where(m => m.Id == id);

            if(delete != null)
            {
                _appDbContext.Remove(delete);
                return Ok(delete);
            }
            else
            {
                return BadRequest("Erro ao deletar.");
            }
        }

    }
}
