// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.ArquivoBilhete
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class ArquivoBilhete
  {
    private int _arquivoBilheteId;
    private int _arquivo2BilheteId;
    private int _arquivo3BilheteId;
    private string _localBilhete;
    private string _localBilhete2;
    private string _localBilhete3;
    private int _tipoArquivo;
    private string _formato;

    public int ArquivoBilheteId
    {
      get => this._arquivoBilheteId;
      set => this._arquivoBilheteId = value;
    }

    public int Arquivo2BilheteId
    {
      get => this._arquivo2BilheteId;
      set => this._arquivo2BilheteId = value;
    }

    public int Arquivo3BilheteId
    {
      get => this._arquivo3BilheteId;
      set => this._arquivo3BilheteId = value;
    }

    public string LocalBilhete
    {
      get => this._localBilhete;
      set => this._localBilhete = value;
    }

    public string LocalBilhete2
    {
      get => this._localBilhete2;
      set => this._localBilhete2 = value;
    }

    public string LocalBilhete3
    {
      get => this._localBilhete3;
      set => this._localBilhete3 = value;
    }

    public int TipoArquivo
    {
      get => this._tipoArquivo;
      set => this._tipoArquivo = value;
    }

    public string Formato
    {
      get => this._formato;
      set => this._formato = value;
    }

    public int VerificarSeExisteArquivo(ArquivoBilhete arquivoBilheteEnt)
    {
      int num = 0;
      try
      {
        if (arquivoBilheteEnt.tipoExportacao == Constantes.TIPO_ARQUIVO.AFD || arquivoBilheteEnt.tipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
        {
          ArquivoBilhete arquivoBilhete = new ArquivoBilhete();
          num = arquivoBilhete.VerificarSeExisteArquivoAFD(arquivoBilheteEnt);
          num += arquivoBilhete.VerificarSeExisteArquivoAFDMesmoDiretorio(arquivoBilheteEnt);
        }
        else
          num = arquivoBilheteEnt.tipoExportacao != Constantes.TIPO_ARQUIVO.NUVEM ? new ArquivoBilhete().VerificarSeExisteArquivoCustom(arquivoBilheteEnt) : new ArquivoBilhete().VerificarSeExisteArquivoNuvem(arquivoBilheteEnt);
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

    public bool VerificarSeExisteTipoArquivo(ArquivoBilhete arquivoBilheteEnt)
    {
      bool flag = false;
      try
      {
        flag = new ArquivoBilhete().VerificarSeExisteTipoArquivo(arquivoBilheteEnt);
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

    public int AtualizarArquivoAFDPorEmpregador(ArquivoBilhete arquivoBilheteEnt)
    {
      int num = 0;
      try
      {
        num = new ArquivoBilhete().AtualizarArquivoAFDPorEmpregador(arquivoBilheteEnt);
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

    public SortableBindingList<ArquivoBilhete> PesquisarArquivoAFDPorEmpregador(
      int empregadorId)
    {
      SortableBindingList<ArquivoBilhete> sortableBindingList = (SortableBindingList<ArquivoBilhete>) null;
      try
      {
        sortableBindingList = new ArquivoBilhete().PesquisarArquivosAFDPorEmpregador(empregadorId);
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

    public SortableBindingList<ArquivoBilhete> PesquisarArquivosCustomizado()
    {
      SortableBindingList<ArquivoBilhete> sortableBindingList = (SortableBindingList<ArquivoBilhete>) null;
      try
      {
        sortableBindingList = new ArquivoBilhete().PesquisarArquivosCustomizado();
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

    public int VerificarPeloMenosUmaComunicacaoNuvem()
    {
      int num = 0;
      try
      {
        num = new ArquivoBilhete().VerificarPeloMenosUmaComunicacaoNuvem();
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

    public SortableBindingList<ArquivoBilhete> PesquisarArquivoAFDPorREP(
      int repId)
    {
      SortableBindingList<ArquivoBilhete> sortableBindingList = (SortableBindingList<ArquivoBilhete>) null;
      try
      {
        sortableBindingList = new ArquivoBilhete().PesquisarArquivosAFDPorREP(repId);
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

    public SortableBindingList<ArquivoBilhete> PesquisarArquivosAFDsREPs()
    {
      SortableBindingList<ArquivoBilhete> sortableBindingList = (SortableBindingList<ArquivoBilhete>) null;
      try
      {
        sortableBindingList = new ArquivoBilhete().PesquisarArquivosAFDsREPs();
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

    public DataTable PesquisarRepsDoArquivo(ArquivoBilhete arquivoBilhete)
    {
      try
      {
        return new ArquivoBilhete().PesquisarRepsDoArquivo(arquivoBilhete);
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
      return (DataTable) null;
    }

    public int ExcluirArquivoAFDPorEmpregador(ArquivoBilhete _arquivoEnt)
    {
      int num = 0;
      try
      {
        num = new ArquivoBilhete().ExcluirArquivoAFDPorEmpregador(_arquivoEnt);
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

    public int ExcluirRepsDoArquivo(ArquivoBilhete _arquivoEnt)
    {
      int num = 0;
      try
      {
        num = new ArquivoBilhete().ExcluirRepsDoArquivo(_arquivoEnt);
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

    public int InserirArquivoBilheteAFDPorEmpregador(ArquivoBilhete _arquivoEnt)
    {
      int num = 0;
      try
      {
        num = new ArquivoBilhete().InserirArquivoBilheteAFDPorEmpregador(_arquivoEnt);
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

    public int InserirRepsDoArquivo(ArquivoBilhete _arquivoEnt, int repId)
    {
      int num = 0;
      try
      {
        num = new ArquivoBilhete().InserirRepsDoArquivo(_arquivoEnt, repId);
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

    public bool VerificaAssociacaoREP_AFDRep()
    {
      List<RepBase> repBaseList = new List<RepBase>();
      foreach (RepBase pesquisarRep in new RepBase().PesquisarReps())
      {
        if (this.PesquisarArquivoAFDPorREP(pesquisarRep.RepId).Count <= 0)
          return false;
      }
      return true;
    }

    public bool UltimoArquivoRep(ArquivoBilhete _arquivoEnt) => this.PesquisarArquivoAFDPorREP(new RepBase().PesquisarRepPorID(_arquivoEnt.repID).RepId).Count == 1;

    public int InserirArquivoBilhetePorEmpregador(ArquivoBilhete arquivoBilheteEnt)
    {
      int num = 0;
      try
      {
        num = new ArquivoBilhete().InserirArquivoBilhetePorEmpregador(arquivoBilheteEnt);
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

    public int AtualizarArquivosBilhete(ArquivoBilhete arquivoBilheteEnt)
    {
      int num = 0;
      try
      {
        num = new ArquivoBilhete().AtualizarArquivoBilhete(arquivoBilheteEnt);
        this._tipoArquivo = arquivoBilheteEnt.TipoArquivo;
        this._localBilhete = arquivoBilheteEnt.LocalBilhete;
        this._localBilhete2 = arquivoBilheteEnt.LocalBilhete2;
        this._localBilhete3 = arquivoBilheteEnt.LocalBilhete3;
        this._arquivoBilheteId = arquivoBilheteEnt.ArquivoBilheteId;
        this._arquivo2BilheteId = arquivoBilheteEnt.Arquivo2BilheteId;
        this._arquivo3BilheteId = arquivoBilheteEnt.Arquivo3BilheteId;
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

    public int AtualizarArquivosBilhetePorEmpregador(ArquivoBilhete arquivoBilheteEnt)
    {
      int num = 0;
      try
      {
        num = new ArquivoBilhete().AtualizarArquivoBilhetePorEmpregador(arquivoBilheteEnt);
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

    public int AtualizarFormatoArquivoBilheteCustomiz(ArquivoBilhete arquivoBilheteEnt)
    {
      int num = 0;
      try
      {
        num = new ArquivoBilhete().AtualizarFormatoArquivoBilheteCustomiz(arquivoBilheteEnt);
        this._formato = arquivoBilheteEnt.Formato;
        this._arquivoBilheteId = arquivoBilheteEnt.ArquivoBilheteId;
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

    public ArquivoBilhete PesquisarArquivoBilhete()
    {
      ArquivoBilhete arquivoBilhete = (ArquivoBilhete) null;
      try
      {
        arquivoBilhete = new ArquivoBilhete().PesquisarArquivoBilhete();
        this._tipoArquivo = arquivoBilhete.TipoArquivo;
        this._localBilhete = arquivoBilhete.LocalBilhete;
        this._formato = arquivoBilhete.Formato;
        this._arquivoBilheteId = arquivoBilhete.ArquivoBilheteId;
        this._arquivo2BilheteId = arquivoBilhete.Arquivo2BilheteId;
        this._arquivo3BilheteId = arquivoBilhete.Arquivo3BilheteId;
        this._localBilhete2 = arquivoBilhete.LocalBilhete2;
        this._localBilhete3 = arquivoBilhete.LocalBilhete3;
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
      return arquivoBilhete;
    }

    public SortableBindingList<ArquivoBilhete> PesquisarArquivoBilhetePorEmpregador(
      int empregadorId)
    {
      SortableBindingList<ArquivoBilhete> sortableBindingList = (SortableBindingList<ArquivoBilhete>) null;
      try
      {
        sortableBindingList = new ArquivoBilhete().PesquisarArquivosPorEmpregador(empregadorId);
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

    public int ExcluirArquivoPorEmpregador(ArquivoBilhete arquivoEnt)
    {
      int num = 0;
      try
      {
        num = new ArquivoBilhete().ExcluirArquivoPorEmpregador(arquivoEnt);
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

    internal void AssociarEntidadeArquivoBilhete(ArquivoBilhete arquivo)
    {
      try
      {
        this.ArquivoBilheteId = arquivo.ArquivoBilheteId;
        this.Arquivo2BilheteId = arquivo.Arquivo2BilheteId;
        this.Arquivo3BilheteId = arquivo.Arquivo3BilheteId;
        this.Formato = arquivo.Formato == null ? string.Empty : arquivo.Formato;
        this.LocalBilhete = arquivo.LocalBilhete == null ? string.Empty : arquivo.LocalBilhete;
        this.LocalBilhete2 = arquivo.LocalBilhete2 == null ? string.Empty : arquivo.LocalBilhete2;
        this.LocalBilhete3 = arquivo.LocalBilhete3 == null ? string.Empty : arquivo.LocalBilhete3;
        this.TipoArquivo = arquivo.TipoArquivo;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void ProcessarArquivoBilhete(string marcacao)
    {
      string str1 = "";
      try
      {
        switch (this._tipoArquivo)
        {
          case 1:
            str1 = marcacao.Remove(18, 3);
            break;
          case 2:
            str1 = marcacao.Remove(18, 3);
            str1 = this.Criptografar(str1);
            break;
          case 3:
            string[] strArray1 = marcacao.Split(' ');
            if (this._formato.Length > 0)
            {
              if (this._formato.Substring(this._formato.Length - 1, 1).Equals(";"))
                this._formato = this._formato.Remove(this._formato.Length - 1);
              if (this._formato.Substring(0, 1).Equals(";"))
                this._formato = this._formato.Remove(0, 1);
            }
            string[] strArray2 = this._formato.Split(';');
            for (int index1 = 0; index1 <= strArray2.Length - 1; ++index1)
            {
              if (!strArray2[index1].Equals("\r\n") && strArray2[index1].Length > 0)
              {
                if (strArray2[index1].Replace("\r\n", "").Substring(0, 1).Equals("["))
                {
                  str1 += strArray2[index1];
                  str1 = str1.Remove(str1.IndexOf('['), 1);
                  str1 = str1.Remove(str1.IndexOf(']'), 1);
                  str1 = str1.Replace("\r\n", "");
                }
                if ((strArray2[index1].IndexOf("#") > 0 || strArray2[index1].IndexOf("-") > 0) && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[")
                {
                  string str2 = "";
                  string str3 = strArray2[index1].Replace("\r\n", "");
                  if (str3.Substring(0, 1).Equals("{"))
                    str3 = str3.Remove(0, 1);
                  if (str3.Substring(str3.Length - 1, 1).Equals("}"))
                    str3 = str3.Remove(str3.Length - 1, 1);
                  for (int startIndex = 0; startIndex <= str3.Length - 1; ++startIndex)
                  {
                    if (!str3.Substring(startIndex, 1).Equals("-"))
                      str2 += strArray1[3].Substring(startIndex, 1);
                  }
                  str1 += str2.Trim();
                }
                if (strArray2[index1].IndexOf("AA") > 0 && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[" && strArray2[index1].Replace("\r\n", "").Length == 4)
                  str1 += strArray1[1].Substring(6, 2);
                if (strArray2[index1].IndexOf("AAAA") > 0 && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[" && strArray2[index1].Replace("\r\n", "").Length == 6)
                {
                  string str4 = DateTime.Now.Year.ToString();
                  str1 = str1 + str4.Substring(0, 2) + strArray1[1].Substring(6, 2);
                }
                if (strArray2[index1].IndexOf("DD") > 0 && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[")
                  str1 += strArray1[1].Substring(0, 2);
                if (strArray2[index1].IndexOf("MM") > 0 && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[")
                  str1 += strArray1[1].Substring(3, 2);
                if (strArray2[index1].IndexOf("NN") > 0 && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[")
                  str1 += strArray1[2].Substring(3, 2);
                if (strArray2[index1].IndexOf("P") > 0 && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[")
                  str1 += strArray1[5].ToString();
                if (strArray2[index1].IndexOf("I") > 0 && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[")
                {
                  string str5 = "";
                  int num = 1;
                  for (int index2 = strArray2[index1].Length - 3; index2 >= 0; --index2)
                  {
                    int startIndex = strArray1[4].Length - num;
                    str5 = strArray1[4].Substring(startIndex, 1) + str5;
                    ++num;
                  }
                  str1 += str5;
                }
                if (strArray2[index1].IndexOf("EE") > 0 && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[")
                  str1 += strArray1[2].Substring(6, 2);
                if (strArray2[index1].IndexOf("S") > 0 && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[")
                {
                  long num1 = 0;
                  FileStream fileStream = (FileStream) null;
                  StreamWriter streamWriter = (StreamWriter) null;
                  StreamReader streamReader = (StreamReader) null;
                  int totalWidth = strArray2[index1].IndexOf('}') + -(strArray2[index1].IndexOf('{') + 1);
                  try
                  {
                    fileStream = new FileStream(this._localBilhete, FileMode.OpenOrCreate);
                    streamReader = new StreamReader((Stream) fileStream);
                    while (streamReader.ReadLine() != null)
                    {
                      ++num1;
                      switch (totalWidth)
                      {
                        case 1:
                          if (num1 == 9L)
                          {
                            num1 = 0L;
                            continue;
                          }
                          continue;
                        case 2:
                          if (num1 == 99L)
                          {
                            num1 = 0L;
                            continue;
                          }
                          continue;
                        case 3:
                          if (num1 == 999L)
                          {
                            num1 = 0L;
                            continue;
                          }
                          continue;
                        case 4:
                          if (num1 == 9999L)
                          {
                            num1 = 0L;
                            continue;
                          }
                          continue;
                        case 5:
                          if (num1 == 99999L)
                          {
                            num1 = 0L;
                            continue;
                          }
                          continue;
                        default:
                          continue;
                      }
                    }
                    fileStream.Close();
                    fileStream = new FileStream(this._localBilhete, FileMode.OpenOrCreate);
                    streamWriter = new StreamWriter((Stream) fileStream);
                    streamWriter.Flush();
                    long num2 = num1 + 1L;
                    string str6 = "";
                    string str7 = string.Join("", strArray2).Replace("\r\n", "");
                    int num3 = 1;
                    for (int index3 = str7.Length - 3; index3 >= 0 && num2.ToString().Length >= num3; --index3)
                    {
                      int startIndex = num2.ToString().Length - num3;
                      str6 = num2.ToString().Substring(startIndex, 1) + str6;
                      ++num3;
                    }
                    long int64 = Convert.ToInt64(str6);
                    str1 += int64.ToString().PadLeft(totalWidth, '0');
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
                  finally
                  {
                    streamReader.Close();
                    streamWriter.Flush();
                    streamWriter.Close();
                    fileStream.Close();
                  }
                }
                if (strArray2[index1].IndexOf("TTT") > 0 && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[")
                  str1 += strArray1[0];
                if (strArray2[index1].IndexOf("HH") > 0 && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[")
                  str1 += strArray1[2].Substring(0, 2);
                if (strArray2[index1].IndexOf("U") > 0 && strArray2[index1].Replace("\r\n", "").Substring(0, 1) != "[")
                {
                  string str8 = "";
                  int num = 1;
                  for (int index4 = strArray2[index1].Length - 3; index4 >= 0; --index4)
                  {
                    int startIndex = strArray1[5].Length - num;
                    str8 = strArray1[5].Substring(startIndex, 1) + str8;
                    ++num;
                  }
                  str1 += str8.Trim();
                }
              }
            }
            break;
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
      this.SalvarEmArquivoRecursivo(str1, 1);
    }

    public void SalvarEmArquivo(string marcacao)
    {
      FileStream fileStream = (FileStream) null;
      StreamWriter streamWriter = (StreamWriter) null;
      try
      {
        fileStream = new FileStream(this._localBilhete, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        streamWriter = new StreamWriter((Stream) fileStream, Encoding.Default);
        streamWriter.BaseStream.Seek(0L, SeekOrigin.End);
        streamWriter.WriteLine(marcacao);
      }
      catch (AppTopdataException ex)
      {
        if (!ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          return;
        throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (!ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          return;
        throw;
      }
      finally
      {
        streamWriter.Close();
        fileStream.Close();
      }
    }

    public void SalvarEmArquivoRecursivo(string marcacao, int numArquivo)
    {
      string path = "";
      switch (numArquivo)
      {
        case 1:
          path = this._localBilhete;
          break;
        case 2:
          path = this._localBilhete2;
          break;
        case 3:
          path = this._localBilhete3;
          break;
      }
      FileStream fileStream = (FileStream) null;
      StreamWriter streamWriter = (StreamWriter) null;
      try
      {
        fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        streamWriter = new StreamWriter((Stream) fileStream, Encoding.Default);
        streamWriter.BaseStream.Seek(0L, SeekOrigin.End);
        streamWriter.WriteLine(marcacao);
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
      finally
      {
        streamWriter.Close();
        fileStream.Close();
      }
      switch (numArquivo)
      {
        case 1:
          if (this._localBilhete2 != "")
          {
            this.SalvarEmArquivoRecursivo(marcacao, 2);
            break;
          }
          if (!(this._localBilhete3 != ""))
            break;
          this.SalvarEmArquivoRecursivo(marcacao, 3);
          break;
        case 2:
          if (!(this._localBilhete3 != ""))
            break;
          this.SalvarEmArquivoRecursivo(marcacao, 3);
          break;
      }
    }

    public string Criptografar(string marcacaoDescriptografada)
    {
      Random random = new Random();
      long num = 65448;
      Thread.Sleep(1);
      long atual = (long) ((int) Math.Floor((double) (num - 1L) * random.NextDouble()) + 1);
      int codigo1 = (int) atual % 256;
      string str = this.Chr((Convert.ToInt32(atual) - codigo1) / 256) + this.Chr(codigo1);
      for (int startIndex = 0; startIndex <= marcacaoDescriptografada.Length - 1; ++startIndex)
      {
        int codigo2 = this.Asc(marcacaoDescriptografada.Substring(startIndex, 1)) ^ (int) atual % 256;
        str += this.Chr(codigo2);
        atual = this.Sequencia2B(atual);
      }
      return str;
    }

    public long Sequencia2B(long atual)
    {
      long num1 = 205;
      long num2 = 65449;
      return atual * num1 % num2;
    }

    public int Asc(string letra) => Convert.ToInt32(Encoding.Default.GetBytes(new char[1]
    {
      Convert.ToChar(letra)
    })[0]);

    public string Chr(int codigo) => Encoding.Default.GetString(new byte[1]
    {
      Convert.ToByte(codigo)
    });

    public string Descripta(string criptado)
    {
      string str = "";
      long atual = (long) this.Asc(criptado.Substring(0, 1)) * 256L + (long) this.Asc(criptado.Substring(1, 1));
      if (criptado.Length > 2)
      {
        criptado = criptado.Remove(0, 2);
        for (int startIndex = 0; startIndex <= criptado.Length - 1; ++startIndex)
        {
          int codigo = this.Asc(criptado.Substring(startIndex, 1)) ^ (int) atual % 256;
          str += this.Chr(codigo);
          atual = this.Sequencia2B(atual);
        }
      }
      return str;
    }
  }
}
