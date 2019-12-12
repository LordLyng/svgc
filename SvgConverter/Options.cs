using System.CommandLine;

namespace SvgConverter
{
    public static class Options
    {
        public static Option InputOption => new Option("--input") {Argument = new Argument<string>(), Description = "Path (relative or absolute to the svg to be converted)", Required = true};
        public static Option OutputOption => new Option("--output") {Argument = new Argument<string>(), Description = "Output path", Required = false};
        public static Option ImageTypeOption => new Option("--image-type") {Argument = Arguments.ImageTypeArgument, Description = "Set the desired type of the output image, defaults to \"png\"", Required = false};
        public static Option HeightOption => new Option("--height") {Argument = Arguments.HeightArgument, Description = "Sets the height in pixels of the resulting image", Required = false};
        public static Option WidthOpton => new Option("--width") {Argument = Arguments.WidthArgument, Description = "Sets the width in pixels of the resulting image", Required = false};
        public static Option BackgroundOption => new Option("--background") {Argument = Arguments.BackgroundArgument, Description = "Sets the background color of the output image (accepts hex colors)", Required = false};
    }
}