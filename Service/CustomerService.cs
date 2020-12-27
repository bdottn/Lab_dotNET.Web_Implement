using EntityOperation.Model;
using EntityOperation.Protocol;
using Service.Model;
using Service.Protocol;

namespace Service
{
    sealed class CustomerService : ICustomerService
    {
        private readonly ISQLRepository<Customer> repository;

        public CustomerService(ISQLRepository<Customer> repository)
        {
            this.repository = repository;
        }

        public ServiceResult<Customer> Create(Customer customer)
        {
            this.repository.Create(customer);

            return
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = customer,
                };
        }
    }
}