using System;
using System.Numerics;
using Extensions;

namespace RSA
{
    [Serializable]
    public class RSAKeys
    {
        private BigInteger[] _privateKey;
        private BigInteger[] _publicKey;

        public int multiplierSize = 32;

        public BigInteger[] privateKey
        {
            get { return this._privateKey; }
            set { _privateKey = value; }
        }

        public BigInteger[] publicKey
        {
            get { return this._publicKey; }
            set { _publicKey = value; }
        }

        public void GenerateKeys()
        {
       
            BigInteger p, q, n, fn, x=0, y=0, d=0;
            Byte e;
            do
            {
                do
                {
                    p = Ext.GetPrime(multiplierSize);
                    q = Ext.GetPrime(multiplierSize);
                }
                while (((int)BigInteger.Log(p * q, 2) + 1) != multiplierSize * 2);

                n = q * p;
                fn=(q-1)*(p-1);


                e=GetNumE(fn);
                Ext.ExtEuclid(fn,e,out x,out y,out d);
            
            }while(d!=1 || y<=1);

            this._publicKey = new BigInteger[2] {e,n};
            this._privateKey = new BigInteger[2] { y, n };
        
        }

        byte GetNumE(BigInteger num)
        {
            byte e;
            do
            {
                Random rnd = new Random();
                e = (byte)rnd.Next(128);
                if (e == 0 || e == 1)
                    continue;

            }
            while (BigInteger.GreatestCommonDivisor((BigInteger)num, (BigInteger)e) != 1);

            return e;

        }

        public override string ToString()
        {
            if (_privateKey == null && _publicKey == null)
            {
                return "Одна из пар ключей не установлена";
            }
            else
            {
                return String.Format("КО:\r\n{0}\r\n{1}r\nКЗ:{2}\r\n{3}", _publicKey[0], _publicKey[1], _privateKey[0], _privateKey[1]);
            }
            
        }
    
    }
}
