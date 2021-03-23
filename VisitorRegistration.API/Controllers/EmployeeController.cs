using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorRegistration.API.Models;
using VisitorRegistration.BLL.Services;

namespace VisitorRegistration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public EmployeeController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesByCompanyId(int companyId)
        {
            var employees = await _companyService.GetEmployeesByCompanyId(companyId);

            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }
    }
}
