namespace Net.Daczkowski.Emineo.Model.Visitors
{
    using System;

    /// <summary>
    /// Indicates that entity has the estimate assigned with itself. 
    /// </summary>
    public interface IHaveEstimate
    {
        TimeSpan Estimate { get; }
    }
}