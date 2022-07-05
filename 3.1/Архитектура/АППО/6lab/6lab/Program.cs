using _6lab;


var middleware = new HandleDataMiddleware(new SearchClientDatabaseMiddleware(new Middleware()));


while (true)
{
    Menu();
    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.D1:
            AddUser();
            break;
        case ConsoleKey.D2:
            Authorization();
            break;
        case ConsoleKey.D3:
            break;
    }
}

void Menu()
{
    Console.Clear();
    Console.WriteLine("1. Добавить пользователя в БД");
    Console.WriteLine("2. Список пользователей");
    Console.WriteLine("3. Пройти авторизацию");
}

void AddUser()
{

}

void Authorization()
{
    Console.Clear();
    Console.WriteLine("В качестве авторизации разрешены клиенты с именем длиннее 3 и короче 14.");
    Console.WriteLine("Пароль может содержать только цифры в количестве от 5 до 10.");

    Console.Write("Введите имя клиента: ");
    var name = Console.ReadLine() ?? string.Empty;
    Console.Write("Введите пароль: ");
    var password = Console.ReadLine() ?? string.Empty;
    var client = new Client(name, password);

    try { middleware.Execute(client); } catch (Exception ex) { Console.WriteLine(ex.Message); }
    Console.ReadKey();
}
