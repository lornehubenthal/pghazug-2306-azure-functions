using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PghAzUg.FunctionAppDemo.CSharp.Models;

namespace PghAzUg.FunctionAppDemo.CSharp;

public static class GetCrewMembers
{
    [OpenApiOperation("Get Crew Member by Job", "Crew Management", Description = "Gets a crew member processed by a job")]
    [OpenApiParameter("jobId", Required = true, Description = "The job id of the job to retrieve the crew.", Type = typeof(Guid), In = ParameterLocation.Path)]
    [OpenApiParameter("name", Required = true, Description = "The name of the crew member to retrieve.", Type = typeof(string), In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Entity))]
    [OpenApiResponseWithBody(HttpStatusCode.InternalServerError, "plain/text", typeof(string))]
    [Function("GetCrewMembers")]
    public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "crew/{jobId}/{name}")] HttpRequestData req, Guid jobId, string name,
        [BlobInput("processed/{jobId}/{name}.json")] Entity data,
        FunctionContext executionContext)
    {
        ILogger logger = executionContext.GetLogger("GetCrewMembers");
        logger.LogInformation("C# HTTP trigger function processed a request");

        HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteAsJsonAsync(data);

        return response;
    }
}
