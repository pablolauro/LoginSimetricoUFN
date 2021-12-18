using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginUser
{
    public partial class CadastroUsuario : Form
    {
        public CadastroUsuario()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Assimetrico
            Usuario user = new Usuario();
            user.nome = txbNome.Text;
            user.email = txbEmail.Text;

            Assimetrico a = new Assimetrico();

            user.senha = a.encrypt(txbSenha.Text);           

            if (user.gravarUsuario())
            {
                MessageBox.Show("Gravado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao gravar");
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Simétrico

            Usuario user = new Usuario();
            user.nome = txbNome.Text;
            user.email = txbEmail.Text;
            

            Simetrico s = new Simetrico();


            user.senha = s.EncryptData(txbSenha.Text, "UsEr");


            if (user.gravarUsuario())
            {
                MessageBox.Show("Gravado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao gravar");
            };
        }
    }
}
