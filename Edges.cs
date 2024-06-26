namespace Gráfelméleti_algoritmusok
{
    [Serializable]
    public class Edges
    {
        public bool IsDirected;
        public Nodes FirstPoint;
        public Nodes SecondPoint;
        public int Weight;

        public int EdgeColorR;
        public int EdgeColorG;
        public int EdgeColorB;

        public int FirstPointId;
        public int SecondPointId;

        public bool Visited;

        public PointF MidPoint { get; set; }

        public Edges()
        {
            this.Visited = false;
        }
        public void MakeRed()
        {
            EdgeColorR = 255;
            EdgeColorG = 0;
            EdgeColorB = 0;
        }

        public void ResetColor()
        {
            EdgeColorR = 0;
            EdgeColorG = 0;
            EdgeColorB = 0;
        }
    }
}
