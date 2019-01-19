using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib
{
    // 출처: http://hayateasdf.hatenablog.com/entry/2014/02/28/154830
    //BitSetTest tes = new BitSetTest();
    //tes.Set(0);
    //tes.Set(1);
    //tes.Set(2);
    //tes.Set(2, false);
    //tes.Set(5);
    //Console.WriteLine(tes.Check(0));
    //Console.WriteLine(tes.Check(1));
    //Console.WriteLine(tes.Check(2));
    //Console.WriteLine(tes.Check(3));
    //Console.WriteLine(tes.Check(4));
    //Console.WriteLine(tes.Check(5));
    //Console.WriteLine(tes.ToString());

    public struct BitSet
    {
        int BitValue;

        public BitSet(int value) { BitValue = value; }

        public void Set(int pos, bool val)
        {
            if (pos >= 32 || pos < 0) return;

            if (val)
                BitValue |= (1 << pos);
            else
                BitValue &= ~(1 << pos);
        }

        public void Set(int pos)
        {
            if (pos >= 32 || pos < 0) return;

            BitValue |= (1 << pos);
        }

        public bool Check(int pos)
        {
            if (pos >= 32 || pos < 0) return false; //throw new Exception();
            return (((BitValue >> pos) & 0x1) == 1) ? true : false;
        }

        public bool IsEmpty()
        {
            return BitValue == 0 ? true : false;
        }

        public int Get() { return BitValue; }

        public override string ToString()
        {
            string str = "";
            for (int i = 1; i <= 32; ++i)
            {
                int val = (1 << 32 - i);
                int a = (BitValue & val);
                if (a == 0)
                    str += "0";
                else
                    str += "1";
            }
            return str;
        }
    }
}
