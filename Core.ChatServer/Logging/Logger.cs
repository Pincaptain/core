using System.Drawing;

using Colorful;
using Console = Colorful.Console;

namespace Core.ChatServer.Logging
{
    class Logger
    {
        public const string InitializationString = "INITIALIZATION";
        public const string ModificationString = "MODIFICATION";
        public const string IdentificationString = "IDENTIFICATION";
        public const string TerminationString = "TERMINATION";

        public static void LogMessage(Client client, string message)
        {
            StyleSheet styleSheet = new StyleSheet(Color.White);
            styleSheet.AddStyle(client.Identification, client.ClientColor);

            Console.WriteLineStyled($"{client.Identification}: {message}", styleSheet);
        }

        public static void LogInitialization(string message)
        {
            StyleSheet styleSheet = new StyleSheet(Color.White);
            styleSheet.AddStyle(InitializationString, Color.LightGreen);

            Console.WriteLineStyled($"[{InitializationString}] {message}", styleSheet);
        }

        public static void LogModification(string message)
        {
            StyleSheet styleSheet = new StyleSheet(Color.White);
            styleSheet.AddStyle(ModificationString, Color.Orange);

            Console.WriteLineStyled($"[{ModificationString}] {message}", styleSheet);
        }

        public static void LogIdentification(string message)
        {
            StyleSheet styleSheet = new StyleSheet(Color.White);
            styleSheet.AddStyle(IdentificationString, Color.LightBlue);

            Console.WriteLineStyled($"[{IdentificationString}] {message}", styleSheet);
        }

        public static void LogTermination(string message)
        {
            StyleSheet styleSheet = new StyleSheet(Color.White);
            styleSheet.AddStyle(TerminationString, Color.Red);

            Console.WriteLineStyled($"[{TerminationString}] {message}", styleSheet);
        }
    }
}
