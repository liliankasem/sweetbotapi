using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SweetBotAPI.Models;
using Swashbuckle.Swagger.Annotations;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.IO;
using System.Web;


namespace SweetBotAPI.Controllers
{
    public class ValuesController : ApiController
    {
        static List<Values> _data = new List<Values>();

        // GET api/values
        [SwaggerOperation("GetAll")]
        public IEnumerable<string> Get()
        {
            var valuesData = _data.Find(o => o.id == 0);
            return new string[] { valuesData.score.ToString(), valuesData.user };
            
        }

        // GET api/values/ add | minus | reset
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public int Get(string id)
        {
            var valuesData = _data.Find(o => o.id == 0);
            var oldScore = valuesData.score;        

            if (id == "reset")
            {
                valuesData.score = 0;
            }
            else if (id == "add")
            {
                valuesData.score++;
            }
            else if (id == "minus")
            {
                if (valuesData.score != 0)
                {
                    valuesData.score--;
                    return oldScore;
                }
            }

            return valuesData.score;
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public IHttpActionResult Post([FromBody]Values data)
        {
            var valuesData = _data.Find(o => o.id == 0);

            if (valuesData == null)
            {
                //create it
                _data.Add(new Values()
                {

                    score = 0,
                    user = "Lilian"
                });

            }
            else
            {
                //update it          
                valuesData.score = data.score;
                valuesData.user = data.user;
            }

            return Created("/api/values", data);
        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Put(int id, [FromBody]Values data)
        {
            var valuesData = _data.Find(o => o.id == 0);
            valuesData.user = data.user;

            return Ok();
        }

    }
}
