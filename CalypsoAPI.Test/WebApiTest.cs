using CalypsoAPI;
using CalypsoAPI.Interface;
using CalypsoAPI.Models;
using CalypsoAPI.Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;

namespace CalypsoAPI.Test
{
    [TestClass]
    public class WebApiTest
    {
        private ICalypso calypso;

        [TestInitialize]
        public async Task Initialize()
        {
            calypso = new CalypsoMock();
            calypso.Configuration = new CalypsoConfiguration() { CMMObserverFolderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "files") };
            calypso.Services.Add(new CalypsoAPI.WebApi.WebApi(calypso));
            await calypso.StartAsync();
        }

        [TestMethod]
        public async Task Controller_Test()
        {
            HttpClient client = new HttpClient();
            var resp = await client.GetFromJsonAsync<List<Measurement>>("http://localhost:80/api/v1/measurements");

            if (resp == null)
                Assert.Fail();

            Assert.AreEqual(30, resp.Count);
            Assert.AreEqual("0.0007984", resp[0].Deviation);
            Assert.AreEqual("FlatnessA", resp[0].Id);

            var state = await client.GetFromJsonAsync<string>("http://localhost:80/api/v1/state");

            Assert.AreEqual("Running", state);
        }
    }
}
