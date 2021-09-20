// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorTestaConexaoREPRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.GerenciadorRep.Bi.Dominio.Properties;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorTestaConexaoREPRepPlus : TarefaAbstrata
  {
    private RepBase _rep;
    public static GerenciadorTestaConexaoREPRepPlus _gerenciadorTestaConexaoREPRepPlus;

    public event EventHandler<NotificarSolicitacaoInfoRep> OnNotificarInfoRep;

    public event EventHandler<NotificarParaUsuarioEventArgs> OnNotificarTesteConexao;

    public event EventHandler<NotificarParaUsuarioEventArgs> OnNotificarTesteConexaoStatusRep;

    public static GerenciadorTestaConexaoREPRepPlus getInstance() => GerenciadorTestaConexaoREPRepPlus._gerenciadorTestaConexaoREPRepPlus != null ? GerenciadorTestaConexaoREPRepPlus._gerenciadorTestaConexaoREPRepPlus : new GerenciadorTestaConexaoREPRepPlus();

    public static GerenciadorTestaConexaoREPRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorTestaConexaoREPRepPlus._gerenciadorTestaConexaoREPRepPlus != null ? GerenciadorTestaConexaoREPRepPlus._gerenciadorTestaConexaoREPRepPlus : new GerenciadorTestaConexaoREPRepPlus(rep);
    }

    public GerenciadorTestaConexaoREPRepPlus()
    {
    }

    public GerenciadorTestaConexaoREPRepPlus(RepBase rep) => this._rep = rep;

    public override void IniciarProcesso() => this.Conectar(this._rep);

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        if (this.OnNotificarTesteConexaoStatusRep != null)
        {
          if (e.StatusRep == (byte) 0)
            this.OnNotificarTesteConexaoStatusRep((object) this.OnNotificarTesteConexao, new NotificarParaUsuarioEventArgs(Resources.msgREP_LIBERADO, EnumEstadoNotificacaoParaUsuario.comSucesso, 0, ""));
          if (e.StatusRep == (byte) 1)
            this.OnNotificarTesteConexaoStatusRep((object) this.OnNotificarTesteConexao, new NotificarParaUsuarioEventArgs(Resources.msgREP_BLOQUEADO, EnumEstadoNotificacaoParaUsuario.comSucesso, 0, ""));
          if (e.StatusRep == (byte) 2)
            this.OnNotificarTesteConexaoStatusRep((object) this.OnNotificarTesteConexao, new NotificarParaUsuarioEventArgs(Resources.msgREP_MEMORIA_CORROMPIDA, EnumEstadoNotificacaoParaUsuario.comSucesso, 0, ""));
        }
        if (this.OnNotificarTesteConexao != null)
          this.OnNotificarTesteConexao((object) this.OnNotificarTesteConexao, new NotificarParaUsuarioEventArgs(this._rep.Serial, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, 0, ""));
        NotificarSolicitacaoInfoRep e1 = new NotificarSolicitacaoInfoRep(this._rep.Serial, this._rep.VersaoFWCompleto, (int) this._rep.Status, this._rep.ModeloFim);
        if (this.OnNotificarInfoRep != null)
          this.OnNotificarInfoRep((object) this, e1);
      }
      this.EncerrarConexao();
    }

    public override void TratarEnvelope(Envelope envelope) => throw new NotImplementedException();

    public override void TratarTimeoutAck() => this.EncerrarConexao();

    public override void TratarNenhumaResposta() => this.EncerrarConexao();
  }
}
