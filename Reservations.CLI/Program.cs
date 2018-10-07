using System;
using CommandLine;

namespace Reservations.CLI
{
    public class Program
    {
        private static int RunAddGuests(AddGuestsOption options)
        {
            return 0;
        }
        
        public static int Main(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments<AddGuestsOption>(args)
                .MapResult(
                    (AddGuestsOption options) => RunAddGuests(options),
                    errs => 1
                );
        }
    }
}