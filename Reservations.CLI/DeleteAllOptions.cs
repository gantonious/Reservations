using CommandLine;

namespace Reservations.CLI
{
    public class DeleteAllOptions
    {
        [Option('t', "token", HelpText = "Token to authenticate to Reservations")]
        public string Token { get; set; }
    }
}