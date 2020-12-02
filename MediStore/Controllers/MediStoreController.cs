using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediStore.Models;
using MediStore.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MediStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediStoreController : ControllerBase
    {

        private readonly IMediStore _mediStore;

        public MediStoreController(IMediStore mediStore)
        {
            _mediStore = mediStore;
        }

        /// <summary>
        /// Get all Medicine
        /// </summary>
        /// <returns>Result Response</returns>
        /// <response code="200">Success/response>
        /// <response code="201">Successful create/response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized and cannot create</response> 
        /// <response code="409">Conflict found and cannot create</response> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineModel>>> Get()
        {
            try
            {

                var results = await _mediStore.GetAllMedicinesAsync();

                return results;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get a Medicine
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result Response</returns>
        /// <response code="200">Success/response>
        /// <response code="201">Successful create/response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized and cannot create</response> 
        /// <response code="409">Conflict found and cannot create</response> 
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicineModel>> Get(string id)
        {
            try
            {

                var results = await _mediStore.GetMedicineByIdAsync(id);

                return results;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Insert a Medicine
        /// </summary>
        /// <param name="medicine"></param>
        /// <returns>Result Response</returns>
        /// <response code="200">Success/response>
        /// <response code="201">Successful create/response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized and cannot create</response> 
        /// <response code="409">Conflict found and cannot create</response> 
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MedicineModel medicine)
        {
            try
            {

                var results = await _mediStore.AddMedicineAsync(medicine);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Upsert a Medicine
        /// </summary>
        /// <param name="medicine"></param>
        /// <returns>Result Response</returns>
        /// <response code="200">Success/response>
        /// <response code="201">Successful create/response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized and cannot create</response> 
        /// <response code="409">Conflict found and cannot create</response>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MedicineModel medicine)
        {
            try
            {

                var results = await _mediStore.UpsertMedicineAsync(medicine);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }


        }

        /// <summary>
        /// Delete a Medicine
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result Response</returns>
        /// <response code="200">Success/response>
        /// <response code="201">Successful create/response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized and cannot create</response> 
        /// <response code="409">Conflict found and cannot create</response> 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            try
            {

                var results = await _mediStore.DeleteMedicineByNameAsync(id);

                return results;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
