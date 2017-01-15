﻿using Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PhilipsHueConnector;
using PhilipsHueConnector.Entities;

namespace PhilipsHueUnitTests
{
    public class UnitTests
    {
        /// <summary>
        /// To set username, you first have to run test GenerateUserTest().
        /// This method will connect to Hue Gateway. BEfore you run it, click 
        /// the link button on the gatewey. Method GenerateUserName will return
        /// username, which you should set as value of this member variable.
        /// </summary>
        private string m_GtwUri = "http://192.168.?.?/";

        private string m_UsrName = "";

        /// <summary>
        /// Used by all tests to create instance of IotApi.
        /// </summary>
        /// <returns></returns>
        private IotApi getApi()
        {
            IotApi api = new IotApi();

            api.UsePhilpsQueueRest(m_GtwUri, m_UsrName);

            api.Open();

            return api;
        }


        /// <summary>
        /// This test will try to get new random user from gateway.
        /// If the button on gateway is not pressed, test will fail after 3 retries.
        /// </summary>
        [Fact]
        public void GenerateUserTest()
        {
            var username = new IotApi().GenerateUserName(m_GtwUri);

            Assert.Throws(typeof(Exception), () =>
            {

            });
            //
        }

        [Fact]
        public void GetLightsTest()
        {
            var iotApi = getApi();

            List<PhilipsHueConnector.Entities.Device> result = iotApi.SendAsync(new GetLights()).Result as List<PhilipsHueConnector.Entities.Device>;

            Assert.NotNull(result);

            Assert.Equal(result.Count, TestDriver.NumOfDevices);
        }


        [Fact]
        public void GetLightsJSApiStyleTest()
        {
            var iotApi = getApi();

            iotApi.SendAsync(new GetLights(), (result) =>
            {
                Assert.NotNull(result);

                Assert.IsType<List<PhilipsHueConnector.Entities.Device>>(result);

                List<PhilipsHueConnector.Entities.Device> res = result as List<PhilipsHueConnector.Entities.Device>;

                Assert.Equal(res.Count, TestDriver.NumOfDevices);
            },
            (err) =>
            {
                throw err;
            }).Wait();
        }


        [Fact]
        public void SwitchOnLightTest()
        {
            var iotApi = getApi();

            var result = iotApi.SendAsync(new SetLightState()
            {
                 Id = TestDriver.LightStateReferenceId,

                 State = new State()
                 {
                     On = true,
                     Bri = 254,
                 },

            }).Result;
        }
    }
}
