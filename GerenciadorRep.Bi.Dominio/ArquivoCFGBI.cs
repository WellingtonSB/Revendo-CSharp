// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.ArquivoCFGBI
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class ArquivoCFGBI
  {
    public int InserirArquivoCFG(ArquivoCFGEntity _arquivoCFGEnt)
    {
      try
      {
        return new ArquivoCFGDA().InserirArquivoCFG(_arquivoCFGEnt);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int AtualizarArquivoCFG(ArquivoCFGEntity _arquivoCFGEnt)
    {
      try
      {
        return new ArquivoCFGDA().AtualizarArquivoCFG(_arquivoCFGEnt);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public ArquivoCFGEntity PesquisarArquivoCFGPorRepID(int repID)
    {
      try
      {
        return new ArquivoCFGDA().PesquisarArquivoCFGPorRepID(repID);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void ExcluirArquivoCFGPorRepID(int repID)
    {
      try
      {
        new ArquivoCFGDA().ExcluirArquivoCFGPorRepID(repID);
      }
      catch (Exception ex)
      {
      }
    }
  }
}
