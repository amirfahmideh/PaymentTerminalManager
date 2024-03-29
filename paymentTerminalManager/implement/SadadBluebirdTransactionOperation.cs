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
                result.AccountNo = terminalResult.MerchantId;
                result.CardNumber = terminalResult.CardNo;
                result.TransactionSerialNumber = terminalResult.TransactionNo;
                result.TerminalNo = terminalResult.TerminalId;
                result.ErrorCode = terminalResult.ResponseCode;
                result.ErrorTitle = terminalResult.ResponseCodeMessage;
                result.TransactionDateTime = DateTimeConvert.ParsPersianDateTime(terminalResult.TransactionDate, terminalResult.TransactionTime);
                result.ReferenceNumber = terminalResult.MerchantId;
            }
            else{

                result.IsSuccess = false;
                result.ErrorCode = terminalResult.PcPosStatusCode.ToString();
                result.ErrorTitle = terminalResult.PcPosStatus;
            }
           
            return result;
        }
        public string ImplementSummery()
        {
            return "5.3.531";
        }

        public Task<RefundFromTerminalResult> RefundRequest(RefundFromTerminal refundFromTerminal)
        {
            throw new NotImplementedException();
        }
    }
}