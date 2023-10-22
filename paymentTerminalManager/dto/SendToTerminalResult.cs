namespace paymentTerminalManager.dto
{
    public class SendToTerminalResult
    {
        public bool IsSuccess {get;set;} = true;
        public string AccountNo { get; set; } = default!;
        public string TerminalNo { get; set; }= default!;
        public string CardNumber { get; set; }= default!;
        public string TransactionDate {get;set;}= default!;
        public string TransactionTime {get;set;}= default!;
        public string TransactionSerialNumber {get;set;}= default!;
        public string ReferenceNumber {get;set;}= default!;
        public string ErrorCode {get;set;}= default!;
        public decimal? Price {get;set;}
    }
}