namespace Net.Daczkowski.Emineo.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Net.Daczkowski.Emineo.Model.Specifications;
    using Net.Daczkowski.Emineo.Model.Visitors;

    /// <summary>
    /// Represents task handled by a developer.
    /// </summary>
    public class Task : Entity, IHaveEstimate
    {
        public Task()
        {
            this.Subtasks = new List<Task>();
            this.RegisteredWork = new List<RegisteredWork>();
            this.RegisterChildCollection(() => this.Subtasks);
            this.RegisterChildCollection(() => this.RegisteredWork);    
        }

        public Task(TaskSpecification specification, Project project) : this()
        {
            Contract.Requires(specification != null);

            this.Summary = specification.Summary;
            this.Description = specification.Description;
            this.Estimate = specification.Estimate;
            this.Project = project;
        }

        public virtual string Summary { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual User AssignedTo { get; protected set; }

        public virtual TimeSpan Estimate { get; protected set; }

        public virtual Task ParentTask { get; protected set; }

        public virtual Project Project { get; protected set; }

        public virtual ICollection<Task> Subtasks { get; protected set; }

        public virtual ICollection<RegisteredWork> RegisteredWork { get; protected set; }
        
        TimeSpan IHaveEstimate.Estimate
        {
            get { return this.Estimate; }
        }

        public virtual void RegisterWork(TimeSpan amount, User developer, Place place)
        {
            Contract.Requires(developer != null);
            
            var registeredWork = new RegisteredWork(amount, developer, place, this);
            this.RegisteredWork.Add(registeredWork);
        }

        public virtual Task CreateSubtask(TaskSpecification specification)
        {
            Contract.Requires(specification != null);

            var subtask = new Task(specification, this.Project);
            subtask.ParentTask = this;
            this.Subtasks.Add(subtask);
            return subtask;
        }
    }
}