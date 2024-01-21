using PaymentTerminalManager.dto;
using PaymentTerminalManager.Interface;
using PaymentTerminalManager.Lib;
using ServiceReference;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("PaymentTerminalManagerTest")]
namespace PaymentTerminalManager.implement
{
    internal class BehpardakhtTransactionOperation : ITransactionOperation
    {
        const string BEHPARDAKHT_CONNECTION_TYPE = "TCP/IP";
        public SendToTerminalResult SendToTerminal(SendToTerminal sendToTerminal)
        {
            SendToTerminalResult result = new SendToTerminalResult();
            POS_PC_v3.Transaction.Connection connection = new POS_PC_v3.Transaction.Connection
            {
                POSPC_TCPCOMMU_SocketRecTimeout = 60000,
                CommunicationType = BEHPARDAKHT_CONNECTION_TYPE,
                POS_IP = sendToTerminal.IP,
                POS_PORTtcp = sendToTerminal.Port
            };


            POS_PC_v3.Transaction t = new POS_PC_v3.Transaction(connection);
            POS_PC_v3.Result terminalResult = t.Debits_Goods_And_Service(sendToTerminal.RequestId, "", PriceConvert.ConvertDecimalToLongString(sendToTerminal.Price), "", "", "");
            if (terminalResult.ReturnCode == (int)POS_PC_v3.Result.return_codes.RET_OK)
            {
                result.IsSuccess = true;
                result.Price = string.IsNullOrEmpty(terminalResult.Amount) ? (decimal?)null : Convert.ToDecimal(terminalResult.Amount);
                result.AccountNo = terminalResult.AccountNo;
                result.CardNumber = terminalResult.PAN;
                result.TransactionSerialNumber = terminalResult.TraceNumber;
                result.TerminalNo = terminalResult.TerminalNo;
                result.ErrorCode = terminalResult.ReturnCode.ToString();
                result.ErrorTitle = FriendlyErrorTitle(terminalResult.ReturnCode);
                result.TransactionDateTime = DateTimeConvert.ParsPersianDateTime(terminalResult.TransactionDate, terminalResult.TransactionTime);
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorCode = terminalResult.ReturnCode.ToString();
                result.ErrorTitle = FriendlyErrorTitle(terminalResult.ReturnCode);
            }
            return result;
        }
        public string ImplementSummery()
        {
            return $"version: {POS_PC_v3.Globals.dllVersion}";
        }
        private string FriendlyErrorTitle(int errorCode)
        {
            const string defaultErrorMessage = "خطای نامشخصی رخداده است";
            Dictionary<int, string> errorMaps = new Dictionary<int, string>() {
                {100,"تراکنش با موفقیت انجام شد"},
                {101,"اندازه پیام دریافتی از ترمینال معتبر نمی باشد"},
                {102,"پیام دریافتی از سیسنم نامعتبر می باشد"},
                {103,"کد پاسخ دریافتی از ترمینال نامعتبر می باشد"},
                {104,"مبلغ واردشده دریافتی/وارد شده نامعتبر می باشد"},
                {105,"کد پرداخت کننده نامعتبر می باشد"},
                {106,"مدت زمان انتظار برای دریافت پاسخ از ترمینال نامعتبر می باشد"},
                {107,"مدت زمان انتظار برای دریافت پیام از ترمینال به پایان رسیده است"},
                {109,"تراکنش به خظا خورد"},
                {110,"تراکنش به دلیل مشکل در چاپگر به خطا خورد"},
                {111,"تراکنش به دلیل خطا در برقراری اطلاعات به خطا خورد"},
                {112,"تراکنش به علت خطا در ارسال تعهدات، ایجاد تراکنش به خطا خورد"},
                {113,"پورت در نظر گرفته شده برای ارتباط سریال نامعتبر می باشد"},
                {114,"تراکنش توسط کاربر لغو شده است"},
                {115,"شناسه قبض برای پرداخت قبض نامعتبر می باشد"},
                {116,"شناسه تراکنش برای پرداخت قبض نامعتبر می باشد"},
                {117,"خطایی در باز کرد پورت سریال رخداده است"},
                {118,"ارسال داده به پرت سریال به خطا خورد"},
                {119,"پورت انتخاب شده، پورت معتبر سریال نمی باشد"},
                {120,"تعداد ورودی ها مربوط به این ورودی نامعتبر می باشد"},
                {121,"ورودی های مربوط به پورت سریال نامعتبر می باشد و یا پورت سریال معتبر می باشد"},
                {122,"خطا در ورودی خالی در هنگام ارسال به پورت سریال"},
                {123,"خطای مدت زمان انتظار در ارسال به پورت سریال"},
                {124,"کارت بانکی به ترمینال ارسال نشده است"},
                {125,"شناسه حساب برای فرایند پرداخت نامعتبر می باشد"},
                {126,"شناسه حساب دریافتی از سیستم نامعتبر می باشد"},
                {127,"شناسه دریاتی پرداخت کننده از سیستم نامعتبر می باشد"},
                {128,"مقدار دریافتی از سیستم نامعتبر می باشد"},
                {129,"شناسه ارجاع دریافتی از سیستم نامعتبر می باشد"},
                {130,"شناسه قبض دریافتی از سیستم نامعتبر می باشد"},
                {131,"شناسه پرداخت از سیستم نامعتبر می باشد"},
                {132,"داده های سربار ارسال شده از سیستم نامعتبر می باشد"},
                {133,"مجموع مقدار چند پرداختی دریافت شده نامعتبر می باشد"},
                {134,"داده دریافتی از سیستم توسط کاربر مورد تایید قرار نگرفت"},
                {161,"داده های ورودی برای پرداخت چند ردیفی نامعتبر می باشد"},
                {162,"مقدار وارد شده برای چندین پرداخت نامعتبر می باشد"},
                {163,"داده ورودی به عنوان کد مرجع نامعتبر می باشد"},
                {164,"خطای crc در هنگام دریافت اطلاعات از سیستم به ترمینال"},
                {166,"داده های ورودی مربوط پرتکل و پورت آن نامعتبر می باشد"},
                {167,"خطا در زمان انتظار برای دریافت اطلاعات از ترمینال "},
                {168,"در ایجاد ارتباط بر روی پرتکل شبکه خطایی اتفاق رخداده است"},
                {169,"در ارسال اطلاعات بر روی پرتکل شبکه خطایی رخداده است"},
                {170,"در دریافت اطلاعات از ترمینال بر روی پرتکل شبکه خطایی رخداده است"},
                {171,"ساختار پیام مرچنت نامعتبر می باشد"},
                {172,"در آماده سازی فرمت تی ال وی قبل از ارسال خطایی رخداده است"},
                {173,"خطای نامشخص در برقراری ارتباط با پرت سریال رخداده است"},
                {174,"خطای نامشخص در برقراری ارتباط با پرت سریال رخداده است"},
                {175,"در ساختار ایجاد سی ار سی پیام خطایی رخداده است"},
                {176,"داده سربار مرچنت نامعتبر می باشد"},
                {200,defaultErrorMessage},
            };
            return errorMaps.TryGetValue(errorCode, out string message) ? message : defaultErrorMessage;
        }

