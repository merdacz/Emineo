namespace Net.Daczkowski.Emineo.App
{
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;

    public static class Cache
    {
        public static void Launch()
        {
            var configuration = new Configuration()
                .Configure("NHibernate.xml");
            new SchemaExport(configuration).Create(false, true);
            var factory = configuration.BuildSessionFactory();
            
            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    //// code goes here
                    transaction.Commit();
                }
            }
        }
    }
}
