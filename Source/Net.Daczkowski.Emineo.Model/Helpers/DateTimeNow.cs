namespace Net.Daczkowski.Emineo.Model.Helpers
{
    using System;

    public static class DateTimeNow
    {
        private static readonly object syncRoot;
       
        static DateTimeNow()
        {
            syncRoot = new object();
            Reset();
        }

        private static Func<DateTime> DateTimeNowFunc { get; set; }

        public static DateTime Value
        {
            get { return DateTimeNowFunc.Invoke(); }
        }

        public static void Set(Func<DateTime> dateTimeNowFunc)
        {
            lock (syncRoot)
            {
                DateTimeNowFunc = dateTimeNowFunc;
            }
        }

        public static void Reset()
        {
            Set(() => DateTime.Now);
        }
    }
}
