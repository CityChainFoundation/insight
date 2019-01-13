using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace City.Chain.Insight.Middleware
{
    public class ApiError
    {
        [JsonProperty("error")]
        public bool IsError { get; set; }

        [JsonProperty("message")]
        public string ExceptionMessage { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("referenceErrorCode")]
        public string ReferenceErrorCode { get; set; }

        [JsonProperty("referenceDocumentLink")]
        public string ReferenceDocumentLink { get; set; }

        [JsonProperty("validationErrors")]
        public IEnumerable<ValidationError> ValidationErrors { get; set; }

        public ApiError(string message)
        {
            this.ExceptionMessage = message;
            this.IsError = true;
        }

        public ApiError(ModelStateDictionary modelState)
        {
            this.IsError = true;

            if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
            {
                this.ExceptionMessage = "Please correct the specified validation errors and try again.";
                this.ValidationErrors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();
            }
        }
    }
}
