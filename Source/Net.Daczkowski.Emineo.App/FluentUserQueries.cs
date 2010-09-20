namespace Net.Daczkowski.Emineo.App
{
    using System;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using LinFu.DynamicProxy;
    using Net.Daczkowski.Emineo.Model;
    using Net.Daczkowski.Emineo.Model.Helpers;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Criterion;
    using NHibernate.Tool.hbm2ddl;
    using NHibernate.LambdaExtensions;

    public static class FluentUserQueries
    {
        private static Random random = new Random();

        public static void Launch()
        {
            var factory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(connectionString => connectionString
                        .Database("Emineo")
                        .Server(@"localhost\LOCAL2008")
                        .TrustedConnection()))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                .ExposeConfiguration(configuration => configuration.SetProperty("proxyfactory.factory_class", "NHibernate.ByteCode.LinFu.ProxyFactoryFactory, NHibernate.ByteCode.LinFu"))
                .ExposeConfiguration(configuration => new SchemaExport(configuration).Drop(false, true))
                .ExposeConfiguration(configuration => new SchemaExport(configuration).Create(false, true))
                .BuildSessionFactory();
            
            CreateRandomUsers(factory);
            QueryUsersFluenty(factory);
        }

        private static void CreateRandomUsers(ISessionFactory factory)
        {
            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    for (int i=0; i < 100; i++)
                    {
                        var user = new User("user #" + i);
                        DateTimeNow.Set(() => random.NextDate());
                        random.NextCall(() => user.Authenticate("koteczek"));
                        
                        session.Save(user);
                    }

                    transaction.Commit();
                }
            }
        }

        private static void QueryUsersFluenty(ISessionFactory factory)
        {
            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var users = session.CreateCriteria<User>()
                        .Add(SqlExpression.IsNotNull<User>(u => u.LastLogin))
                        .Add<User>(u => u.LastLogin > random.NextDate())
                        .AddOrder<User>(x => x.LastLogin, Order.Desc)
                        .List<User>();

                    foreach (var user in users)
                    {
                        Console.WriteLine(user);
                    }

                    transaction.Commit();
                }
            }
        }
    }
}