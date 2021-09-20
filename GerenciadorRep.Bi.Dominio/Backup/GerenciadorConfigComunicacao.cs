// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigComunicacao
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Net;
using TopData.Framework.Comunicacao;
using TopData.GerenciadorRep.Bi.Dominio.Properties;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigComunicacao : TarefaAbstrata
  {
    private RepBase _rep;
    private int _linhaProduto;
    private GerenciadorConfigComunicacao.Estados _estadoRep;
    public static GerenciadorConfigComunicacao _gerenciadorConfigComunicacao;

    public int LinhaProduto => this._linhaProduto;

    public static GerenciadorConfigComunicacao getInstance() => GerenciadorConfigComunicacao._gerenciadorConfigComunicacao != null ? GerenciadorConfigComunicacao._gerenciadorConfigComunicacao : new GerenciadorConfigComunicacao();

    public static GerenciadorConfigComunicacao getInstance(RepBase rep) => GerenciadorConfigComunicacao._gerenciadorConfigComunicacao != null ? GerenciadorConfigComunicacao._gerenciadorConfigComunicacao : new GerenciadorConfigComunicacao(rep);

    public static GerenciadorConfigComunicacao getInstance(
      string IpAddress,
      RepBase rep)
    {
      return GerenciadorConfigComunicacao._gerenciadorConfigComunicacao != null ? GerenciadorConfigComunicacao._gerenciadorConfigComunicacao : new GerenciadorConfigComunicacao(IpAddress, rep);
    }

    public GerenciadorConfigComunicacao()
    {
    }

    public GerenciadorConfigComunicacao(RepBase rep) => this._rep = rep;

    public GerenciadorConfigComunicacao(string IpAddress, RepBase rep)
    {
      this._rep = rep;
      IPAddress.Parse(IpAddress);
    }

    ~GerenciadorConfigComunicacao()
    {
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarTesteConfig()
    {
      try
      {
        this._estadoRep = GerenciadorConfigComunicacao.Estados.estEnvioTesteConfig;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaConfigInfo()
          {
            Info = (byte) 1
          }
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorConfigComunicacao.Estados.estEnvioTesteConfig || envelope.Grp != (byte) 2 || envelope.Cmd != (byte) 1)
        return;
      string empty = string.Empty;
      string menssagem;
      switch (envelope.MsgAplicacaoEmBytes[19])
      {
        case 1:
          this._linhaProduto = 2;
          menssagem = Resources.msgENVIO_TESTE_CONFIG_SUCESSO;
          break;
        case 2:
          this._linhaProduto = 3;
          menssagem = Resources.msgENVIO_TESTE_CONFIG_SUCESSO;
          break;
        case 4:
          this._linhaProduto = 1;
          menssagem = Resources.msgENVIO_TESTE_CONFIG_SUCESSO;
          break;
        default:
          this._linhaProduto = 0;
          this.EncerrarConexao();
          menssagem = Resources.msgEQUIPAMENTO_NAO_IDENTIFICADO;
          break;
      }
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
      this.EncerrarConexao();
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarTesteConfig();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigComunicacao.Estados.estEnvioTesteConfig)
        menssagem = Resources.msgTIMEOUT_ENVIO_TESTE_CONFIG;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigComunicacao.Estados.estEnvioTesteConfig)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_TESTE_CONFIG;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioTesteConfig,
    }
  }
}
