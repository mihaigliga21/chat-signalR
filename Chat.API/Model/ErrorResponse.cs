using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Model
{
    public class ErrorResponse
    {
        /// <summary>
        /// Error response parameterless constructor
        /// </summary>
        public ErrorResponse() { }

        /// <summary>
        /// Error response constructor with parameter
        /// </summary>
        /// <param name="error">A bad request model error</param>
        public ErrorResponse(ErrorModel error)
        {
            Errors.Add(error);
        }

        /// <summary>
        /// List of errors
        /// </summary>
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
