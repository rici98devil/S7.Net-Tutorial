using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PROFINET_STEP_7.Entity;
using PROFINET_STEP_7.Profinet;

namespace PROFINET_STEP_7
{
    public partial class FRMCOLLECTION : Form
    {
        private PLC plc = null;
        private ExceptionCode errCode;

        public FRMCOLLECTION()
        {
            InitializeComponent();
        }
        private void FRMCOLLECTION_Load(object sender, EventArgs e)
        {
            try
            {
                plc = new PLC(CPU_Type.S71200, "192.168.1.19", 0, 0);
                // plc = new PLC(CPU_Type.S7300, "192.168.0.10", 0, 2);
                // plc = new PLC(CPU_Type.S7400, "192.168.0.10", 0, 3);
                errCode = plc.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRealInputOK_Click(object sender, EventArgs e)
        {
            try
            {
                int NumberOfDB = 108; //DB300
                int StartByteAddress = 2;
                int numberOfByte = 16;                
                byte[] result = plc.ReadBytes(DataType.DataBlock, NumberOfDB, StartByteAddress, numberOfByte);

               
                for (int i = 0; i <result.Length; i++)
                {
                   // gg[i]  =result[i].ToString();
                }
                
                // double[] values = Types.Double.ToArray(result);
          /*      List<FData> list = new List<FData>();
                for (int i = 0; i < values.Length; i++)
                {
                    list.Add(new FData(string.Format("DB{0}.DBW{1}", NumberOfDB, (StartByteAddress += 2)), values[i]));
                }
                dataGridView1.DataSource = list;*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {/*
            FRMMAIN frmMain = new FRMMAIN();
            frmMain.Show();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
