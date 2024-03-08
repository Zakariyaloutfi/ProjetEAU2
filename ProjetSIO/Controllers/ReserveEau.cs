using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetSIO.Data;
using ProjetSIO.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetSIO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservesEauController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservesEauController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReserveEau>>> GetReservesEau()
        {
            return await _context.ReservesEau.ToListAsync();
        }

        [HttpGet("{codePostal}")]
        public async Task<ActionResult<ReserveEau>> GetReserveEau(string codePostal)
        {
            var reserveEau = await _context.ReservesEau.FirstOrDefaultAsync(r => r.CodePostal == codePostal);

            if (reserveEau == null)
            {
                return NotFound();
            }

            return reserveEau;
        }

        [HttpPost]
        public async Task<ActionResult<ReserveEau>> PostReserveEau(ReserveEau reserveEau)
        {
            _context.ReservesEau.Add(reserveEau);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReserveEau", new { codePostal = reserveEau.CodePostal }, reserveEau);
        }


        [HttpPost("autonomie")]
        public ActionResult<int> CalculateAutonomie([FromBody] AutonomieRequest request)
        {
 
            var autonomie = (int)(request.VolumeReserve / request.ConsommationJour);
            return Ok(autonomie);
        }

        public class AutonomieRequest
        {
            public double KC { get; set; }
            public double VolumeReserve { get; set; }
            public double SurfaceCulture { get; set; }
            public double ConsommationJour => KC * SurfaceCulture; 
        }
    }
}
