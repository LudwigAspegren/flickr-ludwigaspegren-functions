using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedObjects.DTOs;
using SharedObjects.Utils;
using Ludwigaspegren_functions_app.ViewModels;

namespace Ludwigaspegren_functions_app.Services
{
    public class FlickrService : IFlickrService
    {
        private readonly HttpClient _httpClient;

        public FlickrService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PhotosetViewModel> GetPhotosetImageLinks(string photsetId)
        {
            string queryParams = null;
            queryParams += "method=flickr.photosets.getPhotos&";
            queryParams += $"photoset_id={photsetId}&";
            Response response = await GetResponse<Response>(queryParams);

            IEnumerable<PhotoViewModel> photos = null;
            if (response.Photoset.Photo != null)
            {
                photos = response.Photoset.Photo.Select(p =>
                {
                    var pvm = p.MapToPhotoViewModel();
                    pvm.Album = response.Photoset.Id;
                    return pvm;
                });
            }
            var photosetInfo = GlobalVariables.Pages.FirstOrDefault(p => p.Key == photsetId);
            PhotosetViewModel photosetViewModel = null;
            if (photos != null)
            {
                photosetViewModel = new PhotosetViewModel
                {
                    Photos = photos.ToArray(),
                    Title = photosetInfo.Value,
                    Id = photosetInfo.Key,

                
                };
                Console.WriteLine(photosetViewModel.Title);
            }
            return photosetViewModel;
        }

        public async Task<PhotosetViewModel> GetPhotosetInfo(string photsetId)
        {
            string queryParams = null;
            queryParams += "method=flickr.photosets.getPhotos&";
            queryParams += $"photoset_id={photsetId}&";
            Response response = await GetResponse<Response>(queryParams);
            Console.WriteLine(response.Photoset);
            return new();

        }
            public async Task<IEnumerable<PhotoDto>> GetPhotosFromUser(string userId)
        {
            PhotosetDto photoset = new();
            string queryParams = null;
            queryParams += "method=flickr.people.getPhotos&";
            queryParams += $"user_id={userId}&";
            Response response = await GetResponse<Response>(queryParams);
            photoset.Photo = response.Photos.Photo;
            return response.Photos.Photo;
        }

        public async Task<IEnumerable<IEnumerable<PhotoViewModel>>> GetPhotos(string userId)
        {
            IEnumerable<PhotoDto> photos = await GetPhotosFromUser(userId);
            string queryParams = null;
            var photoDtos = photos.ToList();
            string ids = string.Join(",", photoDtos.Select(p => p.Id));
            queryParams += "method=flickr.photosets.getList&";
            queryParams += $"user_id={userId}&";
            queryParams += $"photo_ids={ids}&";
            Response response = await GetResponse<Response>(queryParams);
            List<List<PhotoViewModel>> albums = new();
            foreach (PhotosetDto photoset in response.Photosets.Photoset.Where(ps => GlobalVariables.Pages.Values.Contains(ps.Title)))
            {
                List<PhotoViewModel> photoViewModels = new();
                (string title, string[] photosIds) = (photoset.Title, photoset.Has_Requested_Photos);
                foreach (string photoId in photosIds)
                {
                    var photoDto = photoDtos.FirstOrDefault(item => item.Id == photoId);
                    var photoViewModel = photoDto.MapToPhotoViewModel();
                    photoViewModel.Album = title;
                    photoViewModels.Add(photoViewModel);
                }
                albums.Add(photoViewModels);
            }
            return albums;
        }




        private async Task<T> GetResponse<T>(string inputQuery)
        {
            string queryParams = "?";
            queryParams += "format=json&";
            queryParams += "api_key=3397c0c2edd6aa046f22d76f9d5b3dd2&";
            queryParams += inputQuery;
            queryParams += "extras=original_format";
            Console.WriteLine(_httpClient.BaseAddress);
            var response = await _httpClient.GetAsync(queryParams);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            json = json.Remove(0, 14);
            json = json.Remove(json.Length - 1, 1);
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
