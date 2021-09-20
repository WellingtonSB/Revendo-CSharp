// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorSolicitarConfigFabricaMacREPPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorSolicitarConfigFabricaMacREPPlus : TarefaAbstrata
  {
    private RepBase _rep;
    private GerenciadorSolicitarConfigFabricaMacREPPlus.Estados _estadoRep;
    public static GerenciadorSolicitarConfigFabricaMacREPPlus _gerenciadorSolicitarConfigFabricaMacREPPlus;

    public event EventHandler<NotificarConfigMacEventArgs> OnNotificarConfigMac;

    public static GerenciadorSolicitarConfigFabricaMacREPPlus getInstance() => GerenciadorSolicitarConfigFabricaMacREPPlus._gerenciadorSolicitarConfigFabricaMacREPPlus != null ? GerenciadorSolicitarConfigFabricaMacREPPlus._gerenciadorSolicitarConfigFabricaMacREPPlus : new GerenciadorSolicitarConfigFabricaMacREPPlus();

    public static GerenciadorSolicitarConfigFabricaMacREPPlus getInstance(
      RepBase rep)
    {
      return GerenciadorSolicitarConfigFabricaMacREPPlus._gerenciadorSolicitarConfigFabricaMacREPPlus != null ? GerenciadorSolicitarConfigFabricaMacREPPlus._gerenciadorSolicitarConfigFabricaMacREPPlus : new GerenciadorSolicitarConfigFabricaMacREPPlus(rep);
    }

    public GerenciadorSolicitarConfigFabricaMacREPPlus()
    {
    }

    public GerenciadorSolicitarConfigFabricaMacREPPlus(RepBase rep) => this._rep = rep;

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarSolicitacaoConfigMAC()
    {
      this._estadoRep = GerenciadorSolicitarConfigFabricaMacREPPlus.Estados.estSolicitaConfigMac;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaConfigMACREPPlus()
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorSolicitarConfigFabricaMacREPPlus.Estados.estSolicitaConfigMac || envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 114)
        return;
      this.EncerrarConexao();
      string empty = string.Empty;
      byte[] numArray = new byte[6];
      Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 2, (Array) numArray, 0, 6);
      foreach (byte num in numArray)
        empty += num.ToString("X").PadLeft(2, '0');
      this.NotificaConfigMAC(empty);
      this.EncerrarConexao();
      this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
    }

    private void NotificaConfigMAC(string mac)
    {
      NotificarConfigMacEventArgs e = new NotificarConfigMacEventArgs(mac);
      if (this.OnNotificarConfigMac == null)
        return;
      this.OnNotificarConfigMac((object) this, e);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarSolicitacaoConfigMAC();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorSolicitarConfigFabricaMacREPPlus.Estados.estSolicitaConfigMac)
        menssagem = Resources.msgTIMEOUT_SOLICIT_CONFIG_MAC;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorSolicitarConfigFabricaMacREPPlus.Estados.estSolicitaConfigMac)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_CONFIG_MAC;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estSolicitaConfigMac,
    }
  }
}
