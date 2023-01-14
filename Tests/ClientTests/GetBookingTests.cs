using InterviewService.Tests.ClientTests.Fixtures;
using Shouldly;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace InterviewService.Tests.ClientTests
{
    public class GetBookingTests
    {
        [Fact]
        public async Task GetBookingById_ShouldReturnNotFound_ForValidId()
        {
            // [Arrange]

            var (testEnvironment, client) = ClientTestFixtures.PrepareTestEnvironMent();
            var provider = testEnvironment.AddProvider();
            var customer = testEnvironment.AddCustomer(provider: provider);
            var evnt = testEnvironment.AddEvent(provider: provider);
            var booking = testEnvironment.AddBooking(evnt: evnt, customer: customer);
            var role = testEnvironment.AddRoleToProvider(provider.Id, testEnvironment.Configuration.SERVICE_USERID);

            // [Act]

            var response = await client.GetBooking(booking.Id);

            // [Assert]

            response.Successful.ShouldBe(true);
            response.HttpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Value.Id.ShouldBe(booking.Id);
        }

        [Fact]
        public async Task GetBookingById_ShouldReturnNotFound_ForInvalidId()
        {
            // [Arrange]

            var (_, client) = ClientTestFixtures.PrepareTestEnvironMent();
            var notExistingBookingId = new Guid("c15e7b6a-09aa-4335-8f1f-b8e25ae0e919");

            // [Act]

            var response = await client.GetBooking(notExistingBookingId);

            // [Assert]

            response.Successful.ShouldBe(false);
            response.HttpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            response.Value.ShouldBe(null);
        }
    }
}
