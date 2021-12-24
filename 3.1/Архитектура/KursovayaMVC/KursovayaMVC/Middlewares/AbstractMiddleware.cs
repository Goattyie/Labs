namespace KursovayaMVC.Middlewares
{
    public abstract class AbstractMiddleware<T>
    {
        protected AbstractMiddleware<T>? _next;
        public AbstractMiddleware(AbstractMiddleware<T> next) { _next = next; }
        public AbstractMiddleware() { }
        public abstract void Execute(T model);
    }
}
