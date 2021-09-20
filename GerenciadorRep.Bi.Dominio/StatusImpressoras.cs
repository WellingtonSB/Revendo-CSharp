// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.StatusImpressoras
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class StatusImpressoras
  {
    private static Dictionary<int, ResultadoStatusImpressora> dicStatusImpressoras = new Dictionary<int, ResultadoStatusImpressora>();
    private static GerenciadorStatusImpressorasREP _gerenciadorStatusPapel;
    private static GerenciadorStatusImpressorasREPRepPlus _gerenciadorStatusPapelRepPlus;
    private bool _verificacaoEmAndamento;
    private static List<RepBase> lstRepsEntidade = new List<RepBase>();
    private static List<RepBase> lstRepsDominio = new List<RepBase>();

    public static ResultadoStatusImpressora PesquisarStatusNoBDSemPapel(
      int RepID)
    {
      StatusPapelDAO statusPapelDao = new StatusPapelDAO();
      try
      {
        return statusPapelDao.PesquisarStatusPapelSemPapel(RepID);
      }
      catch (Exception ex)
      {
      }
      return (ResultadoStatusImpressora) null;
    }

    public static GerenciadorStatusImpressorasREP gerenciadorStatusPapel
    {
      get => StatusImpressoras._gerenciadorStatusPapel;
      set => StatusImpressoras._gerenciadorStatusPapel = value;
    }

    public static GerenciadorStatusImpressorasREPRepPlus gerenciadorStatusPapelRepPlus
    {
      get => StatusImpressoras._gerenciadorStatusPapelRepPlus;
      set => StatusImpressoras._gerenciadorStatusPapelRepPlus = value;
    }

    public bool VerificacaoEmAndamento
    {
      get => this._verificacaoEmAndamento;
      set => this._verificacaoEmAndamento = value;
    }

    public static List<RepBase> LstRepsEntidade
    {
      get => StatusImpressoras.lstRepsEntidade;
      set => StatusImpressoras.lstRepsEntidade = value;
    }

    public static List<RepBase> LstRepsDominio
    {
      get => StatusImpressoras.lstRepsDominio;
      set => StatusImpressoras.lstRepsDominio = value;
    }

    public static event EventHandler<NotificarLstStatusImpressoraEventArgs> OnNotificarLstStatusImpressora;

    public static event EventHandler<NotificarLstStatusImpressoraEventArgs> OnNotificarStatusImpressoraBD;

    public static void VericarStatusPapelSenior()
    {
      try
      {
        ColetaAutomatica coletaAutomatica1 = new ColetaAutomatica();
        ColetaAutomatica coletaAutomatica2 = new ColetaAutomatica();
        ColetaAutomatica coletaAutomatica3 = coletaAutomatica1.PesquisarColetaAutomatica();
        if (!ColetaAutomatica.PermissaoColeta() && !coletaAutomatica3.Servidor || ColetaAutomatica.PermissaoColeta())
        {
          StatusImpressoras.lstRepsEntidade = new RepBase().PesquisarRepsSenior(true);
          StatusImpressoras.lstRepsDominio = new List<RepBase>();
          foreach (KeyValuePair<int, ResultadoStatusImpressora> statusImpressora in StatusImpressoras.dicStatusImpressoras)
            statusImpressora.Value.StatusProcesso = Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.NAO_INICIADO;
          StatusImpressoras.PreencheListaDominio(StatusImpressoras.lstRepsEntidade, StatusImpressoras.lstRepsDominio);
          foreach (RepBase repBase in StatusImpressoras.lstRepsDominio)
          {
            if (!StatusImpressoras.dicStatusImpressoras.ContainsKey(repBase.RepId))
            {
              ResultadoStatusImpressora statusImpressora = new ResultadoStatusImpressora(repBase.RepId, repBase.Desc, (string) null, (string) null, (string) null, repBase.grupoId, Constantes.STATUS_COMUNICACAO.INICIANDO, Constantes.STATUS_IMPRESSORA.INDEFINIDO, Constantes.STATUS_IMPRESSORA.INDEFINIDO, Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.NAO_INICIADO);
              StatusImpressoras.dicStatusImpressoras.Add(repBase.RepId, statusImpressora);
            }
          }
          List<ResultadoStatusImpressora> lstResultado = new List<ResultadoStatusImpressora>();
          GerenciadorStatusImpressorasREP statusImpressorasRep = (GerenciadorStatusImpressorasREP) null;
          GerenciadorStatusImpressorasREPRepPlus impressorasRepRepPlus = (GerenciadorStatusImpressorasREPRepPlus) null;
          foreach (RepBase rep in StatusImpressoras.lstRepsDominio)
          {
            if (StatusImpressoras.dicStatusImpressoras.ContainsKey(rep.RepId) && StatusImpressoras.dicStatusImpressoras[rep.RepId].StatusProcesso != Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.ANDAMENTO)
            {
              StatusImpressoras.dicStatusImpressoras[rep.RepId].StatusProcesso = Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.ANDAMENTO;
              GerenciadorColetaAFD.getInstance(rep);
              if (rep.TipoTerminalId == 13 || rep.TipoTerminalId == 14 || rep.TipoTerminalId == 15 || rep.TipoTerminalId == 16)
              {
                impressorasRepRepPlus = GerenciadorStatusImpressorasREPRepPlus.getInstance(rep);
                impressorasRepRepPlus.OnNotificarParaUsuario += new EventHandler<NotificarParaUsuarioEventArgs>(StatusImpressoras.GerenciadorStatusImpressoras_OnNotificarParaUsuario);
                impressorasRepRepPlus.OnNotificarStatusImpressora += new EventHandler<NotificarStatusImpressoraRepPlusEventArgs>(StatusImpressoras.GerenciadorStatusImpressoras_OnNotificarStatusImpressora);
              }
              else
              {
                statusImpressorasRep = GerenciadorStatusImpressorasREP.getInstance(rep);
                statusImpressorasRep.OnNotificarParaUsuario += new EventHandler<NotificarParaUsuarioEventArgs>(StatusImpressoras.GerenciadorStatusImpressoras_OnNotificarParaUsuario);
                statusImpressorasRep.OnNotificarStatusImpressora += new EventHandler<NotificarStatusImpressoraEventArgs>(StatusImpressoras.GerenciadorStatusImpressoras_OnNotificarStatusImpressora);
              }
              try
              {
                if (!EnvironmentHelper.VerificarRepEmUsoServico(rep.RepId))
                {
                  if (rep.TipoTerminalId == 13 || rep.TipoTerminalId == 14 || rep.TipoTerminalId == 15 || rep.TipoTerminalId == 16)
                    impressorasRepRepPlus.IniciarProcesso();
                  else
                    statusImpressorasRep.IniciarProcesso();
                }
                else
                {
                  StatusImpressoras.dicStatusImpressoras[rep.RepId].MsgImpressora1 = "Ocupado";
                  StatusImpressoras.dicStatusImpressoras[rep.RepId].MsgImpressora2 = "Ocupado";
                  StatusImpressoras.dicStatusImpressoras[rep.RepId].StatusImpressora1 = Constantes.STATUS_IMPRESSORA.OCUPADO;
                  StatusImpressoras.dicStatusImpressoras[rep.RepId].StatusImpressora2 = Constantes.STATUS_IMPRESSORA.OCUPADO;
                  StatusImpressoras.dicStatusImpressoras[rep.RepId].StatusImpressoraRepPlus1 = Constantes.STATUS_IMPRESSORA_REP_PLUS.OCUPADO;
                  StatusImpressoras.dicStatusImpressoras[rep.RepId].StatusImpressoraRepPlus2 = Constantes.STATUS_IMPRESSORA_REP_PLUS.NAO_INSTALADA;
                  StatusImpressoras.dicStatusImpressoras[rep.RepId].HorarioVerificacao = DateTime.Now.ToShortTimeString();
                  StatusImpressoras.dicStatusImpressoras[rep.RepId].StatusProcesso = Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO;
                  lstResultado.Add(StatusImpressoras.dicStatusImpressoras[rep.RepId]);
                }
              }
              catch (Exception ex)
              {
              }
            }
          }
          if (coletaAutomatica3.VerificarStatusPapel)
            return;
          NotificarLstStatusImpressoraEventArgs e = new NotificarLstStatusImpressoraEventArgs(lstResultado, true);
          if (StatusImpressoras.OnNotificarLstStatusImpressora == null)
            return;
          StatusImpressoras.OnNotificarLstStatusImpressora((object) StatusImpressoras.OnNotificarLstStatusImpressora, e);
        }
        else
        {
          List<ResultadoStatusImpressora> statusImpressoraList = new List<ResultadoStatusImpressora>();
          List<ResultadoStatusImpressora> lstResultado = new StatusPapelDAO().PesquisarStatusPapel();
          StatusImpressoras.dicStatusImpressoras.Clear();
          foreach (ResultadoStatusImpressora statusImpressora in lstResultado)
            StatusImpressoras.dicStatusImpressoras.Add(statusImpressora.RepID, statusImpressora);
          NotificarLstStatusImpressoraEventArgs e = new NotificarLstStatusImpressoraEventArgs(lstResultado, true);
          if (StatusImpressoras.OnNotificarLstStatusImpressora == null)
            return;
          StatusImpressoras.OnNotificarLstStatusImpressora((object) StatusImpressoras.OnNotificarLstStatusImpressora, e);
        }
      }
      catch (Exception ex)
      {
      }
    }

    private static void PreencheListaDominio(
      List<RepBase> lstRepsEntidade,
      List<RepBase> lstRepsDominio)
    {
      lstRepsDominio.Clear();
      foreach (RepBase repBase1 in lstRepsEntidade)
      {
        RepBase repBase2 = new RepBase(repBase1.Ip, repBase1.Porta, repBase1.Numero, (ushort) repBase1.TerminalId, repBase1.ChaveComunicacao, (Empregador) null, repBase1.Local, repBase1.Desc + " - " + (repBase1.TipoConexao == 2 ? repBase1.Host : repBase1.Ip), repBase1.SenhaRelogio, repBase1.SenhaComunic, repBase1.SenhaBio, repBase1.Host, repBase1.TipoConexao, repBase1.nomeServidor, repBase1.DNS, repBase1.TipoConexaoDNS, repBase1.NomeRep);
        repBase2.RepId = repBase1.RepId;
        repBase2.PesquisarRepPorID(repBase1.RepId);
        repBase2.Desc = repBase1.Desc + " - " + (repBase1.TipoConexao == 2 ? repBase1.Host : repBase1.Ip);
        lstRepsDominio.Add(repBase2);
      }
    }

    private static void GerenciadorStatusImpressoras_OnNotificarParaUsuario(
      object sender,
      NotificarParaUsuarioEventArgs e)
    {
      switch (e.EstadoResultadoProcesso)
      {
        case EnumEstadoResultadoFinalProcesso.finalizadoComSucesso:
          if (StatusImpressoras.dicStatusImpressoras.ContainsKey(e.RepId))
          {
            StatusImpressoras.dicStatusImpressoras[e.RepId].StatusProcesso = Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO;
            break;
          }
          break;
        case EnumEstadoResultadoFinalProcesso.finalizadoComFalha:
          if (StatusImpressoras.dicStatusImpressoras.ContainsKey(e.RepId))
          {
            StatusImpressoras.dicStatusImpressoras[e.RepId].MsgImpressora1 = "Sem resposta";
            StatusImpressoras.dicStatusImpressoras[e.RepId].MsgImpressora2 = "Sem resposta";
            StatusImpressoras.dicStatusImpressoras[e.RepId].StatusImpressora1 = Constantes.STATUS_IMPRESSORA.INDEFINIDO;
            StatusImpressoras.dicStatusImpressoras[e.RepId].StatusImpressora2 = Constantes.STATUS_IMPRESSORA.INDEFINIDO;
            StatusImpressoras.dicStatusImpressoras[e.RepId].StatusProcesso = Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO;
            break;
          }
          break;
      }
      if (!EnvironmentHelper.VerificarRepEmUsoServico(e.RepId) || !StatusImpressoras.dicStatusImpressoras.ContainsKey(e.RepId))
        return;
      StatusImpressoras.dicStatusImpressoras[e.RepId].MsgImpressora1 = "Ocupado";
      StatusImpressoras.dicStatusImpressoras[e.RepId].MsgImpressora2 = "Ocupado";
      StatusImpressoras.dicStatusImpressoras[e.RepId].StatusImpressora1 = Constantes.STATUS_IMPRESSORA.OCUPADO;
      StatusImpressoras.dicStatusImpressoras[e.RepId].StatusImpressora2 = Constantes.STATUS_IMPRESSORA.OCUPADO;
      StatusImpressoras.dicStatusImpressoras[e.RepId].StatusProcesso = Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO;
    }

    private static void GerenciadorStatusImpressoras_OnNotificarStatusImpressora(
      object sender,
      NotificarStatusImpressoraEventArgs e)
    {
      bool flag = true;
      List<ResultadoStatusImpressora> statusImpressoraList = new List<ResultadoStatusImpressora>();
      try
      {
        if (StatusImpressoras.dicStatusImpressoras.ContainsKey(e.Resultado.RepID))
          StatusImpressoras.dicStatusImpressoras[e.Resultado.RepID] = e.Resultado;
        foreach (RepBase repBase in StatusImpressoras.lstRepsDominio)
        {
          if (StatusImpressoras.dicStatusImpressoras.ContainsKey(repBase.RepId))
          {
            if (StatusImpressoras.dicStatusImpressoras[repBase.RepId].StatusProcesso != Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO)
            {
              flag = false;
              statusImpressoraList.Add(StatusImpressoras.dicStatusImpressoras[repBase.RepId]);
            }
            else
              statusImpressoraList.Add(StatusImpressoras.dicStatusImpressoras[repBase.RepId]);
          }
        }
        if (!flag)
          return;
        NotificarLstStatusImpressoraEventArgs e1 = new NotificarLstStatusImpressoraEventArgs(statusImpressoraList, true);
        if (StatusImpressoras.OnNotificarLstStatusImpressora != null)
          StatusImpressoras.OnNotificarLstStatusImpressora((object) StatusImpressoras.OnNotificarLstStatusImpressora, e1);
        StatusImpressoras.GravaStatusNoBD(statusImpressoraList);
      }
      catch
      {
      }
    }

    private static void GerenciadorStatusImpressoras_OnNotificarStatusImpressora(
      object sender,
      NotificarStatusImpressoraRepPlusEventArgs e)
    {
      bool flag = true;
      List<ResultadoStatusImpressora> statusImpressoraList = new List<ResultadoStatusImpressora>();
      try
      {
        if (StatusImpressoras.dicStatusImpressoras.ContainsKey(e.Resultado.RepID))
          StatusImpressoras.dicStatusImpressoras[e.Resultado.RepID] = e.Resultado;
        foreach (RepBase repBase in StatusImpressoras.lstRepsDominio)
        {
          if (StatusImpressoras.dicStatusImpressoras.ContainsKey(repBase.RepId))
          {
            if (StatusImpressoras.dicStatusImpressoras[repBase.RepId].StatusProcesso != Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO)
            {
              flag = false;
              statusImpressoraList.Add(StatusImpressoras.dicStatusImpressoras[repBase.RepId]);
            }
            else
              statusImpressoraList.Add(StatusImpressoras.dicStatusImpressoras[repBase.RepId]);
          }
        }
        if (!flag)
          return;
        NotificarLstStatusImpressoraEventArgs e1 = new NotificarLstStatusImpressoraEventArgs(statusImpressoraList, true);
        if (StatusImpressoras.OnNotificarLstStatusImpressora != null)
          StatusImpressoras.OnNotificarLstStatusImpressora((object) StatusImpressoras.OnNotificarLstStatusImpressora, e1);
        StatusImpressoras.GravaStatusNoBD(statusImpressoraList);
      }
      catch
      {
      }
    }

    public static void GravaStatusNoBD(int repId, List<ResultadoStatusImpressora> lstResult)
    {
      StatusPapelDAO statusPapelDao = new StatusPapelDAO();
      try
      {
        statusPapelDao.ExcluirStatusPapel(repId);
      }
      catch (Exception ex)
      {
      }
      try
      {
        statusPapelDao.InserirListaStatusPapel(lstResult);
      }
      catch (Exception ex)
      {
      }
    }

    public static void GravaStatusNoBD(List<ResultadoStatusImpressora> lstResult)
    {
      StatusPapelDAO statusPapelDao = new StatusPapelDAO();
      try
      {
        statusPapelDao.ExcluirStatusPapel();
      }
      catch (Exception ex)
      {
      }
      try
      {
        statusPapelDao.InserirListaStatusPapel(lstResult);
      }
      catch (Exception ex)
      {
      }
    }

    public static void ExluirStatusNoBD()
    {
      StatusPapelDAO statusPapelDao = new StatusPapelDAO();
      try
      {
        statusPapelDao.ExcluirStatusPapel();
      }
      catch (Exception ex)
      {
      }
    }

    public static void ExluirStatusRepNoBD(int repId)
    {
      StatusPapelDAO statusPapelDao = new StatusPapelDAO();
      try
      {
        statusPapelDao.ExcluirStatusPapel(repId);
      }
      catch (Exception ex)
      {
      }
    }

    public static ResultadoStatusImpressora PesquisarStatusNoBDById(
      int RepId)
    {
      StatusPapelDAO statusPapelDao = new StatusPapelDAO();
      ResultadoStatusImpressora statusImpressora = new ResultadoStatusImpressora();
      try
      {
        statusImpressora = statusPapelDao.PesquisarStatusPapelById(RepId);
      }
      catch (Exception ex)
      {
      }
      return statusImpressora;
    }

    public static List<ResultadoStatusImpressora> PesquisarStatusPapel()
    {
      StatusPapelDAO statusPapelDao = new StatusPapelDAO();
      List<ResultadoStatusImpressora> statusImpressoraList = new List<ResultadoStatusImpressora>();
      try
      {
        statusImpressoraList = statusPapelDao.PesquisarStatusPapel();
      }
      catch (Exception ex)
      {
      }
      return statusImpressoraList;
    }
  }
}
