// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorColetaSdk
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorColetaSdk : TarefaAbstrata
  {
    public const string DATA_REGISTRO_NULA = "0101010101";
    public const int TIPO_REGISTRO_NULO = 99;
    private RepBase _rep;
    private string _serialNoREP = string.Empty;
    private RepAFD _repAFD = new RepAFD();
    private int _numero_bytes_dump_MRP;
    private byte[] _bufferAFDRecebido = new byte[1];
    private int _ultimoNSRREP;
    private int _nsrSolicitado;
    private REPAFD.ResultadoBuscaRegistroNsr _resultadoBuscaRegistroNsr;
    private bool _novoNsrSolicitado;
    private Envelope _envelopeAnterior = new Envelope();
    private GerenciadorColetaSdk.Estados _estadoRep;
    private int _totRegistrosColetados;
    public static GerenciadorColetaSdk _gerenciadorColetaSdk;

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

    public event EventHandler<NotificarRegistroDoNsrSolicitadoEventArgs> OnNotificarColetaParaSdk;

    public static GerenciadorColetaSdk getInstance() => GerenciadorColetaSdk._gerenciadorColetaSdk != null ? GerenciadorColetaSdk._gerenciadorColetaSdk : new GerenciadorColetaSdk();

    public static GerenciadorColetaSdk getInstance(RepBase rep) => GerenciadorColetaSdk._gerenciadorColetaSdk != null ? GerenciadorColetaSdk._gerenciadorColetaSdk : new GerenciadorColetaSdk(rep);

    public GerenciadorColetaSdk()
    {
    }

    public GerenciadorColetaSdk(RepBase rep)
    {
      this._rep = rep;
      this.NUMERO_BYTES_DUMP_MRP = !RegistrySingleton.GetInstance().REDE_REMOTA ? 2048 : 512;
      this._bufferAFDRecebido = new byte[this.NUMERO_BYTES_DUMP_MRP];
    }

    public void SolicitaRegistroDoNsr(int nsrSolicitado)
    {
      this._nsrSolicitado = nsrSolicitado;
      if (this._estadoRep == GerenciadorColetaSdk.Estados.estAguardaNovaSolicitacaoNsr && this.ClienteSocket.Conectado)
      {
        this._novoNsrSolicitado = true;
        this.AnalisaBufferAFD(this._envelopeAnterior);
      }
      else
        this.IniciarProcesso();
      Application.DoEvents();
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    public override void TratarEnvelope(Envelope envelope)
    {
      switch (this._estadoRep)
      {
        case GerenciadorColetaSdk.Estados.estAguardandoSolicitaUltimoNSR:
          if (envelope.Grp == (byte) 250 && envelope.Cmd == (byte) 105)
          {
            byte[] numArray = new byte[4];
            Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 6, (Array) numArray, 0, 4);
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < 4; ++index)
              stringBuilder.Append(numArray[index].ToString("X").PadLeft(2, '0'));
            this._ultimoNSRREP = Convert.ToInt32(stringBuilder.ToString());
            if (this._nsrSolicitado <= this._ultimoNSRREP && this._nsrSolicitado != 0)
            {
              this.SolicitarDadosAFD(true, 0, 0);
              break;
            }
            this.NotificaMensagemParaSdk(REPAFD.ResultadoBuscaRegistroNsr.REGISTRO_INEXISTENTE, Resources.msgERRO_NSR_NAO_EXISTE, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
            this.EncerrarConexao();
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaSdk(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
        case GerenciadorColetaSdk.Estados.estAguardaDadosAFD:
          if (envelope.Grp == (byte) 250 && (envelope.Cmd == (byte) 102 || envelope.Cmd == (byte) 107))
          {
            this.AnalisaBufferAFD(envelope);
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaSdk(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
        case GerenciadorColetaSdk.Estados.estAguardaNovaSolicitacaoNsr:
          if (!this._novoNsrSolicitado)
            break;
          this._novoNsrSolicitado = false;
          this.AnalisaBufferAFD(envelope);
          break;
      }
    }

    private void SolicitarUltimoNSR()
    {
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
      this._estadoRep = GerenciadorColetaSdk.Estados.estAguardandoSolicitaUltimoNSR;
      MsgTCPAplicacaoSolicUltimoNSR aplicacaoSolicUltimoNsr = new MsgTCPAplicacaoSolicUltimoNSR();
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) aplicacaoSolicUltimoNsr;
      this.ClienteSocket.Enviar(envelope, true);
    }

    private void SolicitarDadosAFD(
      bool primeiraChamadaBusca,
      int posicaoUltimoNsrBloco,
      int ultimoNsrDoBloco)
    {
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
      this._estadoRep = GerenciadorColetaSdk.Estados.estAguardaDadosAFD;
      if (primeiraChamadaBusca)
      {
        this._repAFD.posMem = "000 000 000 000";
        this._repAFD.AtualizaPosicao(0);
      }
      else
      {
        this._repAFD.AtualizaPosicao(posicaoUltimoNsrBloco);
        this._repAFD.AtualizaPosicao(ultimoNsrDoBloco != 0 ? (this._nsrSolicitado - 1 - ultimoNsrDoBloco) * 15 : 2048);
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
      Thread.Sleep(50);
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
      switch (RegistroAFD.BlocoLidoContemNsrSolicitado(byteArrayAux, this._nsrSolicitado, out posicaoUltimoNsrBloco, out ultimoNsrDoBloco))
      {
        case REPAFD.BlocoMemoria.SEM_REGISTROS:
        case REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MENOR:
          this.SolicitarDadosAFD(true, 0, 0);
          break;
        case REPAFD.BlocoMemoria.CONTEM_NSR:
          RegistroAFD registroAfd2 = RegistroAFD.LeRegistroDoNsrSolicitado(byteArrayAux, this._nsrSolicitado);
          if (this._nsrSolicitado == this._ultimoNSRREP)
          {
            this.NotificaMensagemParaSdk(REPAFD.ResultadoBuscaRegistroNsr.ULTIMO_REGISTRO_LIDO, registroAfd2.dadosRegistro, registroAfd2.dtRegistro, registroAfd2.tipoRegistro, this._ultimoNSRREP.ToString(), this._serialNoREP);
            this.EncerrarConexao();
            break;
          }
          this.NotificaMensagemParaSdk(REPAFD.ResultadoBuscaRegistroNsr.REGISTRO_LIDO, registroAfd2.dadosRegistro, registroAfd2.dtRegistro, registroAfd2.tipoRegistro, this._ultimoNSRREP.ToString(), this._serialNoREP);
          this._estadoRep = GerenciadorColetaSdk.Estados.estAguardaNovaSolicitacaoNsr;
          break;
        case REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MAIOR:
          this.SolicitarDadosAFD(false, posicaoUltimoNsrBloco, ultimoNsrDoBloco);
          break;
      }
    }

    private void NotificaMensagemParaSdk(
      REPAFD.ResultadoBuscaRegistroNsr resultadoBuscaRegistroNsr,
      string registroNsrSolicitado,
      string dataRegistro,
      int tipoRegistro,
      string ultimoNsrRep,
      string numeroSerialRep)
    {
      NotificarRegistroDoNsrSolicitadoEventArgs e = new NotificarRegistroDoNsrSolicitadoEventArgs(resultadoBuscaRegistroNsr, registroNsrSolicitado, dataRegistro, tipoRegistro, ultimoNsrRep, numeroSerialRep);
      if (this.OnNotificarColetaParaSdk == null)
        return;
      this.OnNotificarColetaParaSdk((object) this, e);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        this._serialNoREP = this._rep.Serial;
        this.SolicitarUltimoNSR();
      }
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
          if (this._estadoRep == GerenciadorColetaSdk.Estados.estAguardaNovaSolicitacaoNsr)
            break;
          this.NotificaMensagemParaSdk(REPAFD.ResultadoBuscaRegistroNsr.ERRO, empty, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
      }
    }

    public override void TratarNenhumaResposta()
    {
      string registroNsrSolicitado = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorColetaSdk.Estados.estAguardandoSolicitaUltimoNSR:
          registroNsrSolicitado = Resources.msgNENHUMA_RESPOSTA_SOLIC_CONFIG_REP;
          break;
        case GerenciadorColetaSdk.Estados.estAguardaDadosAFD:
          registroNsrSolicitado = Resources.msgNENHUMA_RESPOSTA_SOLIC_DADOS_AFD;
          break;
        case GerenciadorColetaSdk.Estados.estAguardaNovaSolicitacaoNsr:
          registroNsrSolicitado = Resources.msgNENHUMA_RESPOSTA_SOLIC_DADOS_AFD;
          break;
      }
      this.EncerrarConexao();
      if (this._estadoRep == GerenciadorColetaSdk.Estados.estAguardaNovaSolicitacaoNsr)
        return;
      this.NotificaMensagemParaSdk(REPAFD.ResultadoBuscaRegistroNsr.ERRO, registroNsrSolicitado, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
    }

    private new enum Estados
    {
      estAguardandoSolicitaUltimoNSR,
      estAguardaDadosAFD,
      estAguardaNovaSolicitacaoNsr,
    }
  }
}
