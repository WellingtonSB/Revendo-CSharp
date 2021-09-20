// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorSolicitarConfigFabricaModeloREPPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorSolicitarConfigFabricaModeloREPPlus : TarefaAbstrata
  {
    private RepBase _rep;
    private GerenciadorSolicitarConfigFabricaModeloREPPlus.Estados _estadoRep;
    public static GerenciadorSolicitarConfigFabricaModeloREPPlus _gerenciadorSolicitarConfigFabricaModeloREPPlus;

    public event EventHandler<NotificarConfigModeloRepEventArgs> OnNotificarConfigModeloRep;

    public static GerenciadorSolicitarConfigFabricaModeloREPPlus getInstance() => GerenciadorSolicitarConfigFabricaModeloREPPlus._gerenciadorSolicitarConfigFabricaModeloREPPlus != null ? GerenciadorSolicitarConfigFabricaModeloREPPlus._gerenciadorSolicitarConfigFabricaModeloREPPlus : new GerenciadorSolicitarConfigFabricaModeloREPPlus();

    public static GerenciadorSolicitarConfigFabricaModeloREPPlus getInstance(
      RepBase rep)
    {
      return GerenciadorSolicitarConfigFabricaModeloREPPlus._gerenciadorSolicitarConfigFabricaModeloREPPlus != null ? GerenciadorSolicitarConfigFabricaModeloREPPlus._gerenciadorSolicitarConfigFabricaModeloREPPlus : new GerenciadorSolicitarConfigFabricaModeloREPPlus(rep);
    }

    public GerenciadorSolicitarConfigFabricaModeloREPPlus()
    {
    }

    public GerenciadorSolicitarConfigFabricaModeloREPPlus(RepBase rep) => this._rep = rep;

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarSolicitacaoConfigModeloRep()
    {
      this._estadoRep = GerenciadorSolicitarConfigFabricaModeloREPPlus.Estados.estSolicitaConfigModeloRep;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaConfigModeloRepREPPlus()
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorSolicitarConfigFabricaModeloREPPlus.Estados.estSolicitaConfigModeloRep || envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 111)
        return;
      this.EncerrarConexao();
      string empty = string.Empty;
      byte[] numArray = new byte[16];
      Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 2, (Array) numArray, 0, numArray.Length);
      foreach (byte num in numArray)
      {
        switch (num)
        {
          case 0:
          case 48:
            continue;
          default:
            empty += (string) (object) (char) num;
            continue;
        }
      }
      this.NotificaConfigModeloRep(empty);
      this.EncerrarConexao();
      this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
    }

    private void NotificaConfigModeloRep(string modeloRep)
    {
      NotificarConfigModeloRepEventArgs e = new NotificarConfigModeloRepEventArgs(modeloRep);
      if (this.OnNotificarConfigModeloRep == null)
        return;
      this.OnNotificarConfigModeloRep((object) this, e);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarSolicitacaoConfigModeloRep();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorSolicitarConfigFabricaModeloREPPlus.Estados.estSolicitaConfigModeloRep)
        menssagem = Resources.msgTIMEOUT_SOLICIT_CONFIG_MODELO_REP;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorSolicitarConfigFabricaModeloREPPlus.Estados.estSolicitaConfigModeloRep)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_CONFIG_MODELO_REP;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estSolicitaConfigModeloRep,
    }
  }
}
