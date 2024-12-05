using TechTalk.SpecFlow;
using EnsekAPITests.Utils;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using EnsekAPITests.HelperMethods;
using Newtonsoft.Json.Linq;
using FluentAssertions;
using System.Text;

namespace EnsekAPITests.Steps
{
    [Binding]
    public sealed class BuyEnergyStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private readonly Settings _settings;
        string resetMessage = string.Empty;
        string buyFuelMessage = string.Empty;
        string previousOrderID = string.Empty;
        string fuelType = string.Empty;
        int ordersCount = 0;
        int quantity = 0;

        public BuyEnergyStepDefinitions(ScenarioContext scenarioContext, Settings settings)
        {
            _scenarioContext = scenarioContext;
            _settings = settings;
        }

        [Given(@"I login to obtain access token")]
        public void GivenILoginToObtainAccessToken()
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile(@"C:\Users\savio\source\repos\EnsekAPITests\bin\Debug\netcoreapp3.1\appsettings.json").Build();
            _settings.TestServerEndpoint = MyConfig.GetValue<string>("AppSettings:TestServerEndpoint");
            var loginEndpoint = MyConfig.GetValue<string>("AppSettings:LoginEndpoint");

            var y = HttpMethods.PostMethod(_settings.TestServerEndpoint, loginEndpoint).Result;

            dynamic json = JValue.Parse(y);

            _settings.Token = json.access_token;
        }

        [Given(@"I reset the test data back to its initial state")]
        public void GivenIResetTheTestDataBackToItsInitialState()
        {
            var resetEndpoint = "/ENSEK/reset";

            var message = HttpMethods.PostMethod(_settings.Token,_settings.TestServerEndpoint, resetEndpoint).Result;
            dynamic json = JValue.Parse(message);

            resetMessage = json.message;
        }

        [Then(@"The user should see a (.*) message")]
        public void ThenTheUserShouldSeeAMessage(string message)
        {
            message.Should().BeEquivalentTo(resetMessage, "The expected message is {0}", message);
        }

        [When(@"The user buys (.*) quantity of (.*) fuel")]
        public void WhenTheUserBuysQuantityOfFuel(int quantity, string fuelType)
        {
            int id = UtilityMethods.GetIDBasedOnFuelType(fuelType);
            //string updatedEndpoint = UtilityMethods.UpdateEndpointUrl(ApplicationConstants.PUT_BUY_UNIT_ENDPOINT, id: id, quantity: quantity);
            var message = HttpMethods.PutMethod(_settings.Token, _settings.TestServerEndpoint, ApplicationConstants.PUT_BUY_UNIT_ENDPOINT).Result;

            dynamic json = JValue.Parse(message);

            buyFuelMessage = json.message;
        }

        [When(@"The user sends request to obtain details of previous order with order id (.*)")]
        public void WhenTheUserSendsRequestToObtainDetailsOfPreviousOrderWithOrderID(string orderID)
        {
            StringBuilder sb = new StringBuilder(ApplicationConstants.GET_ORDERS_ID_ENDPOINT);
            sb.Replace("{orderId}", orderID);
            var message = HttpMethods.GetMethod(_settings.Token, _settings.TestServerEndpoint, sb.ToString()).Result;

            dynamic json = JValue.Parse(message);

            previousOrderID = json.id;
            fuelType = json.fuel;
            quantity = json.quantity;
        }

        [Then(@"The user should see (.*) details of previous orders")]
        public void ThenTheUserShouldSeeDetailsOfPreviousOrders(string orderId)
        {
            orderId.Should().BeEquivalentTo(previousOrderID, "The expected order id is {0}", orderId);
        }

        [Then(@"The user should see orders with fuel type (.*) , order id (.*) and quantity (.*) were created before the current date")]
        public void ThenTheUserShouldSeeOrdersWithFuelTypeOrderIdAndQuantityWereCreatedBeforeTheCurrentDate(string fuelType, string orderId, int quantity)
        {
            fuelType.Should().BeEquivalentTo(this.fuelType, "The expected order id is {0}", orderId);
            orderId.Should().BeEquivalentTo(previousOrderID, "The expected order id is {0}", orderId);
            quantity.Should().BeGreaterThanOrEqualTo(this.quantity, "The expected order id is {0}", quantity);
        }

        [When(@"The user sends request to obtain details on energy types")]
        public void WhenTheUserSendsRequestToObtainDetailsOnEnergyTypes()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The user would get back response message (.*)")]
        public void ThenTheUserWouldGetBackResponseMessage(string message)
        {
            message.Should().Contain(buyFuelMessage, "The expected message is {0}", message);
        }

        [When(@"The user sends GET request to obtain details of previous orders")]
        public void WhenTheUserSendsGETRequestToObtainDetailsOfPreviousOrders()
        {
            var message = HttpMethods.GetMethod(_settings.Token, _settings.TestServerEndpoint, ApplicationConstants.GET_ORDERS_ENDPOINT).Result;
            dynamic json = JValue.Parse(message);

            ordersCount = json.Count();
        }

        [Then(@"The user should see a total of (.*) orders")]
        public void ThenTheUserShouldSeeATotalOfOrders(int ordersCount)
        {
            ordersCount.Should().BeGreaterThanOrEqualTo(this.ordersCount, "The expected count is {0}", ordersCount);
        }
    }
}
