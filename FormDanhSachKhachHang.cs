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
        //them data
        public void loaddata()
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from khachkhang", conn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dgvDSKH.DataSource = dt;
            conn.Close();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try 
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("vui vòng nhập dữ liệu hợp lệ "+ ex.Message);
            }
            
                loaddata();   
            
        }



       //thêm data vào combobox
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

        private void dgvDSKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }





        //CẬP NHẬT
        string selectID = "";
        private void dgvDSKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo không bấm vào tiêu đề cột
            {
                object cellId = dgvDSKH.Rows[e.RowIndex].Cells["ID"].Value; // lấy giá trị của dòng mình ấn
                if (cellId != null) // Kiểm tra giá trị không null
                {
                   txtID.Text  = cellId.ToString(); //hiển thị lên textbox khi ấn vô dòng
                   selectID = cellId.ToString();
                }
                object cellHoTen = dgvDSKH.Rows[e.RowIndex].Cells["HoTen"].Value;
                if (cellHoTen != null) // Kiểm tra giá trị không null
                {
                    txtHoTen.Text = cellHoTen.ToString();
                }
                object cellSDT = dgvDSKH.Rows[e.RowIndex].Cells["SDT"].Value;
                if (cellSDT != null) // Kiểm tra giá trị không null
                {
                    txtSdt.Text = cellSDT.ToString();
                }
                object cellDiaChi = dgvDSKH.Rows[e.RowIndex].Cells["DiaChi"].Value;
                if (cellSDT != null) // Kiểm tra giá trị không null
                {
                    txtDiaChi.Text = cellDiaChi.ToString();
                }
                object cellNgaySinh = dgvDSKH.Rows[e.RowIndex].Cells["NgaySinh"].Value;
                if (cellNgaySinh != null) // Kiểm tra giá trị không null
                {
                    dtp.Text = cellNgaySinh.ToString();
                }   
               
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("bạn có muốn cập nhật dữ liêu?","confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(str);
                    conn.Open();
                    string insert = "update khachkhang set " + "hoten = @ht, sdt = @sdt, diachi = @dc, ngaysinh = @ns, idloaikhachhang = @id where id = " + selectID;
                    SqlParameter[] p =
                    {
                    new SqlParameter("@ht", txtHoTen.Text),
                    new SqlParameter("@sdt", txtSdt.Text),
                    new SqlParameter("@dc", txtDiaChi.Text),
                    new SqlParameter("@ns", dtp.Value.Date),
                    new SqlParameter("@id",selectid)
                    };
                    SqlCommand cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddRange(p);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) {MessageBox.Show("vui lòng chọn loại khách hàng!!",ex.Message); }
                loaddata();
            }
            
        }



        //DELETE
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("bạn muốn xóa dữ liệu của khách hàng có mã id là: " + selectID,
                "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes) 
            { 
                if(selectID != "") {
                    SqlConnection connection = new SqlConnection(str);
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("delete from khachkhang where id = " + selectID, connection);
                    cmd.ExecuteNonQuery();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from khachkhang", connection);
                    DataTable dt = new DataTable();
                    sqlDataAdapter.Fill(dt);
                    dgvDSKH.DataSource = dt;
                    connection.Close();
                }
                else { MessageBox.Show("bạn chưa chọn dòng"); }
            }

        }
        //tÌM KIẾM
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            string insert = "";
            if(txtTimKiem.Text == "") {  insert = "select * from khachkhang"; }
            else  insert = "select * from khachkhang where hoten = '"+txtTimKiem.Text+"'";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(insert, conn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dgvDSKH.DataSource = dt;
            conn.Close();
        }
    }
}


