// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigHorarioVeraoREPRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigHorarioVeraoREPRepPlus : TarefaAbstrata
  {
    private Relogio _entRelogio;
    private RepBase _rep;
    private bool chamadaPeloSdk;
    private bool chamadaPelaSenior;
    private GerenciadorConfigHorarioVeraoREPRepPlus.Estados _estadoRep;
    public static GerenciadorConfigHorarioVeraoREPRepPlus _gerenciadorConfigHorarioVeraoREPRepPlus;

    public bool ChamadaPelaSenior
    {
      get => this.chamadaPelaSenior;
      set => this.chamadaPelaSenior = value;
    }

    public static GerenciadorConfigHorarioVeraoREPRepPlus getInstance() => GerenciadorConfigHorarioVeraoREPRepPlus._gerenciadorConfigHorarioVeraoREPRepPlus != null ? GerenciadorConfigHorarioVeraoREPRepPlus._gerenciadorConfigHorarioVeraoREPRepPlus : new GerenciadorConfigHorarioVeraoREPRepPlus();

    public static GerenciadorConfigHorarioVeraoREPRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigHorarioVeraoREPRepPlus._gerenciadorConfigHorarioVeraoREPRepPlus != null ? GerenciadorConfigHorarioVeraoREPRepPlus._gerenciadorConfigHorarioVeraoREPRepPlus : new GerenciadorConfigHorarioVeraoREPRepPlus(rep);
    }

    public static GerenciadorConfigHorarioVeraoREPRepPlus getInstance(
      RepBase rep,
      bool senior)
    {
      return GerenciadorConfigHorarioVeraoREPRepPlus._gerenciadorConfigHorarioVeraoREPRepPlus != null ? GerenciadorConfigHorarioVeraoREPRepPlus._gerenciadorConfigHorarioVeraoREPRepPlus : new GerenciadorConfigHorarioVeraoREPRepPlus(rep, senior);
    }

    public static GerenciadorConfigHorarioVeraoREPRepPlus getInstance(
      RepBase rep,
      Relogio entRelogio)
    {
      return GerenciadorConfigHorarioVeraoREPRepPlus._gerenciadorConfigHorarioVeraoREPRepPlus != null ? GerenciadorConfigHorarioVeraoREPRepPlus._gerenciadorConfigHorarioVeraoREPRepPlus : new GerenciadorConfigHorarioVeraoREPRepPlus(rep, entRelogio);
    }

    public GerenciadorConfigHorarioVeraoREPRepPlus() => this.inicializarHorariodeVerao();

    public GerenciadorConfigHorarioVeraoREPRepPlus(RepBase rep)
    {
      this._rep = rep;
      this.chamadaPeloSdk = false;
      this.inicializarHorariodeVerao();
    }

    public GerenciadorConfigHorarioVeraoREPRepPlus(RepBase rep, bool Senior)
    {
      this._rep = rep;
      this.chamadaPeloSdk = false;
      this.chamadaPelaSenior = Senior;
      this.inicializarHorariodeVerao();
    }

    public GerenciadorConfigHorarioVeraoREPRepPlus(RepBase rep, Relogio entRelogio)
    {
      this._rep = rep;
      this._entRelogio = entRelogio;
      this.chamadaPeloSdk = true;
      this.inicializarHorariodeVerao();
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarConfiguracoesHorarioVerao()
    {
      this._estadoRep = GerenciadorConfigHorarioVeraoREPRepPlus.Estados.estEnvioHorarioVerao;
      try
      {
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoConfigHorarioVeraoREP(this._entRelogio.InicioHorVerao, this._entRelogio.FimHorVerao)
        }, true);
      }
      catch (Exception ex)
      {
        this.NotificarParaUsuario(ex.Message.ToString(), EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      }
    }

    private void inicializarHorariodeVerao()
    {
      if (this.chamadaPeloSdk)
        return;
      Relogio relogio = new Relogio();
      if (this.chamadaPelaSenior)
        this._entRelogio = relogio.PesquisarHorVerao(this._rep.RepId);
      else
        this._entRelogio = relogio.PesquisarHorVeraoMulti();
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorConfigHorarioVeraoREPRepPlus.Estados.estEnvioHorarioVerao || envelope.Grp != (byte) 10 || envelope.Cmd != (byte) 101)
        return;
      if (!this.chamadaPeloSdk)
      {
        if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
        }
        else
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_HORARIO_VERAO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        }
      }
      else
      {
        this.EncerrarConexao();
        this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_HORARIO_VERAO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarConfiguracoesHorarioVerao();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigHorarioVeraoREPRepPlus.Estados.estEnvioHorarioVerao)
        menssagem = Resources.msgTIMEOUT_ENVIO_CONFIG_GERAL;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigHorarioVeraoREPRepPlus.Estados.estEnvioHorarioVerao)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioHorarioVerao,
      estFinal,
    }
  }
}
