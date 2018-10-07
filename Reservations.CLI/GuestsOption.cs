using CommandLine;

namespace Reservations.CLI
{
    [Verb("guests", HelpText = "Manage guests")]
    public class AddGuestsOption
    {
        [Option('f', "file", Required = true, HelpText = "Csv file pointing to list of guests.")]
        public string CsvFile { get; set; }
        
        [Option('t', "token", HelpText = "Token to authenticate to Reservations")]
        public string Token { get; set; }
    }
}