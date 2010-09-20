namespace Net.Daczkowski.Emineo.Tests.ProjectSpecs
{
    using System;
    using System.Linq;
    using Net.Daczkowski.Emineo.Model;
    using NUnit.Framework;

    [TestFixture]
    public class GivenAnEmptyProject
    {
        private Project project; 

        [SetUp]
        public void Initialize()
        {
            var manager = new User("merdacz");
            this.project = new Project("Emineo", manager);
        }

        [Test]
        public void ShouldReportNoProgress()
        {
            Assert.AreEqual(TimeSpan.Zero, this.project.Progress.TimeSpent);
            Assert.AreEqual(TimeSpan.Zero, this.project.Progress.Estimate);
        }

        [Test]
        public void ShouldReportNoTasks()
        {
            Assert.AreEqual(0, this.project.AllTasks.Count());
        }
    }
}
