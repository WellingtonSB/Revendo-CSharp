// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigNivelBiometriaRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigNivelBiometriaRepPlus : TarefaAbstrata
  {
    private RepBase _rep;
    private AjusteBiometrico _ajusteBio;
    private int _totRegUsuarios = int.MinValue;
    private bool _isBio;
    private GerenciadorConfigNivelBiometriaRepPlus.Estados _estadoRep;
    public static GerenciadorConfigNivelBiometriaRepPlus _gerenciadorConfigNivelBiometriaRepPlus;

    public static GerenciadorConfigNivelBiometriaRepPlus getInstance() => GerenciadorConfigNivelBiometriaRepPlus._gerenciadorConfigNivelBiometriaRepPlus != null ? GerenciadorConfigNivelBiometriaRepPlus._gerenciadorConfigNivelBiometriaRepPlus : new GerenciadorConfigNivelBiometriaRepPlus();

    public static GerenciadorConfigNivelBiometriaRepPlus getInstance(
      RepBase rep,
      AjusteBiometrico ajusteBio)
    {
      return GerenciadorConfigNivelBiometriaRepPlus._gerenciadorConfigNivelBiometriaRepPlus != null ? GerenciadorConfigNivelBiometriaRepPlus._gerenciadorConfigNivelBiometriaRepPlus : new GerenciadorConfigNivelBiometriaRepPlus(rep, ajusteBio);
    }

    public static GerenciadorConfigNivelBiometriaRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigNivelBiometriaRepPlus._gerenciadorConfigNivelBiometriaRepPlus != null ? GerenciadorConfigNivelBiometriaRepPlus._gerenciadorConfigNivelBiometriaRepPlus : new GerenciadorConfigNivelBiometriaRepPlus(rep);
    }

    public GerenciadorConfigNivelBiometriaRepPlus()
    {
    }

    public GerenciadorConfigNivelBiometriaRepPlus(RepBase rep, AjusteBiometrico ajusteBio)
    {
      this._rep = rep;
      this._ajusteBio = ajusteBio;
      if (this._rep is RepBio)
        this._isBio = true;
      else
        this._isBio = false;
    }

    public GerenciadorConfigNivelBiometriaRepPlus(RepBase rep) => this._rep = rep;

    ~GerenciadorConfigNivelBiometriaRepPlus()
    {
    }

    public override void IniciarProcesso()
    {
      this.NotificarParaUsuario(Resources.msgENVIANDO_CONFIGURACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
      this.Conectar(this._rep);
    }

    private void EnviarConfigBio()
    {
      try
      {
        this._estadoRep = GerenciadorConfigNivelBiometriaRepPlus.Estados.estEnvioConfigBio;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoConfigBio((byte) this._ajusteBio.BioLeitor, (byte) this._ajusteBio.BioCaptura, (byte) this._ajusteBio.BioFiltro, (byte) this._ajusteBio.BioVerif, (byte) this._ajusteBio.BioIdent, (byte) this._ajusteBio.BioTimeOut, (byte) this._ajusteBio.BioLFD, this._rep.VersaoFW, this._rep.VersaoBaixaFW, this._rep.ModeloFim)
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarConfigBioCAMA()
    {
      try
      {
        this._estadoRep = GerenciadorConfigNivelBiometriaRepPlus.Estados.estEnvioConfigBioCAMA;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoConfigBioCAMA((byte) this._ajusteBio.BioLeitor, (byte) this._ajusteBio.BioIdentCama, (byte) this._ajusteBio.BioTimeOutCama, this._ajusteBio.BioDedoDuplicado)
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      switch (this._estadoRep)
      {
        case GerenciadorConfigNivelBiometriaRepPlus.Estados.estEnvioConfigBio:
          if (envelope.Grp != (byte) 19 || envelope.Cmd != (byte) 101 && envelope.Cmd != (byte) 104)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1)
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgCONFIGURACOES_ATUALIZADAS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem1 = this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]);
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 11)
          {
            this.EnviarConfigBio();
            break;
          }
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem1, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
        case GerenciadorConfigNivelBiometriaRepPlus.Estados.estEnvioConfigBioCAMA:
          if (envelope.Grp != (byte) 20 || envelope.Cmd != (byte) 101)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1)
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgCONFIGURACOES_ATUALIZADAS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem2 = this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]);
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 11)
          {
            this.EnviarConfigBioCAMA();
            break;
          }
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem2, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
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

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        if (this._rep.ModeloFim == (int) byte.MaxValue)
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
        }
        else if (this._rep.ModeloFim == 0)
          this.EnviarConfigBio();
        else if (this._rep.ModeloFim == 1)
        {
          this.EnviarConfigBioCAMA();
        }
        else
        {
          string menssagem = "Modulo Biometrico do equipamento é diferente do cadastrado.";
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        }
      }
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigNivelBiometriaRepPlus.Estados.estEnvioConfigBio)
        menssagem = Resources.msgTIMEOUT_ENVIO_CONFIG_BIO;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigNivelBiometriaRepPlus.Estados.estEnvioConfigBio)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_CONFIG_BIO;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioConfigBio,
      estEnvioConfigBioCAMA,
    }
  }
}
