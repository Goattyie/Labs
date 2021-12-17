using _6lab;


var middleware = new HandleDataMiddleware(new SearchClientDatabaseMiddleware(new Middleware()));


while (true)
{
    Console.Clear();
    Console.WriteLine("В качестве авторизации разрешены клиенты с именем длиннее 3 и короче 14.");
    Console.WriteLine("Пароль может содержать только цифры в количестве от 5 до 10.");

    Console.Write("Введите имя клиента: ");
    var name = Console.ReadLine() ?? string.Empty;
    Console.Write("Введите пароль: ");
    var password = Console.ReadLine() ?? string.Empty;
    var client = new Client(name, password);

    try { middleware.Execute(client); }catch (Exception ex){ Console.WriteLine(ex.Message); }
    Console.ReadKey();
}
