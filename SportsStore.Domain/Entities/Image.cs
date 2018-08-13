namespace SportsStore.Domain.Entities
{
    public class Image
    {
        public Image()
        {
        }

        public Image(byte[] imageData, string imageType)
        {
            ImageData = imageData;
            ImageType = imageType;
        }

        public byte[] ImageData { get; protected set; }
        public string ImageType { get; protected set; }

        public bool IsFilled() => ImageData != null && !string.IsNullOrWhiteSpace(ImageType);
    }
}