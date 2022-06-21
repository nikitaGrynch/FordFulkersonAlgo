using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;

namespace FordFulkersonWinForms
{
    public partial class Form1 : Form
    {
        private const String SAMPLE_FILENAME = "graph.json";
        private bool isJsonSelected = false;
        private FordFulkersonAlgo.FordFulkersonAlgo algo;
        public Form1()
        {
            InitializeComponent();
            algo = new FordFulkersonAlgo.FordFulkersonAlgo();
        }

        private void buttonSelectGraph_Click(object sender, EventArgs e)
        {
            openFileDialogSelectGraph.FileName = SAMPLE_FILENAME;
            openFileDialogSelectGraph.InitialDirectory = Application.StartupPath;
            openFileDialogSelectGraph.Filter = "JsonFiles (*.json)|*.json";


            if (openFileDialogSelectGraph.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var jsonFile = new StreamReader(openFileDialogSelectGraph.FileName))
                    {
                        algo.GetDataFromJson(jsonFile.ReadToEnd());
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Incorrect file");
                    return;
                }
                isJsonSelected = true;
                labelStatusJson.Text = "Graph selected";
                labelStatusJson.ForeColor = Color.Green;

                comboBoxSourceVertex.Items.Clear();
                comboBoxTargetVertex.Items.Clear();
                for (int i = 1; i <= algo.VertexCount; i++)
                {
                    comboBoxSourceVertex.Items.Add(i);
                    comboBoxTargetVertex.Items.Add(i);
                }
            }
        }

        private void buttonStartAlgo_Click(object sender, EventArgs e)
        {
            richTextBoxResult.Text = String.Empty;
            if (!isJsonSelected)
            {
                MessageBox.Show("Graph not selected");
                return;
            }
            if(comboBoxSourceVertex.SelectedIndex == -1 || comboBoxTargetVertex.SelectedIndex == -1)
            {
                MessageBox.Show("Select source and target");
                return;
            }
            if(comboBoxSourceVertex.SelectedItem == comboBoxTargetVertex.SelectedItem)
            {
                MessageBox.Show("Source and target cannot be equal");
                return;
            }
            algo.sourceVertex = (int)comboBoxSourceVertex.SelectedItem;
            algo.targetVertex = (int)comboBoxTargetVertex.SelectedItem;
            algo.GetMaxGraphFlow();
            algo.BuildFiniteGraph();
            richTextBoxResult.Text = algo.resultStr;
            
        }
    }
}
