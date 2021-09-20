// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorColetaRepPlusSdk
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Text;
using System.Threading;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorColetaRepPlusSdk : TarefaAbstrata
  {
    public const string DATA_REGISTRO_NULA = "0101010101";
    public const int TIPO_REGISTRO_NULO = 99;
    private RepBase _rep;
    private string _serialNoREP = string.Empty;
    private RepAFD _repAFD = new RepAFD();
    private int _numero_bytes_dump_MRP;
    private byte[] _bufferAFDRecebido = new byte[1];
    private int _ultimoNSRREP;
    private int _posMenUlt;
    private string _posMemAtulizado;
    private int _nsrSolicitado;
    private REPAFD.ResultadoBuscaRegistroNsr _resultadoBuscaRegistroNsr;
    private bool _novoNsrSolicitado;
    private Envelope _envelopeAnterior = new Envelope();
    private GerenciadorColetaRepPlusSdk.Estados _estadoRep;
    private int _totRegistrosColetados;
    public static GerenciadorColetaRepPlusSdk _gerenciadorColetaRepPlusSdk;

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

    public static GerenciadorColetaRepPlusSdk getInstance() => GerenciadorColetaRepPlusSdk._gerenciadorColetaRepPlusSdk != null ? GerenciadorColetaRepPlusSdk._gerenciadorColetaRepPlusSdk : new GerenciadorColetaRepPlusSdk();

    public static GerenciadorColetaRepPlusSdk getInstance(RepBase rep) => GerenciadorColetaRepPlusSdk._gerenciadorColetaRepPlusSdk != null ? GerenciadorColetaRepPlusSdk._gerenciadorColetaRepPlusSdk : new GerenciadorColetaRepPlusSdk(rep);

    public GerenciadorColetaRepPlusSdk()
    {
    }

    public GerenciadorColetaRepPlusSdk(RepBase rep)
    {
      this._rep = rep;
      this.NUMERO_BYTES_DUMP_MRP = 512;
      this._bufferAFDRecebido = new byte[this.NUMERO_BYTES_DUMP_MRP];
    }

    public void SolicitaRegistroDoNsr(int nsrSolicitado)
    {
      this._nsrSolicitado = nsrSolicitado;
      if (this._estadoRep == GerenciadorColetaRepPlusSdk.Estados.estAguardaNovaSolicitacaoNsr && this.ClienteSocket.Conectado)
      {
        this._novoNsrSolicitado = true;
        this.AnalisaBufferAFD(this._envelopeAnterior);
      }
      else
        this.IniciarProcesso();
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    public override void TratarEnvelope(Envelope envelope)
    {
      switch (this._estadoRep)
      {
        case GerenciadorColetaRepPlusSdk.Estados.estAguardandoSolicitaUltimoNSR:
          if (envelope.Grp == (byte) 15 && envelope.Cmd == (byte) 107)
          {
            byte[] numArray1 = new byte[4];
            byte[] numArray2 = new byte[4];
            Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 6, (Array) numArray1, 0, 4);
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < 4; ++index)
              stringBuilder.Append(numArray1[index].ToString("X").PadLeft(2, '0'));
            this._ultimoNSRREP = Convert.ToInt32(stringBuilder.ToString());
            Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 14, (Array) numArray2, 0, 4);
            Array.Reverse((Array) numArray2);
            this._posMenUlt = BitConverter.ToInt32(numArray2, 0);
            if (this._nsrSolicitado <= this._ultimoNSRREP && this._nsrSolicitado != 0)
            {
              this._repAFD.posMem = "000 000 000 000";
              this._posMemAtulizado = "000 000 000 000";
              this.SolicitarDadosAFD(0, 0);
              break;
            }
            this.NotificaMensagemParaSdk(REPAFD.ResultadoBuscaRegistroNsr.REGISTRO_INEXISTENTE, Resources.msgERRO_NSR_NAO_EXISTE, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
            this.EncerrarConexao();
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaSdk(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
        case GerenciadorColetaRepPlusSdk.Estados.estAguardaDadosAFD:
          if (envelope.Grp == (byte) 16 && envelope.Cmd == (byte) 100)
          {
            this.AnalisaBufferAFD(envelope);
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaSdk(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this._serialNoREP);
          break;
        case GerenciadorColetaRepPlusSdk.Estados.estAguardaNovaSolicitacaoNsr:
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
      this._estadoRep = GerenciadorColetaRepPlusSdk.Estados.estAguardandoSolicitaUltimoNSR;
      MsgTCPAplicacaoSolicUltimoNSRRepPlus ultimoNsrRepPlus = new MsgTCPAplicacaoSolicUltimoNSRRepPlus();
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) ultimoNsrRepPlus;
      this.ClienteSocket.Enviar(envelope, true);
    }

    private void SolicitarDadosAFD(int posicaoUltimoNsrBloco, int ultimoNsrDoBloco)
    {
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
      this._estadoRep = GerenciadorColetaRepPlusSdk.Estados.estAguardaDadosAFD;
      this._repAFD.AtualizaPosicao(ultimoNsrDoBloco, this._nsrSolicitado, posicaoUltimoNsrBloco);
      MsgTCPAplicacaoSolicDadosAFDPequenoRepPlus afdPequenoRepPlus = new MsgTCPAplicacaoSolicDadosAFDPequenoRepPlus(this._repAFD.GetPosicaoParaPesquisaRepPlus());
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) afdPequenoRepPlus;
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
      switch (RegistroAFDRepPlus.BlocoLidoContemNsrSolicitado(byteArrayAux, this._nsrSolicitado, out posicaoUltimoNsrBloco, out ultimoNsrDoBloco))
      {
        case REPAFD.BlocoMemoria.SEM_REGISTROS:
        case REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MENOR:
          this._repAFD.posMem = "000 000 000 000";
          this.SolicitarDadosAFD(0, 0);
          break;
        case REPAFD.BlocoMemoria.CONTEM_NSR:
          RegistroAFD registroAfd2 = RegistroAFDRepPlus.LeRegistroDoNsrSolicitado(byteArrayAux, this._nsrSolicitado);
          if (this._nsrSolicitado == this._ultimoNSRREP)
          {
            this.NotificaMensagemParaSdk(REPAFD.ResultadoBuscaRegistroNsr.ULTIMO_REGISTRO_LIDO, registroAfd2.dadosRegistro, registroAfd2.dtRegistro, registroAfd2.tipoRegistro, this._ultimoNSRREP.ToString(), this._serialNoREP);
            this.EncerrarConexao();
            break;
          }
          this.NotificaMensagemParaSdk(REPAFD.ResultadoBuscaRegistroNsr.REGISTRO_LIDO, registroAfd2.dadosRegistro, registroAfd2.dtRegistro, registroAfd2.tipoRegistro, this._ultimoNSRREP.ToString(), this._serialNoREP);
          this._estadoRep = GerenciadorColetaRepPlusSdk.Estados.estAguardaNovaSolicitacaoNsr;
          break;
        case REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MAIOR:
          if (!this.PassouUltimoBloco())
          {
            this.SolicitarDadosAFD(posicaoUltimoNsrBloco, ultimoNsrDoBloco);
            break;
          }
          this._repAFD.posMem = this._posMemAtulizado;
          this._repAFD.AtualizaPosicao(512);
          this._posMemAtulizado = this._repAFD.posMem;
          this.SolicitarDadosAFD(0, 0);
          break;
      }
    }

    private bool PassouUltimoBloco()
    {
      string[] strArray = this._repAFD.posMem.Split(' ');
      byte[] numArray = new byte[4];
      Array.Reverse((Array) strArray);
      for (int index = 0; index < strArray.Length; ++index)
        numArray[index] = Convert.ToByte(strArray[index]);
      return BitConverter.ToInt32(numArray, 0) > this._posMenUlt;
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
          if (this._estadoRep == GerenciadorColetaRepPlusSdk.Estados.estAguardaNovaSolicitacaoNsr)
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
        case GerenciadorColetaRepPlusSdk.Estados.estAguardandoSolicitaUltimoNSR:
          registroNsrSolicitado = Resources.msgNENHUMA_RESPOSTA_SOLIC_CONFIG_REP;
          break;
        case GerenciadorColetaRepPlusSdk.Estados.estAguardaDadosAFD:
          registroNsrSolicitado = Resources.msgNENHUMA_RESPOSTA_SOLIC_DADOS_AFD;
          break;
        case GerenciadorColetaRepPlusSdk.Estados.estAguardaNovaSolicitacaoNsr:
          registroNsrSolicitado = Resources.msgNENHUMA_RESPOSTA_SOLIC_DADOS_AFD;
          break;
      }
      this.EncerrarConexao();
      if (this._estadoRep == GerenciadorColetaRepPlusSdk.Estados.estAguardaNovaSolicitacaoNsr)
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
