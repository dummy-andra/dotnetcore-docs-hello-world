

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;

namespace dotnetcoresample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
    
        public string OSVersion { get { return RuntimeInformation.OSDescription; } }
        public string Message { get; private set; }
    
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    
        public void OnGet()
        {
            var environmentStage = _configuration["environment_stage"];
    
            if (!string.IsNullOrEmpty(environmentStage))
            {
                Message = _configuration["Message"];
            }
            else
            {
                Message = "Hello, World!";
            }
        }
    }
}







// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.Extensions.Configuration;
// using System.Runtime.InteropServices;

// namespace dotnetcoresample.Pages
// {
//     public class IndexModel : PageModel
//     {
//         private readonly IConfiguration _configuration;

//         public string OSVersion { get { return RuntimeInformation.OSDescription; } }

//         public string Message { get; private set; }

//         public IndexModel(IConfiguration configuration)
//         {
//             _configuration = configuration;
//         }

//         public void OnGet()
//         {
//             // Read the value of environment_stage from the configuration
//             var environmentStage = _configuration["environment_stage"];

//             // Check if the environment_stage is "SIT" and set the message accordingly
//             if (environmentStage == "SIT")
//             {
//                 Message = "We are set to use SIT as environment💜";
//             }
//             else
//             {
//                 Message = "Hello, World!";
//             }
//         }
//     }
// }








// ﻿using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using System.Runtime.InteropServices;

// namespace dotnetcoresample.Pages;

// public class IndexModel : PageModel
// {

//     public string OSVersion { get { return RuntimeInformation.OSDescription; }  }
    
//     private readonly ILogger<IndexModel> _logger;

//     public IndexModel(ILogger<IndexModel> logger)
//     {
//         _logger = logger;
//     }

//     public void OnGet()
//     {        
//     }
// }
