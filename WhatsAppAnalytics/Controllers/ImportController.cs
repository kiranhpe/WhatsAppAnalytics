using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendLogic.Features.ImportMessage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WhatsAppAnalyticsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {

        private readonly IImportService m_importService;

        public ImportController(IImportService importService)
        {
            m_importService = importService;
        }

        // GET: api/Import
        [HttpGet]
        public IActionResult Get()
        {
            var data = m_importService.GetImports();
            return Ok(data);
        }

        // GET: api/Import/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "";
        }

        // POST: api/Import
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                return Ok(m_importService.InsertImports());
            }
            catch(Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        // PUT: api/Import/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
