using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2021
{
    partial class Day12
    {
        public class GraphNode{

            string name;
            bool big;
            List<GraphNode> adjecent;

            public GraphNode(string name)
            {
                this.name = name;
            }

            public void ConnectNode(GraphNode node)
            {
                adjecent.Add(node);
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    return false;
                }
                return Equals((GraphNode) obj);
            }

            private bool Equals(GraphNode node){
                return node.ToString() == this.ToString();
            }

            public override string ToString()
            {
                return $"{name}";
            }
        }
    }
}
