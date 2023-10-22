using paymentTerminalManager.dto;

namespace paymentTerminalManager;
public class TransactionOperation {
    public SendToTerminalResult SendToTerminal(SupportedTerminal terminalType, SendToTerminal sendData) {
        var terminalOperation = TransactionOperationFactory.CreateTerminalOperation(terminalType);
        return terminalOperation.SendToTerminal(sendData);
    }
}