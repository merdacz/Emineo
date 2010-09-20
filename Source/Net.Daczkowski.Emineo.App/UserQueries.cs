namespace Net.Daczkowski.Emineo.App
{
    using System;
    using Net.Daczkowski.Emineo.Model;
    using Net.Daczkowski.Emineo.Model.Helpers;
    using NHibernate.Cfg;
    using NHibernate.Criterion;
    using NHibernate.Tool.hbm2ddl;
    

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

            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    for (int i=0; i < 10; i++)
                    {
                        var user = new User("user #" + i);
                        DateTimeNow.Set(() => random.NextDate());
                        random.NextCall(() => user.Authenticate("koteczek"));
                        
                        session.Save(user);
                    }

                    transaction.Commit();
                }
            }

            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var users = session.CreateCriteria<User>()
                        .Add(Restrictions.IsNotNull("LastLogin"))
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
    }
}