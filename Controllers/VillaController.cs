﻿
using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        //private readonly ApplicationDbContext _db;
        private readonly IVillaRepository _villaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public VillaController(ILogger<VillaController> logger, IVillaRepository villaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _mapper = mapper;
            _apiResponse = new (); //Inicializamos
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
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                _logger.LogInformation("Obtener todas las villas");
                IEnumerable<Villa> villaList = await _villaRepo.ObtenerTodos();
                _apiResponse.Resultado = _mapper.Map<IEnumerable<VillaDto>>(villaList);
                _apiResponse.HttpStatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);

            }
            catch (Exception ex)
            {

               _apiResponse.IsSuccess = false;
               _apiResponse.ErrrorMessages = new List<string>() { ex.ToString()};
            }

           return _apiResponse;
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
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer villa con Id" + id);
                    _apiResponse.HttpStatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;
                    return BadRequest(_apiResponse);
                }
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                var villa = await _villaRepo.Obtener(x => x.Id == id);

                if (villa == null)
                {
                    _apiResponse.HttpStatusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsSuccess = false;
                    return NotFound(_apiResponse);
                }

                _apiResponse.Resultado = _mapper.Map<VillaDto>(villa);
                _apiResponse.HttpStatusCode=HttpStatusCode.OK;
                return Ok(_apiResponse);

            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrrorMessages = new List<string>() { ex.ToString() };
            }

            return _apiResponse;
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
        public async Task<ActionResult<APIResponse>> CrearVilla([FromBody] VillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }

                // validacion personalizada
                if (await _villaRepo.Obtener(v => v.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                    return BadRequest(ModelState);
                }

                if (createDto == null) { return BadRequest(createDto); }

                Villa modelo = _mapper.Map<Villa>(createDto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion=DateTime.Now;
                await _villaRepo.Crear(modelo);
                _apiResponse.Resultado = modelo;
                _apiResponse.HttpStatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = modelo.Id }, _apiResponse);

            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrrorMessages = new List<string>() { ex.ToString() };
            }

            return _apiResponse;
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
            try
            {
                if (id == 0)
                {
                    _apiResponse.IsSuccess=false;
                    _apiResponse.HttpStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var villa = await _villaRepo.Obtener(x => x.Id == id);
                if (villa == null)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.HttpStatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                 await _villaRepo.Remover(villa);

                _apiResponse.HttpStatusCode = HttpStatusCode.NoContent;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.IsSuccess = false;
                _apiResponse.ErrrorMessages = new List<string>() { ex.ToString() };
            }

            return BadRequest(_apiResponse);
           
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
            if (updateDto == null || id != updateDto.Id) {
                _apiResponse.IsSuccess = false;
                _apiResponse.HttpStatusCode= HttpStatusCode.BadRequest;
                return BadRequest(_apiResponse);
            }

             Villa modelo = _mapper.Map<Villa>(updateDto);
                      
             await _villaRepo.Actualizar(modelo);
            _apiResponse.HttpStatusCode=HttpStatusCode.NoContent;

            return Ok(_apiResponse);

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

            var villa = await _villaRepo.Obtener(v => v.Id == id,tracked:false);

            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);
                       
            if (villa == null) return BadRequest();
                
            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = _mapper.Map<Villa>(villaDto);
                      

            await _villaRepo.Actualizar(modelo);
            _apiResponse.HttpStatusCode=HttpStatusCode.NoContent;          
            return Ok(_apiResponse);

        }

    }
}
