namespace Net.Daczkowski.Emineo.Tests
{
    using System;
    using System.Linq;
    using Net.Daczkowski.Emineo.Model;
    using Net.Daczkowski.Emineo.Model.Specifications;
    using NUnit.Framework;

    [TestFixture]
    public class GivenProjectWithSubtasks
    {
        private Project project;
        private Task tests;
        private Task initial;

        [SetUp]
        public void Initialize()
        {
            var manager = new User("merdacz");
            this.project = new Project("Emineo", manager);

            var initialStructureTask = new TaskSpecification();
            initialStructureTask.Summary = "Create initial project structure. ";
            initialStructureTask.Description = "Create Model and Tests projects.";
            initialStructureTask.Estimate = TimeSpan.FromHours(2);
            
            var implementModelTask = new TaskSpecification();
            implementModelTask.Summary = "Implement model. ";
            implementModelTask.Description = "Create project/tasks classes. ";
            implementModelTask.Estimate = TimeSpan.FromHours(1);

            var testsTask = new TaskSpecification();
            testsTask.Summary = "Implement tests for domain. ";
            testsTask.Description = "Using NUnit and Moq frameworks";
            testsTask.Estimate = TimeSpan.FromHours(2);

            var mappingsTask = new TaskSpecification();
            mappingsTask.Summary = "Implement NHibernate mappings. ";
            mappingsTask.Description = "Using hbm approach. ";
            mappingsTask.Estimate = TimeSpan.FromHours(3);

            this.initial = this.project.CreateTask(initialStructureTask);
            var task = this.project.CreateTask(implementModelTask);
            this.tests = task.CreateSubtask(testsTask);
            task.CreateSubtask(mappingsTask);
        }

        [Test]
        public void ShouldReportProperEstimate()
        {
            Assert.AreEqual(TimeSpan.FromHours(8), this.project.Progress.Estimate);
        }

        [Test]
        public void ShouldReportAllTasks()
        {
            Assert.AreEqual(4, this.project.AllTasks.Count());
        }

        [Test]
        public void WhenWorkRegisteredShouldReportProperProgress()
        {
            this.initial.RegisterWork(TimeSpan.FromHours(1.5), new User("joe"), Place.Office);
            this.tests.RegisterWork(TimeSpan.FromHours(0.5), new User("mary"), Place.Home);
            Assert.AreEqual(TimeSpan.FromHours(2), this.project.Progress.TimeSpent);
            Assert.AreEqual(25, this.project.Progress.Completeness);
        }
    }
}
