// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorTestaConexaoREP
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorTestaConexaoREP : TarefaAbstrata
  {
    private RepBase _rep;
    public static GerenciadorTestaConexaoREP _gerenciadorTestaConexaoREP;

    public event EventHandler<NotificarParaUsuarioEventArgs> OnNotificarTesteConexao;

    public static GerenciadorTestaConexaoREP getInstance() => GerenciadorTestaConexaoREP._gerenciadorTestaConexaoREP != null ? GerenciadorTestaConexaoREP._gerenciadorTestaConexaoREP : new GerenciadorTestaConexaoREP();

    public static GerenciadorTestaConexaoREP getInstance(RepBase rep) => GerenciadorTestaConexaoREP._gerenciadorTestaConexaoREP != null ? GerenciadorTestaConexaoREP._gerenciadorTestaConexaoREP : new GerenciadorTestaConexaoREP(rep);

    public GerenciadorTestaConexaoREP()
    {
    }

    public GerenciadorTestaConexaoREP(RepBase rep) => this._rep = rep;

    public override void IniciarProcesso() => this.Conectar(this._rep);

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso && this.OnNotificarTesteConexao != null)
        this.OnNotificarTesteConexao((object) this.OnNotificarTesteConexao, new NotificarParaUsuarioEventArgs(this._rep.Serial, EnumEstadoNotificacaoParaUsuario.comSucesso, 0, ""));
      this.EncerrarConexao();
    }

    public override void TratarEnvelope(Envelope envelope)
    {
    }

    public override void TratarTimeoutAck() => this.EncerrarConexao();

    public override void TratarNenhumaResposta() => this.EncerrarConexao();
  }
}
