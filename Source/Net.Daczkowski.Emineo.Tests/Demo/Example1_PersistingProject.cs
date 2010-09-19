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
                    var project = new Project("Emineo", new User("merdacz"));
                    var taskSpecification = new TaskSpecification();
                    taskSpecification.Summary = "Write tests";
                    taskSpecification.Description = "Write tests using NUnit and Moq";
                    taskSpecification.Estimate = TimeSpan.FromHours(1);
                    var task = project.CreateTask(taskSpecification);
                    task.CreateSubtask(taskSpecification);
                    task.CreateSubtask(taskSpecification);
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
                        foreach (var task in project.Tasks)
                        {
                            Console.WriteLine(task.Summary + " " + task.Subtasks.Count);
                        }
                    }
                    
                    transaction.Commit();
                }
            }
        }
    }
}
