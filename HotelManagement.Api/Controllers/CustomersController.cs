using HotelManagement.Core.DTOs;
using HotelManagement.Core;
using HotelManagement.Core.IServices;
using HotelManagement.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HotelManagement.Core.Domains;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IManagerRequestService _managerRequestService;

        public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService, IManagerRequestService managerRequestService)
        {

            _logger = logger;
            _customerService = customerService;
            _managerRequestService = managerRequestService;
        }

        [HttpGet("GetCustomers")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Response<List<GetCustomerDto>>>> GetCustomers(int pageNo)
        {


            try
            {
                //log information
                var result = await _customerService.GetCustomers(pageNo);
                if (result == null)
                {
                    return NotFound("Custmomers not found");
                }
                return Ok(result);
            }

            catch (Exception ex)
            {
                //log the errors
                // catch exception
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("AddCustomerAddress")]
        public async Task<IActionResult> AddCustomerAddress(AddCustomerAddressDto address)
        {
            var result = await _customerService.AddCustomerAddress(address);
            if (result.Succeeded) return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("HotelTopCustomers/{hotelId}")]
        public async Task<IActionResult> HotelTopCustomers(string hotelId)
        {
            var result = await _customerService.GetTopHotelCustomers(hotelId);
            return Ok(result);
        }

        [HttpGet("GetCustomersByHotel")]
        public async Task<ActionResult<Response<GetCustomersByHotelDto>>> GetCustomersByHotel(string hotelId)
        {
            try
            {
                var HotelByCust = await _customerService.GetCustomersByHotelId(hotelId);
                if (HotelByCust == null)
                {
                    return BadRequest();
                }
                return Ok(HotelByCust);
            }
            catch (Exception ex)
            {


                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Response<Customer>>> GetCustomer(string Id)
        {
            var result = await _customerService.GetCustomer(Id);
            return result;
        }

        [HttpGet("Manager/{Id}")]
        public async Task<ActionResult<Response<Manager>>> GetManager(string Id)
        {
            var result = await _managerRequestService.GetManager(Id);
            return result;
        }

    }
}
