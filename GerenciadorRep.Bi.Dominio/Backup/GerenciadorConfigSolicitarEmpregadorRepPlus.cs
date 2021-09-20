// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigSolicitarEmpregadorRepPlus
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
  public class GerenciadorConfigSolicitarEmpregadorRepPlus : TarefaAbstrata
  {
    private RepBase _rep;
    private int _empregadorId;
    private GerenciadorConfigSolicitarEmpregadorRepPlus.Estados _estadoRep;
    private bool _chamadaPeloSdk;
    private Empregador _empregadorNoREP = new Empregador();
    public static GerenciadorConfigSolicitarEmpregadorRepPlus _gerenciadorConfigSolicitarEmpregadorRepPlus;

    public event EventHandler<NotificarRegistrosEmpregadorEventArgs> OnNotificarEmpregadorParaSdk;

    public event EventHandler<NotificarRecebimentoParaUsuarioEventArgs> OnNotificarRecebimentoUsuario;

    public static GerenciadorConfigSolicitarEmpregadorRepPlus getInstance() => GerenciadorConfigSolicitarEmpregadorRepPlus._gerenciadorConfigSolicitarEmpregadorRepPlus != null ? GerenciadorConfigSolicitarEmpregadorRepPlus._gerenciadorConfigSolicitarEmpregadorRepPlus : new GerenciadorConfigSolicitarEmpregadorRepPlus();

    public static GerenciadorConfigSolicitarEmpregadorRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigSolicitarEmpregadorRepPlus._gerenciadorConfigSolicitarEmpregadorRepPlus != null ? GerenciadorConfigSolicitarEmpregadorRepPlus._gerenciadorConfigSolicitarEmpregadorRepPlus : new GerenciadorConfigSolicitarEmpregadorRepPlus(rep);
    }

    public static GerenciadorConfigSolicitarEmpregadorRepPlus getInstance(
      RepBase rep,
      bool chamadaSDK)
    {
      return GerenciadorConfigSolicitarEmpregadorRepPlus._gerenciadorConfigSolicitarEmpregadorRepPlus != null ? GerenciadorConfigSolicitarEmpregadorRepPlus._gerenciadorConfigSolicitarEmpregadorRepPlus : new GerenciadorConfigSolicitarEmpregadorRepPlus(rep, chamadaSDK);
    }

    public static GerenciadorConfigSolicitarEmpregadorRepPlus getInstance(
      RepBase rep,
      Empregador empregadorSdk)
    {
      return GerenciadorConfigSolicitarEmpregadorRepPlus._gerenciadorConfigSolicitarEmpregadorRepPlus != null ? GerenciadorConfigSolicitarEmpregadorRepPlus._gerenciadorConfigSolicitarEmpregadorRepPlus : new GerenciadorConfigSolicitarEmpregadorRepPlus(rep, empregadorSdk);
    }

    public GerenciadorConfigSolicitarEmpregadorRepPlus()
    {
    }

    public GerenciadorConfigSolicitarEmpregadorRepPlus(RepBase rep)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
    }

    public GerenciadorConfigSolicitarEmpregadorRepPlus(RepBase rep, Empregador empregadorSdk)
    {
      this._rep = rep;
      this._chamadaPeloSdk = true;
    }

    public GerenciadorConfigSolicitarEmpregadorRepPlus(RepBase rep, bool chamadaSdk)
    {
      this._rep = rep;
      this._chamadaPeloSdk = chamadaSdk;
    }

    private void RecuperaEmpregadorDoREP(Envelope envelope) => this._empregadorNoREP = new Empregador(envelope.MsgAplicacaoEmBytes, true);

    private void NotificaMensagemParaSdk(Empregador entEmpregador)
    {
      NotificarRegistrosEmpregadorEventArgs e = new NotificarRegistrosEmpregadorEventArgs(entEmpregador);
      if (this.OnNotificarEmpregadorParaSdk == null)
        return;
      this.OnNotificarEmpregadorParaSdk((object) this, e);
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarSolicitacaoDadosEmpregador()
    {
      this._estadoRep = GerenciadorConfigSolicitarEmpregadorRepPlus.Estados.estSolicitaDadosEmpregador;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaDadosEmpregadorRepPlus()
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorConfigSolicitarEmpregadorRepPlus.Estados.estSolicitaDadosEmpregador || envelope.Grp != (byte) 11 || envelope.Cmd != (byte) 102)
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
      {
        this._empregadorNoREP.Cpf = this._empregadorNoREP.Cpf.Substring(3, 11);
        this._empregadorNoREP.Cpf = Convert.ToUInt64(this._empregadorNoREP.Cpf).ToString("000\\.000\\.000\\-00");
      }
      else
        this._empregadorNoREP.Cnpj = Convert.ToUInt64(this._empregadorNoREP.Cnpj).ToString("00\\.000\\.000\\/0000\\-00");
      if (empregador.ExisteCnpj(this._empregadorNoREP) && !this._empregadorNoREP.Cnpj.Equals("") || empregador.ExisteCpf(this._empregadorNoREP) && !this._empregadorNoREP.Cpf.Equals(""))
      {
        foreach (Empregador pesquisarEmpregadore in (Collection<Empregador>) empregador.PesquisarEmpregadores())
        {
          if ((pesquisarEmpregadore.Cnpj.Equals(this._empregadorNoREP.Cnpj) && !this._empregadorNoREP.Cnpj.Equals("") || pesquisarEmpregadore.Cpf.Equals(this._empregadorNoREP.Cpf) && !this._empregadorNoREP.Cpf.Equals("")) && pesquisarEmpregadore.Cei.Equals(this._empregadorNoREP.Cei))
          {
            this._empregadorId = pesquisarEmpregadore.EmpregadorId;
            break;
          }
        }
        if (this._empregadorId != 0)
        {
          this._empregadorNoREP.EmpregadorId = this._empregadorId;
          empregador.AtualizarEmpregador(this._empregadorNoREP);
        }
        else
        {
          empregador.InserirEmpregador(this._empregadorNoREP);
          foreach (Empregador pesquisarEmpregadore in (Collection<Empregador>) empregador.PesquisarEmpregadores())
          {
            if ((pesquisarEmpregadore.Cnpj.Equals(this._empregadorNoREP.Cnpj) && !this._empregadorNoREP.Cnpj.Equals("") || pesquisarEmpregadore.Cpf.Equals(this._empregadorNoREP.Cpf) && !this._empregadorNoREP.Cpf.Equals("")) && pesquisarEmpregadore.Cei.Equals(this._empregadorNoREP.Cei))
            {
              this._empregadorId = pesquisarEmpregadore.EmpregadorId;
              break;
            }
          }
        }
      }
      else
      {
        empregador.InserirEmpregador(this._empregadorNoREP);
        foreach (Empregador pesquisarEmpregadore in (Collection<Empregador>) empregador.PesquisarEmpregadores())
        {
          if ((pesquisarEmpregadore.Cnpj.Equals(this._empregadorNoREP.Cnpj) && !this._empregadorNoREP.Cnpj.Equals("") || pesquisarEmpregadore.Cpf.Equals(this._empregadorNoREP.Cpf) && !this._empregadorNoREP.Cpf.Equals("")) && pesquisarEmpregadore.Cei.Equals(this._empregadorNoREP.Cei))
          {
            this._empregadorId = pesquisarEmpregadore.EmpregadorId;
            break;
          }
        }
      }
      this.NotificarRecebimentoParaUsuario(Resources.msgPROCESSO_DE_SOLICITACAO_DE_EMPREGADOR_SUCESSO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso);
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
      if (this._estadoRep == GerenciadorConfigSolicitarEmpregadorRepPlus.Estados.estSolicitaDadosEmpregador)
        _msg = Resources.msgTIMEOUT_SOLICIT_DADOS_EMPREGADOR;
      this.EncerrarConexao();
      this.NotificarRecebimentoParaUsuario(_msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha);
    }

    public override void TratarNenhumaResposta()
    {
      string _msg = string.Empty;
      if (this._estadoRep == GerenciadorConfigSolicitarEmpregadorRepPlus.Estados.estSolicitaDadosEmpregador)
        _msg = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_DADOS_EMPREGADOR;
      this.EncerrarConexao();
      this.NotificarRecebimentoParaUsuario(_msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha);
    }

    private void NotificarRecebimentoParaUsuario(
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
