namespace Messages
{
    public  interface IUserDataResponse
    {
	    int Id { get; set; }
	    string Firstname { get; set; }
	    string Surname { get; set; }
    }

	public class UserDataResponse : IUserDataResponse
	{
		public int Id { get; set; }
		public string Firstname { get; set; }
		public string Surname { get; set; }
	}

}
