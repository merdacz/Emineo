namespace Net.Daczkowski.Emineo.Model
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Net.Daczkowski.Emineo.Model.Specifications;
    using Net.Daczkowski.Emineo.Model.Visitors;

    public class Project : Entity
    {
        protected Project()
        {
            this.Tasks = new List<Task>();
            this.RegisterChildCollection(() => this.Tasks);
        }

        public Project(string name, User manager) : this()
        {
            this.Name = name;
            this.Manager = manager;
        }

        public string Name { get; protected set; }

        public User Manager { get; protected set; }

        public ICollection<Task> Tasks { get; protected set; }

        public IEnumerable<Task> AllTasks
        {
            get
            {
                var visitor = new TasksFlatteningVisitor();
                this.AcceptVisitor(visitor);
                return visitor.Tasks;
            }
        }

        public Progress Progress
        {
            get
            {
                var visitor = new WorkProgressVisitor();
                this.AcceptVisitor(visitor);
                return visitor.Progress;
            }
        }

        public Task CreateTask(TaskSpecification specification)
        {
            Contract.Requires(specification != null);

            var task = new Task(specification, this);
            this.Tasks.Add(task);
            return task;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + this.Name).GetHashCode();
        }
    }
}