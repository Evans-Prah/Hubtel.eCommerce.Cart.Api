using System;
using System.Text;

namespace Hubtel.eCommerce.Cart.Api.Logger
{
    public interface IFileLogger
    {
        void LogError(string errorMessage);
        void LogError(Exception errorMessage);
        void LogInfo(string infoMessage);
        void LogInfo(StringBuilder infoMessage);
        void LogWarning(string warningMessage);
    }
}