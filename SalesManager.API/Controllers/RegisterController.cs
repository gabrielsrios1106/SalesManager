using AutoMapper;
using DataTransferObjects.Utils;
using Microsoft.AspNetCore.Mvc;
using Models;
using SalesManager.API.Interfaces;

namespace SalesManager.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly IMapper _mapper;

        public RegisterController(IRegisterService registerService, IMapper mapper)
        {
            _registerService = registerService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> PostRegisterAsync([FromBody] UserPostDTO registerPostDTO)
        {
            User register = _mapper.Map<User>(registerPostDTO);

            if(await _registerService.ExistsByEmailAsync(register.Email)) 
            {
                return NotFound($"Ja existe um usuario com esse email({register.Email})");
            }
            
            await _registerService.InsertAsync(register);
            return Created();
        }
    }
}
