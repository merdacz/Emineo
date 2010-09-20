namespace Net.Daczkowski.Emineo.App
{
    using System;
    using Net.Daczkowski.Emineo.Model;
    using Net.Daczkowski.Emineo.Model.Helpers;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Criterion;
    using NHibernate.Tool.hbm2ddl;
    using NHibernate.LambdaExtensions;

    public static class UserQueries
    {
        private static Random random = new Random();

        public static void Launch()
        {
            var configuration = new Configuration()
                .Configure("NHibernate.xml");
            new SchemaExport(configuration).Drop(false, true);
            new SchemaExport(configuration).Create(false, true);
            var factory = configuration.BuildSessionFactory();

            CreateRandomUsers(factory);
            QueryUsers(factory);
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

        private static void QueryUsers(ISessionFactory factory)
        {
            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var users = session.CreateCriteria<User>()
                        .Add(Restrictions.IsNotNull("LastLogin"))
                        .Add(Restrictions.Gt("LastLogin", random.NextDate()))
                        .AddOrder<User>(x => x.LastLogin, Order.Desc)
                        .AddOrder(Order.Asc("LastLogin"))
                        .List<User>();

                    foreach (var user in users)
                    {
                        Console.WriteLine(user);
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