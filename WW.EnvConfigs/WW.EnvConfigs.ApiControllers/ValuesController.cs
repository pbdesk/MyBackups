using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WW.EnvConfigs.DataModels;

namespace WW.EnvConfigs.ApiControllers
{
    public class ValuesController : BaseApiController
    {
        [HttpGet]
        public IQueryable<EnvValue> ByKeyId(int id)
        {
            return Repo.EnvValues.Filter<EnvValue>(p => p.EnvKeyId == id, q => q.OrderBy(s => s.BuildId), "EnvKey,Locale, Build");
        }

        [HttpPut]
        public HttpResponseMessage Update(int id, [FromBody]EnvValue val)
        {
            return GenericPut<EnvValue>(id, val);
            //HttpResponseMessage response = new HttpResponseMessage();
            //if (id > 0 && val != null && val.Id == id)
            //{
            //    EnvValue e = Repo.EnvValues.Find<EnvValue>(id);
            //    if (e != null)
            //    {
            //        Repo.EnvValues.Update<EnvValue>(val);
            //        response = Request.CreateResponse(HttpStatusCode.OK, val);
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

        [HttpPut]
        public HttpResponseMessage UpdateAll([FromBody] List<EnvValue> vals)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            int results = 0;
            if (vals != null && vals.Count() > 0)
            {
                try
                {
                    foreach (EnvValue val in vals)
                    {
                        EnvValue e = Repo.EnvValues.Find<EnvValue>(val.Id);
                        if (e != null)
                        {
                            Repo.EnvValues.UpdateLite<EnvValue>(val);
                        }
                    }
                    results = Repo.SaveChanges();
                    response = Request.CreateResponse(HttpStatusCode.OK, results);
                }
                catch (Exception ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

        [HttpPost]
        public HttpResponseMessage Create([FromBody]EnvValue newEnvValue)
        {
            if (newEnvValue != null)
            {
                if (newEnvValue.EnvKeyId > 0 && newEnvValue.BuildId > 0 && newEnvValue.LocaleId > 0)
                {
                    newEnvValue = Repo.EnvValues.Insert<EnvValue>(newEnvValue);
                    if (newEnvValue != null && newEnvValue.Id > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newEnvValue);
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            return GenericDelete<EnvValue>(id);
            //EnvValue v = Repo.EnvValues.Find<EnvValue>(id);
            //if (v != null)
            //{
            //    Repo.EnvValues.Delete<EnvValue>(v);
            //    return Request.CreateResponse(HttpStatusCode.OK, v);
            //}
            //return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}