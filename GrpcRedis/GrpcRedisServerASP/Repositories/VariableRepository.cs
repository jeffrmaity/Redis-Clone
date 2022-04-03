using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcRedisServerASP.Repositories
{
    public class VariableRepository : IVariableRepository
    {
        public Variable Variable { get; set; }

        public readonly object myLock = new object();

        public VariableRepository()
        {
            
        }

        /// <summary>
        /// Checks if Variable Exists.
        /// </summary>
        private bool VariableExist(List<Variable> Variables, string name)
        {
            if (Variables.Any(x => x.Name == name))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Adds a new Variable.
        /// </summary>
        public List<Variable> AddNewVariable(List<Variable> Variables, string name, string value)
        {
            lock (myLock)
            {
                if (VariableExist(Variables, name))
                {
                    Variables.FirstOrDefault(x => x.Name == name).Value = value;
                }
                else
                {
                    Variable = new Variable();
                    Variable.Name = name;
                    Variable.Value = value;
                    Variables.Add(Variable);
                }
            }

            return Variables;
        }

        /// <summary>
        /// Gets an Existing Variable.
        /// </summary>
        public Variable GetVariable(List<Variable> Variables, string name)
        {
            if (VariableExist(Variables, name))
            {
                return Variables.FirstOrDefault(x => x.Name == name);
            }
            else
                return null;
        }

        /// <summary>
        /// Expires a Variable After Value Seconds.
        /// </summary>
        public List<Variable> ExpireVariableAsync(List<Variable> Variables, string name, int seconds)
        {
            Variable = GetVariable(Variables, name);
            _ = RemoveWithDelay(Variables, Variable, seconds * 1000);
            return Variables;
        }

        private async Task RemoveWithDelay(List<Variable> Variables, Variable variable, int milliseconds)
        {
            await Task.Delay(milliseconds);
            Variables.Remove(variable);
        }
    }
}
