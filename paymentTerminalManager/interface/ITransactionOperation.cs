using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentTerminalManager.dto;

namespace PaymentTerminalManager.Interface
{
    internal interface ITransactionOperation
    {
        SendToTerminalResult SendToTerminal(SendToTerminal sendToTerminal);
        string ImplementSummery();
    }
}
