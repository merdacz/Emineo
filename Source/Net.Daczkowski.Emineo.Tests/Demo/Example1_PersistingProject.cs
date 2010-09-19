namespace Net.Daczkowski.Emineo.Tests.Demo
{
    using System;
    using HibernatingRhinos.Profiler.Appender.NHibernate;
    using Net.Daczkowski.Emineo.Model;
    using Net.Daczkowski.Emineo.Model.Specifications;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;
    using NUnit.Framework;

    [TestFixture]
    public class Example1_PersistingProject
    {
        [Test]
        public void PersistingProject()
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
                    var merdacz = new User("merdacz");
                    var joe = new User("joe");
                    var mary = new User("mary");
                    
                    var project = new Project("Emineo", merdacz);
                    var taskSpecification = new TaskSpecification();
                    taskSpecification.Summary = "Write tests";
                    taskSpecification.Description = "Write tests using NUnit and Moq";
                    taskSpecification.Estimate = TimeSpan.FromHours(2);

                    var task = project.CreateTask(taskSpecification);
                    task.Assign(merdacz);
                    var subtask1 = task.CreateSubtask(taskSpecification);
                    subtask1.RegisterWork(TimeSpan.FromMinutes(30), joe, Place.Office);
                    subtask1.RegisterWork(TimeSpan.FromMinutes(15), joe, Place.Office);

                    var subtask2 = task.CreateSubtask(taskSpecification);
                    subtask2.Assign(mary);
                    subtask2.RegisterWork(TimeSpan.FromMinutes(15), merdacz, Place.Home);
                    subtask2.RegisterWork(TimeSpan.FromMinutes(60), mary, Place.Office);
                    
                    session.Save(project);
                    transaction.Commit();
                }
            }

            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var projects = session.CreateCriteria<Project>().Future<Project>();
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
    }
}
