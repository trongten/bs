using bs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reciver
{
    public partial class Form1 : Form
    {
        MessageQueue queue = null;
        private List<Class1> list = new List<Class1>();
        public Form1()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            string path = @".\private$\concho";
            queue = new MessageQueue(path);
            queue.BeginReceive();
            queue.ReceiveCompleted += ReceiveCompleted;
        }

        public void ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = e.Message;
            int type = msg.BodyType;
            XmlMessageFormatter fmt = new XmlMessageFormatter(
                new System.Type[] { typeof(string), typeof(Class1) });
           
            msg.Formatter = fmt;

            var re = msg.Body;

            String[] a = re.ToString().Split('/');
            Class1 st = new Class1(Int32.Parse(a[0]), a[1], Int32.Parse(a[2]), Boolean.Parse(a[3]));
            list.Add(st);

            Thread th = new Thread(delegate ()
            {
                if (listBox1.InvokeRequired)
                {
                    System.Object[] i = new System.Object[1];
                    i[0] = a[0];
                    listBox1.Invoke(new MethodInvoker(delegate
                    {
                        listBox1.Items.AddRange(i);
                    }));
                }
            });
            th.Start();
            queue.BeginReceive();
        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            int i = listBox1.SelectedIndex;

            textBox1.AppendText(list[i].id.ToString());
            textBox2.AppendText(list[i].ten.ToString());
            textBox3.AppendText(list[i].tuoi.ToString());

            checkBox1.Checked = list[i].gioitinh;
            if (list[i].gioitinh)
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.SelectedIndex = 1;


            if (list[i].gioitinh)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }
                

        }
    }
}
