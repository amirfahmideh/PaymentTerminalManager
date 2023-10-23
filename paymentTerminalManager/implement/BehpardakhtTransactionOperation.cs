using PaymentTerminalManager.dto;
using PaymentTerminalManager.Interface;
using PaymentTerminalManager.Lib;
using System;

namespace PaymentTerminalManager.implement
{
    internal class BehpardakhtTransactionOperation : ITransactionOperation
    {
        const string BEHPARDAKHT_CONNECTION_TYPE = "TCP/IP";
        public SendToTerminalResult SendToTerminal(SendToTerminal sendToTerminal)
        {
            SendToTerminalResult result = new SendToTerminalResult();
            POS_PC_v3.Transaction.Connection connection = new POS_PC_v3.Transaction.Connection
            {
                POSPC_TCPCOMMU_SocketRecTimeout = 60000,
                CommunicationType = BEHPARDAKHT_CONNECTION_TYPE,
                POS_IP = sendToTerminal.IP,
                POS_PORTtcp = sendToTerminal.Port
            };

            POS_PC_v3.Transaction t = new POS_PC_v3.Transaction(connection);
            POS_PC_v3.Result terminalResult = t.Debits_Goods_And_Service(sendToTerminal.RequestId, "", PriceConvert.ConvertDecimalToLongString(sendToTerminal.Price) , "", "", "");
            if (terminalResult.ReturnCode == (int)POS_PC_v3.Result.return_codes.RET_OK)
            {
                result.IsSuccess = true;
                result.Price = string.IsNullOrEmpty(terminalResult.Amount) ? (decimal?)null : Convert.ToDecimal(terminalResult.Amount);
                result.AccountNo = terminalResult.AccountNo;
                result.CardNumber = terminalResult.PAN;
                result.TransactionDate = terminalResult.TransactionDate;
                result.TransactionTime = terminalResult.TransactionTime;
                result.TransactionSerialNumber = terminalResult.SerialTransaction;
                result.TerminalNo = terminalResult.TerminalNo;
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorCode = terminalResult.ReturnCode.ToString();
            }
            return result;
        }
        public string ImplementSummery()
        {
            return $"version: {POS_PC_v3.Globals.dllVersion}";
        }
    }
}