using ImageMagick;
using System;
using System.Drawing;
using System.IO;

namespace svgc
{
    public static class CommandHandler
    {
        public static int Handle(FileInfo input, FileInfo output, MagickFormat imageType, int height, int width, string background)
        {
            if (output == null || !output.Exists)
                output = new FileInfo(input.FullName.Replace("svg", imageType.ToString()));

            Color bgColor = string.IsNullOrWhiteSpace(background) ? Color.Transparent : GetRgbFromHex(background);

            using (var fs = new FileStream(input.FullName, FileMode.Open))
            {
                var readSettings = new MagickReadSettings
                {
                    Format = MagickFormat.Svg,
                    Height = height != 0 ? height : (int?)null,
                    Width = width != 0 ? width : (int?)null,
                    BackgroundColor = bgColor == Color.Transparent ? MagickColors.Transparent : MagickColor.FromRgb(bgColor.R, bgColor.G, bgColor.B)
                };

                using var image = new MagickImage(fs, readSettings)
                {
                    Format = imageType
                };
                using var outputStream = new FileStream(output.FullName, FileMode.Create);
                image.Write(outputStream);
            }

            Console.WriteLine(output);

            return 0;
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