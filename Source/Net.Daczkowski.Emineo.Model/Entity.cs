namespace Net.Daczkowski.Emineo.Model
{
    using System;
    using System.Collections.Generic;
    using Net.Daczkowski.Emineo.Model.Visitors;

    /// <summary>
    /// Common behavior for all entities.
    /// </summary>
    /// <remarks>
    /// Includes necessary logic to handle persistable objects comparisions and
    /// provides entry point for <see cref="IVisitor"/> application. 
    /// </remarks>
    public abstract class Entity 
    {
        private readonly List<Func<IEnumerable<Entity>>> childrenAccessors = new List<Func<IEnumerable<Entity>>>();

        public virtual int ID
        {
            get; protected set;
        }
        
        public virtual T AcceptVisitor<T>(T visitor) where T : IVisitor
        {
            visitor.Visit(this, this.VisitChildren);
            return visitor;
        }

        protected virtual void RegisterChildCollection(Func<IEnumerable<Entity>> children) 
        {
            this.childrenAccessors.Add(children);
        }

        private void VisitChildren(IVisitor visitor)
        {
            foreach (var childAccessor in this.childrenAccessors)
            {
                var children = childAccessor();
                foreach (var child in children)
                {
                    child.AcceptVisitor(visitor);
                }
            }
        }
    }
}