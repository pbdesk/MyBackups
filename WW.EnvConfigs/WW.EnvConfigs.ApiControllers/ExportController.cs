using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WW.EnvConfigs.Utils;


namespace  WW.EnvConfigs.ApiControllers
{
    public class ExportController : ApiController
    {

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]EIParameters parameters)
        {
            HttpResponseMessage response = null;
              if( parameters != null)
              {
                  try
                  {
                      EIHelper.ValidateEIParameters(parameters);
                  }
                  catch(Exception ex)
                  {
                      response = Request.CreateResponse(HttpStatusCode.BadRequest, new Exception("Error while reading EIParameters.", ex));
                  }
                  try
                  {
                      Export.RunExport(parameters);
                      response = Request.CreateResponse(HttpStatusCode.OK);
                  }
                  catch(Exception ex)
                  {
                      response = Request.CreateResponse(HttpStatusCode.BadRequest, new Exception("Error while exporting.", ex));
                  }

              }
            else
              {
                  response = Request.CreateResponse(HttpStatusCode.BadRequest);
              }
            return response;
        }


    }
}