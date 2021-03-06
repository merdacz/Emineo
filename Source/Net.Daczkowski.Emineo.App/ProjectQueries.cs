﻿namespace Net.Daczkowski.Emineo.App
{
    using System;
    using Net.Daczkowski.Emineo.Model;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Transform;

    public static class ProjectQueries
    {
        public static void Launch()
        {
            QueriesLazy();
            QueriesOptimized();
            QueriesPaged();
        }

        public static void QueriesLazy()
        {
            var configuration = new Configuration()
                .Configure("NHibernate.xml");
            var factory = configuration.BuildSessionFactory();

            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var projects = session.CreateCriteria<Project>()
                        .SetFetchMode("Tasks", FetchMode.Join)
                        .List<Project>();

                    foreach (var project in projects)
                    {
                        Console.WriteLine(project);

                        foreach (var task in project.Tasks)
                        {
                            Console.WriteLine(task);
                            foreach (var subtask in task.Subtasks)
                            {
                                Console.WriteLine("\t" + subtask);
                            }
                        }
                    }

                    transaction.Commit();
                }
            }
        }

        public static void QueriesOptimized()
        {
            var configuration = new Configuration()
                .Configure("NHibernate.xml");
            var factory = configuration.BuildSessionFactory();

            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var projects = session.CreateCriteria<Project>()
                        .SetFetchMode("Tasks", FetchMode.Join)
                        .Future<Project>();
                    session.CreateCriteria<Task>()
                        .SetFetchMode("AssignedTo", FetchMode.Join)
                        .SetFetchMode("RegisteredWork", FetchMode.Join)
                        .SetFetchMode("Tasks", FetchMode.Join)
                        .Future<Task>();
                  
                    foreach (var project in projects)
                    {
                        Console.WriteLine(project);

                        foreach (var task in project.Tasks)
                        {
                            Console.WriteLine(task);
                            foreach (var subtask in task.Subtasks)
                            {
                                Console.WriteLine("\t" + subtask);
                            }
                        }
                    }

                    transaction.Commit();
                }
            }
        }

        public static void QueriesPaged()
        {
            var configuration = new Configuration()
                .Configure("NHibernate.xml");
            var factory = configuration.BuildSessionFactory();

            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var tasks = session.CreateCriteria<Task>()
                        .SetFetchMode("AssignedTo", FetchMode.Join)
                        .SetFetchMode("Tasks", FetchMode.Join)
                        .SetFetchMode("RegisteredWork", FetchMode.Join)
                        .SetResultTransformer(Transformers.DistinctRootEntity)
                        .SetFirstResult(1)
                        .SetMaxResults(1)
                        .Future<Task>();
                    
                    foreach (var task in tasks)
                    {
                        Console.WriteLine(task);
                        foreach (var subtask in task.Subtasks)
                        {
                            Console.WriteLine("\t" + subtask);
                        }
                    }

                    transaction.Commit();
                }
            }
        }
    }
}
