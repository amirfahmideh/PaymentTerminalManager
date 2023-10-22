using paymentTerminalManager;
using paymentTerminalManager.implement;

internal class TransactionOperationFactory {
    internal static ITransactionOperation CreateTerminalOperation(SupportedTerminal terminalType ) {
        switch (terminalType) {
            case SupportedTerminal.BEHPARDAKHT : 
                return new BehpardakhtTransactionOperation();
            default:               
                throw new TerminalNotImplementException($"ترمینال از نوع {terminalType} پیاده سازی نشده است");
        }
    }
}