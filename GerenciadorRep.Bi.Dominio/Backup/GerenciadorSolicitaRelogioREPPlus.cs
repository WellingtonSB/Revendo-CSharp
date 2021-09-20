// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorSolicitaRelogioREPPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorSolicitaRelogioREPPlus : TarefaAbstrata
  {
    private RepBase _rep;
    private GerenciadorSolicitaRelogioREPPlus.Estados _estadoRep;
    private bool _chamadaPeloSdk;
    private bool chamadaPelaSenior;
    private DateTime horaSenior;
    public static GerenciadorSolicitaRelogioREPPlus _gerenciadorSolicitaRelogioREPPlus;

    public event EventHandler<NotificarRelogioRepPlusEventArgs> OnNotificarRelogio;

    public static GerenciadorSolicitaRelogioREPPlus getInstance() => GerenciadorSolicitaRelogioREPPlus._gerenciadorSolicitaRelogioREPPlus != null ? GerenciadorSolicitaRelogioREPPlus._gerenciadorSolicitaRelogioREPPlus : new GerenciadorSolicitaRelogioREPPlus();

    public static GerenciadorSolicitaRelogioREPPlus getInstance(
      RepBase rep)
    {
      return GerenciadorSolicitaRelogioREPPlus._gerenciadorSolicitaRelogioREPPlus != null ? GerenciadorSolicitaRelogioREPPlus._gerenciadorSolicitaRelogioREPPlus : new GerenciadorSolicitaRelogioREPPlus(rep);
    }

    public static GerenciadorSolicitaRelogioREPPlus getInstance(
      RepBase rep,
      bool chamadoPeloSDK)
    {
      return GerenciadorSolicitaRelogioREPPlus._gerenciadorSolicitaRelogioREPPlus != null ? GerenciadorSolicitaRelogioREPPlus._gerenciadorSolicitaRelogioREPPlus : new GerenciadorSolicitaRelogioREPPlus(rep, chamadoPeloSDK);
    }

    public GerenciadorSolicitaRelogioREPPlus()
    {
    }

    public GerenciadorSolicitaRelogioREPPlus(RepBase rep)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
    }

    public GerenciadorSolicitaRelogioREPPlus(RepBase rep, DateTime horaSenior)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
      this.horaSenior = horaSenior;
      this.chamadaPelaSenior = true;
    }

    public GerenciadorSolicitaRelogioREPPlus(RepBase rep, bool chamadoPeloSDK)
    {
      this._rep = rep;
      this._chamadaPeloSdk = true;
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarSolicitacaoRelogio()
    {
      this._estadoRep = GerenciadorSolicitaRelogioREPPlus.Estados.estEnvioSolicitaRelogioREP;
      MsgTCPAplicacaoSolicitaRelogioREPPlus solicitaRelogioRepPlus = new MsgTCPAplicacaoSolicitaRelogioREPPlus();
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) solicitaRelogioRepPlus
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorSolicitaRelogioREPPlus.Estados.estEnvioSolicitaRelogioREP || envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 102)
        return;
      this.EncerrarConexao();
      NotificarRelogioRepPlusEventArgs e = new NotificarRelogioRepPlusEventArgs(envelope.MsgAplicacaoEmBytes);
      if (this.OnNotificarRelogio != null)
        this.OnNotificarRelogio((object) this.OnNotificarRelogio, e);
      this.NotificarParaUsuario(Resources.msgENVIANDO_CONFIGURACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarSolicitacaoRelogio();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorSolicitaRelogioREPPlus.Estados.estEnvioSolicitaRelogioREP)
        menssagem = Resources.msgTIMEOUT_SOLICIT_DADOS_EMPREGADOR;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string empty = string.Empty;
      int estadoRep = (int) this._estadoRep;
      this.EncerrarConexao();
      this.NotificarParaUsuario(empty, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioSolicitaRelogioREP,
    }
  }
}
