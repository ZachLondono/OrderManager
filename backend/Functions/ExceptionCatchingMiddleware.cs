/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Functions;

internal class ExceptionCatchingMiddleware : IFunctionsWorkerMiddleware {
    
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next) {
        try {
            
            await next(context);

        } catch (Exception ex) {

            var log = context.GetLogger<ExceptionCatchingMiddleware>();
            log.LogWarning(ex, string.Empty);
            
            var req = context.GetHttpRequestData();
            var res = req.CreateResponse();
            res.StatusCode = System.Net.HttpStatusCode.InsufficientStorage;
            context.InvokeResult(res);
            return;

        }
    }

}

internal static class FunctionUtilities {
    internal static HttpRequestData GetHttpRequestData(this FunctionContext context) {
        var keyValuePair = context.Features.SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");
        var functionBindingsFeature = keyValuePair.Value;
        var type = functionBindingsFeature.GetType();
        var inputData = type.GetProperties().Single(p => p.Name == "InputData").GetValue(functionBindingsFeature) as IReadOnlyDictionary<string, object>;
        return inputData?.Values.SingleOrDefault(o => o is HttpRequestData) as HttpRequestData;
    }

    internal static void InvokeResult(this FunctionContext context, HttpResponseData response) {
        var keyValuePair = context.Features.SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");
        var functionBindingsFeature = keyValuePair.Value;
        var type = functionBindingsFeature.GetType();
        var result = type.GetProperties().Single(p => p.Name == "InvocationResult");
        result.SetValue(functionBindingsFeature, response);
    }
}
*/