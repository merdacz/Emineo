namespace Net.Daczkowski.Emineo.Tests.Spec
{
    using System;
    using System.Linq;
    using Net.Daczkowski.Emineo.Model;
    using Net.Daczkowski.Emineo.Model.Specifications;
    using NUnit.Framework;

    [TestFixture]
    public class GivenProjectWithATask
    {
        private Project project;
        private Task task;

        [SetUp]
        public void Initialize()
        {
            var manager = new User("merdacz");
            this.project = new Project("Emineo", manager);
            
            var initialStructureTask = new TaskSpecification();
            initialStructureTask.Summary = "Create initial project structure. ";
            initialStructureTask.Description = "Create Model and Tests projects.";
            initialStructureTask.Estimate = TimeSpan.FromHours(2);

            this.task = this.project.CreateTask(initialStructureTask);
        }

        [Test]
        public void WhenNoWorkRegisteredShouldReportProperProgress()
        {
            Assert.AreEqual(TimeSpan.FromHours(2), this.project.Progress.Estimate);
            Assert.AreEqual(TimeSpan.Zero, this.project.Progress.TimeSpent);
        }

        [Test]
        public void WhenWorkRegisteredShouldReportProperProgress()
        {
            var developer = new User("joe");
            this.task.RegisterWork(TimeSpan.FromHours(1), developer, Place.Office);
            Assert.AreEqual(TimeSpan.FromHours(2), this.project.Progress.Estimate);
            Assert.AreEqual(TimeSpan.FromHours(1), this.project.Progress.TimeSpent);
            Assert.AreEqual(50, this.project.Progress.Completeness);
        }

        [Test]
        public void ShouldReportSingleTask()
        {
            Assert.AreEqual(1, this.project.AllTasks.Count());
            Assert.AreEqual(this.task.ID, this.project.AllTasks.First().ID);
        }
    }
}
