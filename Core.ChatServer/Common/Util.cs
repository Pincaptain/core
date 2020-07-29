using System.Drawing;
using System;

namespace Core.ChatServer.Common
{
    class Util
    {
        private static readonly Random random = new Random();

        public static string Normalize(string message)
        {
            message = message[0..^1];
            return message.Trim();
        }

        public static string Normalize(string message, short cutout)
        {
            string normalizedMessage = message.Trim();
            return normalizedMessage[0..^cutout];
        }

        public static Color GetRandomColor()
        {
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }
    }
}
