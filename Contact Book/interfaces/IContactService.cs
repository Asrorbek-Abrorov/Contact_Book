namespace Contact_Book.interfaces;

public interface IContactService
{
    public bool Add(Contact contact);
    public bool Delete(int id);
    public bool Update(int id, Contact contact);
    public Contact GetDetails(int id);
    
}