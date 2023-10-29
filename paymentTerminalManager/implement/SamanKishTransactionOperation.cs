using PaymentTerminalManager.dto;
using PaymentTerminalManager.Interface;
using PaymentTerminalManager.Lib;
using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace PaymentTerminalManager.implement
{
    internal class SamanKishTransactionOperation : ITransactionOperation
    {
        public SendToTerminalResult SendToTerminal(SendToTerminal sendToTerminal)
        {
            SendToTerminalResult result = new SendToTerminalResult();
            SSP1126.PcPos.BaseClasses.PcPosFactory pcPos = new SSP1126.PcPos.BaseClasses.PcPosFactory();
            
            pcPos.SetLan(sendToTerminal.IP);
            pcPos.Initialization(SSP1126.PcPos.Infrastructure.ResponseLanguage.Persian,180, SSP1126.PcPos.Infrastructure.AsyncType.Sync);
            // var terminalResult = pcPos.PcStarterPurchase(PriceConvert.ConvertDecimalToLongString(sendToTerminal.Price), string.Empty, string.Empty, string.Empty,null,null,null,-1,null,-1);
            var terminalResult = pcPos.PcStarterPurchase(PriceConvert.ConvertDecimalToLongString(sendToTerminal.Price), string.Empty, string.Empty, string.Empty);
            
            if(terminalResult.ResponseCode == "0") {
                result.IsSuccess = true;
                result.Price = Decimal.TryParse(terminalResult.ReqAmount, out decimal trsPrice) ? trsPrice : 0;
                result.AccountNo = terminalResult.TraceNumber;
                result.CardNumber = terminalResult.CardNumberMask;
                result.TransactionSerialNumber = terminalResult.RRN;
                result.TerminalNo = terminalResult.TerminalId;
                result.ErrorCode = terminalResult.ResponseCode;
                result.ErrorTitle = terminalResult.ResponseDescription;
                result.TransactionDateTime = DateTime.Now;
                result.ReferenceNumber = terminalResult.SerialId;

            }
            else {
                result.IsSuccess = false;
                result.ErrorCode = terminalResult.ResponseCode;
                result.ErrorTitle = terminalResult.ResponseDescription;
            }
            return result;
        }
        public string ImplementSummery()
        {
            return "5.3.531";
        }
   }
}