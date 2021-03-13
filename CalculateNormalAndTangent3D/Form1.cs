using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace CalculateNormalAndTangent3D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Enum型を配列として取得、comboBoxにDataSourceとして指定する
            comboBox1.DataSource = Enum.GetValues(typeof(CalculatePosNormalTangent.RotationMatrixAxis));
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Position
            double RX = double.Parse(textBox6.Text);
            double RY = double.Parse(textBox5.Text);
            double RZ = double.Parse(textBox4.Text);

            Vector3D Rotation = new Vector3D(RX, RY, RZ);

            //comboBoxで選択したItemを取得
            CalculatePosNormalTangent.RotationMatrixAxis AxisType = (CalculatePosNormalTangent.RotationMatrixAxis)comboBox1.SelectedItem;

            //Calculate Rotate Value to Normal And Tangent
            CalculatePosNormalTangent Calculate_PNT = new CalculatePosNormalTangent();
            CalculatePosNormalTangent.PosNormalTangent Pos_Nrm_Tan3D = Calculate_PNT.GetPosNrmTan3D(Rotation, AxisType);

            textBox10.Text = Pos_Nrm_Tan3D.Normal3D.X.ToString();
            textBox11.Text = Pos_Nrm_Tan3D.Normal3D.Y.ToString();
            textBox12.Text = Pos_Nrm_Tan3D.Normal3D.Z.ToString();

            Tangent_X_TXT.Text = Pos_Nrm_Tan3D.Tangent3D.X.ToString();
            Tangent_Y_TXT.Text = Pos_Nrm_Tan3D.Tangent3D.Y.ToString();
            Tangent_Z_TXT.Text = Pos_Nrm_Tan3D.Tangent3D.Z.ToString();

            //bi-normal

            float N1 = float.Parse(Pos_Nrm_Tan3D.Normal3D.X.ToString());
            float N2 = float.Parse(Pos_Nrm_Tan3D.Normal3D.Y.ToString());
            float N3 = float.Parse(Pos_Nrm_Tan3D.Normal3D.Z.ToString());

            CalculatePosNormalTangent.BINormalTangent VN = Calculate_PNT.Calculate_BINormalAndTangent(new Vector3(N1, N2, N3));

            textBox1.Text = VN.BINormal.X.ToString();
            textBox2.Text = VN.BINormal.Y.ToString();
            textBox3.Text = VN.BINormal.Z.ToString();
        }


    }
}
