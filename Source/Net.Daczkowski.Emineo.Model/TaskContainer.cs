namespace Net.Daczkowski.Emineo.Model
{
    using System.Collections.Generic;
    using Net.Daczkowski.Emineo.Model.Visitors;

    public abstract class TaskContainer : Entity
    {
        protected TaskContainer()
        {
            this.Tasks = new List<Task>();
            this.RegisterChildCollection(() => this.Tasks);
        }

        public virtual ICollection<Task> Tasks { get; protected set; }

        public virtual Progress Progress
        {
            get
            {
                var visitor = new WorkProgressVisitor();
                this.AcceptVisitor(visitor);
                return visitor.Progress;
            }
        }
    }
}
