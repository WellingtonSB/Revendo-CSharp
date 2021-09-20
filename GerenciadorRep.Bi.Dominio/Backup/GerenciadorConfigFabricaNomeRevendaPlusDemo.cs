// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigFabricaNomeRevendaPlusDemo
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigFabricaNomeRevendaPlusDemo : TarefaAbstrata
  {
    private Relogio _entRelogio;
    private FormatoCartao _formatoEnt;
    private RepBase _rep;
    private GerenciadorConfigFabricaNomeRevendaPlusDemo.Estados _estadoRep;
    private ConfigFabricaREP _configFabrica;
    public static GerenciadorConfigFabricaNomeRevendaPlusDemo _gerenciadorConfigFabricaNomeRevendaPlusDemo;

    public ConfigFabricaREP ConfigFabrica
    {
      get => this._configFabrica;
      set => this._configFabrica = value;
    }

    public event EventHandler<NotificarLiberacaoNomeRevendaEventArgs> OnNotificarEnvioNomeRevenda;

    public static GerenciadorConfigFabricaNomeRevendaPlusDemo getInstance() => GerenciadorConfigFabricaNomeRevendaPlusDemo._gerenciadorConfigFabricaNomeRevendaPlusDemo != null ? GerenciadorConfigFabricaNomeRevendaPlusDemo._gerenciadorConfigFabricaNomeRevendaPlusDemo : new GerenciadorConfigFabricaNomeRevendaPlusDemo();

    public static GerenciadorConfigFabricaNomeRevendaPlusDemo getInstance(
      RepBase rep)
    {
      return GerenciadorConfigFabricaNomeRevendaPlusDemo._gerenciadorConfigFabricaNomeRevendaPlusDemo != null ? GerenciadorConfigFabricaNomeRevendaPlusDemo._gerenciadorConfigFabricaNomeRevendaPlusDemo : new GerenciadorConfigFabricaNomeRevendaPlusDemo(rep);
    }

    public static GerenciadorConfigFabricaNomeRevendaPlusDemo getInstance(
      RepBase rep,
      ConfigFabricaREP configFabrica)
    {
      return GerenciadorConfigFabricaNomeRevendaPlusDemo._gerenciadorConfigFabricaNomeRevendaPlusDemo != null ? GerenciadorConfigFabricaNomeRevendaPlusDemo._gerenciadorConfigFabricaNomeRevendaPlusDemo : new GerenciadorConfigFabricaNomeRevendaPlusDemo(rep, configFabrica);
    }

    public GerenciadorConfigFabricaNomeRevendaPlusDemo()
    {
    }

    public GerenciadorConfigFabricaNomeRevendaPlusDemo(RepBase rep) => this._rep = rep;

    public GerenciadorConfigFabricaNomeRevendaPlusDemo(RepBase rep, ConfigFabricaREP configFabrica)
    {
      this._configFabrica = configFabrica;
      this._rep = rep;
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarConfigNomeRevenda()
    {
      this._estadoRep = GerenciadorConfigFabricaNomeRevendaPlusDemo.Estados.estEnvioNomeRevenda;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoNomeRevenda(this._configFabrica.NomeRevenda)
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorConfigFabricaNomeRevendaPlusDemo.Estados.estEnvioNomeRevenda || envelope.Grp != (byte) 99 || envelope.Cmd != (byte) 101)
        return;
      if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
      {
        NotificarLiberacaoNomeRevendaEventArgs e = new NotificarLiberacaoNomeRevendaEventArgs(true);
        if (this.OnNotificarEnvioNomeRevenda == null)
          return;
        this.EncerrarConexao();
        this.OnNotificarEnvioNomeRevenda((object) this, e);
      }
      else
      {
        NotificarLiberacaoNomeRevendaEventArgs e = new NotificarLiberacaoNomeRevendaEventArgs(false);
        if (this.OnNotificarEnvioNomeRevenda == null)
          return;
        this.EncerrarConexao();
        this.OnNotificarEnvioNomeRevenda((object) this, e);
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarConfigNomeRevenda();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      this.EncerrarConexao();
      this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      this.EncerrarConexao();
      this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioConfigSerial_MAC,
      estEnvioConfigImpressora,
      estEnvioSolicInfo,
      estEnvioNomeRevenda,
      estFinal,
    }
  }
}
