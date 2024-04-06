namespace FastFeet.Application.Commons.Command;

public abstract record IdempotentCommand(Guid RequestId): CommandBase { }