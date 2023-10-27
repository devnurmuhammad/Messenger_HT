using Messenger_HT;
using Sharprompt;

//Console.CursorLeft = Console.BufferWidth - 4;
//Console.Write("[ok]");
//Console.WriteLine("\t\tTelegram BETA");
//Console.WriteLine(DateTime.UtcNow.ToString());

var choice = Prompt.Select("", new[] { "Kirish", "Ro'yhatdan o'tish", "Barcha xabarlarni ko'rish", "Exit" });
string username, message;

if (choice == "Ro'yhatdan o'tish")
{
    Console.Clear();
    Console.Write("Username kiriting: ");
    username = Console.ReadLine();
    Console.Clear();
    string result = Service.Register(username);
    Console.WriteLine(result);
}

if (choice == "Kirish")
{

    Console.Clear();
    Console.Write("Username kiriting: ");
    username = Console.ReadLine();
    bool res = Service.Log_In(username);

    if (res)
    {
        while (true)
        {
            Console.WriteLine("Xabarni kiriting: ");
            message = Console.ReadLine();
            Console.Clear();
            Service.SendMessage(username, message);
        }
    }
    else
    {
        Console.WriteLine("Username xato kiritildi!");
    }
}

if (choice == "Barcha xabarlarni ko'rish")
{
    Service.GetAllMessages();
}