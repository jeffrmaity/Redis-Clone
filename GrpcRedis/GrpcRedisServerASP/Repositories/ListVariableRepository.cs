using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcRedisServerASP.Repositories
{
    public class ListVariableRepository : VariableRepository, IListVariableRepository
    {
        /// <summary>
        /// Pushes a Value on Right of Variable.
        /// </summary>
        public List<Variable> RPushVariable(List<Variable> Variables, string name, string value)
        {
            lock (myLock)
            {
                Variable = GetVariable(Variables, name);

                if (Variable != null)
                {
                    List<string> result = Variable.Value.Split(',').ToList();
                    result.Add(value);
                    Variables.FirstOrDefault(x => x.Name == name).Value = string.Join(",", result);
                }
                else
                    Variables = AddNewVariable(Variables, name, value);                
            }

            return Variables;
        }

        /// <summary>
        /// Pops a Value from Right of Variable.
        /// </summary>
        public List<Variable> RPopVariable(List<Variable> Variables, string name)
        {
            lock (myLock)
            {
                Variable = GetVariable(Variables, name);

                List<string> result = Variable.Value.Split(',').ToList();
                string popped = result[result.Count - 1];
                result.RemoveAt(result.Count - 1);
                Variables.FirstOrDefault(x => x.Name == name).Value = string.Join(",", result);
            }

            return Variables;
        }

        /// <summary>
        /// Pushes a Value on Left of Variable.
        /// </summary>
        public List<Variable> LPushVariable(List<Variable> Variables, string name, string value)
        {
            lock (myLock)
            {
                Variable = GetVariable(Variables, name);

                if (Variable != null)
                {
                    List<string> result = Variable.Value.Split(',').ToList();
                    result.Insert(0, value);
                    Variables.FirstOrDefault(x => x.Name == name).Value = string.Join(",", result);
                }
                else
                    Variables = AddNewVariable(Variables, name, value);
            }

            return Variables;
        }

        /// <summary>
        /// Pops a Value from Left of Variable.
        /// </summary>
        public List<Variable> LPopVariable(List<Variable> Variables, string name)
        {
            lock (myLock)
            {
                Variable = GetVariable(Variables, name);

                List<string> result = Variable.Value.Split(',').ToList();
                string popped = result[0];
                result.RemoveAt(0);
                Variables.FirstOrDefault(x => x.Name == name).Value = string.Join(",", result);
            }

            return Variables;
        }

        /// <summary>
        /// Gets Index of a Value from Variable.
        /// </summary>
        public int LIndexVariable(List<Variable> Variables, string name, string value)
        {
            Variable = GetVariable(Variables, name);

            List<string> result = Variable.Value.Split(',').ToList();
            return result.IndexOf(value);
        }
    }
}
