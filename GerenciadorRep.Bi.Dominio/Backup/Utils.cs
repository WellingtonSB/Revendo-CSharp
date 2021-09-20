// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.Utils
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class Utils
  {
    public static bool TratarException(Exception ex)
    {
      string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
      ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
      return ExceptionPolicy.HandleException(ex, "Politica Dominio");
    }

    public static bool TratarErroTopdata(AppTopdataException ex) => ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado");
  }
}
