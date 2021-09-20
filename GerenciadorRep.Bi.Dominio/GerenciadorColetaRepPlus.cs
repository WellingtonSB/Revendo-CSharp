// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorColetaRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Timers;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorColetaRepPlus
  {
    private RepBase _rep;
    private string _msg = "";
    private Timer _timerConexao;
    private RSAParameters _RSAKeyInfo;
    private byte[] _chaveAESPrivada;
    private ushort _numTerminal;
    private uint _ultimoNsr;
    private bool _primeiroPedido;
    private bool _primeiroPedidoSemMarcacoes;
    private ClienteAssincrono _clienteSocket;
    private Timer _timerMsgAplicacao;
    private GerenciadorColetaRepPlus.Estados _estadoRep;
    private uint _totBilhetesRecebidos;
    private uint _totBilhetesProcessados;
    private uint _totBilhetesCarimboInvalido;
    private bool _recuperarTodasMarcacoes;
    public static GerenciadorColetaRepPlus _gerenciadorColetaRepPlus;

    public event EventHandler<NotificarParaUsuarioEventArgs> OnNotificarParaUsuario;

    public event EventHandler<NotificarTotBilhetesParaUsuarioEventArgs> OnNotificarBilhetesProcessadosParaUsuario;

    public static GerenciadorColetaRepPlus getInstance() => GerenciadorColetaRepPlus._gerenciadorColetaRepPlus != null ? GerenciadorColetaRepPlus._gerenciadorColetaRepPlus : new GerenciadorColetaRepPlus();

    public static GerenciadorColetaRepPlus getInstance(RepBase rep) => GerenciadorColetaRepPlus._gerenciadorColetaRepPlus != null ? GerenciadorColetaRepPlus._gerenciadorColetaRepPlus : new GerenciadorColetaRepPlus(rep);

    public GerenciadorColetaRepPlus()
    {
    }

    public GerenciadorColetaRepPlus(RepBase rep)
    {
      this._rep = rep;
      this._clienteSocket = rep.ClienteSocket;
      this._numTerminal = rep.NumTerminal;
      this._estadoRep = GerenciadorColetaRepPlus.Estados.estInicial;
      this.InicializarEventosSocket();
      this.InicializarTimer();
    }

    ~GerenciadorColetaRepPlus()
    {
    }

    public ushort NumTerminal => this._numTerminal;

    public uint TotBilhetesRecebidos => this._totBilhetesRecebidos;

    public uint TotBilhetesProcessados => this._totBilhetesProcessados;

    public uint TotBilhetesCarimboInvalido => this._totBilhetesCarimboInvalido;

    private void InicializarEventosSocket()
    {
      this._clienteSocket.OnChegouEnvelope += new EventHandler<ChegouEnvelopeEventArgs>(this.clienteSocket_OnChegouEnvelope);
      this._clienteSocket.OnChegouAck += new EventHandler<ChegouAckEventArgs>(this.clienteSocket_OnChegouAck);
      this._clienteSocket.OnTimeoutEnvioAck += new EventHandler<TimeoutEnvioAckEventArgs>(this.clienteSocket_OnTimeoutEnvioAck);
      this._clienteSocket.OnNotificarEnvioPing += new EventHandler<NotificarEnvioPingEventArgs>(this.clienteSocket_OnNotificarEnvioPing);
      this._clienteSocket.OnSocketExceptionEvento += new EventHandler<SocketExceptionEventoEventArgs>(this.clienteSocket_OnSocketExceptionEvento);
    }

    private void InicializarTimer()
    {
      this._timerMsgAplicacao = new Timer();
      this._timerMsgAplicacao.AutoReset = false;
      this._timerMsgAplicacao.Elapsed += new ElapsedEventHandler(this.timerMsgAplicacao_Elapsed);
      this._timerConexao = new Timer();
      this._timerConexao.AutoReset = false;
      this._timerConexao.Elapsed -= new ElapsedEventHandler(this._timerConexao_Elapsed);
      this._timerConexao.Elapsed += new ElapsedEventHandler(this._timerConexao_Elapsed);
    }

    public void IniciarProcesso(bool recuperarTodasMarcacoes, uint ultimoNsr)
    {
      this._clienteSocket.RepPlus = true;
      this._timerConexao.Interval = (double) (2 * RegistrySingleton.GetInstance().TIMEOUT_BASE);
      this._timerConexao.Enabled = true;
      this._totBilhetesRecebidos = 0U;
      this._totBilhetesProcessados = 0U;
      this._totBilhetesCarimboInvalido = 0U;
      this._ultimoNsr = ultimoNsr;
      this._recuperarTodasMarcacoes = recuperarTodasMarcacoes;
      this._primeiroPedidoSemMarcacoes = false;
      this._primeiroPedido = true;
      if (this._clienteSocket.Conectado)
      {
        this._msg = Resources.msg_BILHETES_BAIXANDO;
        NotificarParaUsuarioEventArgs e = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.comSucesso, this._rep.RepId, this._rep.Local);
        if (this.OnNotificarParaUsuario != null)
          this.OnNotificarParaUsuario((object) this, e);
        this.EnviarSolicitacaoBilheteRep(this._ultimoNsr);
      }
      else
        this.Conectar();
    }

    private void Conectar()
    {
      try
      {
        this._estadoRep = GerenciadorColetaRepPlus.Estados.estConexao;
        this._clienteSocket.Conectar(this._rep.repClient, this._rep.portaServidor, this._rep.tempoEspera);
        this._numTerminal = this._rep.NumTerminal;
        this.EnviarChavePlublicaRSA();
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
        this._estadoRep = GerenciadorColetaRepPlus.Estados.estInicial;
        this._msg = Resources.errABRIR_CONEXAO;
        ex.Data.Add((object) "mensagem", (object) this._msg);
        if (!ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          return;
        throw;
      }
    }

    private void EnviarChavePlublicaRSA()
    {
      try
      {
        this._estadoRep = GerenciadorColetaRepPlus.Estados.estEnviarChavePlublicaRSA;
        this._RSAKeyInfo = new RSACryptoServiceProvider(1024, new CspParameters()
        {
          Flags = CspProviderFlags.NoFlags
        }).ExportParameters(true);
        byte[] numArray = new byte[131];
        Array.Copy((Array) this._RSAKeyInfo.Modulus, 0, (Array) numArray, 0, this._RSAKeyInfo.Modulus.Length);
        Array.Copy((Array) this._RSAKeyInfo.Exponent, 0, (Array) numArray, this._RSAKeyInfo.Modulus.Length, this._RSAKeyInfo.Exponent.Length);
        Envelope envelope = new Envelope(this._clienteSocket.NsuSw, (byte) 0, this._clienteSocket.NsuRx, ushort.MaxValue, (ushort) 0);
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoRSAChavePublica()
        {
          ChavePublica = numArray
        };
        this._clienteSocket.EnviaCriptografado = false;
        this._clienteSocket.Enviar(envelope, true);
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

    private void EnviarAutenticacao()
    {
      try
      {
        this._estadoRep = GerenciadorColetaRepPlus.Estados.estEnvioAutenticacao;
        Envelope envelope = new Envelope(this._clienteSocket.NsuSw, (byte) 0, this._clienteSocket.NsuRx, ushort.MaxValue, (ushort) 0);
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoAutenticacaoRepPlus()
        {
          ChaveComunicacao = this._rep.ChaveComunicacao
        };
        this._clienteSocket.EnviaCriptografado = true;
        this._clienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoBilheteRep(uint nsr)
    {
      try
      {
        this._estadoRep = GerenciadorColetaRepPlus.Estados.estSolicitoBilhete;
        this._clienteSocket.Enviar(new Envelope(this._clienteSocket.NsuSw, (byte) 0, this._clienteSocket.NsuRx, this._numTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaBilheteRep(nsr)
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    public void EncerrarConexao()
    {
      try
      {
        this._timerMsgAplicacao.Enabled = false;
        this._estadoRep = GerenciadorColetaRepPlus.Estados.estInicial;
        if (!this._clienteSocket.Conectado)
          return;
        this._clienteSocket.Desconectar();
      }
      catch (Exception ex)
      {
      }
    }

    private void AnalizarEnvelope(Envelope envelope)
    {
      this._timerMsgAplicacao.Enabled = false;
      switch (this._estadoRep)
      {
        case GerenciadorColetaRepPlus.Estados.estEnviarChavePlublicaRSA:
          if (envelope.Grp != (byte) 1 || envelope.Cmd != (byte) 101)
            break;
          byte[] rgb = new byte[128];
          Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 2, (Array) rgb, 0, rgb.Length);
          RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(1024);
          cryptoServiceProvider.ImportParameters(this._RSAKeyInfo);
          this._chaveAESPrivada = cryptoServiceProvider.Decrypt(rgb, false);
          byte[] numArray1 = new byte[16];
          byte[] numArray2 = new byte[16];
          Array.Copy((Array) this._chaveAESPrivada, 0, (Array) numArray2, 0, numArray2.Length);
          Array.Copy((Array) this._chaveAESPrivada, numArray2.Length, (Array) numArray1, 0, 16);
          this._clienteSocket.EnviaCriptografado = true;
          this._clienteSocket.ChaveAES = numArray2;
          this._clienteSocket.IV = numArray1;
          this.EnviarAutenticacao();
          break;
        case GerenciadorColetaRepPlus.Estados.estEnvioAutenticacao:
          if (envelope.Grp != (byte) 1 || envelope.Cmd != (byte) 1)
            break;
          this._clienteSocket.EnviaCriptografado = false;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
          {
            byte[] term = envelope.Term;
            Array.Reverse((Array) term);
            if ((int) BitConverter.ToUInt16(term, 0) == (int) this._numTerminal)
            {
              this._msg = Resources.msg_BILHETES_BAIXANDO;
              NotificarParaUsuarioEventArgs e = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.comSucesso, this._rep.RepId, this._rep.Local);
              if (this.OnNotificarParaUsuario != null)
                this.OnNotificarParaUsuario((object) this, e);
              this.EnviarSolicitacaoBilheteRep(this._ultimoNsr);
              break;
            }
            this.EncerrarConexao();
            this._msg = Resources.msgERRO_NUM_TERMINAL_ERRADO;
            NotificarParaUsuarioEventArgs e1 = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.semSucesso, this._rep.RepId, this._rep.Local);
            if (this.OnNotificarParaUsuario == null)
              break;
            this.OnNotificarParaUsuario((object) this, e1);
            break;
          }
          this.EncerrarConexao();
          this._msg = Resources.msgCHAVE_COMUNICACAO_ERRADA;
          NotificarParaUsuarioEventArgs e2 = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.semSucesso, this._rep.RepId, this._rep.Local);
          if (this.OnNotificarParaUsuario == null)
            break;
          this.OnNotificarParaUsuario((object) this, e2);
          break;
        case GerenciadorColetaRepPlus.Estados.estSolicitoBilhete:
          if (envelope.Grp != (byte) 5 || envelope.Cmd != (byte) 1)
            break;
          this._timerConexao.Enabled = false;
          MsgTcpAplicacaoRespostaBilheteRep msgRespostaBilhete = this.AbrirMsgRespostaBilheteRep(envelope);
          if (msgRespostaBilhete.TerminouBilhete)
          {
            if (this._primeiroPedidoSemMarcacoes)
            {
              this._msg = Resources.msg_NENHUM_BILHETES;
              NotificarParaUsuarioEventArgs e3 = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.nenhumEstado, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
              if (this.OnNotificarParaUsuario != null)
                this.OnNotificarParaUsuario((object) this, e3);
            }
            else
            {
              this.GravarMarcacoesRep(msgRespostaBilhete);
              this._msg = Resources.msg_BILHETES_BAIXADOS;
              NotificarParaUsuarioEventArgs e4 = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.comSucesso, this._rep.RepId, this._rep.Local);
              if (this.OnNotificarParaUsuario != null)
                this.OnNotificarParaUsuario((object) this, e4);
              if (!this._recuperarTodasMarcacoes)
              {
                this._msg = Resources.msgPROCESSANDO_ARQUIVO;
                NotificarParaUsuarioEventArgs e5 = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.comSucesso, this._rep.RepId, this._rep.Local);
                if (this.OnNotificarParaUsuario != null)
                  this.OnNotificarParaUsuario((object) this, e5);
                this.ProcessarMarcacoesRep();
              }
              this._msg = Resources.msgPROCESSO_FINALIZADO;
              NotificarParaUsuarioEventArgs e6 = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              if (this.OnNotificarParaUsuario != null)
                this.OnNotificarParaUsuario((object) this, e6);
            }
            this.EncerrarConexao();
            break;
          }
          this.GravarMarcacoesRep(msgRespostaBilhete);
          this.EnviarSolicitacaoBilheteRep(this._ultimoNsr);
          this._timerConexao.Enabled = true;
          break;
      }
    }

    private void GravarMarcacoesRep(
      MsgTcpAplicacaoRespostaBilheteRep msgRespostaBilhete)
    {
      try
      {
        Bilhete bilhete1 = new Bilhete();
        Bilhete bilhete2 = new Bilhete();
        foreach (BilheteRep lstBilhete in msgRespostaBilhete.LstBilhetes)
        {
          DateTime dateTime1 = new DateTime(2000 + (int) lstBilhete.Ano, (int) lstBilhete.Mes, (int) lstBilhete.Dia);
          bilhete2.Data = dateTime1;
          DateTime dateTime2 = new DateTime(2000 + (int) lstBilhete.Ano, (int) lstBilhete.Mes, (int) lstBilhete.Dia, (int) lstBilhete.Hora, (int) lstBilhete.Minuto, 0);
          bilhete2.Hora = dateTime2;
          bilhete2.RepId = this._rep.RepId;
          bilhete2.Processado = false;
          StringBuilder stringBuilder = new StringBuilder();
          foreach (byte num in lstBilhete.PIS)
            stringBuilder.Append(num.ToString("x").PadLeft(2, '0'));
          bilhete2.Pis = stringBuilder.ToString();
          if (bilhete2.Pis.Substring(0, 1) == "0")
            bilhete2.Pis = bilhete2.Pis.Substring(1, bilhete2.Pis.Length - 1);
          Array.Reverse((Array) lstBilhete.Nsr);
          uint uint32 = BitConverter.ToUInt32(lstBilhete.Nsr, 0);
          bilhete2.Nsr = uint32;
          CultureInfo cultureInfo = new CultureInfo("");
          bilhete2.Carimbo = CheckSum.GerarCheckSumRep(bilhete2.Pis, dateTime2.ToString("G", (IFormatProvider) cultureInfo), bilhete2.Nsr.ToString());
          int num1 = bilhete1.InserirBilheteRep(bilhete2, CommandType.Text);
          this._ultimoNsr = bilhete2.Nsr;
          if (num1 > 0)
            ++this._totBilhetesRecebidos;
          NotificarTotBilhetesParaUsuarioEventArgs e = new NotificarTotBilhetesParaUsuarioEventArgs(this._rep.RepId, this._totBilhetesRecebidos.ToString());
          if (this.OnNotificarBilhetesProcessadosParaUsuario != null)
            this.OnNotificarBilhetesProcessadosParaUsuario((object) this, e);
        }
      }
      catch (AppTopdataException ex)
      {
        if (!ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          return;
        throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (!ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          return;
        throw;
      }
    }

    private void SetarTimerMsgAplicacao()
    {
      switch (this._estadoRep)
      {
        case GerenciadorColetaRepPlus.Estados.estEnvioAutenticacao:
          this._timerMsgAplicacao.Interval = (double) RegistrySingleton.GetInstance().TIMEOUT_BASE;
          this._timerMsgAplicacao.Enabled = true;
          break;
        case GerenciadorColetaRepPlus.Estados.estSolicitoBilhete:
          this._timerMsgAplicacao.Interval = (double) RegistrySingleton.GetInstance().TIMEOUT_BASE;
          this._timerMsgAplicacao.Enabled = true;
          break;
      }
    }

    private void NotificarNenhumaResposta()
    {
      switch (this._estadoRep)
      {
        case GerenciadorColetaRepPlus.Estados.estEnvioAutenticacao:
          this._msg = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_CHAVE;
          break;
        case GerenciadorColetaRepPlus.Estados.estSolicitoBilhete:
          this._msg = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_BILHETE;
          break;
      }
      this.EncerrarConexao();
      NotificarParaUsuarioEventArgs e = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.semSucesso, this._rep.RepId, this._rep.Local);
      if (this.OnNotificarParaUsuario == null)
        return;
      this.OnNotificarParaUsuario((object) this, e);
    }

    private void NotificarTimeoutAck()
    {
      switch (this._estadoRep)
      {
        case GerenciadorColetaRepPlus.Estados.estEnvioAutenticacao:
          this._msg = Resources.msgTIMEOUT_ENVIO_CHAVE;
          break;
        case GerenciadorColetaRepPlus.Estados.estSolicitoBilhete:
          this._msg = Resources.msgTIMEOUT_SOLICIT_BILHETE;
          break;
      }
      this.EncerrarConexao();
      NotificarParaUsuarioEventArgs e = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.semSucesso, this._rep.RepId, this._rep.Local);
      if (this.OnNotificarParaUsuario == null)
        return;
      this.OnNotificarParaUsuario((object) this, e);
    }

    private MsgTcpAplicacaoRespostaBilheteRep AbrirMsgRespostaBilheteRep(
      Envelope envelope)
    {
      int num = 180;
      MsgTcpAplicacaoRespostaBilheteRep respostaBilheteRep = new MsgTcpAplicacaoRespostaBilheteRep();
      byte[] aplicacaoEmBytes = envelope.MsgAplicacaoEmBytes;
      byte[] numArray1 = new byte[num * 15];
      Array.Copy((Array) aplicacaoEmBytes, 2, (Array) numArray1, 0, numArray1.Length);
      for (int index = 0; index < num; ++index)
      {
        BilheteRep bilhete = new BilheteRep();
        bilhete.Dia = numArray1[4 + index * 15];
        if (bilhete.Dia == (byte) 0)
        {
          respostaBilheteRep.TerminouBilhete = true;
          if (this._primeiroPedido)
            this._primeiroPedidoSemMarcacoes = true;
          return respostaBilheteRep;
        }
        byte[] numArray2 = new byte[4]
        {
          numArray1[index * 15],
          numArray1[1 + index * 15],
          numArray1[2 + index * 15],
          numArray1[3 + index * 15]
        };
        bilhete.Nsr = numArray2;
        bilhete.Mes = numArray1[5 + index * 15];
        bilhete.Ano = numArray1[6 + index * 15];
        bilhete.Hora = numArray1[7 + index * 15];
        bilhete.Minuto = numArray1[8 + index * 15];
        byte[] numArray3 = new byte[6]
        {
          numArray1[9 + index * 15],
          numArray1[10 + index * 15],
          numArray1[11 + index * 15],
          numArray1[12 + index * 15],
          numArray1[13 + index * 15],
          numArray1[14 + index * 15]
        };
        bilhete.PIS = numArray3;
        this._primeiroPedido = false;
        respostaBilheteRep.Add(bilhete);
      }
      return respostaBilheteRep;
    }

    public void ProcessarMarcacoesRep()
    {
      this._totBilhetesProcessados = 0U;
      ArquivoBilhete arquivoBilhete = this._rep.ArquivoBilhete;
      SortableBindingList<ArquivoBilhete> sortableBindingList1 = new SortableBindingList<ArquivoBilhete>();
      int empregadorId = this._rep.Empregador.PesquisarEmpregadorDeUmREP(this._rep.RepId).EmpregadorId;
      SortableBindingList<ArquivoBilhete> sortableBindingList2 = arquivoBilhete.PesquisarArquivoBilhetePorEmpregador(empregadorId);
      Bilhete bilhete1 = new Bilhete();
      foreach (ArquivoBilhete arquivo in (Collection<ArquivoBilhete>) sortableBindingList2)
      {
        foreach (Bilhete bilhete2 in bilhete1.PesquisarBilhetesRep())
        {
          arquivoBilhete.AssociarEntidadeArquivoBilhete(arquivo);
          CultureInfo cultureInfo = new CultureInfo("");
          if (CheckSum.ValidaCheckSumRep(bilhete2.Pis, bilhete2.Hora.ToString("G", (IFormatProvider) cultureInfo), bilhete2.Nsr.ToString(), bilhete2.Carimbo))
          {
            ++this._totBilhetesProcessados;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("010 ");
            DateTime data = bilhete2.Data;
            string str1 = data.Day.ToString().PadLeft(2, '0');
            string str2 = data.Month.ToString().PadLeft(2, '0');
            string str3 = data.Year.ToString();
            switch (str3.Length)
            {
              case 1:
                str3 = "0" + str3;
                break;
              case 3:
                str3 = str3.Substring(1);
                break;
              case 4:
                str3 = str3.Substring(2);
                break;
            }
            stringBuilder.Append(str1 + "/" + str2 + "/" + str3 + " ");
            DateTime hora = bilhete2.Hora;
            string str4 = hora.Hour.ToString().PadLeft(2, '0');
            string str5 = hora.Minute.ToString().PadLeft(2, '0');
            string str6 = "00";
            stringBuilder.Append(str4 + ":" + str5 + ":" + str6 + " ");
            stringBuilder.Append(bilhete2.Cartao.ToString().PadLeft(16, '0') + " ");
            string str7 = this._rep.NumTerminal.ToString();
            stringBuilder.Append(str7.PadLeft(3, '0') + " ");
            stringBuilder.Append(bilhete2.Pis.PadLeft(11, '0') + " ");
            string str8 = stringBuilder.ToString();
            arquivoBilhete.ProcessarArquivoBilhete(str8.Trim());
          }
          else
            ++this._totBilhetesCarimboInvalido;
        }
      }
      bilhete1.AtualizarBilhetesRepProcessados(CommandType.Text);
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
        NotificarParaUsuarioEventArgs e1 = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.semSucesso, this._rep.RepId, this._rep.Local);
        if (this.OnNotificarParaUsuario == null)
          return;
        this.OnNotificarParaUsuario((object) this, e1);
      }
    }

    private void _timerConexao_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        this.NotificarNenhumaResposta();
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        this._msg = Resources.msgERRO_COMUNICACACO_NO_RECEBIMENTO;
        this.NotificaMensagemParaUsuario(this._msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha);
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
        this._msg = ex.ErrorCode != 10058 ? Resources.msgERRO_COMUNIC_NAO_AVALIADO : Resources.msgERRO_CONEXAO_INTERROMPIDA;
        this.EncerrarConexao();
        NotificarParaUsuarioEventArgs e1 = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.semSucesso, this._rep.RepId, this._rep.Local);
        if (this.OnNotificarParaUsuario == null)
          return;
        this.OnNotificarParaUsuario((object) this, e1);
      }
      catch (Exception ex)
      {
        this._msg = Resources.msgERRO_CONEXAO_INTERROMPIDA_ERRO_DB;
        this.EncerrarConexao();
        NotificarParaUsuarioEventArgs e2 = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.semSucesso, this._rep.RepId, this._rep.Local);
        if (this.OnNotificarParaUsuario == null)
          return;
        this.OnNotificarParaUsuario((object) this, e2);
      }
    }

    private void clienteSocket_OnNotificarEnvioPing(object sender, NotificarEnvioPingEventArgs e)
    {
      EnumEstadoNotificacaoParaUsuario estadoNotificacao = e.EstadoEnvioPing != EnumEstadoEnvioPing.comSucesso ? EnumEstadoNotificacaoParaUsuario.semSucesso : EnumEstadoNotificacaoParaUsuario.comSucesso;
      NotificarParaUsuarioEventArgs e1 = new NotificarParaUsuarioEventArgs(e.msg, estadoNotificacao, this._rep.RepId, this._rep.Local);
      if (this.OnNotificarParaUsuario == null)
        return;
      this.OnNotificarParaUsuario((object) this, e1);
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
      NotificarParaUsuarioEventArgs e1 = new NotificarParaUsuarioEventArgs(this._msg, EnumEstadoNotificacaoParaUsuario.semSucesso, this._rep.RepId, this._rep.Local);
      if (this.OnNotificarParaUsuario == null)
        return;
      this.OnNotificarParaUsuario((object) this, e1);
    }

    private void NotificaMensagemParaUsuario(
      string _msg,
      EnumEstadoNotificacaoParaUsuario enumEstadoNotificacaoParaUsuario,
      EnumEstadoResultadoFinalProcesso enumEstadoResultadoFinalProcesso)
    {
      NotificarParaUsuarioEventArgs e = new NotificarParaUsuarioEventArgs(_msg, enumEstadoNotificacaoParaUsuario, enumEstadoResultadoFinalProcesso, this._rep.RepId, this._rep.Local);
      if (this.OnNotificarParaUsuario == null)
        return;
      this.OnNotificarParaUsuario((object) this, e);
    }

    private enum Estados
    {
      estInicial,
      estConexao,
      estEnviarChavePlublicaRSA,
      estEnvioAutenticacao,
      estSolicitoBilhete,
    }
  }
}
