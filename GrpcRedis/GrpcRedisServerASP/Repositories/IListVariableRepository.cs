using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcRedisServerASP.Repositories
{
    public interface IListVariableRepository
    {
        List<Variable> RPushVariable(List<Variable> Variables, string name, string value);

        List<Variable> RPopVariable(List<Variable> Variables, string name);

        List<Variable> LPushVariable(List<Variable> Variables, string name, string value);

        List<Variable> LPopVariable(List<Variable> Variables, string name);

        int LIndexVariable(List<Variable> Variables, string name, string value);
    }
}
