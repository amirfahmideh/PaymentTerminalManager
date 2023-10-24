namespace PaymentTerminalManager.dto
{
    public class SendToTerminalResult
    {
        public bool IsSuccess {get;set;} = false;
        public string AccountNo { get; set; } = default!;
        public string TerminalNo { get; set; }= default!;
        public string CardNumber { get; set; }= default!;
        public string TransactionSerialNumber {get;set;}= default!;
        public string ReferenceNumber {get;set;}= default!;
        public string ErrorCode {get;set;}= default!;
        public string ErrorTitle {get;set;}= default!;
        public decimal? Price {get;set;}
        public DateTime TransactionDateTime {get;set;}
        public SendToTerminalResult()
        {
            IsSuccess = false;
            AccountNo = string.Empty;
            TerminalNo = string.Empty;
            CardNumber = string.Empty;
            TransactionSerialNumber = string.Empty;
            ReferenceNumber = string.Empty;
            ErrorCode = string.Empty;
            ErrorTitle = string.Empty;
            Price = null;
            TransactionDateTime = DateTime.Now;
        }
    }
}