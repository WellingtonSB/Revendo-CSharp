// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorColetaNSRSenior
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorColetaNSRSenior : TarefaAbstrata
  {
    public const string DATA_REGISTRO_NULA = "0101010101";
    public const int TIPO_REGISTRO_NULO = 99;
    private RepBase rep;
    private string _msg = "";
    private string _serialNoREP = string.Empty;
    private RepAFD _repAFD = new RepAFD();
    private int _numero_bytes_dump_MRP;
    private byte[] _bufferAFDRecebido = new byte[1];
    private int _ultimoNSRREP;
    private int nsrSolicitado;
    private REPAFD.ResultadoBuscaRegistroNsr _resultadoBuscaRegistroNsr;
    private bool _novoNsrSolicitado;
    private Envelope _envelopeAnterior = new Envelope();
    private ushort _numTerminal;
    private ClienteAssincrono _clienteSocket;
    private Timer _timerMsgAplicacao;
    private GerenciadorColetaNSRSenior.Estados _estadoRep;
    private int _totRegistrosColetados;
    public static GerenciadorColetaNSRSenior _gerenciadorColetaNSRSenior;

    public event EventHandler<NotificarInicioColetaEventArgs> OnNotificarInicioColeta;

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

    public event EventHandler<NotificarRegistroDoNsrSolicitadoSeniorEventArgs> OnNotificarColetaParaDriverSenior;

    public static GerenciadorColetaNSRSenior getInstance() => GerenciadorColetaNSRSenior._gerenciadorColetaNSRSenior != null ? GerenciadorColetaNSRSenior._gerenciadorColetaNSRSenior : new GerenciadorColetaNSRSenior();

    public static GerenciadorColetaNSRSenior getInstance(RepBase rep) => GerenciadorColetaNSRSenior._gerenciadorColetaNSRSenior != null ? GerenciadorColetaNSRSenior._gerenciadorColetaNSRSenior : new GerenciadorColetaNSRSenior(rep);

    public GerenciadorColetaNSRSenior()
    {
    }

    public GerenciadorColetaNSRSenior(RepBase rep)
    {
      this.rep = rep;
      this._clienteSocket = rep.ClienteSocket;
      this._numTerminal = rep.NumTerminal;
      this._estadoRep = GerenciadorColetaNSRSenior.Estados.estInicial;
      this.InicializarEventosSocket();
      this.InicializarTimer();
      this.NUMERO_BYTES_DUMP_MRP = !RegistrySingleton.GetInstance().REDE_REMOTA ? 2048 : 512;
      this._bufferAFDRecebido = new byte[this.NUMERO_BYTES_DUMP_MRP];
    }

    private void InicializarEventosSocket()
    {
      this._clienteSocket.OnChegouEnvelope += new EventHandler<ChegouEnvelopeEventArgs>(this.clienteSocket_OnChegouEnvelope);
      this._clienteSocket.OnChegouAck += new EventHandler<ChegouAckEventArgs>(this.clienteSocket_OnChegouAck);
      this._clienteSocket.OnTimeoutEnvioAck += new EventHandler<TimeoutEnvioAckEventArgs>(this.clienteSocket_OnTimeoutEnvioAck);
      this._clienteSocket.OnSocketExceptionEvento += new EventHandler<SocketExceptionEventoEventArgs>(this.clienteSocket_OnSocketExceptionEvento);
    }

    private void InicializarTimer()
    {
      this._timerMsgAplicacao = new Timer();
      this._timerMsgAplicacao.AutoReset = false;
      this._timerMsgAplicacao.Elapsed += new ElapsedEventHandler(this.timerMsgAplicacao_Elapsed);
    }

    public void SolicitaRegistroDoNsr(int nsrSolicitado) => this.nsrSolicitado = nsrSolicitado;

    public override void IniciarProcesso() => this.Conectar(this.rep);

    private bool ValidarChaveComunicacao() => this.rep.ChaveComunicacao.Trim().Length >= 16;

    private void Conectar()
    {
      try
      {
        this._estadoRep = GerenciadorColetaNSRSenior.Estados.estConexao;
        this.ClienteSocket.Conectar(this.rep.repClient, this.rep.portaServidor, this.rep.tempoEspera);
        this._numTerminal = this.rep.NumTerminal;
        this.EnviarAutenticacao();
      }
      catch (SocketException ex)
      {
        if (ex.ErrorCode == 10060)
          throw;
        else if (ex.ErrorCode == 10061)
          throw;
        else
          throw;
      }
      catch (Exception ex)
      {
        this._estadoRep = GerenciadorColetaNSRSenior.Estados.estInicial;
        this._msg = Resources.errABRIR_CONEXAO;
        ex.Data.Add((object) "mensagem", (object) this._msg);
        if (!ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          return;
        throw;
      }
    }

    private void EnviarAutenticacao()
    {
      try
      {
        this._estadoRep = GerenciadorColetaNSRSenior.Estados.estEnvioAutenticacao;
        this.ClienteSocket.Enviar(new Envelope(this._clienteSocket.NsuSw, (byte) 0, this._clienteSocket.NsuRx, ushort.MaxValue, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoAutenticacao()
          {
            ChaveComunicacao = this.rep.ChaveComunicacao
          }
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    public new void EncerrarConexao()
    {
      try
      {
        this._timerMsgAplicacao.Enabled = false;
        this._estadoRep = GerenciadorColetaNSRSenior.Estados.estInicial;
        if (!this._clienteSocket.Conectado)
          return;
        this._clienteSocket.Desconectar();
      }
      catch (Exception ex)
      {
      }
    }

    private void AnalisarEnvelope(Envelope envelope)
    {
      this._timerMsgAplicacao.Enabled = false;
      switch (this._estadoRep)
      {
        case GerenciadorColetaNSRSenior.Estados.estEnvioAutenticacao:
          if (envelope.Grp != (byte) 1 || envelope.Cmd != (byte) 1)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
          {
            byte[] term = envelope.Term;
            Array.Reverse((Array) term);
            if ((int) BitConverter.ToUInt16(term, 0) == (int) this._numTerminal)
            {
              this.EnviarVerificaConfiguracao();
              break;
            }
            this.EncerrarConexao();
            this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgCHAVE_COMUNICACAO_ERRADA, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardandoConfigREP:
          byte[] bytes = new byte[17];
          if (envelope.Grp != (byte) 2 || envelope.Cmd != (byte) 1)
            break;
          byte[] term1 = envelope.Term;
          Array.Reverse((Array) term1);
          if ((int) BitConverter.ToUInt16(term1, 0) == (int) this._numTerminal)
          {
            Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 2, (Array) bytes, 0, 17);
            for (int index = 0; index < bytes.Length; ++index)
              bytes[index] += (byte) 48;
            this._serialNoREP = Encoding.Default.GetString(bytes);
            this.SolicitarUltimoNSR();
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardandoSolicitaUltimoNSR:
          if (envelope.Grp == (byte) 250 && envelope.Cmd == (byte) 105)
          {
            byte[] numArray = new byte[4];
            Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 6, (Array) numArray, 0, 4);
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < 4; ++index)
              stringBuilder.Append(numArray[index].ToString("X").PadLeft(2, '0'));
            this._ultimoNSRREP = Convert.ToInt32(stringBuilder.ToString());
            if (this.nsrSolicitado <= this._ultimoNSRREP && this.nsrSolicitado != 0)
            {
              this.SolicitarDadosAFD(true, 0, 0);
              break;
            }
            this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.REGISTRO_INEXISTENTE, Resources.msgERRO_NSR_NAO_EXISTE, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
            this.EncerrarConexao();
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardaDadosAFD:
          if (envelope.Grp == (byte) 250 && (envelope.Cmd == (byte) 102 || envelope.Cmd == (byte) 107))
          {
            this.AnalisaBufferAFD(envelope);
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardaNovaSolicitacaoNsr:
          if (!this._novoNsrSolicitado)
            break;
          this._novoNsrSolicitado = false;
          this.AnalisaBufferAFD(envelope);
          break;
      }
    }

    private void SolicitarUltimoNSR()
    {
      Envelope envelope = this.CriarEnvelope();
      this._estadoRep = GerenciadorColetaNSRSenior.Estados.estAguardandoSolicitaUltimoNSR;
      MsgTCPAplicacaoSolicUltimoNSR aplicacaoSolicUltimoNsr = new MsgTCPAplicacaoSolicUltimoNSR();
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) aplicacaoSolicUltimoNsr;
      this.ClienteSocket.Enviar(envelope, true);
    }

    private void SolicitarDadosAFD(
      bool primeiraChamadaBusca,
      int posicaoUltimoNsrBloco,
      int ultimoNsrDoBloco)
    {
      Envelope envelope = this.CriarEnvelope();
      this._estadoRep = GerenciadorColetaNSRSenior.Estados.estAguardaDadosAFD;
      if (primeiraChamadaBusca)
      {
        this._repAFD.posMem = "000 000 000 000";
        this._repAFD.AtualizaPosicao(0);
      }
      else
      {
        this._repAFD.AtualizaPosicao(posicaoUltimoNsrBloco);
        this._repAFD.AtualizaPosicao(ultimoNsrDoBloco != 0 ? (this.nsrSolicitado - 1 - ultimoNsrDoBloco) * 15 : 2048);
      }
      if (RegistrySingleton.GetInstance().REDE_REMOTA)
      {
        MsgTCPAplicacaoSolicDadosAFDPequeno solicDadosAfdPequeno = new MsgTCPAplicacaoSolicDadosAFDPequeno(this._repAFD.GetPosicaoParaPesquisa());
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) solicDadosAfdPequeno;
      }
      else
      {
        MsgTCPAplicacaoSolicDadosAFDGrande solicDadosAfdGrande = new MsgTCPAplicacaoSolicDadosAFDGrande(this._repAFD.GetPosicaoParaPesquisa());
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) solicDadosAfdGrande;
      }
      this.ClienteSocket.Enviar(envelope, true);
    }

    private void AnalisaBufferAFD(Envelope envelope)
    {
      byte[] byteArrayAux = new byte[this.NUMERO_BYTES_DUMP_MRP];
      int ultimoNsrDoBloco = 0;
      int posicaoUltimoNsrBloco = 0;
      RegistroAFD registroAfd1 = new RegistroAFD();
      this._envelopeAnterior = envelope;
      Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 6, (Array) byteArrayAux, 0, this.NUMERO_BYTES_DUMP_MRP);
      for (int index = 0; index < byteArrayAux.Length; index += 2)
        Array.Reverse((Array) byteArrayAux, index, 2);
      switch (RegistroAFD.BlocoLidoContemNsrSolicitadoSenior(byteArrayAux, this.nsrSolicitado, out posicaoUltimoNsrBloco, out ultimoNsrDoBloco))
      {
        case REPAFD.BlocoMemoria.SEM_REGISTROS:
        case REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MENOR:
          this.SetarTimerMsgAplicacao();
          this.SolicitarDadosAFD(true, 0, 0);
          break;
        case REPAFD.BlocoMemoria.CONTEM_NSR:
          RegistroAFD registroAfd2 = RegistroAFD.LeRegistroDoNsrSolicitado(byteArrayAux, this.nsrSolicitado);
          this._repAFD.AtualizaPosicao(posicaoUltimoNsrBloco);
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ULTIMO_REGISTRO_LIDO, registroAfd2.dadosRegistro, registroAfd2.dtRegistro, registroAfd2.tipoRegistro, this.nsrSolicitado.ToString(), this.rep.Serial);
          this.EncerrarConexao();
          break;
        case REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MAIOR:
          this.SetarTimerMsgAplicacao();
          this.SolicitarDadosAFD(false, posicaoUltimoNsrBloco, ultimoNsrDoBloco);
          break;
      }
    }

    private Envelope CriarEnvelope() => new Envelope(this._clienteSocket.NsuSw, (byte) 0, this._clienteSocket.NsuRx, this._numTerminal, (ushort) 0);

    private void EnviarVerificaConfiguracao()
    {
      Envelope envelope = this.CriarEnvelope();
      this._estadoRep = GerenciadorColetaNSRSenior.Estados.estAguardandoConfigREP;
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaConfigInfo()
      {
        Info = (byte) 1
      };
      this.ClienteSocket.Enviar(envelope, true);
    }

    private void NotificaMensagemParaDriverSenior(
      REPAFD.ResultadoBuscaRegistroNsr resultadoBuscaRegistroNsr,
      string registroNsrSolicitado,
      string dataRegistro,
      int tipoRegistro,
      string ultimoNsrRep,
      string numeroSerialRep)
    {
      NotificarRegistroDoNsrSolicitadoSeniorEventArgs e = new NotificarRegistroDoNsrSolicitadoSeniorEventArgs(resultadoBuscaRegistroNsr, registroNsrSolicitado, dataRegistro, tipoRegistro, ultimoNsrRep, numeroSerialRep, this._repAFD.posMem, this.rep.RepId);
      if (this.OnNotificarColetaParaDriverSenior == null)
        return;
      this.OnNotificarColetaParaDriverSenior((object) this, e);
    }

    private void SetarTimerMsgAplicacao()
    {
      switch (this._estadoRep)
      {
        case GerenciadorColetaNSRSenior.Estados.estEnvioAutenticacao:
          this._timerMsgAplicacao.Interval = (double) RegistrySingleton.GetInstance().TIMEOUT_BASE;
          this._timerMsgAplicacao.Enabled = true;
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardandoConfigREP:
          this._timerMsgAplicacao.Interval = 1.5 * (double) RegistrySingleton.GetInstance().TIMEOUT_BASE;
          this._timerMsgAplicacao.Enabled = true;
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardandoSolicitaUltimoNSR:
          this._timerMsgAplicacao.Interval = 1.5 * (double) RegistrySingleton.GetInstance().TIMEOUT_BASE;
          this._timerMsgAplicacao.Enabled = true;
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardaDadosAFD:
          this._timerMsgAplicacao.Interval = (double) (10 * RegistrySingleton.GetInstance().TIMEOUT_BASE);
          this._timerMsgAplicacao.Enabled = true;
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardaNovaSolicitacaoNsr:
          this._timerMsgAplicacao.Interval = (double) (10 * RegistrySingleton.GetInstance().TIMEOUT_BASE);
          this._timerMsgAplicacao.Enabled = true;
          break;
      }
    }

    private void NotificarNenhumaResposta()
    {
      switch (this._estadoRep)
      {
        case GerenciadorColetaNSRSenior.Estados.estEnvioAutenticacao:
          this._msg = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_CHAVE;
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardandoConfigREP:
          this._msg = Resources.msgNENHUMA_RESPOSTA_SOLIC_CONFIG_REP;
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardandoSolicitaUltimoNSR:
          this._msg = Resources.msgNENHUMA_RESPOSTA_SOLIC_CONFIG_REP;
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardaDadosAFD:
          this._msg = Resources.msgNENHUMA_RESPOSTA_SOLIC_DADOS_AFD;
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardaNovaSolicitacaoNsr:
          this._msg = Resources.msgNENHUMA_RESPOSTA_SOLIC_DADOS_AFD;
          break;
      }
      this.EncerrarConexao();
      if (this._estadoRep == GerenciadorColetaNSRSenior.Estados.estAguardaNovaSolicitacaoNsr || this._estadoRep == GerenciadorColetaNSRSenior.Estados.estInicial)
        return;
      this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, this._msg, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
    }

    private new void NotificarTimeoutAck()
    {
      switch (this._estadoRep)
      {
        case GerenciadorColetaNSRSenior.Estados.estEnvioAutenticacao:
          this._msg = Resources.msgTIMEOUT_ENVIO_CHAVE;
          break;
      }
      this.EncerrarConexao();
      if (this._estadoRep == GerenciadorColetaNSRSenior.Estados.estAguardaNovaSolicitacaoNsr)
        return;
      this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, this._msg, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
    }

    private void timerMsgAplicacao_Elapsed(object source, ElapsedEventArgs e)
    {
      try
      {
        this.NotificarNenhumaResposta();
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        this._msg = Resources.msgERRO_COMUNICACACO_NO_RECEBIMENTO;
        this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, this._msg, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
      }
    }

    private void clienteSocket_OnChegouAck(object sender, ChegouAckEventArgs e) => this.SetarTimerMsgAplicacao();

    private void clienteSocket_OnTimeoutEnvioAck(object sender, TimeoutEnvioAckEventArgs e) => this.NotificarTimeoutAck();

    private void clienteSocket_OnChegouEnvelope(object sender, ChegouEnvelopeEventArgs e)
    {
      try
      {
        this.AnalisarEnvelope(e.Data);
      }
      catch (SocketException ex)
      {
        this._msg = ex.ErrorCode != 10058 ? Resources.msgERRO_COMUNIC_NAO_AVALIADO : Resources.msgERRO_CONEXAO_INTERROMPIDA;
        this.EncerrarConexao();
        this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, this._msg, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
      }
      catch
      {
        this.EncerrarConexao();
        this._msg = Resources.msgERRO_COMUNICACACO_NO_RECEBIMENTO;
        this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, this._msg, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
      }
    }

    private void clienteSocket_OnSocketExceptionEvento(
      object sender,
      SocketExceptionEventoEventArgs e)
    {
      this.EncerrarConexao();
      switch (e.EnumMsg)
      {
        case EnumMsgErro.erroComunicacaoNoRecebimento:
          this._msg = Resources.msgERRO_COMUNICACACO_NO_RECEBIMENTO;
          break;
        case EnumMsgErro.erroComunicacaoNoEnvio:
          this._msg = Resources.msgERRO_COMUNICACAO_NO_ENVIO;
          break;
        case EnumMsgErro.erroConexaoResetada:
          this._msg = Resources.msgERRO_COMUNICACAO_CONEXAO_RESETADA;
          break;
      }
      this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, this._msg, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.VerificaBaseREP();
      else
        this.EncerrarConexao();
    }

    private void VerificaBaseREP()
    {
      if (RepAFD.InicializaBaseRepAFD(this.rep, this.rep.Serial) != null)
      {
        this.NotificarParaUsuario(Resources.msgSOLICITANDO_DADOS_AFD1, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this.rep.RepId, this.rep.Local);
        this.SolicitarUltimoNSR();
      }
      else
        this.NotificarParaUsuario(Resources.msg_FALHA_AO_INICIAR_BD, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this.rep.RepId, this.rep.Local);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      this._timerMsgAplicacao.Enabled = false;
      switch (this._estadoRep)
      {
        case GerenciadorColetaNSRSenior.Estados.estEnvioAutenticacao:
          if (envelope.Grp != (byte) 1 || envelope.Cmd != (byte) 1)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
          {
            byte[] term = envelope.Term;
            Array.Reverse((Array) term);
            if ((int) BitConverter.ToUInt16(term, 0) == (int) this._numTerminal)
            {
              this.EnviarVerificaConfiguracao();
              break;
            }
            this.EncerrarConexao();
            this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgCHAVE_COMUNICACAO_ERRADA, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardandoConfigREP:
          byte[] bytes = new byte[17];
          if (envelope.Grp != (byte) 2 || envelope.Cmd != (byte) 1)
            break;
          byte[] term1 = envelope.Term;
          Array.Reverse((Array) term1);
          if ((int) BitConverter.ToUInt16(term1, 0) == (int) this._numTerminal)
          {
            Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 2, (Array) bytes, 0, 17);
            for (int index = 0; index < bytes.Length; ++index)
              bytes[index] += (byte) 48;
            this._serialNoREP = Encoding.Default.GetString(bytes);
            this.SolicitarUltimoNSR();
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardandoSolicitaUltimoNSR:
          if (envelope.Grp == (byte) 250 && envelope.Cmd == (byte) 105)
          {
            byte[] numArray = new byte[4];
            Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 6, (Array) numArray, 0, 4);
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < 4; ++index)
              stringBuilder.Append(numArray[index].ToString("X").PadLeft(2, '0'));
            this._ultimoNSRREP = Convert.ToInt32(stringBuilder.ToString());
            if (this.nsrSolicitado <= this._ultimoNSRREP && this.nsrSolicitado != 0)
            {
              this.SolicitarDadosAFD(true, 0, 0);
              break;
            }
            this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.REGISTRO_INEXISTENTE, Resources.msgERRO_NSR_NAO_EXISTE, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
            this.EncerrarConexao();
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardaDadosAFD:
          if (envelope.Grp == (byte) 250 && (envelope.Cmd == (byte) 102 || envelope.Cmd == (byte) 107))
          {
            this.AnalisaBufferAFD(envelope);
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
        case GerenciadorColetaNSRSenior.Estados.estAguardaNovaSolicitacaoNsr:
          if (!this._novoNsrSolicitado)
            break;
          this._novoNsrSolicitado = false;
          this.AnalisaBufferAFD(envelope);
          break;
      }
    }

    public override void TratarTimeoutAck() => throw new NotImplementedException();

    public override void TratarNenhumaResposta() => throw new NotImplementedException();

    private new enum Estados
    {
      estInicial,
      estConexao,
      estEnvioAutenticacao,
      estAguardandoConfigREP,
      estAguardandoSolicitaUltimoNSR,
      estAguardaDadosAFD,
      estAguardaNovaSolicitacaoNsr,
    }
  }
}
