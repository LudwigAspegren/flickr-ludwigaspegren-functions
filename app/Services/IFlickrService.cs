using System.Collections.Generic;
using System.Threading.Tasks;
using Ludwigaspegren_functions_app.ViewModels;

namespace Ludwigaspegren_functions_app.Services
{
    public interface IFlickrService
    {
        Task<IEnumerable<IEnumerable<PhotoViewModel>>> GetPhotos(string userId);
        Task<PhotosetViewModel> GetPhotosetImageLinks(string photsetId);
        Task<PhotosetViewModel> GetPhotosetInfo(string photsetId);
    }
}