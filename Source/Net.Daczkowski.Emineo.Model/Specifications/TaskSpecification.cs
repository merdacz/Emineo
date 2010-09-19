namespace Net.Daczkowski.Emineo.Model.Specifications
{
    using System;

    /// <summary>
    /// Specification for <see cref="Task"/>. 
    /// </summary>
    public class TaskSpecification
    {
        public string Summary { get; set; }

        public string Description { get; set; }

        public TimeSpan Estimate { get; set; }
    }
}