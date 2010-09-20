namespace Net.Daczkowski.Emineo.Tests.UserSpecs
{
    using System;
    using System.Security;
    using Net.Daczkowski.Emineo.Model;
    using Net.Daczkowski.Emineo.Model.Helpers;
    using NUnit.Framework;

    [TestFixture]
    public class GivenANewUser
    {
        [Test]
        public void WhenAuthenticatedWithCorrectPasswordShouldUpdateLastLoginDate()
        {
            var now = DateTime.Now;
            DateTimeNow.Set(() => now);
            
            var user = new User("user");
            user.Authenticate("koteczek");

            Assert.IsTrue(user.IsAuthenticated);
            Assert.AreEqual(now, user.LastLogin);
        }

        [Test]
        [ExpectedException(typeof(SecurityException))]
        public void WhenAuthenticatedWithIncorrectPasswordShouldReportError()
        {
            var user = new User("user");
            user.Authenticate("boo");
        }
    }
}
