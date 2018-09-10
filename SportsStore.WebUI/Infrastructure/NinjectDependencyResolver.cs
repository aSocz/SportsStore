using Ninject;
using Ninject.Web.Common;
using SportsStore.Business.Services;
using SportsStore.Business.Validation;
using SportsStore.Business.Validation.Validators;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.Infrastructure;
using SportsStore.Infrastructure.Repositories;
using SportsStore.Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<SportsStoreContext>().ToSelf().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            kernel.Bind<IProductService>().To<ProductService>().InRequestScope();
            kernel.Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            kernel.Bind<IOrderService>().To<OrderService>().InRequestScope();
            kernel.Bind<IEmailSender>().To<EmailSender>().InRequestScope();
            kernel.Bind(typeof(IValidator<Category>)).To(typeof(CategoryValidator)).InRequestScope();
            kernel.Bind(typeof(IValidator<Product>)).To(typeof(ProductValidator)).InRequestScope();
            kernel.Bind<IOwinContextProvider>().To<OwinContextProvider>().InRequestScope();
        }
    }
}