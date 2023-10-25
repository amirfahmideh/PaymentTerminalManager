using PaymentTerminalManager;
using PaymentTerminalManager.Exceptions;
using PaymentTerminalManager.implement;
using PaymentTerminalManager.Interface;
namespace PaymentTerminalManager.Factory;
internal class TransactionOperationFactory {
    internal static ITransactionOperation CreateTerminalOperation(SupportedTerminal terminalType ) {
        switch (terminalType) {
            case SupportedTerminal.BEHPARDAKHT : 
                return new BehpardakhtTransactionOperation();
            case SupportedTerminal.SADAD_BLUEBIRD : {
                return new SadadBluebirdTransactionOperation();
            }
            default:               
                throw new TerminalNotImplementException($"ترمینال از نوع {terminalType} پیاده سازی نشده است");
        }
    }
}