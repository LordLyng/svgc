using ImageMagick;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SvgConverter
{
    class Program
    {
        static int Main(string[] args)
        {

            var dto = DateTimeOffset.UnixEpoch.AddMilliseconds(UInt32.MaxValue - 1);
            var heightArgument = new Argument<int>();
            heightArgument.SetDefaultValue(0);

            var widthArgument = new Argument<int>();
            widthArgument.SetDefaultValue(0);

            var backgroundArgument = new Argument<string>();
            backgroundArgument.SetDefaultValue(null);

            var imageTypeArgument = new Argument<string>();
            imageTypeArgument.AddSuggestions(GetAvailableImageFormats());
            imageTypeArgument.SetDefaultValue("png");
            imageTypeArgument.AddValidator(result =>
            {
                var val = result.GetValueOrDefault<string>();

                if (GetAvailableImageFormats()
                    .Any(name => string.Equals(val, name, StringComparison.CurrentCultureIgnoreCase)))
                {
                    return null;
                }

                return "Invalid image format, please select from the list";
            });

            var command = new RootCommand()
            {
                new Option("--input") {Argument = new Argument<string>(), Description = "Path (relative or absolute to the svg to be converted)", Required = true},
                new Option("--output") {Argument = new Argument<string>(), Description = "Output path", Required = false},
                new Option("--image-type") {Argument = imageTypeArgument, Description = "Set the desired type of the output image, defaults to \"png\"", Required = false},
                new Option("--height") {Argument = heightArgument, Description = "Sets the height in pixels of the resulting image", Required = false},
                new Option("--width") {Argument = widthArgument, Description = "Sets the width in pixels of the resulting image", Required = false},
                new Option("--background") {Argument = backgroundArgument, Description = "Sets the background color of the output image (accepts hex colors)", Required = false},
            };
            command.Description = "Converts svg files to image files";
            command.Handler = CommandHandler.Create((string input, string output, string imageType, int height, int width, string background) =>
            {
                if (string.IsNullOrWhiteSpace(input))
                    return;

                if (string.IsNullOrWhiteSpace(output))
                    output = input.Replace("svg", imageType);

                Color bgColor = string.IsNullOrWhiteSpace(background) ? Color.Transparent : GetRgbFromHex(background);

                using (var fs = new FileStream(input, FileMode.Open))
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Svg,
                        Height = height != 0 ? height : (int?) null,
                        Width = width != 0 ? width : (int?) null,
                        BackgroundColor = bgColor == Color.Transparent ? MagickColors.Transparent : MagickColor.FromRgb(bgColor.R, bgColor.G, bgColor.B)
                    };

                    using (var image = new MagickImage(fs, readSettings))
                    {
                        image.Format = getImageFormatFromString(imageType);
                        using (var outputStream = new FileStream(output, FileMode.Create))
                        {
                            image.Write(outputStream);
                        }
                    }
                }

                var outputUrl = new Uri(output, UriKind.Relative);
                Console.WriteLine(outputUrl);
            });

            return command.InvokeAsync(args).Result;
        }

        static IReadOnlyCollection<string> GetAvailableImageFormats()
        {
            return new List<string>()
            {
                "Png",
                "Jpg"
            };
        }

        static MagickFormat getImageFormatFromString(string input)
        {
            return Enum.Parse<MagickFormat>(input, true);
        }

        static Color GetRgbFromHex(string hex)
        {
            if (!hex.StartsWith('#'))
                hex = "#" + hex;

            var cv = new ColorConverter();
            return (Color)cv.ConvertFromString(hex);
        }
    }
}
