namespace Net.Daczkowski.Emineo.Model
{
    /// <summary>
    /// System user account. 
    /// </summary>
    public class User : Entity
    {
        protected User()
        {
        }

        public User(string name)
        {
            this.Name = name;
        }

        public virtual string Name { get; protected set; }
    }
}