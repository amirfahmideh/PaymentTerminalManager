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
                Assert.That("10", Is.EqualTo(PriceConvert.ConvertDecimalToLongString(num)));
                num = 10.20M;
                Assert.That("10", Is.EqualTo(PriceConvert.ConvertDecimalToLongString(num)));
                num = 0;
                Assert.That("0", Is.EqualTo(PriceConvert.ConvertDecimalToLongString(num)));
                num = 5 / 2;
                Assert.That("2", Is.EqualTo(PriceConvert.ConvertDecimalToLongString(num)));
            });
        }
    }
}