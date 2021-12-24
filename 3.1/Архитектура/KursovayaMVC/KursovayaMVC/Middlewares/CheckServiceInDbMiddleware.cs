using KursovayaMVC.Models;
using KursovayaMVC.Repository;

namespace KursovayaMVC.Middlewares
{
    public class CheckServiceInDbMiddleware : AbstractMiddleware<Service>
    {
        private readonly ServiceRepository _serviceRepository;

        public CheckServiceInDbMiddleware(ServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public override void Execute(Service model)
        {
            var item = _serviceRepository.FindByName(model.Name);
            if (item != null)
                throw new Exception();
        }
    }
}
