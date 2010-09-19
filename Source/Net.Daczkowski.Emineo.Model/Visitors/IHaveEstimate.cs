namespace Net.Daczkowski.Emineo.Model.Visitors
{
    using System;

    public interface IHaveEstimate
    {
        TimeSpan Estimate { get; }
    }
}