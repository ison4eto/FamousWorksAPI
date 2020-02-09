using Business.Services.EraService;
using DataAccess;
using DatabaseStructure.Entities;
using FamousWorksAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FamousWorksAPI.Controllers
{
    /// <summary>
    /// Controller providing all actions for era
    /// </summary>
    [RoutePrefix("api/eras")]
    public class EraController : ApiController
    {
        private readonly EraService eraService;
        public EraController()
        {
            eraService = new EraService();
        }

        public EraController(DbEntitiesContext context)
        {
            eraService = new EraService(context);
        }

        /// <summary>
        /// Gets all eras from the database
        /// </summary>
        /// <returns>All eras</returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route]
        public IHttpActionResult Get()
        {
            var eras = eraService.GetAll().Select(g => new EraDto(g)).ToList();
            if (eras == null)
                return NotFound();
            return Ok(eras);
        }

        /// <summary>
        /// Gets a single era object from the database
        /// </summary>
        /// <param name="id">The id of the era object</param>
        /// <returns>The found era</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response coed="404">NotFound</response>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int? id)
        {
            if (id <= 0 || id == null)
                return BadRequest();
            var era = eraService.GetByID((int)id);
            if (era == null)
                return NotFound();
            var eraDto = new EraDto(era);
            return Ok(eraDto);
        }


        /// <summary>
        /// Get a era by name
        /// </summary>
        /// <param name="name">The name of the era</param>
        /// <returns>The found era</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var era = eraService.GetEraByName(name);
            if (era == null)
                return NotFound();
            var dtoEra = new EraDto(era);
            return Ok(dtoEra);
        }

        /// <summary>
        /// Creates new era
        /// </summary>
        /// <param name="era">The era object to create</param>
        /// <returns>The created era</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpPost]
        [Route]
        public IHttpActionResult Post([FromBody] EraCreateDto era)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                Era newEra = new Era()
                {
                    Name = era.Name
                };

                eraService.Add(newEra);
                eraService.Save();

                var dtoEra = new EraDto(newEra);
                return Ok(dtoEra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing era
        /// </summary>
        /// <param name="id">The id of the era to be updated</param>
        /// <param name="era">The era object containing the update data</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int? id, [FromBody] EraDto era)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (id == null || id <= 0)
                    return NotFound();
                var dbEra = eraService.GetByID((int)id);
                dbEra.Name = era.Name;

                eraService.Update(dbEra);
                eraService.Save();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a era
        /// </summary>
        /// <param name="id">The id of the era to delete</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int? id)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (id == null || id <= 0)
                    return NotFound();
                var era = eraService.GetByID((int)id);
                if (era != null)
                {
                    eraService.Delete(era);
                    eraService.Save();
                    return Content(HttpStatusCode.NoContent, era);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
