using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		private readonly IBus _bus;

		public UsersController(IBus bus)
		{
			_bus = bus;
		}

		// GET api/values
		[HttpGet]
		[Route("{id}")]
		public async Task<IUserDataResponse> Get(int id)
		{
			var address = new Uri("rabbitmq://localhost/UserData");

			var client = new MessageRequestClient<IUserDataRequest, IUserDataResponse>(_bus, address, TimeSpan.FromSeconds(30));
			var response = await client.Request(new {Id = id});
			//var addUserEndpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/UserData"));

			//await addUserEndpoint.Send<IUserDataRequest>(new {Id = 1});
			return response;
		}

		
	}
}
