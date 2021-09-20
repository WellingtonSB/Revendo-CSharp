// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorColetaAFDRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using System.Text;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorColetaAFDRepPlus : TarefaAbstrata
  {
    private bool _coletaManual;
    private RepBase _rep;
    private string _serialNoREP = string.Empty;
    private RepAFD _repAFD;
    private int _numero_bytes_dump_MRP;
    private byte[] _bufferAFDRecebido = new byte[1];
    private int _ultimoNSRREP;
    private GerenciadorColetaAFDRepPlus.Estados _estadoRep;
    private int _totRegistrosColetados;
    public static GerenciadorColetaAFDRepPlus _gerenciadorColetaAFDRepPlus;

    public bool coletaManual
    {
      get => this._coletaManual;
      set => this._coletaManual = value;
    }

    public int NUMERO_BYTES_DUMP_MRP
    {
      get => this._numero_bytes_dump_MRP;
      set => this._numero_bytes_dump_MRP = value;
    }

    public int TotRegistrosColetados
    {
      get => this._totRegistrosColetados;
      set => this._totRegistrosColetados = value;
    }

    public event EventHandler<NotificarParaUsuarioEventArgs> OnNotificarConexaoEncerrada;

    public event EventHandler<NotificarTotBilhetesParaUsuarioEventArgs> OnNotificarBilhetesProcessadosParaUsuario;

    public event EventHandler<NotificarInicioColetaEventArgs> OnNotificarInicioColeta;

    public static GerenciadorColetaAFDRepPlus getInstance() => GerenciadorColetaAFDRepPlus._gerenciadorColetaAFDRepPlus != null ? GerenciadorColetaAFDRepPlus._gerenciadorColetaAFDRepPlus : new GerenciadorColetaAFDRepPlus();

    public static GerenciadorColetaAFDRepPlus getInstance(RepBase rep) => GerenciadorColetaAFDRepPlus._gerenciadorColetaAFDRepPlus != null ? GerenciadorColetaAFDRepPlus._gerenciadorColetaAFDRepPlus : new GerenciadorColetaAFDRepPlus(rep);

    public GerenciadorColetaAFDRepPlus()
    {
    }

    public GerenciadorColetaAFDRepPlus(RepBase rep)
    {
      this._rep = rep;
      this.NUMERO_BYTES_DUMP_MRP = 512;
      this._bufferAFDRecebido = new byte[this.NUMERO_BYTES_DUMP_MRP];
    }

    public override void IniciarProcesso()
    {
      if (!this.PermissaoColeta())
        return;
      NotificarInicioColetaEventArgs e = new NotificarInicioColetaEventArgs(600000L, this._rep.RepId);
      if (this.OnNotificarInicioColeta != null)
        this.OnNotificarInicioColeta((object) this, e);
      this.Conectar(this._rep);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (!this.PermissaoColeta())
        return;
      StringBuilder stringBuilder = new StringBuilder();
      switch (this._estadoRep)
      {
        case GerenciadorColetaAFDRepPlus.Estados.estAguardandoSolicitaUltimoNSR:
          if (envelope.Grp == (byte) 15 && envelope.Cmd == (byte) 107)
          {
            byte[] numArray = new byte[4];
            Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 6, (Array) numArray, 0, 4);
            for (int index = 0; index < 4; ++index)
              stringBuilder.Append(numArray[index].ToString("X").PadLeft(2, '0'));
            this._ultimoNSRREP = Convert.ToInt32(stringBuilder.ToString());
            if (this._ultimoNSRREP > this._repAFD.ultimoNSR)
            {
              this.SolicatarDadosAFD();
              break;
            }
            if (this._ultimoNSRREP == this._repAFD.ultimoNSR)
            {
              this.NotificarParaUsuario(Resources.msg_AFD_COLETADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              this.EncerrarConexao();
              break;
            }
            this.NotificarParaUsuario(Resources.msg_AFD_COLETADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            this.EncerrarConexao();
            break;
          }
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgERRO_NUM_TERMINAL_ERRADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
        case GerenciadorColetaAFDRepPlus.Estados.estAguardaDadosAFD:
          if (envelope.Grp == (byte) 16 && (envelope.Cmd == (byte) 102 || envelope.Cmd == (byte) 100))
          {
            ColetaAutomatica coletaAutomatica1 = new ColetaAutomatica();
            ColetaAutomatica coletaAutomatica2 = new ColetaAutomatica();
            ColetaAutomatica coletaAutomatica3 = coletaAutomatica1.PesquisarColetaAutomatica();
            if (!coletaAutomatica3.ColetaAutoHabilitada && !this.coletaManual && !coletaAutomatica3.ColetaProgHabilitada)
            {
              this.NotificarParaUsuario(Resources.msg_AFD_COLETADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              this.EncerrarConexao();
              break;
            }
            this.AnalizaBufferAFD(envelope);
            break;
          }
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgERRO_NUM_TERMINAL_ERRADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
      }
    }

    private void AnalizaBufferAFD(Envelope envelope)
    {
      if (!this.PermissaoColeta())
        return;
      try
      {
        byte[] byteArrayAux = new byte[this.NUMERO_BYTES_DUMP_MRP];
        List<RegistroAFD> registroAfdList = new List<RegistroAFD>();
        Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 6, (Array) byteArrayAux, 0, this.NUMERO_BYTES_DUMP_MRP);
        List<RegistroAFD> listaRegistrosAFD = RegistroAFDRepPlus.SepararBufferRegistrosAFD(byteArrayAux, this._ultimoNSRREP, this._repAFD);
        if (!new ConfiguracoesGerais().PesquisarConfigGerais().UnificaColetaAFD)
        {
          if (RegistroAFD.GravarRegistrosAFDComTransacao(listaRegistrosAFD, this._repAFD) >= 0)
          {
            RepAFD.AtualizarRegistroRepAFD(this._repAFD);
            RepAFD.AtualizarRegistroRepColetaAFD(this._repAFD);
          }
        }
        else if (RegistroAFD.GravarRegistrosColetaAFDComTransacao(listaRegistrosAFD, this._repAFD) >= 0)
          RepAFD.AtualizarRegistroRepColetaAFD(this._repAFD);
        this._totRegistrosColetados += listaRegistrosAFD.Count;
        NotificarTotBilhetesParaUsuarioEventArgs e = new NotificarTotBilhetesParaUsuarioEventArgs(this._rep.RepId, this._totRegistrosColetados.ToString());
        if (this.OnNotificarBilhetesProcessadosParaUsuario != null)
          this.OnNotificarBilhetesProcessadosParaUsuario((object) this, e);
        if (this._ultimoNSRREP > this._repAFD.ultimoNSR)
        {
          this.SolicatarDadosAFD();
        }
        else
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msg_AFD_COLETADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void VerificaBaseREP()
    {
      if (!this.PermissaoColeta())
        return;
      this._repAFD = RepAFD.InicializaBaseRepAFD(this._rep, this._rep.Serial);
      if (this._repAFD != null)
      {
        this._estadoRep = GerenciadorColetaAFDRepPlus.Estados.estSolicitaDadosAFD;
        this.NotificarParaUsuario(Resources.msgSOLICITANDO_DADOS_AFD1, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
        this.SolicitarUltimoNSR();
      }
      else
        this.NotificarParaUsuario(Resources.msg_FALHA_AO_INICIAR_BD, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private void SolicitarUltimoNSR()
    {
      if (!this.PermissaoColeta())
        return;
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
      this._estadoRep = GerenciadorColetaAFDRepPlus.Estados.estAguardandoSolicitaUltimoNSR;
      MsgTCPAplicacaoSolicUltimoNSRRepPlus ultimoNsrRepPlus = new MsgTCPAplicacaoSolicUltimoNSRRepPlus();
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) ultimoNsrRepPlus;
      this.ClienteSocket.Enviar(envelope, true);
    }

    private void SolicatarDadosAFD()
    {
      if (!this.PermissaoColeta())
        return;
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
      this._estadoRep = GerenciadorColetaAFDRepPlus.Estados.estAguardaDadosAFD;
      MsgTCPAplicacaoSolicDadosAFDPequenoRepPlus afdPequenoRepPlus = new MsgTCPAplicacaoSolicDadosAFDPequenoRepPlus(this._repAFD.GetPosicaoParaPesquisaRepPlus());
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) afdPequenoRepPlus;
      this.ClienteSocket.Enviar(envelope, true);
    }

    private bool PermissaoColeta()
    {
      ColetaAutomatica coletaAutomatica1 = new ColetaAutomatica();
      ColetaAutomatica coletaAutomatica2 = new ColetaAutomatica();
      ColetaAutomatica coletaAutomatica3 = coletaAutomatica1.PesquisarColetaAutomatica();
      string str = Environment.MachineName.ToUpper().Trim();
      try
      {
        return coletaAutomatica3.NomeMaquina.Trim() == "" || coletaAutomatica3.NomeMaquina == null || coletaAutomatica3.NomeMaquina.ToUpper().Trim() == str || coletaAutomatica3.Servidor;
      }
      catch
      {
        return false;
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.VerificaBaseREP();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string empty = string.Empty;
      switch (this._estadoRep)
      {
        default:
          this.EncerrarConexao();
          this.NotificarParaUsuario(empty, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
      }
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorColetaAFDRepPlus.Estados.estAguardandoSolicitaUltimoNSR:
          menssagem = Resources.msgNENHUMA_RESPOSTA_SOLIC_CONFIG_REP;
          break;
        case GerenciadorColetaAFDRepPlus.Estados.estSolicitaDadosAFD:
          menssagem = Resources.msgNENHUMA_RESPOSTA_SOLIC_DADOS_AFD;
          break;
        case GerenciadorColetaAFDRepPlus.Estados.estAguardaDadosAFD:
          menssagem = Resources.msgNENHUMA_RESPOSTA_SOLIC_DADOS_AFD;
          break;
        case GerenciadorColetaAFDRepPlus.Estados.estProcessaDadosAFD:
          menssagem = Resources.msgNENHUMA_RESPOSTA_SOLIC_DADOS_AFD;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioVerificaBaseREP,
      estAguardandoSolicitaUltimoNSR,
      estSolicitaDadosAFD,
      estAguardaDadosAFD,
      estProcessaDadosAFD,
    }
  }
}
