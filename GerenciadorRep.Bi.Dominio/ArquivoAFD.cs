// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.ArquivoAFD
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class ArquivoAFD
  {
    [DllImport("kernel32.dll")]
    private static extern bool CreateSymbolicLink(
      string lpSymlinkFileName,
      string lpTargetFileName,
      ArquivoAFD.SymbolicLink dwFlags);

    public static bool ExportarAFDporREP()
    {
      try
      {
        foreach (ArquivoBilhete arquivoBilhete in (Collection<ArquivoBilhete>) new ArquivoBilhete().PesquisarArquivosCustomizado())
        {
          Console.WriteLine("Gerando arquivo {0}", (object) arquivoBilhete.ToString());
          List<ArquivoCustomizadoEnt> lstRegistro = RepAFD.PesquisarArquivoCustomizado(arquivoBilhete.ArquivoBilheteId);
          List<string> stringList = new ArquivoCustomizado().MontaArquivoCustomizado(lstRegistro, arquivoBilhete.Formato);
          if (File.Exists(arquivoBilhete.LocalBilhete))
            File.Delete(arquivoBilhete.LocalBilhete);
          File.WriteAllLines(arquivoBilhete.LocalBilhete, stringList.ToArray());
          using (List<string>.Enumerator enumerator = RepAFD.ListarTabelasRepAfdArquivo(arquivoBilhete.ArquivoBilheteId).GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              string rep = enumerator.Current;
              ArquivoCustomizadoEnt last = lstRegistro.FindLast((Predicate<ArquivoCustomizadoEnt>) (a => a.Tabela == rep));
              if (last != null)
                RepAFD.ProcessaArquivoCustomizado(last);
            }
          }
        }
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public static bool ExportarAFDporREP(ArquivoBilhete arquivo, DateTime Inicio, DateTime Final)
    {
      try
      {
        ArquivoBilhete arquivoBilhete = new ArquivoBilhete();
        List<string> stringList = new ArquivoCustomizado().MontaArquivoCustomizado(RepAFD.PesquisarArquivoCustomizado(arquivo.ArquivoBilheteId, Inicio, Final), arquivo.Formato);
        if (File.Exists(arquivo.LocalBilhete))
          File.Delete(arquivo.LocalBilhete);
        File.WriteAllLines(arquivo.LocalBilhete, stringList.ToArray());
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public static bool ExportarAFDporREP(RepBase repSelecionado, bool liberacaoProducao)
    {
      SortableBindingList<ArquivoBilhete> sortableBindingList = new ArquivoBilhete().PesquisarArquivoAFDPorREP(repSelecionado.RepId);
      ArquivoCustomizado arquivoCustomizado = new ArquivoCustomizado();
      if (sortableBindingList.Count < 1)
        return false;
      RepAFD _repAFD = RepAFD.PesquisarRepAFDPorREP(repSelecionado.RepId);
      if (_repAFD.ultimoNSR < 1)
        return false;
      foreach (ArquivoBilhete arquivoBilhete in (Collection<ArquivoBilhete>) sortableBindingList)
      {
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        bool flag1 = false;
        List<string> stringList = new List<string>();
        StringBuilder stringBuilder1 = new StringBuilder();
        StringBuilder stringBuilder2 = new StringBuilder();
        bool CriarArquivo = true;
        ConfiguracoesGerais configuracoesGerais = new ConfiguracoesGerais().PesquisarConfigGerais();
        ParametrosExportacaoAFD paramExport = new ParametrosExportacaoAFD();
        try
        {
          string str1 = arquivoBilhete.LocalBilhete;
          paramExport.TipoExportacao = arquivoBilhete.tipoExportacao;
          paramExport.TipoArquivo = Constantes.ARQUIVO_AFD.AFD_PONTO;
          if (arquivoBilhete.tipoExportacao == Constantes.TIPO_ARQUIVO.AFD || arquivoBilhete.tipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
          {
            string str2 = str1 + "\\AFD" + repSelecionado.Serial;
            if (arquivoBilhete.adicionarData)
              str2 = str2 + "_" + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString();
            str1 = str2 + ".txt";
            paramExport.TipoArquivo = Constantes.ARQUIVO_AFD.AFD_COMPLETO;
          }
          else if (arquivoBilhete.adicionarData)
            str1 = str1.Substring(0, str1.Length - 4) + "_" + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString() + ".txt";
          if (arquivoBilhete.diasPeriodo == 0)
          {
            paramExport.DataDe = "";
            paramExport.DataAte = "";
          }
          else
          {
            DateTime dateTime = DateTime.Now.AddDays((double) (arquivoBilhete.diasPeriodo * -1));
            ParametrosExportacaoAFD parametrosExportacaoAfd1 = paramExport;
            string str3 = DateTime.Now.Year.ToString().Substring(2);
            string str4 = DateTime.Now.Month.ToString().PadLeft(2, '0');
            int num6 = DateTime.Now.Day;
            string str5 = num6.ToString().PadLeft(2, '0');
            string str6 = str3 + str4 + str5 + "2359";
            parametrosExportacaoAfd1.DataAte = str6;
            ParametrosExportacaoAFD parametrosExportacaoAfd2 = paramExport;
            num6 = dateTime.Year;
            string str7 = num6.ToString().Substring(2);
            num6 = dateTime.Month;
            string str8 = num6.ToString().PadLeft(2, '0');
            num6 = dateTime.Day;
            string str9 = num6.ToString().PadLeft(2, '0');
            string str10 = str7 + str8 + str9 + "0000";
            parametrosExportacaoAfd2.DataDe = str10;
          }
          RegistroAFD registroAfd1 = configuracoesGerais.UnificaColetaAFD ? RegistroAFD.PesquisarUltimoRegistroEmpresaColetaAFD(_repAFD) : RegistroAFD.PesquisarUltimoRegistroEmpresa(_repAFD);
          if (!liberacaoProducao && registroAfd1.dadosRegistro == null)
            return false;
          if (paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD || paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
          {
            RegistroAFD registroAfd2;
            RegistroAFD registroAfd3;
            if (!configuracoesGerais.UnificaColetaAFD)
            {
              registroAfd2 = RegistroAFD.PesquisarRegistroMenorDataAFD(_repAFD, paramExport);
              registroAfd3 = RegistroAFD.PesquisarRegistroMaiorDataAFD(_repAFD, paramExport);
            }
            else
            {
              registroAfd2 = RegistroAFD.PesquisarRegistroMenorDataColetaAFD(_repAFD, paramExport);
              registroAfd3 = RegistroAFD.PesquisarRegistroMaiorDataColetaAFD(_repAFD, paramExport);
            }
            stringBuilder1.Append("0000000001");
            if (!liberacaoProducao)
            {
              string str11 = registroAfd1.dadosRegistro.Substring(22, 177);
              if (paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
              {
                string str12 = str11.Substring(0, 1);
                string s1 = str11.Substring(1, 14);
                string s2 = str11.Substring(15, 12);
                string str13 = str11.Substring(27, str11.Length - 27);
                if (long.Parse(s2) == 0L)
                  s2 = "            ";
                if (str12.Equals("2"))
                  s1 = long.Parse(s1).ToString().PadLeft(11, '0').PadLeft(14, ' ');
                str11 = str12 + s1 + s2 + str13;
              }
              stringBuilder1.Append(str11);
            }
            stringBuilder1.Append(repSelecionado.Serial);
            stringBuilder1.Append(registroAfd2.dadosRegistro.Substring(10, 8));
            stringBuilder1.Append(registroAfd3.dadosRegistro.Substring(10, 8));
            stringBuilder1.Append(DateTime.Now.ToString("ddMMyyyyHHmm"));
            if (paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
              stringBuilder1.Append(RepAFD.GerarCRC(stringBuilder1.ToString()));
            string[] arrayAFD = new string[1]
            {
              stringBuilder1.ToString()
            };
            if (ArquivoAFD.GravarArquivo(str1, arrayAFD, CriarArquivo))
              CriarArquivo = false;
            else
              continue;
          }
          bool flag2 = true;
          bool ExcluirLog = true;
          paramExport.ultNsrPesquisado = 0;
          while (flag2)
          {
            List<RegistroAFD> lstRegistro = configuracoesGerais.UnificaColetaAFD ? RegistroAFD.PesquisarTodosOsRegistrosColetaAFD(_repAFD, paramExport) : RegistroAFD.PesquisarTodosOsRegistrosAFD(_repAFD, paramExport);
            if (lstRegistro.Count > 0)
            {
              paramExport.ultNsrPesquisado = lstRegistro[lstRegistro.Count - 1].NSR;
              stringList.Clear();
              if (paramExport.TipoArquivo == Constantes.ARQUIVO_AFD.AFD_COMPLETO)
              {
                foreach (RegistroAFD registroAfd4 in lstRegistro)
                {
                  if (registroAfd4.tipoRegistro == 3)
                    ++num3;
                  else if (registroAfd4.tipoRegistro == 2)
                    ++num1;
                  else if (registroAfd4.tipoRegistro == 5)
                    ++num2;
                  else if (registroAfd4.tipoRegistro == 4)
                    ++num4;
                  else if (registroAfd4.tipoRegistro == 6)
                    ++num5;
                }
              }
              if (paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD || paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
              {
                foreach (RegistroAFD registroAfd5 in lstRegistro)
                {
                  string str14 = "";
                  if (paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD)
                    str14 = registroAfd5.dadosRegistro;
                  else if (paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
                  {
                    if (registroAfd5.tipoRegistro != 6)
                    {
                      string registroAfd6;
                      if (registroAfd5.tipoRegistro == 5)
                        registroAfd6 = registroAfd5.dadosRegistro + "    " + registroAfd5.CPFResponsavel;
                      else if (registroAfd5.tipoRegistro == 2)
                      {
                        string str15 = registroAfd5.dadosRegistro.Substring(0, 22);
                        string str16 = registroAfd5.dadosRegistro.Substring(22, 1);
                        string s3 = registroAfd5.dadosRegistro.Substring(23, 14);
                        string s4 = registroAfd5.dadosRegistro.Substring(37, 12);
                        string str17 = registroAfd5.dadosRegistro.Substring(49, 250);
                        if (!str16.Equals("0"))
                        {
                          if (long.Parse(s3) == 0L)
                            s3 = "              ";
                          else if (str16.Equals("2"))
                            s3 = long.Parse(s3).ToString().PadLeft(11, '0').PadLeft(14, ' ');
                          if (long.Parse(s4) == 0L)
                            s4 = "            ";
                        }
                        else
                        {
                          if (long.Parse(s3) != 0L)
                            s3 = long.Parse(s3).ToString().PadLeft(14, ' ');
                          s4 = "            ";
                        }
                        registroAfd6 = str15 + "   " + registroAfd5.CPFResponsavel + str16 + s3 + s4 + str17;
                      }
                      else
                        registroAfd6 = registroAfd5.tipoRegistro != 3 ? registroAfd5.dadosRegistro + registroAfd5.CPFResponsavel : registroAfd5.dadosRegistro;
                      str14 = registroAfd6 + RepAFD.GerarCRC(registroAfd6);
                    }
                    else
                      str14 = registroAfd5.dadosRegistro;
                  }
                  stringList.Add(str14);
                }
              }
              else
              {
                stringList.AddRange((IEnumerable<string>) arquivoCustomizado.MontaArquivoCustomizado(lstRegistro, arquivoBilhete.Formato, repSelecionado, str1, ExcluirLog));
                ExcluirLog = false;
              }
              string[] arrayAFD = new string[stringList.Count];
              for (int index = 0; index < stringList.Count; ++index)
                arrayAFD[index] = stringList[index];
              if (!ArquivoAFD.GravarArquivo(str1, arrayAFD, CriarArquivo))
              {
                flag1 = true;
                break;
              }
              if (CriarArquivo)
                CriarArquivo = false;
            }
            else
              flag2 = false;
          }
          if (!flag1)
          {
            if (paramExport.TipoExportacao != Constantes.TIPO_ARQUIVO.AFD)
            {
              if (paramExport.TipoExportacao != Constantes.TIPO_ARQUIVO.AFD_INMETRO)
                continue;
            }
            stringBuilder2.Append("999999999");
            stringBuilder2.Append(num1.ToString().PadLeft(9, '0'));
            stringBuilder2.Append(num3.ToString().PadLeft(9, '0'));
            stringBuilder2.Append(num4.ToString().PadLeft(9, '0'));
            stringBuilder2.Append(num2.ToString().PadLeft(9, '0'));
            if ((repSelecionado.TerminalId == 13 || repSelecionado.TerminalId == 16 || repSelecionado.TerminalId == 17 || repSelecionado.TerminalId == 18 || repSelecionado.TerminalId == 19 || repSelecionado.TerminalId == 20) && paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
              stringBuilder2.Append(num5.ToString().PadLeft(9, '0'));
            stringBuilder2.Append("9");
            string[] arrayAFD = new string[1]
            {
              stringBuilder2.ToString()
            };
            if (ArquivoAFD.GravarArquivo(str1, arrayAFD, false))
              ;
          }
        }
        catch
        {
        }
      }
      return true;
    }

    public static bool ExportarArquivoCustomizado(RepBase repSelecionado, bool liberacaoProducao)
    {
      SortableBindingList<ArquivoBilhete> sortableBindingList = new ArquivoBilhete().PesquisarArquivoAFDPorREP(repSelecionado.RepId);
      ArquivoCustomizado arquivoCustomizado = new ArquivoCustomizado();
      if (sortableBindingList.Count < 1)
        return false;
      RepAFD _repAFD = RepAFD.PesquisarRepAFDPorREP(repSelecionado.RepId);
      if (_repAFD.ultimoNSR < 1)
        return false;
      foreach (ArquivoBilhete arquivoBilhete in (Collection<ArquivoBilhete>) sortableBindingList)
      {
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        bool flag1 = false;
        List<string> stringList = new List<string>();
        StringBuilder stringBuilder1 = new StringBuilder();
        StringBuilder stringBuilder2 = new StringBuilder();
        bool CriarArquivo = true;
        ConfiguracoesGerais configuracoesGerais = new ConfiguracoesGerais().PesquisarConfigGerais();
        ParametrosExportacaoAFD paramExport = new ParametrosExportacaoAFD();
        try
        {
          string str1 = arquivoBilhete.LocalBilhete;
          paramExport.TipoExportacao = arquivoBilhete.tipoExportacao;
          paramExport.TipoArquivo = Constantes.ARQUIVO_AFD.AFD_PONTO;
          if (arquivoBilhete.tipoExportacao == Constantes.TIPO_ARQUIVO.AFD || arquivoBilhete.tipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
          {
            string str2 = str1 + "\\AFD" + repSelecionado.Serial;
            if (arquivoBilhete.adicionarData)
              str2 = str2 + "_" + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString();
            str1 = str2 + ".txt";
            paramExport.TipoArquivo = Constantes.ARQUIVO_AFD.AFD_COMPLETO;
          }
          else if (arquivoBilhete.adicionarData)
            str1 = str1.Substring(0, str1.Length - 4) + "_" + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString() + ".txt";
          if (arquivoBilhete.diasPeriodo == 0)
          {
            paramExport.DataDe = "";
            paramExport.DataAte = "";
          }
          else
          {
            DateTime dateTime = DateTime.Now.AddDays((double) (arquivoBilhete.diasPeriodo * -1));
            ParametrosExportacaoAFD parametrosExportacaoAfd1 = paramExport;
            string str3 = DateTime.Now.Year.ToString().Substring(2);
            string str4 = DateTime.Now.Month.ToString().PadLeft(2, '0');
            int num6 = DateTime.Now.Day;
            string str5 = num6.ToString().PadLeft(2, '0');
            string str6 = str3 + str4 + str5 + "2359";
            parametrosExportacaoAfd1.DataAte = str6;
            ParametrosExportacaoAFD parametrosExportacaoAfd2 = paramExport;
            num6 = dateTime.Year;
            string str7 = num6.ToString().Substring(2);
            num6 = dateTime.Month;
            string str8 = num6.ToString().PadLeft(2, '0');
            num6 = dateTime.Day;
            string str9 = num6.ToString().PadLeft(2, '0');
            string str10 = str7 + str8 + str9 + "0000";
            parametrosExportacaoAfd2.DataDe = str10;
          }
          RegistroAFD registroAfd1 = configuracoesGerais.UnificaColetaAFD ? RegistroAFD.PesquisarUltimoRegistroEmpresaColetaAFD(_repAFD) : RegistroAFD.PesquisarUltimoRegistroEmpresa(_repAFD);
          if (!liberacaoProducao && registroAfd1.dadosRegistro == null)
            return false;
          if (paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD || paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
          {
            RegistroAFD registroAfd2;
            RegistroAFD registroAfd3;
            if (!configuracoesGerais.UnificaColetaAFD)
            {
              registroAfd2 = RegistroAFD.PesquisarRegistroMenorDataAFD(_repAFD, paramExport);
              registroAfd3 = RegistroAFD.PesquisarRegistroMaiorDataAFD(_repAFD, paramExport);
            }
            else
            {
              registroAfd2 = RegistroAFD.PesquisarRegistroMenorDataColetaAFD(_repAFD, paramExport);
              registroAfd3 = RegistroAFD.PesquisarRegistroMaiorDataColetaAFD(_repAFD, paramExport);
            }
            stringBuilder1.Append("0000000001");
            if (!liberacaoProducao)
            {
              string str11 = registroAfd1.dadosRegistro.Substring(22, 177);
              if (paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
              {
                string str12 = str11.Substring(0, 1);
                string s1 = str11.Substring(1, 14);
                string s2 = str11.Substring(15, 12);
                string str13 = str11.Substring(27, str11.Length - 27);
                if (long.Parse(s2) == 0L)
                  s2 = "            ";
                if (str12.Equals("2"))
                  s1 = long.Parse(s1).ToString().PadLeft(11, '0').PadLeft(14, ' ');
                str11 = str12 + s1 + s2 + str13;
              }
              stringBuilder1.Append(str11);
            }
            stringBuilder1.Append(repSelecionado.Serial);
            stringBuilder1.Append(registroAfd2.dadosRegistro.Substring(10, 8));
            stringBuilder1.Append(registroAfd3.dadosRegistro.Substring(10, 8));
            stringBuilder1.Append(DateTime.Now.ToString("ddMMyyyyHHmm"));
            if (paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
              stringBuilder1.Append(RepAFD.GerarCRC(stringBuilder1.ToString()));
            string[] arrayAFD = new string[1]
            {
              stringBuilder1.ToString()
            };
            if (ArquivoAFD.GravarArquivo(str1, arrayAFD, CriarArquivo))
              CriarArquivo = false;
            else
              continue;
          }
          bool flag2 = true;
          bool ExcluirLog = true;
          paramExport.ultNsrPesquisado = 0;
          while (flag2)
          {
            List<RegistroAFD> lstRegistro = configuracoesGerais.UnificaColetaAFD ? RegistroAFD.PesquisarTodosOsRegistrosColetaAFD(_repAFD, paramExport) : RegistroAFD.PesquisarTodosOsRegistrosAFD(_repAFD, paramExport);
            if (lstRegistro.Count > 0)
            {
              paramExport.ultNsrPesquisado = lstRegistro[lstRegistro.Count - 1].NSR;
              stringList.Clear();
              if (paramExport.TipoArquivo == Constantes.ARQUIVO_AFD.AFD_COMPLETO)
              {
                foreach (RegistroAFD registroAfd4 in lstRegistro)
                {
                  if (registroAfd4.tipoRegistro == 3)
                    ++num3;
                  else if (registroAfd4.tipoRegistro == 2)
                    ++num1;
                  else if (registroAfd4.tipoRegistro == 5)
                    ++num2;
                  else if (registroAfd4.tipoRegistro == 4)
                    ++num4;
                  else if (registroAfd4.tipoRegistro == 6)
                    ++num5;
                }
              }
              if (paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD || paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
              {
                foreach (RegistroAFD registroAfd5 in lstRegistro)
                {
                  string str14 = "";
                  if (paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD)
                    str14 = registroAfd5.dadosRegistro;
                  else if (paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
                  {
                    if (registroAfd5.tipoRegistro != 6)
                    {
                      string registroAfd6;
                      if (registroAfd5.tipoRegistro == 5)
                        registroAfd6 = registroAfd5.dadosRegistro + "    " + registroAfd5.CPFResponsavel;
                      else if (registroAfd5.tipoRegistro == 2)
                      {
                        string str15 = registroAfd5.dadosRegistro.Substring(0, 22);
                        string str16 = registroAfd5.dadosRegistro.Substring(22, 1);
                        string s3 = registroAfd5.dadosRegistro.Substring(23, 14);
                        string s4 = registroAfd5.dadosRegistro.Substring(37, 12);
                        string str17 = registroAfd5.dadosRegistro.Substring(49, 250);
                        if (!str16.Equals("0"))
                        {
                          if (long.Parse(s3) == 0L)
                            s3 = "              ";
                          else if (str16.Equals("2"))
                            s3 = long.Parse(s3).ToString().PadLeft(11, '0').PadLeft(14, ' ');
                          if (long.Parse(s4) == 0L)
                            s4 = "            ";
                        }
                        else
                        {
                          if (long.Parse(s3) != 0L)
                            s3 = long.Parse(s3).ToString().PadLeft(14, ' ');
                          s4 = "            ";
                        }
                        registroAfd6 = str15 + "   " + registroAfd5.CPFResponsavel + str16 + s3 + s4 + str17;
                      }
                      else
                        registroAfd6 = registroAfd5.tipoRegistro != 3 ? registroAfd5.dadosRegistro + registroAfd5.CPFResponsavel : registroAfd5.dadosRegistro;
                      str14 = registroAfd6 + RepAFD.GerarCRC(registroAfd6);
                    }
                    else
                      str14 = registroAfd5.dadosRegistro;
                  }
                  stringList.Add(str14);
                }
              }
              else
              {
                stringList.AddRange((IEnumerable<string>) arquivoCustomizado.MontaArquivoCustomizado(lstRegistro, arquivoBilhete.Formato, repSelecionado, str1, ExcluirLog));
                ExcluirLog = false;
              }
              string[] arrayAFD = new string[stringList.Count];
              for (int index = 0; index < stringList.Count; ++index)
                arrayAFD[index] = stringList[index];
              if (!ArquivoAFD.GravarArquivo(str1, arrayAFD, CriarArquivo))
              {
                flag1 = true;
                break;
              }
              if (CriarArquivo)
                CriarArquivo = false;
            }
            else
              flag2 = false;
          }
          if (!flag1)
          {
            if (paramExport.TipoExportacao != Constantes.TIPO_ARQUIVO.AFD)
            {
              if (paramExport.TipoExportacao != Constantes.TIPO_ARQUIVO.AFD_INMETRO)
                continue;
            }
            stringBuilder2.Append("999999999");
            stringBuilder2.Append(num1.ToString().PadLeft(9, '0'));
            stringBuilder2.Append(num3.ToString().PadLeft(9, '0'));
            stringBuilder2.Append(num4.ToString().PadLeft(9, '0'));
            stringBuilder2.Append(num2.ToString().PadLeft(9, '0'));
            if ((repSelecionado.TerminalId == 13 || repSelecionado.TerminalId == 16 || repSelecionado.TerminalId == 17 || repSelecionado.TerminalId == 18 || repSelecionado.TerminalId == 19 || repSelecionado.TerminalId == 20) && paramExport.TipoExportacao == Constantes.TIPO_ARQUIVO.AFD_INMETRO)
              stringBuilder2.Append(num5.ToString().PadLeft(9, '0'));
            stringBuilder2.Append("9");
            string[] arrayAFD = new string[1]
            {
              stringBuilder2.ToString()
            };
            if (ArquivoAFD.GravarArquivo(str1, arrayAFD, false))
              ;
          }
        }
        catch (Exception ex)
        {
        }
      }
      return true;
    }

    public static void ExecuteCopia(string caminhoReal, int timeout)
    {
      try
      {
        string[] strArray1 = caminhoReal.Split('\\');
        string[] strArray2 = new string[2]
        {
          (AppDomain.CurrentDomain.BaseDirectory.ToString() + strArray1[strArray1.Length - 1]).Replace(' ', '#'),
          caminhoReal.Replace(' ', '#')
        };
        new Process()
        {
          StartInfo = new ProcessStartInfo()
          {
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = (AppDomain.CurrentDomain.BaseDirectory.ToString() + "InnerRepCopy.exe"),
            Arguments = string.Join(" ", strArray2),
            WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory.ToString(),
            LoadUserProfile = true,
            UseShellExecute = false
          }
        }.Start();
      }
      catch (Exception ex)
      {
      }
    }

    private static bool GravarArquivoNaRaiz(
      string nomeArquivo,
      string[] arrayAFD,
      bool criarArquivo)
    {
      try
      {
        string[] strArray = nomeArquivo.Split('\\');
        string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\" + strArray[strArray.Length - 1];
        if (criarArquivo)
        {
          File.WriteAllLines(path, arrayAFD, Encoding.Default);
        }
        else
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (string str in arrayAFD)
            stringBuilder.AppendLine(str);
          File.AppendAllText(path, stringBuilder.ToString(), Encoding.Default);
        }
        ArquivoAFD.ExecuteCopia(nomeArquivo, 10);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private static bool GravarArquivo(string NomeArquivo, string[] arrayAFD, bool CriarArquivo)
    {
      if (CriarArquivo)
      {
        try
        {
          File.WriteAllLines(NomeArquivo, arrayAFD, Encoding.Default);
        }
        catch (IOException ex)
        {
          return ArquivoAFD.GravarArquivoNaRaiz(NomeArquivo, arrayAFD, CriarArquivo);
        }
        catch (Exception ex)
        {
          return ArquivoAFD.GravarArquivoNaRaiz(NomeArquivo, arrayAFD, CriarArquivo);
        }
      }
      else
      {
        try
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (string str in arrayAFD)
            stringBuilder.AppendLine(str);
          File.AppendAllText(NomeArquivo, stringBuilder.ToString(), Encoding.Default);
        }
        catch (PathTooLongException ex)
        {
          return ArquivoAFD.GravarArquivoNaRaiz(NomeArquivo, arrayAFD, CriarArquivo);
        }
        catch (Exception ex)
        {
          return ArquivoAFD.GravarArquivoNaRaiz(NomeArquivo, arrayAFD, CriarArquivo);
        }
      }
      return true;
    }

    private enum SymbolicLink
    {
      File,
      Directory,
    }
  }
}
