using AutoMapper;
using DataTransferObjects.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using SalesManager.API.Interfaces;
using System.Data;

namespace SalesManager.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentsService;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentService departmentsService, IMapper mapper)
        {
            _departmentsService = departmentsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentGetDTO>>> GetDepartmentsAsync([FromQuery] string searchValue, [FromQuery] int idUser, [FromQuery] bool showInactive = true)
        {
            List<Department> departments = await _departmentsService.GetDepartmentsAsync(searchValue, idUser, showInactive);

            if (departments == null) { return NotFound(); }

            List<DepartmentGetDTO> departmentsGetDTO = _mapper.Map<List<DepartmentGetDTO>>(departments);

            return Ok(departmentsGetDTO);
        }

        [HttpGet]
        [Route("GetDepartmentById/{departmentId}")]
        public async Task<ActionResult<DepartmentGetDTO>> GetDepartmentByIdAsync([FromRoute] int departmentId)
        {
            Department department = await _departmentsService.GetDepartmentByIdAsync(departmentId);

            if (department == null)
            {
                return NotFound("Nenhum registro encontrado");
            }

            DepartmentGetDTO departmentGetDTO = _mapper.Map<DepartmentGetDTO>(department);

            return Ok(departmentGetDTO);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentGetDTO>> PostDepartmentAsync([FromBody] DepartmentPostDTO departmentPostDTO)
        {
            if (await _departmentsService.ExistsByNameAsync(departmentPostDTO.DepartmentName, departmentPostDTO.UserId))
            {
                return BadRequest($"Já existe um departamento com o nome {departmentPostDTO.DepartmentName}");
            }

            Department department = _mapper.Map<Department>(departmentPostDTO);
            await _departmentsService.InsertAsync(department);

            //DepartmentGetDTO departmentGetDTO = new DepartmentGetDTO();
            //return CreatedAtAction("GetDepartmentById", new { departmentId = department.Id }, departmentGetDTO);
            return Created();
        }

        [HttpPut]
        public async Task<ActionResult> PutDepartmentAsync([FromBody] DepartmentPutDTO departmentPutDTO)
        {
            Department department = await _departmentsService.GetDepartmentByIdAsync(departmentPutDTO.Id);

            if (department == null)
            {
                return NotFound($"Nenhum registro encontrado com o id {departmentPutDTO.Id}");
            }

            if (await _departmentsService.ExistsByNameUpdateAsync(departmentPutDTO.DepartmentName, departmentPutDTO.Id, departmentPutDTO.UserId))
            {
                return BadRequest($"Já existe um departamento com o nome {departmentPutDTO.DepartmentName}");
            }

            department.DepartmentName = departmentPutDTO.DepartmentName;
            department.Status = departmentPutDTO.Status;

            try
            {
                await _departmentsService.UpdateAsync(department);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest($"Erro interno do sistema: {e.Message}");
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{departmentId}")]
        public async Task<ActionResult> DeleteDepartmentAsync([FromRoute] int departmentId)
        {
            Department department = await _departmentsService.GetDepartmentByIdAsync(departmentId);

            if (department == null)
            {
                return NotFound($"Nenhum registro encontrado com o id {departmentId}");
            }

            try
            {
                if (await _departmentsService.HasProduct(departmentId, department.UserId))
                {
                    department.Status = 0;
                    await _departmentsService.UpdateAsync(department);
                }
                else
                {
                    await _departmentsService.DeleteAsync(department);
                }
            }
            catch (DBConcurrencyException e)
            {
                return BadRequest($"Erro interno do sistema: {e.Message}");
            }

            return NoContent();
        }
    }
}
