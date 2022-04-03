using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcRedisServerASP.Repositories
{
    public class NumberVariableRepository : VariableRepository, INumberVariableRepository
    {
        /// <summary>
        /// Increments an Existing Variable.
        /// </summary>
        public List<Variable> IncrementVariable(List<Variable> Variables, string name, int byValue)
        {
            lock (myLock)
            {
                Variable = GetVariable(Variables, name);
                int newVal = Convert.ToInt32(Variable.Value) + byValue;
                Variables.FirstOrDefault(x => x.Name == name).Value = newVal.ToString();
            }

            return Variables;
        }

        /// <summary>
        /// Decrements an Existing Variable.
        /// </summary>
        public List<Variable> DecrementVariable(List<Variable> Variables, string name, int byValue)
        {
            lock (myLock)
            {
                Variable = GetVariable(Variables, name);
                int newVal = Convert.ToInt32(Variable.Value) - byValue;
                Variables.FirstOrDefault(x => x.Name == name).Value = newVal.ToString();
            }

            return Variables;
        }
    }
}
