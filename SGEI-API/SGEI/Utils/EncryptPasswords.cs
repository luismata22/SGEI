using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace SGEI.Utils
{
  public class EncryptPasswords
  {
    private const string Salt = "D5XC";
    public IConfiguration AppSettings { get; }
    public static string DecryptWithAes(string decryptText, string key)
    {
      var cipherBytes = Convert.FromBase64String(decryptText);
      using var encryptor = Aes.Create();
      var salt = cipherBytes.Take(16).ToArray();
      var iv = cipherBytes.Skip(16).Take(16).ToArray();
      var encrypted = cipherBytes.Skip(32).ToArray();
      var pdb = new Rfc2898DeriveBytes(key, salt, 100);
      encryptor.Key = pdb.GetBytes(32);
      encryptor.Padding = PaddingMode.PKCS7;
      encryptor.Mode = CipherMode.CBC;
      encryptor.IV = iv;
      using var ms = new MemoryStream(encrypted);
      using var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Read);
      using var reader = new StreamReader(cs, Encoding.UTF8);
      return reader.ReadToEnd();
    }

    public static string EncryptWithHash(string encryptText)
    {
      encryptText += Salt;
      return ComputeHash(encryptText);
    }

    private static string ComputeHash(string password)
    {
      StringBuilder builder = new StringBuilder();
      using (SHA256 mySha256 = SHA256.Create())
      {
        byte[] bytes = mySha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < bytes.Length; i++)
        {
          _ = builder.Append(bytes[i].ToString("x2"));
        }
      }

      return builder.ToString();
    }

    public virtual string CreateTokenJWT(int usuarioId, int expireTime)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var tokenDescriptor = CreateSecurityDescriptor(usuarioId, expireTime);
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor CreateSecurityDescriptor(int usuarioId, int expireTime)
    {
      var key = Encoding.ASCII.GetBytes(AppSettings["JWT:Key"]);
      return new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[] { new Claim("id", usuarioId.ToString()) }),
        Expires = DateTime.UtcNow.AddMinutes(expireTime),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
    }
  }
}
