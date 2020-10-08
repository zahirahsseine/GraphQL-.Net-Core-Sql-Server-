using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphqlIt.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using GraphQL.Types;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace GraphqlIt.Controllers
{
    [Route("[controller]")]

    public class GraphQlController : Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExecuter;

        public GraphQlController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
        }


        [HttpPost]

        public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            //?.ToObject<Dictionary<string,object>>()
            var inputs = query.Variables?.ToObject<Dictionary<string, object>>();
            
            //// inputs = JsonConvert.DeserializeObject<Dictionary<string, object>>(inputs?.ToString());
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs.ToInputs()

                };

            var result = await _documentExecuter.ExecuteAsync(executionOptions);

            
            if(result.Errors?.Count>0)
            {
                return BadRequest(result);
            }

            return Ok(result);
       // var inputs
        }
       

    }
}
