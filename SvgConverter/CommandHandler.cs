using ImageMagick;
using System;
using System.Drawing;
using System.IO;

namespace SvgConverter
{
    public static class CommandHandler
    {
        public static void Handle(string input, string output, string imageType, int height, int width, string background)
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

                using var image = new MagickImage(fs, readSettings)
                {
                    Format = Enum.Parse<MagickFormat>(imageType, true)
                };
                using var outputStream = new FileStream(output, FileMode.Create);
                image.Write(outputStream);
            }

                Console.WriteLine(output);
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