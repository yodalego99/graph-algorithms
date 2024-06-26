namespace Gráfelméleti_algoritmusok
{
    public partial class RandomWeights : Form
    {
        public int MinWeight { get; private set; }
        public int MaxWeight { get; private set; }

        public RandomWeights()
        {
            InitializeComponent();
        }

        private void RandomizeButton_Click(object sender, EventArgs e)
        {
            // validate input
            if (int.TryParse(minTextBox.Text, out int min) && int.TryParse(maxTextBox.Text, out int max) && min > 0 && max > 0)
            {
                MinWeight = min;
                MaxWeight = max;
                if (max > min)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Maximum weight must be greater than minimum weight.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid, positive integers for both minimum and maximum weights.",
                                "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
