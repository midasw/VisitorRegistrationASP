using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration_API.Models;
using VisitorRegistration_BLL.Services;
using VisitorRegistration_Models;

namespace VisitorRegistration_API.Controllers
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
