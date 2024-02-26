using PaymentTerminalManager.dto;
using PaymentTerminalManager.Interface;
using PaymentTerminalManager.Lib;
using Intek.PcPosLibrary;
using System.Text;
using System.Text.RegularExpressions;

namespace paymentTerminalManager.implement
{
    internal class PardakhtNovinTransactionOperation : ITransactionOperation
    {
        private readonly PCPOS pcpos = new() { ConnectionType = PCPOS.cnType.LAN };
        private readonly ManualResetEvent responseReadyEvent = new(false);
        public PardakhtNovinTransactionOperation() => pcpos.GetResponse += ResponseReady;

        public SendToTerminalResult SendToTerminal(SendToTerminal sendToTerminal)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            pcpos.Ip = sendToTerminal.IP;
            pcpos.Port = sendToTerminal.Port;
            pcpos.Amount = sendToTerminal.Price.ToString();
            pcpos.PrCode = sendToTerminal.RequestId;
            pcpos.send_transaction();

            responseReadyEvent.WaitOne();
            var result = new SendToTerminalResult();
            var transactionResponse = pcpos.Response.GetTrxnResp();
            int errorCode = ExtractNumberInt(transactionResponse);
            if (errorCode == 00)
            {
                result.IsSuccess = true;
                string amount = pcpos.Response.GetAmount();
                result.Price = ExtractNumberDecimal(amount);

                var panValue = pcpos.Response.GetPANID();
                result.CardNumber = ExtractNumber(panValue);

                result.AccountNo = string.Empty;                                   //please check this   TR = 561925         GetTraceNo
                var serialValue = pcpos.Response.GetTrxnRRN();                     //please check this   RN = 800657696924   GetTrxnRRN
                result.TransactionSerialNumber = ExtractNumber(serialValue);       //please check this   SR = 31             GetTrxnSerial

                var terminalValue = pcpos.Response.GetTerminalID();
                result.TerminalNo = ExtractNumber(terminalValue);

                var dateValue = pcpos.Response.GetTrxnDate();
                var date = ExtractNumber(dateValue);
                result.TransactionDateTime = DateTimeConvert.ParsPersianDateTime(date, "");
            }
            else
                result.IsSuccess = false;

            result.ErrorCode = errorCode.ToString();
            result.ErrorTitle = FriendlyErrorTitle(errorCode);
            return result;
        }

        private void ResponseReady(string response) => responseReadyEvent.Set();

        public string ImplementSummery() => $"version: 1.0.0.0";
        public Task<RefundFromTerminalResult> RefundRequest(RefundFromTerminal refundFromTerminal) => throw new NotImplementedException();

