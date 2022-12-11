using AutoMapper;
using BackendEmployeeAPI.DTOs;
using BackendEmployeeAPI.Models;
using BackendEmployeeAPI.Services.Contract;
using BackendEmployeeAPI.Utilites;
using Microsoft.AspNetCore.Mvc;

namespace BackendEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IMapper mapper, IDepartmentService departmentService)
        {
            _mapper = mapper;
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            ResponseAPI<List<DepartmentDTO>> _response = new ResponseAPI<List<DepartmentDTO>>();

            try
            {
                List<Department> departments = await _departmentService.GetDepartments();
                if (departments.Count > 0)
                {
                    List<DepartmentDTO> departmentDTOs = _mapper.Map<List<DepartmentDTO>>(departments);
                    _response = new ResponseAPI<List<DepartmentDTO>>() { Status = true, Msg = "OK", Value = departmentDTOs };
                }
                else
                {
                    _response = new ResponseAPI<List<DepartmentDTO>>() { Status = false, Msg = "No Data found" };
                }
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new ResponseAPI<List<DepartmentDTO>>() { Status = false, Msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }
    }
}
