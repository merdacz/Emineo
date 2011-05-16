namespace Net.Daczkowski.Emineo.Model
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Security;
    using Net.Daczkowski.Emineo.Model.Helpers;

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

        public virtual DateTime? LastLogin { get; protected set; }

        public virtual bool IsAuthenticated { get; protected set; }

        public virtual void Authenticate(string password)
        {
            Contract.Requires(password != null);

            if (this.IsAuthenticated)
            {
                return;
            }

            if (password == "koteczek")
            {
                this.IsAuthenticated = true;
                this.LastLogin = DateTimeNow.Value;
            }
            else
            {
                throw new SecurityException("Cannot authenticate user. ");    
            }
        }

        public override string ToString()
        {
            var result = this.Name;
            if (this.LastLogin.HasValue)
            {
                result += "(" + this.LastLogin.Value + ")";
            }
            else
            {
                result += "(no logged in yet)";
            }

            return result;
        }
    }
}