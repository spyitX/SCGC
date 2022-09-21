using System.ComponentModel.DataAnnotations;

public class Employee
{
	public int Id { get; set; }
	[Required]
	public string FirstName { get; set; } // this field is use to keep product name
	[Required]
	public string LastName { get; set; }
	[Required]
	public string EmailId { get; set; }

	
	public void SetInitial(int Id,string FirstName,string Lastname,string EmailId)
	{

		//this.Id = Id;
		//this.FirstName = FirstName;
		//this.LastName = Lastname;
		//this.EmailId = EmailId;

		/*
		this.Id = 1;
		this.FirstName = "Supinyo";
		this.LastName = "Chaipanya";
		this.EmailId = "supinyoc@gmail.com";
		*/
	}
	
}