// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.FormatoCartao
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

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class FormatoCartao
  {
    public List<FormatoCartao> PesquisarFormatosCartao()
    {
      List<FormatoCartao> formatoCartaoList = (List<FormatoCartao>) null;
      try
      {
        formatoCartaoList = new FormatoCartao().PesquisarFormatosCartao();
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
      return formatoCartaoList;
    }

    public List<FormatoCartao> PesquisarFormatosCartaoBarrasRepPlus(
      int configuracaoId)
    {
      List<FormatoCartao> formatoCartaoList = (List<FormatoCartao>) null;
      try
      {
        formatoCartaoList = new FormatoCartao().PesquisarFormatosCartaoBarrasRepPlus(configuracaoId);
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
      return formatoCartaoList;
    }

    public List<FormatoCartao> PesquisarFormatosCartaoProxRepPlus(
      int configuracaoId)
    {
      List<FormatoCartao> formatoCartaoList = (List<FormatoCartao>) null;
      try
      {
        formatoCartaoList = new FormatoCartao().PesquisarFormatosCartaoProxRepPlus(configuracaoId);
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
      return formatoCartaoList;
    }

    public static FormatoCartao PesquisarLstFormatosCartaoBarrasRepPlus(
      int formatoId,
      int configuracaoId)
    {
      FormatoCartao formatoCartao = (FormatoCartao) null;
      try
      {
        formatoCartao = new FormatoCartao().PesquisarLstFormatosCartaoBarrasRepPlus(formatoId, configuracaoId);
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
      return formatoCartao;
    }

    public static FormatoCartao PesquisarLstFormatosCartaoProxRepPlus(
      int formatoId,
      int configuracaoId)
    {
      FormatoCartao formatoCartao = (FormatoCartao) null;
      try
      {
        formatoCartao = new FormatoCartao().PesquisarLstFormatosCartaoProxRepPlus(formatoId, configuracaoId);
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
      return formatoCartao;
    }

    public static FormatoCartao PesquisarLstFormatosCartao(
      int formatoId,
      int configuracaoId)
    {
      FormatoCartao formatoCartao = (FormatoCartao) null;
      try
      {
        formatoCartao = new FormatoCartao().PesquisarLstFormatosCartao(formatoId, configuracaoId);
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
      return formatoCartao;
    }

    public static List<FormatoCartao> PesquisarLstFormatosCartao()
    {
      List<FormatoCartao> formatoCartaoList = (List<FormatoCartao>) null;
      try
      {
        formatoCartaoList = new FormatoCartao().PesquisarFormatosCartao();
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
      return formatoCartaoList;
    }

    internal static FormatoCartao PesquisarFormatoCartaoByRepId(int repId)
    {
      FormatoCartao formatoCartao = (FormatoCartao) null;
      try
      {
        formatoCartao = new FormatoCartao().PesquisarFormatoCartaoByRepId(repId);
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
      return formatoCartao;
    }

    internal static FormatoCartao PesquisarFormatoCartaoBarrasByRepIdRepPlus(int repId)
    {
      FormatoCartao formatoCartao = (FormatoCartao) null;
      try
      {
        formatoCartao = new FormatoCartao().PesquisarFormatoCartaoBarrasByRepIdRepPlus(repId);
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
      return formatoCartao;
    }

    internal static FormatoCartao PesquisarFormatoCartaoProxByRepIdRepPlus(int repId)
    {
      FormatoCartao formatoCartao = (FormatoCartao) null;
      try
      {
        formatoCartao = new FormatoCartao().PesquisarFormatoCartaoProxByRepIdRepPlus(repId);
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
      return formatoCartao;
    }

    public int InserirListaFormatosCartao(List<FormatoCartao> listaFormatoCartao)
    {
      int num = 0;
      try
      {
        num = new FormatoCartao().InserirListaFormatosCartao(listaFormatoCartao);
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

    public static int AtualizarFormatoPadraoLivre(string NovoFormatoCartao)
    {
      int num = 0;
      try
      {
        num = new FormatoCartao().AtualizarFormatoPadraoLivre(NovoFormatoCartao);
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

    public static int AtualizarFormatoPadraoLivre(
      string NovoFormatoCartao,
      int formatoId,
      int configuracaoId)
    {
      int num = 0;
      try
      {
        num = new FormatoCartao().AtualizarFormatoPadraoLivre(NovoFormatoCartao, formatoId, configuracaoId);
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

    public static int AtualizarFormatoPadraoLivreRepPlus(
      string NovoFormatoCartao,
      int formatoId,
      int NumDigitosFixos,
      int ConfiguracaoID)
    {
      int num = 0;
      try
      {
        num = new FormatoCartao().AtualizarFormatoPadraoLivreRepPlus(NovoFormatoCartao, formatoId, NumDigitosFixos, ConfiguracaoID);
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

    public static int AtualizarFormatoPadraoAbatrackRepPlus(
      string NovoFormatoCartao,
      int formatoId,
      int NumDigitosFixos,
      int ConfiguracaoID)
    {
      int num = 0;
      try
      {
        num = new FormatoCartao().AtualizarFormatoPadraoAbatrackRepPlus(NovoFormatoCartao, formatoId, NumDigitosFixos, ConfiguracaoID);
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
