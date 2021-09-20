// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.EmpregadoAT
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class EmpregadoAT : Empregado
  {
    public override SortableBindingList<Empregado> PesquisarEmpregadosPorEmpregadorOrdenadoPis(
      int idEmpregador)
    {
      SortableBindingList<Empregado> sortableBindingList = (SortableBindingList<Empregado>) null;
      try
      {
        sortableBindingList = new EmpregadoAT().PesquisarEmpregadosPorEmpregadorOrdenadoPis(idEmpregador);
      }
      catch (AppTopdataException ex)
      {
        if (Utils.TratarErroTopdata(ex))
          throw;
      }
      catch (Exception ex)
      {
        if (Utils.TratarException(ex))
          throw;
      }
      return sortableBindingList;
    }

    public override Empregado PesquisarEmpregadosPorPis(Empregado empregadoLista)
    {
      Empregado empregado = (Empregado) null;
      try
      {
        empregado = new EmpregadoAT().PesquisarEmpregadosPorPis(empregadoLista);
      }
      catch (AppTopdataException ex)
      {
        if (Utils.TratarErroTopdata(ex))
          throw;
      }
      catch (Exception ex)
      {
        if (Utils.TratarException(ex))
          throw;
      }
      return empregado;
    }

    public override SortableBindingList<Empregado> PesquisarEmpregadosPorEmpregador(
      int idEmpregador)
    {
      SortableBindingList<Empregado> sortableBindingList = (SortableBindingList<Empregado>) null;
      try
      {
        sortableBindingList = new EmpregadoAT().PesquisarEmpregadosPorEmpregador(idEmpregador);
      }
      catch (AppTopdataException ex)
      {
        if (Utils.TratarErroTopdata(ex))
          throw;
      }
      catch (Exception ex)
      {
        if (Utils.TratarException(ex))
          throw;
      }
      return sortableBindingList;
    }
  }
}
