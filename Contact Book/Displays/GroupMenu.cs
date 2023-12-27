using Contact_Book.Services;
using Spectre.Console;

namespace Contact_Book.Displays;

public class GroupMenu
{
    private readonly GroupService groupService;

    public GroupMenu(GroupService groupService)
    {
        this.groupService = groupService;
    }


    public void Run()
    {
        bool keepRunning = true;
        while (keepRunning)
        {
            AnsiConsole.Clear();
            var Choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]*** Groups ***[/]?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Create", "Update", "Delete (group not contacts)", 
                        "Group Details", "Add Contact to Group",
                        "Remove Contact from group",
                        "Exit",
                    }));
            switch (Choice)
            {
                case "Exit":
                    keepRunning = false;
                    break;
                
                case "Create":
                    AnsiConsole.Clear();
                    var name = AnsiConsole.Ask<string>("Name : ").Trim();
                    var description = AnsiConsole.Ask<string>("Description : ").Trim();
                    groupService.Create(name, description);
                    break;
                
                case "Group Details":
                    AnsiConsole.Clear();
                    var id = AnsiConsole.Ask<int>("Id : ");
                    var group = groupService.GetById(id);
                    if (group != null)
                    {
                        AnsiConsole.Clear();
                        AnsiConsole.WriteLine("********************************");
                        AnsiConsole.WriteLine($"Id : {group.Id}\nName : {group.Name}\nDescription : {group.Description}");
                        foreach (var Contact in group.contacts)
                        {
                            AnsiConsole.WriteLine("********************************");
                            AnsiConsole.WriteLine($"{Contact.Name}");
                            AnsiConsole.WriteLine($"{Contact.PhoneNumber}");
                            AnsiConsole.WriteLine($"{Contact.Email}");
                            AnsiConsole.WriteLine($"{Contact.Address}");
                        }
                    }
                    else
                    {
                        AnsiConsole.Clear();
                        AnsiConsole.WriteLine("Group not found");
                    }
                    
                    break;
                
                case "Update":
                    AnsiConsole.Clear();
                    AnsiConsole.WriteLine("*** Update ***");
                    
                    id = AnsiConsole.Ask<int>("Id : ");

                    group = groupService.GetById(id);

                    if (group != null)
                    {
                        var upd = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[green]*** Details ***[/]?")
                                .PageSize(10)
                                .AddChoices(new[]
                                {
                                    "Name", "Description",
                                }));
                        switch (upd)
                        {
                            case "Name":
                                name = AnsiConsole.Ask<string>("New Name : ").Trim();
                                group.Name = name;
                                break;
                            
                            case "Description":
                                description = AnsiConsole.Ask<string>("Description : ").Trim();
                                group.Description = description;
                                break;
                        }
                    }
                    else
                    {
                        AnsiConsole.WriteLine("Group not found!");
                    }
                    
                    break;
                
                case "Delete (group not contacts)":
                    AnsiConsole.Clear(); 
                    id = AnsiConsole.Ask<int>("Id : ");
                    var del = groupService.Delete(id);
                    if (del)
                    {
                        AnsiConsole.WriteLine("Group deleted successfully!"); 
                    }
                    else
                    {
                        AnsiConsole.WriteLine("Group not found!"); 
                    }
                    
                    break;
                
                case "Add Contact to Group":
                    id = AnsiConsole.Ask<int>("Id (contact): ");
                    var idGroup = AnsiConsole.Ask<int>("Id (Group): ");
                    
                    var check = groupService.AddContactToGroup(idGroup, id);
                    
                    if (check)
                    {
                        AnsiConsole.WriteLine("Contact added successfully!");
                        AnsiConsole.WriteLine();
                    }
                    else
                    {
                        AnsiConsole.WriteLine("Group or Contact not found!");
                    }
                    
                    break;
                
                case "Remove Contact from group":
                    id = AnsiConsole.Ask<int>("Id (contact): "); 
                    idGroup = AnsiConsole.Ask<int>("Id (Group): ");
                    
                    check = groupService.RemoveContactFromGroup(idGroup, id);
                    
                    if (check)
                    {
                        AnsiConsole.WriteLine("Contact removed successfully!");
                        AnsiConsole.WriteLine();
                    }
                    else
                    {
                        AnsiConsole.WriteLine("Group or Contact not found!");
                    }
                    break;
            }
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine("Enter to continue...");
            Console.ReadKey();

            
        }
    }
}