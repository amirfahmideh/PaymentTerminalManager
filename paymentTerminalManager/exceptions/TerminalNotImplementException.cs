using System;
namespace PaymentTerminalManager.Exceptions;
public class TerminalNotImplementException : Exception {
    public TerminalNotImplementException()
    {
        
    }
    public TerminalNotImplementException(string terminalNotFound)
    : base(terminalNotFound) {

    }
    public TerminalNotImplementException(string terminalNotFound, Exception innerException)
    : base(terminalNotFound, innerException) {
        
    }
}