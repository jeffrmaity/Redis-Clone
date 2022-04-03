using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcRedisServerASP.Services
{
    public interface ICommandService
    {
        bool ValidateCommand(string command, out string message);

        string ExecuteCommand(string command);
    }
}
