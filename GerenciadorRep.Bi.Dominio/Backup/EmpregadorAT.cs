// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.EmpregadorAT
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
  public class EmpregadorAT : Empregador
  {
    public override SortableBindingList<Empregador> PesquisarEmpregadores()
    {
      SortableBindingList<Empregador> sortableBindingList = (SortableBindingList<Empregador>) null;
      try
      {
        sortableBindingList = new Empregador().PesquisarEmpregadores();
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

    public override Empregador PesquisarEmpregadorPorID(int idEmpregador)
    {
      Empregador empregador = (Empregador) null;
      try
      {
        empregador = new EmpregadorAt().PesquisarEmpregadorPorID(idEmpregador);
        this.Descricao = empregador.EmpregadorDesc;
        this.EmpregadorId = empregador.EmpregadorId;
        this.RazaoSocial = empregador.RazaoSocial;
        this.Cnpj = empregador.Cnpj;
        this.Cpf = empregador.Cpf;
        this.Cei = empregador.Cei;
        this.isCnpj = empregador.isCnpj;
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
      return empregador;
    }

    public override SortableBindingList<Empregador> PesquisarEmpregadoresComAssociados()
    {
      SortableBindingList<Empregador> sortableBindingList = (SortableBindingList<Empregador>) null;
      try
      {
        sortableBindingList = new EmpregadorAt().PesquisarEmpregadores();
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
