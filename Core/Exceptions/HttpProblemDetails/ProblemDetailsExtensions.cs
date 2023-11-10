﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.Exceptions.HttpProblemDetails
{
    internal static class ProblemDetailsExtensions
    {
        public static string AsJson(this ProblemDetails details)
        {
            return JsonConvert.SerializeObject(details);
        }
    }
}
