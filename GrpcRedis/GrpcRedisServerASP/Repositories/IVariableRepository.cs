using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcRedisServerASP.Repositories
{
    public interface IVariableRepository
    {
        Variable Variable { get; set; }

        List<Variable> AddNewVariable(List<Variable> Variables, string name, string value);

        Variable GetVariable(List<Variable> Variables, string name);

        List<Variable> ExpireVariableAsync(List<Variable> Variables, string name, int seconds);
    }
}
