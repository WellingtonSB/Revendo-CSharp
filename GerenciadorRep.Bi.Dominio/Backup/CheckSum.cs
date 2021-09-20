// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.CheckSum
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class CheckSum
  {
    public static long GerarCheckSumRep(string pis, string data, string nsr)
    {
      long num1 = 0;
      long num2 = 0;
      string str = "GerenciadorRep" + pis + data + nsr;
      char[] charArray1 = str.ToCharArray();
      int length1 = charArray1.Length;
      for (int index = 0; index < length1; ++index)
        num1 += Convert.ToInt64(charArray1[index]);
      long num3 = num1 % 249L + 1L;
      while (str.Length % 3 != 0)
        str += new string(' ', 1);
      char[] charArray2 = str.ToCharArray();
      int length2 = charArray2.Length;
      for (int index = 0; index < length2; index += 3)
      {
        long num4 = Convert.ToInt64(charArray2[index]) + num3;
        long num5 = num3 * 95L % 251L;
        long num6 = num4;
        long num7 = Convert.ToInt64(charArray2[index + 1]) + num5;
        long num8 = num5 * 95L % 251L;
        long num9 = num6 + num7 * 256L;
        long num10 = Convert.ToInt64(charArray2[index + 2]) + num8;
        num3 = num8 * 95L % 251L;
        long num11 = num9 + num10 * 65536L;
        num2 = (num2 + num11) % 34000009L;
      }
      return num2;
    }

    public static bool ValidaCheckSumRep(string pis, string data, string nsr, long numero) => numero == CheckSum.GerarCheckSumRep(pis, data, nsr);
  }
}
