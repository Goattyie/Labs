namespace _6lab
{
    internal record class Client(string Name, string Password);
    internal abstract class AbstractMiddleware
    {
        protected AbstractMiddleware? _next;
        protected AbstractMiddleware(AbstractMiddleware next)
        {
            _next = next; ;
        }
        protected AbstractMiddleware() { }
        public abstract void Execute(Client client);
    }

    internal class HandleDataMiddleware : AbstractMiddleware
    {
        public HandleDataMiddleware(AbstractMiddleware next) : base(next) { }
        public override void Execute(Client client)
        {
            Console.WriteLine($"Вызван слой {GetType()}");
            if (client.Name.Length < 3 || client.Name.Length > 14)
                throw new Exception("Имя пользователя указано неверно");

            if(client.Password.Length < 5 || client.Password.Length > 10)
                throw new Exception("Пароль введен неверно");

            try { int.Parse(client.Password); } catch { throw new Exception("Пароль должен состоят из цифр"); }

            Console.WriteLine("Данные введены корректно");
            Thread.Sleep(1500);
            _next?.Execute(client);
        }
    }
    internal class Middleware : AbstractMiddleware
    {
        public override void Execute(Client client)
        {
            Console.WriteLine($"Вызван слой {GetType()}");
            Thread.Sleep(1000);
            Console.WriteLine("Авторизация прошла успешно");
            Console.WriteLine("Слой цепочки обязанностей пройден");
        }
    }
    internal class SearchClientDatabaseMiddleware : AbstractMiddleware
    {
        private readonly List<Client> clients = new() { new Client("Михаил", "123321"), new Client("Сергей", "321123"), new Client("Артем", "66666") };
        public SearchClientDatabaseMiddleware(AbstractMiddleware next) : base(next) { }
        public override void Execute(Client client)
        {
            Console.WriteLine($"Вызван слой {GetType()}");
            Console.WriteLine("Ищем пользователя в базе данных..");
            Thread.Sleep(2000);
            var c = clients.FirstOrDefault(x => x.Name == client.Name);
            if (c != null)
            {
                Console.WriteLine("Пользователь найден. Попытка авторизации");
                if(c.Password == client.Password)
                    _next?.Execute(client);
                else throw new Exception("Пароль введен неверно");
            }
            else throw new Exception("Пользователя с таким именем не найдено в базе данных");
        }
    }

}