        private static readonly Dictionary<int, string> errorMaps = new()
            {
                {00 , "تراکنش به درستی انجام شده است."},
                {02 , "تراکنش قبلاً REVERSE شده است(در پاسخ به REVERSE)"},
                {03 , "پذیرنده کارت شناخته شده نیست.باید عمل بازکردن فروشگاه را انجام دهید."},
                {06 , "اشکال در سیستم مرکز"},
                {12 , "پیام نادرست است.یکی از دلایل این خطا عدم مجوز پایانه در انجام عملیات مورد نظر می باشد."},
                {13 , "مقدار ریالی غلط است.(مبلغ تراکنش صحیح نیست)"},
                {14 , "شماره کارت شناخته شده نیست.(کارت در سیستم خدمات کارت نامعتبر است)"},
                {15 , "در پاسخ به تراکنش باز کردن فروشگاه :قبلاً باز شده است"},
                {16 , "در پاسخ به تراکنش بستن فروشگاه: قبلاً بسته شده است."},
                {19 , "در پاسخ به تراکنش تراز تجاری پایانه:مجموع تراکنشهای پایانه با مجموع تراکنشهای مرکز برابر نیست."},
                {20 , "پاسخ دریافت شده از ATM ناشناخته است."},
                {21 , "هیچ عملی بر روی تراکنش انجام نشد."},
                {22 , "خطای سخت افزاری در ATM"},
                {24 , "در پاسخ به تراکنش پایان روز: عملیات انجام نشد.باید تراکنش دوباره تکرار گردد."},
                {25 , "داده ارسالی غلط است واین داده در مرکز پیدا نشد.(ممکن است شماره پایانه باشد)"},
                {29 , "در پاسخ به تراکنش پایان روز انجام نشده.لازم است پیگیری شود."},
                {30 , "فرمت اشتباه است."},
                {33 , "تاریخ انقضای کارت سپری شده است."},
                {34 , "تراکنش مالی تایید نشده است(در پاسخ به REVERSE)"},
                {37 , "کارت مقصد در تراکنش انتقال مشکل دارد."},
                {39 , "حساب اعتباری وجود ندارد."},
                {41 , "کارت به علت مفقود شدن مسدود موقت است."},
                {43 , "کارت به علت دزدیده شدن دائماًمسدود است."},
                {44 , "اطلاعات قبض صحیح نیست یا قبض وجود ندارد."},
                {46 , "بار کد صحیح نیست."},
                {47 , "قبض منقضی شده است."},
                {48 , "قبض قبلاً پرداخت شده است."},
                {50 , "لغو خرید پس از زمان مجاز صورت میگردد."},
                {51 , "کمبود وجه(جهت انجام تراکنش مالی)"},
                {53 , "خطای امنیتی"},
                {55 , "رمز نامعتبر"},
                {56 , "کارت نامعتبر(کارت تعریف نشده یا داده کارت غلط است)"},
                {57 , "تراکنش غیر مجاز"},
                {58 , "ترمینال اجازه عملیات مورد نظر را ندارد."},
                {59 , "مشکوک به تقلب"},
                {60 , "در پاسخ به تراکنش لیست سیاه یا پایان روز: لیست در مرکز حاضر نیست"},
                {61 , "مبلغ تراکنشهای مربوط به بهاین کارت از حد مجاز گذشته است"},
                {62 , "مبلغ تراکنشهای روز مربوط به بهاین کارت از حد مجاز گذشته است"},
                {63 , "MAC صحیح نمی باشد"},
                {64 , "در پاسخ به تراکنش پایان روز: تراکنش ارسال شده صحیح نیست."},
                {65 , "تعداد تراکنش های مربوط به این کارت از حد مجاز گذشته است."},
                {66 , "حساب مسدود است"},
                {67 , "کارت ضبظ شود"},
                {68 , "مانده کارت صحیح نیست"},
                {70 , "در پاسخ به تراکنش پایان روز: تراز صحیح نیست و لیست سیاه در مرکز حاضر نیست"},
                {71 , "در پاسخ به تراکنش پایان روز: تراکنش ارسال شده صحیح نیست ولیست سیاه حاضر نیست"},
                {74 , "بدلیل یکی بودن شماره حساب مبدا و مقصد تراکنش انجام نمی گردد"},
                {75 , "تعدادرمز نامعتبر از حد مجاز گذشته است"},
                {77 , "روز مالی صحیح نیست(در تراکنش شتابی)"},
                {78 , "کارت فعال نیست"},
                {79 , "حساب تعریف نشده است"},
                {80 , "به دلیل اشکال در حساب تراکنش پذیرفته نیست"},
                {81 , "کارت قبلاً باطل شده است"},
                {82 , "پاسخ ACK از ATM دریافت نشد"},
                {83 , "سیستم مرکزی آماده نیست"},
                {84 , "سیستم مرکزی فعال نیست"},
                {85 , "خطای درونی سیستم مرکزی"},
                {86 , "تراکنش روی این دستگاه مجاز نیست"},
                {87 , "خطای درونی سیستم مرکزی"},
                {88 , "خطای درونی سیستم مرکزی"},
                {89 , "خطای درونی سیستم مرکزی"},
                {90 , "ارتباط در حین پردازش قطع شده است"},
                {91 , "عدم دریافت پاسخ به علت وقوع TIMEOUT در مرکزو اعلام آن به دستگاه"},
                {92 , "صارد کننده نامعتبر است"},
                {93 , "به علت تراکنش ها صف تراکنشها در سیستم پر شده است"},
                {94 , "شماره تراکنش تکراری است"},
                {96 , "اشکال در سیستم مرکز"},
                {99 , "عدم ارتباط با مرکز"}
            };
        private string FriendlyErrorTitle(int errorCode)
        {
            const string defaultErrorMessage = "خطای نامشخصی رخداده است";
            return errorMaps.TryGetValue(errorCode, out string message) ? message : defaultErrorMessage;
        }

        int ExtractNumberInt(string input)
        {
            Match match = Regex.Match(input, @"\d+");
            if (match.Success)
                if (int.TryParse(match.Value, out int result))
                    return result;

            return 0;
        }
        decimal ExtractNumberDecimal(string input)
        {
            Match match = Regex.Match(input, @"\d+");
            if (match.Success)
                if (decimal.TryParse(match.Value, out decimal result))
                    return result;

            return 0;
        }
        string ExtractNumber(string input)
        {
            Match match = Regex.Match(input, @"\d+");
            if (match.Success)
                return match.Value;

            return string.Empty;
        }
    }
}