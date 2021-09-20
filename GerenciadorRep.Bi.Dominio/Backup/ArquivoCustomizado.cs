// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.ArquivoCustomizado
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class ArquivoCustomizado
  {
    private static Dictionary<string, Empregado> dicEmpregado = new Dictionary<string, Empregado>();

    public List<string> MontaArquivoCustomizado(
      List<RegistroAFD> lstRegistro,
      string mascara,
      RepBase rep,
      string nomeArqCustom,
      bool ExcluirLog)
    {
      rep = new RepBase().PesquisarRepPorID(rep.RepId);
      Empregado empregado1 = new Empregado();
      RegistrySingleton.GetInstance().COLETA_ARQUIVO_AFD = true;
      List<Empregado> empregadoList = empregado1.RecuperarEmpregadosDBByEmpregador(rep.EmpregadorId);
      RegistrySingleton.GetInstance().COLETA_ARQUIVO_AFD = false;
      ArquivoCustomizado.dicEmpregado.Clear();
      foreach (Empregado empregado2 in empregadoList)
      {
        try
        {
          ArquivoCustomizado.dicEmpregado.Add(empregado2.Pis, empregado2);
        }
        catch
        {
        }
      }
      List<RegistroAFD> registroAfdList = new List<RegistroAFD>();
      List<string> stringList1 = new List<string>();
      foreach (RegistroAFD registroAfd in lstRegistro)
      {
        string key = registroAfd.dadosRegistro.Substring(22);
        if (ArquivoCustomizado.dicEmpregado.ContainsKey(key))
          registroAfdList.Add(registroAfd);
        else
          stringList1.Add(registroAfd.dadosRegistro);
      }
      string[] strArray1 = nomeArqCustom.Split('\\');
      StringBuilder stringBuilder1 = new StringBuilder();
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (index == strArray1.Length - 1)
          stringBuilder1.Append("Log_").Append(strArray1[index]);
        else
          stringBuilder1.Append(strArray1[index]).Append("\\");
      }
      if (ExcluirLog)
      {
        if (File.Exists(stringBuilder1.ToString()))
          File.Delete(stringBuilder1.ToString());
        if (stringList1.Count > 0)
          File.WriteAllLines(stringBuilder1.ToString(), stringList1.ToArray());
      }
      else if (stringList1.Count > 0)
      {
        StringBuilder stringBuilder2 = new StringBuilder();
        foreach (string str in stringList1)
          stringBuilder2.AppendLine(str);
        File.AppendAllText(stringBuilder1.ToString(), stringBuilder2.ToString());
      }
      List<string> stringList2 = new List<string>();
      mascara = mascara.Replace("\r\n", "");
      string[] strArray2 = mascara.Split(';');
      int Sequencial = 0;
      foreach (RegistroAFD registro in registroAfdList)
      {
        ++Sequencial;
        string str = "";
        try
        {
          foreach (string formato in strArray2)
            str += this.AplicaMascara(registro, formato, Sequencial, rep);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
        stringList2.Add(str);
      }
      return stringList2;
    }

    public List<string> MontaArquivoCustomizado(
      List<ArquivoCustomizadoEnt> lstRegistro,
      string mascara)
    {
      List<string> stringList = new List<string>();
      mascara = mascara.Replace("\r\n", "");
      string[] strArray = mascara.Split(';');
      int Sequencial = 0;
      foreach (ArquivoCustomizadoEnt registro in lstRegistro)
      {
        ++Sequencial;
        string str = "";
        foreach (string formato in strArray)
          str += this.AplicaMascara(registro, formato, Sequencial);
        stringList.Add(str);
      }
      return stringList;
    }

    private string AplicaMascara(ArquivoCustomizadoEnt registro, string formato, int Sequencial)
    {
      string str1 = "";
      if (formato.Length > 0)
      {
        switch (formato.Substring(0, 1))
        {
          case "{":
            string str2;
            switch (formato.Substring(1, 1))
            {
              case "S":
                int totalWidth1 = formato.IndexOf('}', 2) - 1;
                str1 = (Sequencial % (int) Math.Pow(10.0, (double) totalWidth1)).ToString().PadLeft(totalWidth1, '0');
                break;
              case "D":
                str1 = registro.DataRegistro.ToString("dd");
                break;
              case "M":
                str1 = registro.DataRegistro.ToString("MM");
                break;
              case "A":
                str1 = formato.Length - 2 != 4 ? registro.DataRegistro.ToString("yy") : registro.DataRegistro.ToString("yyyy");
                break;
              case "h":
                DateTime dataRegistro1 = registro.DataRegistro;
                str1 = str2 = dataRegistro1.ToString("HH");
                break;
              case "m":
                DateTime dataRegistro2 = registro.DataRegistro;
                str1 = str2 = dataRegistro2.ToString("mm");
                break;
              case "s":
                str1 = "00";
                break;
              case "P":
                str1 = registro.Pis;
                break;
              case "N":
                if (ArquivoCustomizado.dicEmpregado.ContainsKey(registro.Pis))
                {
                  string pis = registro.Pis;
                  Empregado empregado;
                  ArquivoCustomizado.dicEmpregado.TryGetValue(pis, out empregado);
                  int length1 = empregado.Nome.Length;
                  int totalWidth2 = formato.Length - 2;
                  int length2 = length1 <= totalWidth2 ? length1 : totalWidth2;
                  str1 = empregado.Nome.Substring(0, length2).PadRight(totalWidth2, ' ');
                  break;
                }
                break;
              case "R":
                str1 = formato.Length - 2 != 2 ? registro.NSR.ToString().PadLeft(9, '0') : registro.Relogio.Substring(0, formato.Length - 2).PadLeft(2, '0');
                break;
              case "#":
                str1 = registro.Matricula.ToString().PadLeft(10, '0');
                break;
              case "E":
                str1 = registro.Empresa.ToString().PadLeft(3, '0');
                break;
              case "F":
                str1 = registro.Fabrica.Substring(0, formato.Length - 2);
                break;
            }
            break;
          case "[":
            str1 = formato.Substring(1, formato.Length - 2);
            break;
        }
      }
      return str1;
    }

    private string AplicaMascara(
      RegistroAFD registro,
      string formato,
      int Sequencial,
      RepBase rep)
    {
      string retorno = "";
      if (formato.Length > 0)
      {
        switch (formato.Substring(0, 1))
        {
          case "{":
            switch (formato.Substring(1, 1))
            {
              case "S":
                int totalWidth1 = formato.IndexOf('}', 2) - 1;
                retorno = (Sequencial % (int) Math.Pow(10.0, (double) totalWidth1)).ToString().PadLeft(totalWidth1, '0');
                break;
              case "D":
                retorno = registro.dtRegistro.Substring(4, 2);
                break;
              case "M":
                retorno = registro.dtRegistro.Substring(2, 2);
                break;
              case "A":
                retorno = formato.Length != 4 ? "20" + registro.dtRegistro.Substring(0, 2) : registro.dtRegistro.Substring(0, 2);
                break;
              case "h":
                retorno = registro.dtRegistro.Substring(6, 2);
                break;
              case "m":
                retorno = registro.dtRegistro.Substring(8, 2);
                break;
              case "s":
                retorno = "00";
                break;
              case "P":
                retorno = formato.Length - 2 != 11 ? registro.dadosRegistro.Substring(22) : registro.dadosRegistro.Substring(23);
                break;
              case "N":
                if (ArquivoCustomizado.dicEmpregado.ContainsKey(registro.dadosRegistro.Substring(22)))
                {
                  string key = registro.dadosRegistro.Substring(22);
                  Empregado empregado;
                  ArquivoCustomizado.dicEmpregado.TryGetValue(key, out empregado);
                  int length1 = empregado.Nome.Length;
                  int totalWidth2 = formato.Length - 2;
                  int length2 = length1 <= totalWidth2 ? length1 : totalWidth2;
                  retorno = empregado.Nome.Substring(0, length2).PadRight(totalWidth2, ' ');
                  break;
                }
                break;
              case "I":
                retorno = rep.Serial.PadLeft(17, '0');
                break;
              case "R":
                retorno = registro.NSR.ToString().PadLeft(9, '0');
                break;
              case "#":
                retorno = ArquivoCustomizado.FormataCartao(registro, formato, retorno);
                break;
              case "-":
                retorno = ArquivoCustomizado.FormataCartao(registro, formato, retorno);
                break;
              case "X":
                if (ArquivoCustomizado.dicEmpregado.ContainsKey(registro.dadosRegistro.Substring(22)))
                {
                  string key = registro.dadosRegistro.Substring(22);
                  Empregado empregado;
                  ArquivoCustomizado.dicEmpregado.TryGetValue(key, out empregado);
                  retorno = empregado.CartaoProx.ToString().PadLeft(16, '0');
                  break;
                }
                break;
              case "B":
                if (ArquivoCustomizado.dicEmpregado.ContainsKey(registro.dadosRegistro.Substring(22)))
                {
                  string key = registro.dadosRegistro.Substring(22);
                  Empregado empregado;
                  ArquivoCustomizado.dicEmpregado.TryGetValue(key, out empregado);
                  retorno = empregado.CartaoBarras.ToString().PadLeft(16, '0');
                  break;
                }
                break;
              case "T":
                if (ArquivoCustomizado.dicEmpregado.ContainsKey(registro.dadosRegistro.Substring(22)))
                {
                  string key = registro.dadosRegistro.Substring(22);
                  Empregado empregado;
                  ArquivoCustomizado.dicEmpregado.TryGetValue(key, out empregado);
                  retorno = empregado.Teclado.ToString().PadLeft(16, '0');
                  break;
                }
                break;
            }
            break;
          case "[":
            retorno = formato.Substring(1, formato.Length - 2);
            break;
        }
      }
      return retorno;
    }

    private static string FormataCartao(RegistroAFD registro, string formato, string retorno)
    {
      string key = registro.dadosRegistro.Substring(22);
      Empregado empregado;
      ArquivoCustomizado.dicEmpregado.TryGetValue(key, out empregado);
      string str = "{" + empregado.Cartao.ToString().PadLeft(16, '0') + "}";
      for (int index = 1; index < formato.Length - 1; ++index)
      {
        if (formato[index] == '#')
          retorno += (string) (object) str[index];
      }
      return retorno;
    }
  }
}
