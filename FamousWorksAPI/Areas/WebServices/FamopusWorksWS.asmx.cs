using Business.Services.ComposerService;
using Business.Services.EraService;
using Business.Services.WorkService;
using DatabaseStructure.Entities;
using FamousWorksAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WorksAPI.DTOs;

namespace FamousWorksAPI.Areas.WebServices
{
    /// <summary>
    /// Summary description for FamopusWorksWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FamopusWorksWS : System.Web.Services.WebService
    {
        private readonly ComposerService composerService;
        private readonly WorkService workService;
        private readonly EraService eraService;
        private string message = "Sucess!";
        private int errorCode = 200;

        public ComposerService ComposerService => composerService;

        public WorkService WorkService => workService;

        public EraService EraService => eraService;

        public string Message { get => message; set => message = value; }
        public int ErrorCode { get => errorCode; set => errorCode = value; }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public WSResponse GetComposers()
        {
            List<ComposerDto> composers = ComposerService.GetAll().Select(d => new ComposerDto(d)).ToList();

            if(composers == null)
            {
                NotFound();
            }

            return new WSResponse(Message, ErrorCode, composers);
        }

        [WebMethod]
        public WSResponse GetComposer(int? id)
        {
            if (id == null || id <= 0)
            {
                BadRequest();
            }

            var composer = ComposerService.GetByID((int)id);
            if (composer == null)
            {
                Message = "Not found";
                ErrorCode = 404;
            }
            return new WSResponse(Message, ErrorCode, new ComposerDto(composer));
        }

        [WebMethod]
        public WSResponse GetComposerByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                BadRequest();
            var composer = composerService.GetComposerByName(name);
            if (composer == null)
                NotFound();
            return new WSResponse(Message, ErrorCode, new ComposerDto(composer));
        }

        [WebMethod]
        public WSResponse GetEras()
        {
            List<EraDto> eras = EraService.GetAll().Select(d => new EraDto(d)).ToList();

            if (eras == null)
            {
                NotFound();
            }

            return new WSResponse(Message, ErrorCode, eras);
        }

        [WebMethod]
        public WSResponse GetEra(int? id)
        {
            if (id == null || id <= 0)
            {
                BadRequest();
            }

            var era = EraService.GetByID((int)id);
            if (era == null)
            {
                Message = "Not found";
                ErrorCode = 404;
            }
            return new WSResponse(Message, ErrorCode, new EraDto(era));
        }

        [WebMethod]
        public WSResponse GetWorks()
        {
            List<WorkDto> works = WorkService.GetAll().Select(d => new WorkDto(d)).ToList();

            if (works == null)
            {
                NotFound();
            }

            return new WSResponse(Message, ErrorCode, works);
        }

        [WebMethod]
        public WSResponse GetWork(int? id)
        {
            if (id == null || id <= 0)
            {
                BadRequest();
            }

            var work = WorkService.GetByID((int)id);
            if (work == null)
            {
                Message = "Not found";
                ErrorCode = 404;
            }
            return new WSResponse(Message, ErrorCode, new WorkDto(work));
        }

        private void BadRequest()
        {
            Message = "Bad request!";
            ErrorCode = 400;
        }

        private void NotFound()
        {
            Message = "Not found";
            ErrorCode = 404;
        }
    }
}
