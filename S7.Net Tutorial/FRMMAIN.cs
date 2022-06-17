using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PROFINET_STEP_7.Profinet;
using PROFINET_STEP_7.Types;

namespace PROFINET_STEP_7
{
    public partial class FRMMAIN : Form
    {
        private PLC plc = null;
        private ExceptionCode errCode;
        public struct prova
        {
            string cesto ;
         DateTime datafine;
           DateTime datainizio;
            string  cassette;
         int ciclo;
       
            
            
        }

        Type pippo = typeof(prova);

        public FRMMAIN()
        {
            InitializeComponent();
        }

        private void FRMMAIN_Load(object sender, EventArgs e)
        {
            try
            {
                plc = new PLC(CPU_Type.S71200, "192.168.1.19", 0, 0);
                errCode = plc.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {

           txtOutput.Text = plc.Read(txtInput.Text).ToString();
              
            
        //        txtOutput.Text = (plc.ReadStruct(pippo, int.Parse(txtInput.Text)).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                                                      
        }

        private void btnReadArray_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            try
            {
                int NumberOfDB = int.Parse(txtDb.Text); //DB108
                int StartByteAddress = int.Parse(txtstart.Text); 
                
                int numberOfByte = int.Parse(txtend.Text); ;

   byte[] result = plc.ReadBytes(DataType.DataBlock, NumberOfDB, StartByteAddress, numberOfByte);
           
     string strBin = string.Empty;
               // byte btindx = 0;
             //   string strAllbin = string.Empty;
             /*
                for (int i = 0; i < numberOfByte; i++)
                {
                    btindx = result[i];

                    strBin = Convert.ToString(btindx, 2); // Convert from Byte to Bin
                    strBin = strBin.PadLeft(8, '0');  // Zero Pad

                   richTextBox1.Text += strBin+" ";
                }
                */
               
                ushort[] values = Word.ToArray(result);

                for (int i = 0; i < values.Length; i++)
                {
                    richTextBox1.Text+= result[i].ToString()+" ";
                  
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            


        }
        private void leggi()
        {


            richTextBox1.Text += "\n";


            try
            {
                int NumberOfDB = int.Parse(txtDb.Text); //DB108
                int StartByteAddress = int.Parse(txtstart.Text);

                int numberOfByte = int.Parse(txtend.Text); ;

                byte[] result = plc.ReadBytes(DataType.DataBlock, NumberOfDB, StartByteAddress, numberOfByte);

                string strBin = string.Empty;
              

                ushort[] values = Word.ToArray(result);

                for (int i = 0; i < values.Length; i++)
                {
                    richTextBox1.Text += result[i].ToString() + " ";

                }

                richTextBox1.Text += "\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
      
 /*  try
            {
                double value = Types.Double.FromDWord((uint)plc.Read("DB108.DBD0"));
                lblReal.Text = value.ToString("0.#");
            }
            catch (Exception)
            {
            }*/
       

        private void btnReadRealArray_Click(object sender, EventArgs e)
        {
            try
            {
                int NumberOfDB = int.Parse(txtInput.Text); //DB300
                int StartByteAddress = 4;
                byte[] result = plc.ReadBytes(DataType.DataBlock, NumberOfDB, StartByteAddress, 16);
                double[] values = Types.Double.ToArray(result);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            leggi();
        }
    }
}
