using Ludwigaspegren_functions_app.ViewModels;
using SharedObjects.DTOs;

namespace SharedObjects.Utils
{
    public static class ObjectMapper
    {
        public static PhotoViewModel MapToPhotoViewModel(this PhotoDto photoDto)
        {

            var photoUri = $"https://live.staticflickr.com/{photoDto.Server}/{photoDto.Id}_{photoDto.OriginalSecret}_b.jpg";
            var title = photoDto.Title;
            var id = photoDto.Id;
            PhotoViewModel photoViewModel = new()
            {
                Title = title,
                Uri = photoUri,
                Id = id,
            };
            photoViewModel.Id = photoDto.Id;
            return photoViewModel;
        }


    }
}
