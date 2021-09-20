// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorStatusImpressorasREP
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Timers;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorStatusImpressorasREP : TarefaAbstrata
  {
    private RepBase _rep;
    private Timer _timerNenhumaResposta;
    private GerenciadorStatusImpressorasREP.Estados _estadoRep;
    private bool _chamadaPeloSdk;
    public static GerenciadorStatusImpressorasREP _gerenciadorStatusImpressorasREP;

    public Timer TimerNenhumaResposta
    {
      get => this._timerNenhumaResposta;
      set => this._timerNenhumaResposta = value;
    }

    public event EventHandler<NotificarStatusImpressoraEventArgs> OnNotificarStatusImpressora;

    public static GerenciadorStatusImpressorasREP getInstance() => GerenciadorStatusImpressorasREP._gerenciadorStatusImpressorasREP != null ? GerenciadorStatusImpressorasREP._gerenciadorStatusImpressorasREP : new GerenciadorStatusImpressorasREP();

    public static GerenciadorStatusImpressorasREP getInstance(
      RepBase rep)
    {
      return GerenciadorStatusImpressorasREP._gerenciadorStatusImpressorasREP != null ? GerenciadorStatusImpressorasREP._gerenciadorStatusImpressorasREP : new GerenciadorStatusImpressorasREP(rep);
    }

    public GerenciadorStatusImpressorasREP()
    {
    }

    public GerenciadorStatusImpressorasREP(RepBase rep)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarSolicitacaoStatusPapel()
    {
      this._estadoRep = GerenciadorStatusImpressorasREP.Estados.estEnvioStatusPapelREP;
      MsgTCPAplicacaoStatusPapelREP aplicacaoStatusPapelRep = new MsgTCPAplicacaoStatusPapelREP();
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) aplicacaoStatusPapelRep
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorStatusImpressorasREP.Estados.estEnvioStatusPapelREP || envelope.Grp != (byte) 250 || envelope.Cmd != (byte) 101)
        return;
      NotificarStatusImpressoraEventArgs e = new NotificarStatusImpressoraEventArgs(this._rep.RepId, this._rep.Desc, this._rep.grupoId, (string) null, (string) null, DateTime.Now.ToShortTimeString(), Constantes.STATUS_COMUNICACAO.SUCESSO, (Constantes.STATUS_IMPRESSORA) envelope.MsgAplicacaoEmBytes[2], (Constantes.STATUS_IMPRESSORA) envelope.MsgAplicacaoEmBytes[3], Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO);
      switch (e.Resultado.StatusImpressora1)
      {
        case Constantes.STATUS_IMPRESSORA.SEM_PAPEL:
          e.Resultado.MsgImpressora1 = "Sem Papel";
          break;
        case Constantes.STATUS_IMPRESSORA.COM_PAPEL:
          e.Resultado.MsgImpressora1 = "Com Papel";
          break;
        case Constantes.STATUS_IMPRESSORA.NAO_INSTALADA:
          e.Resultado.MsgImpressora1 = "Não Instalada";
          break;
        case Constantes.STATUS_IMPRESSORA.INDEFINIDO:
          e.Resultado.MsgImpressora1 = "Sem resposta";
          break;
        case Constantes.STATUS_IMPRESSORA.IMP_COM_PAPEL_QUASE_VAZIO:
          e.Resultado.MsgImpressora1 = "Com Papel";
          break;
        case Constantes.STATUS_IMPRESSORA.IMP_COM_PAPEL_METADE:
          e.Resultado.MsgImpressora1 = "Com Papel";
          break;
        case Constantes.STATUS_IMPRESSORA.IMP_COM_PAPEL_QUASE_CHEIO:
          e.Resultado.MsgImpressora1 = "Com Papel";
          break;
        default:
          e.Resultado.MsgImpressora1 = "Sem resposta";
          break;
      }
      switch (e.Resultado.StatusImpressora2)
      {
        case Constantes.STATUS_IMPRESSORA.SEM_PAPEL:
          e.Resultado.MsgImpressora2 = "Sem Papel";
          break;
        case Constantes.STATUS_IMPRESSORA.COM_PAPEL:
          e.Resultado.MsgImpressora2 = "Com Papel";
          break;
        case Constantes.STATUS_IMPRESSORA.NAO_INSTALADA:
          e.Resultado.MsgImpressora2 = "Não Instalada";
          break;
        case Constantes.STATUS_IMPRESSORA.INDEFINIDO:
          e.Resultado.MsgImpressora2 = "Sem resposta";
          break;
        case Constantes.STATUS_IMPRESSORA.IMP_COM_PAPEL_QUASE_VAZIO:
          e.Resultado.MsgImpressora2 = "Com Papel";
          break;
        case Constantes.STATUS_IMPRESSORA.IMP_COM_PAPEL_METADE:
          e.Resultado.MsgImpressora2 = "Com Papel";
          break;
        case Constantes.STATUS_IMPRESSORA.IMP_COM_PAPEL_QUASE_CHEIO:
          e.Resultado.MsgImpressora2 = "Com Papel";
          break;
        default:
          e.Resultado.MsgImpressora2 = "Sem resposta";
          break;
      }
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
      this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      NotificarStatusImpressoraEventArgs e = new NotificarStatusImpressoraEventArgs(this._rep.RepId, this._rep.Desc, this._rep.grupoId, "Sem resposta", "Sem resposta", DateTime.Now.ToShortTimeString(), Constantes.STATUS_COMUNICACAO.FALHA_AO_CONECTAR, Constantes.STATUS_IMPRESSORA.INDEFINIDO, Constantes.STATUS_IMPRESSORA.INDEFINIDO, Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO);
      if (this.OnNotificarStatusImpressora != null)
        this.OnNotificarStatusImpressora((object) this.OnNotificarStatusImpressora, e);
      this.EncerrarConexao();
    }

    public override void TratarNenhumaResposta()
    {
      this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      NotificarStatusImpressoraEventArgs e = EnvironmentHelper.VerificarRepEmUsoServico(this._rep.RepId) ? new NotificarStatusImpressoraEventArgs(this._rep.RepId, this._rep.Desc, this._rep.grupoId, "Ocupado", "Ocupado", DateTime.Now.ToShortTimeString(), Constantes.STATUS_COMUNICACAO.FALHA_AO_CONECTAR, Constantes.STATUS_IMPRESSORA.INDEFINIDO, Constantes.STATUS_IMPRESSORA.INDEFINIDO, Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO) : new NotificarStatusImpressoraEventArgs(this._rep.RepId, this._rep.Desc, this._rep.grupoId, "Sem resposta", "Sem resposta", DateTime.Now.ToShortTimeString(), Constantes.STATUS_COMUNICACAO.FALHA_AO_CONECTAR, Constantes.STATUS_IMPRESSORA.INDEFINIDO, Constantes.STATUS_IMPRESSORA.INDEFINIDO, Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA.FINALIZADO);
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
