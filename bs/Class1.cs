using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bs
{
    [Serializable]
    public class Class1
    {
        public int id { get; set; }
        public string ten { get; set; }
        public int tuoi { get; set; }
        public bool gioitinh { get; set; }

        public Class1() : this(0, "koco",123, false) { }
        public Class1(int id, string ten, int tuoi, bool gioitinh)
        {
            this.id = id;
            this.ten = ten;
            this.tuoi = tuoi;
            this.gioitinh = gioitinh;
        }

        public override string ToString()
        {
            return id+"/"+ten+ "/" + tuoi+ "/" + gioitinh;
        }


    }
}
