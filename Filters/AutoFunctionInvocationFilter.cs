using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.


namespace QuestionnaireMultiagent.Filters
{
    public sealed class AutoFunctionInvocationFilter() : IAutoFunctionInvocationFilter
        {
            public async Task OnAutoFunctionInvocationAsync(AutoFunctionInvocationContext context, Func<AutoFunctionInvocationContext, Task> next)
            {
                // Example: Get function information
                var functionName = context.Function.Name;

                // Example: Get chat history
                var chatHistory = context.ChatHistory;

                // Example: Get information about all functions which will be invoked
                var functionCalls = FunctionCallContent.GetFunctionCalls(context.ChatHistory.Last());

                // Example: Get request sequence index
                Console.WriteLine($"Request sequence index: {context.RequestSequenceIndex}");

                // Example: Get function sequence index
                Console.WriteLine($"Function sequence index: {context.FunctionSequenceIndex}");

                // Example: Get total number of functions which will be called
                Console.WriteLine($"Total number of functions: {context.FunctionCount}");

                // Calling next filter in pipeline or function itself.
                // By skipping this call, next filters and function won't be invoked, and function call loop will proceed to the next function.
                await next(context);

                // Example: get function result
                var result = context.Result;

                // Example: override function result value
                // context.Result = new FunctionResult(context.Result, "Result from auto function invocation filter");

                //// Example: Terminate function invocation
                //context.Terminate = true;
            }
        }
}
