using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcRedisServer
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        readonly List<Variable> _variables;
        readonly string _CommandNotSupported = "Command Not Supported !";
        readonly string _WrongCommandArgs = "Wrong Command Arguments !";
        readonly object myLock = new object();
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }
        public GreeterService(List<Variable> variables)
        {
            _variables = variables;
        }
        public override Task<CommandReply> ExecuteCommand(Command request, ServerCallContext context)
        {
            string commandreply = string.Empty;
            string[] commandargs = request.Command_.Split(' ');

            switch (commandargs[0].ToUpper())
            {
                case "SET":
                    if (commandargs.Length != 3)
                        commandreply = _WrongCommandArgs;
                    else
                        commandreply = AddNewVariable(commandargs[1], commandargs[2]);
                    break;
                case "GET":
                    if (commandargs.Length != 2)
                        commandreply = _WrongCommandArgs;
                    else
                        commandreply = GetVariable(commandargs[1]);
                    break;
                case "INCR":
                    if (commandargs.Length != 2)
                        commandreply = _WrongCommandArgs;
                    else
                        commandreply = IncrementVariable(commandargs[1]);
                    break;
                case "INCRBY":
                    if (commandargs.Length != 3)
                        commandreply = _WrongCommandArgs;
                    else
                        commandreply = IncrementByVariable(commandargs[1], commandargs[2]);
                    break;
                case "DECR":
                    if (commandargs.Length != 2)
                        commandreply = _WrongCommandArgs;
                    else
                        commandreply = DecrementVariable(commandargs[1]);
                    break;
                case "DECRBY":
                    if (commandargs.Length != 3)
                        commandreply = _WrongCommandArgs;
                    else
                        commandreply = DecrementByVariable(commandargs[1], commandargs[2]);
                    break;
                case "RPUSH":
                    if (commandargs.Length != 3)
                        commandreply = _WrongCommandArgs;
                    else
                        commandreply = RPushVariable(commandargs[1], commandargs[2]);
                    break;
                case "RPOP":
                    if (commandargs.Length != 2)
                        commandreply = _WrongCommandArgs;
                    else
                        commandreply = RPopVariable(commandargs[1]);
                    break;
                case "LPUSH":
                    if (commandargs.Length != 3)
                        commandreply = _WrongCommandArgs;
                    else
                        commandreply = LPushVariable(commandargs[1], commandargs[2]);
                    break;
                case "LPOP":
                    if (commandargs.Length != 2)
                        commandreply = _WrongCommandArgs;
                    else
                        commandreply = LPopVariable(commandargs[1]);
                    break;
                case "LINDEX":
                    if (commandargs.Length != 3)
                        commandreply = _WrongCommandArgs;
                    else
                        commandreply = LIndexVariable(commandargs[1], commandargs[2]);
                    break;
                case "EXPIRES":
                    if (commandargs.Length != 3)
                        commandreply = _WrongCommandArgs;
                    else
                        commandreply = ExpireVariableAsync(commandargs[1], commandargs[2]);
                    break;
                default:
                    commandreply = _CommandNotSupported;
                    break;
            }

            return Task.FromResult(new CommandReply
            {
                Command = commandreply
            });
        }
        /// <summary>
        /// Adds a new Variable.
        /// </summary>
        private string AddNewVariable(string name, string value)
        {
            lock (myLock)
            {
                if (_variables.Any(x => x.Name == name))
                {
                    _variables.FirstOrDefault(x => x.Name == name).Value = value;
                }
                else
                {
                    Variable newvariable = new Variable();
                    newvariable.Name = name;
                    newvariable.Value = value;
                    _variables.Add(newvariable);
                }
            }

            return name + " Added Successfully.";
        }
        /// <summary>
        /// Gets an Existing Variable.
        /// </summary>
        private string GetVariable(string name)
        {
            Variable variable = _variables.Where(x => x.Name == name).FirstOrDefault();

            if (variable != null)
                return variable.Value;
            else
                return name + " Not Defined !";
        }
        /// <summary>
        /// Increments an Existing Variable.
        /// </summary>
        private string IncrementVariable(string name)
        {
            Variable variable = _variables.Where(x => x.Name == name).FirstOrDefault();

            if (variable != null)
            {
                bool intResultTryParse = int.TryParse(variable.Value, out int intStr);
                if (intResultTryParse == true)
                {
                    lock (myLock)
                    {
                        int newVal = intStr + 1;
                        _variables.FirstOrDefault(x => x.Name == name).Value = newVal.ToString();
                        return name + " Incremented Successfully With New Value: " + newVal;
                    }
                }
                else
                    return name + " Do Not Have Integer Value !";
            }
            else
                return name + " Not Defined !";
        }
        /// <summary>
        /// Increments an Existing Variable By Value.
        /// </summary>
        private string IncrementByVariable(string name, string by)
        {
            Variable variable = _variables.Where(x => x.Name == name).FirstOrDefault();

            if (variable != null)
            {
                bool intResultTryParse = int.TryParse(variable.Value, out int intStr);
                if (intResultTryParse == true)
                {
                    intResultTryParse = int.TryParse(by, out int intBy);
                    if (intResultTryParse == true)
                    {
                        lock (myLock)
                        {
                            int newVal = intStr + intBy;
                            _variables.FirstOrDefault(x => x.Name == name).Value = newVal.ToString();
                            return name + " Incremented Successfully With New Value: " + newVal;
                        }
                    }
                    else
                        return name + " Must Be Followed By Integer Value !";
                }
                else
                    return name + " Do Not Have Integer Value !";
            }
            else
                return name + " Not Defined !";
        }
        /// <summary>
        /// Decrements an Existing Variable.
        /// </summary>
        private string DecrementVariable(string name)
        {
            Variable variable = _variables.Where(x => x.Name == name).FirstOrDefault();

            if (variable != null)
            {
                bool intResultTryParse = int.TryParse(variable.Value, out int intStr);
                if (intResultTryParse == true)
                {
                    lock (myLock)
                    {
                        int newVal = intStr - 1;
                        _variables.FirstOrDefault(x => x.Name == name).Value = newVal.ToString();
                        return name + " Decremented Successfully With New Value: " + newVal;
                    }
                }
                else
                    return name + " Do Not Have Integer Value !";
            }
            else
                return name + " Not Defined !";
        }
        /// <summary>
        /// Decrements an Existing Variable By Value.
        /// </summary>
        private string DecrementByVariable(string name, string by)
        {
            Variable variable = _variables.Where(x => x.Name == name).FirstOrDefault();

            if (variable != null)
            {
                bool intResultTryParse = int.TryParse(variable.Value, out int intStr);
                if (intResultTryParse == true)
                {
                    intResultTryParse = int.TryParse(by, out int intBy);
                    if (intResultTryParse == true)
                    {
                        lock (myLock)
                        {
                            int newVal = intStr - intBy;
                            _variables.FirstOrDefault(x => x.Name == name).Value = newVal.ToString();
                            return name + " Decremented Successfully With New Value: " + newVal;
                        }
                    }
                    else
                        return name + " Must Be Followed By Integer Value !";
                }
                else
                    return name + " Do Not Have Integer Value !";
            }
            else
                return name + " Not Defined !";
        }
        /// <summary>
        /// Pushes a Value on Right of Variable.
        /// </summary>
        private string RPushVariable(string name, string value)
        {
            Variable variable = _variables.Where(x => x.Name == name).FirstOrDefault();

            if (variable != null)
            {
                lock (myLock)
                {
                    List<string> result = variable.Value.Split(',').ToList();
                    result.Add(value);
                    _variables.FirstOrDefault(x => x.Name == name).Value = string.Join(",", result);
                }
            }
            else
            {
                Variable newvariable = new Variable();
                newvariable.Name = name;
                newvariable.Value = value;
                _variables.Add(newvariable);
            }

            return value + " Pushed Right Successfully.";
        }
        /// <summary>
        /// Pops a Value from Right of Variable.
        /// </summary>
        private string RPopVariable(string name)
        {
            Variable variable = _variables.Where(x => x.Name == name).FirstOrDefault();

            if (variable != null)
            {
                if (!string.IsNullOrEmpty(variable.Value))
                {
                    lock (myLock)
                    {
                        List<string> result = variable.Value.Split(',').ToList();
                        string popped = result[result.Count - 1];
                        result.RemoveAt(result.Count - 1);
                        _variables.FirstOrDefault(x => x.Name == name).Value = string.Join(",", result);
                        return popped + " Popped from Right of " + name + " Successfully.";
                    }
                }
                else
                    return name + " is Empty.";
            }
            else
                return name + " Not Defined !";
        }
        /// <summary>
        /// Pushes a Value on Left of Variable.
        /// </summary>
        private string LPushVariable(string name, string value)
        {
            Variable variable = _variables.Where(x => x.Name == name).FirstOrDefault();

            if (variable != null)
            {
                lock (myLock)
                {
                    List<string> result = variable.Value.Split(',').ToList();
                    result.Insert(0, value);
                    _variables.FirstOrDefault(x => x.Name == name).Value = string.Join(",", result);
                }
            }
            else
            {
                Variable newvariable = new Variable();
                newvariable.Name = name;
                newvariable.Value = value;
                _variables.Add(newvariable);
            }

            return value + " Pushed Left Successfully.";
        }
        /// <summary>
        /// Pops a Value from Left of Variable.
        /// </summary>
        private string LPopVariable(string name)
        {
            Variable variable = _variables.Where(x => x.Name == name).FirstOrDefault();

            if (variable != null)
            {
                if (!string.IsNullOrEmpty(variable.Value))
                {
                    lock (myLock)
                    {
                        List<string> result = variable.Value.Split(',').ToList();
                        string popped = result[0];
                        result.RemoveAt(0);
                        _variables.FirstOrDefault(x => x.Name == name).Value = string.Join(",", result);
                        return popped + " Popped from Left of " + name + " Successfully.";
                    }
                }
                else
                    return name + " is Empty.";
            }
            else
                return name + " Not Defined !";
        }
        /// <summary>
        /// Gets Index of a Value from Variable.
        /// </summary>
        private string LIndexVariable(string name, string value)
        {
            Variable variable = _variables.Where(x => x.Name == name).FirstOrDefault();

            if (variable != null)
            {
                List<string> result = variable.Value.Split(',').ToList();
                return value + " is on Index " + result.IndexOf(value) + ".";
            }
            else
                return name + " Not Defined !";
        }
        /// <summary>
        /// Expires a Variable After Value Seconds.
        /// </summary>
        private string ExpireVariableAsync(string name, string value)
        {
            Variable variable = _variables.Where(x => x.Name == name).FirstOrDefault();

            if (variable != null)
            {
                bool intResultTryParse = int.TryParse(value, out int intStr);
                if (intResultTryParse == true)
                {
                    RemoveWithDelay(variable, intStr * 1000);
                    return name + " Will Expire After " + intStr + " seconds.";
                }
                else
                    return name + " Time Must Be Integer Value !";
            }
            else
                return name + " Not Defined !";
        }
        async Task RemoveWithDelay(Variable variable, int milliseconds)
        {
            await Task.Delay(milliseconds);
            _variables.Remove(variable);
        }
    }
}
