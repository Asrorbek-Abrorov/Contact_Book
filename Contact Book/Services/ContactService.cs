using Contact_Book.interfaces;

namespace Contact_Book.Services;

public class ContactService : IContactService
{
    private readonly List<Contact> contacts;

    public ContactService(List<Contact> contacts)
    {
        this.contacts = contacts;
    }


    public bool Add(Contact contact)
    {
        contacts.Add(contact);
        return true;
    }

    public bool Delete(int id)
    {
        contacts.RemoveAll(contact => contact.Id == id);
        return true;
    }

    public bool Update(int id, Contact contact)
    {
        Contact firstContact = contacts.FirstOrDefault(contact => contact.Id == id);
        if (firstContact != null)
        {
            firstContact.Name = contact.Name;
            firstContact.Email = contact.Email;
            firstContact.PhoneNumber = contact.PhoneNumber;
            return true;
        }
        else
        {
            return false;
        }
    }

    public Contact GetDetails(int id) => contacts.FirstOrDefault(c => c.Id == id);
    
    public List<Contact> GetAll() => contacts;
}