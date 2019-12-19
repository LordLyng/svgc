using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using ImageMagick;

namespace svgc
{
    class Program
    {
        static int Main(string[] args)
        {

            var command = new RootCommand()
            {
                Arguments.InputArgument,
                Options.OutputOption,
                Options.ImageTypeOption,
                Options.HeightOption,
                Options.WidthOpton,
                Options.BackgroundOption
            };
            command.Description = "Converts svg files to image files";
            command.Handler = System.CommandLine.Invocation.CommandHandler.Create<FileInfo, FileInfo, MagickFormat, int, int, string>(CommandHandler.Handle);

            return command.InvokeAsync(args).Result;
        }

    }
}
