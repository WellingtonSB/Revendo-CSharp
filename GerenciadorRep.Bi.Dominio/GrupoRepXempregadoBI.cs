// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GrupoRepXempregadoBI
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
  public static class GrupoRepXempregadoBI
  {
    public static int InserirGrupoRep(GrupoRepXempregados grupoRepEnt)
    {
      int num = 0;
      try
      {
        num = new GrupoRepXempregadosDAO().InserirGrupoRepXempregados(grupoRepEnt);
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

    public static int InserirListaGrupoRepXempregados(
      SortableBindingList<GrupoRepXempregados> LstGrupoRepEnt)
    {
      int num = 0;
      try
      {
        num = new GrupoRepXempregadosDAO().InserirListaGrupoRepXempregados(LstGrupoRepEnt);
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

    public static SortableBindingList<GrupoRepXempregados> PesquisarGruposXempregados()
    {
      SortableBindingList<GrupoRepXempregados> sortableBindingList = (SortableBindingList<GrupoRepXempregados>) null;
      try
      {
        sortableBindingList = new GrupoRepXempregadosDAO().PesquisarGruposXempregados();
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

    public static List<Empregado> PesquisarEmpregadosDoGrupo(
      int grupoRepId,
      int EmpregadorId)
    {
      List<Empregado> empregadoList = (List<Empregado>) null;
      try
      {
        empregadoList = new GrupoRepXempregadosDAO().PesquisarEmpregadosDoGrupo(grupoRepId, EmpregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        ex.Data.Add((object) "mensagem2", (object) message);
        throw;
      }
      return empregadoList;
    }

    public static List<Empregado> PesquisarEmpregadosDoGrupoSenior(
      int grupoRepId,
      int EmpregadorId,
      int terminalId)
    {
      List<Empregado> empregadoList = (List<Empregado>) null;
      try
      {
        empregadoList = new GrupoRepXempregadosDAO().PesquisarEmpregadosDoGrupoSenior(grupoRepId, EmpregadorId, terminalId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        ex.Data.Add((object) "mensagem2", (object) message);
        throw;
      }
      return empregadoList;
    }

    public static List<Empregado> PesquisarEmpregadosDoGrupoRepPlus(
      int grupoRepId,
      int EmpregadorId)
    {
      List<Empregado> empregadoList = (List<Empregado>) null;
      try
      {
        empregadoList = new GrupoRepXempregadosDAO().PesquisarEmpregadosDoGrupoRepPlus(grupoRepId, EmpregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        ex.Data.Add((object) "mensagem2", (object) message);
        throw;
      }
      return empregadoList;
    }

    public static List<Empregado> PesquisarEmpregadosDoGrupoRepPlusAT(
      int grupoRepId,
      int EmpregadorId)
    {
      List<Empregado> empregadoList = (List<Empregado>) null;
      try
      {
        empregadoList = new GrupoRepXempregadosDAO().PesquisarEmpregadosDoGrupoRepPlusAT(grupoRepId, EmpregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        ex.Data.Add((object) "mensagem2", (object) message);
        throw;
      }
      return empregadoList;
    }

    public static List<Empregado> PesquisarEmpregadosDoGrupoRepPlusSenior(
      int grupoRepId,
      int tecnologiaProx,
      int EmpregadorId)
    {
      List<Empregado> empregadoList = (List<Empregado>) null;
      try
      {
        empregadoList = new GrupoRepXempregadosDAO().PesquisarEmpregadosDoGrupoRepPlusSenior(grupoRepId, tecnologiaProx, EmpregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        ex.Data.Add((object) "mensagem2", (object) message);
        throw;
      }
      return empregadoList;
    }

    public static SortableBindingList<UsuarioBio> PesquisarTemplatesPorEmpregadorDoGrupoRepPlus(
      int grupoRepId,
      int EmpregadorId)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = (SortableBindingList<UsuarioBio>) null;
      try
      {
        sortableBindingList = new GrupoRepXempregadosDAO().PesquisarTemplatesPorEmpregadorDoGrupoRepPlus(grupoRepId, EmpregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        ex.Data.Add((object) "mensagem2", (object) message);
        throw;
      }
      return sortableBindingList;
    }

    public static SortableBindingList<UsuarioBio> PesquisarTemplatesCAMAPorEmpregadorDoGrupoRepPlus(
      int grupoRepId,
      int EmpregadorId)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = (SortableBindingList<UsuarioBio>) null;
      try
      {
        sortableBindingList = new GrupoRepXempregadosDAO().PesquisarTemplatesCAMAPorEmpregadorDoGrupoRepPlus(grupoRepId, EmpregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        ex.Data.Add((object) "mensagem2", (object) message);
        throw;
      }
      return sortableBindingList;
    }

    public static SortableBindingList<UsuarioBio> PesquisarTemplatesSAGEMPorEmpregadorDoGrupoRepPlus(
      int grupoRepId,
      int EmpregadorId)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = (SortableBindingList<UsuarioBio>) null;
      try
      {
        sortableBindingList = new GrupoRepXempregadosDAO().PesquisarTemplatesSAGEMPorEmpregadorDoGrupoRepPlus(grupoRepId, EmpregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        ex.Data.Add((object) "mensagem2", (object) message);
        throw;
      }
      return sortableBindingList;
    }

    public static SortableBindingList<GrupoRepXempregados> PesquisarGruposDoEmpregado(
      int empregadoId)
    {
      SortableBindingList<GrupoRepXempregados> sortableBindingList = (SortableBindingList<GrupoRepXempregados>) null;
      try
      {
        sortableBindingList = new GrupoRepXempregadosDAO().PesquisarGruposDoEmpregado(empregadoId);
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

    public static SortableBindingList<GrupoRepXempregados> PesquisarSomenteEmpregadosDoGrupo(
      int empregadoId)
    {
      SortableBindingList<GrupoRepXempregados> sortableBindingList = (SortableBindingList<GrupoRepXempregados>) null;
      try
      {
        sortableBindingList = new GrupoRepXempregadosDAO().PesquisarSomenteEmpregadosDoGrupo(empregadoId);
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

    public static int ExcluirEmpregadoDeTodosGrupos(int EmpregadoId)
    {
      int num = 0;
      try
      {
        num = new GrupoRepXempregadosDAO().ExcluirEmpregadoDeTodosGrupos(EmpregadoId);
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

    public static int ExcluirEmpregadoDoGrupo(GrupoRepXempregados grupoRepEnt)
    {
      int num = 0;
      try
      {
        num = new GrupoRepXempregadosDAO().ExcluirEmpregadoDoGrupo(grupoRepEnt);
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

    public static int ExcluirTodosEmpregadosDoGrupo(int grupoRepId)
    {
      int num = 0;
      try
      {
        num = new GrupoRepXempregadosDAO().ExcluirTodosEmpregadosDoGrupo(grupoRepId);
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

    public static List<Empregado> PesquisarEmpregadosComInstrucaoInclusaoDoGrupo(
      int RepId,
      int grupoRepId,
      int empregadorId)
    {
      List<Empregado> empregadoList = (List<Empregado>) null;
      try
      {
        empregadoList = new GrupoRepXempregadosDAO().PesquisarEmpregadosComInstrucaoInclusaoDoGrupo(RepId, grupoRepId, empregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        ex.Data.Add((object) "mensagem2", (object) message);
        throw;
      }
      return empregadoList;
    }

    public static List<Empregado> PesquisarEmpregadosComInstrucaoInclusaoDoGrupoAT(
      int RepId,
      int grupoRepId,
      int empregadorId)
    {
      List<Empregado> empregadoList = (List<Empregado>) null;
      try
      {
        empregadoList = new GrupoRepXempregadosDAO().PesquisarEmpregadosComInstrucaoInclusaoDoGrupoAT(RepId, grupoRepId, empregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        ex.Data.Add((object) "mensagem2", (object) message);
        throw;
      }
      return empregadoList;
    }

    public static List<Empregado> PesquisarEmpregadosComInstrucaoExclusaoDoGrupo(
      int RepId,
      int empregadorId)
    {
      List<Empregado> empregadoList = (List<Empregado>) null;
      try
      {
        empregadoList = new GrupoRepXempregadosDAO().PesquisarEmpregadosComInstrucaoExclusaoDoGrupo(RepId, empregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        ex.Data.Add((object) "mensagem2", (object) message);
        throw;
      }
      return empregadoList;
    }

    public static int ExcluirEmpregadoSemGrupoSenior(int empregadoID)
    {
      int num = 0;
      try
      {
        num = new GrupoRepXempregadosDAO().ExcluirEmpregadoSemGrupoSenior(empregadoID);
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

    public static int ExcluirTemplatesDoEmpregado(Empregado empregado)
    {
      int num = 0;
      try
      {
        TemplateBio templateBio = new TemplateBio();
        TemplatesBio _templateEnt = new TemplatesBio();
        _templateEnt.EmpregadoID = empregado.EmpregadoId;
        _templateEnt.EmpregadorID = empregado.EmpregadorId;
        num += templateBio.ExcluirTemplate(_templateEnt, "TemplatesNitgen");
        num += templateBio.ExcluirTemplate(_templateEnt, "TemplatesCama");
        num += templateBio.ExcluirTemplate(_templateEnt, "TemplatesSagem");
      }
      catch (Exception ex)
      {
      }
      return num;
    }

    public static int ExcluirEmpregadoDoGrupoRepInativo(int empregadoID)
    {
      int num = 0;
      try
      {
        num = new GrupoRepXempregadosDAO().ExcluirEmpregadoDoGrupoRepInativo(empregadoID);
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
  }
}
