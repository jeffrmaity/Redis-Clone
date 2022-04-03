using Grpc.Core;
using GrpcRedisServerASP.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcRedisServerASP.Services
{
    public class VariableService : IVariableService
    {
        private readonly IVariableRepository _IVariableRepository;
        private readonly INumberVariableRepository _INumberVariableRepository;
        private readonly IListVariableRepository _IListVariableRepository;

        public List<Variable> Variables { get; set; }

        public VariableService(IVariableRepository variableRepository, INumberVariableRepository numberVariableRepository, IListVariableRepository listVariableRepository)
        {
            Variables = new List<Variable>();
            _IVariableRepository = variableRepository;
            _INumberVariableRepository = numberVariableRepository;
            _IListVariableRepository = listVariableRepository;
        }

        /// <summary>
        /// Adds a new Variable.
        /// </summary>
        public string AddNewVariable(string name, string value)
        {
            Variables = _IVariableRepository.AddNewVariable(Variables, name, value);
            return name + " Added Successfully.";
        }

        /// <summary>
        /// Gets an Existing Variable.
        /// </summary>
        public string GetVariable(string name)
        {
            Variable variable = _IVariableRepository.GetVariable(Variables, name);
            return variable != null ? variable.Value : name + " Not Defined !";
        }

        /// <summary>
        /// Increments an Existing Variable.
        /// </summary>
        public string IncrementVariable(string name)
        {
            Variable variable = _IVariableRepository.GetVariable(Variables, name);

            if (variable != null)
            {
                bool intResultTryParse = int.TryParse(variable.Value, out int intStr);
                if (intResultTryParse == true)
                {
                    Variables = _INumberVariableRepository.IncrementVariable(Variables, name, 1);
                    return name + " Incremented Successfully With New Value: " + (intStr + 1);
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
        public string IncrementByVariable(string name, string by)
        {
            Variable variable = _IVariableRepository.GetVariable(Variables, name);

            if (variable != null)
            {
                bool intResultTryParse = int.TryParse(variable.Value, out int intStr);
                if (intResultTryParse == true)
                {
                    intResultTryParse = int.TryParse(by, out int intBy);
                    if (intResultTryParse == true)
                    {
                        Variables = _INumberVariableRepository.IncrementVariable(Variables, name, intBy);
                        return name + " Incremented Successfully With New Value: " + (intStr + intBy);
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
        public string DecrementVariable(string name)
        {
            Variable variable = _IVariableRepository.GetVariable(Variables, name);

            if (variable != null)
            {
                bool intResultTryParse = int.TryParse(variable.Value, out int intStr);
                if (intResultTryParse == true)
                {
                    Variables = _INumberVariableRepository.DecrementVariable(Variables, name, 1);
                    return name + " Decremented Successfully With New Value: " + (intStr - 1);
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
        public string DecrementByVariable(string name, string by)
        {
            Variable variable = _IVariableRepository.GetVariable(Variables, name);

            if (variable != null)
            {
                bool intResultTryParse = int.TryParse(variable.Value, out int intStr);
                if (intResultTryParse == true)
                {
                    intResultTryParse = int.TryParse(by, out int intBy);
                    if (intResultTryParse == true)
                    {
                        Variables = _INumberVariableRepository.DecrementVariable(Variables, name, intBy);
                        return name + " Decremented Successfully With New Value: " + (intStr - intBy);
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
        public string RPushVariable(string name, string value)
        {
            Variables = _IListVariableRepository.RPushVariable(Variables, name, value);
            return value + " Pushed Right Successfully.";
        }

        /// <summary>
        /// Pops a Value from Right of Variable.
        /// </summary>
        public string RPopVariable(string name)
        {
            Variable variable = _IVariableRepository.GetVariable(Variables, name);

            if (variable != null)
            {
                if (!string.IsNullOrEmpty(variable.Value))
                {
                    Variables = _IListVariableRepository.RPopVariable(Variables, name); 
                    return name + " Right Popped Successfully.";
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
        public string LPushVariable(string name, string value)
        {
            Variables = _IListVariableRepository.LPushVariable(Variables, name, value);
            return value + " Pushed Left Successfully.";
        }

        /// <summary>
        /// Pops a Value from Left of Variable.
        /// </summary>
        public string LPopVariable(string name)
        {
            Variable variable = _IVariableRepository.GetVariable(Variables, name);

            if (variable != null)
            {
                if (!string.IsNullOrEmpty(variable.Value))
                {
                    Variables = _IListVariableRepository.LPopVariable(Variables, name);
                    return name + " Left Popped Successfully.";
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
        public string LIndexVariable(string name, string value)
        {
            Variable variable = _IVariableRepository.GetVariable(Variables, name);

            if (variable != null)
            {
                int index = _IListVariableRepository.LIndexVariable(Variables, name, value);
                return value + " is on Index " + index + ".";
            }
            else
                return name + " Not Defined !";
        }

        /// <summary>
        /// Expires a Variable After Value Seconds.
        /// </summary>
        public string ExpireVariableAsync(string name, string value)
        {
            Variable variable = _IVariableRepository.GetVariable(Variables, name);

            if (variable != null)
            {
                bool intResultTryParse = int.TryParse(value, out int intStr);
                if (intResultTryParse == true)
                {
                    Variables = _IVariableRepository.ExpireVariableAsync(Variables, name, intStr);
                    return name + " Will Expire After " + intStr + " seconds.";
                }
                else
                    return name + " Time Must Be Integer Value !";
            }
            else
                return name + " Not Defined !";
        }
    }
}
