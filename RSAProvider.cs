using System;
using System.Numerics;

namespace RSA
{
    public class RSAProvider
    {
        private RSAKeys _keyObj;
        public RSAProvider(RSAKeys keys) 
        {
            _keyObj = keys;
        }

        public BigInteger Encryption(BigInteger data)
        {
            if (_keyObj.privateKey == null && _keyObj.publicKey == null)
                _keyObj.GenerateKeys();

            if (data>_keyObj.publicKey[1])
            {
                throw new Exception(String.Format("Цифровое сообщение больше значения модуля", _keyObj.multiplierSize * 2));
            }
            
            return BigInteger.ModPow(data,_keyObj.publicKey[0],_keyObj.publicKey[1]);
        }

        public BigInteger Decryption(BigInteger data)
        {
            if (_keyObj.privateKey == null)
                throw new Exception("КЗ не определён");

            return BigInteger.ModPow(data, _keyObj.privateKey[0], _keyObj.privateKey[1]);
        }
    }
}
