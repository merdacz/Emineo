namespace Net.Daczkowski.Emineo.Model
{
    using System;
    using System.Collections.Generic;
    using Net.Daczkowski.Emineo.Model.Visitors;

    /// <summary>
    /// Common behavior for all entities.
    /// </summary>
    /// <remarks>
    /// Includes necessary logic to handle persitabel objects comparisions and
    /// provides entry point for <see cref="IVisitor"/> application. 
    /// </remarks>
    public abstract class Entity 
    {
        private readonly List<Func<IEnumerable<Entity>>> childrenAccessors = new List<Func<IEnumerable<Entity>>>();

        private const int Id = default(int);

        public int ID
        {
            get { return Id; }
        }

        public override sealed bool Equals(object obj)
        {
            var compareTo =
                obj as Entity;

            return compareTo != null &&
                   (this.HasSameNonDefaultIdAs(compareTo) ||
                    ((this.IsTransient() || compareTo.IsTransient()) &&
                     this.HasSameBusinessSignatureAs(compareTo)));
        }

        public bool IsTransient()
        {
            return this.ID.Equals(default(int));
        }

        public abstract override int GetHashCode();

        private bool HasSameBusinessSignatureAs(Entity compareTo)
        {
            return this.GetHashCode().Equals(compareTo.GetHashCode());
        }

        private bool HasSameNonDefaultIdAs(Entity compareTo)
        {
            return (!this.ID.Equals(default(int))) &&
                   (!compareTo.ID.Equals(default(int))) &&
                   this.ID.Equals(compareTo.ID);
        }

        public T AcceptVisitor<T>(T visitor) where T : IVisitor
        {
            visitor.Visit(this, this.VisitChildren);
            return visitor;
        }

        protected void RegisterChildCollection(Func<IEnumerable<Entity>> children) 
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