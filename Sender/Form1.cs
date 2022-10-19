using bs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sender
{
    public partial class Form1 : Form
    {
        MessageQueue queue = null;
        public Form1()
        {
            InitializeComponent();
            intit();
        }
        private void intit()
        {
            string path = @".\private$\concho";
            if (MessageQueue.Exists(path))
            {
                queue = new MessageQueue(path,QueueAccessMode.Send);
            }
            else
            {
                queue = MessageQueue.Create(path,true);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ma = Int32.Parse(textBox1.Text);
            string ten = textBox2.Text;
            int tuoi = Int32.Parse(textBox3.Text);
            // bool gioitinh = checkBox1.Checked;
            bool gioitinh = true;
            if (radioButton2.Checked)
            {
                gioitinh = false;
            }
            else
            {
                gioitinh = true;
            }

            Class1 st = new Class1(ma,ten,tuoi,gioitinh);

            MessageQueueTransaction stran = new MessageQueueTransaction();
            stran.Begin();
            queue.Send(st,stran);
            stran.Commit();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar);
        }
    }
}
