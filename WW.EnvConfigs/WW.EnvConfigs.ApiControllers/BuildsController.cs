using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WW.EnvConfigs.DataModels;

namespace WW.EnvConfigs.ApiControllers
{
    public class BuildsController   : BaseApiController
    {
        public HttpResponseMessage Get()
        {
            return GenericGet<Build>();
            //HttpResponseMessage response = null;
            //try
            //{
            //    var allKeys = Repo.Builds.GetAll<Build>().ToList<Build>();
            //    response = Request.CreateResponse(HttpStatusCode.OK, allKeys);
            //}
            //catch (Exception ex)
            //{
            //    Log("Error in BuildsController.Get", ex);
            //    response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            //}
            //return response;
        }

        public HttpResponseMessage Post([FromBody]Build newBuild)
        {
            return GenericPost<Build>(newBuild);
            //HttpResponseMessage response = null;
            //if (newBuild != null)
            //{
            //    try
            //    {
            //        UpdateAuditInfo(newBuild);
            //        var newlyCreatedBuild = Repo.InsertBuildWithBlankValues(newBuild, User.Identity.Name);
            //        response = Request.CreateResponse(HttpStatusCode.Created, newlyCreatedBuild);
            //    }
            //    catch (Exception ex)
            //    {
            //        Log("Error in BuildsController.Post", ex);
            //        response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            //    }
            //}
            //else
            //{
            //    Log("Null argument in call to BuildsController.Post");
            //    response = Request.CreateResponse(HttpStatusCode.BadRequest);
            //}

            //return response;
        }

        public HttpResponseMessage Put(int id, Build build)
        {
            return GenericPut<Build>(id, build);
            //HttpResponseMessage response = null;
            //if (id > 0 && build != null)
            //{
            //    Build l = null;
            //    try
            //    {
            //         l = Repo.Builds.Find<Build>(id);
            //    }
            //    catch(Exception ex)
            //    {
            //        Log("Error in BuildsController.Put() While making database call(Find<Build>).", ex);
            //        response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            //        return response;
            //    }
            //    if (l == null || id != build.Id)
            //    {
            //        response = Request.CreateResponse(HttpStatusCode.NotFound);
            //    }
            //    try
            //    {
            //        UpdateAuditInfo(build);
            //        Repo.Builds.Update<Build>(build);
            //        response = Request.CreateResponse(HttpStatusCode.OK, build);
            //    }
            //    catch (Exception ex)
            //    {
            //        Log("Error in BuildsController.Put() While making database call(Update<Build>).", ex);
            //        response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            //    }

                
            //}
            //else
            //{
            //    Log("Null argument in call to BuildsController.Post");
            //    response = Request.CreateResponse(HttpStatusCode.BadRequest);
            //}
            //return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            return GenericDelete<Build>(id);
            //if (id > 0)
            //{
            //    Build s = null;
            //    try
            //    {
            //        s = Repo.Builds.Find<Build>(id);
            //    }
            //    catch(Exception ex)
            //    {
            //       Log("Error in BuildsController.Delete() While making database call(Find<Build>).", ex);
            //       return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            //    }
            //    if (s == null)
            //    {
            //        return Request.CreateResponse(HttpStatusCode.NotFound);
            //    }

            //    try
            //    {
            //        Repo.Builds.Delete<Build>(s);
            //        return Request.CreateResponse(HttpStatusCode.OK, s);
            //    }
            //    catch (Exception ex)
            //    {
            //        Log("Error in BuildsController.Delete() While making database call(Delete<Build>).", ex);
            //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            //    }

               
            //}
            //else
            //{
            //    Log("Null argument in call to BuildsController.Delete");
            //    return Request.CreateResponse(HttpStatusCode.BadRequest);
            //}
        }
    }
}