using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private  IStorageRepository _storageRepository;
        private  IOrderRepository _orderRepository;
        private IManufacturerRepository _manufacturerRepository;
        private IProductRepository _productRepository;
        private IBuyerRepository _buyerRepository;
       
        
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IOrderRepository Order
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_repositoryContext);
                return _orderRepository;
            }
        }

        public IProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_repositoryContext);
                return _productRepository;
            }
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }


        public IStorageRepository Storage
        {
            get
            {
                if (_storageRepository == null)
                    _storageRepository = new StorageRepository(_repositoryContext);
                return _storageRepository;
            }
        }

        public IManufacturerRepository Manufacturer
        {
            get
            {
                if (_manufacturerRepository == null)
                    _manufacturerRepository = new ManufacturerRepository(_repositoryContext);
                return _manufacturerRepository;
            }
        }
        public IBuyerRepository Buyer
        {
            get
            {
                if (_buyerRepository == null)
                    _buyerRepository = new BuyerRepository1(_repositoryContext);
                return _buyerRepository;
            }
        }
        public void Save() => _repositoryContext.SaveChanges();
    }
}
