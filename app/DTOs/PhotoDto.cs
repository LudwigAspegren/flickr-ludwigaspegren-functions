namespace SharedObjects.DTOs
{
    public class PhotoDto
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Server { get; set; }
        public int Farm { get; set; }
        public string Title { get; set; }
        public string IsPrimary { get; set; }
        public bool IsPublic { get; set; }
        public bool IsFriend { get; set; }
        public bool IsFamily { get; set; }
        public string OriginalSecret { get; set; }
    }
}