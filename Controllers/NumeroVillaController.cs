
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
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepository _villaRepo;
        private readonly INumeroVillaRepository _numeroVillaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public NumeroVillaController(ILogger<VillaController> logger, IVillaRepository villaRepo, 
                                           INumeroVillaRepository numeroVillaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numeroVillaRepo= numeroVillaRepo;
            _mapper = mapper;
            _apiResponse = new (); //Inicializamos
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Obtener todos los numeros de villas");
                IEnumerable<NumeroVilla> numeroVillaList = await _numeroVillaRepo.ObtenerTodos();
                _apiResponse.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numeroVillaList);
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

      
        [HttpGet("id:int", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Numero villa con Id" + id);
                    _apiResponse.HttpStatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;
                    return BadRequest(_apiResponse);
                }
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                var numeroVilla = await _numeroVillaRepo.Obtener(x => x.villaNo == id);

                if (numeroVilla == null)
                {
                    _apiResponse.HttpStatusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsSuccess = false;
                    return NotFound(_apiResponse);
                }

                _apiResponse.Resultado = _mapper.Map<NumeroVillaDto>(numeroVilla);
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


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid) { 
                    return BadRequest(ModelState);
                }

                if (await _numeroVillaRepo.Obtener(v => v.villaNo == createDto.villaNo) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El numero de villa ya existe");
                    return BadRequest(ModelState);
                }
                if(await _villaRepo.Obtener(v => v.Id == createDto.villaId) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id De la Villa No existe!");
                    return BadRequest(ModelState);
                }

                if (createDto == null) { 
                    return BadRequest(createDto);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion=DateTime.Now;
                await _numeroVillaRepo.Crear(modelo);
                _apiResponse.Resultado = modelo;
                _apiResponse.HttpStatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.villaNo }, _apiResponse);

            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrrorMessages = new List<string>() { ex.ToString() };
            }

            return _apiResponse;
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiResponse.IsSuccess=false;
                    _apiResponse.HttpStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var numerovilla = await _numeroVillaRepo.Obtener(x => x.villaNo == id);

                if (numerovilla == null)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.HttpStatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                 await _numeroVillaRepo.Remover(numerovilla);

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


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.villaNo) {
                _apiResponse.IsSuccess = false;
                _apiResponse.HttpStatusCode= HttpStatusCode.BadRequest;
                return BadRequest(_apiResponse);
            }

            if(await _villaRepo.Obtener(v => v.Id == updateDto.villaId) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El id de la villa No Existe!");
                return BadRequest(ModelState);
            }

             NumeroVilla modelo = _mapper.Map<NumeroVilla>(updateDto);
                      
             await _numeroVillaRepo.Actualizar(modelo);
            _apiResponse.HttpStatusCode=HttpStatusCode.NoContent;

            return Ok(_apiResponse);

        }

              

    }
}
