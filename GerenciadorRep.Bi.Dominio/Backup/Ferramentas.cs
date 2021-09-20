// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.Ferramentas
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System.Text;
using System.Text.RegularExpressions;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class Ferramentas
  {
    public static bool RemoveCaracteresEspeciais(string str, out string strOut)
    {
      strOut = str;
      bool flag = false;
      byte[] bytes = Encoding.Default.GetBytes(str);
      for (int index = 0; index < bytes.Length; ++index)
      {
        if (bytes[index] == (byte) 131)
          bytes[index] = (byte) 0;
      }
      str = Encoding.Default.GetString(bytes);
      string input = Regex.Replace(str, "[^-\\ \\w\\.@-]^[\\/]", "");
      strOut = Regex.Replace(input, "[°]", "");
      if (str.Length != strOut.Length)
        flag = true;
      return flag;
    }

    public static bool ValidaPIS(string vrPIS)
    {
      string str = vrPIS.Replace(".", "").Replace("-", "").Replace("//", "");
      int[] numArray1 = new int[11];
      int[] numArray2 = new int[10]
      {
        3,
        2,
        9,
        8,
        7,
        6,
        5,
        4,
        3,
        2
      };
      int num1 = 0;
      long num2 = 9999;
      if (str.Length != 11)
        return false;
      for (int index = 0; index < 11; ++index)
        numArray1[index] = int.Parse(str[index].ToString());
      if (long.Parse(str.Substring(0, 10)) < num2)
        return false;
      for (int index = 0; index < 10; ++index)
        num1 += numArray1[index] * numArray2[index];
      int num3 = 11 - num1 % 11;
      if (num3 > 9)
        num3 = 0;
      return num3 == numArray1[10];
    }
  }
}
