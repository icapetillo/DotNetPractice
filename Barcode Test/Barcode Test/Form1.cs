namespace Barcode_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    MessageBox.Show(textBox1.Text, "Barcode reading");
        //}

        private void textBox1_Leave(object sender, EventArgs e)
        {
            MessageBox.Show(textBox1.Text, "Barcode reading");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                MessageBox.Show("El código leido es: " + textBox1.Text, "Lectura de código");
            }
        }
    }
}
