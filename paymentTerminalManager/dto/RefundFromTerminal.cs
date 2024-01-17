namespace PaymentTerminalManager.dto
{
    public class RefundFromTerminal
    {
        public long TerminalId {get;set;}
        public string UserName {get;set;} = "";
        public string Password {get;set;} = "";
        public long SaleReferenceId {get;set;}
        public long RefundPrice {get;set;}
    }
}