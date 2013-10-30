using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WW.EnvConfigs.DataModels;

namespace WW.EnvConfigs.ApiControllers
{
    public class LocalesController : BaseApiController
    {
        public HttpResponseMessage Get()
        {
            return GenericGet<Locale>();
            //HttpResponseMessage response = null;
            //try
            //{
            //    var locales = Repo.Locales.GetAll<Locale>().OrderBy(t => t.SiteId);
            //    response = Request.CreateResponse(HttpStatusCode.OK, locales);
            //}
            //catch (Exception ex)
            //{
            //    Log("Error in LocalesController.Get() while making database call(GetAll<Locale>).", ex);
            //    response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError,ex);
            //}
            //return response;

        }

        public HttpResponseMessage Post([FromBody]Locale newLocale)
        {
            return GenericPost<Locale>(newLocale);
            //HttpResponseMessage response = null;
            //if (newLocale != null)
            //{
            //    try
            //    {
            //        UpdateAuditInfo(newLocale);
            //        var newlyCreatedLocale = Repo.InsertLocaleWithBlankValues(newLocale, User.Identity.Name);
            //        response = Request.CreateResponse(HttpStatusCode.Created, newlyCreatedLocale);
            //    }
            //    catch (Exception ex)
            //    {
            //        Log("Error in LocalesController.Get() while making database call(Insert<Locale>).", ex);
            //        response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //    }
            //}
            //else
            //{
            //    Log("Null argument in call to LocalesController.Post");
            //    response = Request.CreateResponse(HttpStatusCode.BadRequest);
            //}
            //return response;
        }

        public HttpResponseMessage Put(int id, Locale locale)
        {
            return GenericPut<Locale>(id, locale);
            //Locale l = Repo.Locales.Find<Locale>(id);
            //if (l == null || id != locale.Id)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            //try
            //{
            //    UpdateAuditInfo(locale);
            //    Repo.Locales.Update<Locale>(locale);
            //}
            //catch (Exception ex)
            //{
            //    Log("Error in LocalesController.Put() while making database call(Update<Locale>).", ex);
            //    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //}

            //return Request.CreateResponse(HttpStatusCode.OK, locale);

        }

        public HttpResponseMessage Delete(int id)
        {
            return GenericDelete<Locale>(id);
            //Locale s = Repo.Locales.Find<Locale>(id);
            //if (s == null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}

            //try
            //{
            //    Repo.Locales.Delete<Locale>(s);

            //}
            //catch (Exception ex)
            //{
            //    Log("Error in LocalesController.Delete() while making database call(Delete<Locale>).", ex);
            //    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //}

            //return Request.CreateResponse(HttpStatusCode.OK, s);
        }
        
        
    }
}