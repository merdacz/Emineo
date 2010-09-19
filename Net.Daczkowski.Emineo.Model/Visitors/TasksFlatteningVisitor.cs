namespace Net.Tgd.NH.Model.Visitors
{
    using System;
    using Net.Daczkowski.Emineo.Model;

    public class TasksFlatteningVisitor : IVisitor
    {
        public void Visit(Entity node, Action<IVisitor> visitChildren)
        {
            visitChildren(this);
        }
    }
}
