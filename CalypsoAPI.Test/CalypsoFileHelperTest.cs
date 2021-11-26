using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CalypsoAPI.Core.Models.State;
using CalypsoAPI.Core;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;

namespace CalypsoAPI.Test
{
    [TestClass]
    public class CalypsoFileHelperTest
    {
        [TestMethod]
        public async Task ParseCommandFile()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "files");
            var result = await CalypsoFileHelper.GetCommandFileAsync(path);

            Assert.IsNotNull(result);
            Assert.AreEqual<string>("set_cnc_end", result.state);
            Assert.AreEqual(@"c:\Users\Public\Documents\Zeiss\CALYPSO\workarea\results\Test_chr.txt", result.chrPath);
        }

        [TestMethod]
        public async Task ParseStartFile()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "files");
            var result = await CalypsoFileHelper.GetStartFileAsync(path);

            Assert.IsNotNull(result);
            Assert.AreEqual("300", result.speed);
            Assert.AreEqual("60", result.partnbinc);
        }

        [TestMethod]
        public async Task ParseObserver()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "files");
            var result = await CalypsoFileHelper.GetObserverFileAsync(path);

            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.planid);
            Assert.AreEqual("13", result.partnbinc);
        }

        [TestMethod]
        public async Task ParseChrFile()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "files/test_chr.txt");
            var result = await CalypsoFileHelper.GetMeasurementResultAsync(path);

            Assert.IsNotNull(result);
            Assert.AreEqual("0.0007984", result.Measurements[0].Deviation);
            Assert.AreEqual("FlatnessA", result.Measurements[0].Id);
            Assert.AreEqual(30, result.Measurements.Count);

            Assert.AreEqual("10", result.ChrTable.Rows[0].ItemArray[1]);

            Assert.IsTrue(result.ChrFile.Length > 0);
        }
    }
}
