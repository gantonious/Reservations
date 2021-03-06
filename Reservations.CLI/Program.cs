﻿using System;
using CommandLine;
using Reservations.Sdk;

namespace Reservations.CLI
{
    public class Program
    {
        private static int RunAddGuests(AddGuestsOption options)
        {
            try
            {
                var reservationsClient = ReservationsClientFactory.BuildClient(new ReservationsConfig
                {
                    AuthenticationToken = options.Token
                });

                var action = new AddGuestsAction(reservationsClient, options.CsvFile);
                action.Execute().Wait();
                return 0;
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return -1;
            }
        }
        
        private static int RunDeleteGuests(DeleteAllOptions options)
        {
            try
            {
                var reservationsClient = ReservationsClientFactory.BuildClient(new ReservationsConfig
                {
                    AuthenticationToken = options.Token
                });

                var action = new DeleteGuestsAction(reservationsClient);
                action.Execute().Wait();
                return 0;
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return -1;
            }
        }
        
        public static int Main(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments<AddGuestsOption, DeleteAllOptions>(args)
                .MapResult(
                    (AddGuestsOption options) => RunAddGuests(options),
                    (DeleteAllOptions options) => RunDeleteGuests(options),
                    errs => 1
                );
        }
    }
}