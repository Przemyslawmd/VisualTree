
namespace VisualTree
{
    class ActionTree
    {
        public ActionTree( ActionTreeType actionTreeType, Node node )
        {
            ActionTreeType = actionTreeType;
            Node = node;
        }

        public ActionTreeType ActionTreeType { get; }
        public Node Node {  get; set; }
    }
}

