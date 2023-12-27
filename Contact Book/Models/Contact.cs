namespace Contact_Book;

public class Contact
{
    private static int id = 0;

    public Contact(string name, string phoneNumber, string email, string address)
    {
        Id = ++id;
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}