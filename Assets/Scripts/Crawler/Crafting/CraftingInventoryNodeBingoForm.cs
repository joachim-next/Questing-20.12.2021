namespace Crawler.Crafting
{
    public class CraftingInventoryNodeBingoForm
    {
        public CraftingInventoryNode Node { get; }
        public CraftingFormation Formation { get; }
        public bool[] CheckedNodes { get; }

        public CraftingInventoryNodeBingoForm(CraftingInventoryNode node, CraftingFormation formation)
        {
            Node = node;
            Formation = formation;
            CheckedNodes = new bool[formation.Nodes.Length];
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var target = (CraftingInventoryNodeBingoForm) obj;

            if (!NodeEquals(target.Node))
            {
                return false;
            }

            if (!FormationEquals(target.Formation))
            {
                return false;
            }

            if (!CheckedNodesEquals(target.CheckedNodes))
            {
                return false;
            }

            return true;
        }

        private bool NodeEquals(CraftingInventoryNode target)
        {
            return target != null && target.IngredientType == Node.IngredientType;
        }

        private bool FormationEquals(CraftingFormation target)
        {
            if (target?.Nodes == null)
            {
                return false;
            }
            
            if (target.Nodes.Length != Formation.Nodes.Length)
            {
                return false;
            }

            for (int i = 0; i < target.Nodes.Length; i++)
            {
                var targetNode = target.Nodes[i];
                var node = Formation.Nodes[i];

                if (targetNode.IngredientType != node.IngredientType)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckedNodesEquals(bool[] target)
        {
            if (target == null ||
                target.Length != CheckedNodes.Length)
            {
                return false;
            }

            for (int i = 0; i < target.Length; i++)
            {
                var targetValue = target[i];
                var value = CheckedNodes[i];

                if (targetValue != value)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            var nodeHash = NodeGetHashCode();
            var formationHash = FormationGetHashCode();

            return nodeHash ^ formationHash;
        }

        private int NodeGetHashCode()
        {
            return Node.IngredientType;
        }

        private int FormationGetHashCode()
        {
            if (Formation?.Nodes == null)
            {
                return 0;
            }

            int value = Formation.Nodes[0]?.IngredientType ?? 0;

            if (Formation.Nodes.Length == 1)
            {
                return value;
            }
            
            for (int i = 1; i < Formation.Nodes.Length; i++)
            {
                value = value ^ Formation.Nodes[i]?.IngredientType ?? 0;
            }

            return value;
        }

        public void TryCheck(int x, int y)
        {
            CraftingFormationNode match = null;
            int indexOfMatch = -1;
            for (int i = 0; i < Formation.Nodes.Length; i++)
            {
                var formationNode = Formation.Nodes[i];

                var normalizedX = Node.X + formationNode.RelativeX;
                var normalizedY = Node.Y + formationNode.RelativeY;
                
                if (normalizedX != x ||
                    normalizedY != y)
                {
                    continue;
                }

                match = formationNode;
                indexOfMatch = i;
                break;
            }
            
            if (match == default)
            {
                return;
            }

            CheckedNodes[indexOfMatch] = true;
        }
    }
}