// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.RepBaseAT
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class RepBaseAT : RepBase
  {
    public override RepBase PesquisarRepPorID(int repId)
    {
      RepBase repBase = (RepBase) null;
      try
      {
        repBase = new RepBaseAT().PesquisarRepPorID(repId);
        this.TipoTerminalId = repBase.TerminalId;
        this.ConfiguracaoId = repBase.ConfiguracaoId;
        this.RepId = repBase.RepId;
        this.IpAddress = repBase.Ip;
        this.Port = repBase.Porta;
        this.ChaveComunicacao = repBase.ChaveComunicacao;
        this.Local = repBase.Local;
        this.SenhaComunic = repBase.SenhaComunic;
        this.SenhaRelogio = repBase.SenhaRelogio;
        this.SenhaBio = repBase.SenhaBio;
        this.Serial = repBase.Serial;
        this.RepIdSenior = repBase.RepIdSenior;
        this.RepIdLeitoraSenior = repBase.RepIdLeitoraSenior;
        this.repClient = repBase.repClient;
        this.ipServidor = repBase.ipServidor;
        this.mascaraRede = repBase.mascaraRede;
        this.Gateway = repBase.Gateway;
        this.portaServidor = repBase.portaServidor;
        this.tempoEspera = repBase.tempoEspera;
        this.intervaloConexao = repBase.intervaloConexao;
        this.PortaVariavel = repBase.portaVariavel;
        this.Desc = repBase.Desc;
        this.nomeServidor = repBase.nomeServidor;
        this.grupoId = repBase.grupoId;
        this.redeRemota = repBase.redeRemota;
        this.Sincronizado = repBase.Sincronizado;
        this.TarefaAbstrata = new List<TarefaAbstrata>();
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
      return repBase;
    }

    public override List<RepBase> PesquisarReps()
    {
      List<RepBase> repBaseList = new List<RepBase>();
      try
      {
        repBaseList = new RepBaseAT().PesquisarReps();
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
      return repBaseList;
    }

    public SortableBindingList<RepBase> PesquisarRepPorEmpregador(
      int idEmpregador)
    {
      SortableBindingList<RepBase> sortableBindingList = (SortableBindingList<RepBase>) null;
      try
      {
        sortableBindingList = new RepBaseAT().PesquisarRepPorEmpregador(idEmpregador);
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
