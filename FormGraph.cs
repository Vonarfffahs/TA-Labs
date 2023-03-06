using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Lab2
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
        private List<Tuple<int, int>> directedEdges = new List<Tuple<int, int>>(); // Список орієнтованих ребер графа
        
        static List<(int node, float weight)>[] adjacencyList = new List<(int node, float weight)>[]  
            {
                new List<(int node, float weight)> { (node: 1, weight: 4.6f) },
                new List<(int node, float weight)> { (node: 0, weight: 4.6f),(node: 2, weight: 0.9f) },
                new List<(int node, float weight)> { (node: 1, weight: 0.9f),(node: 3, weight: 0.6f),(node: 10, weight: 1f) },
                new List<(int node, float weight)> { (node: 2, weight: 0.6f),(node:4, weight: 0.7f),(node: 9, weight: 0.65f) },
                new List<(int node, float weight)> { (node: 3, weight: 0.7f),(node: 5, weight: 0.6f),(node: 8, weight: 0.6f) },
                new List<(int node, float weight)> { (node: 4, weight: 0.6f),(node: 6, weight: 0.8f) },
                new List<(int node, float weight)> { (node: 7, weight: 0.75f),(node: 8, weight: 0.35f) },
                new List<(int node, float weight)> { (node: 6, weight: 0.75f) },
                new List<(int node, float weight)> { (node: 3, weight: 0.5f),(node: 6, weight: 0.35f) },
                new List<(int node, float weight)> { (node: 8, weight: 0.5f),(node: 10, weight: 1.1f) },
                new List<(int node, float weight)> { (node: 9, weight: 1.1f) },
            };// Список суміжності де рядок - це визначні місця, стовпець - це місце до якого є сполучення та відстань до цього сполучення в кілометрах.
        static int sourceNode = 0;
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
            directedEdges.Add(Tuple.Create(2, 10));
            directedEdges.Add(Tuple.Create(3, 9));
            directedEdges.Add(Tuple.Create(4, 8));
            directedEdges.Add(Tuple.Create(5, 6));
            directedEdges.Add(Tuple.Create(8, 3));
            directedEdges.Add(Tuple.Create(9, 8));
            pictureBox1.Paint += new PaintEventHandler(pictureBox_Paint);
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(Color.Black, 10);

            // Малюємо ребра графа
            foreach (var edge in edges)
            {
                var point1 = vertices[edge.Item1];
                var point2 = vertices[edge.Item2];
                e.Graphics.DrawLine(pen, point1, point2);
            }
            foreach (var directedEdge in directedEdges)
            {
                var point1 = vertices[directedEdge.Item1];
                var point2 = vertices[directedEdge.Item2];
                pen.EndCap = LineCap.ArrowAnchor;
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
            DijkstraAlgorithm dijkstraAlgorithm = new DijkstraAlgorithm(adjacencyList, sourceNode);
            dijkstraAlgorithm.Run();
            float[] shortestDistances = dijkstraAlgorithm.GetShortestDistances();
            StringBuilder inTextBox = new StringBuilder();

            for (int i = 1; i < shortestDistances.Length; i++)
            {
                inTextBox.AppendLine($"Найкоротша відстань від ({1}){(Name)sourceNode} до ({i + 1}){(Name)i} = {shortestDistances[i]}");
            }
            textBox1.Text = inTextBox.ToString();
        }
        private void textBox1_TextChanged(object sender, EventArgs e){ }
    }
}

