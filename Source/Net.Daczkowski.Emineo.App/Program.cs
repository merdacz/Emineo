namespace Net.Daczkowski.Emineo.App
{
    using HibernatingRhinos.Profiler.Appender.NHibernate;
    using log4net;

    public class Program
    {
        public static void Main(string[] args)
        {
            NHibernateProfiler.Initialize();
            UserQueries.Launch();
            ////PersistingProject.Launch();
            ////Queries.Launch();
            ////BatchLoad.Launch();
            ////Cache.Launch();
            LogManager.Shutdown();
        }
    }
}
