namespace Contact_Book.interfaces;

public interface IGroupService
{
    bool Create(string name, string description);
    bool Delete(int id);
    bool Update(int id, string name, string description);
    List<Group> GetAll();
    Group GetById(int id);
    bool AddContactToGroup(int groupId, int contactId);
    bool RemoveContactFromGroup(int groupId, int contactId);
}