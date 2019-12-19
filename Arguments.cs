using ImageMagick;
using System.CommandLine;
using System.CommandLine.Builder;
using System.IO;

namespace svgc
{
    public static class Arguments
    {
        static Arguments()
        {
        }
        public static Argument<int> HeightArgument => new Argument<int>(() => 0);
        public static Argument<int> WidthArgument => new Argument<int>(() => 0);
        public static Argument<string> BackgroundArgument => new Argument<string>(() => null);
        public static Argument<MagickFormat> ImageTypeArgument => new Argument<MagickFormat>(() => MagickFormat.Png)
        {
            Description = "All media types supported by ImageMagick, see https://github.com/dlemstra/Magick.NET/blob/master/src/Magick.NET/Shared/Enums/MagickFormat.cs",
            IsHidden = true
        };
        public static Argument<FileInfo> InputArgument => new Argument<FileInfo>("input") { Description = "Path (relative or absolute to the svg to be converted)" }.ExistingOnly();
    }
}