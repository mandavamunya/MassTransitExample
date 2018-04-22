using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Messages;

namespace ExampleMicroservice
{
	public class ListUserConsumer : IConsumer<IUserDataRequest>
	{
		public async Task Consume(ConsumeContext<IUserDataRequest> context)
		{
			var userId = context.Message.Id;
			var db = new Database();

			var user = db.Find().SingleOrDefault(u => u.Id == userId);

			var response = new UserDataResponse
			{
				Id = user.Id,
				Surname = user.Surname,
				Firstname = user.Firstname
			};

			await context.RespondAsync<IUserDataResponse>(response);
		}
	}
}