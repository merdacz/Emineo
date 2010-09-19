namespace Net.Daczkowski.Emineo.Model
{
    using System.Collections.Generic;
    using Net.Tgd.NH.Model;

    public class Task : Entity
    {
        public Task()
        {
            this.RegisterChildCollection(this.Subtasks);
            this.RegisterChildCollection(this.RelatedTrackerIssues);
        }

        public string Summary { get; protected set; }

        public string Description { get; protected set; }

        public IEnumerable<Task> Subtasks { get; protected set; }

        public IEnumerable<TrackerIssue> RelatedTrackerIssues { get; protected set; }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + this.Summary).GetHashCode();
        }
    }
}