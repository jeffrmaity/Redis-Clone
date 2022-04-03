using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcRedisServerASP.Repositories
{
    public interface INumberVariableRepository
    {
        List<Variable> IncrementVariable(List<Variable> Variables, string name, int byValue);

        List<Variable> DecrementVariable(List<Variable> Variables, string name, int byValue);
    }
}
