using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrpcRedisServerASP.Services
{
    public interface IVariableService
    {
        List<Variable> Variables { get; set; }

        string AddNewVariable(string name, string value);

        string GetVariable(string name);

        string IncrementVariable(string name);

        string IncrementByVariable(string name, string by);

        string DecrementVariable(string name);

        string DecrementByVariable(string name, string by);

        string RPushVariable(string name, string value);

        string RPopVariable(string name);

        string LPushVariable(string name, string value);

        string LPopVariable(string name);

        string LIndexVariable(string name, string value);

        string ExpireVariableAsync(string name, string value);
    }
}
