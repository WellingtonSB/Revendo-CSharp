// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorColetaRepPlusNSRSenior
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Text;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorColetaRepPlusNSRSenior : TarefaAbstrata
  {
    public const string DATA_REGISTRO_NULA = "0101010101";
    public const int TIPO_REGISTRO_NULO = 99;
    private RepBase rep;
    private RepAFD repAFD = new RepAFD();
    private int _numero_bytes_dump_MRP;
    private byte[] _bufferAFDRecebido = new byte[1];
    private int _ultimoNSRREP;
    private int nsrSolicitado;
    private REPAFD.ResultadoBuscaRegistroNsr _resultadoBuscaRegistroNsr;
    private bool _novoNsrSolicitado;
    private Envelope _envelopeAnterior = new Envelope();
    private GerenciadorColetaRepPlusNSRSenior.Estados estadoRep;
    private int _totRegistrosColetados;
    public static GerenciadorColetaRepPlusNSRSenior _gerenciadorColetaRepPlusNSRSenior;

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

    public static GerenciadorColetaRepPlusNSRSenior getInstance() => GerenciadorColetaRepPlusNSRSenior._gerenciadorColetaRepPlusNSRSenior != null ? GerenciadorColetaRepPlusNSRSenior._gerenciadorColetaRepPlusNSRSenior : new GerenciadorColetaRepPlusNSRSenior();

    public static GerenciadorColetaRepPlusNSRSenior getInstance(
      RepBase rep)
    {
      return GerenciadorColetaRepPlusNSRSenior._gerenciadorColetaRepPlusNSRSenior != null ? GerenciadorColetaRepPlusNSRSenior._gerenciadorColetaRepPlusNSRSenior : new GerenciadorColetaRepPlusNSRSenior(rep);
    }

    public GerenciadorColetaRepPlusNSRSenior()
    {
    }

    public GerenciadorColetaRepPlusNSRSenior(RepBase rep)
    {
      this.rep = rep;
      this.NUMERO_BYTES_DUMP_MRP = 512;
      this._bufferAFDRecebido = new byte[this.NUMERO_BYTES_DUMP_MRP];
    }

    public void SolicitaRegistroDoNsr(int nsrSolicitado) => this.nsrSolicitado = nsrSolicitado;

    public override void TratarEnvelope(Envelope envelope)
    {
      switch (this.estadoRep)
      {
        case GerenciadorColetaRepPlusNSRSenior.Estados.estAguardandoSolicitaUltimoNSR:
          if (envelope.Grp == (byte) 15 && envelope.Cmd == (byte) 107)
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
            this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.REGISTRO_INEXISTENTE, Resources.msgERRO_NSR_NAO_EXISTE, "0101010101", 99, this._ultimoNSRREP.ToString(), this.rep.Serial);
            this.EncerrarConexao();
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this.rep.Serial);
          break;
        case GerenciadorColetaRepPlusNSRSenior.Estados.estAguardaDadosAFD:
          if (envelope.Grp == (byte) 16 && envelope.Cmd == (byte) 100)
          {
            this.AnalisaBufferAFD(envelope);
            break;
          }
          this.EncerrarConexao();
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, Resources.msgERRO_NUM_TERMINAL_ERRADO, "0101010101", 99, this._ultimoNSRREP.ToString(), this.rep.Serial);
          break;
        case GerenciadorColetaRepPlusNSRSenior.Estados.estAguardaNovaSolicitacaoNsr:
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
      this.estadoRep = GerenciadorColetaRepPlusNSRSenior.Estados.estAguardandoSolicitaUltimoNSR;
      MsgTCPAplicacaoSolicUltimoNSRRepPlus ultimoNsrRepPlus = new MsgTCPAplicacaoSolicUltimoNSRRepPlus();
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) ultimoNsrRepPlus;
      this.ClienteSocket.Enviar(envelope, true);
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

    private void SolicitarDadosAFD(
      bool primeiraChamadaBusca,
      int posicaoUltimoNsrBloco,
      int ultimoNsrDoBloco)
    {
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
      this.estadoRep = GerenciadorColetaRepPlusNSRSenior.Estados.estAguardaDadosAFD;
      if (primeiraChamadaBusca)
      {
        this.repAFD.posMem = "000 000 000 000";
        this.repAFD.AtualizaPosicao(0);
      }
      else
      {
        this.repAFD.AtualizaPosicao(posicaoUltimoNsrBloco);
        this.repAFD.AtualizaPosicao(ultimoNsrDoBloco != 0 ? (this.nsrSolicitado - 1 - ultimoNsrDoBloco) * 15 : 512);
      }
      MsgTCPAplicacaoSolicDadosAFDPequenoRepPlus afdPequenoRepPlus = new MsgTCPAplicacaoSolicDadosAFDPequenoRepPlus(this.repAFD.GetPosicaoParaPesquisaRepPlus());
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) afdPequenoRepPlus;
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
      switch (RegistroAFDRepPlus.BlocoLidoContemNsrSolicitadoSenior(byteArrayAux, this.nsrSolicitado, out posicaoUltimoNsrBloco, out ultimoNsrDoBloco))
      {
        case REPAFD.BlocoMemoria.SEM_REGISTROS:
        case REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MENOR:
          this.SolicitarDadosAFD(true, 0, 0);
          break;
        case REPAFD.BlocoMemoria.CONTEM_NSR:
          RegistroAFD registroAfd2 = RegistroAFDRepPlus.LeRegistroDoNsrSolicitado(byteArrayAux, this.nsrSolicitado);
          this.repAFD.AtualizaPosicao(posicaoUltimoNsrBloco);
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.REGISTRO_LIDO, registroAfd2.dadosRegistro, registroAfd2.dtRegistro, registroAfd2.tipoRegistro, this.nsrSolicitado.ToString(), this.rep.Serial);
          this.EncerrarConexao();
          break;
        case REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MAIOR:
          Console.WriteLine("Ultimo Nsr do Bloco: " + (object) ultimoNsrDoBloco);
          this.SolicitarDadosAFD(false, posicaoUltimoNsrBloco, ultimoNsrDoBloco);
          break;
      }
    }

    private void AtualizaAFDRegistro(RegistroAFD registroDoNsrSolicitado)
    {
      switch (registroDoNsrSolicitado.tipoRegistro)
      {
        case 2:
          this.repAFD.AtualizaPosicao(274);
          break;
        case 3:
          this.repAFD.AtualizaPosicao(16);
          break;
        case 4:
          this.repAFD.AtualizaPosicao(15);
          break;
        case 5:
          this.repAFD.AtualizaPosicao(69);
          break;
        case 6:
          this.repAFD.AtualizaPosicao(12);
          break;
      }
    }

    private void NotificaMensagemParaDriverSenior(
      REPAFD.ResultadoBuscaRegistroNsr resultadoBuscaRegistroNsr,
      string registroNsrSolicitado,
      string dataRegistro,
      int tipoRegistro,
      string ultimoNsrRep,
      string numeroSerialRep)
    {
      NotificarRegistroDoNsrSolicitadoSeniorEventArgs e = new NotificarRegistroDoNsrSolicitadoSeniorEventArgs(resultadoBuscaRegistroNsr, registroNsrSolicitado, dataRegistro, tipoRegistro, ultimoNsrRep, numeroSerialRep, this.repAFD.posMem, this.rep.RepId);
      if (this.OnNotificarColetaParaDriverSenior == null)
        return;
      this.OnNotificarColetaParaDriverSenior((object) this, e);
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
      switch (this.estadoRep)
      {
        default:
          this.EncerrarConexao();
          if (this.estadoRep == GerenciadorColetaRepPlusNSRSenior.Estados.estAguardaNovaSolicitacaoNsr)
            break;
          this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, empty, "0101010101", 99, this._ultimoNSRREP.ToString(), this.rep.Serial);
          break;
      }
    }

    public override void TratarNenhumaResposta()
    {
      string registroNsrSolicitado = string.Empty;
      switch (this.estadoRep)
      {
        case GerenciadorColetaRepPlusNSRSenior.Estados.estAguardandoSolicitaUltimoNSR:
          registroNsrSolicitado = Resources.msgNENHUMA_RESPOSTA_SOLIC_CONFIG_REP;
          break;
        case GerenciadorColetaRepPlusNSRSenior.Estados.estAguardaDadosAFD:
          registroNsrSolicitado = Resources.msgNENHUMA_RESPOSTA_SOLIC_DADOS_AFD;
          break;
        case GerenciadorColetaRepPlusNSRSenior.Estados.estAguardaNovaSolicitacaoNsr:
          registroNsrSolicitado = Resources.msgNENHUMA_RESPOSTA_SOLIC_DADOS_AFD;
          break;
      }
      this.EncerrarConexao();
      if (this.estadoRep == GerenciadorColetaRepPlusNSRSenior.Estados.estAguardaNovaSolicitacaoNsr)
        return;
      this.NotificaMensagemParaDriverSenior(REPAFD.ResultadoBuscaRegistroNsr.ERRO, registroNsrSolicitado, "0101010101", 99, this._ultimoNSRREP.ToString(), this.rep.Serial);
    }

    public override void IniciarProcesso() => this.Conectar(this.rep);

    private new enum Estados
    {
      estAguardandoSolicitaUltimoNSR,
      estAguardaDadosAFD,
      estAguardaNovaSolicitacaoNsr,
    }
  }
}
