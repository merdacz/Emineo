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

        public string Name { get; protected set; }
        
        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + this.Name).GetHashCode();
        }
    }
}