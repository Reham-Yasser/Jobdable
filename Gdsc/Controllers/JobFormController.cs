using AutoMapper;
using BLL.InterFaces;
using BLL.Spcefication.JobFormSpec;
using BLL.Spcefication.JobSpecification;
using DAL.Entities;
using Gdsc.Dto;
using Gdsc.Errors;
using Gdsc.Helper;
using Gdsc.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gdsc.Controllers
{
   
    public class JobFormController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public JobFormController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErroeResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JopForm>> AddJob([FromForm] JobFormDto jobFormDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var addJobForm = new JopForm()
                {

                    Name = jobFormDto.Name,
                    Email = jobFormDto.Email,
                    JopId = jobFormDto.JopId,
                    Cv = DocumentSetting.UploadeFile(jobFormDto.CvFile, "Cvs"),

                };

                await _unitOfWork.Repositry<JopForm>().Add(addJobForm);
                var result = await _unitOfWork.Complet();

                return Ok(addJobForm);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }


        }
        [HttpGet("allData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagintation<IReadOnlyList<JobFormDto>>>> GetJobForms([FromQuery] JobFormSpecParams specParams)
        {

            var specCount = new JobFormWithFilterForCountSpecification(specParams);
            var itemCount = await _unitOfWork.Repositry<JopForm>().Count(specCount);
            var jobForms = await _unitOfWork.Repositry<JopForm>().GetAllDataWithSpecificatonAsync(specCount);
            var data = _mapper.Map<IReadOnlyList<JopForm>, IReadOnlyList<JobFormDto>>(jobForms);
            return Ok(new Pagintation<JobFormDto>(specParams.PageIndex, specParams.PageSize, itemCount, data));


        }

        // Update Filtration
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<JopForm>> GetJobFormById(int id)

        {
            var jobForm = await _unitOfWork.Repositry<JopForm>().GetDataByIdAsync(id);
           
            if (jobForm == null) 
                
              return NotFound();
         
            return Ok(jobForm);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErroeResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JobFormDto>> UpdateJobForms(int id, [FromForm] JobFormDto JobFormDTO)
        {
            if (!ModelState.IsValid)

                return BadRequest(ModelState);
            try
            {
                var jobForm = await _unitOfWork.Repositry<JopForm>().GetDataByIdAsync(id);

                if (jobForm == null)

                    return BadRequest("Cant Find Job Forms");

                jobForm.Name = JobFormDTO.Name != null ? JobFormDTO.Name : jobForm.Name;
                jobForm.Email = JobFormDTO.Email != null ? JobFormDTO.Email : jobForm.Email;
                jobForm.PhoneNumber = JobFormDTO.PhoneNumber != null ? JobFormDTO.PhoneNumber : jobForm.PhoneNumber;
                jobForm.Cv = JobFormDTO.CvFile != null ? DocumentSetting.UploadeFile(JobFormDTO.CvFile, "Cvs") : jobForm.Cv;
                _unitOfWork.Repositry<JopForm>().Update(jobForm);
                await _unitOfWork.Complet();
                
                return Ok(jobForm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErroeResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JopForm>> DeleteJobForm(int id)
        {
            var JobForm = await _unitOfWork.Repositry<JopForm>().GetDataByIdAsync(id);
            if (JobForm == null)

                return BadRequest("Cant Find Job Form");

            _unitOfWork.Repositry<JopForm>().Delete(JobForm);
            await _unitOfWork.Complet();

            return Ok(true);

        }
    }
}