        public async Task<RefundFromTerminalResult> RefundRequest(RefundFromTerminal rft)
        {
            try
            {
                int errorCode = -1;
                string referenceNumber = "";
                bool isSuccess = false;
                PaymentGatewayClient client = new PaymentGatewayClient();
                var resultResponse = await client.bpRefundRequestAsync(rft.TerminalId, rft.UserName, rft.Password, 0, 0, rft.SaleReferenceId, rft.RefundPrice);
                var stringResult = resultResponse.Body.@return;
                if(!string.IsNullOrEmpty(stringResult)){
                    if(stringResult.IndexOf(",") > -1) {
                        var result = stringResult.Split(",");
                        if(int.TryParse(result[0], out int res)) {
                            if(res == 0) {
                                isSuccess = true;
                                referenceNumber = result[1];
                            }
                        }
                    }
                    else {
                        if(int.TryParse(stringResult, out int err))  {
                            errorCode = err;
                        }
                    }
                }
                if(isSuccess) {
                    return new RefundFromTerminalResult() {
                                        IsSuccess = true,
                                        ReferenceNumber = referenceNumber,
                                        ErrorCode = "",
                                        ErrorTitle = ""
                                    };
                }
                else {
                    return new RefundFromTerminalResult() {
                                        IsSuccess = false,
                                        ReferenceNumber = "",
                                        ErrorCode = errorCode.ToString(),
                                        ErrorTitle = FriendlyRefundErrorTitle(errorCode)
                                    };
                }
            }
            catch (Exception ex)
            {
                return new RefundFromTerminalResult() {
                    ErrorCode = "-1",
                    ErrorTitle = ex.Message,
                    IsSuccess = false,
                    ReferenceNumber = ""
                };
            }
        }

