namespace Net.Daczkowski.Emineo.App
{
    using System;
    using HibernatingRhinos.Profiler.Appender.NHibernate;
    using log4net;

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
            Cache.Launch();
            LogManager.Shutdown();
        }
    }
}
