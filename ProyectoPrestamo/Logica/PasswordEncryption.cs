using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordEncryption
{
    public static string EncryptPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Convierte la contraseña en un arreglo de bytes
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convierte los bytes en una cadena hexadecimal
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public static bool VerifyPassword(string password, string hash)
    {
        // Encripta la contraseña ingresada
        string hashOfInput = EncryptPassword(password);

        // Compara la contraseña encriptada ingresada con la contraseña encriptada almacenada
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;
        return comparer.Compare(hashOfInput, hash) == 0;
    }
}