// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorEnvioDesafioRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorEnvioDesafioRepPlus : TarefaAbstrata
  {
    private byte[] _numeroRandomico;
    private RepBase _rep;
    public static GerenciadorEnvioDesafioRepPlus _gerenciadorEnvioDesafioRepPlus;
    private GerenciadorEnvioDesafioRepPlus.Estados _estadoRep;

    public byte[] NumeroRandomico
    {
      get => this._numeroRandomico;
      set => this._numeroRandomico = value;
    }

    public event EventHandler<NotificarSolicitarDesafio> OnNotificarSolicitarDesafio;

    public static GerenciadorEnvioDesafioRepPlus getInstance() => GerenciadorEnvioDesafioRepPlus._gerenciadorEnvioDesafioRepPlus != null ? GerenciadorEnvioDesafioRepPlus._gerenciadorEnvioDesafioRepPlus : new GerenciadorEnvioDesafioRepPlus();

    public static GerenciadorEnvioDesafioRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorEnvioDesafioRepPlus._gerenciadorEnvioDesafioRepPlus != null ? GerenciadorEnvioDesafioRepPlus._gerenciadorEnvioDesafioRepPlus : new GerenciadorEnvioDesafioRepPlus(rep);
    }

    public static GerenciadorEnvioDesafioRepPlus getInstance(
      RepBase rep,
      byte[] numeroRandomico)
    {
      return GerenciadorEnvioDesafioRepPlus._gerenciadorEnvioDesafioRepPlus != null ? GerenciadorEnvioDesafioRepPlus._gerenciadorEnvioDesafioRepPlus : new GerenciadorEnvioDesafioRepPlus(rep, numeroRandomico);
    }

    public GerenciadorEnvioDesafioRepPlus()
    {
    }

    public GerenciadorEnvioDesafioRepPlus(RepBase rep, byte[] numeroRandomico)
    {
      this._rep = rep;
      this._numeroRandomico = numeroRandomico;
    }

    public GerenciadorEnvioDesafioRepPlus(RepBase rep) => this._rep = rep;

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarDesafioResposta()
    {
      try
      {
        this._estadoRep = GerenciadorEnvioDesafioRepPlus.Estados.estEnvioDesafio;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, ushort.MaxValue, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoDesafio()
          {
            NumeroRandomico = this.NumeroRandomico
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
      if (this._estadoRep != GerenciadorEnvioDesafioRepPlus.Estados.estEnvioDesafio || envelope.Grp != (byte) 17 || envelope.Cmd != (byte) 101)
        return;
      byte[] numArray = new byte[20];
      Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 2, (Array) numArray, 0, 20);
      string empty = string.Empty;
      foreach (byte num in numArray)
        empty += num.ToString("X").PadLeft(2, '0');
      this.EncerrarConexao();
      NotificarSolicitarDesafio e = new NotificarSolicitarDesafio(empty);
      if (this.OnNotificarSolicitarDesafio == null)
        return;
      this.OnNotificarSolicitarDesafio((object) this, e);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarDesafioResposta();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorEnvioDesafioRepPlus.Estados.estEnvioDesafio)
        menssagem = "Falha de comunicação,\ntime out envio do desafio";
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorEnvioDesafioRepPlus.Estados.estEnvioDesafio)
        menssagem = "Falha de comunicação,\nnenhuma resposta chegou após envio do desafio";
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioDesafio,
    }
  }
}
