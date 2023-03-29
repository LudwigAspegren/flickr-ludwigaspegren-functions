using System.Threading.Tasks;
using Ludwigaspegren_functions_app.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace Ludwigaspegren_functions_app
{
    public class GetPhotosets
    {
        private readonly IFlickrService _flickrService;

        public GetPhotosets(IFlickrService flickrService)
        {
            _flickrService = flickrService;
        }
        
        [FunctionName("getPhotosets")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetPhotoset/{id}")] HttpRequest req, string id,
            ILogger log)
        {
            var photosets = await _flickrService.GetPhotosetImageLinks(id);
                
            if (photosets == null)
            {
                return new NotFoundObjectResult(null);
            }
            return new OkObjectResult(photosets);
        }
    }
}
