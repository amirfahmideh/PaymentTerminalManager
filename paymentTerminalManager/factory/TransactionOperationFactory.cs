using paymentTerminalManager.implement;
using PaymentTerminalManager;
using PaymentTerminalManager.Exceptions;
using PaymentTerminalManager.implement;
using PaymentTerminalManager.Interface;
namespace PaymentTerminalManager.Factory;
internal class TransactionOperationFactory
{
    internal static ITransactionOperation CreateTerminalOperation(SupportedTerminal terminalType)
    {
        return terminalType switch
        {
            SupportedTerminal.BEHPARDAKHT => new BehpardakhtTransactionOperation(),
            SupportedTerminal.SADAD_BLUEBIRD => new SadadBluebirdTransactionOperation(),
            SupportedTerminal.SAMANKISH => new SamanKishTransactionOperation(),
            SupportedTerminal.PARDAKHTNOVIN => new PardakhtNovinTransactionOperation(),
            _ => throw new TerminalNotImplementException($"ترمینال از نوع {terminalType} پیاده سازی نشده است"),
        };
    }
}