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
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IMapper mapper, IEmployeeService employeeService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            ResponseAPI<List<EmployeeDTO>> _response = new ResponseAPI<List<EmployeeDTO>>();

            try
            {
                List<Employee> employees = await _employeeService.GetEmployees();
                if (employees.Count > 0)
                {
                    List<EmployeeDTO> employeeDTOs = _mapper.Map<List<EmployeeDTO>>(employees);
                    _response = new ResponseAPI<List<EmployeeDTO>>()
                    {
                        Status = true,
                        Msg = "OK",
                        Value = employeeDTOs
                    };
                }
                else
                {
                    _response = new ResponseAPI<List<EmployeeDTO>>()
                    {
                        Status = false,
                        Msg = "No data found",
                    };

                }
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new ResponseAPI<List<EmployeeDTO>>()
                {
                    Status = false,
                    Msg = ex.Message,

                };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }


        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO request)
        {
            ResponseAPI<EmployeeDTO> _response = new ResponseAPI<EmployeeDTO>();
            try
            {

                Employee model = _mapper.Map<Employee>(request);
                Employee createdEmployee = await _employeeService.AddEmployee(model);

                if (createdEmployee.IdEmployee != 0)
                {
                    _response = new ResponseAPI<EmployeeDTO>()
                    {
                        Status = true
                        ,
                        Msg = "ok",
                        Value = _mapper.Map<EmployeeDTO>(createdEmployee)
                    };
                }
                else
                {
                    _response = new ResponseAPI<EmployeeDTO>()
                    {
                        Status = false,
                        Msg = "enable to create employee"
                    };

                }
                return StatusCode(StatusCodes.Status200OK, _response);

            }
            catch (Exception ex)
            {
                _response = new ResponseAPI<EmployeeDTO>()
                {
                    Status = true,
                    Msg = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);

            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeDTO request)
        {
            ResponseAPI<EmployeeDTO> _response = new ResponseAPI<EmployeeDTO>();

            try
            {
                Employee model = _mapper.Map<Employee>(request);
                Employee updatedEmployee = await _employeeService.UpdateEmployee(model);

                if (updatedEmployee.IdEmployee != 0)
                {
                    _response = new ResponseAPI<EmployeeDTO>()
                    {
                        Status = true,
                        Msg = "ok",
                        Value = _mapper.Map<EmployeeDTO>(updatedEmployee)
                    };
                }
                else
                {
                    _response = new ResponseAPI<EmployeeDTO>()
                    {
                        Status = false,
                        Msg = "Enable to update the employee"
                    };
                }
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new ResponseAPI<EmployeeDTO>()
                {
                    Status = true,
                    Msg = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            ResponseAPI<bool> _response = new ResponseAPI<bool>();

            try
            {
                Employee employee = await _employeeService.GetEmployee(id);
                if (employee != null)
                {
                    await _employeeService.DeleteEmployee(employee);
                    _response = new ResponseAPI<bool>()
                    {
                        Status = true,
                        Msg = "Ok",
                        Value = true
                    };
                }
                else
                {
                    _response = new ResponseAPI<bool>()
                    {
                        Status = false,
                        Msg = "Enable to Delete Employee",
                        Value = false
                    };
                }

                return StatusCode(StatusCodes.Status200OK, _response);

            }
            catch (Exception ex)
            {
                _response = new ResponseAPI<bool>()
                {
                    Status = false,
                    Msg = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

    }
}
