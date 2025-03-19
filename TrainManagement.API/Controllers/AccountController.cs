using AutoMapper;
using TrainManagement.API.ViewModels;
using TrainManagement.Common.Abstract.Services;
using TrainManagement.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrainManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(IAuthService authService,
                                 IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.Authenticate(_mapper.Map<User>(model));
                if (user == null)
                {
                    return Unauthorized();
                }
                AuthViewModel result = null;

                result = _mapper.Map<AuthViewModel>(user);

                return Ok(result);
            }
            else
            {
                ModelState.AddModelError("LoginError", "Incorrect login or password");
                return ValidationProblem(ModelState);
            }
        }
       
    }
}
