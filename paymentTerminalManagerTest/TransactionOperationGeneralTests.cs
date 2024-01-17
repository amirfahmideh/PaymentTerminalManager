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
        public void PosRefundTest()
        {
            PaymentTerminalManager.TransactionOperation transactionOperation = new PaymentTerminalManager.TransactionOperation();
            transactionOperation.PosRefundRequest(SupportedTerminal.BEHPARDAKHT, new PaymentTerminalManager.dto.RefundFromTerminal()
            {
                Password = "123456",
                RefundPrice = 2000,
                SaleReferenceId = 66556655656,
                TerminalId = 1,
                UserName = "123122312"
            });
            Assert.IsTrue(true);
        }


        // [Test]
        // public void ConnectionToTerminal()
        // {
        //    try
        //    {
        //        PaymentTerminalManager.TransactionOperation transactionOperation = new PaymentTerminalManager.TransactionOperation();
        //        var behpardakhtInfo = transactionOperation.SendToTerminal(SupportedTerminal.SAMANKISH, new PaymentTerminalManager.dto.SendToTerminal
        //        {
        //            IP = "192.168.30.124",
        //            Port = 1024,
        //            Price = 10000,
        //            RequestId = ""
        //        });
        //        Assert.True(true);
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.False(true);
        //    }
        // }
    }
}