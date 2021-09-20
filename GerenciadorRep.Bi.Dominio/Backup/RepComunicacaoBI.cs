// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.RepComunicacaoBI
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public static class RepComunicacaoBI
  {
    public static int InserirRepComunicacao(RepComunicacao RepComunicEnt)
    {
      int num = 0;
      try
      {
        num = new RepComunicacaoDAO().InserirRepComunicacao(RepComunicEnt);
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

    public static int AlterarRepComunicacao(RepComunicacao RepComunicEnt)
    {
      int num = 0;
      try
      {
        num = new RepComunicacaoDAO().AlterarRepComunicacao(RepComunicEnt);
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

    public static int ExcluirRepComunicacao(int RepID)
    {
      int num = 0;
      try
      {
        num = new RepComunicacaoDAO().ExcluirRepComunicacao(RepID);
      }
      catch
      {
      }
      return num;
    }

    public static int ExcluirMaquinaComunicacao(int RepId)
    {
      int num = 0;
      try
      {
        num = new RepComunicacaoDAO().ExcluirMaquinaComunicacao(RepId);
      }
      catch (Exception ex)
      {
      }
      return num;
    }

    public static int ExcluirMaquinaComunicacao(string NomeMaquina)
    {
      int num = 0;
      try
      {
        num = new RepComunicacaoDAO().ExcluirMaquinaComunicacao(NomeMaquina);
      }
      catch (Exception ex)
      {
      }
      return num;
    }

    public static RepComunicacao PesquisarRepComunicacao(int RepID)
    {
      RepComunicacao repComunicacao = (RepComunicacao) null;
      try
      {
        repComunicacao = new RepComunicacaoDAO().PesquisarRepComunicacao(RepID);
      }
      catch
      {
      }
      return repComunicacao;
    }
  }
}
