using Contact_Book.interfaces;

namespace Contact_Book.Services;

public class GroupService : IGroupService
{
    private readonly List<Group> groups;
    private readonly List<Contact> contacts;

    public GroupService(List<Contact> contacts, List<Group> groups)
    {
        this.groups = groups;
        this.contacts = contacts;
    }

    public bool Create(string name, string description)
    {
        var group = new Group(name, description);
        groups.Add(group);
        return true;
    }


    public bool Update(int id, string name, string description)
    {
        var group = groups.Find(g => g.Id == id);

        if (group == null) return false;

        group.Name = name;
        group.Description = description;
        return true;
    }

    public List<Group> GetAll() => groups;

    public Group GetById(int id) => groups.Find(g => g.Id == id);

    public bool Delete(int id)
    {
        var group = groups.Find(x => x.Id == id);
        if (group == null) return false;
        groups.Remove(group);
        return true;
    }

    public bool AddContactToGroup(int groupId, int contactId)
    {
        Group group = groups.Find(g => g.Id == groupId);
        Contact contact = contacts.Find(c => c.Id == contactId);
        if (group == null || contact == null) return false;
        group.contacts.Add(contact);
        return true;
    }

    public bool RemoveContactFromGroup(int groupId, int contactId)
    {
        Group group = groups.Find(g => g.Id == groupId);
        Contact contact = contacts.Find(c => c.Id == contactId);

        if (group == null || contact == null) return false;
        group.contacts.Remove(contact);
        return true;
    }

    public List<Contact> GetContacts() => contacts;

}