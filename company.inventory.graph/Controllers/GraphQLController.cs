using System;
using System.Linq;
using System.Threading.Tasks;
using company.inventory.graph.Models;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace company.inventory.graph.Controllers
{
    [Route("graphql")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly IDocumentExecuter documentExecutor;
        private readonly ISchema schema;
        private readonly DataLoaderDocumentListener listener;
        private readonly bool showExceptions;

        public GraphQLController(IDocumentExecuter documentExecutor,
            ISchema schema,
            DataLoaderDocumentListener listener,
            IHostingEnvironment env)
        {
            this.documentExecutor = documentExecutor;
            this.schema = schema;
            this.listener = listener;
            showExceptions = env.IsDevelopment();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var executeOptions = new ExecutionOptions
            {
                Schema = schema,
                Query = request.Query,
                Inputs = request.Variables.ToInputs(),
                Listeners = { listener },
                ExposeExceptions = showExceptions
            };

            var result = await documentExecutor.ExecuteAsync(executeOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0) return BadRequest(result.Errors.Select(e => e.Message));

            return Ok(result.Data);
        }
    }
}