using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Base.Application.UseCases.Voters.Commands.VoterRegister;
using Base.Application.UseCases.Voters.Queries.GetVoter;
using Base.Domain.Entities;
using Base.Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Tests;

public class VoterApiTest
{
    [Fact]
        public async Task GetSingleClientsSuccess()
        {            
            await using var webApp = new ApiApp();
            var serviceCollection = webApp.GetServiceCollection();
            using var scope = serviceCollection.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IGenericRepository<Voter>>();
            var voter = new Voter("1234567890", DateTime.Now.AddYears(-18), "Colombia");                
            var voterCreated = await repository.AddAsync( new Voter("1234567890", DateTime.Now.AddYears(-18), "Colombia"){ Id = webApp.UserId});
            var client = webApp.CreateClient();
            var singleVoter = await client.GetFromJsonAsync<List<VoterDto>>($"/api/voter");
            Assert.True(singleVoter is List<VoterDto>);                
        }


        [Fact]
        public async Task PostClientsSuccess()
        {            
            await using var webApp = new ApiApp();                                
            VoterRegisterCommand voter = new("123456789", "Colombia", DateTime.Now.AddYears(-20));                                              
            var client = webApp.CreateClient();
            var request = await client.PostAsJsonAsync("/api/voter/", voter);
            request.EnsureSuccessStatusCode();   
            var deserializeOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };  
            var responseData =  JsonSerializer.Deserialize<VoterDto>(await request.Content.ReadAsStringAsync(), deserializeOptions);
            Assert.True(responseData is not null);
            Assert.IsType<VoterDto>(responseData);          
        }

        [Fact]
        public async Task PostClientsFailureByAge()
        {
            HttpResponseMessage request = default!;
            try
            {
                await using var webApp = new ApiApp();                                
                VoterRegisterCommand voter = new("123456789", "Colombia", DateTime.Now.AddYears(-16));                                              
                var client = webApp.CreateClient();
                request = await client.PostAsJsonAsync<VoterRegisterCommand>("/api/voter/",voter);                
                request.EnsureSuccessStatusCode();                
                Assert.Fail("There's no way to get here if voter is underage");
            }
            catch (Exception)
            {
                var responseMessage = await request.Content.ReadAsStringAsync();
                Assert.True(request.StatusCode is HttpStatusCode.BadRequest && responseMessage.Contains("Voter is not 18 years or older"));
            }
        }
}

