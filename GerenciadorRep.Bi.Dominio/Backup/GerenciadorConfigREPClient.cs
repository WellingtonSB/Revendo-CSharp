// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigREPClient
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigREPClient : TarefaAbstrata
  {
    private RepBase _rep;
    private bool _repClient;
    private bool _chamadaPeloSdk;
    private GerenciadorConfigREPClient.Estados _estadoRep;
    public static GerenciadorConfigREPClient _gerenciadorConfigREPClient;

    public new bool RepClient
    {
      get => this._repClient;
      set => this._repClient = value;
    }

    public static GerenciadorConfigREPClient getInstance() => GerenciadorConfigREPClient._gerenciadorConfigREPClient != null ? GerenciadorConfigREPClient._gerenciadorConfigREPClient : new GerenciadorConfigREPClient();

    public static GerenciadorConfigREPClient getInstance(RepBase rep) => GerenciadorConfigREPClient._gerenciadorConfigREPClient != null ? GerenciadorConfigREPClient._gerenciadorConfigREPClient : new GerenciadorConfigREPClient(rep);

    public GerenciadorConfigREPClient()
    {
    }

    public GerenciadorConfigREPClient(RepBase rep)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
    }

    public override void IniciarProcesso()
    {
      this._rep.EnvioConfigAvancadas = true;
      this.Conectar(this._rep);
    }

    private void EnviarConfigRepClient()
    {
      this._estadoRep = GerenciadorConfigREPClient.Estados.estEnvioConfigRepClient;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoConfigRepClient(this._rep.repClient, this._rep.PortaVariavel, this._rep.ipServidor, (short) this._rep.portaServidor, this._rep.mascaraRede, this._rep.Gateway, (byte) this._rep.intervaloConexao)
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorConfigREPClient.Estados.estEnvioConfigRepClient)
        return;
      if (envelope.Grp == (byte) 3 && envelope.Cmd == (byte) 0)
      {
        if (envelope.MsgAplicacaoEmBytes.Length < 3 || envelope.MsgAplicacaoEmBytes[2] != (byte) 4)
        {
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          this.EncerrarConexao();
        }
        else if (!this._chamadaPeloSdk)
        {
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
          this.EncerrarConexao();
        }
        else
        {
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
          this.EncerrarConexao();
        }
      }
      else
      {
        this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        this.EncerrarConexao();
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarConfigRepClient();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigREPClient.Estados.estEnvioConfigRepClient)
        menssagem = Resources.msgTIMEOUT_ENVIO_CONFIG_GERAL;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigREPClient.Estados.estEnvioConfigRepClient)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioConfigRepClient,
    }
  }
}
