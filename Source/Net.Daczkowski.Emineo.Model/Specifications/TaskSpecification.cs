namespace Net.Daczkowski.Emineo.Model.Specifications
{
    using System;

    public class TaskSpecification
    {
        public string Summary { get; set; }

        public string Description { get; set; }

        public TimeSpan Estimate { get; set; }
    }
}