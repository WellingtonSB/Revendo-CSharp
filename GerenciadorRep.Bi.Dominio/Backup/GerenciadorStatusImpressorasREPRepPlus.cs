// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorStatusImpressorasREPRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorStatusImpressorasREPRepPlus : TarefaAbstrata
  {
    private RepBase _rep;
    private GerenciadorStatusImpressorasREPRepPlus.Estados _estadoRep;
    private bool _chamadaPeloSdk;
    public static GerenciadorStatusImpressorasREPRepPlus _gerenciadorStatusImpressorasREPRepPlus;

    public event EventHandler<NotificarStatusImpressoraRepPlusEventArgs> OnNotificarStatusImpressora;

    public static GerenciadorStatusImpressorasREPRepPlus getInstance() => GerenciadorStatusImpressorasREPRepPlus._gerenciadorStatusImpressorasREPRepPlus != null ? GerenciadorStatusImpressorasREPRepPlus._gerenciadorStatusImpressorasREPRepPlus : new GerenciadorStatusImpressorasREPRepPlus();

    public static GerenciadorStatusImpressorasREPRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorStatusImpressorasREPRepPlus._gerenciadorStatusImpressorasREPRepPlus != null ? GerenciadorStatusImpressorasREPRepPlus._gerenciadorStatusImpressorasREPRepPlus : new GerenciadorStatusImpressorasREPRepPlus(rep);
    }

    public GerenciadorStatusImpressorasREPRepPlus()
    {
    }

    public GerenciadorStatusImpressorasREPRepPlus(RepBase rep)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarSolicitacaoStatusPapel()
    {
      this._estadoRep = GerenciadorStatusImpressorasREPRepPlus.Estados.estEnvioStatusPapelREP;
      MsgTCPAplicacaoStatusPapelREPRepPlus statusPapelRepRepPlus = new MsgTCPAplicacaoStatusPapelREPRepPlus();
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) statusPapelRepRepPlus
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorStatusImpressorasREPRepPlus.Estados.estEnvioStatusPapelREP || envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 106)
        return;
      NotificarStatusImpressoraRepPlusEventArgs e = new NotificarStatusImpressoraRepPlusEventArgs(this._rep.RepId, this._rep.Desc, this._rep.grupoId, (string) null, (string) null, DateTime.Now.ToShortTimeString(), Constantes.STATUS_COMUNICACAO.SUCESSO, (Constantes.STATUS_IMPRESSORA_REP_PLUS) envelope.MsgAplicacaoEmBytes[2], Constantes.STATUS_IMPRESSORA_REP_PLUS.NAO_INSTALADA, Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO);
      e.Resultado.MsgImpressora1 = "-";
      e.Resultado.MsgImpressora2 = "-";
      if (this.OnNotificarStatusImpressora != null)
        this.OnNotificarStatusImpressora((object) this.OnNotificarStatusImpressora, e);
      this.EncerrarConexao();
      this.NotificarParaUsuario(Resources.msgENVIANDO_CONFIGURACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarSolicitacaoStatusPapel();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
    }

    public override void TratarNenhumaResposta()
    {
      this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      NotificarStatusImpressoraRepPlusEventArgs e = EnvironmentHelper.VerificarRepEmUsoServico(this._rep.RepId) ? new NotificarStatusImpressoraRepPlusEventArgs(this._rep.RepId, this._rep.Desc, this._rep.grupoId, "Ocupado", "Ocupado", DateTime.Now.ToShortTimeString(), Constantes.STATUS_COMUNICACAO.FALHA_AO_CONECTAR, Constantes.STATUS_IMPRESSORA_REP_PLUS.INDEFINIDO, Constantes.STATUS_IMPRESSORA_REP_PLUS.INDEFINIDO, Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO) : new NotificarStatusImpressoraRepPlusEventArgs(this._rep.RepId, this._rep.Desc, this._rep.grupoId, "Sem resposta", "Sem resposta", DateTime.Now.ToShortTimeString(), Constantes.STATUS_COMUNICACAO.FALHA_AO_CONECTAR, Constantes.STATUS_IMPRESSORA_REP_PLUS.INDEFINIDO, Constantes.STATUS_IMPRESSORA_REP_PLUS.INDEFINIDO, Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO);
      if (this.OnNotificarStatusImpressora != null)
        this.OnNotificarStatusImpressora((object) this.OnNotificarStatusImpressora, e);
      this.EncerrarConexao();
    }

    private new enum Estados
    {
      estEnvioStatusPapelREP,
    }
  }
}
