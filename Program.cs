using System.CommandLine;
using System.CommandLine.Invocation;

namespace SvgConverter
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
            command.Handler = System.CommandLine.Invocation.CommandHandler.Create<string, string, string, int, int, string, int>(CommandHandler.Handle);

            return command.InvokeAsync(args).Result;
        }

    }
}
