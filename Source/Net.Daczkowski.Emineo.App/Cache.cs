namespace Net.Daczkowski.Emineo.App
{
    using System;
    using Net.Daczkowski.Emineo.Model;
    using Net.Daczkowski.Emineo.Model.Helpers;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Criterion;
    using NHibernate.Tool.hbm2ddl;

    public static class Cache
    {
        private static Random random = new Random();

        public static void Launch()
        {
            var configuration = new Configuration()
                .Configure("NHibernate.xml");
            new SchemaExport(configuration).Create(false, true);
            var factory = configuration.BuildSessionFactory();
            
            CreateUsers(factory);
            QueryUsers(factory);
            CreateSingleUser(factory);
            QueryUsers(factory);
        }

        private static void CreateUsers(ISessionFactory factory)
        {
            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    for (int i = 0; i < 10; i++)
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
                    var users = session.CreateQuery("from User")
                        .SetCacheable(true)
                        .Enumerable();

                    foreach (var user in users)
                    {
                        Console.WriteLine(user);
                    }

                    transaction.Commit();
                }
            }
        }

        private static void CreateSingleUser(ISessionFactory factory)
        {
            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var user = session.Get<User>(1);
                    DateTimeNow.Set(() => new DateTime(2008, 1, 1));
                    user.Authenticate("koteczek");

                    session.Save(user);
                    transaction.Commit();
                }
            }
        }
    }
}
