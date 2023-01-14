using InterviewService.Client;
using Tests;

namespace InterviewService.Tests.ClientTests.Fixtures
{
    internal static class ClientTestFixtures
    {
        public static (TestEnvironment TestEnVironMent, InterviewServiceClient Client) PrepareTestEnvironMent()
        {
            //Ensure service is running
            TestServer.StartService();

            var testEnvironment = new TestEnvironment();
            var client = new InterviewServiceClient("http://localhost:5000", testEnvironment.Configuration.SERVICE_USERTOKEN);

            return (testEnvironment, client);
        }
    }
}
