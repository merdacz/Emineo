namespace Net.Daczkowski.Emineo.Model.Visitors
{
    using System;

    public interface IHaveRegisteredWork
    {
        TimeSpan Amount { get; }
    }
}