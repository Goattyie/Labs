using KursovayaMVC.Models;

namespace KursovayaMVC.Middlewares
{
    public class CheckServiceDataMiddleware : AbstractMiddleware<Service>
    {
        public CheckServiceDataMiddleware (AbstractMiddleware<Service> next) : base(next) { }
        public override void Execute(Service service)
        {
            if (service.Id != 0)
                throw new Exception();

            else if (service.Difficult <= 0)
                throw new Exception();

            _next.Execute(service);
        } 
    }
}
