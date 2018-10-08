using CommandLine;

namespace Reservations.CLI
{
    [Verb("delete", HelpText = "Delete all guests")]
    public class DeleteAllOptions
    {
        [Option('t', "token", HelpText = "Token to authenticate to Reservations")]
        public string Token { get; set; }
    }
}