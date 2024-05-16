using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;

namespace MagicVilla_API.Repository
{
    public class NumeroVillaRepository : Repository<NumeroVilla>, INumeroVillaRepository
    {

        private readonly ApplicationDbContext _db;

        public NumeroVillaRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
                
        }

        public async Task<NumeroVilla> Actualizar(NumeroVilla enty_villa)
        {
           enty_villa.FechaActualizacion = DateTime.Now;
            _db.NumeroVillas.Update(enty_villa);
            await _db.SaveChangesAsync();
            return enty_villa;

        }
    }
}
