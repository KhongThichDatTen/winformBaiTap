using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTongHopTMDT
{
    public partial class FormBaoCao : Form
    {
        string str = "Data Source=DESKTOP-GRFN403\\KIENLE;Initial Catalog=ThuongMaiDienTu;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        public FormBaoCao()
        {
            InitializeComponent();
            intodatagridview();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
        public void intodatagridview()
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select * from khachkhang order by ngaysinh ", conn);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
           
            
        }
    }
}
