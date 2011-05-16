namespace Net.Daczkowski.Emineo.App
{
    using System;
    using HibernatingRhinos.Profiler.Appender.NHibernate;
    using log4net;
    using Net.Daczkowski.Emineo.Model;
    using Net.Daczkowski.Emineo.Model.Visitors;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.SqlCommand;
    using NHibernate.LambdaExtensions;

    public class Program
    {
        public static void Main(string[] args)
        {
            NHibernateProfiler.Initialize();
            ////UserQueries.Launch();
            ////FluentUserQueries.Launch();
            ////PersistingProject.Launch();
            ////ProjectQueries.Launch();
            ////BatchLoad.Launch();
            ////Cache.Launch();
            
            var configuration = new Configuration()
                .Configure("NHibernate.xml");
            var factory = configuration.BuildSessionFactory();

            //using (var session = factory.OpenSession())
            //{
            //    using (var transaction = session.BeginTransaction())
            //    {
            //        var merdacz = new User("merdacz");
            //        var admin = new User("admin");
            //        session.Save(merdacz);
            //        session.Save(admin);
            //        transaction.Commit(); 
            //    }
            //}

            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    ////var user = session.CreateCriteria<User>().UniqueResult();
                    Task alias;
                    var projects = session.CreateCriteria<Project>()
                        .SetFetchMode<Project>(x => x.Tasks, FetchMode.Join)
                        .List<Project>();

////                    var projects =
////                        session.CreateQuery(
////"from Project as p inner join fetch p.Tasks as t inner join fetch t.RegisteredWork inner join fetch t.Tasks as tt inner join fetch tt.RegisteredWork")
////                            .List<Project>();


                    foreach (var project in projects)
                    { 
                        var workProgressVisitor = new WorkProgressVisitor();
                        project.AcceptVisitor(workProgressVisitor);
                    }

                    transaction.Commit();
                }
            }

            LogManager.Shutdown();
            Console.ReadLine();
        }
    }
}
