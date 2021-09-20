// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.ManutencaoDB
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class ManutencaoDB
  {
    public static void CompactarBase(string caminhoDBBak)
    {
      try
      {
        new ManutencaoDB().CompactAccessDB(caminhoDBBak);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void CopiarBase(ManutencaoDB objManutencaoDB)
    {
      try
      {
        new ManutencaoDB().CopiarBase(objManutencaoDB);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static ManutencaoDB PesquisarManutencaoDB()
    {
      try
      {
        return new ManutencaoDB().PesquisarManutencaoDB();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static bool AlterarManutencaoDB(ManutencaoDB _objManutencaoDB)
    {
      try
      {
        return new ManutencaoDB().AlterarManutencaoDB(_objManutencaoDB);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static bool CriarBKPSqlServer()
    {
      try
      {
        ManutencaoDB manutencaoDb = new ManutencaoDB();
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int TestarConexaoDB() => new ManutencaoDB().TestarConexaoDB();
  }
}
