using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaClientes
{
    public partial class LoginForm : Form
    {
        private readonly string connectionString = "Data Source=localhost;Initial Catalog=ClientesDB;User ID=seu_usuario;Password=sua_senha";
        private DataGridView dgvClientes; // Tabela para exibir os clientes
        private TextBox txtNome; // Campo de texto para inserir o nome
        private TextBox txtEmail; // Campo de texto para inserir o email
        private Button btnLogin; // Botão de login

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Configuração das propriedades para os controles dgvClientes, txtNome, txtEmail e btnLogin

            // dgvClientes
            this.dgvClientes = new DataGridView();
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Location = new System.Drawing.Point(12, 12);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.Size = new System.Drawing.Size(500, 200);
            this.dgvClientes.TabIndex = 0;

            // txtNome
            this.txtNome = new TextBox();
            this.txtNome.Location = new System.Drawing.Point(12, 220);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(200, 20);
            this.txtNome.TabIndex = 1;

            // txtEmail
            this.txtEmail = new TextBox();
            this.txtEmail.Location = new System.Drawing.Point(12, 250);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmail.TabIndex = 2;

            // btnLogin
            this.btnLogin = new Button();
            this.btnLogin.Location = new System.Drawing.Point(12, 280);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new EventHandler(BtnLogin_Click);

            // Configuração das propriedades do formulário LoginForm
            this.ClientSize = new System.Drawing.Size(524, 319);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnLogin);
            this.Name = "LoginForm";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtNome.Text;
            string password = txtNome.Text;

            if (AuthenticateUser(username, password))
            {
                ShowMainForm();
            }
            else
            {
                MessageBox.Show("Nome de usuário ou senha inválidos.");
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Clientes WHERE Nome=@Username AND Email=@Password", connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private void ShowMainForm()
        {
            MainForm mainForm = new MainForm();
            mainForm.ShowDialog();
        }
    }

    public partial class MainForm : Form
    {
        private readonly string ConnectionString = "Data Source=localhost;Initial Catalog=ClientesDB;User ID=seu_usuario;Password=sua_senha";
        private DataGridView dgvClientes; // DataGridView para exibir os clientes
        private TextBox txtNome; // TextBox para inserir o nome
        private TextBox txtEmail; // TextBox para inserir o email
        private Button btnAdd; // Botão de adicionar
        private Button btnUpdate; // Botão de atualizar
        private Button btnDelete; // Botão de excluir

        public MainForm()
        {
            InitializeComponent();
            LoadClientes();
        }

        private void LoadClientes()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Clientes", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvClientes.DataSource = dataTable;
            }
        }

        private void InitializeComponent()
        {
            // Configuração das propriedades para os controles dgvClientes, txtNome, txtEmail, btnAdd, btnUpdate e btnDelete

            // dgvClientes
            this.dgvClientes = new DataGridView();
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Location = new System.Drawing.Point(12, 12);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.Size = new System.Drawing.Size(500, 200);
            this.dgvClientes.TabIndex = 0;

            // txtNome
            this.txtNome = new TextBox();
            this.txtNome.Location = new System.Drawing.Point(12, 220);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(200, 20);
            this.txtNome.TabIndex = 1;

            // txtEmail
            this.txtEmail = new TextBox();
            this.txtEmail.Location = new System.Drawing.Point(12, 250);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmail.TabIndex = 2;

            // btnAdd
            this.btnAdd = new Button();
            this.btnAdd.Location = new System.Drawing.Point(12, 280);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Adicionar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(btnAdd_Click);

            // btnUpdate
            this.btnUpdate = new Button();
            this.btnUpdate.Location = new System.Drawing.Point(93, 280);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Atualizar";

            // btnDelete
            this.btnDelete = new Button();
            this.btnDelete.Location = new System.Drawing.Point(174, 280);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Excluir";

            // Configuração das propriedades do formulário MainForm
            this.ClientSize = new System.Drawing.Size(524, 319);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Name = "MainForm";
            this.Text = "Sistema de Clientes";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string email = txtEmail.Text;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Clientes (Nome, Email) VALUES (@Nome, @Email)", connection);
                command.Parameters.AddWithValue("@Nome", nome);
                command.Parameters.AddWithValue("@Email", email);
                command.ExecuteNonQuery();
            }

            LoadClientes();
            ClearInputs();
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvClientes.SelectedRows[0];
                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
            }
            else
            {
                ClearInputs();
            }
        }

        private void ClearInputs()
        {
            txtNome.Clear();
            txtEmail.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int clienteId = GetSelectedClienteId();

            if (clienteId != -1)
            {
                string nome = txtNome.Text;
                string email = txtEmail.Text;

                UpdateCliente(clienteId, nome, email);
            }
            else
            {
                MessageBox.Show("Selecione um cliente para atualizar.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int clienteId = GetSelectedClienteId();

            if (clienteId != -1)
            {
                DeleteCliente(clienteId);
            }
            else
            {
                MessageBox.Show("Selecione um cliente para excluir.");
            }
        }

        private int GetSelectedClienteId()
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                return (int)dgvClientes.SelectedRows[0].Cells["ClienteId"].Value;
            }

            return -1;
        }

        private void UpdateCliente(int clienteId, string nome, string email)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Clientes SET Nome=@Nome, Email=@Email WHERE ClienteId=@ClienteId", connection);
                command.Parameters.AddWithValue("@Nome", nome);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@ClienteId", clienteId);
                command.ExecuteNonQuery();
            }

            LoadClientes();
            ClearInputs();
        }

        private void DeleteCliente(int clienteId)
        {
            if (MessageBox.Show("Tem certeza que deseja excluir o cliente?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Clientes WHERE ClienteId=@ClienteId", connection);
                    command.Parameters.AddWithValue("@ClienteId", clienteId);
                    command.ExecuteNonQuery();
                }

                LoadClientes();
                ClearInputs();
            }
        }
    }
}


