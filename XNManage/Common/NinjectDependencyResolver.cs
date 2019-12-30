using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Syntax;
using XNManage.Repository;

namespace XNManage.Common
{
    /// <summary>
    /// NinjectControllerFactory只是解决了控制器的DI，但还可以服用到其他的地方。
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        public IBindingToSyntax<T> Bind<T>()
        {
            return _kernel.Bind<T>();
        }

        public IKernel Kernel
        {
            get { return _kernel; }
        }

        private void AddBindings()
        {
            _kernel.Bind<IUsersRepository>().To<UsersRepository>();//UserRepository
            _kernel.Bind<IProductRepository>().To<ProductRepository>();
            //绑定
           
        }
    }
}