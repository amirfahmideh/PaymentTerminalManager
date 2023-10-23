using NUnit.Framework.Interfaces;
using PaymentTerminalManager.Lib;

namespace PaymentTerminalManagerTest
{
    public class PriceConvertTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestStringPrice()
        {
            decimal num;
            Assert.Multiple(()=>{
                num = 10;
                Assert.That(PriceConvert.ConvertDecimalToLongString(num), Is.EqualTo("10"));
                num = 10.20M;
                Assert.That(PriceConvert.ConvertDecimalToLongString(num), Is.EqualTo("10"));
                num = 0;
                Assert.That(PriceConvert.ConvertDecimalToLongString(num), Is.EqualTo("0"));
                num = 5 / 2;
                Assert.That(PriceConvert.ConvertDecimalToLongString(num), Is.EqualTo("2"));
            });
        }
    }
}