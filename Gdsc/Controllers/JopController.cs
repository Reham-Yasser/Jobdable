using AutoMapper;
using BLL.InterFaces;
using BLL.Repositries;
using BLL.Spcefication.JobSpecification;
using DAL.Entities;
using Gdsc.Dto;
using Gdsc.Errors;
using Gdsc.Helper;
using Gdsc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Gdsc.Controllers
{

    public class JopController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public JopController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErroeResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Jop>> AddJob([FromForm] JopDto JopDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var addJob = new Jop()
                {
                    Name = JopDTO.Name,
                    Description = JopDTO.Description,
                    Location = JopDTO.Location,
                    Salary = JopDTO.Salary,
                    UserId = JopDTO.HierId,
                    DisabilityId = JopDTO.DisabilityId,
                    
                };

                await _unitOfWork.Repositry<Jop>().Add(addJob);
                var result = await _unitOfWork.Complet();

                return Ok(addJob);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }


        }
        [HttpGet("allData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagintation<IReadOnlyList<JopDto>>>> GetJobs([FromQuery] JobtSpecParams specParams)
        {

            var spec = new JobWithHirerSpecification(specParams);
            var specCount = new JobWithFiltersForCountSpecification(specParams);
            var itemCount = await _unitOfWork.Repositry<Jop>().Count(specCount);
            var product = await _unitOfWork.Repositry<Jop>().GetAllDataWithSpecificatonAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Jop>, IReadOnlyList<JopDto>>(product);
            return Ok(new Pagintation<JopDto>(specParams.PageIndex, specParams.PageSize, itemCount, data));


        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Jop>> GetJobByyId(int id)

        {
            var spec = new JobWithHirerSpecification(id);
            var job = await _unitOfWork.Repositry<Jop>().GetDataByIdWithSpecificatonAsync(spec);
            if (job == null) return NotFound();
            return Ok(job);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErroeResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JopDto>> UpdateJop(int id ,[FromForm] JopDto JobDTO)
        {
            if (!ModelState.IsValid)

                return BadRequest(ModelState);
            try
            {
                var spec = new JobWithHirerSpecification(id);
                var job = await _unitOfWork.Repositry<Jop>().GetDataByIdWithSpecificatonAsync(spec);

                if (job == null)

                    return BadRequest("Cant Find Job");


                job.Name = JobDTO.Name != null ? JobDTO.Name : job.Name;
                job.Description = JobDTO.Description != null ? JobDTO.Description : job.Description;
                job.Location = JobDTO.Location != null ? JobDTO.Location : job.Location;
                job.Salary = JobDTO.Salary != null ? JobDTO.Salary : job.Salary;
                _unitOfWork.Repositry<Jop>().Update(job);
                await _unitOfWork.Complet();


                return Ok(job);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErroeResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Jop>> DeleteJob(int id)
        {
            var spec = new JobWithHirerSpecification(id);
            var Job = await _unitOfWork.Repositry<Jop>().GetDataByIdWithSpecificatonAsync(spec);
            if (Job == null)

                return BadRequest("Cant Find Job");


            _unitOfWork.Repositry<Jop>().Delete(Job);
            await _unitOfWork.Complet();

            return Ok(true);

        }




    }
}
