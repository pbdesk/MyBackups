using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WW.EnvConfigs.DataModels;

namespace WW.EnvConfigs.ApiControllers
{
    public class KeysController : BaseApiController
    {


        public HttpResponseMessage Get()
        {
            return GenericGet<EnvKey>();
            //HttpResponseMessage response = null;
            //try
            //{
            //    var allKeys = Repo.EnvKeys.GetAll<EnvKey>();
            //    response = Request.CreateResponse(HttpStatusCode.OK, allKeys);
            //}
            //catch (Exception ex)
            //{

            //    response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //}
            //return response;

        }

        public HttpResponseMessage Get(int id)
        {   
            // id = WWFrameworkId
            HttpResponseMessage response = null;
            try
            {
                var allKeys = Repo.EnvKeys.Filter<EnvKey>(p => p.WWFrameworkId == id,null, "WWFramework");
                response = Request.CreateResponse(HttpStatusCode.OK, allKeys);
            }
            catch (Exception ex)
            {

                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            return response;

        }


        public HttpResponseMessage Post([FromBody]EnvKey newKey)
        {
            if(newKey != null)
            {
                newKey.CreateDate = DateTime.Today;
            }
            return GenericPost<EnvKey>(newKey);
            //if (newKey != null)
            //{       
            //    try
            //    {
            //        UpdateAuditInfo(newKey);
            //        newKey.CreateDate = DateTime.Today;
            //        var newInsertedKey = Repo.InsertKeyWithBlankValues(newKey, User.Identity.Name);
            //        if (newInsertedKey != null && newInsertedKey.Id > 0)
            //        {
            //            return Request.CreateResponse(HttpStatusCode.Created, newInsertedKey);
            //        }
            //        else
            //        {
            //            string errMsg = "Error in KeysController.Post(). Insert returned null as newly created key";
            //            Log(errMsg);
            //            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,new Exception(errMsg));
            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        Log("Error in KeysController.Post() while making database call(InsertKeyWithBlankValues()).", ex);
            //        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //    }
                
            //}
            //else
            //{
            //    Log("Null argument in call to KeysController.Post()");
            //   return Request.CreateResponse(HttpStatusCode.BadRequest);
            //}
            
        }

        public HttpResponseMessage Put(int id, [FromBody]EnvKey envKey)
        {
            return GenericPut<EnvKey>(id, envKey);
            //HttpResponseMessage response = new HttpResponseMessage();
            //if (id > 0 && envKey != null && envKey.Id == id)
            //{
            //    EnvKey e = Repo.EnvKeys.Find<EnvKey>(id);
            //    if (e != null)
            //    {
            //        Repo.EnvKeys.Update<EnvKey>(envKey);
            //        response = Request.CreateResponse(HttpStatusCode.OK, envKey);
            //    }
            //    else
            //    {
            //        response.StatusCode = HttpStatusCode.BadRequest;
            //    }

            //}
            //else
            //{
            //    response.StatusCode = HttpStatusCode.BadRequest;
            //}

            //return response;

        }

        public HttpResponseMessage Delete(int id)
        {
            return GenericDelete<EnvKey>(id);
            //EnvKey v = Repo.EnvKeys.Find<EnvKey>(id);
            //if (v != null)
            //{
            //    Repo.EnvKeys.Delete<EnvKey>(v);
            //    return Request.CreateResponse(HttpStatusCode.OK, v);
            //}
            //return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

       
    }
}