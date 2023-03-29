using System;
using System.Collections.Generic;

namespace SharedObjects.DTOs
{
    public class Response
    {
        public PhotosetDto Photoset { get; set; }
        public PhotosetsDto Photosets { get; set; }
        public string Stat { get; set; }
        public PhotosDto Photos { get; set; }
    }
}
