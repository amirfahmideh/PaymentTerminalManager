using PaymentTerminalManager.dto;
using PaymentTerminalManager.Interface;
using PaymentTerminalManager.Lib;
using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PaymentTerminalManagerTest")]
namespace PaymentTerminalManager.implement
{
    internal class SadadBluebirdTransactionOperation : ITransactionOperation
    {
        public SendToTerminalResult SendToTerminal(SendToTerminal sendToTerminal)
        {
            SendToTerminalResult result = new SendToTerminalResult();
            Sadad.PcPos.Core.PcPosBusiness pcPosBusiness = new Sadad.PcPos.Core.PcPosBusiness(Sadad.PcPos.Core.DeviceType.BlueBird)
            {
                Ip = sendToTerminal.IP,
                Port = sendToTerminal.Port,
                Amount = PriceConvert.ConvertDecimalToLongString(sendToTerminal.Price)
            };
            var terminalResult = pcPosBusiness.SyncSaleTransaction();
            if(terminalResult.ResponseCode == "00" ) {
                result.IsSuccess = true;
                result.Price = Decimal.TryParse(terminalResult.Amount, out decimal trsPrice) ? trsPrice : 0;
                result.AccountNo = terminalResult.OrderId;
                result.CardNumber = terminalResult.CardNo;
                result.TransactionSerialNumber = terminalResult.Rrn;
                result.TerminalNo = terminalResult.TerminalId;
                result.ErrorCode = terminalResult.ResponseCode;
                result.ErrorTitle = terminalResult.ResponseCodeMessage;
                result.TransactionDateTime = DateTimeConvert.ParsPersianDateTime(terminalResult.TransactionDate, terminalResult.TransactionTime);
            }
            else{

                result.IsSuccess = false;
                result.ErrorCode = terminalResult.ResponseCode;
                result.ErrorTitle = terminalResult.ResponseCodeMessage;
            }
           
            return result;
        }
        public string ImplementSummery()
        {
            return "5.3.531";
        }
   }
}