// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.TemplateBio
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class TemplateBio
  {
    private static string _connectionStringImportacao;

    public static string ConnectionStringImportacao
    {
      get => TemplateBio._connectionStringImportacao;
      set => TemplateBio._connectionStringImportacao = value;
    }

    public static event EventHandler<EventArgsCustomizados.NotificarImportacaoTemplate> OnNotificarImportacaoTemplate;

    public SortableBindingList<UsuarioBio> PesquisarTemplates()
    {
      SortableBindingList<UsuarioBio> sortableBindingList = (SortableBindingList<UsuarioBio>) null;
      try
      {
        sortableBindingList = new TemplateBio().PesquisarTemplates();
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

    public SortableBindingList<UsuarioBio> PesquisarTemplatesPorEmpregador(
      Empregador objEmpregador,
      int modeloBIO)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = (SortableBindingList<UsuarioBio>) null;
      try
      {
        sortableBindingList = new TemplateBio().PesquisarTemplatesPorEmpresa(objEmpregador, modeloBIO);
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

    public SortableBindingList<UsuarioBio> PesquisarTemplatesPorEmpregadorSenior(
      Empregador objEmpregador,
      int modeloBIO)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = (SortableBindingList<UsuarioBio>) null;
      try
      {
        sortableBindingList = new TemplateBio().PesquisarTemplatesPorEmpresaSenior(objEmpregador, modeloBIO);
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

    public DataTable PesquisarTemplatesPorEmpregadorDataSource(
      Empregador objEmpregador,
      int modeloBIO)
    {
      DataTable dataTable = (DataTable) null;
      try
      {
        dataTable = new TemplateBio().PesquisarTemplatesPorEmpresaDataSource(objEmpregador, modeloBIO);
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
      return dataTable;
    }

    public DataTable PesquisarTemplatesPorRepDataSourceSenior(int repIdGrupo)
    {
      DataTable dataTable = (DataTable) null;
      try
      {
        dataTable = new TemplateBio().PesquisarTemplatesPorRepSourceSenior(repIdGrupo);
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
      return dataTable;
    }

    public SortableBindingList<UsuarioBio> PesquisarTemplatesPorEmpregadorRepPlus(
      Empregador objEmpregador)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = (SortableBindingList<UsuarioBio>) null;
      try
      {
        sortableBindingList = new TemplateBio().PesquisarTemplatesPorEmpresaRepPlus(objEmpregador);
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

    public SortableBindingList<UsuarioBio> PesquisarTemplatesCAMAPorEmpregadorRepPlus(
      Empregador objEmpregador)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = (SortableBindingList<UsuarioBio>) null;
      try
      {
        sortableBindingList = new TemplateBio().PesquisarTemplatesCAMAPorEmpresaRepPlus(objEmpregador);
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

    public SortableBindingList<UsuarioBio> PesquisarTemplatesSAGEMPorEmpregadorRepPlus(
      Empregador objEmpregador)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = (SortableBindingList<UsuarioBio>) null;
      try
      {
        sortableBindingList = new TemplateBio().PesquisarTemplatesSAGEMPorEmpresaRepPlus(objEmpregador);
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

    public TemplatesBio PesquisarTemplatesSemEmpregado(Empregado empregado)
    {
      TemplatesBio templatesBio = new TemplatesBio();
      try
      {
        templatesBio = new TemplateBio().PesquisarTemplateSemEmpregado(empregado, "TemplatesNitgen");
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return templatesBio;
    }

    public TemplatesBio PesquisarTemplatesSemEmpregadoCAMA(Empregado empregado)
    {
      TemplatesBio templatesBio = new TemplatesBio();
      try
      {
        templatesBio = new TemplateBio().PesquisarTemplateSemEmpregado(empregado, "TemplatesCama");
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return templatesBio;
    }

    public DataTable PesquisarTemplatesPorEmpregadorRepPlusDataSource(
      Empregador objEmpregador)
    {
      DataTable dataTable = (DataTable) null;
      try
      {
        dataTable = new TemplateBio().PesquisarTemplatesPorEmpresaRepPlusDataSource(objEmpregador);
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
      return dataTable;
    }

    public DataTable PesquisarTemplatesPorRepPlusDataSourceSenior(int RepGrupoId)
    {
      DataTable dataTable = (DataTable) null;
      try
      {
        dataTable = new TemplateBio().PesquisarTemplatesPorRepPlusDataSourceSenior(RepGrupoId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return dataTable;
    }

    public DataTable PesquisarTemplatesCAMAPorEmpregadorRepPlusDataSource(
      Empregador objEmpregador)
    {
      DataTable dataTable = (DataTable) null;
      try
      {
        dataTable = new TemplateBio().PesquisarTemplatesCAMAPorEmpresaRepPlusDataSource(objEmpregador);
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
      return dataTable;
    }

    public DataTable PesquisarTemplatesCAMAPorRepPlusDataSourceSenior(int GrupoRepID)
    {
      DataTable dataTable = (DataTable) null;
      try
      {
        dataTable = new TemplateBio().PesquisarTemplatesCAMAPorRepPlusDataSourceSenior(GrupoRepID);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return dataTable;
    }

    public DataTable PesquisarTemplatesSagemPorRepPlusDataSourceSenior(int GrupoRepID)
    {
      DataTable dataTable = (DataTable) null;
      try
      {
        dataTable = new TemplateBio().PesquisarTemplatesSagemPorRepPlusDataSourceSenior(GrupoRepID);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return dataTable;
    }

    public DataTable PesquisarTemplatesSagemPorEmpresaRepPlusDataSource(
      Empregador objEmpregador,
      int grupoId)
    {
      DataTable dataTable = (DataTable) null;
      try
      {
        dataTable = new TemplateBio().PesquisarTemplatesSagemPorEmpresaRepPlusDataSource(objEmpregador, grupoId);
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
      return dataTable;
    }

    public int InserirTemplatesBio(List<TemplatesBio> templatesBio)
    {
      int num = 0;
      try
      {
        foreach (TemplatesBio templateBio1 in templatesBio)
        {
          TemplateBio templateBio2 = new TemplateBio();
          num += templateBio2.InserirTemplateBio(templateBio1, "TemplatesNitgen");
        }
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

    public int InserirTemplatesBioCAMA(List<TemplatesBio> templatesBio)
    {
      int num = 0;
      try
      {
        foreach (TemplatesBio templateBio1 in templatesBio)
        {
          TemplateBio templateBio2 = new TemplateBio();
          num += templateBio2.InserirTemplateBioCAMA(templateBio1);
        }
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

    public int InserirTemplatesBioSAGEM(List<TemplatesBio> templatesBio)
    {
      int num = 0;
      try
      {
        foreach (TemplatesBio templateBio1 in templatesBio)
        {
          TemplateBio templateBio2 = new TemplateBio();
          num += templateBio2.InserirTemplateBioSagem(templateBio1);
        }
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

    public int VincularTemplateCartao(TemplatesBio templateBio)
    {
      int num = 0;
      try
      {
        num = new TemplateBio().VincularTemplateSemEmpregado(templateBio, "TemplatesNitgen");
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

    public int VincularTemplateCartaoCAMA(TemplatesBio templateBio)
    {
      int num = 0;
      try
      {
        num = new TemplateBio().VincularTemplateSemEmpregado(templateBio, "TemplatesCama");
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

    public List<TemplatesBio> PesquisarTemplatesPorEmpregado(Empregado empregadoEnt)
    {
      List<TemplatesBio> templatesBioList = new List<TemplatesBio>();
      try
      {
        templatesBioList = new TemplateBio().PesquisarTemplatesPorEmpregado(empregadoEnt);
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
      return templatesBioList;
    }

    public bool EmpregadoTemTamplates(Empregado empregadoEnt) => this.PesquisarTemplatesPorEmpregado(empregadoEnt).Count > 0;

    public bool EmpregadoTemTamplatesCAMA(Empregado empregadoEnt) => this.PesquisarTemplatesCAMAPorEmpregado(empregadoEnt).Count > 0;

    public List<TemplatesBio> PesquisarTemplatesCAMAPorEmpregado(
      Empregado empregadoEnt)
    {
      List<TemplatesBio> templatesBioList = new List<TemplatesBio>();
      try
      {
        templatesBioList = new TemplateBio().PesquisarTemplatesCAMAPorEmpregado(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return templatesBioList;
    }

    public List<TemplatesBio> PesquisarTemplatesSAGEMPorEmpregado(
      Empregado empregadoEnt)
    {
      List<TemplatesBio> templatesBioList = new List<TemplatesBio>();
      try
      {
        templatesBioList = new TemplateBio().PesquisarTemplatesSAGEMPorEmpregado(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return templatesBioList;
    }

    public int ExcluirTemplates(List<TemplatesBio> _templatesEnt)
    {
      int num = 0;
      try
      {
        foreach (TemplatesBio _templateEnt in _templatesEnt)
        {
          TemplateBio templateBio = new TemplateBio();
          num += templateBio.ExcluirTemplate(_templateEnt, "TemplatesNitgen");
        }
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int ExcluirTemplates3030(List<TemplatesBio> _templatesEnt)
    {
      int num = 0;
      try
      {
        foreach (TemplatesBio _templateEnt in _templatesEnt)
        {
          TemplateBio templateBio = new TemplateBio();
          num += templateBio.ExcluirTemplate3030(_templateEnt, "TemplatesNitgen");
        }
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int ExcluirTemplatesCAMA(List<TemplatesBio> _templatesEnt)
    {
      int num = 0;
      try
      {
        foreach (TemplatesBio _templateEnt in _templatesEnt)
        {
          TemplateBio templateBio = new TemplateBio();
          num += templateBio.ExcluirTemplateCAMA(_templateEnt);
        }
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

    public int ExcluirTemplatesSagem(List<TemplatesBio> _templatesEnt)
    {
      int num = 0;
      try
      {
        foreach (TemplatesBio _templateEnt in _templatesEnt)
        {
          TemplateBio templateBio = new TemplateBio();
          num += templateBio.ExcluirTemplateSagem(_templateEnt);
        }
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

    public int ExcluirTemplateIdBiometricoDuplicado(long IdBiometrico)
    {
      int num = 0;
      try
      {
        num = new TemplateBio().ExcluirTemplateIdBiometricoDuplicado(IdBiometrico);
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

    public static List<TemplatesBio> RecuperarTemplatesBIO(
      string _caminhoDB,
      Constantes.PRODUTO _tipoProduto,
      int empregadorId)
    {
      List<TemplatesBio> templatesBioList = (List<TemplatesBio>) null;
      switch (_tipoProduto)
      {
        case Constantes.PRODUTO.GERENCIADOR_INNERS5:
          if (TemplateBio.VerificaConexaoBD(_caminhoDB, _tipoProduto))
          {
            try
            {
              templatesBioList = ImportadorTemplatesBio.RecTemplatesGerInner5(_caminhoDB, empregadorId);
              break;
            }
            catch
            {
              break;
            }
          }
          else
            break;
        case Constantes.PRODUTO.GERENCIADOR_INNER_PRO:
          if (TemplateBio.VerificaConexaoBD(_caminhoDB, _tipoProduto))
          {
            try
            {
              templatesBioList = ImportadorTemplatesBio.RecTemplatesGerInnerPRO(_caminhoDB, empregadorId);
              break;
            }
            catch (Exception ex)
            {
              break;
            }
          }
          else
            break;
      }
      return templatesBioList;
    }

    public static List<TemplatesBio> RecuperarTemplatesBIOLC(
      string _caminhoDB,
      Constantes.PRODUTO _tipoProduto,
      int empregadorId)
    {
      List<TemplatesBio> templatesBioList = (List<TemplatesBio>) null;
      if (_tipoProduto == Constantes.PRODUTO.GERENCIADOR_INNERS5)
      {
        if (TemplateBio.VerificaConexaoBD(_caminhoDB, _tipoProduto))
        {
          try
          {
            templatesBioList = ImportadorTemplatesBio.RecTemplatesLCGerInner5(_caminhoDB, empregadorId);
          }
          catch
          {
          }
        }
      }
      return templatesBioList;
    }

    private static bool VerificaConexaoBD(string _caminhoDB, Constantes.PRODUTO _tipoProduto)
    {
      bool ret = false;
      string conString = string.Empty;
      DbConnection con = (DbConnection) null;
      switch (_tipoProduto)
      {
        case Constantes.PRODUTO.GERENCIADOR_INNERS5:
          ret = ImportadorTemplatesBio.TestaConexaoGerInner5(_caminhoDB, ret, out conString, out con);
          TemplateBio._connectionStringImportacao = conString;
          break;
        case Constantes.PRODUTO.GERENCIADOR_INNER_PRO:
          ret = ImportadorTemplatesBio.TestaConexaoGerInnerPRO(_caminhoDB, ret, out conString, out con);
          TemplateBio._connectionStringImportacao = conString;
          break;
      }
      return ret;
    }

    public static int Asc(char String)
    {
      int int32 = Convert.ToInt32(String);
      if (int32 < 128)
        return int32;
      try
      {
        Encoding ascii = Encoding.ASCII;
        char[] chars = new char[1]{ String };
        if (ascii.IsSingleByte)
        {
          byte[] bytes = new byte[1];
          ascii.GetBytes(chars, 0, 1, bytes, 0);
          return (int) bytes[0];
        }
        byte[] bytes1 = new byte[2];
        if (ascii.GetBytes(chars, 0, 1, bytes1, 0) == 1)
          return (int) bytes1[0];
        if (BitConverter.IsLittleEndian)
        {
          byte num = bytes1[0];
          bytes1[0] = bytes1[1];
          bytes1[1] = num;
        }
        return (int) BitConverter.ToInt16(bytes1, 0);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int InserirTemplatesBIO(List<TemplatesBio> _templatesEnt, string _tabela)
    {
      int num = 0;
      try
      {
        TemplateBio templateBio = new TemplateBio();
        templateBio.OnNotificarImportacaoTemplate += new EventHandler<EventArgsCustomizados.NotificarImportacaoTemplate>(TemplateBio.templateBio_OnNotificarImportacaoTemplate);
        foreach (TemplatesBio templatesBio in _templatesEnt)
        {
          if (templatesBio.EmpregadoID != 0 && templatesBio.IdBiometrico != 0L)
          {
            num += templateBio.ExcluirTemplateIdBiometrico(templatesBio, _tabela);
            num += templateBio.ExcluirTemplate(templatesBio, _tabela);
          }
          else if (templatesBio.EmpregadoID == 0)
            num += templateBio.ExcluirTemplateIdBiometrico(templatesBio, _tabela);
          else
            num += templateBio.ExcluirTemplate(templatesBio, _tabela);
          Thread.Sleep(2);
          num += templateBio.InserirTemplateBio(templatesBio, _tabela);
          Application.DoEvents();
        }
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

    private static void templateBio_OnNotificarImportacaoTemplate(
      object sender,
      EventArgsCustomizados.NotificarImportacaoTemplate e)
    {
      if (TemplateBio.OnNotificarImportacaoTemplate == null)
        return;
      TemplateBio.OnNotificarImportacaoTemplate((object) null, e);
    }

    public int InserirTemplatesBioNitgenSenior(List<TemplatesBio> templatesBio)
    {
      int num = 0;
      try
      {
        foreach (TemplatesBio templateBio1 in templatesBio)
        {
          TemplateBio templateBio2 = new TemplateBio();
          num += templateBio2.InserirTemplateBioNitgenSenior(templateBio1);
        }
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

    public int InserirTemplatesCamaBioSenior(List<TemplatesBio> templatesBio)
    {
      int num = 0;
      try
      {
        foreach (TemplatesBio templateBio1 in templatesBio)
        {
          TemplateBio templateBio2 = new TemplateBio();
          num += templateBio2.InserirTemplateCamaBioSenior(templateBio1);
        }
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

    public int InserirTemplatesSagemBioSenior(List<TemplatesBio> templatesBio)
    {
      int num = 0;
      try
      {
        foreach (TemplatesBio templateBio1 in templatesBio)
        {
          TemplateBio templateBio2 = new TemplateBio();
          templateBio1.Template1 = templateBio1.Template1.PadRight(512, '0');
          templateBio1.Template2 = templateBio1.Template2.PadRight(512, '0');
          num += templateBio2.InserirTemplateSagemBioSenior(templateBio1);
        }
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
