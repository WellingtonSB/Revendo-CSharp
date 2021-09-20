// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigEmpregadosInclusaoRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigEmpregadosInclusaoRepPlus : TarefaAbstrata
  {
    private RepBase _rep;
    private GerenciadorConfigEmpregadosInclusaoRepPlus.Estados _estadoRep;
    private int _index;
    private List<Empregado> _lstEmpregadosNoCadastro = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroOrdenado = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroInclusao = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroInclusaoEnviado = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroInclusaoEnviadoRetorno = new List<Empregado>();
    private List<MsgTCPAplicacaoPosPISEmpregados.PosPis> _lstPosPIS = new List<MsgTCPAplicacaoPosPISEmpregados.PosPis>();
    private int _totalPosPIS;
    private int _totalLOGS;
    private List<ResultadoAtualizacaoEmpregados> _lstResultadoInclusaoSDK = new List<ResultadoAtualizacaoEmpregados>();
    private List<ResultadoAtualizacaoEmpregados> _lstResultadoInclusao = new List<ResultadoAtualizacaoEmpregados>();
    private List<Empregado> _lstEmpregadosPisDupNoCadastro = new List<Empregado>();
    private List<Empregado> _lstEmpregadosPisVazioNoCadastro = new List<Empregado>();
    private List<string> _lstPisOrdenadaNoCadastro = new List<string>();
    private bool _chamadaPeloSdk;
    private int _totalEmpregadosCadastroInclusao;
    public static GerenciadorConfigEmpregadosInclusaoRepPlus _gerenciadorConfigEmpregadosInclusaoRepPlus;

    public int index
    {
      get => this._index;
      set => this._index = value;
    }

    public List<MsgTCPAplicacaoPosPISEmpregados.PosPis> lstPosPIS
    {
      get => this._lstPosPIS;
      set => this._lstPosPIS = value;
    }

    public int totalPosPIS
    {
      get => this._totalPosPIS;
      set => this._totalPosPIS = value;
    }

    public int totalLOGS
    {
      get => this._totalLOGS;
      set => this._totalLOGS = value;
    }

    public int totalEmpregadosCadastroInclusao
    {
      get => this._totalEmpregadosCadastroInclusao;
      set => this._totalEmpregadosCadastroInclusao = value;
    }

    public event EventHandler<NotificarInclusaoEmpregadosEventArgs> OnNotificarEmpregadosInclusaoParaSdk;

    public static GerenciadorConfigEmpregadosInclusaoRepPlus getInstance() => GerenciadorConfigEmpregadosInclusaoRepPlus._gerenciadorConfigEmpregadosInclusaoRepPlus != null ? GerenciadorConfigEmpregadosInclusaoRepPlus._gerenciadorConfigEmpregadosInclusaoRepPlus : new GerenciadorConfigEmpregadosInclusaoRepPlus();

    public static GerenciadorConfigEmpregadosInclusaoRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigEmpregadosInclusaoRepPlus._gerenciadorConfigEmpregadosInclusaoRepPlus != null ? GerenciadorConfigEmpregadosInclusaoRepPlus._gerenciadorConfigEmpregadosInclusaoRepPlus : new GerenciadorConfigEmpregadosInclusaoRepPlus(rep);
    }

    public static GerenciadorConfigEmpregadosInclusaoRepPlus getInstance(
      RepBase rep,
      List<Empregado> lstEmpregadosSdk)
    {
      return GerenciadorConfigEmpregadosInclusaoRepPlus._gerenciadorConfigEmpregadosInclusaoRepPlus != null ? GerenciadorConfigEmpregadosInclusaoRepPlus._gerenciadorConfigEmpregadosInclusaoRepPlus : new GerenciadorConfigEmpregadosInclusaoRepPlus(rep, lstEmpregadosSdk);
    }

    public GerenciadorConfigEmpregadosInclusaoRepPlus()
    {
    }

    public GerenciadorConfigEmpregadosInclusaoRepPlus(RepBase rep)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
    }

    public GerenciadorConfigEmpregadosInclusaoRepPlus(RepBase rep, List<Empregado> lstEmpregadosSdk)
    {
      this._rep = rep;
      this._lstEmpregadosNoCadastroInclusao = lstEmpregadosSdk;
      this._chamadaPeloSdk = true;
      this._totalEmpregadosCadastroInclusao = lstEmpregadosSdk.Count;
    }

    private void ProcessaDadosEmpregadosInclusao()
    {
      if (!this._chamadaPeloSdk)
      {
        Empregado empregado = new Empregado();
        Empregador empregador = new Empregador().PesquisarEmpregadorDeUmREP(this._rep.RepId);
        this._lstEmpregadosNoCadastroInclusao = !RegistrySingleton.GetInstance().UTILIZA_GRUPOS ? empregado.RecuperarEmpregadosDBInclusaoByEmpregador(this._rep.RepId, empregador.EmpregadorId) : (this._rep.grupoId != 0 ? GrupoRepXempregadoBI.PesquisarEmpregadosComInstrucaoInclusaoDoGrupo(this._rep.RepId, this._rep.grupoId, empregador.EmpregadorId) : empregado.RecuperarEmpregadosDBInclusaoByEmpregador(this._rep.RepId, empregador.EmpregadorId));
        this._lstPisOrdenadaNoCadastro = !RegistrySingleton.GetInstance().UTILIZA_GRUPOS ? empregado.PesquisarLstPISOrdenadaInclusao(empregador.EmpregadorId) : (this._rep.grupoId != 0 ? empregado.PesquisarLstPISOrdenadaGrupoRepInclusao(this._rep.grupoId, empregador.EmpregadorId) : empregado.PesquisarLstPISOrdenadaInclusao(empregador.EmpregadorId));
      }
      else
      {
        this._lstEmpregadosNoCadastroInclusao.Sort((Comparison<Empregado>) ((emp1, emp2) => emp1.Pis.CompareTo(emp2.Pis)));
        for (int index = 0; index < this._lstEmpregadosNoCadastroInclusao.Count; ++index)
          this._lstPisOrdenadaNoCadastro.Add(this._lstEmpregadosNoCadastroInclusao[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty));
      }
      foreach (string str in this._lstPisOrdenadaNoCadastro)
      {
        for (int index = 0; index < this._lstEmpregadosNoCadastroInclusao.Count; ++index)
        {
          if (this._lstEmpregadosNoCadastroInclusao[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty).Equals(str))
          {
            this._lstPosPIS.Add(new MsgTCPAplicacaoPosPISEmpregados.PosPis((short) (index + 1)));
            break;
          }
        }
      }
      this._totalEmpregadosCadastroInclusao = this._lstEmpregadosNoCadastroInclusao.Count;
      this._totalPosPIS = this._lstPosPIS.Count;
      this._rep.TimeoutExtra = 1350000L;
      int count = this._lstEmpregadosNoCadastroInclusao.Count;
    }

    public override void IniciarProcesso()
    {
      this.index = 1;
      this.Conectar(this._rep);
    }

    private void EnviaDadosEmpregadosInclusao(int index)
    {
      this.NotificarParaUsuario(Resources.msg_ENVIANDO_DADOS_EMPREGADOS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
      bool flag = false;
      switch (index)
      {
        case 0:
          index = 1;
          break;
        case 1:
          flag = true;
          break;
      }
      this._estadoRep = GerenciadorConfigEmpregadosInclusaoRepPlus.Estados.estEnviaDadosEmpregadosInclusao;
      if (this._lstEmpregadosNoCadastroInclusao.Count == 0)
        return;
      MsgTCPAplicacaoRegistroEmpregadosAtualizacaoRepPlus atualizacaoRepPlus = new MsgTCPAplicacaoRegistroEmpregadosAtualizacaoRepPlus();
      this._lstEmpregadosNoCadastroInclusaoEnviadoRetorno = new List<Empregado>();
      foreach (Empregado empregado in this._lstEmpregadosNoCadastroInclusao)
      {
        MsgTCPAplicacaoRegistroEmpregadosAtualizacaoRepPlus.ConfigEmpregado configEmpregado = new MsgTCPAplicacaoRegistroEmpregadosAtualizacaoRepPlus.ConfigEmpregado("I", empregado.Pis, empregado.CartaoBarras.ToString(), empregado.CartaoProx.ToString(), empregado.Nome.ToUpper(), empregado.NomeExibicao.ToUpper(), empregado.Senha, empregado.VerificaBiometria, empregado.DuplaVerificacao, empregado.Teclado.ToString(), this._rep.VersaoFW);
        if (atualizacaoRepPlus.AddEmpregado(configEmpregado))
          this._lstEmpregadosNoCadastroInclusaoEnviadoRetorno.Add(empregado);
        else
          break;
      }
      for (int index1 = 0; index1 < atualizacaoRepPlus.getRegMsg(); ++index1)
      {
        this._lstEmpregadosNoCadastroInclusaoEnviado.Add(this._lstEmpregadosNoCadastroInclusao[0]);
        this._lstEmpregadosNoCadastroInclusao.RemoveAt(0);
      }
      atualizacaoRepPlus.setIndex(index);
      atualizacaoRepPlus.setRegTotal(this._lstEmpregadosNoCadastroInclusaoEnviadoRetorno.Count);
      if (atualizacaoRepPlus.getRegMsg() > 0 && this._lstEmpregadosNoCadastroInclusaoEnviado.Count > 0)
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) atualizacaoRepPlus
        }, true);
      else if (this._lstEmpregadosNoCadastroInclusaoEnviado.Count == 0 && !flag)
      {
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        atualizacaoRepPlus.setIndex(1);
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) atualizacaoRepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      else if (flag)
        this._index = 1;
      else
        this._index = 1;
    }

    private void EnviaExecucaoEmpregadosInclusao()
    {
      this.NotificarParaUsuario(Resources.msg_ENVIANDO_DADOS_EMPREGADOS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
      bool flag = false;
      this._estadoRep = GerenciadorConfigEmpregadosInclusaoRepPlus.Estados.estEnviaEmpregadosInclusao;
      MsgTCPAplicacaoExecucaoEmpregadosAtualizacaoRepPlus atualizacaoRepPlus = new MsgTCPAplicacaoExecucaoEmpregadosAtualizacaoRepPlus();
      atualizacaoRepPlus.setRegTotal(this._lstEmpregadosNoCadastroInclusaoEnviadoRetorno.Count);
      atualizacaoRepPlus.setRegMsg(this._lstEmpregadosNoCadastroInclusaoEnviado.Count);
      if (atualizacaoRepPlus.getRegMsg() > 0 && this._lstEmpregadosNoCadastroInclusaoEnviado.Count > 0)
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) atualizacaoRepPlus
        }, true);
      else if (this._lstEmpregadosNoCadastroInclusaoEnviado.Count == 0 && !flag)
      {
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        atualizacaoRepPlus.setIndex(1);
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) atualizacaoRepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      else if (flag)
        this._index = 1;
      else
        this._index = 1;
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      try
      {
        switch (this._estadoRep)
        {
          case GerenciadorConfigEmpregadosInclusaoRepPlus.Estados.estEnviaEmpregadosInclusao:
            try
            {
              if (envelope.Grp != (byte) 11 || envelope.Cmd != (byte) 107)
                break;
              if (!this._chamadaPeloSdk && this._lstEmpregadosNoCadastroInclusao.Count <= 0)
              {
                TipoTerminal TipoTerminalEnt = new TipoTerminal();
                TipoTerminal tipoTerminal = new TipoTerminal();
                TipoTerminalEnt.RepId = this._rep.RepId;
                TipoTerminalEnt.Sincronizado = true;
                tipoTerminal.AtualizarSincronizado(TipoTerminalEnt);
              }
              this.VerificarRetornoIncluidos(envelope);
              if (this._lstEmpregadosNoCadastroInclusao.Count > 0)
              {
                this.index = 0;
                this.EnviaDadosEmpregadosInclusao(this.index);
                break;
              }
              this._estadoRep = GerenciadorConfigEmpregadosInclusaoRepPlus.Estados.estFinal;
              this.EncerrarConexao();
              this.NotificarParaUsuario(Resources.msg_PROCESSO_EMPREGADOS_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            catch (Exception ex)
            {
              throw;
            }
          case GerenciadorConfigEmpregadosInclusaoRepPlus.Estados.estEnviaDadosEmpregadosInclusao:
            try
            {
              if (envelope.Grp != (byte) 11 || envelope.Cmd != (byte) 106)
                break;
              int msgAplicacaoEmByte1 = (int) envelope.MsgAplicacaoEmBytes[4];
              int msgAplicacaoEmByte2 = (int) envelope.MsgAplicacaoEmBytes[5];
              this.EnviaExecucaoEmpregadosInclusao();
              this.index = 0;
              break;
            }
            catch (Exception ex)
            {
              throw;
            }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void NotificaMensagemParaSdk(
      List<ResultadoAtualizacaoEmpregados> entAtualizacaoEmpregados)
    {
      NotificarInclusaoEmpregadosEventArgs e = new NotificarInclusaoEmpregadosEventArgs(entAtualizacaoEmpregados);
      if (this.OnNotificarEmpregadosInclusaoParaSdk == null)
        return;
      this.OnNotificarEmpregadosInclusaoParaSdk((object) this, e);
    }

    private void VerificarRetornoIncluidos(Envelope env)
    {
      if (!this._chamadaPeloSdk)
      {
        byte num = 2;
        Empregado empregado1 = new Empregado();
        foreach (Empregado empregado2 in this._lstEmpregadosNoCadastroInclusaoEnviadoRetorno)
        {
          NotificarInclusaoEmpregadosEventArgs empregadosEventArgs = new NotificarInclusaoEmpregadosEventArgs(env.MsgAplicacaoEmBytes[(int) num], empregado2.Pis);
          ++num;
          empregado1.GravaLogAtualizacao(empregado2.EmpregadorId, empregado2.EmpregadoId, empregadosEventArgs.Resultado.MsgInclusao, this._rep.RepId);
          if (empregadosEventArgs.Resultado.Retorno == 0 || empregadosEventArgs.Resultado.Retorno == 1 || empregadosEventArgs.Resultado.Retorno == 3)
            empregado1.ExcluirStatusAtualizacaoByRepEmpregado(empregado2.EmpregadoId, this._rep.RepId);
          else
            empregado1.GravaLogAtualizacao(empregado2.EmpregadorId, empregado2.EmpregadoId, empregadosEventArgs.Resultado.MsgInclusao, this._rep.RepId);
        }
        this._lstEmpregadosNoCadastroInclusaoEnviado.Clear();
        this._lstEmpregadosNoCadastroInclusaoEnviado = new List<Empregado>();
      }
      else
      {
        byte num = 2;
        foreach (Empregado empregado in this._lstEmpregadosNoCadastroInclusaoEnviadoRetorno)
        {
          NotificarInclusaoEmpregadosEventArgs empregadosEventArgs = new NotificarInclusaoEmpregadosEventArgs(env.MsgAplicacaoEmBytes[(int) num], empregado.Pis);
          ++num;
          this._lstResultadoInclusaoSDK.Add(empregadosEventArgs.Resultado);
        }
        if (this._lstResultadoInclusaoSDK.Count != this._totalEmpregadosCadastroInclusao)
          return;
        this.NotificaMensagemParaSdk(this._lstResultadoInclusaoSDK);
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        this.index = 0;
        this.EnviaDadosEmpregadosInclusao(this.index);
      }
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigEmpregadosInclusaoRepPlus.Estados.estEnviaEmpregadosInclusao:
          menssagem = Resources.msgTIMEOUT_ENVIO_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregadosInclusaoRepPlus.Estados.estEnviaDadosEmpregadosInclusao:
          menssagem = Resources.msgTIMEOUT_ENVIO_EMPREGADOS;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigEmpregadosInclusaoRepPlus.Estados.estEnviaEmpregadosInclusao)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_EMPREGADOS;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnviaEmpregadosInclusao,
      estEnviaDadosEmpregadosInclusao,
      estFinal,
    }
  }
}
