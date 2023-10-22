using paymentTerminalManager.dto;

namespace paymentTerminalManager.implement;
internal class BehpardakhtTransactionOperation : ITransactionOperation
{
    public SendToTerminalResult SendToTerminal(SendToTerminal sendToTerminal)
    {
        SendToTerminalResult result = new SendToTerminalResult();
        POS_PC_v3.Transaction.Connection connection = new POS_PC_v3.Transaction.Connection
        {
            CommunicationType = "serial",
            POS_IP = sendToTerminal.IP,
            POS_PORTtcp = sendToTerminal.Port
        };

        POS_PC_v3.Transaction t = new POS_PC_v3.Transaction(connection);
        POS_PC_v3.Result terminalResult = t.Payment("True",sendToTerminal.RequestId,"1",sendToTerminal.Price.ToString(),string.Empty,string.Empty,string.Empty);
        if(terminalResult.ReturnCode == (int)POS_PC_v3.Result.return_codes.RET_OK) {
            result.IsSuccess = true;
            result.Price = string.IsNullOrEmpty(terminalResult.Amount) ? (decimal?)null : Convert.ToDecimal(terminalResult.Amount);
            result.AccountNo = terminalResult.AccountNo;
            result.CardNumber = terminalResult.PAN;
            result.TransactionDate = terminalResult.TransactionDate;
            result.TransactionTime = terminalResult.TransactionTime;
            result.TransactionSerialNumber = terminalResult.SerialTransaction;
            result.TerminalNo = terminalResult.TerminalNo;
        }
        else {
            result.ErrorCode = terminalResult.ReturnCode.ToString();
        }
        return result;
    }
    public string ImplementSummery()
    {
        return $"version: {POS_PC_v3.Globals.dllVersion}"; 
    }

}
