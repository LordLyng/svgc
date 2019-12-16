using ImageMagick;
using System;
using System.Drawing;
using System.IO;

namespace SvgConverter
{
    public static class CommandHandler
    {
        public static int Handle(FileInfo input, FileInfo output, string imageType, int height, int width, string background)
        {
            if (!output.Exists)
                output = new FileInfo(input.FullName.Replace("svg", imageType));

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
                    Format = Enum.Parse<MagickFormat>(imageType, true)
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