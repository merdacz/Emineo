namespace Net.Daczkowski.Emineo.Model.Visitors
{
    using System;

    /// <summary>
    /// Indicates that entity has the registered work assigned to itself.
    /// </summary>
    public interface IHaveRegisteredWork
    {
        TimeSpan Amount { get; }
    }
}