// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigSolicitarEmpregador
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.ObjectModel;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigSolicitarEmpregador : TarefaAbstrata
  {
    private int _empregadorId;
    private RepBase _rep;
    private GerenciadorConfigSolicitarEmpregador.Estados _estadoRep;
    private Empregador _empregadorNoCadastro = new Empregador();
    private bool _chamadaPeloSdk;
    private Empregador _empregadorNoREP = new Empregador();
    private string _localPrestacaoServicoNoREP;
    public static GerenciadorConfigSolicitarEmpregador _gerenciadorConfigSolicitarEmpregador;

    public string localPrestacaoServicoNoREP
    {
      get => this._localPrestacaoServicoNoREP;
      set => this._localPrestacaoServicoNoREP = value;
    }

    public event EventHandler<NotificarRegistrosEmpregadorEventArgs> OnNotificarEmpregadorParaSdk;

    public event EventHandler<NotificarRecebimentoParaUsuarioEventArgs> OnNotificarRecebimentoUsuario;

    public static GerenciadorConfigSolicitarEmpregador getInstance() => GerenciadorConfigSolicitarEmpregador._gerenciadorConfigSolicitarEmpregador != null ? GerenciadorConfigSolicitarEmpregador._gerenciadorConfigSolicitarEmpregador : new GerenciadorConfigSolicitarEmpregador();

    public static GerenciadorConfigSolicitarEmpregador getInstance(
      RepBase rep)
    {
      return GerenciadorConfigSolicitarEmpregador._gerenciadorConfigSolicitarEmpregador != null ? GerenciadorConfigSolicitarEmpregador._gerenciadorConfigSolicitarEmpregador : new GerenciadorConfigSolicitarEmpregador(rep);
    }

    public static GerenciadorConfigSolicitarEmpregador getInstance(
      RepBase rep,
      bool chamadaSDK)
    {
      return GerenciadorConfigSolicitarEmpregador._gerenciadorConfigSolicitarEmpregador != null ? GerenciadorConfigSolicitarEmpregador._gerenciadorConfigSolicitarEmpregador : new GerenciadorConfigSolicitarEmpregador(rep, chamadaSDK);
    }

    public static GerenciadorConfigSolicitarEmpregador getInstance(
      RepBase rep,
      Empregador empregadorSdk)
    {
      return GerenciadorConfigSolicitarEmpregador._gerenciadorConfigSolicitarEmpregador != null ? GerenciadorConfigSolicitarEmpregador._gerenciadorConfigSolicitarEmpregador : new GerenciadorConfigSolicitarEmpregador(rep, empregadorSdk);
    }

    public GerenciadorConfigSolicitarEmpregador()
    {
    }

    public GerenciadorConfigSolicitarEmpregador(RepBase rep)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
    }

    public GerenciadorConfigSolicitarEmpregador(RepBase rep, Empregador empregadorSdk)
    {
      this._rep = rep;
      this._empregadorNoCadastro.RazaoSocial = empregadorSdk.RazaoSocial;
      this._empregadorNoCadastro.Cnpj = empregadorSdk.Cnpj;
      this._empregadorNoCadastro.Cpf = empregadorSdk.Cpf;
      this._empregadorNoCadastro.Cei = empregadorSdk.Cei;
      this._empregadorNoCadastro.isCnpj = empregadorSdk.isCnpj;
      this._chamadaPeloSdk = true;
    }

    public GerenciadorConfigSolicitarEmpregador(RepBase rep, bool chamadaSdk)
    {
      this._rep = rep;
      this._chamadaPeloSdk = chamadaSdk;
    }

    private void RecuperaEmpregadorDoREP(Envelope envelope) => this._empregadorNoREP = new Empregador(envelope.MsgAplicacaoEmBytes, false);

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarSolicitacaoDadosEmpregador()
    {
      this._estadoRep = GerenciadorConfigSolicitarEmpregador.Estados.estSolicitaDadosEmpregador;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaDadosEmpregador()
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorConfigSolicitarEmpregador.Estados.estSolicitaDadosEmpregador || envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 106)
        return;
      this.RecuperaEmpregadorDoREP(envelope);
      this.EncerrarConexao();
      if (this._chamadaPeloSdk)
        this.NotificaMensagemParaSdk(this._empregadorNoREP);
      else
        this.GravarEmpregadorNoBD();
    }

    private void GravarEmpregadorNoBD()
    {
      Empregador empregador = new Empregador();
      if (!this._empregadorNoREP.isCnpj)
        this._empregadorNoREP.Cpf = this._empregadorNoREP.Cpf.Substring(3, 11);
      if (empregador.ExisteCnpj(this._empregadorNoREP) || empregador.ExisteCpf(this._empregadorNoREP))
      {
        foreach (Empregador pesquisarEmpregadore in (Collection<Empregador>) empregador.PesquisarEmpregadores())
        {
          if (pesquisarEmpregadore.Cnpj.Equals(this._empregadorNoREP.Cnpj) || pesquisarEmpregadore.Cpf.Equals(this._empregadorNoREP.Cpf))
          {
            this._empregadorId = pesquisarEmpregadore.EmpregadorId;
            break;
          }
        }
        this._empregadorNoREP.EmpregadorId = this._empregadorId;
        empregador.AtualizarEmpregador(this._empregadorNoREP);
      }
      else
      {
        empregador.InserirEmpregador(this._empregadorNoREP);
        foreach (Empregador pesquisarEmpregadore in (Collection<Empregador>) empregador.PesquisarEmpregadores())
        {
          if (pesquisarEmpregadore.Cnpj.Equals(this._empregadorNoREP.Cnpj) || pesquisarEmpregadore.Cpf.Equals(this._empregadorNoREP.Cpf))
          {
            this._empregadorId = pesquisarEmpregadore.EmpregadorId;
            break;
          }
        }
      }
      this.NotificaMensagemParaUsuario(Resources.msgPROCESSO_DE_SOLICITACAO_DE_EMPREGADOR_SUCESSO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso);
    }

    private void NotificaMensagemParaSdk(Empregador entEmpregador)
    {
      NotificarRegistrosEmpregadorEventArgs e = new NotificarRegistrosEmpregadorEventArgs(entEmpregador);
      if (this.OnNotificarEmpregadorParaSdk == null)
        return;
      this.OnNotificarEmpregadorParaSdk((object) this, e);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarSolicitacaoDadosEmpregador();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string _msg = string.Empty;
      if (this._estadoRep == GerenciadorConfigSolicitarEmpregador.Estados.estSolicitaDadosEmpregador)
        _msg = Resources.msgTIMEOUT_SOLICIT_DADOS_EMPREGADOR;
      this.EncerrarConexao();
      this.NotificaMensagemParaUsuario(_msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha);
    }

    public override void TratarNenhumaResposta()
    {
      string _msg = string.Empty;
      if (this._estadoRep == GerenciadorConfigSolicitarEmpregador.Estados.estSolicitaDadosEmpregador)
        _msg = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_DADOS_EMPREGADOR;
      this.EncerrarConexao();
      this.NotificaMensagemParaUsuario(_msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha);
    }

    private void NotificaMensagemParaUsuario(
      string _msg,
      EnumEstadoNotificacaoParaUsuario enumEstadoNotificacaoParaUsuario,
      EnumEstadoResultadoFinalProcesso enumEstadoResultadoFinalProcesso)
    {
      NotificarRecebimentoParaUsuarioEventArgs e = new NotificarRecebimentoParaUsuarioEventArgs(_msg, enumEstadoNotificacaoParaUsuario, enumEstadoResultadoFinalProcesso, this._empregadorId, this._empregadorNoREP.Local);
      if (this.OnNotificarRecebimentoUsuario == null)
        return;
      this.OnNotificarRecebimentoUsuario((object) this, e);
    }

    private new enum Estados
    {
      estSolicitaDadosEmpregador,
    }
  }
}
