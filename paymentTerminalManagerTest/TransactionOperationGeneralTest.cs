using System.Threading.Tasks.Dataflow;

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
    }
}