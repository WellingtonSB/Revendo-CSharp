// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorSolicitarConfigFabricaVersaoProxPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorSolicitarConfigFabricaVersaoProxPlus : TarefaAbstrata
  {
    private RepBase _rep;
    private GerenciadorSolicitarConfigFabricaVersaoProxPlus.Estados _estadoRep;
    public static GerenciadorSolicitarConfigFabricaVersaoProxPlus _gerenciadorSolicitarConfigFabricaVersaoProxPlus;

    public event EventHandler<NotificarConfigVersaoProxEventArgs> OnNotificarConfigVersaoProx;

    public static GerenciadorSolicitarConfigFabricaVersaoProxPlus getInstance() => GerenciadorSolicitarConfigFabricaVersaoProxPlus._gerenciadorSolicitarConfigFabricaVersaoProxPlus != null ? GerenciadorSolicitarConfigFabricaVersaoProxPlus._gerenciadorSolicitarConfigFabricaVersaoProxPlus : new GerenciadorSolicitarConfigFabricaVersaoProxPlus();

    public static GerenciadorSolicitarConfigFabricaVersaoProxPlus getInstance(
      RepBase rep)
    {
      return GerenciadorSolicitarConfigFabricaVersaoProxPlus._gerenciadorSolicitarConfigFabricaVersaoProxPlus != null ? GerenciadorSolicitarConfigFabricaVersaoProxPlus._gerenciadorSolicitarConfigFabricaVersaoProxPlus : new GerenciadorSolicitarConfigFabricaVersaoProxPlus(rep);
    }

    public GerenciadorSolicitarConfigFabricaVersaoProxPlus()
    {
    }

    public GerenciadorSolicitarConfigFabricaVersaoProxPlus(RepBase rep) => this._rep = rep;

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarSolicitacaoConfigVersaoProx()
    {
      this._estadoRep = GerenciadorSolicitarConfigFabricaVersaoProxPlus.Estados.estSolicitaConfigVersaoProx;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaConfigVersaoProxREPPlus()
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorSolicitarConfigFabricaVersaoProxPlus.Estados.estSolicitaConfigVersaoProx || envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 113)
        return;
      this.EncerrarConexao();
      string empty = string.Empty;
      byte[] numArray = new byte[16];
      Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 2, (Array) numArray, 0, numArray.Length);
      foreach (byte num in numArray)
      {
        if (num != (byte) 0)
          empty += (string) (object) (char) num;
      }
      this.NotificaConfigVersaoProx(empty);
      this.EncerrarConexao();
      this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
    }

    private void NotificaConfigVersaoProx(string versaoProx)
    {
      NotificarConfigVersaoProxEventArgs e = new NotificarConfigVersaoProxEventArgs(versaoProx);
      if (this.OnNotificarConfigVersaoProx == null)
        return;
      this.OnNotificarConfigVersaoProx((object) this, e);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarSolicitacaoConfigVersaoProx();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorSolicitarConfigFabricaVersaoProxPlus.Estados.estSolicitaConfigVersaoProx)
        menssagem = Resources.msgTIMEOUT_SOLICIT_CONFIG_VERSAO_PROX;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorSolicitarConfigFabricaVersaoProxPlus.Estados.estSolicitaConfigVersaoProx)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_CONFIG_VERSAO_PROX;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estSolicitaConfigVersaoProx,
    }
  }
}
