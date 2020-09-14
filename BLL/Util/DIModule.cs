using Autofac;
using IsraelitProTestTask.BLL.DTO;
using IsraelitProTestTask.BLL.Interfaces;
using IsraelitProTestTask.BLL.Services;
using IsraelitProTestTask.DAL.EnFr;
using IsraelitProTestTask.DAL.Interface;
using IsraelitProTestTask.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IsraelitProTestTask.BLL.Util
{
    public class DIModule : Module
    {
        private string connectionString;
        public DIModule(string connection)
        {
            connectionString = connection;
        }
        protected override void Load(ContainerBuilder builder)
        {
            var set = new DbContextOptionsBuilder<BooksContext>().UseSqlite(connectionString).Options;
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>().WithParameter("options", set);
            builder.RegisterType<BookService>().As<IDataService<BookDTO>>();
            builder.RegisterType<AutorService>().As<IDataService<AutorDTO>>();
        }
    }
}
