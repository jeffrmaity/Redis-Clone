// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/greet.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace GrpcRedisServerASP {
  /// <summary>
  /// The greeting service definition.
  /// </summary>
  public static partial class Greeter
  {
    static readonly string __ServiceName = "greet.Greeter";

    static readonly grpc::Marshaller<global::GrpcRedisServerASP.Command> __Marshaller_greet_Command = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcRedisServerASP.Command.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcRedisServerASP.CommandReply> __Marshaller_greet_CommandReply = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcRedisServerASP.CommandReply.Parser.ParseFrom);

    static readonly grpc::Method<global::GrpcRedisServerASP.Command, global::GrpcRedisServerASP.CommandReply> __Method_ExecuteCommand = new grpc::Method<global::GrpcRedisServerASP.Command, global::GrpcRedisServerASP.CommandReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ExecuteCommand",
        __Marshaller_greet_Command,
        __Marshaller_greet_CommandReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GrpcRedisServerASP.GreetReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Greeter</summary>
    [grpc::BindServiceMethod(typeof(Greeter), "BindService")]
    public abstract partial class GreeterBase
    {
      public virtual global::System.Threading.Tasks.Task<global::GrpcRedisServerASP.CommandReply> ExecuteCommand(global::GrpcRedisServerASP.Command request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(GreeterBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_ExecuteCommand, serviceImpl.ExecuteCommand).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GreeterBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_ExecuteCommand, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcRedisServerASP.Command, global::GrpcRedisServerASP.CommandReply>(serviceImpl.ExecuteCommand));
    }

  }
}
#endregion