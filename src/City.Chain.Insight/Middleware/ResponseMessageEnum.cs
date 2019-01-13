using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace City.Chain.Insight.Middleware
{
    public enum ResponseMessageEnum
    {
        [Description("Success")]
        Success,

        [Description("Request responded with exceptions")]
        Exception,

        [Description("Request denied")]
        UnAuthorized,

        [Description("Request responded with validation error(s)")]
        ValidationError,

        [Description("Unable to process the request")]
        Failure
    }
}
