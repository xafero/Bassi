namespace Bassi.Core
{
    public class PictureFilter : AbstractFilter
    {
        protected override bool IsValidExtension(string ext) => ext == "png" || ext == "jpg";
    }

    public class VideoFilter : AbstractFilter
    {
        protected override bool IsValidExtension(string ext) => ext == "mp4" || ext == "flv";
    }

    public class DocumentFilter : AbstractFilter
    {
        protected override bool IsValidExtension(string ext) => ext == "pdf" || ext == "docx";
    }
}