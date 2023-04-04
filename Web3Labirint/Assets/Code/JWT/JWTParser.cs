using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Security.Cryptography;
namespace JWT
{
    public class JWTParser
    {
        public static UserJWT Parse(string jwt)
        {
            string jwtBody = jwt.Split(".")[1];
            jwtBody += new string('=', (4 - jwtBody.Length % 4) % 4);
            byte[] bytes = System.Convert.FromBase64String(jwtBody);
            string json = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            return JsonUtility.FromJson<UserJWT>(json);
        } 
    }
}
