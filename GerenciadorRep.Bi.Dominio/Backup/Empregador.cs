// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.Empregador
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class Empregador
  {
    private int _empregadorId;
    private int _empregadorIdSenior;
    private string _descricao = "";
    private string _razaoSocial = "";
    private string _cnpj = "";
    private string _cpf = "";
    private string _cei = "";
    private string _local = "";
    private bool _isCnpj;

    public string Local
    {
      get => this._local;
      set => this._local = value;
    }

    public string Descricao
    {
      get => this._descricao;
      set => this._descricao = value;
    }

    public int EmpregadorId
    {
      get => this._empregadorId;
      set => this._empregadorId = value;
    }

    public int EmpregadorIdSenior
    {
      get => this._empregadorIdSenior;
      set => this._empregadorIdSenior = value;
    }

    public string RazaoSocial
    {
      get => this._razaoSocial;
      set => this._razaoSocial = value;
    }

    public string Cnpj
    {
      get => this._cnpj;
      set => this._cnpj = value;
    }

    public bool isCnpj
    {
      get => this._isCnpj;
      set => this._isCnpj = value;
    }

    public string Cpf
    {
      get => this._cpf;
      set => this._cpf = value;
    }

    public string Cei
    {
      get => this._cei;
      set => this._cei = value;
    }

    public int AtualizarEmpregador(Empregador empregadorEnt)
    {
      int num = 0;
      try
      {
        num = new Empregador().AtualizarEmpregador(empregadorEnt);
        this._descricao = empregadorEnt.EmpregadorDesc;
        this._empregadorId = empregadorEnt.EmpregadorId;
        this._razaoSocial = empregadorEnt.RazaoSocial;
        this._cnpj = empregadorEnt.Cnpj;
        this._cpf = empregadorEnt.Cpf;
        this._cei = empregadorEnt.Cei;
        this._isCnpj = empregadorEnt.isCnpj;
        this._empregadorIdSenior = empregadorEnt.EmpregadorIdSenior;
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int AtualizarEmpregadores(Empregador empregadorEnt)
    {
      int num = 0;
      try
      {
        num = new Empregador().AtualizarEmpregadores(empregadorEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int InserirEmpregador(Empregador empregadorEnt)
    {
      int num = 0;
      try
      {
        num = new Empregador().InserirEmpregador(empregadorEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int InserirEmpregadorSenior(Empregador empregadorEnt)
    {
      int num = 0;
      try
      {
        num = new Empregador().InserirEmpregadorSenior(empregadorEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public static int InserirEmpregadorStatic(Empregador empregadorEnt)
    {
      int num = 0;
      try
      {
        num = new Empregador().InserirEmpregador(empregadorEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public Empregador PesquisarEmpregador(int idRep)
    {
      Empregador empregador = (Empregador) null;
      try
      {
        empregador = new Empregador().PesquisarEmpregador(idRep);
        this._descricao = empregador.EmpregadorDesc;
        this._empregadorId = empregador.EmpregadorId;
        this._razaoSocial = empregador.RazaoSocial;
        this._cnpj = empregador.Cnpj;
        this._cpf = empregador.Cpf;
        this._cei = empregador.Cei;
        this._isCnpj = empregador.isCnpj;
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregador;
    }

    public virtual Empregador PesquisarEmpregadorPorID(int idEmpregador)
    {
      Empregador empregador = (Empregador) null;
      try
      {
        empregador = new Empregador().PesquisarEmpregadorPorID(idEmpregador);
        this._descricao = empregador.EmpregadorDesc;
        this._empregadorId = empregador.EmpregadorId;
        this._razaoSocial = empregador.RazaoSocial;
        this._cnpj = empregador.Cnpj;
        this._cpf = empregador.Cpf;
        this._cei = empregador.Cei;
        this._isCnpj = empregador.isCnpj;
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregador;
    }

    public Empregador PesquisarEmpregadorPorIDSenior(int idSenior)
    {
      Empregador empregador = (Empregador) null;
      try
      {
        empregador = new Empregador().PesquisarEmpregadorPorIDSenior(idSenior);
        this._descricao = empregador.EmpregadorDesc;
        this._empregadorId = empregador.EmpregadorId;
        this._razaoSocial = empregador.RazaoSocial;
        this._cnpj = empregador.Cnpj;
        this._cpf = empregador.Cpf;
        this._cei = empregador.Cei;
        this._isCnpj = empregador.isCnpj;
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregador;
    }

    public virtual SortableBindingList<Empregador> PesquisarEmpregadores()
    {
      SortableBindingList<Empregador> sortableBindingList = (SortableBindingList<Empregador>) null;
      try
      {
        sortableBindingList = new Empregador().PesquisarEmpregadores();
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public SortableBindingList<Empregador> PesquisarEmpregadoresSenior()
    {
      SortableBindingList<Empregador> sortableBindingList = (SortableBindingList<Empregador>) null;
      try
      {
        sortableBindingList = new Empregador().PesquisarEmpregadoresSenior();
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public static SortableBindingList<Empregador> PesquisarEmpregadoresStatic()
    {
      SortableBindingList<Empregador> sortableBindingList = (SortableBindingList<Empregador>) null;
      try
      {
        sortableBindingList = new Empregador().PesquisarEmpregadores();
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public virtual SortableBindingList<Empregador> PesquisarEmpregadoresComAssociados()
    {
      SortableBindingList<Empregador> sortableBindingList = (SortableBindingList<Empregador>) null;
      try
      {
        sortableBindingList = new Empregador().PesquisarEmpregadores();
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public SortableBindingList<Empregador> PesquisarEmpregadoresComAssociadosSenior()
    {
      SortableBindingList<Empregador> sortableBindingList = (SortableBindingList<Empregador>) null;
      try
      {
        sortableBindingList = new Empregador().PesquisarEmpregadoresSenior();
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public Empregador PesquisarEmpregadorComAssociadosPorID(int empregadorID)
    {
      Empregador empregador = (Empregador) null;
      try
      {
        empregador = new Empregador().PesquisarEmpregadorComAssociadosPorID(empregadorID);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregador;
    }

    public Empregador PesquisarEmpregadorComAssociadosPorIDRepPlus(int empregadorID)
    {
      Empregador empregador = (Empregador) null;
      try
      {
        empregador = new Empregador().PesquisarEmpregadorComAssociadosPorIDRepPlus(empregadorID);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregador;
    }

    public Empregador PesquisarEmpregadorDeUmREP(int repId)
    {
      Empregador empregador = (Empregador) null;
      try
      {
        empregador = new Empregador().PesquisarEmpregadorDeUmREP(repId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregador;
    }

    public int ExcluirEmpregador(Empregador empregadorEnt)
    {
      int num = 0;
      try
      {
        num = new Empregador().ExcluirEmpregador(empregadorEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public bool ExisteRazaoSocial(Empregador empregadorEnt)
    {
      bool flag = false;
      try
      {
        flag = new Empregador().ExisteRazaoSocial(empregadorEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return flag;
    }

    public bool ExisteCnpj(Empregador empregadorEnt)
    {
      bool flag = false;
      try
      {
        flag = new Empregador().ExisteCnpj(empregadorEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return flag;
    }

    public bool ExisteCei(Empregador empregadorEnt)
    {
      bool flag = false;
      try
      {
        flag = new Empregador().ExisteCei(empregadorEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return flag;
    }

    public bool VerificaMultiploCEI(Empregador empregadorEnt)
    {
      bool flag = false;
      try
      {
        flag = new Empregador().VerificaMultiploCEI(empregadorEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return flag;
    }

    public bool ExisteCpf(Empregador empregadorEnt)
    {
      bool flag = false;
      try
      {
        flag = new Empregador().ExisteCpf(empregadorEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return flag;
    }

    public bool VerificarEmpregadorCadastrado(int idRep)
    {
      bool flag = false;
      try
      {
        flag = !(new Empregador().PesquisarEmpregador(idRep).RazaoSocial.Trim() == string.Empty);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return flag;
    }

    public int PesquisarQuatidadeEmpregadores()
    {
      int num = int.MinValue;
      try
      {
        num = new Empregador().PesquisarQuatidadeEmpregadores();
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    internal static List<string> PesquisarLstPisEmpregador(int empregadorId)
    {
      List<string> stringList = (List<string>) null;
      try
      {
        stringList = new Empregador().PesquisarLstPisEmpregador(empregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return stringList;
    }

    internal static List<string> PesquisarLstCartoesEmpregador(int empregadorId)
    {
      List<string> stringList = (List<string>) null;
      try
      {
        stringList = new Empregador().PesquisarLstCartoesEmpregador(empregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return stringList;
    }

    public Empregador PesquisarEmpregadorPorCNPJ(string CNPJ, string CEI)
    {
      Empregador empregador = (Empregador) null;
      try
      {
        empregador = new Empregador().PesquisarEmpregadorPorCNPJ(CNPJ, CEI);
        this._descricao = empregador.EmpregadorDesc;
        this._empregadorId = empregador.EmpregadorId;
        this._razaoSocial = empregador.RazaoSocial;
        this._cnpj = empregador.Cnpj;
        this._cpf = empregador.Cpf;
        this._cei = empregador.Cei;
        this._isCnpj = empregador.isCnpj;
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregador;
    }

    public Empregador PesquisarEmpregadorPorCPF(string CPF, string CEI)
    {
      Empregador empregador = (Empregador) null;
      try
      {
        empregador = new Empregador().PesquisarEmpregadorPorCPF(CPF, CEI);
        this._descricao = empregador.EmpregadorDesc;
        this._empregadorId = empregador.EmpregadorId;
        this._razaoSocial = empregador.RazaoSocial;
        this._cnpj = empregador.Cnpj;
        this._cpf = empregador.Cpf;
        this._cei = empregador.Cei;
        this._isCnpj = empregador.isCnpj;
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregador;
    }
  }
}
