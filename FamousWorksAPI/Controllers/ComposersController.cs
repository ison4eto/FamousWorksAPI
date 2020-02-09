using Business.Services.ComposerService;
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
    /// Controller providing all actions for composer
    /// </summary>
    [RoutePrefix("api/composers")]
    public class ComposersController : ApiController
    {
        private readonly ComposerService composerService;
        public ComposersController()
        {
            composerService = new ComposerService(new DbEntitiesContext());
        }

        public ComposersController(DbEntitiesContext context)
        {
            composerService = new ComposerService(context);
        }

        /// <summary>
        /// Gets all composers from the database
        /// </summary>
        /// <returns>All composers</returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var allComposers = composerService.GetAll().Select(d => new ComposerDto(d)).ToList();
            if (allComposers == null)
                return NotFound();
            return Ok(allComposers);
        }

        /// <summary>
        /// Gets a single composer object from the database
        /// </summary>
        /// <param name="id">The id of the composer object</param>
        /// <returns>The found composer</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response coed="404">NotFound</response>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest();
            var composer = composerService.GetByID((int)id);
            if (composer == null)
                return NotFound();
            var dtoComposer = new ComposerDto(composer);
            return Ok(dtoComposer);
        }

        /// <summary>
        /// Creates new composer
        /// </summary>
        /// <param name="composer">The composer object to create</param>
        /// <returns>The created composer</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] ComposerCreateDto composer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                Composer newComposer = new Composer
                {
                    FirstName = composer.FirstName,
                    LastName = composer.LastName,
                    Country = composer.Country
                };

                composerService.Add(newComposer);
                composerService.Save();

                var dtoComposer = new ComposerDto(newComposer);
                return Ok(dtoComposer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing composer
        /// </summary>
        /// <param name="id">The id of the composer to be updated</param>
        /// <param name="composer">The composer object containing the update data</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int? id, [FromBody] ComposerDto composer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (id == null || id <= 0)
                    return BadRequest();
                var dbComposer = composerService.GetByID((int)id);
                if (dbComposer == null)
                    return NotFound();
                dbComposer.FirstName = composer.FirstName;
                dbComposer.LastName = composer.LastName;
                dbComposer.Country = composer.Country;

                composerService.Update(dbComposer);
                composerService.Save();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a composer
        /// </summary>
        /// <param name="id">The id of the composer to delete</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (id == null || id <= 0)
                    return BadRequest();
                var composer = composerService.GetByID((int)id);
                if (composer != null)
                {
                    composerService.Delete(composer);
                    composerService.Save();
                    return StatusCode(HttpStatusCode.NoContent);
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


        /// <summary>
        /// Get a composer by first, last or full name
        /// </summary>
        /// <param name="name">The name of the composer</param>
        /// <returns>The found composer</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var composer = composerService.GetComposerByName(name);
            if (composer == null)
                return NotFound();
            var dtoComposer = new ComposerDto(composer);
            return Ok(dtoComposer);
        }
    }
}
