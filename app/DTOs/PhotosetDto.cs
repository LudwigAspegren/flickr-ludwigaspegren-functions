using System;
using System.Collections.Generic;
using SharedObjects.DTOs;

namespace SharedObjects.DTOs
{
    public class PhotosetDto
    {
        public string Id { get; set; }
        public string Primary { get; set; }
        public string Owner { get; set; }
        public string OwnerName { get; set; }
        public PhotoDto[] Photo { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Pages { get; set; }
        public string Title { get; set; }
        public int Total { get; set; }
        public string[]  Has_Requested_Photos { get; set; }
    }
}
