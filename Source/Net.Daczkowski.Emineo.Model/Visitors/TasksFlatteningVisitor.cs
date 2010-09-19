namespace Net.Daczkowski.Emineo.Model.Visitors
{
    using System;
    using System.Collections.Generic;
    using Net.Daczkowski.Emineo.Model;

    public class TasksFlatteningVisitor : IVisitor
    {
        private readonly IList<Task> tasks = new List<Task>();

        public IEnumerable<Task> Tasks
        {
            get { return this.tasks; }
        }

        public void Visit(Entity node, Action<IVisitor> visitChildren)
        {
            var task = node as Task;
            if (task != null)
            {
                this.tasks.Add(task);
            }

            visitChildren(this);
        }
    }
}
