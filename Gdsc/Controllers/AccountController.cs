using AutoMapper;
using BLL.InterFaces;
using DAL;
using DAL.Entities;
using Gdsc.Dto;
using Gdsc.Errors;
using Gdsc.Extaintions;
using Gdsc.Extensions;
using Gdsc.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gdsc.Controllers
{

    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _token;
        private readonly Gdcs_Context gdcs_Context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,IMapper mapper, RoleManager<IdentityRole> roleManager, ITokenService token, Gdcs_Context gdcs_Context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.mapper = mapper;
            _roleManager = roleManager;
            _token = token;
            this.gdcs_Context = gdcs_Context;
        }

        [HttpPost("Emp")]
        public async Task<ActionResult> EmpRegister([FromForm] EmpRegisterDto empRegisterDto)
        {
            if (emailExist(empRegisterDto.Email).Result.Value)
            {

                return BadRequest(new ApiErroeResponse(400, "this Email is Already in use ! "));
            }
            try
            {

                var addUserEmp = new Employee()
                {
                    FirstName = empRegisterDto.FirstName,
                    LastName = empRegisterDto.LastName,
                    UserName = $"{empRegisterDto.FirstName} {empRegisterDto.LastName}",
                    PhoneNumber = empRegisterDto.PhoneNumber,
                    Gender = empRegisterDto.Gender,
                    Image = DocumentSetting.UploadeFile(empRegisterDto.ImageFile, "imgs"),
                    Cv = DocumentSetting.UploadeFile(empRegisterDto.CvFile, "Cvs"),
                    Email = empRegisterDto.Email,
                    Skils = empRegisterDto.Skils
                 



                };
                var result = await _userManager.CreateAsync(addUserEmp, empRegisterDto.Password);
                if (!result.Succeeded) return BadRequest(new ApiErroeResponse(400));


                if (!await _roleManager.RoleExistsAsync(RoleContentHelper.Emp))
                    await _roleManager.CreateAsync(new IdentityRole(RoleContentHelper.Emp));
                await _userManager.AddToRoleAsync(addUserEmp, RoleContentHelper.Emp);
                return Ok(empRegisterDto);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

          

        }
        [HttpPost("Hier")]
        public async Task<ActionResult> HierRegister([FromForm] HierRegisterDto hierRegisterDto)
        {
            if (emailExist(hierRegisterDto.Email).Result.Value)
            {

                return BadRequest(new ApiErroeResponse(400, "this Email is Already in use ! "));
            }
            try
            {

                var addUserHier = new Hier()
                {
                    FirstName = hierRegisterDto.FirstName,
                    LastName = hierRegisterDto.LastName,
                    UserName = hierRegisterDto.Email,
                    PhoneNumber = hierRegisterDto.PhoneNumber,
                    Gender = hierRegisterDto.Gender,
                    Company = hierRegisterDto.Company,
                    Image = DocumentSetting.UploadeFile(hierRegisterDto.ImageFile, "imgs"),
                    Email = hierRegisterDto.Email,



                };
                var result = await _userManager.CreateAsync(addUserHier, hierRegisterDto.Password);
                if (!result.Succeeded) return BadRequest(new ApiErroeResponse(400));


                if (!await _roleManager.RoleExistsAsync(RoleContentHelper.Hier))
                    await _roleManager.CreateAsync(new IdentityRole(RoleContentHelper.Hier));
                await _userManager.AddToRoleAsync(addUserHier, RoleContentHelper.Hier);
                return Ok(hierRegisterDto);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

          

        }

        [HttpGet("emailExist")]
        public async Task<ActionResult<bool>> emailExist([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;

        }



        [HttpPost("login")]
        public async Task<ActionResult<LoginDto>> LoginUser(LoginDto loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized(new ApiErroeResponse(400, "email not exist"));
            var password = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (password)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
                if (!result.Succeeded) return BadRequest(new ApiErroeResponse(400, "signIn Filed"));
            }
            else { return BadRequest(new ApiErroeResponse(400, "invalid passwor")); };
            var userRole = await _userManager.GetRolesAsync(user);
            var authUser = new UserDto()
            {
                UserName = user.UserName,
                Roles = userRole[0],
                Token = await _token.CreateToken(user, _userManager)

            };


            return Ok(authUser);
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<User>> CurrentUser()
        {
            var user = await _userManager.FindByEmailWithJobsAsync(User);
            var userRole = await _userManager.GetRolesAsync(user);

            if (userRole[0] == "Employee")
            return Ok(new CurrentUserEmpDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Image = user.Image,
                Gender = user.Gender,
                Role = userRole[0],
                PhoneNumber = user.PhoneNumber,
                Cv = user.Cv,
                Skils = user.Skils

            });
            else
                return Ok(new CurrentUserHierDto()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Image = user.Image,
                    Gender = user.Gender,
                    Role = userRole[0],
                    PhoneNumber = user.PhoneNumber,
                    Company = user.Company,
                    Jops = user.Jop

                });


        }
        //[Authorize]
        //[HttpPost("updateUser")]
        //public async Task<ActionResult<UpdateDataOfUser>> UpdateUser([FromForm] UpdateDataOfUser newDataOfuser)
        //{
        //    newDataOfuser.Image = DocumentSettings.UploadFile(newDataOfuser.ImageFile, "imgs");
        //    var user = await _userManager.FindUserByEmail(User);
        //    user.PhoneNumber = newDataOfuser.PhonNumber;
        //    user.Address.Country = newDataOfuser.Country;
        //    user.Address.City = newDataOfuser.City;
        //    user.Image = newDataOfuser.Image;
        //    user.DisplayNaem = newDataOfuser.DisplayNaem;
        //    user.Address.Street = newDataOfuser.Street;

        //    var result = await _userManager.UpdateAsync(user);

        //    if (!result.Succeeded) return BadRequest(new ApiValidationErrorRespons() { Errors = new[] { "an Error Acourd" } });
        //    return Ok(newDataOfuser);
        //}


    }
}
