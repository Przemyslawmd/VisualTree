
namespace VisualTree
{
    class Action
    {
        public Action( ActionType actionType, Node node )
        {
            ActionType = actionType;
            Node = node;
        }

        public ActionType ActionType { get; }
        public Node Node {  get; }
    }
}

