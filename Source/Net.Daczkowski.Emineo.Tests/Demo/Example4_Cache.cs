namespace Net.Daczkowski.Emineo.Tests.Demo
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using HibernatingRhinos.Profiler.Appender.NHibernate;
    using log4net;
    using Net.Daczkowski.Emineo.Model;
    using Net.Daczkowski.Emineo.Model.Specifications;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;
    using NUnit.Framework;

    [TestFixture]
    public class Example4_Cache
    {
        [Test]
        public void Cache()
        {
            NHibernateProfiler.Initialize();
            var configuration = new Configuration()
                .Configure("NHibernate.xml");
            new SchemaExport(configuration).Create(false, true);
            var factory = configuration.BuildSessionFactory();
            
            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    
                    transaction.Commit();
                }

                LogManager.Shutdown();
            }
        }
    }
}
