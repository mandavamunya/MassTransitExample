using System.Collections.Generic;
using System.Linq;

namespace ExampleMicroservice
{
    public class Database
    {
	    private readonly List<User> _list;

	    public Database()
	    {
		    _list = new List<User>
		    {
			    new User
			    {
				    Id = 1,
				    Firstname = "Alex",
				    Surname = "Smith"
			    },

			    new User
			    {
				    Id = 2,
				    Firstname = "John",
				    Surname = "Doe"
			    },

			    new User
			    {
				    Id = 3,
				    Firstname = "Ben",
				    Surname = "Tudor"
			    },
		    };
	    }

	    public IQueryable<User> Find()
	    {
		    return _list.AsQueryable();
	    }
    }

	public interface IDatabase
	{
		IQueryable<User> Find();
	}

	public class User
	{
		public int Id { get; set; }
		public string Firstname { get; set; }
		public string Surname { get; set; }
	}
}
