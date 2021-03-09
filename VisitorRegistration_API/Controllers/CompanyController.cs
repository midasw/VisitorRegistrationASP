using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration_API.Models;
using VisitorRegistration_BLL.Services;

namespace VisitorRegistration_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            var companies = await _companyService.GetAllCompanies();

            return Ok(_mapper.Map<IEnumerable<CompanyDto>>(companies));
        }
    }
}
