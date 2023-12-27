using Contact_Book.Services;
using Spectre.Console;
using System.Collections.Generic;

namespace Contact_Book.Displays
{
    public class MainMenu
    {
        private readonly List<Group> groups;
        private readonly GroupMenu groupMenu;
        private readonly List<Contact> contacts;
        private readonly ContactMenu contactMenu;
        private readonly GroupService groupService;
        private readonly ContactService contactService;

        public MainMenu()
        {
            groups = new List<Group>();
            contacts = new List<Contact>();
            contactService = new ContactService(contacts);
            groupService = new GroupService(contacts, groups);
            groupMenu = new GroupMenu(groupService);
            contactMenu = new ContactMenu(contactService);
        }

        public void Run()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                AnsiConsole.Clear();
                var Choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]*** Contact Book ***[/]?")
                        .PageSize(10)
                        .AddChoices(new[] {
                            "Contact", "Groups", "Exit"
                        }));

                switch (Choice)
                {
                    case "Contact":
                        contactMenu.Run();
                        break;

                    case "Groups":
                        groupMenu.Run();
                        break;

                    case "Exit":
                        keepRunning = false;
                        break;
                }
            }
            AnsiConsole.WriteLine("Goodbye!");
        }
    }
}