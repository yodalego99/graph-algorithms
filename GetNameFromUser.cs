namespace Gráfelméleti_algoritmusok
{
    public partial class GetNameFromUser : Form
    {
        public GetNameFromUser()
        {
            InitializeComponent();
        }
        public string name;
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                name = textBox1.Text;
                MessageBox.Show(name);
                DialogResult = DialogResult.OK;
            }
        }
    }
}
