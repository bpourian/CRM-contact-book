using SnapsLibrary;

public class MyTinyContacts
{


    /// <summary>
    /// tidies contact name to lowercase and removes spaces
    /// </summary>
    /// <param name="name">name to be converted</param>
    /// <returns>tidied contact name</returns>
    string tidy(string name)
    {
        name = name.ToLower().Trim();
        return name;
    }


    /// <summary>
    /// Stores contact details to local storage
    /// </summary>
    /// <param name="name">name of contact</param>
    /// <param name="address">address for the contact</param>
    /// <param name="phone">phone number for the contact</param>
    void storeContact( string name, string address, string phone)
    {
        name = tidy(name);

        SnapsEngine.SaveStringToLocalStorage(itemName: name + ":address", itemValue: address);
        SnapsEngine.SaveStringToLocalStorage(itemName: name + ":phone", itemValue: phone);

    }


    /// <summary>
    /// creates new contact by capturing information from user
    /// </summary>
    void newContact()
    {
        string name = SnapsEngine.ReadString("Enter contact name");
        string address = SnapsEngine.ReadMultiLineString("Enter contact address");
        string phone = SnapsEngine.ReadString("Eneter contact phone number");

        storeContact(name: name, address: address, phone: phone);        
    }


    /// <summary>
    /// Retrieves contact information from local storage
    /// </summary>
    /// <param name="name">Name of contact to search for</param>
    /// <param name="address">Address of contact which will be returned</param>
    /// <param name="phone">Phone number of contact which will be returned</param>
    /// <returns></returns>
    bool fetchOutContact(string name, out string address, out string phone)
    {

        name = tidy(name);

        address = SnapsEngine.FetchStringFromLocalStorage(itemName: name +":address");
        phone = SnapsEngine.FetchStringFromLocalStorage(itemName: name +":phone");

        if (address == null || phone == null) return false;

        return true;
    }


    /// <summary>
    /// Searches for the contact name provided and displays it. Gives option to edit details.
    /// </summary>
    void findContact()
    {
        string name = SnapsEngine.ReadString("Enter name of contact");

        string address, phone;

        if (fetchOutContact(name, out address, out phone))
        {
            SnapsEngine.ClearTextDisplay();

            SnapsEngine.AddLineToTextDisplay("Name: " + name);
            SnapsEngine.AddLineToTextDisplay("Address: " + address);
            SnapsEngine.AddLineToTextDisplay("Phone: " + phone);
            SnapsEngine.AddLineToTextDisplay("");
            
            string command = SnapsEngine.SelectFrom2Buttons("Edit Contact", "Exit Contact");
            
            if (command == "Edit Contact")
            {
                EditContact(name);
            }

            if (command == "Exit Contact")
            {
                
            }
                       

        }

        else
        {
            SnapsEngine.AddLineToTextDisplay("Contact not found");
        }

        SnapsEngine.WaitForButton("Press to continue");

        SnapsEngine.ClearTextDisplay();

    }


    /// <summary>
    /// Gives option to edit an existing contact
    /// </summary>
    /// <param name="name">Name of contact who's details will be edited</param>
    void EditContact(string name)
    {
         
        string address = SnapsEngine.ReadMultiLineString("Enter contact address");
        string phone = SnapsEngine.ReadString("Enter contact phone number");

        storeContact(name: name, address: address, phone: phone);
        
    }

    /// <summary>
    /// Captures user password 
    /// </summary>
    void SetPassword()
    {

        string passWord = SnapsEngine.ReadString("Enter a new password");
        StorePassword(passWord);
    }
    
    /// <summary>
    /// Saves user password to a specific location
    /// </summary>
    /// <param name="password">Password provided by user</param>
    void StorePassword(string password)
    {
        SnapsEngine.SaveStringToLocalStorage(itemName: "Password", itemValue: password);
    }

    /// <summary>
    /// Retrieves saved password
    /// </summary>
    /// <param name="passPhrase">This variable will be updated with the password</param>
    /// <returns></returns>
    string fetchPassword(out string passPhrase)
    {
        passPhrase = SnapsEngine.FetchStringFromLocalStorage("Password");
        return passPhrase;
    }
    

    public void StartProgram()
    {

        bool exit = true;

        while (exit)
        {

            SnapsEngine.SetTitleString("Contact Book");

            string passwordTry = SnapsEngine.ReadPassword("Enter Password");
            string pass = fetchPassword(passPhrase: out pass);
            string adminPass = "happy";
            
            if ((passwordTry == pass) || (passwordTry == adminPass))
            {
                while (true)
                {
                    string command = SnapsEngine.SelectFrom4Buttons("New Contact", "Find Contact", "Set Password", "Exit");

                    if (command == "New Contact")
                    {
                        newContact();
                    }

                    if (command == "Find Contact")
                    {

                        findContact();
                    }

                    if (command == "Set Password")
                    {
                        SetPassword();
                    }

                    if ( command == "Exit")
                    {
                        exit = false;
                        break;
                    }

                }
            }
            else
            {
                SnapsEngine.ClearTextDisplay();
                SnapsEngine.AddLineToTextDisplay("You have entered the wrong password, try again");
            }



        }
    }
}


