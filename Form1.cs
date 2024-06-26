using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Gráfelméleti_algoritmusok
{
    public partial class Form1 : Form
    {
        List<Nodes> nodeList = new List<Nodes>();
        List<Edges> edgeList = new List<Edges>();
        Random rnd = new Random();
        private bool editMode = false;
        private bool editStartpoint = false;

        private bool editEndpoint = false;
        Nodes? currentEndPoint;

        // thumbnail sizes for load menu
        int thumbWidth;
        int thumbHeight;

        public Form1()
        {
            InitializeComponent();
            InitializeAlgorithmComboBox();
            thumbHeight = (pictureBox1.Height / 4);
            thumbWidth = (pictureBox1.Width / 4);
            ApplyButtonStyle(SaveButton, false);
            ApplyButtonStyle(LoadButton, false);
            ApplyButtonStyle(StopButton, false);
            ApplyButtonStyle(NextButton, false);
            ApplyButtonStyle(PrevButton, false);
        }

        private void InitializeAlgorithmComboBox()
        {
            comboBox1.Items.Add("Depth-First Search");
            comboBox1.Items.Add("Breadth-First Search");
            comboBox1.Items.Add("Hamiltonian route");
            comboBox1.Items.Add("Dijkstra");
            comboBox1.Items.Add("Boruvka");
            comboBox1.Items.Add("Topological Sort");
            comboBox1.SelectedIndex = -1; //leave empty as default
        }
        private void AddNodeButton_Click(object sender, EventArgs e)
        {
            //avoiding duplicate names
            int newName = 1;
            HashSet<int> existingNames = new HashSet<int>(nodeList.Select(g => int.Parse(g.Name)));
            while (existingNames.Contains(newName))
            {
                newName++;
            }

            Nodes newNode = new Nodes();
            bool nodePositionValid;
            Point newPosition;
            int attempts = 0;
            int maxAttempts = 500; // it can give up too easily if unlucky but this is an edge case anyway
            int margin = 31;

            do
            {
                nodePositionValid = true;
                newPosition = new Point(rnd.Next(31, pictureBox1.Width - 31), rnd.Next(31, pictureBox1.Height - 31));
                foreach (Nodes node in nodeList)
                {
                    if (Math.Abs(node.Position.X - newPosition.X) < margin && Math.Abs(node.Position.Y - newPosition.Y) < margin)
                    {
                        nodePositionValid = false;
                        break;
                    }
                }

                attempts++;

                if (attempts >= maxAttempts)
                {
                    nodePositionValid = true; //breaking loop
                    newNode = null;
                    break;
                }
            } while (!nodePositionValid);

            if (newNode != null)
            {
                newNode.Position = newPosition;
                newNode.Name = newName.ToString();
                nodeList.Add(newNode);
            }
            else
            {
                MessageBox.Show("No space for the new node.");
            }

            if (currentStartpoint == null)
            {
                // find first node that is not set as the end point
                Nodes potentialStartpoint = nodeList.FirstOrDefault(g => !g.EndingPoint);
                if (potentialStartpoint != null)
                {
                    currentStartpoint = potentialStartpoint;
                    potentialStartpoint.StartingPoint = true;
                }
            }
            pictureBox1.Refresh();
        }

        private void DrawArrow(Graphics g, Pen pen, PointF start, PointF end)
        {
            const int arrowHeadLength = 15;
            const int arrowDegrees = 45;

            // get line direction
            double dx = end.X - start.X;
            double dy = end.Y - start.Y;
            double angle = Math.Atan2(dy, dx);

            // pulling it back a bit so it doesnt overlap with the end node
            PointF arrowBase = new PointF(end.X - (float)(Math.Cos(angle) * 20), end.Y - (float)(Math.Sin(angle) * 20));

            PointF arrowLeft = new PointF(
                arrowBase.X - (float)(Math.Cos(angle + Math.PI / 180 * arrowDegrees) * arrowHeadLength),
                arrowBase.Y - (float)(Math.Sin(angle + Math.PI / 180 * arrowDegrees) * arrowHeadLength)
            );

            PointF arrowRight = new PointF(
                arrowBase.X - (float)(Math.Cos(angle - Math.PI / 180 * arrowDegrees) * arrowHeadLength),
                arrowBase.Y - (float)(Math.Sin(angle - Math.PI / 180 * arrowDegrees) * arrowHeadLength)
            );

            g.DrawLine(pen, arrowBase, arrowLeft);
            g.DrawLine(pen, arrowBase, arrowRight);
        }

        private PointF GetControlPoint(PointF start, PointF end, bool flip)
        {
            PointF middle = new PointF((start.X + end.X) / 2, (start.Y + end.Y) / 2);
            float changeX = end.X - start.X;
            float changeY = end.Y - start.Y;

            float distance = (float)Math.Sqrt(changeX * changeX + changeY * changeY);

            float curveSize = flip ? -50.0f : 50.0f;

            // shifting middle point to right for curve
            PointF control = new PointF(middle.X - changeY * curveSize / distance, middle.Y + changeX * curveSize / distance);
            return control;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            StringFormat TextStyle = new StringFormat();
            TextStyle.Alignment = StringAlignment.Center;
            TextStyle.LineAlignment = StringAlignment.Center;
            Font PointName = new Font(this.Font, FontStyle.Bold);
            Brush GraphPointName = new SolidBrush(Color.Black);
            float weightNameSize = 18;
            Font weightFont = new Font("Arial", weightNameSize);

            //draw edges
            if (editMode && FirstPoint != null && DrawDraggedLine) //addedge on
            {
                Pen EdgePencil = new Pen(Color.Black, 5);
                e.Graphics.DrawLine(EdgePencil, FirstPoint.Position, CursorPosition);
            }
            if (directedEditMode && FirstPoint != null && DrawDraggedLine) //directed addedge on
            {
                Pen EdgePencil = new Pen(Color.Black, 5);
                DrawArrow(e.Graphics, EdgePencil, FirstPoint.Position, CursorPosition);
                e.Graphics.DrawLine(EdgePencil, FirstPoint.Position, CursorPosition);
            }
            foreach (Edges item in edgeList)
            {
                Pen EdgePencil = new Pen(Color.FromArgb(item.EdgeColorR, item.EdgeColorG, item.EdgeColorB), 5);
                PointF startPointF = item.FirstPoint.Position;
                PointF endPointF = item.SecondPoint.Position;
                bool hasReciprocal = edgeList.Any(e => e.FirstPoint == item.SecondPoint && e.SecondPoint == item.FirstPoint && e.IsDirected);
                PointF controlPoint = GetControlPoint(startPointF, endPointF, hasReciprocal);

                if (item.IsDirected)
                {
                    e.Graphics.DrawBezier(EdgePencil, startPointF, controlPoint, controlPoint, endPointF);
                    DrawArrow(e.Graphics, EdgePencil, controlPoint, endPointF);

                    item.MidPoint = new PointF(
                        (controlPoint.X + endPointF.X) / 2,
                        (controlPoint.Y + endPointF.Y) / 2);
                }
                else // non directed edges
                {
                    e.Graphics.DrawLine(EdgePencil, startPointF, endPointF);
                    item.MidPoint = new PointF((startPointF.X + endPointF.X) / 2, (startPointF.Y + endPointF.Y) / 2);
                }

                // draw detection area for weight edit and delete modes
                if ((weightEditMode && item.Weight == 0) || deleteMode)
                {
                    const float circleRadius = 15f;
                    e.Graphics.DrawEllipse(new Pen(Color.HotPink, 2), item.MidPoint.X - circleRadius, item.MidPoint.Y - circleRadius, circleRadius * 2, circleRadius * 2);
                }

                // draw weight text and its background
                if (item.Weight > 0)
                {
                    string weightText = item.Weight.ToString();
                    SizeF textSize = e.Graphics.MeasureString(weightText, weightFont);
                    PointF textPoint = item.MidPoint;
                    RectangleF backgroundRect = new RectangleF(
                        textPoint.X - (textSize.Width / 2), textPoint.Y - (textSize.Height / 2),
                        textSize.Width, textSize.Height);

                    e.Graphics.FillRectangle(Brushes.White, backgroundRect);
                    e.Graphics.DrawString(weightText, weightFont, new SolidBrush(Color.Blue), item.MidPoint, TextStyle);
                }
            }

            //draw nodes
            foreach (Nodes item in nodeList)
            {
                Pen Pencil = new Pen(Color.FromArgb(item.NodeColorR, item.NodeColorG, item.NodeColorB), 3);
                Rectangle PointSize = new Rectangle(item.Position.X - 15, item.Position.Y - 15, 30, 30);
                e.Graphics.DrawEllipse(Pencil, PointSize);
                e.Graphics.FillEllipse(new SolidBrush(Color.White), PointSize);
                if (item.StartingPoint)
                {
                    e.Graphics.FillEllipse(new SolidBrush(Color.Yellow), PointSize);
                }
                if (item.EndingPoint)
                {
                    e.Graphics.FillEllipse(new SolidBrush(Color.RosyBrown), PointSize);
                }

                e.Graphics.DrawString(item.Name, PointName, GraphPointName, PointSize, TextStyle);
            }
            richTextBox1.ScrollToCaret();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            nodeList.Clear();
            edgeList.Clear();
            pictureBox1.Refresh();
            EdgeCount = 0;
            currentStartpoint = null;
            currentEndPoint = null;
        }

        Nodes FirstPoint;
        Nodes SecondPoint;

        bool DrawDraggedLine = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (weightEditMode || editStartpoint || editEndpoint)
            {
                return;
            }

            foreach (Nodes node in nodeList)
            {
                if (IsMouseOverNode(e.Location, node))
                {
                    if (editMode || directedEditMode)
                    {
                        FirstPoint = node; // set up first node for edge drawing
                        CursorPosition = e.Location;
                        pictureBox1.MouseMove += pictureBox1_MouseMove;
                    }
                    else
                    {
                        MovingGraph = node; // set up node for moving 
                        pictureBox1.MouseMove += pictureBox1_MouseMove;
                    }
                    break; // stop loop once the first applicable node is found
                }
            }
            DrawDraggedLine = true;
        }

        Nodes MovingGraph;
        Point CursorPosition;

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location.X > 30 && e.Location.Y > 30 && e.Location.X < pictureBox1.Width - 30 && e.Location.Y < pictureBox1.Height - 30)
            {
                if (editMode || directedEditMode)
                {
                    CursorPosition = e.Location;
                    pictureBox1.Refresh();
                }
                else
                {
                    MovingGraph.Position = e.Location;
                    pictureBox1.Refresh();
                }
            }
        }

        int EdgeCount = 0;

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int PointCount = 0; // counter for multiple points in same spot

            pictureBox1.MouseMove -= pictureBox1_MouseMove;
            if (weightEditMode || FirstPoint == null)
            {
                return;
            }

            if (editMode || directedEditMode)
            {
                foreach (Nodes item in nodeList)
                {
                    if (IsMouseOverNode(e.Location, item))
                    {
                        PointCount++;
                        if (item == FirstPoint)
                        {
                            break;
                        }
                        if (PointCount == 1)
                        {
                            Edges newEdge = new Edges
                            {
                                FirstPoint = FirstPoint,
                                SecondPoint = item,
                                IsDirected = directedEditMode,
                                Weight = 0,
                                EdgeColorR = 0,
                                EdgeColorG = 0,
                                EdgeColorB = 0
                            };

                            edgeList.Add(newEdge);

                            if (directedEditMode)
                            {
                                edgeList = edgeList.AsParallel().ToList();
                            }
                            else
                            {
                                edgeList = edgeList.AsParallel().Distinct().ToList();
                            }

                            EdgeCount++;
                        }
                    }
                }
            }
            DrawDraggedLine = false;
            pictureBox1.Refresh();
        }

        private void AddEdgeButton_Click(object sender, EventArgs e)
        {
            editMode = !editMode;
            pictureBox1.Refresh();

            ApplyButtonStyle(SelectStartButton, !editMode);
            ApplyButtonStyle(ResetButton, !editMode);
            ApplyButtonStyle(AddNodeButton, !editMode);
            ApplyButtonStyle(AddWeightButton, !editMode);
            ApplyButtonStyle(AddDirectedEdgeButton, !editMode);
            ApplyButtonStyle(StartButton, !editMode);
            ApplyButtonStyle(SelectEndPointButton, !editMode);
            ApplyButtonStyle(DeleteButton, !editMode);
        }

        private bool IsMouseOverNode(Point mousePosition, Nodes node)
        {
            return mousePosition.X <= node.Position.X + 30 &&
                   mousePosition.X >= node.Position.X - 30 &&
                   mousePosition.Y <= node.Position.Y + 30 &&
                   mousePosition.Y >= node.Position.Y - 30;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if (comboBox1.SelectedItem == null)
            {
                return;
            }
            string selectedAlgorithm = comboBox1.SelectedItem.ToString();

            switch (selectedAlgorithm)
            {
                case "Depth-First Search":
                    if (nodeList.Count == 0)
                    {
                        MessageBox.Show("The graph is empty. Please create one first.", "Graph Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    runDFS(sender, e);
                    ApplyButtonStyle(NextButton, true);
                    ApplyButtonStyle(PrevButton, true);
                    ApplyButtonStyle(StopButton, true);
                    break;
                case "Breadth-First Search":
                    if (nodeList.Count == 0)
                    {
                        MessageBox.Show("The graph is empty. Please create one first.", "Graph Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    runBFS();
                    ApplyButtonStyle(NextButton, true);
                    ApplyButtonStyle(PrevButton, false);
                    ApplyButtonStyle(StopButton, true);
                    break;
                case "Hamiltonian route":
                    if (nodeList.Count == 0)
                    {
                        MessageBox.Show("The graph is empty. Please create one first.", "Graph Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    ApplyButtonStyle(NextButton, false);
                    ApplyButtonStyle(PrevButton, false);
                    ApplyButtonStyle(StopButton, false);
                    DisplayHamiltonianPathExplanation();
                    break;
                case "Dijkstra":
                    if (currentStartpoint == null || nodeList.Count == 0 || currentEndPoint == null)
                    {
                        MessageBox.Show("Please define a starting and ending point, then ensure the graph is not empty.");
                        return;
                    }
                    ApplyButtonStyle(NextButton, false);
                    ApplyButtonStyle(PrevButton, false);
                    ApplyButtonStyle(StopButton, false);
                    RunDijkstra(currentStartpoint, currentEndPoint);
                    break;
                case "Boruvka":
                    if (nodeList.Count == 0)
                    {
                        MessageBox.Show("The graph is empty. Please create one first.", "Graph Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (ContainsDirectedEdges())
                    {
                        MessageBox.Show("Boruvka's algorithm cannot be used with directed graphs.", "Inappropriate Graph Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    RunBoruvka();
                    ApplyButtonStyle(NextButton, true);
                    ApplyButtonStyle(PrevButton, false);
                    ApplyButtonStyle(StopButton, true);
                    break;
                case "Topological Sort":
                    if (nodeList.Count == 0)
                    {
                        MessageBox.Show("The graph is empty. Please create one first.", "Graph Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (ContainsUndirectedEdges() || HasCycles())
                    {
                        MessageBox.Show("The graph contains cycles or undirected edges. Please ensure all edges are directed and the graph has no cycles before attempting a topological sort.", "Topological Sort Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    runTopologicalSort();
                    ApplyButtonStyle(NextButton, true);
                    ApplyButtonStyle(PrevButton, false);
                    ApplyButtonStyle(StopButton, true);
                    break;
            }

            ApplyButtonStyle(StartButton, false);
        }

        private bool ContainsDirectedEdges()
        {
            return edgeList.Any(edge => edge.IsDirected);
        }


        List<Nodes> visitedNodesHistory = new List<Nodes>();
        List<Edges> visitedEdgesHistory = new List<Edges>();
        int currentStep = -1;

        private Stack<Nodes> ForwardStack = new Stack<Nodes>();
        private Stack<Nodes> BackwardStack = new Stack<Nodes>();

        EventHandler NextEvent;
        EventHandler PrevEvent;

        private void runDFS(object sender, EventArgs e)
        {
            NextButton.Click -= NextEvent; // removing any event handlers before adding the current ones
            NextEvent = new EventHandler(DFSMoveForward);
            NextButton.Click += NextEvent;

            PrevButton.Click -= PrevEvent;
            PrevEvent = new EventHandler(DFSMoveBackward);
            PrevButton.Click += PrevEvent;

            // reset
            richTextBox1.Clear();
            foreach (var node in nodeList) node.Visited = false;
            foreach (var edge in edgeList) edge.Visited = false;
            visitedNodesHistory.Clear();
            visitedEdgesHistory.Clear();
            currentStep = -1;

            // find starting point and run dfs from there
            var startingNode = nodeList.FirstOrDefault(node => node.StartingPoint);
            if (startingNode != null)
            {
                DFS(startingNode);
            }
        }

        private void DFS(Nodes node)
        {
            if (!node.Visited)
            {
                node.Visited = true;
                visitedNodesHistory.Add(node);
                // find edges that are directed and the current node is the first point OR are undirected and the current node is the first or second point
                foreach (var edge in edgeList.Where(e => (e.FirstPoint == node && e.IsDirected) || (!e.IsDirected && (e.FirstPoint == node || e.SecondPoint == node))))
                {
                    Nodes neighbor = edge.FirstPoint == node ? edge.SecondPoint : edge.FirstPoint;
                    if (!neighbor.Visited)
                    {
                        visitedEdgesHistory.Add(edge);
                        DFS(neighbor);
                    }
                }
            }
        }

        private void DFSMoveForward(object sender, EventArgs e)
        {
            if (currentStep + 1 < visitedNodesHistory.Count)
            {
                currentStep++;
                richTextBox1.AppendText($"Moving forward to Node {visitedNodesHistory[currentStep].Name}\n");
                visitedNodesHistory[currentStep].MakeRed();
                if (currentStep > 0)
                {
                    visitedEdgesHistory[currentStep - 1].MakeRed();
                }
                pictureBox1.Refresh();
            }
            else
            {
                richTextBox1.AppendText("\nReached the end of the traversal.\n");
            }
        }

        private void DFSMoveBackward(object sender, EventArgs e)
        {
            if (currentStep >= 0)
            {
                richTextBox1.AppendText($"Moving backward, unvisiting Node {visitedNodesHistory[currentStep].Name}\n");
                visitedNodesHistory[currentStep].ResetColor();
                if (currentStep > 0)
                {
                    visitedEdgesHistory[currentStep - 1].ResetColor();
                }
                currentStep--;
                pictureBox1.Refresh();
            }
            else
            {
                richTextBox1.AppendText("\nCannot move backward anymore, at the start of the traversal.\n");
            }
        }

        private Queue<Nodes> bfsQueue = new Queue<Nodes>();
        private HashSet<Nodes> visitedBFS = new HashSet<Nodes>();

        private void runBFS()
        {
            NextButton.Click -= NextEvent;
            NextEvent = new EventHandler(BFSMoveForward);
            NextButton.Click += NextEvent;

            PrevButton.Click -= PrevEvent;

            ApplyButtonStyle(PrevButton, false);

            richTextBox1.Clear();
            Nodes startingNode = nodeList.FirstOrDefault(g => g.StartingPoint);
            if (startingNode == null)
            {
                richTextBox1.AppendText("\nError: No starting node defined for BFS.\n");
                return;
            }

            // reset
            bfsQueue.Clear();
            visitedBFS.Clear();
            bfsQueue.Enqueue(startingNode);

            richTextBox1.AppendText($"\nBFS initialized at Node {startingNode.Name}. Press 'Forward' to start exploring.\n");
        }

        private void BFSMoveForward(object sender, EventArgs e)
        {
            if (bfsQueue.Count == 0)
            {
                richTextBox1.AppendText("\nBFS traversal done.\n");
                return;
            }

            var currentNode = bfsQueue.Dequeue();
            currentNode.MakeRed();
            visitedBFS.Add(currentNode);
            richTextBox1.AppendText($"Checking connections from Node {currentNode.Name}...\n");

            foreach (var edge in edgeList.Where(e => (e.FirstPoint == currentNode && e.IsDirected) ||
                                                     (!e.IsDirected && (e.FirstPoint == currentNode || e.SecondPoint == currentNode))))
            {
                Nodes neighbor = edge.FirstPoint == currentNode ? edge.SecondPoint : edge.FirstPoint;
                if (!visitedBFS.Contains(neighbor))
                {
                    bfsQueue.Enqueue(neighbor);
                    richTextBox1.AppendText($"-- Added Node {neighbor.Name} to queue.\n");
                }
                edge.MakeRed();
                if (!visitedEdgesHistory.Contains(edge))
                {
                    visitedEdgesHistory.Add(edge);
                }
            }

            richTextBox1.AppendText($"Done with Node {currentNode.Name}.\n\n");
            pictureBox1.Refresh();
        }

        public List<Nodes> CalculateHamiltonianPath()
        {
            ApplyButtonStyle(StopButton, false);
            List<Nodes> path = new List<Nodes>();
            HashSet<Nodes> visited = new HashSet<Nodes>();
            Nodes startingNode = nodeList.FirstOrDefault(g => g.StartingPoint);

            visited.Add(startingNode);
            path.Add(startingNode);

            if (FindHamiltonianPath(startingNode, path, visited))
            {
                return path;
            }
            else
            {
                return null; // no path found
            }
        }

        private bool FindHamiltonianPath(Nodes currentNode, List<Nodes> path, HashSet<Nodes> visited)
        {
            if (path.Count == nodeList.Count)
            {
                return true; // path found
            }

            foreach (var edge in edgeList)
            {
                Nodes nextNode = null;

                if (edge.IsDirected && edge.FirstPoint == currentNode && !visited.Contains(edge.SecondPoint))
                {
                    nextNode = edge.SecondPoint;
                }
                else if (!edge.IsDirected && edge.FirstPoint == currentNode && !visited.Contains(edge.SecondPoint))
                {
                    nextNode = edge.SecondPoint;
                }
                else if (!edge.IsDirected && edge.SecondPoint == currentNode && !visited.Contains(edge.FirstPoint))
                {
                    nextNode = edge.FirstPoint;
                }

                if (nextNode != null && !visited.Contains(nextNode))
                {
                    visited.Add(nextNode);
                    path.Add(nextNode);

                    if (FindHamiltonianPath(nextNode, path, visited))
                    {
                        return true; // path found
                    }

                    // dead end, backtracking
                    visited.Remove(nextNode);
                    path.RemoveAt(path.Count - 1);
                }
            }
            return false; // no path found
        }

        private async void DisplayHamiltonianPathExplanation()
        {
            richTextBox1.Clear();
            richTextBox1.AppendText("Initiating the search for a Hamiltonian Path...\n\n");
            richTextBox1.AppendText("A Hamiltonian path is a unique route in a graph that visits each node exactly once. Finding such a path is critical in many fields such as logistics, networking, and optimization. Unlike Eulerian paths which traverse every edge once, Hamiltonian paths are concerned with visiting every vertex once without revisiting.\n\n");
            richTextBox1.AppendText("The algorithm will now attempt to discover such a path in the current graph, backtracking if necessary, and visualize each step of the journey. Let's begin.\n\n");
            ApplyButtonStyle(StopButton, false);

            List<Nodes> hamiltonianPath = CalculateHamiltonianPath();

            if (hamiltonianPath != null)
            {
                richTextBox1.AppendText($"Moving from: \n");
                // animate nodes and edges
                for (int i = 0; i < hamiltonianPath.Count; i++)
                {
                    if (i > 0)
                    {
                        // highlight the edge between the current node and the previous node.
                        var edge = FindEdgeBetweenNodes(hamiltonianPath[i - 1], hamiltonianPath[i]);
                        if (edge != null)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                edge.MakeRed();
                                pictureBox1.Refresh();
                            });
                        }
                        richTextBox1.AppendText($" {hamiltonianPath[i - 1].Name} to {hamiltonianPath[i].Name}.\n");
                        await Task.Delay(500);
                    }

                    Nodes currentNode = hamiltonianPath[i];
                    this.Invoke((MethodInvoker)delegate
                    {
                        currentNode.MakeRed();
                        pictureBox1.Refresh();
                    });

                    await Task.Delay(500);
                }

                this.Invoke((MethodInvoker)delegate
                {
                    richTextBox1.AppendText("\n\nHamiltonian Path Successfully Found.\n\n");
                    richTextBox1.AppendText("The completed path that visits each node exactly once in this graph is as follows:\n");
                    foreach (var node in hamiltonianPath)
                    {
                        richTextBox1.AppendText($"{node.Name} -> ");
                    }
                    richTextBox1.AppendText("End.\n\n");
                    ApplyButtonStyle(StopButton, true);
                });
            }
            else
            {
                richTextBox1.AppendText("\nAfter thorough exploration, no Hamiltonian path was found in the current graph. This indicates that it's not possible to visit each node exactly once from this starting point.\n");
                ApplyButtonStyle(StopButton, true);
            }
        }


        private Edges FindEdgeBetweenNodes(Nodes node1, Nodes node2)
        {
            return edgeList.FirstOrDefault(e => (e.FirstPoint == node1 && e.SecondPoint == node2) || (e.FirstPoint == node2 && e.SecondPoint == node1));
        }

        private void RunDijkstra(Nodes startNode, Nodes endNode = null)
        {
            richTextBox1.Clear();
            ApplyButtonStyle(StopButton, false);

            foreach (var node in nodeList) node.Visited = false;
            foreach (var edge in edgeList) edge.Visited = false;

            visitedNodesHistory.Clear();
            visitedEdgesHistory.Clear();
            currentStep = -1;

            var startingNode = nodeList.FirstOrDefault(node => node.StartingPoint);
            if (startingNode != null)
            {
                Dijkstra(startingNode);
            }
        }

        private async void Dijkstra(Nodes startNode)
        {
            var priorityQueue = new PriorityQueue<Nodes, int>();
            var distances = new Dictionary<Nodes, int>();
            var previous = new Dictionary<Nodes, Nodes>();

            // initialize distances to infinity, and startNode's distance to 0
            foreach (var node in nodeList)
            {
                distances[node] = int.MaxValue;
                previous[node] = null;
                priorityQueue.Enqueue(node, int.MaxValue);
            }
            distances[startNode] = 0;
            priorityQueue.Enqueue(startNode, 0);

            startNode.MakeRed();
            richTextBox1.AppendText($"Starting at node {startNode.Name}...\n");

            Nodes endNode = nodeList.FirstOrDefault(node => node.EndingPoint);
            bool goalReached = false;

            while (priorityQueue.Count > 0 && !goalReached)
            {
                if (priorityQueue.TryDequeue(out Nodes currentNode, out int currentPriority))
                {
                    if (distances[currentNode] == int.MaxValue) break;

                    currentNode.MakeRed();
                    pictureBox1.Refresh();
                    richTextBox1.AppendText($"\nExploring node {currentNode.Name} with current distance {currentPriority}.\n");
                    await Task.Delay(500);

                    if (currentNode == endNode)
                    {
                        richTextBox1.AppendText("Goal node reached. Constructing shortest path...\n");
                        goalReached = true;
                        // break out of the loop if the goal node is reached
                        break;
                    }

                    var relevantEdges = edgeList.Where(edge =>
                        (edge.FirstPoint == currentNode && edge.IsDirected) || // directed edge going out from current
                        (!edge.IsDirected && (edge.FirstPoint == currentNode || edge.SecondPoint == currentNode))); // undirected edge

                    foreach (var edge in relevantEdges)
                    {
                        Nodes neighbor = edge.IsDirected ? edge.SecondPoint : (edge.FirstPoint == currentNode ? edge.SecondPoint : edge.FirstPoint);
                        if (!edge.IsDirected || edge.FirstPoint == currentNode)
                        {
                            int newDist = distances[currentNode] + edge.Weight;

                            if (newDist < distances[neighbor])
                            {
                                distances[neighbor] = newDist;
                                previous[neighbor] = currentNode;
                                priorityQueue.Enqueue(neighbor, newDist);

                                edge.MakeRed();
                                pictureBox1.Refresh();
                                richTextBox1.AppendText($"\tEdge to node {neighbor.Name}:\n" +
                                                         $"\t\tUpdated distance: {newDist}\n" +
                                                         $"\t\tvia edge {edge.Weight}\n");
                            }
                            await Task.Delay(500);
                        }
                    }
                }
                else
                {
                    richTextBox1.AppendText("Error in dequeuing from the priority queue.\n");
                    break;
                }
            }

            if (goalReached)
            {
                HighlightShortestPath(startNode, endNode, previous);
            }

            richTextBox1.AppendText("\nDijkstra's algorithm has completed.\n");
        }

        private void HighlightShortestPath(Nodes startNode, Nodes endNode, Dictionary<Nodes, Nodes> previous)
        {
            var path = new List<Nodes>();
            var currentNode = endNode;
            foreach (var node in nodeList)
            {
                node.ResetColor();
            }
            foreach (var edge in edgeList)
            {
                edge.ResetColor();
            }

            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = previous[currentNode];
            }
            path.Reverse();
            foreach (var node in path)
            {
                node.MakeRed();
            }

            for (int i = 0; i < path.Count - 1; i++)
            {
                Nodes u = path[i];
                Nodes v = path[i + 1];

                Edges edge = edgeList.FirstOrDefault(e =>
                    (e.FirstPoint == u && e.SecondPoint == v && e.IsDirected) ||
                    (e.FirstPoint == v && e.SecondPoint == u && !e.IsDirected) ||
                    (e.FirstPoint == u && e.SecondPoint == v && !e.IsDirected));

                if (edge != null)
                {
                    edge.MakeRed();
                }
            }
            ApplyButtonStyle(StopButton, true);
            pictureBox1.Refresh();
        }


        private void RunBoruvka()
        {

            richTextBox1.Clear();

            if (NextEvent != null) NextButton.Click -= NextEvent;
            NextEvent = new EventHandler(BoruvkaStep);
            NextButton.Click += NextEvent;

            PrevButton.Click -= PrevEvent;
            PrevEvent = null;

            InitializeBoruvka();
        }

        private void InitializeBoruvka()
        {
            mstEdges.Clear();
            int numberOfComponents = nodeList.Count;
            List<Nodes> componentRepresentatives = new List<Nodes>(nodeList);
            List<List<Nodes>> connectedComponents = new List<List<Nodes>>();
            nodeList.ForEach(node => connectedComponents.Add(new List<Nodes> { node }));

            lightestEdges = new List<Edges>(new Edges[numberOfComponents]);
            this.connectedComponents = connectedComponents;
            this.componentRepresentatives = componentRepresentatives;
            this.numberOfComponents = numberOfComponents;

            richTextBox1.AppendText("Initialization complete. Each node is its own component.\n");
            pictureBox1.Refresh();
        }

        private List<Edges> lightestEdges = new List<Edges>();
        private List<List<Nodes>> connectedComponents = new List<List<Nodes>>();
        private List<Nodes> componentRepresentatives = new List<Nodes>();
        private int numberOfComponents;
        private List<Edges> mstEdges = new List<Edges>();

        private void BoruvkaStep(object sender, EventArgs e)
        {
            bool progress = false;
            List<string> newConnections = new List<string>();
            newConnections.Clear();

            // clear previous edges
            foreach (var edge in lightestEdges)
            {
                if (edge != null) edge.ResetColor();
            }

            richTextBox1.AppendText("\n--- Next step ---\n");

            richTextBox1.AppendText($"Inspecting component \n");

            // find the lightest outgoing edge for each connected component
            for (int i = 0; i < connectedComponents.Count; i++)
            {
                var component = connectedComponents[i];
                Edges lightestEdge = null;

                richTextBox1.AppendText($" {i + 1},");

                foreach (var node in component)
                {
                    foreach (var edge in edgeList.Where(e => (e.FirstPoint == node || e.SecondPoint == node) && !component.Contains(e.FirstPoint == node ? e.SecondPoint : e.FirstPoint)))
                    {
                        if (lightestEdge == null || edge.Weight < lightestEdge.Weight)
                        {
                            lightestEdge = edge;
                        }
                    }
                }

                if (lightestEdge != null)
                {
                    Debug.WriteLine($"Selected lightest edge for component {i + 1}: {lightestEdge.FirstPoint.Name}-{lightestEdge.SecondPoint.Name}");
                    // directly add the lightest edge to the MST if it's not already there and if it connects different components
                    if (!mstEdges.Contains(lightestEdge))
                    {
                        lightestEdges[i] = lightestEdge;
                        Nodes u = lightestEdge.FirstPoint;
                        Nodes v = lightestEdge.SecondPoint;
                        int componentUIndex = connectedComponents.FindIndex(c => c.Contains(u));
                        int componentVIndex = connectedComponents.FindIndex(c => c.Contains(v));

                        if (componentUIndex != componentVIndex)
                        {
                            mstEdges.Add(lightestEdge);
                            newConnections.Add($"{u.Name}-{v.Name}");
                            lightestEdge.MakeRed();
                        }
                    }
                }
            }

            // merge components using the lightest edges
            for (int i = 0; i < connectedComponents.Count; i++)
            {
                var edge = lightestEdges[i];
                if (edge != null)
                {
                    Nodes u = edge.FirstPoint;
                    Nodes v = edge.SecondPoint;
                    int componentUIndex = connectedComponents.FindIndex(c => c.Contains(u));
                    int componentVIndex = connectedComponents.FindIndex(c => c.Contains(v));

                    if (componentUIndex != componentVIndex)
                    {
                        var componentUList = connectedComponents[componentUIndex];
                        var componentVList = connectedComponents[componentVIndex];

                        componentUList.AddRange(componentVList);
                        connectedComponents[componentUIndex] = componentUList;
                        connectedComponents.RemoveAt(componentVIndex);

                        numberOfComponents--;

                        componentRepresentatives[componentUIndex] = componentUList[0];
                        componentRepresentatives.RemoveAt(componentVIndex);

                        if (!mstEdges.Contains(edge))
                        {
                            mstEdges.Add(edge);
                            Debug.WriteLine($"Adding new connection: {u.Name}-{v.Name}");
                            newConnections.Add($"{u.Name}-{v.Name}");
                        }
                        progress = true;
                    }
                }
            }
            // debug check
            //foreach (var redEdge in lightestEdges.Where(e => e != null))
            //{
            //    string edgeString = $"{redEdge.FirstPoint.Name}-{redEdge.SecondPoint.Name}";
            //    if (!newConnections.Contains(edgeString))
            //    {
            //        richTextBox1.AppendText($"Red edge not logged: {edgeString}\n");
            //    }
            //}
            pictureBox1.Refresh();

            if (newConnections.Any())
            {
                richTextBox1.AppendText($"\nNew connections added: {string.Join(", ", newConnections)}\n");
            }

            if (!progress || numberOfComponents <= 1)
            {
                richTextBox1.AppendText("\nMST construction complete. The minimum spanning tree includes the following edges:\n");

                // clear previous edge colors for MST visualization
                foreach (var edge in edgeList)
                {
                    edge.ResetColor();
                }

                // add lightest edges to the final MST list and display them
                foreach (var edge in lightestEdges)
                {
                    if (edge != null)
                    {
                        if (mstEdges.Contains(edge) == false)
                        {
                            mstEdges.Add(edge);
                        }
                        richTextBox1.AppendText($"Edge {edge.FirstPoint.Name}-{edge.SecondPoint.Name} with weight {edge.Weight}\n");
                    }
                }

                DisplayFinalMST();
                NextButton.Enabled = false;
                pictureBox1.Refresh();
            }
        }
        private void DisplayFinalMST()
        {
            foreach (var edge in mstEdges)
            {
                edge.MakeRed();
            }

            List<string> mstConnections = mstEdges.Select(edge => $"{edge.FirstPoint.Name}-{edge.SecondPoint.Name}").ToList();

            richTextBox1.AppendText("\nThe final Minimum Spanning Tree (MST) has been constructed. The connections are:\n");
            richTextBox1.AppendText($"{string.Join(" ", mstConnections)}\n");

            richTextBox1.AppendText("\nThese connections represent the most efficient way to link all points with the minimal total distance. " +
                                    "The MST ensures that there are no loops and that each point is reachable from every other point in the network.\n");
            pictureBox1.Refresh();
        }

        private bool HasCycles()
        {
            HashSet<Nodes> visited = new HashSet<Nodes>();
            HashSet<Nodes> recursionStack = new HashSet<Nodes>();

            bool HasCyclesUtil(Nodes node)
            {
                if (recursionStack.Contains(node))
                    return true;

                if (visited.Contains(node))
                    return false;

                visited.Add(node);
                recursionStack.Add(node);

                foreach (var edge in edgeList.Where(e => e.FirstPoint == node && e.IsDirected))
                {
                    if (HasCyclesUtil(edge.SecondPoint))
                        return true;
                }

                recursionStack.Remove(node);
                return false;
            }

            foreach (var node in nodeList)
            {
                if (HasCyclesUtil(node))
                    return true;
            }

            return false;
        }

        private bool ContainsUndirectedEdges()
        {
            return edgeList.Any(edge => !edge.IsDirected);
        }

        private List<int> topologicalOrder;
        private Stack<Nodes> topologicalStack;
        private Dictionary<Nodes, int> indegreeMap;
        private void runTopologicalSort()
        {
            NextButton.Click -= NextEvent;
            NextEvent = new EventHandler(TopologicalSortMoveForward);
            NextButton.Click += NextEvent;

            PrevButton.Click -= PrevEvent;

            topologicalOrder = new List<int>();
            topologicalStack = new Stack<Nodes>();
            indegreeMap = new Dictionary<Nodes, int>();

            foreach (var node in nodeList)
            {
                indegreeMap[node] = 0;  // initialize all indegrees to zero
            }

            foreach (var edge in edgeList.Where(edge => edge.IsDirected))
            {
                indegreeMap[edge.SecondPoint]++;
            }

            foreach (var node in nodeList.Where(node => indegreeMap[node] == 0))
            {
                if (node.StartingPoint)
                {
                    topologicalStack.Push(node);
                    break; // prioritize the starting point
                }
            }

            foreach (var node in nodeList.Where(node => indegreeMap[node] == 0))
            {
                topologicalStack.Push(node);
            }

            richTextBox1.Clear();
            richTextBox1.AppendText("Topological sort ready. Press 'Forward' to proceed.\n");
        }
        private void TopologicalSortMoveForward(object sender, EventArgs e)
        {
            if (topologicalStack.Count > 0)
            {
                var currentNode = topologicalStack.Pop();
                topologicalOrder.Add(Convert.ToInt32(currentNode.Name));
                currentNode.MakeRed();

                richTextBox1.AppendText($"[Step {topologicalOrder.Count}]\n");
                richTextBox1.AppendText($"Now processing Node {currentNode.Name}, which has no remaining prerequisites.\n");

                foreach (var edge in edgeList.Where(x => x.FirstPoint == currentNode && x.IsDirected))
                {
                    indegreeMap[edge.SecondPoint]--;
                    richTextBox1.AppendText($"- Node {currentNode.Name} leads to Node {edge.SecondPoint.Name}. ");
                    richTextBox1.AppendText($"We've completed a prerequisite for Node {edge.SecondPoint.Name}, so we reduce its required count to {indegreeMap[edge.SecondPoint]}.\n");

                    if (indegreeMap[edge.SecondPoint] == 0)
                    {
                        richTextBox1.AppendText($"-- Node {edge.SecondPoint.Name} now has all prerequisites met and will be processed next.\n");
                        topologicalStack.Push(edge.SecondPoint);
                    }
                }

                richTextBox1.AppendText("\n");
                pictureBox1.Refresh();

                if (topologicalStack.Count == 0)
                {
                    richTextBox1.AppendText("All nodes have been processed. The topological sort is complete!\n");
                    richTextBox1.AppendText("The order in which tasks can be completed is: " + string.Join(" ➔ ", topologicalOrder.Select(x => nodeList[x - 1].Name)) + "\n\n");
                    NextButton.Enabled = false;
                    pictureBox1.Refresh();
                }
            }
            else
            {
                richTextBox1.AppendText("No more nodes to process. If there are unprocessed nodes, please check for cycles or disconnected components.\n");
                pictureBox1.Refresh();
            }
        }





        private void ExplainAlgoButton_Click(object sender, EventArgs e) //explains what the current algorithm is 
        {
            if (comboBox1.SelectedItem == null)
            {
                return;
            }
            string selectedAlgorithm = comboBox1.SelectedItem.ToString();
            switch (selectedAlgorithm)
            {
                case "Depth-First Search":
                    richTextBox1.Clear();
                    richTextBox1.Text = "Depth First Search (DFS) is a recursive algorithm that uses the idea of backtracking. " +
                    "It explores a graph or tree by going deep into branching before backtracking. This means that in a given path, " +
                    "DFS will keep going forward until it reaches the end, and then it will go back and try the next path. " +
                    "DFS can be implemented using a stack data structure where the next node to visit is taken from the top of the stack. " +
                    "It's important to mark each node as visited to prevent revisiting and getting caught in cycles.\n\n" +
                    "The algorithm works as follows: start at a root node, push it onto the stack, and mark it as visited. Then, " +
                    "continue popping nodes from the stack and pushing all unvisited adjacent nodes back onto the stack. " +
                    "DFS is complete when the stack is empty. The process ensures all nodes are visited and can be used for various applications " +
                    "like finding connected components and solving puzzles.\n\n" +
                    "In practical terms, think of DFS as exploring a maze by taking one path as far as you can go before trying another. " +
                    "It's a methodical approach that can be applied to many real-world situations, from game development to network analysis.";
                    break;
                case "Breadth-First Search":
                    richTextBox1.Clear();
                    richTextBox1.Text = "Breadth First Search (BFS) is an algorithm that systematically explores a graph's nodes, layer by layer. " +
                    "Starting from the source node, it visits all neighboring nodes, then moves to the next level of nodes, and continues until all " +
                    "nodes are visited. This method is like a ripple spreading across the nodes, reaching out as far as possible from the starting point " +
                    "before moving deeper into the graph.\n\n" +
                    "BFS is efficient for finding the shortest path in unweighted graphs, with applications ranging from puzzle solving to network routing. " +
                    "In practical applications, BFS can help ensure " +
                    "all possibilities are considered without redundancy, organizing the exploration of paths or connections in a clear and logical manner.";
                    break;
                case "Hamiltonian route":
                    richTextBox1.Clear();
                    richTextBox1.Text = "A Hamiltonian route, or Hamiltonian path, is a sequence of edges in a graph " +
                    "that visits each vertex exactly once. If this route can start and end at the same vertex, " +
                    "forming a loop, it becomes a Hamiltonian cycle. The challenge of determining whether such a " +
                    "path or cycle exists in a graph is known to be NP-complete. \n\n" +
                    "The concept stems from a puzzle game invented in 1857 by Sir William Rowan Hamilton called " +
                    "the Icosian Game, involving a dodecahedron-shaped graph. Practical applications of " +
                    "Hamiltonian paths are found in operational research, genome mapping, and computer graphics.\n\n" +
                    "Theorems like Dirac's and Ore's provide conditions under which a Hamiltonian cycle is likely " +
                    "to exist. Dirac's theorem requires every vertex to have a degree of at least half the total " +
                    "number of vertices for a cycle to exist, while Ore's theorem relates to the sum of degrees " +
                    "of non-adjacent vertices. The closure of a graph becoming Hamiltonian is another indicator " +
                    "of a Hamiltonian cycle.\n\n" +
                    "In essence, a Hamiltonian path is like planning a trip to visit several places, ensuring you " +
                    "visit each place only once, and if you're able to return to your starting point on the same " +
                    "path, then you've found a Hamiltonian cycle. It's a key concept for efficient travel and " +
                    "scientific discovery.";
                    break;
                case "Dijkstra":
                    richTextBox1.Clear();
                    richTextBox1.Text = "Dijkstra's algorithm is an efficient way to find the shortest path from a start point " +
                    "to all other points in a weighted graph.\nIt works by marking the distance to the start node as zero and " +
                    "all others as infinity. It then uses a priority queue to visit each node, updating the distance for each " +
                    "neighboring node if a shorter path is found. This continues until the shortest distances to all nodes are " +
                    "determined. It’s crucial that all edges have non-negative weights for the algorithm to work correctly.\n\n " +
                    "To visualize how the algorithm works, imagine you are trying to find the shortest path in a city from your " +
                    "current location to a particular destination. Each intersection represents a node, " +
                    "and each street represents an edge with a weight equal to the street's length. Starting at your location, you evaluate each intersection you can reach directly, updating your map with the " +
                    "shortest distance to each intersection. You then pick the closest intersection and repeat the process for its neighbors until you have determined the shortest path to your destination.\n\n" +
                    "The effectiveness of Dijkstra's algorithm in these scenarios shows how it can be a powerful tool for routing and pathfinding in various real-world and computational applications. ";
                    break;
                case "Boruvka":
                    richTextBox1.Clear();
                    richTextBox1.Text = "Borůvka's algorithm, also known as Borůvka's Minimum Spanning Tree algorithm, is designed to find the minimum spanning tree (MST) of a graph. " +
                    "This algorithm is particularly effective for graphs that are sparse but can work with any graph. It operates in rounds, with each round consisting of several steps that " +
                    "progressively reduce the graph by selecting the cheapest edge from each component and adding it to the MST. These components are then merged to form larger components. " +
                    "\n\nThe algorithm starts with each vertex as a separate component and, in each iteration, connects components with the least expensive edge that has exactly one endpoint in " +
                    "each component. The process repeats until all vertices are connected, resulting in the minimum spanning tree. Borůvka's algorithm is efficient and deterministic, " +
                    "with a time complexity generally performing well in practical applications, particularly due to its logarithmic number of rounds relative to the number of vertices. " +
                    "\n\nBorůvka's algorithm is not only foundational in the field of graph algorithms but also serves as a base for more complex algorithms like the ones used in parallel computing environments. " +
                    "It illustrates a systematic approach to constructing a minimum spanning tree by focusing on local minima and progressively building towards a global solution.";
                    break;
                case "Topological Sort":
                    richTextBox1.Clear();
                    richTextBox1.Text = "Topological Sort is a linear ordering of a directed graph's vertices such that for every directed edge UV from vertex U to vertex V, U comes before V in the ordering. " +
                    "This algorithm is crucial for scheduling tasks, where some tasks must be performed before others. It can be implemented using depth-first search or through a queue-based approach. " +
                    "\n\nThe algorithm starts by identifying vertices with no incoming edges (i.e., no prerequisites) and iteratively adds them to the sorted order while removing outgoing edges. " +
                    "Each removal might create new nodes with no incoming edges, which are then added to the order. The process repeats until all vertices are ordered or the graph is determined to have a cycle (in which case, a topological sort is impossible). " +
                    "\n\nIn practical terms, think of it as organizing a set of tasks where some tasks depend on the completion of others. The topological sort provides a clear sequence to follow. ";
                    break;
                case null:
                    break;

            }
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.AppendText(
                "Graph Creation & Visualization Tutorial:\n\n" +
                " Add nodes: Click 'Add Node Button' to place new nodes onto the graph.\n\n" +
                " Create edges: Toggle 'Add Edge' or 'Add directed edge' buttons to start drawing edges by clicking and dragging between nodes.\n\n" +
                " Add weights: If needed, click the 'Add weight' button, then click on a desired edge.\n\n" +
                " Set start/end: Click the 'Select starting (or ending) point' button before clicking the node you wish to start (or end) at.\n\n" +
                " Choose algorithm: Select an algorithm from the dropdown menu. \n" +
                "Press the '?' button next to the dropdown menu for an introduction to the currently selected algorithm.\n\n" +
                " Visualize: Hit the 'Start' button to see the algorithm in action. \n" +
                "Press the 'Forward' and 'Backward' buttons to see the steps. Click the 'Stop' button to cancel the algorithm.\n\n" +
                " Reset Graph: Use the 'Reset' button to clear the graph and start anew.\n\n" +
                " Save/Load Graph: Use the 'Select folder' button to choose the location for saving or loading, then \n" +
                "use the 'Save current graph' and 'Load graph' buttons to manage your graph's state."
            );
        }
        XmlSerializer edgeserializer = new XmlSerializer(typeof(List<Edges>));
        XmlSerializer nodeserializer = new XmlSerializer(typeof(List<Nodes>));

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (nodeList.Count > 0)
            {
                // ask the user for a name
                string? name = null;
                GetNameFromUser getnameForm = new GetNameFromUser();
                if (getnameForm.ShowDialog(this) == DialogResult.OK)
                {
                    name = getnameForm.name;
                    Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);
                    pictureBox1.Refresh();

                    string edgesPath = Path.Combine(selectedFolder, $"{name} edges.xml");
                    string bmpPath = Path.Combine(selectedFolder, $"{name}.bmp");
                    string nodesPath = Path.Combine(selectedFolder, $"{name} nodes.xml");

                    bmp.Save(bmpPath, System.Drawing.Imaging.ImageFormat.Bmp);

                    using (FileStream stream = File.Create(edgesPath))
                    {
                        edgeserializer.Serialize(stream, edgeList);
                    }

                    using (FileStream stream = File.Create(nodesPath))
                    {
                        nodeserializer.Serialize(stream, nodeList);
                    }
                }
                if (name == null)
                {
                    MessageBox.Show("Please add a name.");
                }
            }
            else
            {
                MessageBox.Show("No data to save.");
            }
        }

        private void SelectStartButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            editStartpoint = !editStartpoint;

            ApplyButtonStyle(AddNodeButton, !editStartpoint);
            ApplyButtonStyle(AddEdgeButton, !editStartpoint);
            ApplyButtonStyle(AddWeightButton, !editStartpoint);
            ApplyButtonStyle(AddDirectedEdgeButton, !editStartpoint);
            ApplyButtonStyle(ResetButton, !editStartpoint);
            ApplyButtonStyle(StartButton, !editStartpoint);
            ApplyButtonStyle(SelectEndPointButton, !editStartpoint);
            ApplyButtonStyle(DeleteButton, !editStartpoint);
        }

        Nodes currentStartpoint;
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (deleteMode)
            {
                var clickPoint = e.Location;
                bool isNodeDeleted = false;


                foreach (Nodes node in nodeList.ToList()) //ToList() to avoid modification issues
                {
                    if (IsMouseOverNode(clickPoint, node))
                    {
                        // remove connected edges
                        edgeList.RemoveAll(edge => edge.FirstPoint == node || edge.SecondPoint == node);
                        // remove the node itself
                        nodeList.Remove(node);
                        isNodeDeleted = true;
                        // if the node was a start/end point clear the variables and assign a new start node
                        if (node.StartingPoint && currentStartpoint == node)
                        {
                            currentStartpoint = nodeList.FirstOrDefault(n => !n.EndingPoint);
                            if (currentStartpoint != null)
                            {
                                currentStartpoint.StartingPoint = true;
                            }
                        }
                        if (node.EndingPoint && currentEndPoint == node)
                        {
                            currentEndPoint = null;
                        }


                        break; //exiting to make sure only one node is deleted
                    }
                    if (!isNodeDeleted) // if it wasnt a node then check if the click is on an edge
                    {
                        Edges clickedEdge = findClickedEdge(clickPoint);
                        if (clickedEdge != null)
                        {
                            edgeList.Remove(clickedEdge);
                        }
                    }
                }

                pictureBox1.Refresh();
            }
            if (editStartpoint)
            {
                foreach (Nodes item in nodeList)
                {
                    if (IsMouseOverNode(e.Location, item))
                    {
                        if (currentStartpoint != null)
                        {
                            currentStartpoint.StartingPoint = false;
                        }
                        if (currentEndPoint == item)
                        {
                            currentEndPoint.EndingPoint = false;
                            currentEndPoint = null;
                        }

                        item.StartingPoint = true;
                        currentStartpoint = item;
                    }
                }
            }
            if (weightEditMode)
            {
                var clickpoint = new Point(e.X, e.Y);
                Edges closestEdge = findClickedEdge(clickpoint);
                if (closestEdge != null)
                {
                    string input = Interaction.InputBox("Add weight", "Enter weight: ", "");
                    if (int.TryParse(input, out int weight) && weight > 0)
                    {
                        closestEdge.Weight = weight;
                        pictureBox1.Refresh();
                    }
                }
            }
            if (editEndpoint)
            {
                foreach (Nodes item in nodeList)
                {
                    if (IsMouseOverNode(e.Location, item))
                    {
                        if (currentStartpoint == item)
                        {
                            break;
                        }
                        if (currentEndPoint != null)
                        {
                            currentEndPoint.EndingPoint = false;
                        }
                        item.EndingPoint = true;
                        currentEndPoint = item;
                    }
                }
            }
            pictureBox1.Refresh();
        }
        private Edges findClickedEdge(Point clickPoint)
        {
            const float radius = 15f;

            foreach (Edges edge in edgeList)
            {
                // check if click is inside the circle
                if (Math.Sqrt(Math.Pow(clickPoint.X - edge.MidPoint.X, 2) + Math.Pow(clickPoint.Y - edge.MidPoint.Y, 2)) <= radius)
                {
                    return edge;
                }
            }

            return null; // No edge was clicked
        }

        // pictureboxes for the load menu
        private List<PictureBox> PictureBoxes = new List<PictureBox>();


        private void LoadButton_Click(object sender, EventArgs e)
        {
            List<string> filenames = new List<string>();
            string[] patterns = { "*.bmp" };
            if (selectedFolder != null)
            {
                foreach (string pattern in patterns)
                {
                    filenames.AddRange(Directory.GetFiles(selectedFolder, pattern, SearchOption.TopDirectoryOnly));
                }
            }

            filenames.Sort();
            LoadMenu loadList = new LoadMenu();

            foreach (string filename in filenames)
            {
                // assigning every graph bmp to a picturebox
                PictureBox pic = new PictureBox();
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.ClientSize = new Size(thumbWidth, thumbHeight);
                pic.Image = new Bitmap(filename);

                // tooltip
                FileInfo file_info = new FileInfo(filename);
                ToolTip graphInfo = new ToolTip();
                graphInfo.SetToolTip(pic, file_info.Name + "\nCreated: " + file_info.CreationTime.ToShortDateString());
                pic.Tag = file_info;

                string loadname = file_info.Name.Remove(file_info.Name.Length - 4);
                pic.Click += new EventHandler(PictureBox_Click);
                pic.Name = loadname;

                loadList.graphThumbnails.Controls.Add(pic);
                // add the pictureboxes to the flowlayoutpanel
                pic.Parent = loadList.graphThumbnails;
            }
            loadList.ShowDialog();
        }

        private void PictureBox_Click(object? sender, EventArgs e)
        {
            PictureBox Sender = (PictureBox)sender;
            string loadname = Sender.Name;
            MessageBox.Show(loadname);
            string edgesPath = Path.Combine(selectedFolder, $"{loadname} edges.xml");
            string nodesPath = Path.Combine(selectedFolder, $"{loadname} nodes.xml");

            edgeList.Clear();
            nodeList.Clear();
            using (FileStream stream = File.OpenRead(nodesPath))
            {
                nodeList = (List<Nodes>)nodeserializer.Deserialize(stream);
                foreach (Nodes item in nodeList)
                {
                    if (item.StartingPoint)
                    {
                        currentStartpoint = item;
                    }
                    if (item.EndingPoint)
                    {
                        currentEndPoint = item;
                    }
                }
            }
            using (FileStream stream = File.OpenRead(edgesPath))
            {
                edgeList = (List<Edges>)edgeserializer.Deserialize(stream);
                foreach (Edges item in edgeList)
                {
                    item.FirstPoint = nodeList[Convert.ToInt32(item.FirstPoint.Name.ToString()) - 1];
                    item.SecondPoint = nodeList[Convert.ToInt32(item.SecondPoint.Name.ToString()) - 1];
                    EdgeCount++;
                }
            }
            pictureBox1.Refresh();
            Sender.FindForm().Hide();
        }

        string selectedFolder;
        private void selectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog chooseFolder = new FolderBrowserDialog();
            if (chooseFolder.ShowDialog() == DialogResult.OK)
            {
                selectedFolder = chooseFolder.SelectedPath;
                ApplyButtonStyle(SaveButton, true);
                ApplyButtonStyle(LoadButton, true);
            }
        }

        private void StopButton_Click(object sender, EventArgs e) //reset colors, reset .visited, reset stacks
        {
            // reset all nodes and edges to their initial state
            foreach (var node in nodeList)
            {
                node.ResetColor();
                node.Visited = false;
            }
            foreach (var edge in edgeList)
            {
                edge.ResetColor();
            }
            pictureBox1.Refresh();

            // clear history and reset steps
            visitedNodesHistory.Clear();
            visitedEdgesHistory.Clear();
            currentStep = -1;
            ForwardStack.Clear();
            BackwardStack.Clear();

            bfsQueue.Clear();
            visitedBFS.Clear();

            // reset button states
            ApplyButtonStyle(PrevButton, false);
            ApplyButtonStyle(NextButton, false);
            ApplyButtonStyle(StartButton, true);
            ApplyButtonStyle(StopButton, false);

            richTextBox1.Clear();
            pictureBox1.Refresh();

        }
        bool directedEditMode = false;
        private void AddDirectedEdgeButton_Click(object sender, EventArgs e)
        {
            directedEditMode = !directedEditMode;

            // show that the other buttons are locked
            ApplyButtonStyle(AddNodeButton, !directedEditMode);
            ApplyButtonStyle(ResetButton, !directedEditMode);
            ApplyButtonStyle(SelectStartButton, !directedEditMode);
            ApplyButtonStyle(AddEdgeButton, !directedEditMode);
            ApplyButtonStyle(AddWeightButton, !directedEditMode);
            ApplyButtonStyle(StartButton, !directedEditMode);
            ApplyButtonStyle(SelectEndPointButton, !directedEditMode);
            ApplyButtonStyle(DeleteButton, !directedEditMode);
        }

        bool weightEditMode = false;
        private void AddWeightButton_Click(object sender, EventArgs e)
        {
            weightEditMode = !weightEditMode;

            // show that the other buttons are locked
            ApplyButtonStyle(AddNodeButton, !weightEditMode);
            ApplyButtonStyle(ResetButton, !weightEditMode);
            ApplyButtonStyle(SelectStartButton, !weightEditMode);
            ApplyButtonStyle(AddEdgeButton, !weightEditMode);
            ApplyButtonStyle(StartButton, !weightEditMode);
            ApplyButtonStyle(AddDirectedEdgeButton, !weightEditMode);
            ApplyButtonStyle(SelectEndPointButton, !weightEditMode);
            ApplyButtonStyle(DeleteButton, !weightEditMode);

            pictureBox1.Refresh();
        }

        private void SelectEndPointButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            editEndpoint = !editEndpoint;

            // show that the other buttons are locked
            ApplyButtonStyle(AddNodeButton, !editEndpoint);
            ApplyButtonStyle(AddEdgeButton, !editEndpoint);
            ApplyButtonStyle(AddWeightButton, !editEndpoint);
            ApplyButtonStyle(AddDirectedEdgeButton, !editEndpoint);
            ApplyButtonStyle(ResetButton, !editEndpoint);
            ApplyButtonStyle(StartButton, !editEndpoint);
            ApplyButtonStyle(SelectStartButton, !editEndpoint);
            ApplyButtonStyle(DeleteButton, !editEndpoint);

        }

        private void ApplyButtonStyle(Button button, bool isEnabled)
        {
            Color backColor = isEnabled ? Color.White : Color.Gray;

            button.BackColor = backColor;
            button.Enabled = isEnabled;
        }
        bool deleteMode = false;
        private void DeleteButton_Click(object sender, EventArgs e)
        {

            deleteMode = !deleteMode;
            pictureBox1.Refresh();

            // show that the other buttons are locked
            ApplyButtonStyle(AddNodeButton, !deleteMode);
            ApplyButtonStyle(AddEdgeButton, !deleteMode);
            ApplyButtonStyle(AddWeightButton, !deleteMode);
            ApplyButtonStyle(AddDirectedEdgeButton, !deleteMode);
            ApplyButtonStyle(ResetButton, !deleteMode);
            ApplyButtonStyle(StartButton, !deleteMode);
            ApplyButtonStyle(SelectStartButton, !deleteMode);
            ApplyButtonStyle(SelectEndPointButton, !deleteMode);
        }
        Random weightrnd = new Random();
        private void RandomizeWeightsButton_Click(object sender, EventArgs e)
        {
            RandomWeights randomWeightsForm = new RandomWeights();

            if (randomWeightsForm.ShowDialog() == DialogResult.OK)
            {
                int minWeight = randomWeightsForm.MinWeight;
                int maxWeight = randomWeightsForm.MaxWeight;

                foreach (Edges item in edgeList)
                {
                    item.Weight = weightrnd.Next(minWeight, maxWeight + 1);
                }

                pictureBox1.Refresh();

            }
        }
    }
}