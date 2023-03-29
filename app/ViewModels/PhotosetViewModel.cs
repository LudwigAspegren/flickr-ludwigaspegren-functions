using SharedObjects.DTOs;

namespace Ludwigaspegren_functions_app.ViewModels
{
    public class PhotosetViewModel
    {
        public PhotosetViewModel()
        {
        }
            public string Id { get; set; }
            public string Title { get; set; }
            public PhotoViewModel[] Photos { get; set; }
    }
}
