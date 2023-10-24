using NUnit.Framework.Interfaces;
using PaymentTerminalManager.Lib;

namespace PaymentTerminalManagerTest
{
    public class DateTimeConvertTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void DateTimeUtilityTest()
        {
            Assert.Multiple(()=>{
                var d1 = DateTimeConvert.ParsPersianDateTime("1402/08/01","02:20");
                var d2 = DateTime.Parse("2023/10/23 02:20");
                Assert.That(d2, Is.EqualTo(d1));

                var d3 = DateTimeConvert.ParsPersianDateTime("1402/8/1","2:20");
                var d4 = DateTime.Parse("2023/10/23 2:20");
                Assert.That(d4, Is.EqualTo(d3));

                var d5 = DateTimeConvert.ParsPersianDateTime("1402/8/1","");
                var d6 = DateTime.Parse($"2023/10/23 {DateTime.Now.Hour}:{DateTime.Now.Minute}");
                Assert.That(d5, Is.EqualTo(d6));
            });
        }
    }
}