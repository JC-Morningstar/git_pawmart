using System.Security.Cryptography;
using System.Text;

namespace pawmart_jc.Recursos
{
    public class Utilidades
    {
        public static string EncriptarContraseña(string Contraseña)
        {
           if (Contraseña==null)
            {
                throw new ArgumentNullException(nameof(Contraseña), "La contraseña no puede ser nula.");
            }
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(Contraseña));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}
