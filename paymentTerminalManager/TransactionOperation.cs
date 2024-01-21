using PaymentTerminalManager.dto;
using PaymentTerminalManager.Factory;

namespace PaymentTerminalManager {
    public class TransactionOperation {
        public SendToTerminalResult SendToTerminal(SupportedTerminal terminalType, SendToTerminal sendData) {
            var terminalOperation = TransactionOperationFactory.CreateTerminalOperation(terminalType);
            return terminalOperation.SendToTerminal(sendData);
        }

        public string ImplementSummery(SupportedTerminal terminalType) {
            var terminalOperation = TransactionOperationFactory.CreateTerminalOperation(terminalType);
            return terminalOperation.ImplementSummery();
        }

        public async Task<RefundFromTerminalResult> RefundRequest(SupportedTerminal terminalType, RefundFromTerminal refundFromTerminal) {
            var terminalOperation = TransactionOperationFactory.CreateTerminalOperation(terminalType);
            return await terminalOperation.RefundRequest(refundFromTerminal);
        }
    }
}