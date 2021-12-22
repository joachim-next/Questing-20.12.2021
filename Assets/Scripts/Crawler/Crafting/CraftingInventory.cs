using NSubstitute;

namespace Crawler.Crafting
{
    public class CraftingInventory : ICraftingInventory
    {
        public bool HasItems => Nodes.Length != 0; 
        public CraftingInventoryItem[] Nodes { get; }
        
        public CraftingInventory(CraftingInventoryItem[] nodes)
        {
            Nodes = nodes;
        }

        public override bool Equals(object obj)
        {
            if (obj == null ||
                obj.GetType() != GetType())
            {
                return false;
            }
        
            var target = (CraftingInventory) obj;
        
            if (target.Nodes == null)
            {
                return false;
            }
        
            if (Nodes == null)
            {
                return false;
            }
        
            if (target.Nodes.Length != Nodes.Length)
            {
                return false;
            }
        
            for (int i = 0; i < target.Nodes.Length; i++)
            {
                var targetNode = target.Nodes[i];
                var node = Nodes[i];
        
                if (!targetNode.Equals(node))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public override int GetHashCode()
        {
            var hash = 0;

            if (Nodes.Length == 0)
            {
                return hash;
            }

            hash = Nodes[0].GetHashCode();

            if (Nodes.Length == 1)
            {
                return hash;
            }

            for (int i = 1; i < Nodes.Length; i++)
            {
                var node = Nodes[i];
                hash = hash ^ node.GetHashCode();
            }

            return hash;
        }
    }
}