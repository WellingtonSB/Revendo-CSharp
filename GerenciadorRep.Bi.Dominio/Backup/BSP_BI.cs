// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.BSP_BI
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using NITGEN.SDK.NBioBSP;
using System;
using System.Globalization;
using System.Text;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class BSP_BI
  {
    public static byte[] TransformaTemplateEmBytes(string template)
    {
      byte[] numArray = new byte[template.Length / 2];
      int index1 = 0;
      int length = template.Length;
      for (int index2 = 0; index2 < length; index2 += 2)
      {
        string s = template[index2].ToString() + template[index2 + 1].ToString();
        numArray[index1] = (byte) int.Parse(s, NumberStyles.HexNumber);
        ++index1;
      }
      return numArray;
    }

    public static byte[] ConverteTemplate(byte[] Template, int tipoTemplate)
    {
      NBioAPI.Export export = new NBioAPI.Export(new NBioAPI());
      byte[] data;
      if (tipoTemplate == 2)
      {
        NBioAPI.Type.HFIR ProcessedFIR;
        if (export.FDxToNBioBSPEx(Template, 400U, NBioAPI.Type.MINCONV_DATA_TYPE.MINCONV_TYPE_FIM10_HV, NBioAPI.Type.FIR_PURPOSE.ENROLL, out ProcessedFIR) != 4U)
          ;
        NBioAPI.Export.EXPORT_DATA ExportData;
        int fdx = (int) export.NBioBSPToFDx(ProcessedFIR, out ExportData, NBioAPI.Type.MINCONV_DATA_TYPE.MINCONV_TYPE_FIM01_HV);
        data = ExportData.FingerData[0].Template[0].Data;
      }
      else
      {
        NBioAPI.Type.HFIR ProcessedFIR;
        int nbioBspEx = (int) export.FDxToNBioBSPEx(Template, 404U, NBioAPI.Type.MINCONV_DATA_TYPE.MINCONV_TYPE_FIM01_HV, NBioAPI.Type.FIR_PURPOSE.ENROLL, out ProcessedFIR);
        NBioAPI.Export.EXPORT_DATA ExportData;
        if (export.NBioBSPToFDx(ProcessedFIR, out ExportData, NBioAPI.Type.MINCONV_DATA_TYPE.MINCONV_TYPE_FIM10_HV) != 4U)
          ;
        data = ExportData.FingerData[0].Template[0].Data;
      }
      return data;
    }

    public static string ConverteByteArrayParaString(byte[] tempBytes)
    {
      string empty = string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (byte tempByte in tempBytes)
        stringBuilder.Append(tempByte.ToString("x").PadLeft(2, '0'));
      return stringBuilder.ToString();
    }

    public static TemplatesBio ConverteTemp3030para2030(TemplatesBio tempBioEnt)
    {
      byte[] numArray1 = new byte[400];
      byte[] numArray2 = new byte[400];
      byte[] numArray3 = new byte[404];
      byte[] numArray4 = new byte[404];
      TemplatesBio templatesBio = new TemplatesBio();
      templatesBio.EmpregadorID = tempBioEnt.EmpregadorID;
      templatesBio.Id = tempBioEnt.Id;
      templatesBio.IdBiometrico = tempBioEnt.IdBiometrico;
      templatesBio.Senha = tempBioEnt.Senha;
      templatesBio.Template1 = tempBioEnt.Template1;
      templatesBio.Template2 = tempBioEnt.Template2;
      templatesBio.TipoTemplate = tempBioEnt.TipoTemplate;
      templatesBio.EmpregadoID = tempBioEnt.EmpregadoID;
      byte[] Template1 = BSP_BI.TransformaTemplateEmBytes(templatesBio.Template1);
      byte[] Template2 = BSP_BI.TransformaTemplateEmBytes(templatesBio.Template2);
      byte[] tempBytes1 = BSP_BI.ConverteTemplate(Template1, 2);
      byte[] tempBytes2 = BSP_BI.ConverteTemplate(Template2, 2);
      try
      {
        templatesBio.TipoTemplate = 2;
        templatesBio.Template1 = BSP_BI.ConverteByteArrayParaString(tempBytes1);
        templatesBio.Template2 = BSP_BI.ConverteByteArrayParaString(tempBytes2);
      }
      catch (Exception ex)
      {
        throw;
      }
      return templatesBio;
    }

    public static TemplatesBio ConverteTemp2030para3030(TemplatesBio tempBioEnt)
    {
      byte[] numArray1 = new byte[400];
      byte[] numArray2 = new byte[400];
      byte[] numArray3 = new byte[404];
      byte[] numArray4 = new byte[404];
      TemplatesBio templatesBio = new TemplatesBio();
      templatesBio.EmpregadorID = tempBioEnt.EmpregadorID;
      templatesBio.Id = tempBioEnt.Id;
      templatesBio.IdBiometrico = tempBioEnt.IdBiometrico;
      templatesBio.Senha = tempBioEnt.Senha;
      templatesBio.Template1 = tempBioEnt.Template1;
      templatesBio.Template2 = tempBioEnt.Template2;
      templatesBio.TipoTemplate = tempBioEnt.TipoTemplate;
      byte[] Template1 = BSP_BI.TransformaTemplateEmBytes(templatesBio.Template1);
      byte[] Template2 = BSP_BI.TransformaTemplateEmBytes(templatesBio.Template2);
      byte[] tempBytes1 = BSP_BI.ConverteTemplate(Template1, 1);
      byte[] tempBytes2 = BSP_BI.ConverteTemplate(Template2, 1);
      try
      {
        templatesBio.TipoTemplate = 1;
        templatesBio.Template1 = BSP_BI.ConverteByteArrayParaString(tempBytes1);
        templatesBio.Template2 = BSP_BI.ConverteByteArrayParaString(tempBytes2);
      }
      catch
      {
        throw;
      }
      return templatesBio;
    }
  }
}
