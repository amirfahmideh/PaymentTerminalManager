using PaymentTerminalManager.dto;

namespace PaymentTerminalManager.Interface
{
    internal interface ITransactionOperation
    {
        SendToTerminalResult SendToTerminal(SendToTerminal sendToTerminal);
        string ImplementSummery();
        Task<RefundFromTerminalResult> RefundRequest(RefundFromTerminal refundFromTerminal);
    }
}