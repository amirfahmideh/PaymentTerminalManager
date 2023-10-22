using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using paymentTerminalManager.dto;

namespace paymentTerminalManager
{
    internal interface ITransactionOperation
    {
        SendToTerminalResult SendToTerminal(SendToTerminal sendToTerminal);
        string ImplementSummery();
    }
}
