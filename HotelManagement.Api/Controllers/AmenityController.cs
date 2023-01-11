using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmenityController : ControllerBase
    {
        private readonly ILogger<AmenityController> _logger;
        private readonly IAmenityService _amenityService;

        public AmenityController(ILogger<AmenityController> logger, IAmenityService amenityService)
        {
            _logger = logger;
            _amenityService = amenityService;
        }

        // [Authorize(Roles = "Admin")]
        //HttpGet("GetAmenities")]


       [HttpGet("GetAmenities")]
        public async Task<ActionResult<Response<List<AmenityDTO>>>> GetAmenities()
        {


            try
            {
                //log information
                var result = await _amenityService.GetAmenities();
                if (result == null)
                {
                    return Ok(result);
                }
                return Ok(result);
            }

            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }


        // [Authorize(Roles = "Manager")]
        [HttpPost("CreateAmenity")]

        public async Task<ActionResult<Response<CreateAmenitiesDTO>>> CreateAmenity([FromBody] CreateAmenitiesDTO createDto)
        {


            try
            {

                //Check if this  amenity has been created for the hotel
                if (await _amenityService.GetAsync(x => x.Name.ToLower() ==
                createDto.Name.ToLower()) != null)
                {
                    //log error
                    ModelState.AddModelError("ErrorMessage", "This Amenity has been created for this hotel!");
                    return BadRequest(ModelState);
                }
                //check if the record exist for the id
                if (await _amenityService.GetAsync(u => u.HotelId == createDto.HotelId) == null)
                {   //log error
                    ModelState.AddModelError("errormessage", "hotel id is invalid");
                    return BadRequest(ModelState);
                }


                var result = await _amenityService.CreateAmenity(createDto);
                return CreatedAtAction("GetAmenities", new { id = createDto.HotelId }, result);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);

            }

        }


        //HttpPost("CreateAmenity")]


        //[Authorize(Roles = "Admin")]
        [HttpPut("UpdateAmenity")]
        public async Task<ActionResult<Response<UpdateAmenityDTO>>> UpdateAmenity(string id,  UpdateAmenityDTO updateDto)
        {
         

            try
            {
                if (await _amenityService.GetAsync(u => u.Id == updateDto.Id) == null)
                {
                    //log error
                    ModelState.AddModelError("ErrorMessage", "Amenity ID is Invalid");
                    return BadRequest(ModelState);
                }


                var result = await _amenityService.UpdateAmenity(id, updateDto);
                return Ok(result);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
          
        }
        // [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteAmenity")]
        public async Task<ActionResult<Response<string>>>DeleteAmenity(string id)
        {
            try
            {
                //check if the record exist for the id
                if (await _amenityService.GetAsync(u => u.Id == id) == null)
                {   //log error
                    ModelState.AddModelError("errormessage", "invalid Amenity Id");
                    return BadRequest(ModelState);
                }
                //log information
                var result = await _amenityService.DeleteAmenity(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}