        private string FriendlyRefundErrorTitle(int errorCode)
        {
            const string defaultErrorMessage = "در فرایند ریفاند نامشخصی رخداده است";
            Dictionary<int, string> errorMaps = new Dictionary<int, string>() {
                {0,"تراکنش با موفقیت انجام شد"},
                {11,"شماره کارت نامعتبر می باشد "},
                {12,"موجودی کافی نیست"},
                {13,"رمز نادرست است"},
                {14,"تعداد دفعات وارد کردن رمز بیش از حد مجاز است"},
                {15,"کارت نامعتبر است"},
                {16,"دفعات برداشت بیش از حد مجاز است"},
                {17,"کاربر از انجام تراکنش منصرف شده است"},
                {18,"تاریخ انقضای کارت گذشته است"},
                {19,"مبلغ برداشت بیش از حد مجاز است"},
                {111,"صادر کننده کارت نامعتبر است"},
                {112,"خطایی سوییچ صادر کننده کارت"},
                {113,"پاسخی از صادر کننده کارت دریافت نشده است"},
                {114,"دارنده کارت مجاز به انجام این تراکنش نیست"},
                {21,"پذیرنده نامعتبر است"},
                {23,"خطای امنیتی رخداده است"},
                {24,"اطلاعات کاربری پذیرنده نامعتبر است"},
                {25,"مبلغ نامعتبر است"},
                {31,"پاسخ نامعتبر است"},
                {32,"فرمت اطلاعات ارسالی صحیح نمی باشد"},
                {33,"حساب نامعتبر است"},
                {34,"خطای سیستمی"},
                {35,"تاریخ نامعتبر است"},
                {41,"شماره درخواست تکراری است"},
                {42,"تراکنش فروش با این شناسه یافت نشد"},
                {43,"درخواست قبلا اعتبار سنجی شده است"},
                {44,"درخواست اعتبار سنجی یافت نشد"},
                {45,"تراکنش settle یافت نشد"},
                {46,"تراکنش settle نشده است"},
                {47,"تراکنش settle یافت نشد"},
                {48,"تراکنش reverse شده است"},
                {412,"شناسه قبض نادرست است"},
                {413,"شناسه پرداخت نادرست است"},
                {414,"سازمان صادر کننده قبض نامعتبر است"},
                {415,"زمان جلسه کاری به پایان رسیده است"},
                {416,"خطا در ثبت اطلاعات"},
                {417, "شناسه پرداخت کننده نامعتبر است"},
                {418,"اشکال در تعریف اطلاعات مشتری"},
                {419,"تعداد دفعات ورود اطلاعات از حد مجاز گذشته است"},
                {421,"IP نامعتبر است"},
                {51,"تراکنش تکراری است"},
                {54,"تراکنش مرجع موجود نیست"},
                {55,"تراکنش نامعتبر است "},
                {61,"خطا در واریز"},
                {62,"مسیر بازگشت به سایت در دامنه ثبت شده برای پذیرنده قرار ندارد"},
                {98,"سقف استفاده از رمز ایستا به پایان رسیده است"},
                {-1,defaultErrorMessage},
            };
            return errorMaps.TryGetValue(errorCode, out string message) ? message : defaultErrorMessage;
        }
    }
}