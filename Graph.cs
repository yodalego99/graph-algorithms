namespace Gráfelméleti_algoritmusok
{
    [Serializable]
    public class Nodes
    {
        public int Id { get; set; }
        public Point Position { get; set; }
        public string Name { get; set; }
        public bool Visited;
        public int NodeColorR;
        public int NodeColorG;
        public int NodeColorB;
        public bool StartingPoint { get; set; }
        public bool EndingPoint { get; set; }

        public Nodes()
        {
            this.StartingPoint = false;
            this.EndingPoint = false;
            this.NodeColorR = 0;
            this.NodeColorG = 0;
            this.NodeColorB = 0;
            this.Visited = false;
        }

        public void MakeRed()
        {
            NodeColorR = 255;
            NodeColorG = 0;
            NodeColorB = 0;
        }

        public void ResetColor()
        {
            NodeColorR = 0;
            NodeColorG = 0;
            NodeColorB = 0;
        }
    }
}
