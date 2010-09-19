namespace Net.Daczkowski.Emineo.Model
{
    using System.Collections.Generic;

    public abstract class TaskContainer : Entity
    {
        protected TaskContainer()
        {
            this.Tasks = new List<Task>();
            this.RegisterChildCollection(() => this.Tasks);
        }

        public virtual ICollection<Task> Tasks { get; protected set; }
    }
}
