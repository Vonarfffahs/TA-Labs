using lab2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace lab2
{
     enum Name
    {
        KPI = 0,
        RedBodyKNU = 1,
        GoldenGate = 2,
        SophiaCathedral = 3,
        AndrewsChurch = 4,
        MuseumOfOneStreet = 5,
        KyivFunicular = 6,
        NationalPhilharmonicOfUkraine = 7,
        MichaelsMonastery = 8,
        LyadskyGate = 9,
        FountainOnKhreshchatyk = 10
    }
    public partial class FormGraph : Form
    {
        private List<PointF> vertices = new List<PointF>(); // Список координат точок
        private List<Tuple<int, int>> edges = new List<Tuple<int, int>>(); // Список ребер графа
        static List<(int node, float weight)>[] adjacencyList = new List<(int node, float weight)>[]
            {
                new List<(int node, float weight)> { (node: 1, weight: 4.60f) },
                new List<(int node, float weight)> { (node: 0, weight: 4.60f),(node: 2, weight: 0.90f) },
                new List<(int node, float weight)> { (node: 1, weight: 0.90f),(node: 3, weight: 0.60f),(node: 10, weight: 1.00f) },
                new List<(int node, float weight)> { (node: 2, weight: 0.60f),(node: 4, weight: 0.70f),(node: 9, weight: 0.65f), (node: 8, weight: 0.50f) },
                new List<(int node, float weight)> { (node: 3, weight: 0.70f),(node: 5, weight: 0.60f),(node: 8, weight: 0.60f) },
                new List<(int node, float weight)> { (node: 4, weight: 0.60f),(node: 6, weight: 0.80f) },
                new List<(int node, float weight)> { (node: 5, weight: 0.80f),(node: 7, weight: 0.75f),(node: 8, weight: 0.35f) /*,(node: 8, weight: -1f), (node: 7, weight: -1f)*/   },
                new List<(int node, float weight)> { (node: 6, weight: 0.75f), (node: 9, weight: 1.00f) /*,(node: 6, weight: -1f), (node: 8, weight: -1f)*/  },
                new List<(int node, float weight)> { (node: 3, weight: 0.50f),(node: 6, weight: 0.35f), (node: 4, weight: 0.60f), (node: 9, weight: 0.50f) /*,(node: 6, weight: -1f), (node: 7, weight: -1f)*/ },
                new List<(int node, float weight)> { (node: 8, weight: 0.50f),(node: 10, weight: 1.10f), (node: 7, weight: 1.00f), (node: 3, weight: 0.65f) },
                new List<(int node, float weight)> { (node: 9, weight: 1.10f),(node: 2, weight: 1.00f) },
            };// Список суміжності де рядок - це визначні місця, стовпець - це місце до якого є сполучення та відстань до цього сполучення в кілометрах.
        static int sourceNode = 0;// Вихідна точкаа

        public FormGraph()
        {
            InitializeComponent();
            // Координати точок і ребра графа
            vertices.Add(new PointF(15, 450));//0
            vertices.Add(new PointF(213, 504));//1
            vertices.Add(new PointF(245, 350));//2
            vertices.Add(new PointF(259, 258));//3
            vertices.Add(new PointF(311, 118));//4
            vertices.Add(new PointF(303, 47));//5
            vertices.Add(new PointF(395, 133));//6
            vertices.Add(new PointF(460, 241));//7
            vertices.Add(new PointF(386, 191));//8
            vertices.Add(new PointF(384, 302));//9
            vertices.Add(new PointF(369, 447));//10
            edges.Add(Tuple.Create(0, 1));
            edges.Add(Tuple.Create(1, 2));
            edges.Add(Tuple.Create(2, 3));
            edges.Add(Tuple.Create(3, 4));
            edges.Add(Tuple.Create(4, 5));
            edges.Add(Tuple.Create(6, 7));
            edges.Add(Tuple.Create(6, 8));
            edges.Add(Tuple.Create(9, 10));
            edges.Add(Tuple.Create(2, 10));
            edges.Add(Tuple.Create(3, 9));
            edges.Add(Tuple.Create(4, 8));
            edges.Add(Tuple.Create(5, 6));
            edges.Add(Tuple.Create(8, 3));
            edges.Add(Tuple.Create(9, 8));
            edges.Add(Tuple.Create(7, 9));
            pictureBox1.Paint += new PaintEventHandler(pictureBox_Paint);
            StringBuilder inLabel = new StringBuilder();
            for (int i = 0; i < 11; i++)
            {
                inLabel.AppendLine($"({i + 1}){(Name)i}");
            }
            label2.Text = inLabel.ToString();
        }

        private void pictureBox_Paint(object sender, EventArgs e, float[] shortestDistances, Brush color )
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            Font boltFont = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold);
            for (int i = 1; i <= vertices.Count; i++)
            {
                graphics.DrawString(Math.Round(shortestDistances[i-1], 2, MidpointRounding.AwayFromZero).ToString(), boltFont, color, vertices[i - 1].X + 8, vertices[i - 1].Y - 25);
            } 
        }
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(Color.Black, 8);

            // Малюємо ребра графа
            foreach (var edge in edges)
            {
                var point1 = vertices[edge.Item1];
                var point2 = vertices[edge.Item2];
                e.Graphics.DrawLine(pen, point1, point2);
            }

            // Малюємо вершини графа
            Font boltFont = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold);
            
            for (int i = 1; i <= vertices.Count; i++)
            {
                e.Graphics.DrawString(i.ToString(), boltFont, Brushes.Red, vertices[i - 1].X - 15, vertices[i - 1].Y - 30);
                e.Graphics.FillEllipse(Brushes.Blue, vertices[i - 1].X - 10, vertices[i - 1].Y - 10, 20, 20);
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            switch (algorithmSelectionBox.SelectedIndex)
            {
                case 0:
                    {
                        Stopwatch clock = new Stopwatch();
                        clock.Start();
                        DijkstraAlgorithm dijkstraAlgorithm = new DijkstraAlgorithm(adjacencyList, sourceNode);
                        dijkstraAlgorithm.Run();
                        clock.Stop();
                        float[] shortestDistances = dijkstraAlgorithm.GetShortestDistances();
                        pictureBox_Paint(sender, e, shortestDistances, Brushes.HotPink);
                        List<int>[] shortestPaths = dijkstraAlgorithm.GetShortestPaths();
                        OutputInTextBox(shortestDistances, shortestPaths, clock.Elapsed);
                        break;
                    }
                case 1:
                    {
                        Stopwatch clock = new Stopwatch();
                        clock.Start();
                        BellmanFordAlgorithm bellmanFordAlgorithm = new BellmanFordAlgorithm(adjacencyList, sourceNode);
                        bellmanFordAlgorithm.Run();
                        if (bellmanFordAlgorithm.IsNegativeCycle() == true)
                        {
                            textBox1.Text = "Присутній негативний цикл";
                            clock.Stop();
                            break;
                        }
                        clock.Stop();
                        float[] shortestDistances = bellmanFordAlgorithm.GetShortestDistances();
                        pictureBox_Paint(sender, e, shortestDistances, Brushes.DeepPink);
                        List<int>[] shortestPaths = bellmanFordAlgorithm.GetShortestPaths();
                        OutputInTextBox(shortestDistances, shortestPaths, clock.Elapsed);
                        break;
                    }
                default:
                    break;

            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e){ }
        private void algorithmSelectionBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void FormGraph_Load(object sender, EventArgs e) { }
        private void OutputInTextBox(float[] shortestDistances, List<int>[] shortestPaths,TimeSpan  clockElapsed)
        {
            StringBuilder inTextBox = new StringBuilder();
            for (int i = 0; i < shortestDistances.Length; i++)
            {
                if (sourceNode != i)
                {
                    inTextBox.AppendLine($"Найкоротша відстань від ({sourceNode + 1}){(Name)sourceNode} до ({i + 1}){(Name)i} = {Math.Round(shortestDistances[i], 2, MidpointRounding.AwayFromZero)}");
                    inTextBox.AppendLine($"Найкоротший шлях від вузла ({sourceNode + 1}){(Name)sourceNode} до вузла ({i + 1}){(Name)i}: ");
                    inTextBox.AppendLine($"({string.Join(" -> ", shortestPaths[i])})");
                    inTextBox.AppendLine();
                }
            }
            inTextBox.AppendLine($"Витрачено часу на виконання програми: {clockElapsed}");
            textBox1.Text = inTextBox.ToString();
        }
    }
}

