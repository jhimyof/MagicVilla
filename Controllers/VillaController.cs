
using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }


        //*****METODO SYNCRONO
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IEnumerable<VillaDto>> GetVillas()
        //{
        //    _logger.LogInformation("Obtener todas las villas");
        //    // return Ok(VillaStore.villaList
        //     return Ok(_db.Villas.ToList());
        //}

        ////*****METODO ASYNCRONO 
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()        {
        //    _logger.LogInformation("Obtener todas las villas");
        //    // return Ok(VillaStore.villaList
        //    return Ok(await _db.Villas.ToListAsync());
        //}


        ////*****METODO ASYNCRONO USANDO MAPPER
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obtener todas las villas");
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }


        //*****METODO SYNCRONO
        //[HttpGet("id:int", Name = "GetVilla")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<VillaDto> GetVilla(int id)
        //{
        //    if (id == 0)
        //    {
        //        _logger.LogError("Error al traer villa con Id" + id);
        //        return BadRequest();
        //    }
        //    //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
        //    var villa = _db.Villas.FirstOrDefault(x => x.Id == id);

        //    if (villa == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(villa);
        //}

        ////*****METODO ASYNCRONO
        //[HttpGet("id:int", Name = "GetVilla")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task< ActionResult<VillaDto>>GetVilla(int id)
        //{
        //    if (id == 0)
        //    {
        //        _logger.LogError("Error al traer villa con Id" + id);
        //        return BadRequest();
        //    }
        //    //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
        //    var villa = await _db.Villas.FirstOrDefaultAsync(x => x.Id == id);

        //    if (villa == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(villa);
        //}


        //*****METODO ASYNCRONO USANDO MAPPER
        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer villa con Id" + id);
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = await _db.Villas.FirstOrDefaultAsync(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDto>(villa));
        }


        //*****METODO SYNCRONO
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public ActionResult<VillaDto> CrearVilla([FromBody] VillaCreateDto villaDto)
        //{
        //    if (!ModelState.IsValid) { return BadRequest(ModelState); }

        //    // validacion personalizada
        //    if (_db.Villas.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null){
        //       // if (VillaStore.villaList.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null) {
        //        ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
        //        return BadRequest(ModelState);
        //    }

        //    if (villaDto == null) { return BadRequest(); }

        //    //if (villaDto.Id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }

        //    //villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
        //    //VillaStore.villaList.Add(villaDto);

        //    Villa modelo = new()
        //    {
        //        Nombre = villaDto.Nombre,
        //        Detalle = villaDto.Detalle,
        //        ImagenUrl = villaDto.ImagenUrl,
        //        Ocupantes = villaDto.Ocupantes,
        //        Tarifa = villaDto.Tarifa,
        //        MetrosCuadrados = villaDto.MetrosCuadrados,
        //        Amenidad = villaDto.Amenidad
        //    };


        //    _db.Villas.Add(modelo);
        //    _db.SaveChanges();                

        //    // return Ok(villaDto);
        //    return CreatedAtRoute("GetVilla", new { id = modelo.Id }, villaDto);

        //}

        ////*****METODO ASYNCRONO
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task <ActionResult<VillaDto>> CrearVilla([FromBody] VillaCreateDto villaDto)
        //{
        //    if (!ModelState.IsValid) { return BadRequest(ModelState); }

        //    // validacion personalizada
        //    if (await _db.Villas.FirstOrDefaultAsync(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
        //    {
        //        // if (VillaStore.villaList.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null) {
        //        ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
        //        return BadRequest(ModelState);
        //    }

        //    if (villaDto == null) { return BadRequest(); }

        //    //if (villaDto.Id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }

        //    //villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
        //    //VillaStore.villaList.Add(villaDto);

        //    Villa modelo = new()
        //    {
        //        Nombre = villaDto.Nombre,
        //        Detalle = villaDto.Detalle,
        //        ImagenUrl = villaDto.ImagenUrl,
        //        Ocupantes = villaDto.Ocupantes,
        //        Tarifa = villaDto.Tarifa,
        //        MetrosCuadrados = villaDto.MetrosCuadrados,
        //        Amenidad = villaDto.Amenidad
        //    };


        //    await _db.Villas.AddAsync(modelo);
        //    await _db.SaveChangesAsync();

        //    // return Ok(villaDto);
        //    return CreatedAtRoute("GetVilla", new { id = modelo.Id }, villaDto);

        //}


        //*****METODO ASYNCRONO con mapper
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CrearVilla([FromBody] VillaCreateDto createDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // validacion personalizada
            if (await _db.Villas.FirstOrDefaultAsync(v => v.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (createDto == null) { return BadRequest(createDto); }

            Villa modelo =_mapper.Map<Villa>(createDto);

            await _db.Villas.AddAsync(modelo);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetVilla", new { id = modelo.Id }, modelo);

        }




        //*****METODO SYNCRONO
        //[HttpDelete("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult DeleteVilla(int id) {

        //    if (id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
        //    var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
        //    if (villa == null) {
        //        return NotFound();
        //    }

        //   // VillaStore.villaList.Remove(villa);
        //   _db.Villas.Remove(villa);
        //   _db.SaveChanges(true);


        //    return NoContent();
        //}

        //*****METODO ASYNCRONO
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            var villa = await _db.Villas.FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            // VillaStore.villaList.Remove(villa);
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync(true);


            return NoContent();
        }


        ////*****METODO SYNCRONO
        ////[HttpPut("{id:int}")]
        ////[ProducesResponseType(StatusCodes.Status204NoContent)]
        ////[ProducesResponseType(StatusCodes.Status400BadRequest)]
        ////public IActionResult UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto)
        ////{
        ////    if (villaDto == null || id != villaDto.Id) { return BadRequest(); }

        ////    //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
        ////    //villa.Nombre = villaDto.Nombre;
        ////    //villa.Ocupantes = villaDto.Ocupantes;
        ////    //villa.MetrosCuadrados = villaDto.MetrosCuadrados

        ////    Villa modelo = new()
        ////    {
        ////        Id = villaDto.Id,
        ////        Nombre = villaDto.Nombre,
        ////        Detalle = villaDto.Detalle,
        ////        ImagenUrl = villaDto.ImagenUrl,
        ////        Ocupantes = villaDto.Ocupantes,
        ////        Tarifa = villaDto.Tarifa,
        ////        MetrosCuadrados = villaDto.MetrosCuadrados,
        ////        Amenidad = villaDto.Amenidad

        ////    };

        ////    _db.Villas.Update(modelo);
        ////    _db.SaveChanges();

        ////    return NoContent();

        ////}


        ////*****METODO ASYNCRONO
        //[HttpPut("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task< IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto)
        //{
        //    if (villaDto == null || id != villaDto.Id) { return BadRequest(); }

        //    //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
        //    //villa.Nombre = villaDto.Nombre;
        //    //villa.Ocupantes = villaDto.Ocupantes;
        //    //villa.MetrosCuadrados = villaDto.MetrosCuadrados

        //    Villa modelo = new()
        //    {
        //        Id = villaDto.Id,
        //        Nombre = villaDto.Nombre,
        //        Detalle = villaDto.Detalle,
        //        ImagenUrl = villaDto.ImagenUrl,
        //        Ocupantes = villaDto.Ocupantes,
        //        Tarifa = villaDto.Tarifa,
        //        MetrosCuadrados = villaDto.MetrosCuadrados,
        //        Amenidad = villaDto.Amenidad

        //    };

        //    _db.Villas.Update(modelo);
        //    await  _db.SaveChangesAsync();

        //    return NoContent();

        //}


        //*****METODO SYNCRONO
        //[HttpPut("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto)
        //{
        //    if (villaDto == null || id != villaDto.Id) { return BadRequest(); }

        //    //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
        //    //villa.Nombre = villaDto.Nombre;
        //    //villa.Ocupantes = villaDto.Ocupantes;
        //    //villa.MetrosCuadrados = villaDto.MetrosCuadrados

        //    Villa modelo = new()
        //    {
        //        Id = villaDto.Id,
        //        Nombre = villaDto.Nombre,
        //        Detalle = villaDto.Detalle,
        //        ImagenUrl = villaDto.ImagenUrl,
        //        Ocupantes = villaDto.Ocupantes,
        //        Tarifa = villaDto.Tarifa,
        //        MetrosCuadrados = villaDto.MetrosCuadrados,
        //        Amenidad = villaDto.Amenidad

        //    };

        //    _db.Villas.Update(modelo);
        //    _db.SaveChanges();

        //    return NoContent();

        //}


        //*****METODO ASYNCRONO con MAPPER
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id) { return BadRequest(); }

            Villa modelo = _mapper.Map<Villa>(updateDto);
                      
            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();

            return NoContent();

        }





        //*****METODO SYNCRONO
        //[HttpPatch("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        //{
        //    if (patchDto == null || id ==0) { return BadRequest(); }

        //    //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
        //    var villa = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);

        //    VillaUpdateDto villaDto = new()
        //    {
        //        Id = villa.Id,
        //        Nombre = villa.Nombre,
        //        Detalle = villa.Detalle,
        //        ImagenUrl = villa.ImagenUrl,
        //        Ocupantes = villa.Ocupantes,
        //        Tarifa = villa.Tarifa,
        //        MetrosCuadrados = villa.MetrosCuadrados,
        //        Amenidad = villa.Amenidad

        //    };

        //    if(villa == null) return BadRequest();

        //    //patchDto.ApplyTo(villad,ModelState);
        //    patchDto.ApplyTo(villaDto, ModelState);

        //    if (!ModelState.IsValid) {
        //        return BadRequest(ModelState);
        //    }

        //    Villa modelo = new()
        //    {
        //        Id = villaDto.Id,
        //        Nombre = villaDto.Nombre,
        //        Detalle = villaDto.Detalle,
        //        ImagenUrl = villaDto.ImagenUrl,
        //        Ocupantes = villaDto.Ocupantes,
        //        Tarifa = villaDto.Tarifa,
        //        MetrosCuadrados = villaDto.MetrosCuadrados,
        //        Amenidad = villaDto.Amenidad
        //    };

        //    _db.Villas.Update(modelo);
        //    _db.SaveChanges();
        //    return NoContent();

        //}

        ////*****METODO ASYNCRONO
        //[HttpPatch("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        //{
        //    if (patchDto == null || id == 0) { return BadRequest(); }

        //    //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
        //    var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

        //    VillaUpdateDto villaDto = new()
        //    {
        //        Id = villa.Id,
        //        Nombre = villa.Nombre,
        //        Detalle = villa.Detalle,
        //        ImagenUrl = villa.ImagenUrl,
        //        Ocupantes = villa.Ocupantes,
        //        Tarifa = villa.Tarifa,
        //        MetrosCuadrados = villa.MetrosCuadrados,
        //        Amenidad = villa.Amenidad

        //    };

        //    if (villa == null) return BadRequest();

        //    //patchDto.ApplyTo(villad,ModelState);
        //    patchDto.ApplyTo(villaDto, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Villa modelo = new()
        //    {
        //        Id = villaDto.Id,
        //        Nombre = villaDto.Nombre,
        //        Detalle = villaDto.Detalle,
        //        ImagenUrl = villaDto.ImagenUrl,
        //        Ocupantes = villaDto.Ocupantes,
        //        Tarifa = villaDto.Tarifa,
        //        MetrosCuadrados = villaDto.MetrosCuadrados,
        //        Amenidad = villaDto.Amenidad
        //    };

        //    _db.Villas.Update(modelo);
        //    await _db.SaveChangesAsync();
        //    return NoContent();

        //}


        //*****METODO ASYNCRONO con MAPPER
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0) { return BadRequest(); }

            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);
                       
            if (villa == null) return BadRequest();

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = _mapper.Map<Villa>(villaDto);
                      

            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();

        }

    }
}
