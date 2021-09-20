// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.ColetaAutomaticaSenior
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using TopData.Framework.Comunicacao;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class ColetaAutomaticaSenior : TarefaAbstrata
  {
    private const int INNER_REP_PLUS_LFD = 13;
    private const int INNER_REP_PLUS_BIO_PROX = 14;
    private const int INNER_REP_PLUS_BIO_BARRAS = 15;
    private const int INNER_REP_PLUS_LFD_DEMO = 16;
    private const int INNER_REP_PLUS_LC = 17;
    private const int INNER_REP_PLUS_LC_DEMO = 18;
    private const int INNER_REP_PLUS = 19;
    private const int INNER_REP_PLUS_DEMO = 20;
    private const int TASK_INOVA3 = 21;
    private static Dictionary<int, ResultadoColetaAuto> dicStatusColetaAuto = new Dictionary<int, ResultadoColetaAuto>();
    private static Dictionary<int, ResultadoColetaAuto> dicStatusColetaAutoAux = new Dictionary<int, ResultadoColetaAuto>();
    private static GerenciadorColetaAFD _gerenciadorColetaAFD;
    private static int _totBilhetesColetados;
    private bool _coletaEmAndamento;
    private static List<RepBase> lstRepsEntidade = new List<RepBase>();
    private static List<RepBase> lstRepsDominio = new List<RepBase>();
    private static NotificarColetaEventArgs eNotificaFimColeta = (NotificarColetaEventArgs) null;
    private static bool ocoreuErro = false;
    private static bool aguardando = false;
    private static string numeroSerie = "";
    private static string registro = "";
    private static string ultimoNSR = "";
    private static string posMem = "";
    private static int tipoRegistro = 0;
    private static GerenciadorColetaNSRSenior gerenciadorRep;
    private static GerenciadorColetaRepPlusNSRSenior gerenciadorRepPlus;

    public static GerenciadorColetaAFD gerenciadorColetaAFD
    {
      get => ColetaAutomaticaSenior._gerenciadorColetaAFD;
      set => ColetaAutomaticaSenior._gerenciadorColetaAFD = value;
    }

    public static int TotBilhetesColetados
    {
      get => ColetaAutomaticaSenior._totBilhetesColetados;
      set => ColetaAutomaticaSenior._totBilhetesColetados = value;
    }

    public bool ColetaEmAndamento
    {
      get => this._coletaEmAndamento;
      set => this._coletaEmAndamento = value;
    }

    public static List<RepBase> LstRepsEntidade
    {
      get => ColetaAutomaticaSenior.lstRepsEntidade;
      set => ColetaAutomaticaSenior.lstRepsEntidade = value;
    }

    public static List<RepBase> LstRepsDominio
    {
      get => ColetaAutomaticaSenior.lstRepsDominio;
      set => ColetaAutomaticaSenior.lstRepsDominio = value;
    }

    public static event EventHandler<NotificarInicioColetaEventArgs> OnNotificarInicioColeta;

    public static event EventHandler<EventArgs> OnNotificarFimDeColeta;

    public static event EventHandler<NotificarLstColetaEventArgs> OnNotificarColetaParcial;

    public static event EventHandler<NotificarParaUsuarioEventArgs> OnNotificarColetaRepEncerrada;

    private static void GerenciadorColetaAFD_OnNotificarBilhetesProcessadosParaUsuario(
      object sender,
      NotificarTotBilhetesParaUsuarioEventArgs e)
    {
      if (!ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(e.RepID))
        return;
      ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepID].TotRegistrosColetados = int.Parse(e.TotBilhetes);
      ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepID].Status = Constantes.RESULTADO_COLETA.Andamento;
      ColetaAutomaticaSenior.NotificaColetaParcial();
    }

    private static void NotificaColetaParcial()
    {
      lock (typeof (ResultadoColetaAuto))
      {
        try
        {
          List<ResultadoColetaAuto> lstResultado = new List<ResultadoColetaAuto>();
          lock (ColetaAutomaticaSenior.lstRepsDominio)
          {
            foreach (RepBase repBase in ColetaAutomaticaSenior.lstRepsDominio)
            {
              if (ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(repBase.RepId))
              {
                ResultadoColetaAuto resultadoColetaAuto = new ResultadoColetaAuto(repBase.RepId, repBase.Desc, ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].TotRegistrosColetados, ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].HoraInicio, ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].HoraFim, ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].Status);
                lstResultado.Add(resultadoColetaAuto);
                if (ColetaAutomaticaSenior.dicStatusColetaAutoAux.ContainsKey(repBase.RepId))
                {
                  ColetaAutomaticaSenior.dicStatusColetaAutoAux[repBase.RepId].TotRegistrosColetados = ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].TotRegistrosColetados;
                  ColetaAutomaticaSenior.dicStatusColetaAutoAux[repBase.RepId].HoraInicio = ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].HoraInicio;
                  ColetaAutomaticaSenior.dicStatusColetaAutoAux[repBase.RepId].HoraFim = ColetaAutomaticaSenior.dicStatusColetaAutoAux[repBase.RepId].HoraFim;
                  ColetaAutomaticaSenior.dicStatusColetaAutoAux[repBase.RepId].Status = ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].Status;
                }
              }
              else if (ColetaAutomaticaSenior.dicStatusColetaAutoAux.ContainsKey(repBase.RepId))
              {
                ResultadoColetaAuto resultadoColetaAuto = new ResultadoColetaAuto(repBase.RepId, repBase.Desc, ColetaAutomaticaSenior.dicStatusColetaAutoAux[repBase.RepId].TotRegistrosColetados, ColetaAutomaticaSenior.dicStatusColetaAutoAux[repBase.RepId].HoraInicio, ColetaAutomaticaSenior.dicStatusColetaAutoAux[repBase.RepId].HoraFim, ColetaAutomaticaSenior.dicStatusColetaAutoAux[repBase.RepId].Status);
                lstResultado.Add(resultadoColetaAuto);
              }
            }
            NotificarLstColetaEventArgs e = new NotificarLstColetaEventArgs(lstResultado);
            if (ColetaAutomaticaSenior.OnNotificarColetaParcial == null)
              return;
            ColetaAutomaticaSenior.OnNotificarColetaParcial((object) ColetaAutomaticaSenior.OnNotificarColetaParcial, e);
          }
        }
        catch
        {
        }
      }
    }

    private static void GerenciadorColetaAFD_OnNotificarConexaoEncerrada(
      object sender,
      NotificarParaUsuarioEventArgs e)
    {
      if (!ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(e.RepId))
        return;
      switch (e.EstadoResultadoProcesso)
      {
        case EnumEstadoResultadoFinalProcesso.finalizadoComSucesso:
          if (ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(e.RepId) && ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepId].Status != Constantes.RESULTADO_COLETA.Realizada)
          {
            ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepId].Status = Constantes.RESULTADO_COLETA.Realizada;
            ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepId].HoraFim = DateTime.Now.ToShortTimeString();
            ColetaAutomaticaSenior.VerificaFinalColeta();
            break;
          }
          break;
        case EnumEstadoResultadoFinalProcesso.finalizadoComFalha:
          if (ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(e.RepId) && ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepId].Status != Constantes.RESULTADO_COLETA.Falhou)
          {
            if (EnvironmentHelper.VerificarRepEmUsoServico(e.RepId))
            {
              if (ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(e.RepId))
                ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepId].Status = Constantes.RESULTADO_COLETA.Ocupado;
            }
            else if (ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(e.RepId))
              ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepId].Status = Constantes.RESULTADO_COLETA.Falhou;
            if (ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(e.RepId))
              ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepId].HoraFim = DateTime.Now.ToShortTimeString();
            ColetaAutomaticaSenior.VerificaFinalColeta();
            break;
          }
          break;
      }
      if (ColetaAutomaticaSenior.OnNotificarColetaRepEncerrada == null)
        return;
      ColetaAutomaticaSenior.OnNotificarColetaRepEncerrada(sender, e);
    }

    private static void VerificaFinalColeta()
    {
      bool flag = true;
      List<ResultadoColetaAuto> resultadoColetaAutoList = new List<ResultadoColetaAuto>();
      try
      {
        lock (ColetaAutomaticaSenior.lstRepsDominio)
        {
          foreach (RepBase repBase in ColetaAutomaticaSenior.lstRepsDominio)
          {
            if (ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(repBase.RepId))
            {
              if (ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].Status != Constantes.RESULTADO_COLETA.Realizada && ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].Status != Constantes.RESULTADO_COLETA.Falhou && ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].Status != Constantes.RESULTADO_COLETA.Ocupado)
              {
                flag = false;
                break;
              }
              resultadoColetaAutoList.Add(ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId]);
            }
          }
        }
      }
      catch
      {
      }
      ColetaAutomaticaSenior.NotificaColetaParcial();
      if (!flag)
        return;
      try
      {
        foreach (RepBase repBase in ColetaAutomaticaSenior.lstRepsDominio)
        {
          if (ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(repBase.RepId) && ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].Status != Constantes.RESULTADO_COLETA.Ocupado)
            EnvironmentHelper.LiberarRep(repBase.RepId);
        }
        if (ColetaAutomaticaSenior.OnNotificarFimDeColeta == null)
          return;
        ColetaAutomaticaSenior.OnNotificarFimDeColeta((object) ColetaAutomaticaSenior.OnNotificarFimDeColeta, EventArgs.Empty);
      }
      catch (Exception ex)
      {
      }
    }

    private static void GerenciadorColetaAFD_OnNotificarParaUsuario(
      object sender,
      NotificarParaUsuarioEventArgs e)
    {
      switch (e.EstadoResultadoProcesso)
      {
        case EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado:
          break;
        case EnumEstadoResultadoFinalProcesso.finalizadoComSucesso:
          if (!ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(e.RepId) || ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepId].Status == Constantes.RESULTADO_COLETA.Realizada)
            break;
          ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepId].Status = Constantes.RESULTADO_COLETA.Realizada;
          ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepId].HoraFim = DateTime.Now.ToShortTimeString();
          ColetaAutomaticaSenior.VerificaFinalColeta();
          break;
        case EnumEstadoResultadoFinalProcesso.finalizadoComFalha:
          if (ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(e.RepId))
          {
            if (EnvironmentHelper.VerificarRepEmUsoServico(e.RepId))
            {
              if (ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(e.RepId))
                ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepId].Status = Constantes.RESULTADO_COLETA.Ocupado;
            }
            else if (ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(e.RepId))
              ColetaAutomaticaSenior.dicStatusColetaAuto[e.RepId].Status = Constantes.RESULTADO_COLETA.Falhou;
          }
          ColetaAutomaticaSenior.VerificaFinalColeta();
          break;
        default:
          ColetaAutomaticaSenior.VerificaFinalColeta();
          break;
      }
    }

    public static bool VerificarTipoColeta(ColetaAutomatica _coletaAutomatica)
    {
      bool flag = false;
      if (_coletaAutomatica.ColetaAutoHabilitada)
        flag = ColetaAutomaticaSenior.GerenciarColetaAutomatica(_coletaAutomatica);
      if (_coletaAutomatica.ColetaProgHabilitada)
        flag = ColetaAutomaticaSenior.GerenciarColetaProgramada(_coletaAutomatica);
      return flag;
    }

    private static bool GerenciarColetaAutomatica(ColetaAutomatica _coletaAutomatica)
    {
      DateTime now = DateTime.Now;
      DateTime autoUltimaColeta = _coletaAutomatica.ColetaAutoUltimaColeta;
      bool flag = true;
      if (now.Subtract(autoUltimaColeta).TotalSeconds > (double) _coletaAutomatica.ColetaAutoIntervaloS || autoUltimaColeta > now || _coletaAutomatica.ColetaAutoIntervaloIndex == 25)
      {
        if (ColetaAutomaticaSenior.PermissaoColeta())
        {
          _coletaAutomatica.ColetaAutoUltimaColeta = now;
          ColetaAutomatica.AlterarColetaAutomatica(_coletaAutomatica);
          NotificarInicioColetaEventArgs e = new NotificarInicioColetaEventArgs(600000L);
          if (ColetaAutomaticaSenior.OnNotificarInicioColeta != null)
            ColetaAutomaticaSenior.OnNotificarInicioColeta((object) ColetaAutomaticaSenior.OnNotificarInicioColeta, e);
          ColetaAutomaticaSenior.ColetarRegistrosAFD();
        }
      }
      else
        flag = false;
      return flag;
    }

    private static bool GerenciarColetaProgramada(ColetaAutomatica _coletaAutomatica)
    {
      bool flag = true;
      string str = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
      if (str.Equals(_coletaAutomatica.ColetaProgHorario1) || str.Equals(_coletaAutomatica.ColetaProgHorario2) || str.Equals(_coletaAutomatica.ColetaProgHorario3) || str.Equals(_coletaAutomatica.ColetaProgHorario4))
      {
        if (_coletaAutomatica.ColetaAutoUltimaColeta.AddMinutes(1.0) < DateTime.Now)
        {
          _coletaAutomatica.ColetaAutoUltimaColeta = DateTime.Now;
          ColetaAutomatica.AlterarColetaAutomatica(_coletaAutomatica);
          NotificarInicioColetaEventArgs e = new NotificarInicioColetaEventArgs(600000L);
          if (ColetaAutomaticaSenior.OnNotificarInicioColeta != null)
            ColetaAutomaticaSenior.OnNotificarInicioColeta((object) ColetaAutomaticaSenior.OnNotificarInicioColeta, e);
          ColetaAutomaticaSenior.ColetarRegistrosAFD();
        }
        else
          flag = false;
      }
      else
        flag = false;
      return flag;
    }

    public static ColetaAutomatica PesquisarColetaAutomatica()
    {
      ColetaAutomatica coletaAutomatica = new ColetaAutomatica();
      ColetaAutomatica objColetaAuto;
      try
      {
        objColetaAuto = new ColetaAutomatica().PesquisarColetaAutomaticaSenior();
        ColetaAutomaticaSenior.AssociarIntervaloSegundos(objColetaAuto);
      }
      catch (Exception ex)
      {
        ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
        if (ConfigBD_BI.getInstance().tipoConexao != 0)
        {
          try
          {
            ServiceController serviceController = new ServiceController("InnerRepService");
            if (!serviceController.Status.Equals((object) ServiceControllerStatus.Stopped))
            {
              if (!serviceController.Status.Equals((object) ServiceControllerStatus.StopPending))
                goto label_7;
            }
            serviceController.Start();
          }
          catch
          {
          }
        }
label_7:
        throw ex;
      }
      return objColetaAuto;
    }

    public static bool AlterarColetaAutomaticaSenior(ColetaAutomatica _objColetaAuto)
    {
      try
      {
        return new ColetaAutomatica().AlterarColetaAutomaticaSenior(_objColetaAuto);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static bool ConfigurarSomenteServico(ColetaAutomatica _objColetaAuto)
    {
      try
      {
        return new ColetaAutomatica().ConfigurarSomenteServico(_objColetaAuto);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static void AssociarIntervaloSegundos(ColetaAutomatica objColetaAuto)
    {
      switch (objColetaAuto.ColetaAutoIntervaloIndex)
      {
        case 0:
          objColetaAuto.ColetaAutoIntervaloS = 600L;
          break;
        case 1:
          objColetaAuto.ColetaAutoIntervaloS = 900L;
          break;
        case 2:
          objColetaAuto.ColetaAutoIntervaloS = 1200L;
          break;
        case 3:
          objColetaAuto.ColetaAutoIntervaloS = 1500L;
          break;
        case 4:
          objColetaAuto.ColetaAutoIntervaloS = 1800L;
          break;
        case 5:
          objColetaAuto.ColetaAutoIntervaloS = 2100L;
          break;
        case 6:
          objColetaAuto.ColetaAutoIntervaloS = 2400L;
          break;
        case 7:
          objColetaAuto.ColetaAutoIntervaloS = 2700L;
          break;
        case 8:
          objColetaAuto.ColetaAutoIntervaloS = 3000L;
          break;
        case 9:
          objColetaAuto.ColetaAutoIntervaloS = 3300L;
          break;
        case 10:
          objColetaAuto.ColetaAutoIntervaloS = 3600L;
          break;
        case 11:
          objColetaAuto.ColetaAutoIntervaloS = 5400L;
          break;
        case 12:
          objColetaAuto.ColetaAutoIntervaloS = 7200L;
          break;
        case 13:
          objColetaAuto.ColetaAutoIntervaloS = 9000L;
          break;
        case 14:
          objColetaAuto.ColetaAutoIntervaloS = 10800L;
          break;
        case 15:
          objColetaAuto.ColetaAutoIntervaloS = 12600L;
          break;
        case 16:
          objColetaAuto.ColetaAutoIntervaloS = 14400L;
          break;
        case 17:
          objColetaAuto.ColetaAutoIntervaloS = 16200L;
          break;
        case 18:
          objColetaAuto.ColetaAutoIntervaloS = 18000L;
          break;
        case 19:
          objColetaAuto.ColetaAutoIntervaloS = 19800L;
          break;
        case 20:
          objColetaAuto.ColetaAutoIntervaloS = 21600L;
          break;
        case 21:
          objColetaAuto.ColetaAutoIntervaloS = 23400L;
          break;
        case 22:
          objColetaAuto.ColetaAutoIntervaloS = 25200L;
          break;
        case 23:
          objColetaAuto.ColetaAutoIntervaloS = 27000L;
          break;
        case 24:
          objColetaAuto.ColetaAutoIntervaloS = 28800L;
          break;
        default:
          objColetaAuto.ColetaAutoIntervaloS = 3600L;
          break;
      }
    }

    private static void PreencheListaDominio(
      List<RepBase> lstRepsEntidade,
      List<RepBase> lstRepsDominio)
    {
      lstRepsDominio.Clear();
      try
      {
        foreach (RepBase repBase1 in lstRepsEntidade)
        {
          RepBase repBase2 = new RepBase(repBase1.Ip, repBase1.Porta, repBase1.Numero, (ushort) repBase1.TerminalId, repBase1.ChaveComunicacao, (Empregador) null, repBase1.Local, repBase1.Desc + " - " + (repBase1.TipoConexao == 2 ? repBase1.Host : repBase1.Ip), repBase1.SenhaRelogio, repBase1.SenhaComunic, repBase1.SenhaBio, repBase1.Host, repBase1.TipoConexao, repBase1.nomeServidor, repBase1.DNS, repBase1.TipoConexaoDNS, repBase1.NomeRep);
          repBase2.RepId = repBase1.RepId;
          repBase2.PesquisarRepPorID(repBase1.RepId);
          repBase2.Desc = repBase1.Desc + " - " + (repBase1.TipoConexao == 2 ? repBase1.Host : repBase1.Ip);
          lstRepsDominio.Add(repBase2);
        }
      }
      catch
      {
      }
    }

    private static void ColetarRegistrosAFD()
    {
      try
      {
        lock (typeof (ResultadoColetaAuto))
        {
          ColetaAutomaticaSenior.lstRepsEntidade = new RepBase().PesquisarRepsSenior(true);
          foreach (RepBase repAux in ColetaAutomaticaSenior.lstRepsEntidade)
          {
            if (repAux.UltimoNSR == 0 && repAux.NsrInicial > repAux.UltimoNSR)
              ColetaAutomaticaSenior.ProcessoAtualizarRepNSRInicial(repAux);
          }
          ColetaAutomaticaSenior.lstRepsDominio = new List<RepBase>();
          if (ColetaAutomaticaSenior.dicStatusColetaAutoAux.Count <= 0)
            ColetaAutomaticaSenior.dicStatusColetaAutoAux = ColetaAutomaticaSenior.dicStatusColetaAuto;
          ColetaAutomaticaSenior.PreencheListaDominio(ColetaAutomaticaSenior.lstRepsEntidade, ColetaAutomaticaSenior.lstRepsDominio);
          try
          {
            foreach (RepBase repBase in ColetaAutomaticaSenior.lstRepsDominio)
            {
              if (!ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(repBase.RepId))
              {
                ResultadoColetaAuto resultadoColetaAuto = new ResultadoColetaAuto(repBase.RepId, repBase.Desc, 0, DateTime.Now.ToShortTimeString(), string.Empty, Constantes.RESULTADO_COLETA.Conectando);
                ColetaAutomaticaSenior.dicStatusColetaAuto.Add(repBase.RepId, resultadoColetaAuto);
              }
              else if (ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].Status == Constantes.RESULTADO_COLETA.Realizada || ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].Status == Constantes.RESULTADO_COLETA.Falhou)
              {
                ResultadoColetaAuto resultadoColetaAuto = new ResultadoColetaAuto(repBase.RepId, repBase.Desc, 0, DateTime.Now.ToShortTimeString(), string.Empty, Constantes.RESULTADO_COLETA.Conectando);
                ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId] = resultadoColetaAuto;
              }
              Thread.Sleep(1);
            }
            foreach (RepBase rep in ColetaAutomaticaSenior.lstRepsDominio)
            {
              GerenciadorColetaAFDRepPlus coletaAfdRepPlus = (GerenciadorColetaAFDRepPlus) null;
              GerenciadorColetaAFD gerenciadorColetaAfd = (GerenciadorColetaAFD) null;
              if (ColetaAutomaticaSenior.isRepPlus(rep.TipoTerminalId))
              {
                coletaAfdRepPlus = GerenciadorColetaAFDRepPlus.getInstance(rep);
                coletaAfdRepPlus.OnNotificarBilhetesProcessadosParaUsuario += new EventHandler<NotificarTotBilhetesParaUsuarioEventArgs>(ColetaAutomaticaSenior.GerenciadorColetaAFD_OnNotificarBilhetesProcessadosParaUsuario);
                coletaAfdRepPlus.OnNotificarParaUsuario += new EventHandler<NotificarParaUsuarioEventArgs>(ColetaAutomaticaSenior.GerenciadorColetaAFD_OnNotificarParaUsuario);
                coletaAfdRepPlus.OnNotificarConexaoEncerrada += new EventHandler<NotificarParaUsuarioEventArgs>(ColetaAutomaticaSenior.GerenciadorColetaAFD_OnNotificarConexaoEncerrada);
              }
              else
              {
                gerenciadorColetaAfd = GerenciadorColetaAFD.getInstance(rep);
                gerenciadorColetaAfd.OnNotificarBilhetesProcessadosParaUsuario += new EventHandler<NotificarTotBilhetesParaUsuarioEventArgs>(ColetaAutomaticaSenior.GerenciadorColetaAFD_OnNotificarBilhetesProcessadosParaUsuario);
                gerenciadorColetaAfd.OnNotificarParaUsuario += new EventHandler<NotificarParaUsuarioEventArgs>(ColetaAutomaticaSenior.GerenciadorColetaAFD_OnNotificarParaUsuario);
                gerenciadorColetaAfd.OnNotificarConexaoEncerrada += new EventHandler<NotificarParaUsuarioEventArgs>(ColetaAutomaticaSenior.GerenciadorColetaAFD_OnNotificarConexaoEncerrada);
              }
              try
              {
                if (!EnvironmentHelper.VerificarRepEmUsoServico(rep.RepId))
                {
                  if (ColetaAutomaticaSenior.dicStatusColetaAutoAux.ContainsKey(rep.RepId))
                  {
                    if (ColetaAutomaticaSenior.dicStatusColetaAuto[rep.RepId].Status != Constantes.RESULTADO_COLETA.Andamento && !EnvironmentHelper.VerificarRepEmUsoServico(rep.RepId))
                    {
                      if (ColetaAutomaticaSenior.isRepPlus(rep.TipoTerminalId))
                        coletaAfdRepPlus.IniciarProcesso();
                      else
                        gerenciadorColetaAfd.IniciarProcesso();
                    }
                  }
                  else if (ColetaAutomaticaSenior.isRepPlus(rep.TipoTerminalId))
                    coletaAfdRepPlus.IniciarProcesso();
                  else
                    gerenciadorColetaAfd.IniciarProcesso();
                }
                Thread.Sleep(10);
                Application.DoEvents();
              }
              catch (Exception ex)
              {
              }
            }
          }
          catch
          {
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private static void ProcessoAtualizarRepNSRInicial(RepBase repAux)
    {
      Empregador empregador = new Empregador();
      empregador.PesquisarEmpregadorDeUmREP(repAux.RepId);
      RepBase repBase = (RepBase) new RepBio(repAux.Ip, repAux.Porta, (ushort) 1, repAux.ChaveComunicacao, empregador, repAux.Local, repAux.SenhaRelogio, repAux.SenhaComunic, repAux.SenhaBio, repAux.repClient, repAux.portaServidor, 20, repAux.ConfiguracaoLeitorCpf);
      ColetaAutomaticaSenior.aguardando = true;
      ColetaAutomaticaSenior.AguardarResultado();
      ColetaAutomaticaSenior.GravarResultado(repAux, repBase);
      ColetaAutomaticaSenior.EncerrarConexao(repAux);
    }

    private static void AguardarResultado()
    {
      do
      {
        Thread.Sleep(10);
      }
      while (ColetaAutomaticaSenior.aguardando);
    }

    private static void GravarResultado(RepBase repAux, RepBase repBase)
    {
      if (ColetaAutomaticaSenior.ocoreuErro)
        return;
      ColetaAutomaticaSenior.GravarRegistro(repAux, repBase);
    }

    private static void EncerrarConexao(RepBase repAux)
    {
      if (repAux.TerminalId == 13 || repAux.TerminalId == 14 || repAux.TerminalId == 15)
        ColetaAutomaticaSenior.gerenciadorRepPlus.EncerrarConexao();
      else
        ColetaAutomaticaSenior.gerenciadorRep.EncerrarConexao();
    }

    private static void GravarRegistro(RepBase repAux, RepBase repBase)
    {
      repBase.RepId = repAux.RepId;
      RepAFD _repAFD = RepAFD.InicializaBaseRepAFD(repBase, ColetaAutomaticaSenior.numeroSerie);
      _repAFD.posMem = ColetaAutomaticaSenior.posMem;
      _repAFD.ultimoNSR = repAux.NsrInicial;
      _repAFD.repID = repAux.RepId;
      RepAFD.AtualizarRegistroRepColetaAFD(_repAFD);
      RegistroAFD.GravarRegistrosAFD(new List<RegistroAFD>()
      {
        new RegistroAFD()
        {
          dadosRegistro = ColetaAutomaticaSenior.registro,
          dtRegistro = ColetaAutomaticaSenior.ConverterDataRegistro(ColetaAutomaticaSenior.registro),
          NSR = repAux.NsrInicial,
          RepId = repAux.RepId,
          tipoRegistro = ColetaAutomaticaSenior.tipoRegistro
        }
      }, _repAFD);
    }

    public static void GravarRegistro(
      RepBase repBase,
      NotificarRegistroDoNsrSolicitadoSeniorEventArgs evento)
    {
      if (repBase == null)
        return;
      RepAFD _repAFD = RepAFD.InicializaBaseRepAFD(repBase, evento.NumeroSerialRep);
      _repAFD.posMem = evento.PosMem;
      _repAFD.ultimoNSR = Convert.ToInt32(evento.NsrSolicitado);
      _repAFD.repID = repBase.RepId;
      RepAFD.AtualizarRegistroRepColetaAFD(_repAFD);
      RegistroAFD.GravarRegistrosAFD(new List<RegistroAFD>()
      {
        new RegistroAFD()
        {
          dadosRegistro = evento.RegistroNsrSolicitado,
          dtRegistro = ColetaAutomaticaSenior.ConverterDataRegistro(evento.DataRegistro),
          NSR = Convert.ToInt32(evento.NsrSolicitado),
          RepId = repBase.RepId,
          tipoRegistro = evento.TipoRegistro
        }
      }, _repAFD);
    }

    private static string ConverterDataRegistro(string registro)
    {
      try
      {
        return registro != null && registro.Length > 18 ? registro.Substring(16, 2) + registro.Substring(12, 2) + registro.Substring(10, 2) + registro.Substring(18, 4) : registro;
      }
      catch (Exception ex)
      {
        return registro;
      }
    }

    private static void SolicitarRegistroNsr(int nsrSolicitado)
    {
      ColetaAutomaticaSenior.gerenciadorRep.OnNotificarColetaParaDriverSenior += new EventHandler<NotificarRegistroDoNsrSolicitadoSeniorEventArgs>(ColetaAutomaticaSenior.gerenciadorColeta_OnNotificarColetaParaRep);
      try
      {
        ColetaAutomaticaSenior.gerenciadorRep.SolicitaRegistroDoNsr(nsrSolicitado);
      }
      catch
      {
        ColetaAutomaticaSenior.aguardando = false;
        ColetaAutomaticaSenior.ocoreuErro = true;
      }
    }

    private static void gerenciadorColeta_OnNotificarColetaParaRep(
      object sender,
      NotificarRegistroDoNsrSolicitadoSeniorEventArgs e)
    {
      switch (e.ResultadoBuscaRegistroNsr)
      {
        case REPAFD.ResultadoBuscaRegistroNsr.REGISTRO_LIDO:
        case REPAFD.ResultadoBuscaRegistroNsr.ULTIMO_REGISTRO_LIDO:
          ColetaAutomaticaSenior.numeroSerie = e.NumeroSerialRep;
          ColetaAutomaticaSenior.registro = e.RegistroNsrSolicitado;
          ColetaAutomaticaSenior.ultimoNSR = e.NsrSolicitado;
          ColetaAutomaticaSenior.posMem = e.PosMem;
          ColetaAutomaticaSenior.tipoRegistro = e.TipoRegistro;
          ColetaAutomaticaSenior.ocoreuErro = false;
          break;
        case REPAFD.ResultadoBuscaRegistroNsr.REGISTRO_INEXISTENTE:
        case REPAFD.ResultadoBuscaRegistroNsr.ERRO:
          ColetaAutomaticaSenior.ocoreuErro = true;
          break;
      }
      ColetaAutomaticaSenior.aguardando = false;
    }

    public static bool AbortarColeta()
    {
      try
      {
        foreach (RepBase repBase in ColetaAutomaticaSenior.lstRepsDominio)
        {
          GerenciadorColetaAFDRepPlus coletaAfdRepPlus = (GerenciadorColetaAFDRepPlus) null;
          GerenciadorColetaAFD gerenciadorColetaAfd = (GerenciadorColetaAFD) null;
          if (ColetaAutomaticaSenior.isRepPlus(repBase.TipoTerminalId))
            coletaAfdRepPlus.EncerrarConexao();
          else
            gerenciadorColetaAfd.EncerrarConexao();
        }
      }
      catch (Exception ex)
      {
        return false;
      }
      return true;
    }

    public static bool PermissaoColeta()
    {
      ColetaAutomatica coletaAutomatica1 = new ColetaAutomatica();
      ColetaAutomatica coletaAutomatica2 = new ColetaAutomatica();
      ColetaAutomatica coletaAutomatica3 = coletaAutomatica1.PesquisarColetaAutomatica();
      string str = Environment.MachineName.ToUpper().Trim();
      try
      {
        return coletaAutomatica3.NomeMaquina.Trim() == "" || coletaAutomatica3.NomeMaquina == null || coletaAutomatica3.NomeMaquina.ToUpper().Trim() == str;
      }
      catch
      {
        return false;
      }
    }

    public static bool VerificarStatusPapel()
    {
      ColetaAutomatica coletaAutomatica1 = new ColetaAutomatica();
      ColetaAutomatica coletaAutomatica2 = new ColetaAutomatica();
      return coletaAutomatica1.PesquisarColetaAutomatica().VerificarStatusPapel;
    }

    public static bool VerificarColetaTravada()
    {
      bool flag1 = false;
      bool flag2 = false;
      if (ColetaAutomaticaSenior.dicStatusColetaAuto != null)
      {
        if (ColetaAutomaticaSenior.lstRepsEntidade != null)
        {
          try
          {
            foreach (RepBase repBase in ColetaAutomaticaSenior.lstRepsEntidade)
            {
              if (ColetaAutomaticaSenior.dicStatusColetaAuto.ContainsKey(repBase.RepId))
              {
                if (ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].Status != Constantes.RESULTADO_COLETA.Andamento && ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].Status != Constantes.RESULTADO_COLETA.Realizada)
                  flag1 = true;
                else if (ColetaAutomaticaSenior.dicStatusColetaAuto[repBase.RepId].Status != Constantes.RESULTADO_COLETA.Andamento)
                  flag2 = true;
              }
            }
          }
          catch
          {
          }
        }
      }
      if (flag2)
        return false;
      return !flag1 || true;
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e) => throw new NotImplementedException();

    public override void IniciarProcesso() => throw new NotImplementedException();

    public override void TratarEnvelope(Envelope envelope) => throw new NotImplementedException();

    public override void TratarTimeoutAck() => throw new NotImplementedException();

    public override void TratarNenhumaResposta() => throw new NotImplementedException();

    private static bool isRepPlus(int teminalId) => teminalId == 13 || teminalId == 14 || teminalId == 15 || teminalId == 16 || teminalId == 17 || teminalId == 18 || teminalId == 19 || teminalId == 20 || teminalId > 20;
  }
}
