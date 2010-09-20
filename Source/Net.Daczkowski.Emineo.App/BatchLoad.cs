namespace Net.Daczkowski.Emineo.App
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Net.Daczkowski.Emineo.Model;
    using Net.Daczkowski.Emineo.Model.Specifications;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;

    public static class BatchLoad
    {
        public static void Launch()
        {
            PersistingProjectImplicitSave();
            PersistingProjectExplicitSave();
            PersistingProjectBatched();
        }

        public static void PersistingProjectImplicitSave()
        {
            var configuration = new Configuration()
                .Configure("NHibernate.xml");
            new SchemaExport(configuration).Create(false, true);
            var factory = configuration.BuildSessionFactory();
            
            var stopwach = new Stopwatch();
            stopwach.Start();
            
            using (var session = factory.OpenSession())
            {
                session.SetBatchSize(1000);

                using (var transaction = session.BeginTransaction())
                {
                    for (int i = 0; i < 100; i++)
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
                    }

                    transaction.Commit();
                }

                stopwach.Stop();
                Console.WriteLine(stopwach.Elapsed.TotalSeconds);
            }
        }

        public static void PersistingProjectExplicitSave()
        {
            var configuration = new Configuration()
                .Configure("NHibernate.xml");
            new SchemaExport(configuration).Create(false, true);
            var factory = configuration.BuildSessionFactory();

            var stopwach = new Stopwatch();
            stopwach.Start();

            using (var session = factory.OpenSession())
            {
                session.SetBatchSize(1000);

                using (var transaction = session.BeginTransaction())
                {
                    for (int i = 0; i < 100; i++)
                    {
                        var merdacz = new User("merdacz");
                        var joe = new User("joe");
                        var mary = new User("mary");
                        session.Save(merdacz);
                        session.Save(joe);
                        session.Save(mary);

                        var project = new Project("Emineo", merdacz);
                        session.Save(project);

                        var taskSpecification = new TaskSpecification();
                        taskSpecification.Summary = "Write tests";
                        taskSpecification.Description = "Write tests using NUnit and Moq";
                        taskSpecification.Estimate = TimeSpan.FromHours(2);

                        var task = project.CreateTask(taskSpecification);
                        task.Assign(merdacz);
                        session.Save(task);
                        var subtask1 = task.CreateSubtask(taskSpecification);
                        subtask1.RegisterWork(TimeSpan.FromMinutes(30), joe, Place.Office);
                        subtask1.RegisterWork(TimeSpan.FromMinutes(15), joe, Place.Office);
                        session.Save(subtask1);

                        var subtask2 = task.CreateSubtask(taskSpecification);
                        subtask2.Assign(mary);
                        subtask2.RegisterWork(TimeSpan.FromMinutes(15), merdacz, Place.Home);
                        subtask2.RegisterWork(TimeSpan.FromMinutes(60), mary, Place.Office);
                        session.Save(subtask2);

                        var project2 = new Project("TestingAssistant", merdacz);
                        session.Save(project2);

                        var task2 = project2.CreateTask(taskSpecification);
                        session.Save(task2);
                    }

                    transaction.Commit();
                }

                stopwach.Stop();
                Console.WriteLine(stopwach.Elapsed.TotalSeconds);
            }
        }

        public static void PersistingProjectBatched()
        {
            var configuration = new Configuration()
                .Configure("NHibernate.xml");
            new SchemaExport(configuration).Create(false, true);
            var factory = configuration.BuildSessionFactory();

            var stopwach = new Stopwatch();
            stopwach.Start();
            using (var session = factory.OpenSession())
            {
                session.SetBatchSize(1000);
                using (var transaction = session.BeginTransaction())
                {
                    var merdacz = new Dictionary<int, User>();
                    var joe = new Dictionary<int, User>();
                    var mary = new Dictionary<int, User>();

                    for (int i = 0; i < 100; i++)
                    {
                        merdacz[i] = new User("merdacz");
                        joe[i] = new User("joe");
                        mary[i] = new User("mary");
                        session.Save(merdacz[i]);
                        session.Save(joe[i]);
                        session.Save(mary[i]);
                    }

                    var project = new Dictionary<int, Project>();
                    var project2 = new Dictionary<int, Project>();

                    for (int i = 0; i < 100; i++)
                    {
                        project[i] = new Project("Emineo", merdacz[i]);
                        session.Save(project[i]);
                        project2[i] = new Project("TestingAssistant", merdacz[i]);
                        session.Save(project2[i]);
                    }

                    var taskSpecification = new TaskSpecification();
                    taskSpecification.Summary = "Write tests";
                    taskSpecification.Description = "Write tests using NUnit and Moq";
                    taskSpecification.Estimate = TimeSpan.FromHours(2);

                    var task = new Dictionary<int, Task>();
                    var task2 = new Dictionary<int, Task>();

                    for (int i = 0; i < 100; i++)
                    {
                        task[i] = project[i].CreateTask(taskSpecification);
                        session.Save(task[i]);
                        task[i].Assign(merdacz[i]);

                        task2[i] = project2[i].CreateTask(taskSpecification);
                        session.Save(task2[i]);
                    }

                    var subtask1 = new Dictionary<int, Task>();
                    var subtask2 = new Dictionary<int, Task>();

                    for (int i = 0; i < 100; i++)
                    {
                        subtask1[i] = task[i].CreateSubtask(taskSpecification);
                        session.Save(subtask1[i]);

                        subtask2[i] = task[i].CreateSubtask(taskSpecification);
                        session.Save(subtask2[i]);
                        subtask2[i].Assign(mary[i]);
                    }

                    for (int i = 0; i < 100; i++)
                    {
                        subtask1[i].RegisterWork(TimeSpan.FromMinutes(30), joe[i], Place.Office);
                        subtask1[i].RegisterWork(TimeSpan.FromMinutes(15), joe[i], Place.Office);
                        subtask2[i].RegisterWork(TimeSpan.FromMinutes(15), merdacz[i], Place.Home);
                        subtask2[i].RegisterWork(TimeSpan.FromMinutes(60), mary[i], Place.Office);
                    }
                    
                    transaction.Commit();
                }
            }

            stopwach.Stop();
            Console.WriteLine(stopwach.Elapsed.TotalSeconds);
        }
    }
}
