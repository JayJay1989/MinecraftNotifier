using System;

namespace MinecraftNotifier.Lib.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public string Content { get; set; }
    }
}
