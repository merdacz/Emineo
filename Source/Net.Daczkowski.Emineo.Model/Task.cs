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
    public class Task : TaskContainer, IHaveEstimate
    {
        public Task()
        {
            this.RegisteredWork = new List<RegisteredWork>();
            this.RegisterChildCollection(() => this.RegisteredWork);    
        }

        public Task(TaskSpecification specification, Project project, TaskContainer parent) : this()
        {
            Contract.Requires(specification != null);

            this.Summary = specification.Summary;
            this.Description = specification.Description;
            this.Estimate = specification.Estimate;
            this.Project = project;
            this.Parent = parent;
        }

        public virtual string Summary { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual User AssignedTo { get; protected set; }

        public virtual TimeSpan Estimate { get; protected set; }

        public virtual TaskContainer Parent { get; protected set; }

        public virtual Project Project { get; protected set; }

        public virtual ICollection<RegisteredWork> RegisteredWork { get; protected set; }
        
        TimeSpan IHaveEstimate.Estimate
        {
            get { return this.Estimate; }
        }

        public virtual ICollection<Task> Subtasks
        {
            get { return this.Tasks; }
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

            var subtask = new Task(specification, this.Project, this);
            this.Tasks.Add(subtask);
            return subtask;
        }
    }
}