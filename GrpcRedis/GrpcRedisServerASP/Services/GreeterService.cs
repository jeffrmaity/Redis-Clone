using Grpc.Core;
using GrpcRedisServerASP.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcRedisServerASP
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly ICommandService _ICommandService;

        public GreeterService(ILogger<GreeterService> logger, ICommandService commandService)
        {
            _logger = logger;
            _ICommandService = commandService;
        }

        public override Task<CommandReply> ExecuteCommand(Command request, ServerCallContext context)
        {
            if (_ICommandService.ValidateCommand(request.Command_, out string commandreply))
                commandreply = _ICommandService.ExecuteCommand(request.Command_);

            return Task.FromResult(new CommandReply
            {
                Command = commandreply
            });
        }
    }
}
