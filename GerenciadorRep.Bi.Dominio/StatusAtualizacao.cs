// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.StatusAtualizacao
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class StatusAtualizacao
  {
    public SortableBindingList<StatusAtualizacao> PesquisarStatusAtualizacaoNoBDByRep(
      int RepId)
    {
      Empregado empregado = new Empregado();
      try
      {
        return empregado.PesquisarStatusAtualizacaoByRep(RepId);
      }
      catch (Exception ex)
      {
        return (SortableBindingList<StatusAtualizacao>) null;
      }
    }

    public SortableBindingList<StatusAtualizacao> PesquisarTodosStatusAtualizacaoNoBD(
      int idEmpregador)
    {
      Empregado empregado = new Empregado();
      try
      {
        return empregado.PesquisarTodosStatusAtualizacao(idEmpregador);
      }
      catch
      {
        return (SortableBindingList<StatusAtualizacao>) null;
      }
    }

    public int InserirLogAtualizacao(StatusAtualizacao status)
    {
      Empregado empregado = new Empregado();
      try
      {
        return empregado.InserirLogAtualizacao(status);
      }
      catch (Exception ex)
      {
        return 0;
      }
    }

    public void ExluirStatusAtualizacaoNoBD()
    {
      Empregado empregado = new Empregado();
      try
      {
        empregado.ExcluirStatusAtualizacao();
      }
      catch (Exception ex)
      {
      }
    }

    public void ExluirStatusAtualizacaoNoBDByRepId(int repId)
    {
      Empregado empregado = new Empregado();
      try
      {
        empregado.ExcluirStatusAtualizacaoByRepId(repId);
      }
      catch (Exception ex)
      {
      }
    }

    public void ExluirStatusAtualizacaoNoBDByEmpregadoId(int empregadoId)
    {
      Empregado empregado = new Empregado();
      try
      {
        empregado.ExcluirStatusAtualizacaoByEmpregadoId(empregadoId);
      }
      catch (Exception ex)
      {
      }
    }
  }
}
