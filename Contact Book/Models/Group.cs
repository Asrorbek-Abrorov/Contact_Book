namespace Contact_Book;

public class Group
{
    private static int id = 0;

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Contact> contacts;

    public Group(string name, string description)
    {
        Id = ++id;
        Name = name;
        Description = description;
        contacts = new List<Contact>();

    }
}