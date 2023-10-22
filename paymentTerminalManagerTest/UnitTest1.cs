namespace paymentTerminalManagerTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            paymentTerminalManager.TransactionOperation to = new paymentTerminalManager.TransactionOperation();
            var id = to.SendToTerminal(SupportedTerminal.BEHPARDAKHT, new paymentTerminalManager.dto.SendToTerminal
            {
                IP = "192.168.30.182",
                Port = 1024,
                Price = 100000,
                RequestId = "10"
            });
        }
    }
}