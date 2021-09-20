// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigFabricaREPPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigFabricaREPPlus : TarefaAbstrata
  {
    private TipoMensagemConfigDeFabrica _tipoMensagem;
    private Relogio _entRelogio;
    private FormatoCartao _formatoEnt;
    private RepBase _rep;
    private GerenciadorConfigFabricaREPPlus.Estados _estadoRep;
    private ConfigFabricaREP _configFabrica;
    public static GerenciadorConfigFabricaREPPlus _gerenciadorConfigFabricaREPPlus;

    public ConfigFabricaREP ConfigFabrica
    {
      get => this._configFabrica;
      set => this._configFabrica = value;
    }

    public event EventHandler<NotificarLiberacaoSerialMACEventArgs> OnNotificarEnvioConfigSerialMac;

    public event EventHandler<EventArgs> OnNotificarEnvioConfigImpressoras;

    public event EventHandler<EventArgs> OnNotificarEnvioApagaMem;

    public event EventHandler<NotificarSolicitacaoInfoRep> OnNotificarEnvioSolicInfo;

    public event EventHandler<NotificarMemoriasApagadas> OnNotificarMemoriasApagadas;

    public event EventHandler<NotificarSolicitacaoInfoMRPRep> OnNotificarEnvioSolicInfoMRP;

    public static GerenciadorConfigFabricaREPPlus getInstance() => GerenciadorConfigFabricaREPPlus._gerenciadorConfigFabricaREPPlus != null ? GerenciadorConfigFabricaREPPlus._gerenciadorConfigFabricaREPPlus : new GerenciadorConfigFabricaREPPlus();

    public static GerenciadorConfigFabricaREPPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigFabricaREPPlus._gerenciadorConfigFabricaREPPlus != null ? GerenciadorConfigFabricaREPPlus._gerenciadorConfigFabricaREPPlus : new GerenciadorConfigFabricaREPPlus(rep);
    }

    public static GerenciadorConfigFabricaREPPlus getInstance(
      RepBase rep,
      ConfigFabricaREP configFabrica)
    {
      return GerenciadorConfigFabricaREPPlus._gerenciadorConfigFabricaREPPlus != null ? GerenciadorConfigFabricaREPPlus._gerenciadorConfigFabricaREPPlus : new GerenciadorConfigFabricaREPPlus(rep, configFabrica);
    }

    public GerenciadorConfigFabricaREPPlus()
    {
    }

    public GerenciadorConfigFabricaREPPlus(RepBase rep) => this._rep = rep;

    public GerenciadorConfigFabricaREPPlus(RepBase rep, ConfigFabricaREP configFabrica)
    {
      this._configFabrica = configFabrica;
      this._rep = rep;
    }

    public void IniciarProcesso(TipoMensagemConfigDeFabrica tipoMensagem)
    {
      this._tipoMensagem = tipoMensagem;
      this.Conectar(this._rep);
    }

    private void EnviarConfigSerialMAC()
    {
      this._estadoRep = GerenciadorConfigFabricaREPPlus.Estados.estEnvioConfigSerial_MAC;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoMacSerialRepPlus(this._configFabrica.MACAddress, this._configFabrica.Serial.PadLeft(18, '0'), this._configFabrica.TipoEquipamento, this._configFabrica.FWInova4)
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      switch (this._estadoRep)
      {
        case GerenciadorConfigFabricaREPPlus.Estados.estEnvioConfigSerial_MAC:
          if (envelope.Grp != (byte) 14 || envelope.Cmd != (byte) 100)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
          {
            NotificarLiberacaoSerialMACEventArgs e = new NotificarLiberacaoSerialMACEventArgs(true);
            if (this.OnNotificarEnvioConfigSerialMac == null)
              break;
            this.EncerrarConexao();
            this.OnNotificarEnvioConfigSerialMac((object) this, e);
            break;
          }
          NotificarLiberacaoSerialMACEventArgs e1 = new NotificarLiberacaoSerialMACEventArgs(false);
          if (this.OnNotificarEnvioConfigSerialMac == null)
            break;
          this.EncerrarConexao();
          this.OnNotificarEnvioConfigSerialMac((object) this, e1);
          break;
        case GerenciadorConfigFabricaREPPlus.Estados.estEnvioConfigImpressora:
          if (envelope.Grp != (byte) 14 || envelope.Cmd != (byte) 110 || this.OnNotificarEnvioConfigImpressoras == null)
            break;
          this.OnNotificarEnvioConfigImpressoras((object) this, (EventArgs) null);
          this.EncerrarConexao();
          break;
        case GerenciadorConfigFabricaREPPlus.Estados.estEnvioSolicInfo:
          if (envelope.Grp != (byte) 2 || envelope.Cmd != (byte) 1)
            break;
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          byte[] aplicacaoEmBytes1 = envelope.MsgAplicacaoEmBytes;
          for (int index = 2; index < 19; ++index)
            empty1 += aplicacaoEmBytes1[index].ToString();
          string VersaoFWRep = aplicacaoEmBytes1[24].ToString() + "." + aplicacaoEmBytes1[25].ToString("X").PadLeft(2, '0');
          bool bloqueado = aplicacaoEmBytes1[38] == (byte) 1;
          if (this.OnNotificarEnvioSolicInfo == null)
            break;
          this.EncerrarConexao();
          this.OnNotificarEnvioSolicInfo((object) this, new NotificarSolicitacaoInfoRep(empty1, VersaoFWRep, bloqueado));
          break;
        case GerenciadorConfigFabricaREPPlus.Estados.estEnvioSolicInfoMRP:
          if (envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 112)
            break;
          string empty3 = string.Empty;
          string empty4 = string.Empty;
          string empty5 = string.Empty;
          byte[] numArray1 = new byte[32];
          byte[] numArray2 = new byte[20];
          byte[] aplicacaoEmBytes2 = envelope.MsgAplicacaoEmBytes;
          Array.Copy((Array) aplicacaoEmBytes2, 13, (Array) numArray1, 0, 32);
          Array.Copy((Array) aplicacaoEmBytes2, 47, (Array) numArray2, 0, 20);
          string VersaoFWMRPRep = aplicacaoEmBytes2[46].ToString();
          foreach (byte num in numArray1)
            empty4 += num.ToString("X").PadLeft(2, '0');
          foreach (byte num in numArray2)
            empty5 += num.ToString("X").PadLeft(2, '0');
          if (this.OnNotificarEnvioSolicInfoMRP == null)
            break;
          this.EncerrarConexao();
          this.OnNotificarEnvioSolicInfoMRP((object) this, new NotificarSolicitacaoInfoMRPRep(VersaoFWMRPRep, empty4, empty5));
          break;
      }
    }

    private void SolicitarInfo()
    {
      this._estadoRep = GerenciadorConfigFabricaREPPlus.Estados.estEnvioSolicInfo;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaConfigInfoRepPlus()
        {
          Info = (byte) 1
        }
      }, true);
    }

    private void SolicitarInfoMRP()
    {
      this._estadoRep = GerenciadorConfigFabricaREPPlus.Estados.estEnvioSolicInfoMRP;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaConfigInfoMRPRepPlus()
      }, true);
    }

    private void EnviarConfigImpressoras()
    {
      this._estadoRep = GerenciadorConfigFabricaREPPlus.Estados.estEnvioConfigImpressora;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoConfigImpressoras()
      }, true);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        switch (this._tipoMensagem)
        {
          case TipoMensagemConfigDeFabrica.SERIAL_MAC:
            this.EnviarConfigSerialMAC();
            break;
          case TipoMensagemConfigDeFabrica.SOLICITA_INFO:
            this.SolicitarInfo();
            break;
          case TipoMensagemConfigDeFabrica.SOLICITA_INFO_MRP:
            this.SolicitarInfoMRP();
            break;
          default:
            this.SolicitarInfo();
            break;
        }
      }
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

    public override void IniciarProcesso()
    {
    }

    private new enum Estados
    {
      estEnvioConfigSerial_MAC,
      estEnvioConfigImpressora,
      estEnvioSolicInfo,
      estEnvioSolicInfoMRP,
      estFinal,
    }
  }
}
