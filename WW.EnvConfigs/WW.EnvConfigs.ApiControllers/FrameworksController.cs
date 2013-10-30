using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WW.EnvConfigs.DataModels;

namespace WW.EnvConfigs.ApiControllers
{
    public class FrameworksController : BaseApiController
    {
        public HttpResponseMessage Get()
        {
            return GenericGet<WWFramework>();
            //HttpResponseMessage response = null;
            //try
            //{
            //    var items = Repo.WWFrameworks.GetAll<WWFramework>();
            //    response = Request.CreateResponse(HttpStatusCode.OK, items);
            //}
            //catch (Exception ex)
            //{
            //    Log("Error in FrameworksController.Get", ex);
            //    response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            //}
            //return response;
        }

        public HttpResponseMessage Post([FromBody]WWFramework newObject)
        {
            return GenericPost<WWFramework>(newObject);
            //HttpResponseMessage response = null;
            //try
            //{
            //    var newlyCreatedObject = Repo.WWFrameworks.Insert<WWFramework>(newObject);
            //    response = Request.CreateResponse(HttpStatusCode.Created, newlyCreatedObject);
            //}
            //catch (Exception ex)
            //{
            //    response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //}
            //return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Put(int id, WWFramework obj)
        {
            return GenericPut<WWFramework>(id, obj);
            //WWFramework item = Repo.WWFrameworks.Find<WWFramework>(id);
            //if (item == null || id != obj.Id)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            //try
            //{
            //    Repo.WWFrameworks.Update<WWFramework>(obj);
            //}
            //catch (Exception ex)
            //{
            //    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //}

            //return Request.CreateResponse(HttpStatusCode.OK, obj);

        }

        public HttpResponseMessage Delete(int id)
        {
            return GenericDelete<WWFramework>(id);
            //WWFramework item = Repo.WWFrameworks.Find<WWFramework>(id);
            //if (item == null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}

            //try
            //{
            //    Repo.WWFrameworks.Delete<WWFramework>(item);

            //}
            //catch (Exception ex)
            //{
            //    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //}

            //return Request.CreateResponse(HttpStatusCode.OK, item);
        }
    }
}