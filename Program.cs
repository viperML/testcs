using Serilog;
using CommandLine;

public static class Utils
{
    public static void makeNothing()
    {
        Log.Warning("Hello");
    }
}

class Program
{
    class Options
    {
        [Option(Default = false, HelpText = "This flag does something")]
        public bool Verbose { get; set; }
    }

    static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Verbose()
             .WriteTo.Console()
             .CreateLogger();

        await Parser.Default.ParseArguments<Options>(args).WithParsedAsync(async opts =>
        {
            Log.Information("{@Opts}", opts);
            await RealMain();
        });
    }

    static async Task RealMain()
    {
        var foo = "x";
        var bar = "y";

        Utils.makeNothing();


        var result = foo + bar;
        Log.Information("{@Result}", result);

        Log.Debug("Before");
        var fut = Task.Delay(1000);
        Log.Debug("After");
        await fut;
    }
}

