// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GrupoRepBI
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.ObjectModel;
using System.Data;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public static class GrupoRepBI
  {
    public static int InserirGrupoRep(GrupoRep grupoRepEnt)
    {
      int num = 0;
      try
      {
        num = new GrupoRepDAO().InserirGrupoRep(grupoRepEnt);
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

    public static int InserirGrupoRepSenior(GrupoRep grupoRepEnt)
    {
      int num = 0;
      try
      {
        num = new GrupoRepDAO().InserirGrupoRepSenior(grupoRepEnt);
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

    public static int InserirGrupoRep(SortableBindingList<GrupoRep> lstGrupoRepEnt)
    {
      int num = 0;
      try
      {
        num = new GrupoRepDAO().InserirGrupoRepComID(lstGrupoRepEnt);
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

    public static GrupoRep PesquisarPorGrupoId(int grupoRepId)
    {
      GrupoRep grupoRep = (GrupoRep) null;
      try
      {
        grupoRep = new GrupoRepDAO().PesquisarPorGrupoId(grupoRepId);
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
      return grupoRep;
    }

    public static SortableBindingList<GrupoRep> PesquisarGrupoPorEmpregador(
      int empregadorId)
    {
      SortableBindingList<GrupoRep> sortableBindingList = new SortableBindingList<GrupoRep>();
      try
      {
        sortableBindingList = new GrupoRepDAO().PesquisarGrupoPorEmpregador(empregadorId);
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

    public static SortableBindingList<GrupoRep> PesquisarTodosGrupos()
    {
      SortableBindingList<GrupoRep> sortableBindingList = (SortableBindingList<GrupoRep>) null;
      try
      {
        sortableBindingList = new GrupoRepDAO().PesquisarTodosGrupos();
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

    public static int AtualizaGrupoRep(GrupoRep grupoEnt)
    {
      int num = 0;
      try
      {
        num = new GrupoRepDAO().AtualizaGrupoRep(grupoEnt);
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

    public static int ExcluirGrupoRep(int grupoId)
    {
      int num = 0;
      try
      {
        RepBase repBase = new RepBase();
        foreach (RepBase RepBaseEnt in (Collection<RepBase>) repBase.PesquisarRepsPorGrupo(grupoId))
        {
          RepBaseEnt.grupoId = 0;
          repBase.AtualizarRep(RepBaseEnt);
        }
        new GrupoRepXempregadosDAO().ExcluirTodosEmpregadosDoGrupo(grupoId);
        num = new GrupoRepDAO().ExcluirGrupoRep(grupoId);
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

    public static int ExcluirGrupoRepPorEmpregador(int empregadorId)
    {
      int num = 0;
      try
      {
        foreach (GrupoRep grupoRep in (Collection<GrupoRep>) new GrupoRepDAO().PesquisarGrupoPorEmpregador(empregadorId))
          GrupoRepBI.ExcluirGrupoRep(grupoRep.grupoID);
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

    public static bool ExisteNomeCadastrado(string nomeGrupo, int empregadorId)
    {
      bool flag = false;
      try
      {
        flag = new GrupoRepDAO().ExisteNomeCadastrado(nomeGrupo, empregadorId);
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

    public static int PesquisarUltimoGrupoIdPorEmpregador(string nomeGrupoRep)
    {
      int num = 0;
      try
      {
        num = new GrupoRepDAO().PesquisarUltimoGrupoIdPorEmpregador(nomeGrupoRep, CommandType.Text);
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

    public static int AtualizaGrupoRepSenior(GrupoRep grupoEnt)
    {
      int num = 0;
      try
      {
        num = new GrupoRepDAO().AtualizaGrupoRepSenior(grupoEnt);
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
