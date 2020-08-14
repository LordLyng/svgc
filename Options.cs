using System.CommandLine;
using System.CommandLine.Builder;
using System.IO;

namespace svgc
{
    public static class Options
    {
        public static Option OutputOption => new Option(new[] { "--output", "-o" }) { Argument = new Argument<FileInfo>().LegalFilePathsOnly(), Description = "Output path", IsRequired = false };
        public static Option ImageTypeOption => new Option(new[] { "--image-type", "-it" }) { Argument = Arguments.ImageTypeArgument, Description = "Set the desired type of the output image, defaults to \"png\"", IsRequired = false };
        public static Option HeightOption => new Option(new[] { "--height", "-h" }) { Argument = Arguments.HeightArgument, Description = "Sets the height in pixels of the resulting image", IsRequired = false };
        public static Option WidthOpton => new Option(new[] { "--width", "-w" }) { Argument = Arguments.WidthArgument, Description = "Sets the width in pixels of the resulting image", IsRequired = false };
        public static Option BackgroundOption => new Option(new[] { "--background", "-b" }) { Argument = Arguments.BackgroundArgument, Description = "Sets the background color of the output image (accepts hex colors)", IsRequired = false };
    }
}