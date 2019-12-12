using ImageMagick;
using System;
using System.Collections.Generic;
using System.CommandLine;

namespace SvgConverter
{
    public static class Arguments
    {
        static Arguments()
        {
            ImageTypeArgument = new Argument<string>(() => "png");
            ImageTypeArgument.Description = "All media types supported by ImageMagick, see https://github.com/dlemstra/Magick.NET/blob/master/src/Magick.NET/Shared/Enums/MagickFormat.cs";
            ImageTypeArgument.AddValidator(result =>
            {
                var val = result.GetValueOrDefault<string>();

                var parsed = Enum.TryParse(typeof(MagickFormat), val, true, out _);

                if (parsed)
                    return null;

                return "Invalid image format, please select from the list";
            });
        }
        public static Argument<int> HeightArgument => new Argument<int>(() => 0);
        public static Argument<int> WidthArgument => new Argument<int>(() => 0);
        public static Argument<string> BackgroundArgument => new Argument<string>(() => null);
        public static Argument<string> ImageTypeArgument {get;}
    }
}