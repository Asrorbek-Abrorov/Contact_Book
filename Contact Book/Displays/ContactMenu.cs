using System.Text.RegularExpressions;
using Contact_Book.Services;
using Spectre.Console;

namespace Contact_Book.Displays;

public class ContactMenu
{
    
    private readonly ContactService contactService;
    
    public ContactMenu(ContactService contactService)
    {
        this.contactService = contactService;
    }
    
    
    public void Run()
    {
        bool keepRunning = true;
        while (keepRunning)
        {
            AnsiConsole.Clear();
            var Choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]*** Contacts ***[/]?")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "Create", "Update", "Delete", "Details",
                        "All contacts", "Exit",
                    }));

            switch (Choice)
            {
                case "Create":
                    AnsiConsole.WriteLine("*** Creating a new contact ***");
                    AnsiConsole.WriteLine();
                    var name = AnsiConsole.Ask<string>("Name: ").Trim();
                    var phone = AskPhoneNumber("Phone Number: ").Trim();
                    var email = AskEmailAddress("Email Address: ").Trim();
                    var address = AnsiConsole.Ask<string>("Address: ").Trim();
                    var contact = new Contact(name, phone, email, address);
                    contactService.Add(contact);
                    break;
                
                case "Update":
                    int id = AnsiConsole.Ask<int>("Id : ");
                    var check = contactService.GetDetails(id);
                    if (check != null)
                    {
                        var isAll = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[green]*** What do you want to update ? (All or 1 detail) ***[/]?")
                                .PageSize(10)
                                .AddChoices(new[]
                                {
                                    "All", "1 detail",
                                }));
                        
                        switch (isAll)
                        {
                            case "All":
                                AnsiConsole.Clear();
                                name = AnsiConsole.Ask<string>("Name: ").Trim();
                                phone = AskPhoneNumber("Phone Number: ").Trim();
                                email = AskEmailAddress("Email Address: ").Trim();
                                address = AnsiConsole.Ask<string>("Address: ").Trim();
                                contact = new Contact(name, phone, email, address);
                                
                                contactService.Update(id, contact);
                                break;
                            
                            case "1 detail":
                                AnsiConsole.Clear();
                                var updateProp = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("[green]*** What do you want to update ? ***[/]?")
                                        .PageSize(10)
                                        .AddChoices(new[]
                                        {
                                            "Name", "Phone", "Email", "Address"
                                        }));
                                switch (updateProp)
                                {
                                    case "Name":
                                        var updContact = contactService.GetDetails(id);
                                        AnsiConsole.Clear();
                                        name = AnsiConsole.Ask<string>("Name: ").Trim();
                                        updContact.Name = name;
                                        break;
                                    
                                    case "Phone":
                                        updContact = contactService.GetDetails(id);
                                        AnsiConsole.Clear();
                                        phone = AskPhoneNumber("Phone Number: ").Trim();
                                        updContact.PhoneNumber = phone;
                                        break;
                                    
                                    case "Email":
                                        updContact = contactService.GetDetails(id);
                                        AnsiConsole.Clear();
                                        email = AskEmailAddress("Email Address: ").Trim();
                                        updContact.Email = email;
                                        break;
                                    
                                    case "Address":
                                        updContact = contactService.GetDetails(id);
                                        AnsiConsole.Clear();
                                        address = AnsiConsole.Ask<string>("Address : ").Trim();
                                        updContact.Address = address;
                                        break;
                                }
                                
                                AnsiConsole.WriteLine("Details Updated Successfully");
                                break;
                        }
                    }
                    else
                    {
                        AnsiConsole.Clear();
                        AnsiConsole.WriteLine("Contact not found");
                    }

                    break;
                
                case "Delete":
                    AnsiConsole.Clear();
                    id = AnsiConsole.Ask<int>("Enter ID to delete : ");
                    var deleteContact = contactService.GetDetails(id);
                    if (deleteContact != null)
                    {
                        contactService.Delete(id);
                    }
                    else
                    {
                        AnsiConsole.Clear();
                        AnsiConsole.WriteLine("Contact not found");
                    }
                    break;
                
                case "Details":
                    AnsiConsole.Clear();
                    id = AnsiConsole.Ask<int>("Enter ID to get contact details : ");
                    var contactDetails = contactService.GetDetails(id);
                    if (contactDetails != null)
                    {
                        AnsiConsole.WriteLine("***********************************");
                        AnsiConsole.WriteLine($"ID:{contactDetails.Id}\nName:{contactDetails.Name}\nPhone: {contactDetails.PhoneNumber}\nEmail: {contactDetails.Email}\nAddress: {contactDetails.Address}");
                        AnsiConsole.WriteLine("***********************************");
                        AnsiConsole.WriteLine();   
                    }
                    else
                    {
                        AnsiConsole.Clear();
                        AnsiConsole.WriteLine("Contact not found");
                    }
                    break;
                
                case "All contacts":
                    var prints = contactService.GetAll();
                    foreach (var variable in prints)
                    {
                        AnsiConsole.WriteLine("***********************************");
                        AnsiConsole.WriteLine($"ID:{variable.Id}\nName:{variable.Name}\nPhone: {variable.PhoneNumber}\nEmail: {variable.Email}\nAddress: {variable.Address}");
                        AnsiConsole.WriteLine();
                    }
                    break;
                
                case "Exit":
                    keepRunning = false;
                    break;
            }
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
    
    private string AskPhoneNumber(string prompt)
    {
        while (true)
        {
            var phoneNumber = AnsiConsole.Ask<string>(prompt).Trim();
            if (ValidatePhoneNumber(phoneNumber))
            {
                return phoneNumber;
            }
            else
            {
                AnsiConsole.WriteLine("Invalid phone number format. Please enter a number in the format +998*********");
                AnsiConsole.WriteLine();
            }
        }
    }
    
    private string AskEmailAddress(string prompt)
    {
        while (true)
        {
            var emailAddress = AnsiConsole.Ask<string>(prompt).Trim();
            if (ValidateEmailAddress(emailAddress))
            {
                return emailAddress;
            }
            else
            {
                AnsiConsole.WriteLine("Invalid email address format. Please enter a valid email address.");
                AnsiConsole.WriteLine();
            }
        }
    }
    
    private bool ValidatePhoneNumber(string phoneNumber)
    {
        string pattern = @"^\+998\d{9}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }
    
    private bool ValidateEmailAddress(string emailAddress)
    {
        string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
        return Regex.IsMatch(emailAddress, pattern);
    }
}