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
    
    public partial class FormDanhSachKhachHang : Form
    {
        String str = "Data Source=DESKTOP-GRFN403\\KIENLE;Initial Catalog=ThuongMaiDienTu;Integrated Security=True;";
        public FormDanhSachKhachHang()
        {
            InitializeComponent();
            intoComboBoxData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            string insert = "insert into khachkhang " + "values(@ht, @sdt, @dc, @ns, @id)";
            SqlParameter[] p =
            {
                new SqlParameter("@ht",txtHoTen.Text),
                new SqlParameter("@sdt",txtSdt.Text),
                new SqlParameter("@dc",txtDiaChi.Text),
                new SqlParameter("@ns",dtp.Value.Date),
                new SqlParameter("@id",selectid)
            };
            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddRange(p);
            cmd.ExecuteNonQuery();
            MessageBox.Show("thêm thành công");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from khachkhang",conn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dgvDSKH.DataSource = dt;
            conn.Close();
            
        }
        public void intoComboBoxData()
        {
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from loaikhachhang", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem cb = new ComboBoxItem(reader["tenloaikhachhang"].ToString(), (int)reader["idloaikhachhang"]);
                cbLoaiKhachHang.Items.Add(cb);
            }
            reader.Close();
            
        }
        int selectid = 0;
        private void cbLoaiKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectid = ((ComboBoxItem)cbLoaiKhachHang.SelectedItem).id;
        }
    }
}
