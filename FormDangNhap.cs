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
    public partial class FormDangNhap : Form
    {
        string str = "Data Source=DESKTOP-GRFN403\\KIENLE;Initial Catalog=ThuongMaiDienTu;Integrated Security=True;";
        public FormDangNhap()
        {
            InitializeComponent();
           
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            //FormDangNhap formDangNhap = new FormDangNhap();
            //formDangNhap.MdiParent = this;
            //formDangNhap.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("bạn có muốn thoát không", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes) { 
                Application.Exit();
            }
        }
           //SqlCommand cmd = new SqlCommand("select * from dangnhap", conn);
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read()) { Console.WriteLine( reader["matkhau"].ToString()); }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            Boolean isSuccess = false;
            while (true)
            {
                //SqlDataAdapter adap = new SqlDataAdapter ("select * from thuongmaidientu",conn);
                SqlCommand cmd = new SqlCommand("select * from dangnhap", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["TenDangNhap"].ToString().Trim() == txtDangNhap.Text && reader["MatKhau"].ToString().Trim() == txtMatKhau.Text)
                    {
                        MessageBox.Show("đăng nhập thành con mẹ nó công rồi đấy");
                        this.Close();
                        isSuccess = true;
                        break;
                    }
                }
                reader.Close();
                if (isSuccess) { break; }
                else
                {
                    MessageBox.Show("sai tên đăng nhập hoặc mật khẩu!!");
                    txtDangNhap.Clear();
                    txtMatKhau.Clear();
                    txtDangNhap.Focus();
                    break;
                }
            }
            conn.Close();

        }
    }
}
