namespace paymentTerminalManagerTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ImplementedSummaryIsExistForAllTypes()
        {
            paymentTerminalManager.TransactionOperation transactionOperation = new paymentTerminalManager.TransactionOperation();
            var behpardakhtInfo = transactionOperation.ImplementSummery(SupportedTerminal.BEHPARDAKHT);
            Assert.IsFalse(string.IsNullOrEmpty(behpardakhtInfo));
        }

        //[Test]
        //public void ConnectionToTerminal()
        //{
        //    try
        //    {
        //        paymentTerminalManager.TransactionOperation transactionOperation = new paymentTerminalManager.TransactionOperation();
        //        var behpardakhtInfo = transactionOperation.SendToTerminal(SupportedTerminal.BEHPARDAKHT, new paymentTerminalManager.dto.SendToTerminal
        //        {
        //            IP = "192.168.30.182",
        //            Port = 1024,
        //            Price = 10000,
        //            RequestId = "1"
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