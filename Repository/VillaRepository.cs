﻿using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;

namespace MagicVilla_API.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {

        private readonly ApplicationDbContext _db;

        public VillaRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
                
        }

        public async Task<Villa> Actualizar(Villa enty_villa)
        {
           enty_villa.FechaActualizacion = DateTime.Now;
            _db.Villas.Update(enty_villa);
            await _db.SaveChangesAsync();
            return enty_villa;

        }
    }
}
