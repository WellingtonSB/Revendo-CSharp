// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.Marcacao
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class Marcacao
  {
    private RepBase _rep;

    public Marcacao(RepBase rep) => this._rep = rep;

    public Marcacao()
    {
    }

    public uint PesquisarUltimoNsrBilheteDeUmRep(int repId)
    {
      uint num = 0;
      Bilhete bilhete = new Bilhete();
      try
      {
        num = bilhete.PesquisarUltimoNsrBilheteDeUmRep(repId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }
  }
}
