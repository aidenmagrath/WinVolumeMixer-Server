using System.Collections.Generic;
using System.Web.Http;
using System;

namespace WinVolumeMixer.Server
{
    public class ApplicationController : ApiController
    {
        public IEnumerable<Application> Get()
        {
            ApplicationManager.getManager().UpdateApplications();
            return ApplicationManager.getManager().GetApplications();
        }

        public IHttpActionResult Get(int id)
        {
            Application app = ApplicationManager.getManager().GetApplication(id);
            if (app == null)
            {
                return NotFound();
            }

            return Ok(app);
        }

        public IHttpActionResult Put([FromBody]Application application)
        {
            VolumeManager.getManager().SetVolume(application.Id, application.Volume);
            VolumeManager.getManager().SetMuted(application.Id, application.Muted);
            ApplicationManager.getManager().UpdateApplications();
            return Ok();
        }
    }
}