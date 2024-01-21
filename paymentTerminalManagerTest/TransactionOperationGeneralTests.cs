namespace PaymentTerminalManagerTest
{
    public class TransactionOperationGeneralTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ImplementedSummaryIsExistForAllTypes()
        {
            PaymentTerminalManager.TransactionOperation transactionOperation = new PaymentTerminalManager.TransactionOperation();
            var behpardakhtInfo = transactionOperation.ImplementSummery(SupportedTerminal.BEHPARDAKHT);
            Assert.IsFalse(string.IsNullOrEmpty(behpardakhtInfo));
        }


        [Test]
        public async Task PosRefundTest()
        {
            PaymentTerminalManager.TransactionOperation transactionOperation = new PaymentTerminalManager.TransactionOperation();
            await transactionOperation.RefundRequest(SupportedTerminal.BEHPARDAKHT, new PaymentTerminalManager.dto.RefundFromTerminal()
            {
                Password = "54075867",
                RefundPrice = 50000,
                SaleReferenceId = 263072438688,
                TerminalId = 6520823,
                UserName = "land1401"
            });
            Assert.IsTrue(true);
        }


        //[Test]
        //public void ConnectionToTerminal()
        //{
        //    try
        //    {
        //        PaymentTerminalManager.TransactionOperation transactionOperation = new PaymentTerminalManager.TransactionOperation();
        //        var behpardakhtInfo = transactionOperation.SendToTerminal(SupportedTerminal.BEHPARDAKHT, new PaymentTerminalManager.dto.SendToTerminal
        //        {
        //            IP = "192.168.30.131",
        //            Port = 1024,
        //            Price = 50000,
        //            RequestId = ""
        //        });
        //        Assert.True(true);
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.False(true);
        //    }
        //}
    }
}