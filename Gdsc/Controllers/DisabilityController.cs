using AutoMapper;
using BLL.InterFaces;
using BLL.Spcefication.DisabilitySpc;
using DAL.Entities;
using Gdsc.Dto;
using Gdsc.Errors;
using Gdsc.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Gdsc.Controllers
{

    public class DisabilityController : BaseController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DisabilityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErroeResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DisabilityDto>> AddDisability([FromForm] DisabilityDto DisabilityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var Disability = new Disability()
                {

                    Name = DisabilityDto.Name,
                    Percentage = DisabilityDto.Percentage,

                };

                await _unitOfWork.Repositry<Disability>().Add(Disability);
                var result = await _unitOfWork.Complet();

                return Ok(Disability);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [HttpGet("allData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagintation<IReadOnlyList<DisabilityDto>>>> GetDisability([FromQuery] DisabilitySpecParams specParams)
        {

            var specCount = new DisabilityWithFilterForCountSpecification(specParams);
            var itemCount = await _unitOfWork.Repositry<Disability>().Count(specCount);
            var Disability = await _unitOfWork.Repositry<Disability>().GetAllDataWithSpecificatonAsync(specCount);
            var data = _mapper.Map<IReadOnlyList<Disability>, IReadOnlyList<JobFormDto>>(Disability);
            return Ok(new Pagintation<JobFormDto>(specParams.PageIndex, specParams.PageSize, itemCount, data));

        }

        // Update Filtration
        [HttpGet("{Name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Disability>> GetDisabilityById(string Name)

        {

            var Disability = await _unitOfWork.Repositry<Disability>().GetDataByNameAsync(Name);

            if (Disability == null)

                return NotFound();

            return Ok(Disability);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErroeResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DisabilityDto>> UpdateDisability(int id, [FromForm] DisabilityDto DisabilityDTO)
        {
            if (!ModelState.IsValid)

                return BadRequest(ModelState);
            try
            {
                var Disability = await _unitOfWork.Repositry<Disability>().GetDataByIdAsync(id);

                if (Disability == null)

                    return BadRequest("Cant Find Disability");

                Disability.Name = DisabilityDTO.Name != null ? DisabilityDTO.Name : Disability.Name;
                Disability.Percentage = DisabilityDTO.Percentage != null ? DisabilityDTO.Percentage : Disability.Percentage;
                _unitOfWork.Repositry<Disability>().Update(Disability);
                await _unitOfWork.Complet();

                return Ok(Disability);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpDelete("{Name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErroeResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Disability>> DeleteDisability(string Name)
        {
            var Disability = await _unitOfWork.Repositry<Disability>().GetDataByNameAsync(Name);

            if (Disability == null)

                return BadRequest("Cant Find Disability");

            _unitOfWork.Repositry<Disability>().Delete(Disability);
            await _unitOfWork.Complet();

            return Ok(true);

        }
    }

    }

