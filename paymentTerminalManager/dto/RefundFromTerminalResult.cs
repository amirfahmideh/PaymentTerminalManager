namespace PaymentTerminalManager.dto
{
    public class RefundFromTerminalResult
    {
        public bool IsSuccess {get;set;} = false;
        public string ReferenceNumber {get;set;}= default!;
        public string ErrorCode {get;set;}= default!;
        public string ErrorTitle {get;set;}= default!;
    }
}