// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigNivelBiometriaRepPlusSagem
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigNivelBiometriaRepPlusSagem : TarefaAbstrata
  {
    private RepBase _rep;
    private AjusteBiometrico ajusteBio;
    private GerenciadorConfigNivelBiometriaRepPlusSagem.Estados estadoRep;
    public static GerenciadorConfigNivelBiometriaRepPlusSagem _gerenciadorConfigNivelBiometriaRepPlus;

    public static GerenciadorConfigNivelBiometriaRepPlusSagem getInstance() => GerenciadorConfigNivelBiometriaRepPlusSagem._gerenciadorConfigNivelBiometriaRepPlus != null ? GerenciadorConfigNivelBiometriaRepPlusSagem._gerenciadorConfigNivelBiometriaRepPlus : new GerenciadorConfigNivelBiometriaRepPlusSagem();

    public static GerenciadorConfigNivelBiometriaRepPlusSagem getInstance(
      RepBase rep,
      AjusteBiometrico ajusteBio)
    {
      return GerenciadorConfigNivelBiometriaRepPlusSagem._gerenciadorConfigNivelBiometriaRepPlus != null ? GerenciadorConfigNivelBiometriaRepPlusSagem._gerenciadorConfigNivelBiometriaRepPlus : new GerenciadorConfigNivelBiometriaRepPlusSagem(rep, ajusteBio);
    }

    public static GerenciadorConfigNivelBiometriaRepPlusSagem getInstance(
      RepBase rep)
    {
      return GerenciadorConfigNivelBiometriaRepPlusSagem._gerenciadorConfigNivelBiometriaRepPlus != null ? GerenciadorConfigNivelBiometriaRepPlusSagem._gerenciadorConfigNivelBiometriaRepPlus : new GerenciadorConfigNivelBiometriaRepPlusSagem(rep);
    }

    public GerenciadorConfigNivelBiometriaRepPlusSagem()
    {
    }

    public GerenciadorConfigNivelBiometriaRepPlusSagem(RepBase rep, AjusteBiometrico ajusteBio)
    {
      this._rep = rep;
      this.ajusteBio = ajusteBio;
    }

    public GerenciadorConfigNivelBiometriaRepPlusSagem(RepBase rep) => this._rep = rep;

    ~GerenciadorConfigNivelBiometriaRepPlusSagem()
    {
    }

    private void EnviarConfigBio()
    {
      try
      {
        this.estadoRep = GerenciadorConfigNivelBiometriaRepPlusSagem.Estados.estEnvioConfigBio;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoConfigBioSagem()
          {
            Leitor = (byte) 0,
            SegIdent = (byte) this.ajusteBio.IdentSagem,
            AdvancedMadchine = this.ajusteBio.AdvancedMadchine,
            DedoDuplicado = (byte) this.ajusteBio.DedoDuplicadoSagem,
            Filtro = (byte) this.ajusteBio.BioFiltro,
            SegVerif = (byte) this.ajusteBio.VerifSagem,
            TimeOut = (byte) this.ajusteBio.TimeoutSagem,
            FFD = (byte) this.ajusteBio.FFD
          }
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private string ExtrairRespostaBio(byte respostaBio)
    {
      string str = "";
      switch (respostaBio)
      {
        case 0:
          str = Resources.msgBIO_ERRO_COMUNICACAO;
          break;
        case 2:
          str = Resources.msgBIO_FALHA_COMANDO;
          break;
        case 3:
          str = Resources.msgBIO_NAO_MODO_MASTER;
          break;
        case 4:
          str = Resources.msgBIO_USUARIO_JA_CADASTRADO;
          break;
        case 5:
          str = Resources.msgBIO_USUARIO_NAO_CADASTRADO;
          break;
        case 6:
          str = Resources.msgBIO_BASE_CHEIA;
          break;
        case 7:
          str = Resources.msgBIO_TIME_OUT_COMUNICACAO;
          break;
        case 9:
          str = Resources.msgBIO_PARAMETRO_INVALIDO;
          break;
        case 22:
          str = Resources.msgBIO_TEMPLATE_INVALIDO;
          break;
      }
      return str;
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this.estadoRep != GerenciadorConfigNivelBiometriaRepPlusSagem.Estados.estEnvioConfigBio || envelope.Grp != (byte) 21 || envelope.Cmd != (byte) 101)
        return;
      if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1)
      {
        this.EncerrarConexao();
        this.NotificarParaUsuario(Resources.msgCONFIGURACOES_ATUALIZADAS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
      }
      else
      {
        string menssagem = this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]);
        if (envelope.MsgAplicacaoEmBytes[2] == (byte) 11)
        {
          this.EnviarConfigBio();
        }
        else
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        }
      }
    }

    public override void IniciarProcesso()
    {
      this.NotificarParaUsuario(Resources.msgENVIANDO_CONFIGURACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
      this.Conectar(this._rep);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarConfigBio();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this.estadoRep == GerenciadorConfigNivelBiometriaRepPlusSagem.Estados.estEnvioConfigBio)
        menssagem = Resources.msgTIMEOUT_ENVIO_CONFIG_BIO;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this.estadoRep == GerenciadorConfigNivelBiometriaRepPlusSagem.Estados.estEnvioConfigBio)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_CONFIG_BIO;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioConfigBio,
    }
  }
}
