﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Recursos
    {
        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            //USAR LA REFERENCIA DE "System.Security.Cryptography
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
    }
}
