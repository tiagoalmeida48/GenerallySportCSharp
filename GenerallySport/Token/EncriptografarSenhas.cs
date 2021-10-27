using System.Security.Cryptography;
using System.Text;

namespace GenerallySport.Token
{
    public class EncriptografarSenhas
    {
        private HashAlgorithm _algoritmo;

        public EncriptografarSenhas(HashAlgorithm algoritmo)
        {
            _algoritmo = algoritmo;
        }

        public string EncriptografarSenha(string senha)
        {
            var md5 = MD5.Create();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(senha);
            byte[] hash = md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public bool ConfirmarSenha(string senha, string senhaArmazenada)
        {
            var senhaLogin = EncriptografarSenha(senha);
            if(senhaLogin == senhaArmazenada)
                return true;
            else
                return false;
        }
    }
}
