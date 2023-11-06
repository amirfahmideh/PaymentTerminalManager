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