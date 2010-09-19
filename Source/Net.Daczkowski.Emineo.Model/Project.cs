﻿namespace Net.Daczkowski.Emineo.Model
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Net.Daczkowski.Emineo.Model.Specifications;
    using Net.Daczkowski.Emineo.Model.Visitors;

    /// <summary>
    /// Represents the single project being maintained.
    /// </summary>
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

        public virtual string Name { get; protected set; }

        public virtual User Manager { get; protected set; }

        public virtual ICollection<Task> Tasks { get; protected set; }

        public virtual ICollection<Task> AllTasks
        {
            get
            {
                var visitor = new TasksFlatteningVisitor();
                this.AcceptVisitor(visitor);
                return new List<Task>(visitor.Tasks);
            }
        }

        public virtual Progress Progress
        {
            get
            {
                var visitor = new WorkProgressVisitor();
                this.AcceptVisitor(visitor);
                return visitor.Progress;
            }
        }

        public virtual Task CreateTask(TaskSpecification specification)
        {
            Contract.Requires(specification != null);

            var task = new Task(specification, this);
            this.Tasks.Add(task);
            return task;
        }
    }
}