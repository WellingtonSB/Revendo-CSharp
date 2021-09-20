// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigEmpregadosRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigEmpregadosRepPlus : TarefaAbstrata
  {
    private bool _empregadorREPDiferente;
    public int QuantidadeUsuarioLM;
    private long _timeoutExtraInclusao;
    private long _timeoutExtraExclusao;
    private bool _comInstrucoes;
    private RepBase _rep;
    private GerenciadorConfigEmpregadosRepPlus.Estados _estadoRep;
    private int _index;
    private string _acao;
    private List<MsgTCPAplicacaoRegistroLOGEmpregados.ConfigLOG> _lstLogs = new List<MsgTCPAplicacaoRegistroLOGEmpregados.ConfigLOG>();
    private List<MsgTCPAplicacaoPosPISEmpregados.PosPis> _lstPosPIS = new List<MsgTCPAplicacaoPosPISEmpregados.PosPis>();
    private List<Empregado> _lstEmpregadosNoREP = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastro = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroOrdenado = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroInclusao = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroExclusao = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroInclusaoEnviado = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroExclusaoEnviado = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroInclusaoEnviadoRetorno = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroExclusaoEnviadoRetorno = new List<Empregado>();
    private bool _enviaSincronizacao;
    private bool _testeProducao;
    private bool _liberacaoAT;
    private List<string> _lstDadosInvalidosEmpregados = new List<string>();
    private List<Empregado> _lstEmpregadosPisDupNoCadastro = new List<Empregado>();
    private List<Empregado> _lstEmpregadosPisVazioNoCadastro = new List<Empregado>();
    private List<string> _lstPisOrdenadaNoCadastro = new List<string>();
    private bool _chamadaPeloSdk;
    private bool chamadaPeloSenior;
    private int _totalEmpregadosCadastro;
    private int _totalEmpregadosCadastroInclusao;
    private int _totalLOGS;
    private int _totalPosPIS;
    public static GerenciadorConfigEmpregadosRepPlus _gerenciadorConfigEmpregadosRepPlus;

    public bool EmpregadorREPDiferente
    {
      get => this._empregadorREPDiferente;
      set => this._empregadorREPDiferente = value;
    }

    public int index
    {
      get => this._index;
      set => this._index = value;
    }

    public string acao
    {
      get => this._acao;
      set => this._acao = value;
    }

    public List<MsgTCPAplicacaoRegistroLOGEmpregados.ConfigLOG> lstLogs
    {
      get => this._lstLogs;
      set => this._lstLogs = value;
    }

    public List<MsgTCPAplicacaoPosPISEmpregados.PosPis> lstPosPIS
    {
      get => this._lstPosPIS;
      set => this._lstPosPIS = value;
    }

    public bool ChamadaPeloSenior
    {
      get => this.chamadaPeloSenior;
      set => this.chamadaPeloSenior = value;
    }

    public event EventHandler<NotificarParaUsuarioInclusaoEventArgs> OnNotificarParaUsuarioInclusao;

    public event EventHandler<NotificarRegistrosEmpregadosEventArgs> OnNotificarEmpregadosEnviados;

    public static GerenciadorConfigEmpregadosRepPlus getInstance() => GerenciadorConfigEmpregadosRepPlus._gerenciadorConfigEmpregadosRepPlus != null ? GerenciadorConfigEmpregadosRepPlus._gerenciadorConfigEmpregadosRepPlus : new GerenciadorConfigEmpregadosRepPlus();

    public static GerenciadorConfigEmpregadosRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigEmpregadosRepPlus._gerenciadorConfigEmpregadosRepPlus != null ? GerenciadorConfigEmpregadosRepPlus._gerenciadorConfigEmpregadosRepPlus : new GerenciadorConfigEmpregadosRepPlus(rep);
    }

    public static GerenciadorConfigEmpregadosRepPlus getInstance(
      RepBase rep,
      bool enviaSincronizacao)
    {
      return GerenciadorConfigEmpregadosRepPlus._gerenciadorConfigEmpregadosRepPlus != null ? GerenciadorConfigEmpregadosRepPlus._gerenciadorConfigEmpregadosRepPlus : new GerenciadorConfigEmpregadosRepPlus(rep, enviaSincronizacao);
    }

    public static GerenciadorConfigEmpregadosRepPlus getInstance(
      RepBase rep,
      bool enviaSincronizacao,
      bool testeProducao)
    {
      return GerenciadorConfigEmpregadosRepPlus._gerenciadorConfigEmpregadosRepPlus != null ? GerenciadorConfigEmpregadosRepPlus._gerenciadorConfigEmpregadosRepPlus : new GerenciadorConfigEmpregadosRepPlus(rep, enviaSincronizacao, testeProducao);
    }

    public static GerenciadorConfigEmpregadosRepPlus getInstance(
      RepBase rep,
      List<Empregado> lstEmpregadosSdk,
      bool enviaSincronizacao)
    {
      return GerenciadorConfigEmpregadosRepPlus._gerenciadorConfigEmpregadosRepPlus != null ? GerenciadorConfigEmpregadosRepPlus._gerenciadorConfigEmpregadosRepPlus : new GerenciadorConfigEmpregadosRepPlus(rep, lstEmpregadosSdk, enviaSincronizacao);
    }

    public static GerenciadorConfigEmpregadosRepPlus getInstance(
      RepBase rep,
      bool enviaSincronizacao,
      bool testeProducao,
      bool liberacaoAT)
    {
      return GerenciadorConfigEmpregadosRepPlus._gerenciadorConfigEmpregadosRepPlus != null ? GerenciadorConfigEmpregadosRepPlus._gerenciadorConfigEmpregadosRepPlus : new GerenciadorConfigEmpregadosRepPlus(rep, enviaSincronizacao, testeProducao, liberacaoAT);
    }

    public GerenciadorConfigEmpregadosRepPlus()
    {
    }

    public GerenciadorConfigEmpregadosRepPlus(RepBase rep)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
    }

    public GerenciadorConfigEmpregadosRepPlus(RepBase rep, bool enviaSincronizacao)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
      this._enviaSincronizacao = enviaSincronizacao;
    }

    public GerenciadorConfigEmpregadosRepPlus(
      RepBase rep,
      bool enviaSincronizacao,
      bool testeProducao)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
      this._enviaSincronizacao = enviaSincronizacao;
      this._testeProducao = testeProducao;
    }

    public GerenciadorConfigEmpregadosRepPlus(
      RepBase rep,
      bool enviaSincronizacao,
      bool testeProducao,
      bool liberacaoAT)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
      this._enviaSincronizacao = enviaSincronizacao;
      this._testeProducao = testeProducao;
      this._liberacaoAT = liberacaoAT;
    }

    public GerenciadorConfigEmpregadosRepPlus(
      RepBase rep,
      List<Empregado> lstEmpregadosSdk,
      bool enviaSincronizacao)
    {
      this._rep = rep;
      this._lstEmpregadosNoCadastro = lstEmpregadosSdk;
      this._chamadaPeloSdk = true;
      this._enviaSincronizacao = enviaSincronizacao;
    }

    private void ProcessaDadosEmpregados()
    {
      if (!this._chamadaPeloSdk)
      {
        Empregado empregado = !this._liberacaoAT ? new Empregado() : (Empregado) new EmpregadoAT();
        Empregador empregador = new Empregador().PesquisarEmpregadorDeUmREP(this._rep.RepId);
        this._lstEmpregadosNoCadastro = !RegistrySingleton.GetInstance().UTILIZA_GRUPOS ? empregado.RecuperarEmpregadosDBByEmpregadorRepPlus(empregador.EmpregadorId) : (this._rep.grupoId != 0 ? (this.chamadaPeloSenior ? GrupoRepXempregadoBI.PesquisarEmpregadosDoGrupoRepPlusSenior(this._rep.grupoId, this._rep.TecnologiaProx, empregador.EmpregadorId) : (!this._liberacaoAT ? GrupoRepXempregadoBI.PesquisarEmpregadosDoGrupoRepPlus(this._rep.grupoId, empregador.EmpregadorId) : GrupoRepXempregadoBI.PesquisarEmpregadosDoGrupoRepPlusAT(this._rep.grupoId, empregador.EmpregadorId))) : empregado.RecuperarEmpregadosDBByEmpregadorRepPlus(empregador.EmpregadorId));
        this._lstPisOrdenadaNoCadastro = !RegistrySingleton.GetInstance().UTILIZA_GRUPOS ? empregado.PesquisarLstPISOrdenada(empregador.EmpregadorId) : (this._rep.grupoId != 0 ? empregado.PesquisarLstPISOrdenadaGrupoRep(this._rep.grupoId, empregador.EmpregadorId) : empregado.PesquisarLstPISOrdenada(empregador.EmpregadorId));
      }
      else
      {
        this._lstEmpregadosNoCadastro.Sort((Comparison<Empregado>) ((emp1, emp2) => emp1.Pis.CompareTo(emp2.Pis)));
        for (int index = 0; index < this._lstEmpregadosNoCadastro.Count; ++index)
          this._lstPisOrdenadaNoCadastro.Add(this._lstEmpregadosNoCadastro[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty));
        this._lstEmpregadosNoCadastro.Sort((Comparison<Empregado>) ((emp1, emp2) => emp1.Cartao.CompareTo(emp2.Cartao)));
      }
      foreach (Empregado empregado in this._lstEmpregadosNoREP)
        empregado.Pis = empregado.Pis.Remove(0, 1);
      foreach (string str in this._lstPisOrdenadaNoCadastro)
      {
        for (int index = 0; index < this._lstEmpregadosNoCadastro.Count; ++index)
        {
          if (this._lstEmpregadosNoCadastro[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty).Equals(str))
          {
            this._lstEmpregadosNoCadastroOrdenado.Add(this._lstEmpregadosNoCadastro[index]);
            break;
          }
        }
      }
      this._totalEmpregadosCadastro = this._lstEmpregadosNoCadastro.Count;
      this._totalPosPIS = this._lstPosPIS.Count;
      this._totalLOGS = this._lstLogs.Count;
      this._rep.TimeoutExtra = 1350000L;
      if (this._rep.TipoTerminalId == 27 || this._rep.TipoTerminalId == 26 || this._rep.TipoTerminalId == 28 || this._rep.TipoTerminalId == 30)
        this._rep.TimeoutExtra += (long) (this.QuantidadeUsuarioLM * 370 + this._lstEmpregadosNoCadastro.Count * 20);
      if (this._chamadaPeloSdk || FormatoCartao.PesquisarFormatoCartaoProxByRepIdRepPlus(this._rep.RepId).formatoCartaoID != 14)
        return;
      this.TratarWiegandDecimal(this._lstEmpregadosNoCadastro);
    }

    private void TratarWiegandDecimal(List<Empregado> ListEmpregado)
    {
      try
      {
        foreach (Empregado empregado in ListEmpregado)
        {
          string str1 = empregado.CartaoProx.ToString("X").PadLeft(6, '0');
          string str2 = "" + int.Parse(str1.Substring(0, 2), NumberStyles.HexNumber).ToString() + int.Parse(str1.Substring(2, 4), NumberStyles.HexNumber).ToString();
          empregado.CartaoProx = Convert.ToUInt64(str2);
        }
      }
      catch
      {
      }
    }

    private void ProcessaDadosEmpregadosInclusao()
    {
      if (!this._chamadaPeloSdk)
      {
        Empregado empregado = !this._liberacaoAT ? new Empregado() : (Empregado) new EmpregadoAT();
        Empregador empregador = new Empregador().PesquisarEmpregadorDeUmREP(this._rep.RepId);
        this._lstEmpregadosNoCadastroInclusao = !RegistrySingleton.GetInstance().UTILIZA_GRUPOS ? empregado.RecuperarEmpregadosDBInclusaoByEmpregador(this._rep.RepId, empregador.EmpregadorId) : (this._rep.grupoId != 0 ? (!this._liberacaoAT ? GrupoRepXempregadoBI.PesquisarEmpregadosComInstrucaoInclusaoDoGrupo(this._rep.RepId, this._rep.grupoId, empregador.EmpregadorId) : GrupoRepXempregadoBI.PesquisarEmpregadosComInstrucaoInclusaoDoGrupoAT(this._rep.RepId, this._rep.grupoId, empregador.EmpregadorId)) : empregado.RecuperarEmpregadosDBInclusaoByEmpregador(this._rep.RepId, empregador.EmpregadorId));
        this._lstPisOrdenadaNoCadastro = !RegistrySingleton.GetInstance().UTILIZA_GRUPOS ? empregado.PesquisarLstPISOrdenadaInclusao(empregador.EmpregadorId) : (this._rep.grupoId != 0 ? empregado.PesquisarLstPISOrdenadaGrupoRepInclusao(this._rep.grupoId, this._rep.RepId) : empregado.PesquisarLstPISOrdenadaInclusao(this._rep.RepId));
      }
      else
      {
        this._lstEmpregadosNoCadastro.Sort((Comparison<Empregado>) ((emp1, emp2) => emp1.Pis.CompareTo(emp2.Pis)));
        for (int index = 0; index < this._lstEmpregadosNoCadastro.Count; ++index)
          this._lstPisOrdenadaNoCadastro.Add(this._lstEmpregadosNoCadastro[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty));
        this._lstEmpregadosNoCadastro.Sort((Comparison<Empregado>) ((emp1, emp2) => emp1.Cartao.CompareTo(emp2.Cartao)));
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
      this._timeoutExtraInclusao = 1350000L;
      if (this._lstEmpregadosNoCadastroInclusao.Count == 0 || this._chamadaPeloSdk || FormatoCartao.PesquisarFormatoCartaoProxByRepIdRepPlus(this._rep.RepId).formatoCartaoID != 14)
        return;
      this.TratarWiegandDecimal(this._lstEmpregadosNoCadastroInclusao);
    }

    private void ProcessaDadosEmpregadosExclusao()
    {
      if (!this._chamadaPeloSdk)
      {
        Empregado empregado = new Empregado();
        Empregador empregador = new Empregador().PesquisarEmpregadorDeUmREP(this._rep.RepId);
        this._lstEmpregadosNoCadastroExclusao = !RegistrySingleton.GetInstance().UTILIZA_GRUPOS ? empregado.RecuperarEmpregadosDBExclusaoByEmpregador(this._rep.RepId, empregador.EmpregadorId) : (this._rep.grupoId != 0 ? GrupoRepXempregadoBI.PesquisarEmpregadosComInstrucaoExclusaoDoGrupo(this._rep.RepId, empregador.EmpregadorId) : empregado.RecuperarEmpregadosDBExclusaoByEmpregador(this._rep.RepId, empregador.EmpregadorId));
        this._lstPisOrdenadaNoCadastro = !RegistrySingleton.GetInstance().UTILIZA_GRUPOS ? empregado.PesquisarLstPISOrdenadaExclusao(this._rep.RepId) : (this._rep.grupoId != 0 ? empregado.PesquisarLstPISOrdenadaGrupoRepExclusao(this._rep.grupoId, this._rep.RepId) : empregado.PesquisarLstPISOrdenadaExclusao(this._rep.RepId));
      }
      else
      {
        this._lstEmpregadosNoCadastro.Sort((Comparison<Empregado>) ((emp1, emp2) => emp1.Pis.CompareTo(emp2.Pis)));
        for (int index = 0; index < this._lstEmpregadosNoCadastro.Count; ++index)
          this._lstPisOrdenadaNoCadastro.Add(this._lstEmpregadosNoCadastro[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty));
        this._lstEmpregadosNoCadastro.Sort((Comparison<Empregado>) ((emp1, emp2) => emp1.Cartao.CompareTo(emp2.Cartao)));
      }
      foreach (string str in this._lstPisOrdenadaNoCadastro)
      {
        for (int index = 0; index < this._lstEmpregadosNoCadastroExclusao.Count; ++index)
        {
          if (this._lstEmpregadosNoCadastroExclusao[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty).Equals(str))
          {
            this._lstPosPIS.Add(new MsgTCPAplicacaoPosPISEmpregados.PosPis((short) (index + 1)));
            break;
          }
        }
      }
      this._totalEmpregadosCadastro = this._lstEmpregadosNoCadastroExclusao.Count;
      this._totalPosPIS = this._lstPosPIS.Count;
      this._timeoutExtraExclusao = 1350000L;
      int count = this._lstEmpregadosNoCadastroExclusao.Count;
    }

    public bool ValidaDadosEmpregadosPisDuplicados(
      ref string empregadosPisDupAux,
      int EmpregadorID,
      ref string empregadosSemPis)
    {
      if (!this._chamadaPeloSdk)
      {
        Empregado empregado = new Empregado();
        this._lstEmpregadosNoCadastro = empregado.RecuperarEmpregadosDBByEmpregador(EmpregadorID);
        this._lstPisOrdenadaNoCadastro = empregado.PesquisarLstPISOrdenada(EmpregadorID);
      }
      else
        this._lstEmpregadosNoCadastro.Sort((Comparison<Empregado>) ((emp1, emp2) => emp1.Cartao.CompareTo(emp2.Cartao)));
      foreach (Empregado empregado in this._lstEmpregadosNoREP)
        empregado.Pis = empregado.Pis.Remove(0, 1);
      foreach (string str in this._lstPisOrdenadaNoCadastro)
      {
        for (int index = 0; index < this._lstEmpregadosNoCadastro.Count; ++index)
        {
          if (this._lstEmpregadosNoCadastro[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty).Equals(str))
          {
            this._lstPosPIS.Add(new MsgTCPAplicacaoPosPISEmpregados.PosPis((short) (index + 1)));
            break;
          }
        }
      }
      this._totalEmpregadosCadastro = this._lstEmpregadosNoCadastro.Count;
      this._totalPosPIS = this._lstPosPIS.Count;
      Empregado empregado1 = new Empregado();
      this._lstEmpregadosPisDupNoCadastro = empregado1.RecuperarEmpregadosDBByEmpregadorPisDuplicado(EmpregadorID);
      this._lstEmpregadosPisVazioNoCadastro = empregado1.RecuperarEmpregadosDBByEmpregadorSemPis(EmpregadorID);
      foreach (Empregado empregado2 in this._lstEmpregadosPisDupNoCadastro)
      {
        ref string local = ref empregadosPisDupAux;
        local = local + empregado2.Pis + " - " + empregado2.Nome + "\n";
      }
      foreach (Empregado empregado3 in this._lstEmpregadosPisVazioNoCadastro)
      {
        ref string local = ref empregadosSemPis;
        local = local + empregado3.Nome + "\n";
      }
      return this._totalEmpregadosCadastro == this._totalPosPIS && !(empregadosPisDupAux != "") && !(empregadosSemPis != "");
    }

    public override void IniciarProcesso()
    {
      try
      {
        this.NotificarParaUsuario(Resources.msgCARREGANDO_EMPREGADOS_BASE_DADOS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
        if (!this._chamadaPeloSdk)
        {
          SortableBindingList<StatusAtualizacao> sortableBindingList = new StatusAtualizacao().PesquisarStatusAtualizacaoNoBDByRep(this._rep.RepId);
          string str = this._rep.IpAddress.Trim();
          RepBase repBase = this._rep.PesquisarRepPorID(this._rep.RepId);
          if (str != null && !str.Equals(""))
            this._rep.IpAddress = str;
          this._enviaSincronizacao = sortableBindingList.Count >= 500 || sortableBindingList.Count == 0 || !repBase.Sincronizado || this._empregadorREPDiferente || this._testeProducao;
        }
        if (this._enviaSincronizacao)
        {
          this.ProcessaDadosEmpregados();
        }
        else
        {
          this.index = 0;
          this.ProcessaDadosEmpregadosInclusao();
          this.ProcessaDadosEmpregadosExclusao();
          this._rep.TimeoutExtra = this._timeoutExtraInclusao + this._timeoutExtraExclusao;
        }
        this._lstEmpregadosNoREP = new List<Empregado>();
        this._lstLogs = new List<MsgTCPAplicacaoRegistroLOGEmpregados.ConfigLOG>();
        this.index = 1;
        this.Conectar(this._rep);
      }
      catch (Exception ex)
      {
        this.NotificarParaUsuario(ex.Message, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        this.EncerrarConexao();
      }
    }

    private void EnviaDadosEmpregados(int index)
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
      this._estadoRep = GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregados;
      if (this._lstEmpregadosNoCadastroOrdenado.Count == 0)
        return;
      MsgTCPAplicacaoRegistroEmpregadosRepPlus empregadosRepPlus = new MsgTCPAplicacaoRegistroEmpregadosRepPlus();
      foreach (Empregado empregado in this._lstEmpregadosNoCadastroOrdenado)
      {
        MsgTCPAplicacaoRegistroEmpregadosRepPlus.ConfigEmpregado configEmpregado = this._liberacaoAT ? new MsgTCPAplicacaoRegistroEmpregadosRepPlus.ConfigEmpregado(empregado.Pis, empregado.CartaoBarras.ToString(), empregado.CartaoProx.ToString(), empregado.Nome, empregado.NomeExibicao, empregado.Senha, empregado.VerificaBiometria, empregado.DuplaVerificacao, empregado.Teclado.ToString(), this._rep.VersaoFW) : new MsgTCPAplicacaoRegistroEmpregadosRepPlus.ConfigEmpregado(empregado.Pis, empregado.CartaoBarras.ToString(), empregado.CartaoProx.ToString(), empregado.Nome.ToUpper(), empregado.NomeExibicao.ToUpper(), empregado.Senha, empregado.VerificaBiometria, empregado.DuplaVerificacao, empregado.Teclado.ToString(), this._rep.VersaoFW);
        if (!empregadosRepPlus.AddEmpregado(configEmpregado))
          break;
      }
      for (int index1 = 0; index1 < empregadosRepPlus.getRegMsg(); ++index1)
        this._lstEmpregadosNoCadastroOrdenado.RemoveAt(0);
      empregadosRepPlus.setIndex(index);
      empregadosRepPlus.setRegTotal(this._totalEmpregadosCadastro);
      if (empregadosRepPlus.getRegMsg() > 0 && this._totalEmpregadosCadastro > 0)
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) empregadosRepPlus
        }, true);
      else if (this._totalEmpregadosCadastro == 0 && !flag)
      {
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        empregadosRepPlus.setIndex(1);
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) empregadosRepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      else if (flag)
        this._index = 1;
      else
        this._index = 1;
    }

    private void EnviaDadosEmpregadosExclusao()
    {
      this.NotificarParaUsuario(Resources.msg_ENVIANDO_DADOS_EMPREGADOS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
      this._estadoRep = GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregadosExclusao;
      if (this._lstEmpregadosNoCadastroExclusao.Count == 0)
        return;
      MsgTCPAplicacaoRegistroEmpregadosExclusaoRepPlus empregadosExclusaoRepPlus = new MsgTCPAplicacaoRegistroEmpregadosExclusaoRepPlus();
      this._lstEmpregadosNoCadastroExclusaoEnviadoRetorno = new List<Empregado>();
      foreach (Empregado empregado in this._lstEmpregadosNoCadastroExclusao)
      {
        MsgTCPAplicacaoRegistroEmpregadosExclusaoRepPlus.ConfigEmpregado configEmpregado = new MsgTCPAplicacaoRegistroEmpregadosExclusaoRepPlus.ConfigEmpregado(empregado.Pis);
        if (empregadosExclusaoRepPlus.AddEmpregado(configEmpregado))
          this._lstEmpregadosNoCadastroExclusaoEnviadoRetorno.Add(empregado);
        else
          break;
      }
      for (int index = 0; index < empregadosExclusaoRepPlus.getRegMsg(); ++index)
      {
        this._lstEmpregadosNoCadastroExclusaoEnviado.Add(this._lstEmpregadosNoCadastroExclusao[0]);
        this._lstEmpregadosNoCadastroExclusao.RemoveAt(0);
      }
      empregadosExclusaoRepPlus.setIndex(this.index);
      empregadosExclusaoRepPlus.setRegTotal(this._lstEmpregadosNoCadastroExclusaoEnviadoRetorno.Count);
      if (empregadosExclusaoRepPlus.getRegMsg() > 0 && this._lstEmpregadosNoCadastroExclusaoEnviado.Count > 0)
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) empregadosExclusaoRepPlus
        }, true);
      else if (this._lstEmpregadosNoCadastroExclusaoEnviado.Count == 0)
      {
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        empregadosExclusaoRepPlus.setIndex(1);
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) empregadosExclusaoRepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      else
        this._index = 1;
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
      this._estadoRep = GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaDadosEmpregadosInclusao;
      if (this._lstEmpregadosNoCadastroInclusao.Count == 0)
        return;
      MsgTCPAplicacaoRegistroEmpregadosAtualizacaoRepPlus atualizacaoRepPlus = new MsgTCPAplicacaoRegistroEmpregadosAtualizacaoRepPlus();
      this._lstEmpregadosNoCadastroInclusaoEnviadoRetorno = new List<Empregado>();
      foreach (Empregado empregado in this._lstEmpregadosNoCadastroInclusao)
      {
        MsgTCPAplicacaoRegistroEmpregadosAtualizacaoRepPlus.ConfigEmpregado configEmpregado = new MsgTCPAplicacaoRegistroEmpregadosAtualizacaoRepPlus.ConfigEmpregado("I", empregado.Pis, empregado.CartaoBarras.ToString(), empregado.CartaoProx.ToString(), empregado.Nome, empregado.NomeExibicao, empregado.Senha, empregado.VerificaBiometria, empregado.DuplaVerificacao, empregado.Teclado.ToString(), this._rep.VersaoFW);
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
      this._estadoRep = GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregadosInclusao;
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

    private void EnviaExecucaoSincronizacaoEmpregados()
    {
      this.NotificarParaUsuario(Resources.msg_ENVIANDO_DADOS_EMPREGADOS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
      this._estadoRep = GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregadosRespSincronizacao;
      MsgTCPAplicacaoExecucaoEmpregadosSincronizacaoRepPlus sincronizacaoRepPlus = new MsgTCPAplicacaoExecucaoEmpregadosSincronizacaoRepPlus();
      sincronizacaoRepPlus.setRegTotal(this._totalEmpregadosCadastro);
      if (sincronizacaoRepPlus.getRegMsg() > 0 && this._totalEmpregadosCadastro > 0)
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) sincronizacaoRepPlus
        }, true);
      else if (this._lstEmpregadosNoCadastroOrdenado.Count == 0)
      {
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        sincronizacaoRepPlus.setIndex(1);
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) sincronizacaoRepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      else
        this._index = 1;
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      try
      {
        switch (this._estadoRep)
        {
          case GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregados:
            try
            {
              if (envelope.Grp != (byte) 11 || envelope.Cmd != (byte) 103)
                break;
              int index = ((int) envelope.MsgAplicacaoEmBytes[4] << 8) + (int) envelope.MsgAplicacaoEmBytes[5];
              if (this._lstEmpregadosNoCadastroOrdenado.Count <= 0)
              {
                this.EnviaExecucaoSincronizacaoEmpregados();
                break;
              }
              this.EnviaDadosEmpregados(index);
              break;
            }
            catch (Exception ex)
            {
              throw;
            }
          case GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregadosInclusao:
            try
            {
              if (envelope.Grp != (byte) 11 || envelope.Cmd != (byte) 107)
                break;
              this.VerificarRetornoIncluidos(envelope);
              if (this._lstEmpregadosNoCadastroInclusao.Count > 0)
              {
                this.index = 0;
                this.EnviaDadosEmpregadosInclusao(this.index);
                break;
              }
              this._estadoRep = GerenciadorConfigEmpregadosRepPlus.Estados.estFinal;
              this.EncerrarConexao();
              this.NotificarParaUsuario(Resources.msg_PROCESSO_EMPREGADOS_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            catch (Exception ex)
            {
              throw;
            }
          case GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaDadosEmpregadosInclusao:
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
          case GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregadosRespSincronizacao:
            try
            {
              if (envelope.Grp != (byte) 11 || envelope.Cmd != (byte) 104)
                break;
              this._estadoRep = GerenciadorConfigEmpregadosRepPlus.Estados.estFinal;
              if (this._testeProducao)
              {
                NotificarRegistrosEmpregadosEventArgs e = new NotificarRegistrosEmpregadosEventArgs(this._lstEmpregadosNoCadastro);
                if (this.OnNotificarEmpregadosEnviados != null)
                  this.OnNotificarEmpregadosEnviados((object) this, e);
              }
              if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
              {
                this.FinalizaProcessamentoSincronizacao(Resources.msg_PROCESSO_EMPREGADOS_FINALIZADO);
                break;
              }
              this.FinalizaProcessamentoSincronizacao(Resources.msg_PROCESSO_EMPREGADOS_FINALIZADO_SEM_ALTERACOES);
              break;
            }
            catch (Exception ex)
            {
              throw;
            }
          case GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregadosExclusao:
            try
            {
              if (envelope.Grp != (byte) 11 || envelope.Cmd != (byte) 108)
                break;
              this.VerificarRetornoExcluidos(envelope);
              if (this._lstEmpregadosNoCadastroExclusao.Count <= 0)
              {
                if (this._lstEmpregadosNoCadastroInclusao.Count > 0)
                {
                  this.index = 0;
                  this.EnviaDadosEmpregadosInclusao(this.index);
                  break;
                }
                this._estadoRep = GerenciadorConfigEmpregadosRepPlus.Estados.estFinal;
                this.EncerrarConexao();
                this.NotificarParaUsuario(Resources.msg_PROCESSO_EMPREGADOS_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
                break;
              }
              this.EnviaDadosEmpregadosExclusao();
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

    public SortableBindingList<EmpregadosInvalidos> ProcessaDadosInvalidosEmpregados(
      int EmpregadorId)
    {
      return new Empregado().RecuperarEmpregadosDBByEmpregadorDadosInvalidosRepPlus(EmpregadorId);
    }

    private void VerificarRetornoIncluidos(Envelope env)
    {
      byte num = 2;
      Empregado empregado1 = new Empregado();
      foreach (Empregado empregado2 in this._lstEmpregadosNoCadastroInclusaoEnviadoRetorno)
      {
        NotificarInclusaoEmpregadosEventArgs empregadosEventArgs = new NotificarInclusaoEmpregadosEventArgs(env.MsgAplicacaoEmBytes[(int) num], empregado2.Pis);
        ++num;
        if (empregadosEventArgs.Resultado.Retorno == 0 || empregadosEventArgs.Resultado.Retorno == 1 || empregadosEventArgs.Resultado.Retorno == 3)
          empregado1.ExcluirStatusAtualizacaoByRepEmpregado(empregado2.EmpregadoId, this._rep.RepId);
        else
          empregado1.GravaLogAtualizacao(empregado2.EmpregadorId, empregado2.EmpregadoId, empregadosEventArgs.Resultado.MsgInclusao, this._rep.RepId);
      }
      this._lstEmpregadosNoCadastroInclusaoEnviado.Clear();
      this._lstEmpregadosNoCadastroInclusaoEnviado = new List<Empregado>();
      this._lstEmpregadosNoCadastroInclusaoEnviadoRetorno = new List<Empregado>();
    }

    private void VerificarRetornoExcluidos(Envelope env)
    {
      byte num = 2;
      Empregado empregado = new Empregado();
      foreach (Empregado empregadoEnt in this._lstEmpregadosNoCadastroExclusaoEnviadoRetorno)
      {
        NotificarExclusaoEmpregadosEventArgs empregadosEventArgs = new NotificarExclusaoEmpregadosEventArgs(env.MsgAplicacaoEmBytes[(int) num], empregadoEnt.Pis);
        ++num;
        if (empregadosEventArgs.Resultado.Retorno == 0 || empregadosEventArgs.Resultado.Retorno == 1)
        {
          empregadoEnt.Processado = true;
          empregado.ExcluirEmpregadoExcluidosDoRep(empregadoEnt, this._rep.RepId);
          empregado.ExcluirStatusAtualizacaoByRepEmpregado(empregadoEnt.EmpregadoId, this._rep.RepId);
        }
        else
          empregado.GravaLogAtualizacao(empregadoEnt.EmpregadorId, empregadoEnt.EmpregadoId, empregadosEventArgs.Resultado.MsgInclusao, this._rep.RepId);
      }
      this._lstEmpregadosNoCadastroExclusaoEnviado.Clear();
      this._lstEmpregadosNoCadastroExclusaoEnviado = new List<Empregado>();
      this._lstEmpregadosNoCadastroExclusaoEnviadoRetorno = new List<Empregado>();
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        if (this._enviaSincronizacao)
        {
          if (this._lstEmpregadosNoCadastroOrdenado.Count == 0)
          {
            this._rep.TimeoutExtra = 1800000L;
            this.EnviaExecucaoSincronizacaoEmpregados();
          }
          else
          {
            this.index = 0;
            this.EnviaDadosEmpregados(this.index);
          }
        }
        else if (this._lstEmpregadosNoCadastroExclusao.Count > 0)
          this.EnviaDadosEmpregadosExclusao();
        else if (this._lstEmpregadosNoCadastroInclusao.Count > 0)
        {
          this.index = 0;
          this.EnviaDadosEmpregadosInclusao(this.index);
        }
        else
        {
          this._estadoRep = GerenciadorConfigEmpregadosRepPlus.Estados.estFinal;
          this.NotificarParaUsuario(Resources.msg_PROCESSO_EMPREGADOS_FINALIZADO_SEM_ALTERACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
        }
      }
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigEmpregadosRepPlus.Estados.estProcessaDadosEmpregados:
          menssagem = Resources.msgTIMEOUT_PROCESSA_DADOS_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregados:
          menssagem = Resources.msgTIMEOUT_ENVIO_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregadosInclusao:
          menssagem = Resources.msgTIMEOUT_ENVIO_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaDadosEmpregadosInclusao:
          menssagem = Resources.msgTIMEOUT_ENVIO_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregadosExclusao:
          menssagem = Resources.msgTIMEOUT_ENVIO_EMPREGADOS;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigEmpregadosRepPlus.Estados.estProcessaDadosEmpregados:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_PROCESSA_DADOS_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregados:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregadosInclusao:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregadosRepPlus.Estados.estEnviaEmpregadosExclusao:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_EMPREGADOS;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private void FinalizaProcessamentoSincronizacao(string mensagem)
    {
      this.EncerrarConexao();
      this.NotificarParaUsuario(mensagem, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
      if (this._chamadaPeloSdk)
        return;
      new StatusAtualizacao().ExluirStatusAtualizacaoNoBDByRepId(this._rep.RepId);
      if (!this._enviaSincronizacao)
        return;
      new TipoTerminal().AtualizarSincronizado(new TipoTerminal()
      {
        RepId = this._rep.RepId,
        Sincronizado = true
      });
    }

    private new enum Estados
    {
      estProcessaDadosEmpregados,
      estEnviaEmpregados,
      estEnviaEmpregadosInclusao,
      estEnviaDadosEmpregadosInclusao,
      estEnviaEmpregadosRespSincronizacao,
      estEnviaEmpregadosExclusao,
      estFinal,
    }
  }
}
