using Business.Services.WorkService;
using DataAccess;
using DatabaseStructure.Entities;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using FamousWorksAPI.DTOs;
using WorksAPI.DTOs;

namespace WorksAPI.Controllers
{
    /// <summary>
    /// Controller providing all actions for work
    /// </summary>
    [RoutePrefix("api/works")]
    public class WorksController : ApiController
    {
        private readonly WorkService workService;

        public WorksController()
        {
            workService = new WorkService();
        }

        public WorksController(DbEntitiesContext context)
        {
            workService = new WorkService(context);
        }

        /// <summary>
        /// Gets all works from the database
        /// </summary>
        /// <returns>All works</returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var allWorks = workService.GetAll().Select(m => new WorkDto(m)).ToList();
            if (allWorks == null)
                return NotFound();
            return Ok(allWorks);
        }

        /// <summary>
        /// Gets a single work object from the database
        /// </summary>
        /// <param name="id">The id of the work object</param>
        /// <returns>The found work</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response coed="404">NotFound</response>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest();
            var work = workService.GetByID((int)id);
            if (work == null)
                return NotFound();
            var dtoWork = new WorkDto(work);
            return Ok(dtoWork);
        }

        /// <summary>
        /// Creates new work
        /// </summary>
        /// <param name="work">The work object to create</param>
        /// <returns>The created work</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]WorkCreateDto work)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                Work newWork = new Work
                {
                    ComposerID = work.ComposerId,
                    Title = work.Title,
                    EraID = work.EraId,
                    Description = work.Description,
                    Year = work.Year
                };

                workService.Add(newWork);
                workService.Save();

                var dtoWork = new WorkDto(newWork);
                return Ok(dtoWork);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing work
        /// </summary>
        /// <param name="id">The id of the work to be updated</param>
        /// <param name="work">The work object containing the update data</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int? id, [FromBody]WorkDto work)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (id == null || id <= 0)
                    return BadRequest();
                var dbWork = workService.GetByID((int)id);
                if (dbWork == null)
                    return NotFound();
                dbWork.ComposerID = work.ComposerID;
                dbWork.Title = work.Title;
                dbWork.EraID = work.EraID;
                dbWork.Description = work.Description;
                dbWork.Year = work.Year;
                workService.Update(dbWork);
                workService.Save();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a work
        /// </summary>
        /// <param name="id">The id of the work to delete</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();
                var work = workService.GetByID(id);
                if (work != null)
                {
                    workService.Delete(work);
                    workService.Save();
                    return Content(HttpStatusCode.NoContent, work);
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
        /// Get a work by title
        /// </summary>
        /// <param name="title">The title of the work</param>
        /// <returns>The found work</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route("{title}")]
        public IHttpActionResult Get(string title)
        {
            if (string.IsNullOrEmpty(title))
                return BadRequest();
            var work = workService.GetWorkByTitle(title);
            if (work == null)
                return NotFound();
            var dtoWork = new WorkDto(work);
            return Ok(dtoWork);
        }

        /// <summary>
        /// Gets a work by the id of a composer
        /// </summary>
        /// <param name="id">The id of the composer</param>
        /// <returns>The found work</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route("~/api/composers/{id:int}/works")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
                return BadRequest();
            var works = workService.GetAllWorksForComposer(id)
                .Select(m => new WorkDto(m)).ToList();
            if (works.Count == 0)
                return NotFound();
            return Ok(works);
        }
    }
}
