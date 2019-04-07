using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MDPatientClusterer.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MDPatientClusterer.Controllers.Patient_Groups
{
    [Route("api/Patient-Groups/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] JObject jObject)
        {
            //ClusterManager manager = new ClusterManager();                  //declaring the patient manager class.
            //int patientGroups = manager.GetClusters(jObject);               //passing the json object to getclusters function to find clusters.
            //return Ok(new { numberOfGroups = patientGroups });              //returning the result.
            return Ok();
        }
    }   
    
}