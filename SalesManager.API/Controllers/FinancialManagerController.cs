using AutoMapper;
using DataTransferObjects.Departments;
using DataTransferObjects.FinancialManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using SalesManager.API.Interfaces;
using System.Data;

namespace SalesManager.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FinancialManagerController : ControllerBase
    {
        private readonly IFinancialManagerService _financialManagerService;
        private readonly IMapper _mapper;

        public FinancialManagerController(IFinancialManagerService financialManagerService, IMapper mapper)
        {
            _financialManagerService = financialManagerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<FinancialManagerGetDTO>> GetFinancialManagersAsync([FromQuery] int idUser) => await _financialManagerService.GetFinancialManagersAsync(idUser);

        [HttpGet]
        [Route("Balance")]
        public async Task<BalanceGetDTO> GetBalanceAsync([FromQuery] int idUser) => await _financialManagerService.GetBalanceAsync(idUser);
    }
}
