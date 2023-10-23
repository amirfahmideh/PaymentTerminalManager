namespace PaymentTerminalManager.dto
{
    public class SendToTerminal
    {
        public string RequestId {get;set;} = default!;
        public decimal Price { get; set; }
        public string IP {get;set;} = "192.168.30.182";
        public int Port {get;set;} = 1024;
    }
}