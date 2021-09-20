// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.TarefaAbstrata
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Timers;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public abstract class TarefaAbstrata
  {
    private bool _chamadaSDK;
    private Constantes.STATUS_PROCESSO_TAREFA _StatusProcesso;
    private Constantes.TIPO_TAREFA _tipoTarefa;
    private TarefaAbstrata.Estados _estadoRep;
    private string _msg = string.Empty;
    private bool _repClient;
    private RepBase _rep;
    private ushort _numTerminal;
    private RSAParameters _RSAKeyInfo;
    private byte[] _chaveAESPrivada;
    private ClienteAssincrono _clienteSocket = ClienteAssincrono.getInstance();
    private System.Timers.Timer _timerMsgAplicacao = new System.Timers.Timer();

    public abstract void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e);

    public abstract void IniciarProcesso();

    public abstract void TratarEnvelope(Envelope envelope);

    public abstract void TratarTimeoutAck();

    public abstract void TratarNenhumaResposta();

    public bool ChamadaSDK
    {
      get => this._chamadaSDK;
      set => this._chamadaSDK = value;
    }

    public DateTime DataCriacao { get; set; }

    public DateTime DataLogSenior { get; set; }

    public Constantes.STATUS_PROCESSO_TAREFA StatusProcesso
    {
      get => this._StatusProcesso;
      set => this._StatusProcesso = value;
    }

    public Constantes.TIPO_TAREFA TipoTarefa
    {
      get => this._tipoTarefa;
      set => this._tipoTarefa = value;
    }

    public bool RepClient
    {
      get => this._repClient;
      set => this._repClient = value;
    }

    public ClienteAssincrono ClienteSocket
    {
      get => this._clienteSocket;
      set => this._clienteSocket = value;
    }

    public event EventHandler<NotificarParaUsuarioEventArgs> OnNotificarParaUsuario;

    public event EventHandler<NotificarConexaoEventArgs> OnNotificarConexao;

    public void Conectar(RepBase rep)
    {
      try
      {
        if (!this._clienteSocket.Conectado)
        {
          this._clienteSocket = rep.TipoConexao != 2 ? ClienteAssincrono.getInstance(rep.Port, IPAddress.Parse(rep.IpAddress), rep.NumTerminal, (byte) 1, RegistrySingleton.GetInstance().PINGINATIVO) : ClienteAssincrono.getInstance(rep.Port, rep.Host, rep.NumTerminal, (byte) 1, rep.TipoConexao, RegistrySingleton.GetInstance().PINGINATIVO);
          this._rep = rep;
          this._clienteSocket.ChamadaSDK = this._chamadaSDK;
          this._clienteSocket.TempoEsperaConexao = rep.TempoEsperaConexao;
          this._clienteSocket.RepPlus = this._rep.TipoTerminalId >= 13;
          this.InicializarTimer();
          this.InicializarEventosSocket();
          this._estadoRep = TarefaAbstrata.Estados.estConexao;
          if (this._rep.EnvioConfigAvancadas)
            this._clienteSocket.Conectar(this._repClient, rep.portaServidor, rep.tempoEspera);
          else
            this._clienteSocket.Conectar(rep.repClient, rep.portaServidor, rep.tempoEspera);
          this._numTerminal = rep.NumTerminal;
        }
        else
        {
          this._estadoRep = TarefaAbstrata.Estados.estConexaoSucesso;
          this.TarefaAbstrata_OnNotificarConexao(new NotificarConexaoEventArgs(EnumEstadoNotificacaoParaUsuario.comSucesso, this._rep.RepId, this._rep.Status));
        }
      }
      catch (SocketException ex)
      {
        this.EncerrarConexao();
        if (ex.ErrorCode == 10060)
          this.NotificarParaUsuario(Resources.errABRIR_CONEXAO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        if (ex.ErrorCode == 10061)
          this.NotificarParaUsuario(Resources.msgERRO_COMUNICACAO_CONEXAO_RESETADA, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        else
          this.NotificarParaUsuario(Resources.msgERRO_COMUNIC_NAO_AVALIADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      }
      catch (Exception ex)
      {
        try
        {
          this._estadoRep = TarefaAbstrata.Estados.estInicial;
          this._msg = Resources.errABRIR_CONEXAO;
          ex.Data.Add((object) "mensagem", (object) this._msg);
          if (!ExceptionPolicy.HandleException(ex, "Politica Dominio"))
            return;
          throw;
        }
        catch
        {
        }
      }
    }

    private void InicializarEventosSocket()
    {
      this._clienteSocket.OnChegouEnvelope += new EventHandler<ChegouEnvelopeEventArgs>(this.clienteSocket_OnChegouEnvelope);
      this._clienteSocket.OnChegouAck += new EventHandler<ChegouAckEventArgs>(this.clienteSocket_OnChegouAck);
      this._clienteSocket.OnTimeoutEnvioAck += new EventHandler<TimeoutEnvioAckEventArgs>(this.clienteSocket_OnTimeoutEnvioAck);
      this._clienteSocket.OnNotificarEnvioPing += new EventHandler<NotificarEnvioPingEventArgs>(this.clienteSocket_OnNotificarEnvioPing);
      this._clienteSocket.OnSocketExceptionEvento += new EventHandler<SocketExceptionEventoEventArgs>(this.clienteSocket_OnSocketExceptionEvento);
    }

    public void EncerrarConexao()
    {
      try
      {
        this._timerMsgAplicacao.Enabled = false;
        this._estadoRep = TarefaAbstrata.Estados.estInicial;
        if (!this._clienteSocket.Conectado)
          return;
        this._clienteSocket.Desconectar();
      }
      catch (Exception ex)
      {
      }
    }

    public void NotificarParaUsuario(
      byte idResultadoBio,
      string menssagem,
      EnumEstadoNotificacaoParaUsuario resultado,
      EnumEstadoResultadoFinalProcesso resultadoFinal,
      int repId,
      string local)
    {
      NotificarParaUsuarioEventArgs e = new NotificarParaUsuarioEventArgs((int) idResultadoBio, menssagem, resultado, resultadoFinal, repId, local);
      if (this.OnNotificarParaUsuario == null)
        return;
      this.OnNotificarParaUsuario((object) this, e);
    }

    public void NotificarParaUsuario(
      string menssagem,
      EnumEstadoNotificacaoParaUsuario resultado,
      EnumEstadoResultadoFinalProcesso resultadoFinal,
      int repId,
      string local)
    {
      Console.WriteLine(resultado.ToString() + " - > " + menssagem + " " + (object) repId);
      NotificarParaUsuarioEventArgs e = new NotificarParaUsuarioEventArgs(menssagem, resultado, resultadoFinal, repId, local);
      if (this.OnNotificarParaUsuario == null)
        return;
      this.OnNotificarParaUsuario((object) this, e);
    }

    public void NotificarConexao(
      EnumEstadoNotificacaoParaUsuario resultado,
      int repId,
      byte statusRep)
    {
      Console.WriteLine(resultado.ToString());
      NotificarConexaoEventArgs e = new NotificarConexaoEventArgs(resultado, repId, statusRep);
      if (this.OnNotificarConexao == null)
        return;
      this.OnNotificarConexao((object) this, e);
    }

    public void AnalizarEnvelope(Envelope envelope)
    {
      this._timerMsgAplicacao.Enabled = false;
      switch (this._estadoRep)
      {
        case TarefaAbstrata.Estados.estConexao:
          break;
        case TarefaAbstrata.Estados.estEnvioAutenticacao:
          if (envelope.Grp != (byte) 1 || envelope.Cmd != (byte) 102 && envelope.Cmd != (byte) 1)
            break;
          this.ClienteSocket.EnviaCriptografado = false;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
          {
            byte[] term = envelope.Term;
            Array.Reverse((Array) term);
            if ((int) BitConverter.ToUInt16(term, 0) == (int) this._numTerminal)
            {
              this.EnviarVerificaCongfiguracao();
              break;
            }
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgERRO_NUM_TERMINAL_ERRADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            break;
          }
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgCHAVE_COMUNICACAO_ERRADA, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
        case TarefaAbstrata.Estados.estEnvioVerificaConfig:
          if (envelope.Grp != (byte) 2 || envelope.Cmd != (byte) 1)
            break;
          byte[] bytes = new byte[17];
          Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 2, (Array) bytes, 0, 17);
          for (int index = 0; index < bytes.Length; ++index)
            bytes[index] += (byte) 48;
          string str = Encoding.Default.GetString(bytes);
          if (RegistrySingleton.GetInstance().VALIDAR_SERIAL)
          {
            this._rep.Serial = this._rep.Serial == null || this._rep.Serial.Trim().Equals("") ? str : this._rep.Serial;
            if (!this._rep.Serial.Equals(str))
            {
              this.EncerrarConexao();
              this.NotificarParaUsuario("Número de série do equipamento é diferente: " + str, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
              break;
            }
          }
          else
            this._rep.Serial = str;
          this._rep.Status = envelope.MsgAplicacaoEmBytes[38];
          this._rep.ModeloFim = (int) envelope.MsgAplicacaoEmBytes[22];
          this._rep.VersaoFW = (int) envelope.MsgAplicacaoEmBytes[24];
          this._rep.VersaoBaixaFW = Convert.ToInt32(envelope.MsgAplicacaoEmBytes[25].ToString("X"));
          this._rep.VersaoFWCompleto = envelope.MsgAplicacaoEmBytes[24].ToString("X") + "." + envelope.MsgAplicacaoEmBytes[25].ToString("X");
          if (this._rep.CpfResponsavel != null && this._clienteSocket.RepPlus)
          {
            if (!this._rep.CpfResponsavel.Equals(""))
            {
              this.EnviarCPFResponsavel();
              break;
            }
            this._estadoRep = TarefaAbstrata.Estados.estConexaoSucesso;
            this.TarefaAbstrata_OnNotificarConexao(new NotificarConexaoEventArgs(EnumEstadoNotificacaoParaUsuario.comSucesso, this._rep.RepId, this._rep.Status));
            break;
          }
          if (envelope.MsgAplicacaoEmBytes[38] != (byte) 0 && !this._clienteSocket.RepPlus)
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgEQUIPAMENTO_BLOQUEADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            break;
          }
          this._estadoRep = TarefaAbstrata.Estados.estConexaoSucesso;
          this.TarefaAbstrata_OnNotificarConexao(new NotificarConexaoEventArgs(EnumEstadoNotificacaoParaUsuario.comSucesso, this._rep.RepId, this._rep.Status));
          break;
        case TarefaAbstrata.Estados.estEnvioCPFResponsavel:
          if (envelope.Grp != (byte) 10 || envelope.Cmd != (byte) 105)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
          {
            this._estadoRep = TarefaAbstrata.Estados.estConexaoSucesso;
            this.TarefaAbstrata_OnNotificarConexao(new NotificarConexaoEventArgs(EnumEstadoNotificacaoParaUsuario.comSucesso, this._rep.RepId, this._rep.Status));
            break;
          }
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgCPF_RESPONSAVEL_INVALIDO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
        case TarefaAbstrata.Estados.estEnviarChavePlublicaRSA:
          if (envelope.Grp != (byte) 1 || envelope.Cmd != (byte) 101)
            break;
          byte[] rgb = new byte[128];
          Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 2, (Array) rgb, 0, rgb.Length);
          RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(1024, new CspParameters()
          {
            Flags = CspProviderFlags.UseMachineKeyStore
          });
          cryptoServiceProvider.ImportParameters(this._RSAKeyInfo);
          this._chaveAESPrivada = cryptoServiceProvider.Decrypt(rgb, false);
          byte[] numArray1 = new byte[16];
          byte[] numArray2 = new byte[16];
          Array.Copy((Array) this._chaveAESPrivada, 0, (Array) numArray2, 0, numArray2.Length);
          Array.Copy((Array) this._chaveAESPrivada, numArray2.Length, (Array) numArray1, 0, 16);
          this.ClienteSocket.EnviaCriptografado = true;
          this.ClienteSocket.ChaveAES = numArray2;
          this.ClienteSocket.IV = numArray1;
          this.EnviarAutenticacao();
          break;
        default:
          this.TratarEnvelope(envelope);
          break;
      }
    }

    private void EnviarAutenticacao()
    {
      try
      {
        this._estadoRep = TarefaAbstrata.Estados.estEnvioAutenticacao;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, ushort.MaxValue, (ushort) 0);
        if (this._clienteSocket.RepPlus)
        {
          envelope.MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoAutenticacaoRepPlus()
          {
            ChaveComunicacao = this._rep.ChaveComunicacao
          };
          this.ClienteSocket.EnviaCriptografado = true;
        }
        else
        {
          envelope.MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoAutenticacao()
          {
            ChaveComunicacao = this._rep.ChaveComunicacao
          };
          this.ClienteSocket.EnviaCriptografado = false;
        }
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarChavePlublicaRSA()
    {
      try
      {
        this._estadoRep = TarefaAbstrata.Estados.estEnviarChavePlublicaRSA;
        this._RSAKeyInfo = new RSACryptoServiceProvider(1024, new CspParameters()
        {
          Flags = CspProviderFlags.NoFlags
        }).ExportParameters(true);
        byte[] numArray = new byte[131];
        Array.Copy((Array) this._RSAKeyInfo.Modulus, 0, (Array) numArray, 0, this._RSAKeyInfo.Modulus.Length);
        Array.Copy((Array) this._RSAKeyInfo.Exponent, 0, (Array) numArray, this._RSAKeyInfo.Modulus.Length, this._RSAKeyInfo.Exponent.Length);
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, ushort.MaxValue, (ushort) 0);
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoRSAChavePublica()
        {
          ChavePublica = numArray
        };
        this.ClienteSocket.EnviaCriptografado = false;
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private string ImprimeHexadecimal(byte[] dado)
    {
      string str = "";
      foreach (byte num1 in dado)
      {
        uint num2 = (uint) num1;
        str = str + (num1 <= (byte) 15 ? "0" : "") + string.Format("{0:X}", (object) num2) + " ";
      }
      return str;
    }

    private void EnviarCPFResponsavel()
    {
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this._numTerminal, (ushort) 0);
      this._estadoRep = TarefaAbstrata.Estados.estEnvioCPFResponsavel;
      MsgTCPAplicacaoCPFResponsavel aplicacaoCpfResponsavel = new MsgTCPAplicacaoCPFResponsavel(this._rep.CpfResponsavel);
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) aplicacaoCpfResponsavel;
      this.ClienteSocket.EnviaCriptografado = false;
      this.ClienteSocket.Enviar(envelope, true);
    }

    private void EnviarVerificaCongfiguracao()
    {
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this._numTerminal, (ushort) 0);
      this._estadoRep = TarefaAbstrata.Estados.estEnvioVerificaConfig;
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaConfigInfo()
      {
        Info = (byte) 1
      };
      this.ClienteSocket.Enviar(envelope, true);
    }

    private void SetarTimerMsgAplicacao()
    {
      this._timerMsgAplicacao.Interval = (double) ((long) RegistrySingleton.GetInstance().TIMEOUT_BASE + this._rep.TimeoutExtra);
      this._timerMsgAplicacao.Enabled = true;
    }

    private void NotificarNenhumaResposta()
    {
      switch (this._estadoRep)
      {
        case TarefaAbstrata.Estados.estEnvioAutenticacao:
          this._msg = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_CHAVE;
          break;
        case TarefaAbstrata.Estados.estConexaoSucesso:
          this.TratarNenhumaResposta();
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(this._msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public virtual void NotificarTimeoutAck()
    {
      switch (this._estadoRep)
      {
        case TarefaAbstrata.Estados.estEnvioAutenticacao:
          this._msg = Resources.msgTIMEOUT_ENVIO_CHAVE;
          break;
        case TarefaAbstrata.Estados.estConexaoSucesso:
          this.TratarTimeoutAck();
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(this._msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
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
        this.NotificarParaUsuario(Resources.msgERRO_COMUNICACACO_NO_RECEBIMENTO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      }
    }

    private void clienteSocket_OnChegouAck(object sender, ChegouAckEventArgs e) => this.SetarTimerMsgAplicacao();

    private void clienteSocket_OnTimeoutEnvioAck(object sender, TimeoutEnvioAckEventArgs e) => this.NotificarTimeoutAck();

    private void clienteSocket_OnChegouEnvelope(object sender, ChegouEnvelopeEventArgs e)
    {
      try
      {
        this.AnalizarEnvelope(e.Data);
      }
      catch (SocketException ex)
      {
        if (ex.ErrorCode == 10058)
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgERRO_CONEXAO_INTERROMPIDA, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        }
        else
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgERRO_COMUNIC_NAO_AVALIADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        }
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        this.NotificarParaUsuario(ex.Message, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      }
    }

    private void clienteSocket_OnNotificarEnvioPing(object sender, NotificarEnvioPingEventArgs e)
    {
      if (e.EstadoEnvioPing == EnumEstadoEnvioPing.comSucesso)
      {
        if (this._clienteSocket.RepPlus)
          this.EnviarChavePlublicaRSA();
        else
          this.EnviarAutenticacao();
      }
      else
      {
        EnumEstadoNotificacaoParaUsuario resultado = EnumEstadoNotificacaoParaUsuario.semSucesso;
        EnumEstadoResultadoFinalProcesso resultadoFinal = EnumEstadoResultadoFinalProcesso.finalizadoComFalha;
        this.NotificarParaUsuario(e.msg, resultado, resultadoFinal, this._rep.RepId, this._rep.Local);
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
      this.NotificarParaUsuario(this._msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private void InicializarTimer()
    {
      this._timerMsgAplicacao = new System.Timers.Timer();
      this._timerMsgAplicacao.AutoReset = false;
      this._timerMsgAplicacao.Elapsed += new ElapsedEventHandler(this.timerMsgAplicacao_Elapsed);
    }

    private enum Estados
    {
      estInicial,
      estConexao,
      estEnvioAutenticacao,
      estEnvioVerificaConfig,
      estEnvioCPFResponsavel,
      estEnviarChavePlublicaRSA,
      estConexaoSucesso,
    }
  }
}
