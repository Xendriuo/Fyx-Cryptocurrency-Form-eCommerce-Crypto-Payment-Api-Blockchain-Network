using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            removeBG(pbOrder,lblOrder);
            removeBG(pbOrder, lblOrder2);
            removeBG(pbOrder, lblOrder3);
            removeBG(pbOrder, lblOrder4);

            removeBG(pbProfit, lblProfit);
            removeBG(pbProfit, lblProfit3);
            removeBG(pbProfit, lblProfit4);

            removeBG(pbSales, lblsales);
            removeBG(pbSales, lblsales2);
            removeBG(pbSales, lblsales4);

            removeBG(pbClients, lblClient);
            removeBG(pbClients, lblClient2);
            removeBG(pbClients, lblClient3);
            removeBG(pbClients, lblClient4);



        }



        private void removeBG(PictureBox pb,Label lbl)
        {
            var pos = this.PointToScreen(lbl.Location);
            pos = pb.PointToClient(pos);
            lbl.Parent = pb;
            lbl.Location = pos;
            lbl.BackColor = Color.Transparent;

        }

    }
}
