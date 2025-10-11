using System;

namespace CartApp
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message);
    }
}
