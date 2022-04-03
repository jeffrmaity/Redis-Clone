using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcRedisServerASP.Services
{
    public class CommandService : ICommandService
    {
        private readonly string _CommandNotSupported = "Command Not Supported !";
        private readonly string _WrongCommandArgs = "Wrong Command Arguments !";
        private readonly List<string> TwoArgsCommands = new List<string> { "GET", "INCR", "DECR", "RPOP", "LPOP" };
        private readonly List<string> ThreeArgsCommands = new List<string> { "SET", "INCRBY", "DECRBY", "RPUSH", "LPUSH", "LINDEX", "EXPIRES" };

        private readonly IVariableService _IVariableService;

        public CommandService(IVariableService variableService)
        {
            _IVariableService = variableService;
        }

        public bool ValidateCommand(string command, out string message)
        {
            message = string.Empty;

            string[] commandargs = command.Split(' ');

            if (TwoArgsCommands.Contains(commandargs[0].ToUpper()))
            {
                if (commandargs.Length == 2)
                    return true;
                else
                {
                    message = _WrongCommandArgs;
                    return false;
                }
            }
            else if (ThreeArgsCommands.Contains(commandargs[0].ToUpper()))
            {
                if (commandargs.Length == 3)
                    return true;
                else
                {
                    message = _WrongCommandArgs;
                    return false;
                }
            }
            else
            {
                message = _CommandNotSupported;
                return false;
            }
        }

        public string ExecuteCommand(string command)
        {
            string[] commandargs = command.Split(' ');

            switch (commandargs[0].ToUpper())
            {
                case "SET":
                    return _IVariableService.AddNewVariable(commandargs[1], commandargs[2]);
                case "GET":
                    return _IVariableService.GetVariable(commandargs[1]);
                case "INCR":
                    return _IVariableService.IncrementVariable(commandargs[1]);
                case "INCRBY":
                    return _IVariableService.IncrementByVariable(commandargs[1], commandargs[2]);
                case "DECR":
                    return _IVariableService.DecrementVariable(commandargs[1]);
                case "DECRBY":
                    return _IVariableService.DecrementByVariable(commandargs[1], commandargs[2]);
                case "RPUSH":
                    return _IVariableService.RPushVariable(commandargs[1], commandargs[2]);
                case "RPOP":
                    return _IVariableService.RPopVariable(commandargs[1]);
                case "LPUSH":
                    return _IVariableService.LPushVariable(commandargs[1], commandargs[2]);
                case "LPOP":
                    return _IVariableService.LPopVariable(commandargs[1]);
                case "LINDEX":
                    return _IVariableService.LIndexVariable(commandargs[1], commandargs[2]);
                case "EXPIRES":
                    return _IVariableService.ExpireVariableAsync(commandargs[1], commandargs[2]);
            }

            return _CommandNotSupported;
        }
    }
}
