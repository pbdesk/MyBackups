using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WW.EnvConfigs.DataModels;

namespace WW.EnvConfigs.ApiControllers
{
    public class AppsController : BaseApiController
    {
        public HttpResponseMessage Get()
        {
            return GenericGet<WWApp>();
            //HttpResponseMessage response = null;
            //try
            //{
            //    var allKeys = Repo.WWApps.GetAll<WWApp>();
            //    response = Request.CreateResponse(HttpStatusCode.OK, allKeys);
            //}
            //catch (Exception ex)
            //{
            //    response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //}
            //return response;
        }

        public HttpResponseMessage Post([FromBody]WWApp newObject)
        {
            return GenericPost<WWApp>(newObject);
            //HttpResponseMessage response = null;
            //try
            //{
            //    var newlyCreatedObject = Repo.WWApps.Insert<WWApp>(newObject);
            //    response = Request.CreateResponse(HttpStatusCode.Created, newlyCreatedObject);
            //}
            //catch (Exception ex)
            //{
            //    response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //}
            //return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Put(int id, WWApp appObj)
        {
            return GenericPut<WWApp>(id, appObj);
            //WWApp item = Repo.WWApps.Find<WWApp>(id);
            //if (item == null || id != obj.Id)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            //try
            //{
            //    Repo.WWApps.Update<WWApp>(obj);
            //}
            //catch (Exception ex)
            //{
            //    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //}

            //return Request.CreateResponse(HttpStatusCode.OK, obj);

        }

        public HttpResponseMessage Delete(int id)
        {
            return GenericDelete<WWApp>(id);
            //WWApp item = Repo.WWApps.Find<WWApp>(id);
            //if (item == null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}

            //try
            //{
            //    Repo.WWApps.Delete<WWApp>(item);

            //}
            //catch (Exception ex)
            //{
            //    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //}

            //return Request.CreateResponse(HttpStatusCode.OK, item);
        }
    }


}