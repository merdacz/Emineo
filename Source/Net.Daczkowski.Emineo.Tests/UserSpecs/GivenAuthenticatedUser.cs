namespace Net.Daczkowski.Emineo.Tests.UserSpecs
{
    using System;
    using Net.Daczkowski.Emineo.Model;
    using Net.Daczkowski.Emineo.Model.Helpers;
    using NUnit.Framework;

    [TestFixture]
    public class GivenAuthenticatedUser
    {
        [Test]
        public void WhenAuthenticateAgainShouldNotUpdateLastLoginDate()
        {
            var now = DateTime.Now;
            DateTimeNow.Set(() => now);
            
            var user = new User("user");
            user.Authenticate("koteczek");

            var later = DateTime.Now;
            DateTimeNow.Set(() => later);
            user.Authenticate("koteczek");

            Assert.AreEqual(now, user.LastLogin);
        }
    }
}
