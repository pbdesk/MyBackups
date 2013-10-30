using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WW.EnvConfigs.DAL.Contexts;
using WW.EnvConfigs.DAL;
using WW.EnvConfigs.DataModels;
using PBDesk.EFRepository;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace WW.EnvConfigs.ApiControllers
{
    public class BaseApiController : ApiController
    {
        protected RepoHelper Repo = new RepoHelper();

        protected void Log(Exception ex)
        {
            if (ex != null)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        protected void Log(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Log(new Exception(message));
            }
        }

        protected void Log (string message, Exception ex)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                Log(ex);
            }
            else
            {
                Log(new Exception(message, ex));
            }
        }

        protected void UpdateAuditInfo(Entity obj)
        {
            Repo.UpdateAuditInfo(obj, User.Identity.Name);
        }

        protected HttpResponseMessage GenericGet<T>()   where T : IEntity
        {
            HttpResponseMessage response = null;
            
            try
            {
                IQueryable<T> items = Repo.GetReposiotry<T>().GetAll<T>();
                response = Request.CreateResponse(HttpStatusCode.OK, items);                 
            }
            catch (Exception ex)
            {
                Log(string.Format("Error in BaseApiController.GenericGet<{0}>()", typeof(T).Name), ex);
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return response;
        }

        protected HttpResponseMessage GenericGet<T>(int id) where T : IEntity
        {
            HttpResponseMessage response = null;

            try
            {
                T item = Repo.GetReposiotry<T>().GetSingle<T>(id);
                if (item != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, item);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                Log(string.Format("Error in BaseApiController.GenericGet<{0}>(id)", typeof(T).Name), ex);
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return response;
        }
        protected HttpResponseMessage GenericPost<T>([FromBody]T newItem) where T : Entity
        {
            HttpResponseMessage response = null;
            if (newItem != null)
            {
                try
                {
                    UpdateAuditInfo(newItem);
                    T newlyCreatedItem = Repo.GetReposiotry<T>().Insert<T>(newItem);
                    response = Request.CreateResponse(HttpStatusCode.OK, newlyCreatedItem);
                }
                catch (Exception ex)
                {
                    Log(string.Format("Error in BaseApiController.Post() while making database call(Insert<{0}>).", typeof(T).Name), ex);
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                Log(string.Format("Null argument in call to BaseApiController.GenericPost<{0}>",typeof(T).Name));
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return response;
        }

        public HttpResponseMessage GenericPut<T>(int id, T item) where T : Entity
        {
            HttpResponseMessage response = null;
            if (id > 0 && item != null)
            {
                T tObj = default(T);
                try
                {
                    tObj = Repo.GetReposiotry<T>().Find<T>(id);
                }
                catch (Exception ex)
                {
                    Log( string.Format("Error in BaseApiController.GenericPut<{0}>() While making database call(Find<{0}>).", typeof(T).Name), ex);
                    response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                    return response;
                }
                if (tObj == null || id != tObj.Id)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    try
                    {
                        UpdateAuditInfo(item);
                        Repo.GetReposiotry<T>().Update<T>(item);
                        response = Request.CreateResponse(HttpStatusCode.OK, item);
                    }
                    catch (Exception ex)
                    {
                        Log(string.Format("Error in BaseApiController.GenericPut<{0}>() while making database call(Update<{0}>).", typeof(T).Name), ex);
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                    }
                }
            }
            else
            {
                Log(string.Format("Null argument in call to BaseApiController.GenericPut<{0}>", typeof(T).Name));
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return response;

        }

        protected HttpResponseMessage GenericDelete<T>(int id) where     T: IEntity
        {
            if (id > 0)
            {
                T tObj = default(T);
                try
                {
                    tObj = Repo.GetReposiotry<T>().Find<T>(id);
                }
                catch (Exception ex)
                {
                    Log(string.Format("Error in BaseApiController.GenericDelete<{0}>() While making database call(Find<{0}>).", typeof(T).Name ), ex);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
                if (tObj == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    try
                    {
                        Repo.GetReposiotry<T>().Delete<T>(tObj);
                        return Request.CreateResponse(HttpStatusCode.OK, tObj);
                    }
                    catch (Exception ex)
                    {
                        Log(string.Format("Error in BaseApiController.GenericDelete<{0}>() while making database call(Delete<Locale>).", typeof(T).Name), ex);
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                    }
                }


            }
            else
            {
                Log(string.Format("Null argument in call to BaseApiController.GenericDelete<{0}>(T)>", typeof(T).Name));
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
           
        }
    }
}