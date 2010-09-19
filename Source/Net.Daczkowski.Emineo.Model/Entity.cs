namespace Net.Daczkowski.Emineo.Model
{
    using System.Collections.Generic;
    using Net.Tgd.NH.Model.Visitors;

    public abstract class Entity 
    {
        private List<Entity> children = new List<Entity>();

        /// <summary>  
        /// ID may be of type string, int,   
        /// custom type, etc.  
        /// </summary>  
        public int ID
        {
            get { return id; }
        }

        public override sealed bool Equals(object obj)
        {
            Entity compareTo =
                obj as Entity;

            return (compareTo != null) &&
                   (HasSameNonDefaultIdAs(compareTo) ||
                    // Since the IDs aren't the same, either   
                    // of them must be transient to compare   
                    // business value signatures  
                    (((IsTransient()) || compareTo.IsTransient()) &&
                     HasSameBusinessSignatureAs(compareTo)));
        }

        /// <summary>  
        /// Transient objects are not associated with an   
        /// item already in storage.  For instance, a   
        /// Customer is transient if its ID is 0.  
        /// </summary>  
        public bool IsTransient()
        {
            return ID.Equals(default(int));
        }

        /// <summary>  
        /// Must be implemented to compare two objects  
        /// </summary>  
        public abstract override int GetHashCode();

        private bool HasSameBusinessSignatureAs(Entity compareTo)
        {
            return GetHashCode().Equals(compareTo.GetHashCode());
        }

        /// <summary>  
        /// Returns true if self and the provided domain   
        /// object have the same ID values and the IDs   
        /// are not of the default ID value  
        /// </summary>  
        private bool HasSameNonDefaultIdAs(Entity compareTo)
        {
            return (!ID.Equals(default(int))) &&
                   (!compareTo.ID.Equals(default(int))) &&
                   ID.Equals(compareTo.ID);
        }

        /// <summary>  
        /// Set to protected to allow unit tests to set   
        /// this property via reflection and to allow   
        /// domain objects more flexibility in setting   
        /// this for those objects with assigned IDs.  
        /// </summary>  
        protected int id = default(int);

        public T AcceptVisitor<T>(T visitor) where T : IVisitor
        {
            visitor.Visit(this, VisitChildren);
            return visitor;
        }

        protected void RegisterChildCollection(IEnumerable<Entity> children)
        {
            this.children.AddRange(children);
        }

        private void VisitChildren(IVisitor visitor)
        {
            foreach (var child in this.children)
            {
                child.AcceptVisitor(visitor);
            }
        }
    }
}