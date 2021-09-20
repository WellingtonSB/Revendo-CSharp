// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.CriptoBI
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class CriptoBI
  {
    public const byte POS_DIA_INI = 0;
    public const byte POS_MES_INI = 1;
    public const byte POS_ANO_INI = 2;
    public const byte POS_HORA_INI = 3;
    public const byte POS_MIN_INI = 4;
    public const byte POS_DIA_FIM = 5;
    public const byte POS_MES_FIM = 6;
    public const byte POS_ANO_FIM = 7;
    public const byte POS_HORA_FIM = 8;
    public const byte POS_MIN_FIM = 9;
    private Empregador _EmpregadorSDK = new Empregador();
    private Empregador _entEmpregadorSDK = new Empregador();
    private List<Empregado> _listaEmpregadosSDK = new List<Empregado>();
    private RepBase _ConfigRepSDK = new RepBase();
    private RepBase _entConfigRepSDK = new RepBase();
    private Relogio _RelogioRepSDK = new Relogio();
    private FormatoCartao _FormatoCartaoBarrasSDK = new FormatoCartao();
    private FormatoCartao _FormatoCartaoProxSDK = new FormatoCartao();
    private List<UsuarioBio> _listaTemplatesSDK = new List<UsuarioBio>();
    private AjusteBiometrico _AjusteBiometricoSDK = new AjusteBiometrico();
    public ConfiguracaoBarras20 _Barras20SDK = new ConfiguracaoBarras20();
    private int versaoIR;
    private string numeroSerie;
    private Empregador objempregador;
    private int repId;
    private int tipoExportacao;
    private string localRep;
    private ArquivoCFGEntity _arquivoCFGEntSDK = new ArquivoCFGEntity();
    private bool _chamadaSDK;

    public bool ChamadoPelaAT { get; set; }

    public Empregador EntEmpregadorSDK
    {
      get => this._entEmpregadorSDK;
      set => this._entEmpregadorSDK = value;
    }

    public List<Empregado> ListaEmpregadosSDK
    {
      get => this._listaEmpregadosSDK;
      set => this._listaEmpregadosSDK = value;
    }

    public RepBase EntConfigRepSDK
    {
      get => this._entConfigRepSDK;
      set => this._entConfigRepSDK = value;
    }

    public Relogio RelogioRepSDK
    {
      get => this._RelogioRepSDK;
      set => this._RelogioRepSDK = value;
    }

    public FormatoCartao FormatoCartaoBarrasSDK
    {
      get => this._FormatoCartaoBarrasSDK;
      set => this._FormatoCartaoBarrasSDK = value;
    }

    public FormatoCartao FormatoCartaoProxSDK
    {
      get => this._FormatoCartaoProxSDK;
      set => this._FormatoCartaoProxSDK = value;
    }

    public List<UsuarioBio> ListaTemplatesSDK
    {
      get => this._listaTemplatesSDK;
      set => this._listaTemplatesSDK = value;
    }

    public AjusteBiometrico AjusteBiometricoSDK
    {
      get => this._AjusteBiometricoSDK;
      set => this._AjusteBiometricoSDK = value;
    }

    public int VersaoIR
    {
      get => this.versaoIR;
      set => this.versaoIR = value;
    }

    public string NumeroSerie
    {
      get => this.numeroSerie;
      set => this.numeroSerie = value;
    }

    public Empregador Objempregador
    {
      get => this.objempregador;
      set => this.objempregador = value;
    }

    public int RepId
    {
      get => this.repId;
      set => this.repId = value;
    }

    public int TipoExportacao
    {
      get => this.tipoExportacao;
      set => this.tipoExportacao = value;
    }

    public string LocalRep
    {
      get => this.localRep;
      set => this.localRep = value;
    }

    public ArquivoCFGEntity ArquivoCFGEntSDK
    {
      get => this._arquivoCFGEntSDK;
      set => this._arquivoCFGEntSDK = value;
    }

    public event EventHandler<NotificarEventArgsCripto> OnNotificarCripto;

    public event EventHandler<NotificarEventArgsCripto> OnNotificarTipoCripto;

    public event EventHandler<NotificarEventArgsCripto> OnNotificarBytesProcessadosCripto;

    public event EventHandler<NotificarEventArgsCripto> OnNotificaMaxBytesCripto;

    public CriptoBI()
    {
    }

    public CriptoBI(bool chamado) => this.ChamadoPelaAT = chamado;

    public CriptoBI(bool chamadaSDK, int tipoImportacao)
    {
      this._chamadaSDK = chamadaSDK;
      this.tipoExportacao = tipoImportacao;
    }

    public CriptoBI(bool chamadaSDK, Empregador EmpregadorSDK)
    {
      this._chamadaSDK = chamadaSDK;
      this._EmpregadorSDK = EmpregadorSDK;
    }

    public CriptoBI(bool chamadaSDK, List<Empregado> listaEmpregadosSDK)
    {
      this._chamadaSDK = chamadaSDK;
      this._listaEmpregadosSDK = listaEmpregadosSDK;
    }

    public CriptoBI(
      bool chamadaSDK,
      RepBase ConfigRepSDK,
      Relogio RelogioRepSDK,
      FormatoCartao FormatoCartaoBarrasSDK,
      FormatoCartao FormatoCartaoProxSDK)
    {
      this._chamadaSDK = chamadaSDK;
      this._ConfigRepSDK = ConfigRepSDK;
      this._RelogioRepSDK = RelogioRepSDK;
      this._FormatoCartaoBarrasSDK = FormatoCartaoBarrasSDK;
      this._FormatoCartaoProxSDK = FormatoCartaoProxSDK;
    }

    public CriptoBI(
      bool chamadaSDK,
      RepBase ConfigRepSDK,
      Relogio RelogioRepSDK,
      FormatoCartao FormatoCartaoBarrasSDK,
      FormatoCartao FormatoCartaoProxSDK,
      AjusteBiometrico _ajusteBio)
    {
      this._chamadaSDK = chamadaSDK;
      this._ConfigRepSDK = ConfigRepSDK;
      this._RelogioRepSDK = RelogioRepSDK;
      this._FormatoCartaoBarrasSDK = FormatoCartaoBarrasSDK;
      this._FormatoCartaoProxSDK = FormatoCartaoProxSDK;
      this._AjusteBiometricoSDK = _ajusteBio;
    }

    public CriptoBI(
      bool chamadaSDK,
      RepBase ConfigRepSDK,
      Relogio RelogioRepSDK,
      FormatoCartao FormatoCartaoBarrasSDK,
      FormatoCartao FormatoCartaoProxSDK,
      AjusteBiometrico _ajusteBio,
      ConfiguracaoBarras20 confBarras20)
    {
      this._chamadaSDK = chamadaSDK;
      this._ConfigRepSDK = ConfigRepSDK;
      this._RelogioRepSDK = RelogioRepSDK;
      this._FormatoCartaoBarrasSDK = FormatoCartaoBarrasSDK;
      this._FormatoCartaoProxSDK = FormatoCartaoProxSDK;
      this._AjusteBiometricoSDK = _ajusteBio;
      this._Barras20SDK = confBarras20;
    }

    public CriptoBI(bool chamadaSDK, List<UsuarioBio> listaTemplatesSDK)
    {
      this._chamadaSDK = chamadaSDK;
      this._listaTemplatesSDK = listaTemplatesSDK;
    }

    public bool GerarDados(string path)
    {
      bool flag1 = false;
      bool flag2;
      switch (this.tipoExportacao)
      {
        case 1:
          flag1 = this.CriptografaRB1Empregador(path);
          break;
        case 2:
          flag1 = this.CriptografaRB1Empregados(path);
          break;
        case 3:
          flag1 = this.CriptografaRB1ConfiguracoesRep(path, 8);
          if (!this._chamadaSDK)
          {
            flag2 = this.CriptografaRB1ConfiguracoesRep(path.Replace("BASE_IMPORTA_CONFIG_REP", "BASE_IMPORTA_CONFIG_2_REP"), 15);
            flag1 = this.CriptografaRB1ConfiguracoesRep(path.Replace("BASE_IMPORTA_CONFIG_REP", "BASE_IMPORTA_CONFIG_3_REP"), 21);
            break;
          }
          break;
        case 4:
          flag1 = this.CriptografaRB1Templates(path, 4);
          if (!this._chamadaSDK)
          {
            flag2 = this.CriptografaRB1Templates(path.Replace("BASE_IMPORTA_BIO_{0}_REP", "BASE_IMPORTA_BIO_N2_{0}_REP"), 10);
            flag2 = this.CriptografaRB1Templates(path.Replace("BASE_IMPORTA_BIO_{0}_REP", "BASE_IMPORTA_BIO_C1_{0}_REP"), 12);
            flag2 = this.CriptografaRB1Templates(path.Replace("BASE_IMPORTA_BIO_{0}_REP", "BASE_IMPORTA_BIO_C2_{0}_REP"), 17);
            flag1 = this.CriptografaRB1TemplatesSagem(path.Replace("BASE_IMPORTA_BIO_{0}_REP", "BASE_IMPORTA_BIO_M2_{0}_REP"), 19);
            break;
          }
          break;
        case 10:
          flag1 = this.CriptografaRB1Templates(path, 10);
          break;
        case 12:
          flag1 = this.CriptografaRB1Templates(path, 12);
          break;
        case 15:
          flag1 = this.CriptografaRB1ConfiguracoesRep(path, 15);
          break;
        case 17:
          flag1 = this.CriptografaRB1Templates(path, 17);
          break;
        case 19:
          flag1 = this.CriptografaRB1TemplatesSagem(path, 19);
          break;
        case 21:
          flag1 = this.CriptografaRB1ConfiguracoesRep(path, 21);
          break;
      }
      return flag1;
    }

    public bool ImportarDados(string path, int tipoConfiguracoes)
    {
      bool flag = false;
      switch (this.tipoExportacao)
      {
        case 1:
          flag = this.DesCriptografaRB1Empregador(path);
          break;
        case 2:
          flag = this.DesCriptografaRB1Empregados(path);
          break;
        case 3:
        case 16:
        case 22:
          flag = this.DesCriptografaRB1Configuracoes(path, tipoConfiguracoes);
          break;
        case 4:
        case 11:
        case 13:
        case 18:
        case 20:
          flag = this.DesCriptografaRB1Templates(path);
          break;
      }
      return flag;
    }

    private byte[] ConverteHexaToASCii(string hexastring)
    {
      byte[] numArray = new byte[hexastring.Length / 2];
      for (int index = 0; index < hexastring.Length / 2; ++index)
      {
        char ch = Convert.ToChar(short.Parse(hexastring.Substring(index * 2, 2), NumberStyles.AllowHexSpecifier));
        numArray[index] = (byte) ch;
      }
      return numArray;
    }

    private byte[] ConverteHexaToASCiiCorpoBinario(string hexastring)
    {
      byte[] numArray = new byte[hexastring.Length / 2];
      for (int index = 0; index < hexastring.Length / 2; ++index)
      {
        char ch = Convert.ToChar(short.Parse(hexastring.Substring(index * 2, 2), NumberStyles.AllowHexSpecifier));
        numArray[index] = (byte) ch;
      }
      return numArray;
    }

    private byte[] ConverteToASCii(string aux)
    {
      byte[] numArray = new byte[aux.Length];
      int index = 0;
      foreach (char ch1 in aux)
      {
        char ch2 = Convert.ToChar(int.Parse(ch1.ToString()));
        numArray[index] = (byte) ch2;
        ++index;
      }
      return numArray;
    }

    private bool CriptografaRB1Empregados(string pathCripto)
    {
      try
      {
        byte[] buffer1 = new byte[2];
        byte[] buffer2 = new byte[512];
        int CheckSumBufferCripto = 0;
        Random random = new Random(DateTime.Now.Millisecond);
        random.NextBytes(buffer1);
        random.NextBytes(buffer2);
        ushort uint16 = BitConverter.ToUInt16(buffer1, 0);
        File.WriteAllLines(pathCripto, new string[0]);
        string aux1 = "0001";
        string s1 = "16";
        string str1 = "0";
        string str2 = "2";
        string str3 = this.VersaoIR.ToString();
        string str4 = this.NumeroSerie.Trim().PadLeft(18, '0').PadRight(32, '0');
        string aux2 = "".PadLeft(444, '0');
        Empregado empregado1 = !this.ChamadoPelaAT ? new Empregado() : (Empregado) new EmpregadoAT();
        SortableBindingList<Empregado> sortableBindingList = new SortableBindingList<Empregado>();
        if (!this._chamadaSDK)
        {
          if (RegistrySingleton.GetInstance().UTILIZA_GRUPOS)
          {
            RepBase repBase = (!this.ChamadoPelaAT ? new RepBase() : (RepBase) new RepBaseAT()).PesquisarRepPorID(this.repId);
            if (this.repId != 0)
            {
              if (repBase.grupoId == 0)
              {
                sortableBindingList = empregado1.PesquisarEmpregadosPorEmpregadorOrdenadoPis(this.objempregador.EmpregadorId);
              }
              else
              {
                List<Empregado> empregadoList = !this.ChamadoPelaAT ? GrupoRepXempregadoBI.PesquisarEmpregadosDoGrupoRepPlus(repBase.grupoId, this.objempregador.EmpregadorId) : GrupoRepXempregadoBI.PesquisarEmpregadosDoGrupoRepPlusAT(repBase.grupoId, this.objempregador.EmpregadorId);
                sortableBindingList.Clear();
                foreach (Empregado empregado2 in empregadoList)
                  sortableBindingList.Add(empregado2);
              }
            }
            else
              sortableBindingList = empregado1.PesquisarEmpregadosPorEmpregadorOrdenadoPis(this.objempregador.EmpregadorId);
          }
          else
            sortableBindingList = empregado1.PesquisarEmpregadosPorEmpregadorOrdenadoPis(this.objempregador.EmpregadorId);
          if (RegistrySingleton.GetInstance().FORMATO_WIEGAND_DEC)
          {
            foreach (Empregado empDec in (Collection<Empregado>) sortableBindingList)
              this.ConvertToWiegandDecimal(empDec);
          }
        }
        else
        {
          sortableBindingList.Clear();
          foreach (Empregado empregado3 in this.ListaEmpregadosSDK)
            sortableBindingList.Add(empregado3);
        }
        this.NotificaUsuario(Resources.msgPROCESSANDO_DADOS_RB1, 2);
        byte[] numArray1 = new byte[1000000];
        int destinationIndex1 = 0;
        int num1 = 0;
        foreach (Empregado empregado4 in (Collection<Empregado>) sortableBindingList)
        {
          byte[] numArray2 = new byte[6];
          byte[] numArray3 = new byte[52];
          byte[] numArray4 = new byte[16];
          byte[] numArray5 = new byte[2];
          byte[] numArray6 = new byte[8];
          byte[] numArray7 = new byte[8];
          byte[] numArray8 = new byte[8];
          string str5 = empregado4.Pis.Replace(".", string.Empty).Replace("-", string.Empty).PadLeft(12, '0');
          if (!this._chamadaSDK && RegistrySingleton.GetInstance().VALIDA_PIS && Convert.ToInt64(str5).ToString().Length != 12 && !CriptoBI.ValidaPIS(str5.Substring(1, 11)))
          {
            this.NotificaUsuario("PIS de empregado inválido!", 1);
            return false;
          }
          string str6 = empregado4.CartaoBarras.ToString().PadLeft(16, '0');
          string str7 = empregado4.CartaoProx.ToString().PadLeft(16, '0');
          string str8 = empregado4.Teclado.ToString().PadLeft(16, '0');
          string str9 = empregado4.Nome.ToUpper().ToString().PadRight(52, Convert.ToChar(176));
          for (int startIndex = 0; startIndex < empregado4.Nome.Length; ++startIndex)
          {
            if (!CriptoBI.ValidaCaracteresInvalidos(empregado4.Nome.Substring(startIndex, 1), false))
            {
              this.NotificaUsuario("Caracter inválido no nome do empregado!", 1);
              return false;
            }
          }
          string nomeExibicao = empregado4.NomeExibicao;
          for (int startIndex = 0; startIndex < empregado4.NomeExibicao.Length; ++startIndex)
          {
            if (!CriptoBI.ValidaCaracteresInvalidos(empregado4.NomeExibicao.Substring(startIndex, 1), false))
            {
              this.NotificaUsuario("Caracter inválido no nome de exibição do empregado!", 1);
              return false;
            }
          }
          for (int index = 0; index < 6; ++index)
          {
            string s2 = str5.Substring(index * 2, 2);
            numArray2[index] = byte.Parse(s2, NumberStyles.HexNumber);
          }
          byte[] bytes1 = Encoding.Default.GetBytes(str9.ToCharArray());
          if (nomeExibicao.ToString().Trim().Length > 0)
          {
            byte[] bytes2 = Encoding.Default.GetBytes(nomeExibicao.ToCharArray());
            Array.Copy((Array) bytes2, 0, (Array) numArray4, 0, bytes2.Length);
          }
          if (!empregado4.VerificaBiometria && !empregado4.DuplaVerificacao)
          {
            if (empregado4.Senha.ToString().Length > 0)
            {
              for (int index = 0; index < 2; ++index)
              {
                string s3 = empregado4.Senha.ToString().Substring(index * 2, 2);
                numArray5[index] = byte.Parse(s3, NumberStyles.HexNumber);
              }
            }
            else
            {
              for (int index = 0; index < 2; ++index)
                numArray5[index] = byte.MaxValue;
            }
          }
          else
          {
            for (int index = 0; index < 2; ++index)
              numArray5[index] = empregado4.VerificaBiometria ? (byte) 238 : (byte) 221;
          }
          for (int index = 0; index < 8; ++index)
          {
            string s4 = str6.Substring(index * 2, 2);
            numArray6[index] = byte.Parse(s4, NumberStyles.HexNumber);
          }
          for (int index = 0; index < 8; ++index)
          {
            string s5 = str7.Substring(index * 2, 2);
            numArray7[index] = byte.Parse(s5, NumberStyles.HexNumber);
          }
          for (int index = 0; index < 8; ++index)
          {
            string s6 = str8.Substring(index * 2, 2);
            numArray8[index] = byte.Parse(s6, NumberStyles.HexNumber);
          }
          Array.Copy((Array) numArray2, 0, (Array) numArray1, destinationIndex1, numArray2.Length);
          int destinationIndex2 = destinationIndex1 + numArray2.Length;
          Array.Copy((Array) bytes1, 0, (Array) numArray1, destinationIndex2, bytes1.Length);
          int destinationIndex3 = destinationIndex2 + bytes1.Length;
          Array.Copy((Array) numArray5, 0, (Array) numArray1, destinationIndex3, numArray5.Length);
          int destinationIndex4 = destinationIndex3 + numArray5.Length;
          Array.Copy((Array) numArray4, 0, (Array) numArray1, destinationIndex4, numArray4.Length);
          int destinationIndex5 = destinationIndex4 + numArray4.Length;
          Array.Copy((Array) numArray6, 0, (Array) numArray1, destinationIndex5, numArray6.Length);
          int destinationIndex6 = destinationIndex5 + numArray6.Length;
          Array.Copy((Array) numArray7, 0, (Array) numArray1, destinationIndex6, numArray7.Length);
          int destinationIndex7 = destinationIndex6 + numArray7.Length;
          Array.Copy((Array) numArray8, 0, (Array) numArray1, destinationIndex7, numArray8.Length);
          destinationIndex1 = destinationIndex7 + numArray8.Length;
          ++num1;
        }
        byte[] asCii1 = this.ConverteHexaToASCii("a55a5aa5");
        byte[] asCii2 = this.ConverteHexaToASCii(num1.ToString("X").PadLeft(4, '0'));
        Array.Reverse((Array) asCii2);
        byte[] asCii3 = this.ConverteHexaToASCii("0001");
        byte[] asCii4 = this.ConverteHexaToASCii("5aa5a55a");
        byte[] registro1 = new byte[1000012];
        int destinationIndex8 = 0;
        Array.Copy((Array) asCii1, 0, (Array) registro1, destinationIndex8, asCii1.Length);
        int destinationIndex9 = destinationIndex8 + asCii1.Length;
        Array.Copy((Array) asCii2, 0, (Array) registro1, destinationIndex9, asCii2.Length);
        int destinationIndex10 = destinationIndex9 + asCii2.Length;
        Array.Copy((Array) asCii3, 0, (Array) registro1, destinationIndex10, asCii3.Length);
        int destinationIndex11 = destinationIndex10 + asCii3.Length;
        Array.Copy((Array) numArray1, 0, (Array) registro1, destinationIndex11, numArray1.Length);
        int destinationIndex12 = destinationIndex11 + numArray1.Length;
        Array.Copy((Array) asCii4, 0, (Array) registro1, destinationIndex12, asCii4.Length);
        int destinationIndex13 = 0;
        string hexastring1 = CriptoBI.HashRegistro(registro1);
        byte[] registro2 = new byte[1000012];
        int tamanhoBytes = numArray1.Length + asCii1.Length + asCii3.Length + asCii4.Length + 2;
        this.NotificaMaxBytesCripto(tamanhoBytes.ToString());
        int num2 = 0;
        int index1 = 0;
        byte[] buffer3 = new byte[asCii1.Length];
        foreach (byte texto in asCii1)
        {
          buffer3[index1] = (byte) short.Parse(this.CriptoTexto(texto, tamanhoBytes, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
          this.NotificaBytesCripto(num2.ToString());
          ++num2;
          ++index1;
          Application.DoEvents();
        }
        Array.Copy((Array) buffer3, 0, (Array) registro2, destinationIndex13, buffer3.Length);
        int destinationIndex14 = destinationIndex13 + buffer3.Length;
        int index2 = 0;
        byte[] asCii5 = this.ConverteHexaToASCii(num1.ToString("X").PadLeft(4, '0'));
        Array.Reverse((Array) asCii5);
        byte[] buffer4 = new byte[asCii5.Length];
        foreach (byte texto in asCii5)
        {
          buffer4[index2] = (byte) short.Parse(this.CriptoTexto(texto, tamanhoBytes, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
          this.NotificaBytesCripto(num2.ToString());
          ++num2;
          ++index2;
          Application.DoEvents();
        }
        Array.Copy((Array) buffer4, 0, (Array) registro2, destinationIndex14, buffer4.Length);
        int destinationIndex15 = destinationIndex14 + buffer4.Length;
        int index3 = 0;
        byte[] buffer5 = new byte[asCii3.Length];
        foreach (byte texto in asCii3)
        {
          buffer5[index3] = (byte) short.Parse(this.CriptoTexto(texto, tamanhoBytes, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
          this.NotificaBytesCripto(num2.ToString());
          ++num2;
          ++index3;
          Application.DoEvents();
        }
        Array.Copy((Array) buffer5, 0, (Array) registro2, destinationIndex15, buffer5.Length);
        int destinationIndex16 = destinationIndex15 + buffer5.Length;
        int index4 = 0;
        StringBuilder stringBuilder = new StringBuilder();
        byte[] buffer6 = new byte[numArray1.Length];
        foreach (byte texto in numArray1)
        {
          buffer6[index4] = (byte) short.Parse(this.CriptoTexto(texto, tamanhoBytes, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
          this.NotificaBytesCripto(num2.ToString());
          ++num2;
          ++index4;
          Application.DoEvents();
        }
        Array.Copy((Array) buffer6, 0, (Array) registro2, destinationIndex16, buffer6.Length);
        int destinationIndex17 = destinationIndex16 + buffer6.Length;
        byte[] numArray9 = new byte[16];
        for (int index5 = 0; index5 < 16; ++index5)
        {
          string s7 = str4.Substring(index5 * 2, 2);
          numArray9[index5] = byte.Parse(s7, NumberStyles.HexNumber);
        }
        int index6 = 0;
        byte[] buffer7 = new byte[asCii4.Length];
        foreach (byte texto in asCii4)
        {
          buffer7[index6] = (byte) short.Parse(this.CriptoTexto(texto, tamanhoBytes, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
          this.NotificaBytesCripto(num2.ToString());
          ++num2;
          ++index6;
          Application.DoEvents();
        }
        Array.Copy((Array) buffer7, 0, (Array) registro2, destinationIndex17, buffer7.Length);
        int num3 = destinationIndex17 + buffer7.Length;
        int index7 = CheckSumBufferCripto % 512;
        buffer2[index7] = buffer1[0];
        buffer2[index7 + 1] = buffer1[1];
        byte[] numArray10 = new byte[512];
        Array.Copy((Array) numArray9, 0, (Array) numArray10, 0, 16);
        byte[] buffer8 = new byte[512];
        for (int index8 = 0; index8 < numArray10.Length; ++index8)
          buffer8[index8] = (byte) short.Parse(this.CriptoTexto(numArray10[index8], tamanhoBytes, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
        string hexastring2 = "";
        for (int index9 = 0; index9 < buffer2.Length; ++index9)
          hexastring2 += buffer2[index9].ToString("X").PadLeft(2, '0');
        byte[] buffer9 = new byte[512];
        string aux3 = str1 + str2 + str3;
        string hexastring3 = tamanhoBytes.ToString("X").PadLeft(8, '0');
        byte[] asCii6 = this.ConverteToASCii(aux1);
        Array.Reverse((Array) asCii6);
        byte[] asCii7 = this.ConverteHexaToASCii(byte.Parse(s1).ToString("X").PadLeft(2, '0'));
        byte[] asCii8 = this.ConverteToASCii(aux3);
        byte[] asCii9 = this.ConverteHexaToASCii(hexastring3);
        Array.Reverse((Array) asCii9);
        byte[] asCii10 = this.ConverteHexaToASCii(CriptoBI.HashRegistro(registro2));
        byte[] asCii11 = this.ConverteHexaToASCii(hexastring1);
        this.ConverteToASCii(aux2);
        int num4 = 0;
        Array.Copy((Array) asCii6, 0, (Array) buffer9, 0, asCii6.Length);
        int destinationIndex18 = num4 + asCii6.Length;
        Array.Copy((Array) asCii7, 0, (Array) buffer9, destinationIndex18, asCii7.Length);
        int destinationIndex19 = destinationIndex18 + asCii7.Length;
        Array.Copy((Array) asCii8, 0, (Array) buffer9, destinationIndex19, asCii8.Length);
        int destinationIndex20 = destinationIndex19 + asCii8.Length;
        Array.Copy((Array) numArray9, 0, (Array) buffer9, destinationIndex20, numArray9.Length);
        int destinationIndex21 = destinationIndex20 + numArray9.Length;
        Array.Copy((Array) asCii9, 0, (Array) buffer9, destinationIndex21, asCii9.Length);
        int destinationIndex22 = destinationIndex21 + asCii9.Length;
        Array.Copy((Array) asCii10, 0, (Array) buffer9, destinationIndex22, asCii10.Length);
        int destinationIndex23 = destinationIndex22 + asCii10.Length;
        Array.Copy((Array) asCii11, 0, (Array) buffer9, destinationIndex23, asCii11.Length);
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer9);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(this.ConverteHexaToASCii(hexastring2));
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer3);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer4);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer5);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer6);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer7);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer8);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(this.ConverteHexaToASCii("FEFFFFFF"));
          binaryWriter.Close();
        }
        this.NotificaUsuario(Resources.msgDADOS_RB1_GERADOS_SUCESSO, 0);
        return true;
      }
      catch (Exception ex)
      {
        if (!File.Exists(pathCripto))
          this.NotificaUsuario("Atenção: Falha ao gravar o arquivo, diretório não encontrado", 1);
        else
          this.NotificaUsuario("Atenção: " + Environment.NewLine + ex.Message, 1);
        return false;
      }
    }

    private void ConvertToWiegandDecimal(Empregado empDec)
    {
      try
      {
        string str1 = empDec.CartaoProx.ToString("X").PadLeft(6, '0');
        string str2 = "" + int.Parse(str1.Substring(0, 2), NumberStyles.HexNumber).ToString() + int.Parse(str1.Substring(2, 4), NumberStyles.HexNumber).ToString();
        empDec.CartaoProx = Convert.ToUInt64(str2);
      }
      catch
      {
      }
    }

    private void ConvertToWiegandFC(Empregado empFC)
    {
      try
      {
        string str = empFC.CartaoProx.ToString().PadLeft(8, '0');
        string s = Convert.ToInt16(str.Substring(0, 3)).ToString("X") + Convert.ToInt64(str.Substring(3)).ToString("X");
        empFC.CartaoProx = ulong.Parse(s, NumberStyles.HexNumber);
      }
      catch
      {
      }
    }

    private bool CriptografaRB1Empregador(string pathCripto)
    {
      try
      {
        byte[] buffer1 = new byte[2];
        byte[] buffer2 = new byte[512];
        int CheckSumBufferCripto = 0;
        Random random = new Random(DateTime.Now.Millisecond);
        random.NextBytes(buffer1);
        random.NextBytes(buffer2);
        ushort uint16 = BitConverter.ToUInt16(buffer1, 0);
        File.WriteAllLines(pathCripto, new string[0]);
        string aux1 = "0001";
        string s1 = "16";
        string str1 = "0";
        string str2 = "6";
        string str3 = this.VersaoIR.ToString();
        string str4 = this.NumeroSerie.Trim().PadLeft(18, '0').PadRight(32, '0');
        string aux2 = "".PadLeft(444, '0');
        Empregador empregador1 = new Empregador();
        RepBase repBase1 = new RepBase();
        if (!this._chamadaSDK)
        {
          Empregador empregador2 = !this.ChamadoPelaAT ? new Empregador() : (Empregador) new EmpregadorAT();
          RepBase repBase2 = !this.ChamadoPelaAT ? new RepBase() : (RepBase) new RepBaseAT();
          empregador1 = empregador2.PesquisarEmpregadorPorID(this.objempregador.EmpregadorId);
          repBase1 = repBase2.PesquisarRepPorID(this.repId);
        }
        else
        {
          empregador1.Cei = this._EmpregadorSDK.Cei;
          empregador1.Cnpj = this._EmpregadorSDK.Cnpj;
          empregador1.Cpf = this._EmpregadorSDK.Cpf;
          empregador1.EmpregadorDesc = this._EmpregadorSDK.RazaoSocial;
          empregador1.RazaoSocial = this._EmpregadorSDK.RazaoSocial;
          empregador1.Local = this._EmpregadorSDK.Local;
          empregador1.isCnpj = this._EmpregadorSDK.isCnpj;
        }
        this.NotificaUsuario(Resources.msgPROCESSANDO_DADOS_RB1, 2);
        byte[] numArray1 = new byte[264];
        int num1 = 0;
        byte[] numArray2 = new byte[7];
        byte[] numArray3 = new byte[6];
        byte[] numArray4 = new byte[150];
        byte[] numArray5 = new byte[100];
        byte num2;
        int num3;
        if (!empregador1.isCnpj)
        {
          num2 = (byte) 2;
          num3 = (int) Convert.ToByte(Convert.ToChar(num2.ToString()));
        }
        else
        {
          num2 = (byte) 1;
          num3 = (int) Convert.ToByte(Convert.ToChar(num2.ToString()));
        }
        byte num4 = (byte) num3;
        numArray1[0] = num4;
        int destinationIndex1 = num1 + 1;
        string str5;
        if (empregador1.isCnpj)
        {
          str5 = empregador1.Cnpj.Replace(",", string.Empty).Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty).PadLeft(14, '0');
          if (!CriptoBI.ValidaCNPJ(empregador1.Cnpj))
          {
            this.NotificaUsuario("CNPJ de empregador inválido!", 1);
            return false;
          }
        }
        else
        {
          str5 = empregador1.Cpf.Replace(",", string.Empty).Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty).PadLeft(14, '0');
          if (!CriptoBI.ValidaCPF(empregador1.Cpf))
          {
            this.NotificaUsuario("CPF de empregador inválido!", 1);
            return false;
          }
        }
        for (int index = 0; index < 7; ++index)
        {
          string s2 = str5.Substring(index * 2, 2);
          numArray2[index] = byte.Parse(s2, NumberStyles.HexNumber);
        }
        Array.Copy((Array) numArray2, 0, (Array) numArray1, destinationIndex1, numArray2.Length);
        int destinationIndex2 = destinationIndex1 + numArray2.Length;
        for (int index = 0; index < 6; ++index)
        {
          string s3 = empregador1.Cei.Substring(index * 2, 2);
          numArray3[index] = byte.Parse(s3, NumberStyles.HexNumber);
        }
        Array.Copy((Array) numArray3, 0, (Array) numArray1, destinationIndex2, numArray3.Length);
        int destinationIndex3 = destinationIndex2 + numArray3.Length;
        string str6 = empregador1.RazaoSocial.ToUpper().PadRight(150, Convert.ToChar(176));
        for (int startIndex = 0; startIndex < empregador1.RazaoSocial.Length; ++startIndex)
        {
          if (!CriptoBI.ValidaCaracteresInvalidos(empregador1.RazaoSocial.Substring(startIndex, 1), false))
          {
            this.NotificaUsuario("Caracter inválido na razão social do empregador!", 1);
            return false;
          }
        }
        byte[] bytes1 = Encoding.Default.GetBytes(str6.ToCharArray());
        Array.Copy((Array) bytes1, 0, (Array) numArray1, destinationIndex3, bytes1.Length);
        int destinationIndex4 = destinationIndex3 + bytes1.Length;
        string str7 = str6.Substring(0, 100);
        if (!this._chamadaSDK)
        {
          if (repBase1.Local != null && !repBase1.Local.Equals(""))
            str7 = repBase1.Local.ToUpper().PadRight(100, Convert.ToChar(176));
        }
        else if (empregador1.Local != null && !empregador1.Local.Equals(""))
          str7 = empregador1.Local.ToUpper().PadRight(100, Convert.ToChar(176));
        for (int startIndex = 0; startIndex < empregador1.Local.Length; ++startIndex)
        {
          if (!CriptoBI.ValidaCaracteresInvalidos(empregador1.Local.Substring(startIndex, 1), false))
          {
            this.NotificaUsuario("Caracter inválido no local do empregador!", 1);
            return false;
          }
        }
        byte[] bytes2 = Encoding.Default.GetBytes(str7.ToCharArray());
        Array.Copy((Array) bytes2, 0, (Array) numArray1, destinationIndex4, bytes2.Length);
        int num5 = destinationIndex4 + bytes2.Length;
        byte[] registro = new byte[264];
        int destinationIndex5 = 0;
        Array.Copy((Array) numArray1, 0, (Array) registro, destinationIndex5, numArray1.Length);
        int num6 = destinationIndex5 + numArray1.Length;
        int destinationIndex6 = 0;
        string hexastring1 = CriptoBI.HashRegistro(registro);
        int length = numArray1.Length;
        this.NotificaMaxBytesCripto(length.ToString());
        int num7 = 0;
        int index1 = 0;
        StringBuilder stringBuilder = new StringBuilder();
        byte[] buffer3 = new byte[numArray1.Length];
        foreach (byte texto in numArray1)
        {
          buffer3[index1] = (byte) short.Parse(this.CriptoTexto(texto, length, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
          this.NotificaBytesCripto(num7.ToString());
          ++num7;
          ++index1;
          Application.DoEvents();
        }
        Array.Copy((Array) buffer3, 0, (Array) registro, destinationIndex6, buffer3.Length);
        num6 = destinationIndex6 + buffer3.Length;
        byte[] numArray6 = new byte[16];
        for (int index2 = 0; index2 < 16; ++index2)
        {
          string s4 = str4.Substring(index2 * 2, 2);
          numArray6[index2] = byte.Parse(s4, NumberStyles.HexNumber);
        }
        int index3 = CheckSumBufferCripto % 512;
        buffer2[index3] = buffer1[0];
        buffer2[index3 + 1] = buffer1[1];
        byte[] numArray7 = new byte[512];
        Array.Copy((Array) numArray6, 0, (Array) numArray7, 0, 16);
        byte[] buffer4 = new byte[512];
        for (int index4 = 0; index4 < numArray7.Length; ++index4)
          buffer4[index4] = (byte) short.Parse(this.CriptoTexto(numArray7[index4], length, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
        string hexastring2 = "";
        for (int index5 = 0; index5 < buffer2.Length; ++index5)
          hexastring2 += buffer2[index5].ToString("X").PadLeft(2, '0');
        byte[] buffer5 = new byte[512];
        string aux3 = str1 + str2 + str3;
        string hexastring3 = length.ToString("X").PadLeft(8, '0');
        byte[] asCii1 = this.ConverteToASCii(aux1);
        Array.Reverse((Array) asCii1);
        num2 = byte.Parse(s1);
        byte[] asCii2 = this.ConverteHexaToASCii(num2.ToString("X").PadLeft(2, '0'));
        byte[] asCii3 = this.ConverteToASCii(aux3);
        byte[] asCii4 = this.ConverteHexaToASCii(hexastring3);
        Array.Reverse((Array) asCii4);
        byte[] asCii5 = this.ConverteHexaToASCii(CriptoBI.HashRegistro(registro));
        byte[] asCii6 = this.ConverteHexaToASCii(hexastring1);
        this.ConverteToASCii(aux2);
        int num8 = 0;
        Array.Copy((Array) asCii1, 0, (Array) buffer5, 0, asCii1.Length);
        int destinationIndex7 = num8 + asCii1.Length;
        Array.Copy((Array) asCii2, 0, (Array) buffer5, destinationIndex7, asCii2.Length);
        int destinationIndex8 = destinationIndex7 + asCii2.Length;
        Array.Copy((Array) asCii3, 0, (Array) buffer5, destinationIndex8, asCii3.Length);
        int destinationIndex9 = destinationIndex8 + asCii3.Length;
        Array.Copy((Array) numArray6, 0, (Array) buffer5, destinationIndex9, numArray6.Length);
        int destinationIndex10 = destinationIndex9 + numArray6.Length;
        Array.Copy((Array) asCii4, 0, (Array) buffer5, destinationIndex10, asCii4.Length);
        int destinationIndex11 = destinationIndex10 + asCii4.Length;
        Array.Copy((Array) asCii5, 0, (Array) buffer5, destinationIndex11, asCii5.Length);
        int destinationIndex12 = destinationIndex11 + asCii5.Length;
        Array.Copy((Array) asCii6, 0, (Array) buffer5, destinationIndex12, asCii6.Length);
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer5);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(this.ConverteHexaToASCii(hexastring2));
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer3);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer4);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(this.ConverteHexaToASCii("FEFFFFFF"));
          binaryWriter.Close();
        }
        this.NotificaUsuario(Resources.msgDADOS_RB1_GERADOS_SUCESSO, 0);
        return true;
      }
      catch (Exception ex)
      {
        if (!File.Exists(pathCripto))
          this.NotificaUsuario("Atenção: Falha ao gravar o arquivo, diretório não encontrado", 1);
        else
          this.NotificaUsuario("Atenção: " + Environment.NewLine + ex.Message, 1);
        return false;
      }
    }

    private bool CriptografaRB1ConfiguracoesRep(string pathCripto, int tipoConfiguracao)
    {
      try
      {
        byte[] buffer1 = new byte[2];
        byte[] buffer2 = new byte[512];
        int CheckSumBufferCripto = 0;
        Random random = new Random(DateTime.Now.Millisecond);
        random.NextBytes(buffer1);
        random.NextBytes(buffer2);
        ushort uint16 = BitConverter.ToUInt16(buffer1, 0);
        File.WriteAllLines(pathCripto, new string[0]);
        string aux1 = "0001";
        string s1 = "16";
        string aux2 = "0";
        string s2 = tipoConfiguracao.ToString();
        string aux3 = this.VersaoIR.ToString();
        string str1 = this.NumeroSerie.Trim().PadLeft(18, '0').PadRight(32, '0');
        string aux4 = "".PadLeft(444, '0');
        RepBase repBase1 = new RepBase();
        RepBio repBio = new RepBio();
        FormatoCartao formatoCartao1 = new FormatoCartao();
        FormatoCartao formatoCartao2 = new FormatoCartao();
        Relogio relogio1 = new Relogio();
        Relogio relogio2;
        FormatoCartao formatoCartao3;
        FormatoCartao formatoCartao4;
        if (!this._chamadaSDK)
        {
          RepBase repBase2 = !this.ChamadoPelaAT ? new RepBase() : (RepBase) new RepBaseAT();
          relogio2 = new Relogio().PesquisarHorVeraoMulti();
          repBase1 = repBase2.PesquisarRepPorID(this.repId);
          formatoCartao3 = FormatoCartao.PesquisarFormatoCartaoBarrasByRepIdRepPlus(this.repId);
          formatoCartao4 = FormatoCartao.PesquisarFormatoCartaoProxByRepIdRepPlus(this.repId);
        }
        else
        {
          relogio2 = this.RelogioRepSDK;
          formatoCartao3 = this.FormatoCartaoBarrasSDK;
          formatoCartao4 = this._FormatoCartaoProxSDK;
          repBase1.ChaveComunicacao = this._ConfigRepSDK.ChaveComunicacao;
          repBase1.SenhaBio = this._ConfigRepSDK.SenhaBio;
          repBase1.SenhaComunic = this._ConfigRepSDK.SenhaComunic;
          repBase1.SenhaRelogio = this._ConfigRepSDK.SenhaRelogio;
          repBase1.Serial = this._ConfigRepSDK.Serial;
          repBase1.tempoEspera = this._ConfigRepSDK.tempoEspera;
          repBase1.TerminalId = this._ConfigRepSDK.TipoTerminalId;
          repBase1.repClient = this._ConfigRepSDK.repClient;
          repBase1.redeRemota = this._ConfigRepSDK.redeRemota;
          repBase1.portaServidor = this._ConfigRepSDK.portaServidor;
          repBase1.portaVariavel = this._ConfigRepSDK.PortaVariavel;
          repBase1.Porta = this._ConfigRepSDK.Port;
          repBase1.nomeServidor = this._ConfigRepSDK.nomeServidor;
          repBase1.mascaraRede = this._ConfigRepSDK.mascaraRede;
          repBase1.Gateway = this._ConfigRepSDK.Gateway;
          repBase1.ipServidor = this._ConfigRepSDK.ipServidor;
          repBase1.TipoConexaoDNS = this._ConfigRepSDK.TipoConexaoDNS;
          repBase1.DNS = this._ConfigRepSDK.DNS;
          repBase1.intervaloConexao = this._ConfigRepSDK.intervaloConexao;
          if (tipoConfiguracao == 15 || tipoConfiguracao == 21)
          {
            if (this._ConfigRepSDK.Local != null)
              repBase1.Local = this._ConfigRepSDK.Local;
            repBio.BioCaptura = this._AjusteBiometricoSDK.BioCaptura;
            repBio.BioFiltro = this._AjusteBiometricoSDK.BioFiltro;
            repBio.BioIdent = this._AjusteBiometricoSDK.BioIdent;
            repBio.BioTimeOut = this._AjusteBiometricoSDK.BioTimeOut;
            repBio.BioVerif = this._AjusteBiometricoSDK.BioVerif;
            repBio.BioDedoDuplicado = this._AjusteBiometricoSDK.BioDedoDuplicado;
            repBio.BioLFD = this._AjusteBiometricoSDK.BioLFD;
            repBio.BioTimeOutCama = this._AjusteBiometricoSDK.BioTimeOutCama;
            repBio.BioIdentCama = this._AjusteBiometricoSDK.BioIdentCama;
            repBio.IdentSagem = this._AjusteBiometricoSDK.IdentSagem;
            repBio.VerifSagem = this._AjusteBiometricoSDK.VerifSagem;
            repBio.TimeoutSagem = this._AjusteBiometricoSDK.TimeoutSagem;
            repBio.DedoDuplicadoSagem = this._AjusteBiometricoSDK.DedoDuplicadoSagem;
            repBio.AdvancedMadchine = this._AjusteBiometricoSDK.AdvancedMadchine;
            repBio.FFD = this._AjusteBiometricoSDK.FFD;
          }
        }
        this.NotificaUsuario(Resources.msgPROCESSANDO_DADOS_RB1, 2);
        byte[] numArray1 = new byte[100];
        int length1 = 100;
        switch (tipoConfiguracao)
        {
          case 15:
            length1 = 724;
            break;
          case 21:
            length1 = 792;
            break;
        }
        byte[] numArray2 = new byte[length1];
        int destinationIndex1 = 0;
        byte[] numArray3 = new byte[10];
        byte[] numArray4 = new byte[12];
        byte[] numArray5 = new byte[16];
        byte[] numArray6 = new byte[4];
        byte[] numArray7 = new byte[2];
        byte[] numArray8 = new byte[4];
        byte[] numArray9 = new byte[4];
        byte[] numArray10 = new byte[3];
        byte[] numArray11 = new byte[3];
        byte[] numArray12 = new byte[3];
        byte[] numArray13 = new byte[16];
        byte[] numArray14 = new byte[16];
        byte[] numArray15 = new byte[2];
        byte[] numArray16 = new byte[512];
        byte[] numArray17 = new byte[4];
        DateTime dateTime;
        if (relogio2.InicioHorVerao.Year == 1900)
        {
          numArray3[2] = (byte) 0;
          numArray3[1] = (byte) 0;
          numArray3[0] = (byte) 0;
          numArray3[3] = (byte) 0;
          numArray3[4] = (byte) 0;
        }
        else
        {
          byte[] numArray18 = numArray3;
          dateTime = relogio2.InicioHorVerao;
          int num = (int) (byte) (dateTime.Year % 100);
          numArray18[2] = (byte) num;
          byte[] numArray19 = numArray3;
          dateTime = relogio2.InicioHorVerao;
          int month = (int) (byte) dateTime.Month;
          numArray19[1] = (byte) month;
          byte[] numArray20 = numArray3;
          dateTime = relogio2.InicioHorVerao;
          int day = (int) (byte) dateTime.Day;
          numArray20[0] = (byte) day;
          byte[] numArray21 = numArray3;
          dateTime = relogio2.InicioHorVerao;
          int hour = (int) (byte) dateTime.Hour;
          numArray21[3] = (byte) hour;
          byte[] numArray22 = numArray3;
          dateTime = relogio2.InicioHorVerao;
          int minute = (int) (byte) dateTime.Minute;
          numArray22[4] = (byte) minute;
        }
        dateTime = relogio2.FimHorVerao;
        if (dateTime.Year == 1900)
        {
          numArray3[7] = (byte) 0;
          numArray3[6] = (byte) 0;
          numArray3[5] = (byte) 0;
          numArray3[8] = (byte) 0;
          numArray3[9] = (byte) 0;
        }
        else
        {
          byte[] numArray23 = numArray3;
          dateTime = relogio2.FimHorVerao;
          int num = (int) (byte) (dateTime.Year % 100);
          numArray23[7] = (byte) num;
          byte[] numArray24 = numArray3;
          dateTime = relogio2.FimHorVerao;
          int month = (int) (byte) dateTime.Month;
          numArray24[6] = (byte) month;
          byte[] numArray25 = numArray3;
          dateTime = relogio2.FimHorVerao;
          int day = (int) (byte) dateTime.Day;
          numArray25[5] = (byte) day;
          byte[] numArray26 = numArray3;
          dateTime = relogio2.FimHorVerao;
          int hour = (int) (byte) dateTime.Hour;
          numArray26[8] = (byte) hour;
          byte[] numArray27 = numArray3;
          dateTime = relogio2.FimHorVerao;
          int minute = (int) (byte) dateTime.Minute;
          numArray27[9] = (byte) minute;
        }
        byte[] numArray28 = new byte[numArray3.Length];
        int index1 = 0;
        foreach (byte num in numArray3)
        {
          numArray28[index1] = byte.Parse(num.ToString(), NumberStyles.HexNumber);
          ++index1;
        }
        Array.Copy((Array) numArray28, 0, (Array) numArray2, destinationIndex1, numArray3.Length);
        int destinationIndex2 = destinationIndex1 + numArray3.Length;
        for (int index2 = 0; index2 < 3; ++index2)
        {
          string s3 = repBase1.SenhaComunic.Substring(index2 * 2, 2);
          numArray10[index2] = byte.Parse(s3, NumberStyles.HexNumber);
        }
        Array.Copy((Array) numArray10, 0, (Array) numArray2, destinationIndex2, numArray10.Length);
        int destinationIndex3 = destinationIndex2 + numArray10.Length;
        if (repBase1.SenhaBio.Length > 0)
        {
          for (int index3 = 0; index3 < 3; ++index3)
          {
            string s4 = repBase1.SenhaBio.Substring(index3 * 2, 2);
            numArray11[index3] = byte.Parse(s4, NumberStyles.HexNumber);
          }
        }
        else
        {
          for (int index4 = 0; index4 < 3; ++index4)
            numArray11[index4] = byte.MaxValue;
        }
        Array.Copy((Array) numArray11, 0, (Array) numArray2, destinationIndex3, numArray11.Length);
        int destinationIndex4 = destinationIndex3 + numArray11.Length;
        for (int index5 = 0; index5 < 3; ++index5)
        {
          string s5 = repBase1.SenhaRelogio.Substring(index5 * 2, 2);
          numArray12[index5] = byte.Parse(s5, NumberStyles.HexNumber);
        }
        Array.Copy((Array) numArray12, 0, (Array) numArray2, destinationIndex4, numArray12.Length);
        int destinationIndex5 = destinationIndex4 + numArray12.Length;
        string[] strArray1 = formatoCartao3.formatoCartao.Split(' ');
        for (int index6 = 0; index6 < strArray1.Length; ++index6)
          numArray13[index6] = Convert.ToByte(strArray1[index6]);
        Array.Copy((Array) numArray13, 0, (Array) numArray2, destinationIndex5, numArray13.Length);
        int destinationIndex6 = destinationIndex5 + numArray13.Length;
        string[] strArray2 = formatoCartao4.formatoCartao.Split(' ');
        for (int index7 = 0; index7 < strArray2.Length; ++index7)
          numArray14[index7] = Convert.ToByte(strArray2[index7]);
        Array.Copy((Array) numArray14, 0, (Array) numArray2, destinationIndex6, numArray14.Length);
        int index8 = destinationIndex6 + numArray14.Length;
        numArray2[index8] = (byte) formatoCartao3.numDigitosFixos;
        int index9 = index8 + 1;
        int num1 = 0;
        int num2 = 0;
        if (!this._chamadaSDK)
        {
          switch (formatoCartao3.formatoCartaoID)
          {
            case 0:
              num1 = 0;
              break;
            case 3:
              num1 = 1;
              break;
            case 4:
              num1 = 2;
              break;
            case 5:
              num1 = 4;
              break;
            case 6:
              num1 = 3;
              break;
            case 8:
              num1 = tipoConfiguracao != 8 ? 5 : 0;
              break;
            case 9:
              num1 = tipoConfiguracao != 8 ? 5 : 0;
              break;
          }
          switch (formatoCartao4.formatoCartaoID)
          {
            case 1:
            case 11:
              num2 = 1;
              break;
            case 2:
            case 12:
            case 13:
            case 14:
              num2 = 2;
              break;
            case 7:
              num2 = 0;
              break;
          }
        }
        else
        {
          num1 = formatoCartao3.formatoCartaoID;
          if (tipoConfiguracao == 8 && formatoCartao3.formatoCartaoID == 5)
            num1 = 0;
          num2 = formatoCartao4.formatoCartaoID;
        }
        numArray2[index9] = (byte) num1;
        int index10 = index9 + 1;
        numArray2[index10] = (byte) num2;
        int destinationIndex7 = index10 + 1;
        if (tipoConfiguracao == 21)
        {
          byte[] numArray29 = new byte[55];
          ConfiguracaoBarras20 configuracaoBarras20 = this._chamadaSDK ? this._Barras20SDK : new ConfiguracaoBarras20().Pesquisar(this.repId);
          int index11 = 1;
          numArray29[0] = configuracaoBarras20.ignorarFormatoPrincipal ? (byte) 1 : (byte) 0;
          numArray29[index11] = configuracaoBarras20.tab1QtdDigitos == 0 ? byte.MaxValue : (byte) configuracaoBarras20.tab1QtdDigitos;
          int destinationIndex8 = index11 + 1;
          byte[] numArray30 = new byte[16];
          string[] strArray3;
          try
          {
            strArray3 = configuracaoBarras20.tab1DigitosLidos.Trim().Split(' ');
          }
          catch
          {
            strArray3 = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00".Split(' ');
          }
          for (int index12 = 0; index12 < strArray3.Length; ++index12)
            numArray30[index12] = Convert.ToByte(strArray3[index12]);
          Array.Copy((Array) numArray30, 0, (Array) numArray29, destinationIndex8, numArray30.Length);
          int index13 = destinationIndex8 + numArray30.Length;
          numArray29[index13] = (byte) configuracaoBarras20.tipoPadraoRep(configuracaoBarras20.tab1TipoCartao);
          int index14 = index13 + 1;
          numArray29[index14] = configuracaoBarras20.tab2QtdDigitos == 0 ? byte.MaxValue : (byte) configuracaoBarras20.tab2QtdDigitos;
          int destinationIndex9 = index14 + 1;
          byte[] numArray31 = new byte[16];
          string[] strArray4;
          try
          {
            strArray4 = configuracaoBarras20.tab2DigitosLidos.Trim().Split(' ');
          }
          catch
          {
            strArray4 = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00".Split(' ');
          }
          for (int index15 = 0; index15 < strArray4.Length; ++index15)
            numArray31[index15] = Convert.ToByte(strArray4[index15]);
          Array.Copy((Array) numArray31, 0, (Array) numArray29, destinationIndex9, numArray31.Length);
          int index16 = destinationIndex9 + numArray31.Length;
          numArray29[index16] = (byte) configuracaoBarras20.tipoPadraoRep(configuracaoBarras20.tab2TipoCartao);
          int index17 = index16 + 1;
          numArray29[index17] = configuracaoBarras20.tab3QtdDigitos == 0 ? byte.MaxValue : (byte) configuracaoBarras20.tab3QtdDigitos;
          int destinationIndex10 = index17 + 1;
          byte[] numArray32 = new byte[16];
          string[] strArray5;
          try
          {
            strArray5 = configuracaoBarras20.tab3DigitosLidos.Trim().Split(' ');
          }
          catch
          {
            strArray5 = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00".Split(' ');
          }
          for (int index18 = 0; index18 < strArray5.Length; ++index18)
            numArray32[index18] = Convert.ToByte(strArray5[index18]);
          Array.Copy((Array) numArray32, 0, (Array) numArray29, destinationIndex10, numArray32.Length);
          int index19 = destinationIndex10 + numArray32.Length;
          numArray29[index19] = (byte) configuracaoBarras20.tipoPadraoRep(configuracaoBarras20.tab3TipoCartao);
          Array.Copy((Array) numArray29, 0, (Array) numArray2, destinationIndex7, numArray29.Length);
          destinationIndex7 += numArray29.Length;
        }
        numArray2[destinationIndex7] = repBase1.repClient ? (byte) 1 : (byte) 0;
        int destinationIndex11 = destinationIndex7 + 1;
        try
        {
          if (repBase1.ipServidor != null)
          {
            if (!repBase1.ipServidor.Trim().Equals(""))
            {
              string[] strArray6 = repBase1.ipServidor.Split('.');
              for (int index20 = 0; index20 < numArray6.Length; ++index20)
                numArray6[index20] = byte.Parse(strArray6[index20]);
            }
          }
        }
        catch
        {
        }
        Array.Copy((Array) numArray6, 0, (Array) numArray2, destinationIndex11, numArray6.Length);
        int destinationIndex12 = destinationIndex11 + numArray6.Length;
        numArray7[0] = BitConverter.GetBytes(repBase1.portaServidor)[0];
        numArray7[1] = BitConverter.GetBytes(repBase1.portaServidor)[1];
        Array.Copy((Array) numArray7, 0, (Array) numArray2, destinationIndex12, numArray7.Length);
        int destinationIndex13 = destinationIndex12 + numArray7.Length;
        if (repBase1.mascaraRede != null && !repBase1.mascaraRede.Trim().Equals(""))
        {
          string[] strArray7 = repBase1.mascaraRede.Split('.');
          for (int index21 = 0; index21 < numArray8.Length; ++index21)
            numArray8[index21] = byte.Parse(strArray7[index21]);
        }
        Array.Copy((Array) numArray8, 0, (Array) numArray2, destinationIndex13, numArray8.Length);
        int destinationIndex14 = destinationIndex13 + numArray8.Length;
        if (repBase1.Gateway != null && !repBase1.Gateway.Trim().Equals(""))
        {
          string[] strArray8 = repBase1.Gateway.Split('.');
          for (int index22 = 0; index22 < numArray9.Length; ++index22)
            numArray9[index22] = byte.Parse(strArray8[index22]);
        }
        Array.Copy((Array) numArray9, 0, (Array) numArray2, destinationIndex14, numArray9.Length);
        int index23 = destinationIndex14 + numArray9.Length;
        numArray2[index23] = (byte) repBase1.intervaloConexao;
        int destinationIndex15 = index23 + 1;
        byte[] bytes1 = Encoding.Default.GetBytes(repBase1.ChaveComunicacao);
        Array.Copy((Array) bytes1, 0, (Array) numArray2, destinationIndex15, bytes1.Length);
        int destinationIndex16 = destinationIndex15 + bytes1.Length;
        ArquivoCFGBI arquivoCfgbi = new ArquivoCFGBI();
        ArquivoCFGEntity arquivoCfgEntity = this._chamadaSDK ? this._arquivoCFGEntSDK : arquivoCfgbi.PesquisarArquivoCFGPorRepID(this.repId);
        if (!arquivoCfgEntity.CFG.Equals("") && !arquivoCfgEntity.CFG.Equals("\0\0\0\0\0\0\0\0\0\0\0\0") && !arquivoCfgEntity.CFG.Equals("ÿÿÿÿÿÿÿÿÿÿÿÿ"))
        {
          byte[] byteArray = PadraoTLM.GetByteArray(arquivoCfgEntity.CFG);
          byte[] numArray33 = new byte[109];
          StringBuilder stringBuilder = new StringBuilder();
          for (int index24 = 1; index24 < 109; ++index24)
            numArray33[index24] = (byte) ((uint) byteArray[index24] ^ 105U);
          for (int index25 = 59; index25 < numArray33.Length; ++index25)
            stringBuilder.Append((char) numArray33[index25]);
          for (int index26 = 0; index26 < 10; ++index26)
            numArray4[index26] = Convert.ToByte((int) numArray33[index26 * 2 + 84] - 48);
          numArray4[10] = byte.Parse(stringBuilder.ToString().Substring(46, 2), NumberStyles.HexNumber);
          numArray4[11] = byte.Parse(stringBuilder.ToString().Substring(44, 2), NumberStyles.HexNumber);
        }
        Array.Copy((Array) numArray4, 0, (Array) numArray2, destinationIndex16, numArray4.Length);
        int destinationIndex17 = destinationIndex16 + numArray4.Length;
        int num3;
        if (tipoConfiguracao == 15 || tipoConfiguracao == 21)
        {
          if (!this._chamadaSDK)
            repBio = new RepBio().RecuperaAjusteBiometricoPorRep(this.RepId);
          numArray2[destinationIndex17] = (byte) repBio.BioIdent;
          int index27 = destinationIndex17 + 1;
          numArray2[index27] = (byte) repBio.BioVerif;
          int index28 = index27 + 1;
          numArray2[index28] = (byte) repBio.BioFiltro;
          int index29 = index28 + 1;
          numArray2[index29] = (byte) repBio.BioCaptura;
          int index30 = index29 + 1;
          numArray2[index30] = (byte) repBio.BioTimeOut;
          int index31 = index30 + 1;
          numArray2[index31] = (byte) repBio.BioLFD;
          int index32 = index31 + 1;
          numArray2[index32] = (byte) repBio.BioIdentCama;
          int index33 = index32 + 1;
          numArray2[index33] = (byte) repBio.BioTimeOutCama;
          int index34 = index33 + 1;
          numArray2[index34] = repBio.BioDedoDuplicado ? (byte) 1 : (byte) 0;
          int destinationIndex18 = index34 + 1;
          if (tipoConfiguracao == 21)
          {
            numArray2[destinationIndex18] = (byte) repBio.IdentSagem;
            int index35 = destinationIndex18 + 1;
            numArray2[index35] = (byte) repBio.VerifSagem;
            int index36 = index35 + 1;
            numArray2[index36] = (byte) repBio.BioFiltro;
            int index37 = index36 + 1;
            numArray2[index37] = repBio.AdvancedMadchine;
            int index38 = index37 + 1;
            numArray2[index38] = (byte) repBio.TimeoutSagem;
            int index39 = index38 + 1;
            numArray2[index39] = (byte) repBio.DedoDuplicadoSagem;
            int index40 = index39 + 1;
            numArray2[index40] = (byte) repBio.FFD;
            destinationIndex18 = index40 + 1;
          }
          string str2 = "";
          if (repBase1.Local != null && !repBase1.Local.Equals(""))
            str2 = repBase1.Local.ToUpper().PadRight(100, Convert.ToChar(176));
          for (int startIndex = 0; startIndex < repBase1.Local.Length; ++startIndex)
          {
            if (!CriptoBI.ValidaCaracteresInvalidos(repBase1.Local.Substring(startIndex, 1), false))
            {
              this.NotificaUsuario("Caracter inválido no local do rep!", 1);
              return false;
            }
          }
          byte[] bytes2 = Encoding.Default.GetBytes(str2.ToCharArray());
          Array.Copy((Array) bytes2, 0, (Array) numArray2, destinationIndex18, bytes2.Length);
          int index41 = destinationIndex18 + bytes2.Length;
          numArray2[index41] = (byte) repBase1.TipoConexaoDNS;
          int destinationIndex19 = index41 + 1;
          byte[] bytes3 = Encoding.Default.GetBytes(repBase1.nomeServidor.ToCharArray());
          Array.Copy((Array) bytes3, 0, (Array) numArray16, 0, bytes3.Length);
          Array.Copy((Array) numArray16, 0, (Array) numArray2, destinationIndex19, numArray16.Length);
          int destinationIndex20 = destinationIndex19 + numArray16.Length;
          try
          {
            if (repBase1.DNS != null)
            {
              string[] strArray9 = repBase1.DNS.Split('.');
              for (int index42 = 0; index42 < numArray17.Length; ++index42)
                numArray17[index42] = byte.Parse(strArray9[index42]);
            }
          }
          catch
          {
          }
          Array.Copy((Array) numArray17, 0, (Array) numArray2, destinationIndex20, numArray17.Length);
          int destinationIndex21 = destinationIndex20 + numArray17.Length;
          if (tipoConfiguracao == 21)
          {
            byte[] bytes4 = BitConverter.GetBytes(repBase1.intervaloNuvem);
            Array.Copy((Array) bytes4, 0, (Array) numArray2, destinationIndex21, bytes4.Length);
            int destinationIndex22 = destinationIndex21 + bytes4.Length;
            byte[] numArray34 = new byte[2]
            {
              BitConverter.GetBytes(repBase1.portaNuvem)[0],
              BitConverter.GetBytes(repBase1.portaNuvem)[1]
            };
            Array.Copy((Array) numArray34, 0, (Array) numArray2, destinationIndex22, numArray34.Length);
            num3 = destinationIndex22 + numArray34.Length;
          }
        }
        else
        {
          Array.Copy((Array) numArray15, 0, (Array) numArray2, destinationIndex17, numArray15.Length);
          num3 = destinationIndex17 + numArray15.Length;
        }
        byte[] registro = new byte[length1];
        int destinationIndex23 = 0;
        Array.Copy((Array) numArray2, 0, (Array) registro, destinationIndex23, numArray2.Length);
        int num4 = destinationIndex23 + numArray2.Length;
        int destinationIndex24 = 0;
        string hexastring1 = CriptoBI.HashRegistro(registro);
        int length2 = numArray2.Length;
        this.NotificaMaxBytesCripto(length2.ToString());
        int num5 = 0;
        int index43 = 0;
        StringBuilder stringBuilder1 = new StringBuilder();
        byte[] buffer3 = new byte[numArray2.Length];
        foreach (byte texto in numArray2)
        {
          buffer3[index43] = (byte) short.Parse(this.CriptoTexto(texto, length2, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
          this.NotificaBytesCripto(num5.ToString());
          ++num5;
          ++index43;
          Application.DoEvents();
        }
        Array.Copy((Array) buffer3, 0, (Array) registro, destinationIndex24, buffer3.Length);
        num4 = destinationIndex24 + buffer3.Length;
        byte[] numArray35 = new byte[16];
        for (int index44 = 0; index44 < 16; ++index44)
        {
          string s6 = str1.Substring(index44 * 2, 2);
          numArray35[index44] = byte.Parse(s6, NumberStyles.HexNumber);
        }
        int index45 = CheckSumBufferCripto % 512;
        buffer2[index45] = buffer1[0];
        buffer2[index45 + 1] = buffer1[1];
        byte[] numArray36 = new byte[512];
        Array.Copy((Array) numArray35, 0, (Array) numArray36, 0, 16);
        byte[] buffer4 = new byte[512];
        for (int index46 = 0; index46 < numArray36.Length; ++index46)
          buffer4[index46] = (byte) short.Parse(this.CriptoTexto(numArray36[index46], length2, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
        string hexastring2 = "";
        for (int index47 = 0; index47 < buffer2.Length; ++index47)
          hexastring2 += buffer2[index47].ToString("X").PadLeft(2, '0');
        byte[] buffer5 = new byte[512];
        string str3 = aux2 + s2 + aux3;
        string hexastring3 = length2.ToString("X").PadLeft(8, '0');
        byte[] asCii1 = this.ConverteToASCii(aux1);
        Array.Reverse((Array) asCii1);
        byte[] asCii2 = this.ConverteHexaToASCii(byte.Parse(s1).ToString("X").PadLeft(2, '0'));
        byte[] numArray37 = new byte[3]
        {
          this.ConverteToASCii(aux2)[0],
          this.ConverteHexaToASCii(byte.Parse(s2).ToString("X").PadLeft(2, '0'))[0],
          this.ConverteToASCii(aux3)[0]
        };
        byte[] asCii3 = this.ConverteHexaToASCii(hexastring3);
        Array.Reverse((Array) asCii3);
        byte[] asCii4 = this.ConverteHexaToASCii(CriptoBI.HashRegistro(registro));
        byte[] asCii5 = this.ConverteHexaToASCii(hexastring1);
        this.ConverteToASCii(aux4);
        int num6 = 0;
        Array.Copy((Array) asCii1, 0, (Array) buffer5, 0, asCii1.Length);
        int destinationIndex25 = num6 + asCii1.Length;
        Array.Copy((Array) asCii2, 0, (Array) buffer5, destinationIndex25, asCii2.Length);
        int destinationIndex26 = destinationIndex25 + asCii2.Length;
        Array.Copy((Array) numArray37, 0, (Array) buffer5, destinationIndex26, numArray37.Length);
        int destinationIndex27 = destinationIndex26 + numArray37.Length;
        Array.Copy((Array) numArray35, 0, (Array) buffer5, destinationIndex27, numArray35.Length);
        int destinationIndex28 = destinationIndex27 + numArray35.Length;
        Array.Copy((Array) asCii3, 0, (Array) buffer5, destinationIndex28, asCii3.Length);
        int destinationIndex29 = destinationIndex28 + asCii3.Length;
        Array.Copy((Array) asCii4, 0, (Array) buffer5, destinationIndex29, asCii4.Length);
        int destinationIndex30 = destinationIndex29 + asCii4.Length;
        Array.Copy((Array) asCii5, 0, (Array) buffer5, destinationIndex30, asCii5.Length);
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer5);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(this.ConverteHexaToASCii(hexastring2));
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer3);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(buffer4);
          binaryWriter.Close();
        }
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(pathCripto, FileMode.Append)))
        {
          binaryWriter.Write(this.ConverteHexaToASCii("FEFFFFFF"));
          binaryWriter.Close();
        }
        this.NotificaUsuario(Resources.msgDADOS_RB1_GERADOS_SUCESSO, 0);
        return true;
      }
      catch (Exception ex)
      {
        if (!File.Exists(pathCripto))
          this.NotificaUsuario("Atenção: Falha ao gravar o arquivo, diretório não encontrado", 1);
        else
          this.NotificaUsuario("Atenção: " + Environment.NewLine + ex.Message, 1);
        return false;
      }
    }

    private bool CriptografaRB1Templates(string pathCripto, int tipoTemplate)
    {
      try
      {
        byte[] buffer1 = new byte[2];
        byte[] buffer2 = new byte[512];
        ushort num1 = 0;
        string aux1 = "0001";
        string s1 = "16";
        string aux2 = "0";
        string s2 = tipoTemplate.ToString();
        string aux3 = this.VersaoIR.ToString();
        string str = this.NumeroSerie.Trim().PadLeft(18, '0').PadRight(32, '0');
        string aux4 = "".PadLeft(444, '0');
        TemplateBio templateBio = new TemplateBio();
        SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
        this.NotificaUsuario(Resources.msgPROCESSANDO_DADOS_TEMPLATES_RB1, 2);
        if (!this._chamadaSDK)
        {
          RepBase repBase = (!this.ChamadoPelaAT ? new RepBase() : (RepBase) new RepBaseAT()).PesquisarRepPorID(this.repId);
          if (RegistrySingleton.GetInstance().UTILIZA_GRUPOS && this.repId != 0)
          {
            if (repBase.grupoId == 0)
            {
              if (tipoTemplate == 12 || tipoTemplate == 17)
              {
                foreach (DataRow row in (InternalDataCollectionBase) templateBio.PesquisarTemplatesCAMAPorEmpregadorRepPlusDataSource(this.objempregador).Rows)
                  sortableBindingList.Add(new UsuarioBio()
                  {
                    Nome = row["nome"].ToString(),
                    IdBiometria = ulong.Parse(row["idBiometria"].ToString()),
                    Pis = row["idBiometria"].ToString(),
                    Senha = row["senha"].ToString(),
                    Template1 = row["template1"].ToString(),
                    Template2 = row["template2"].ToString(),
                    TipoTemplate = int.Parse(row["TipoTemplate"].ToString()),
                    InfoNivelSeguranca = (int) byte.MaxValue,
                    NivelSeguranca = (int) byte.MaxValue,
                    EmpregadorID = int.Parse(row["EmpregadorId"].ToString())
                  });
              }
              else
              {
                foreach (DataRow row in (InternalDataCollectionBase) templateBio.PesquisarTemplatesPorEmpregadorRepPlusDataSource(this.objempregador).Rows)
                  sortableBindingList.Add(new UsuarioBio()
                  {
                    Nome = row["nome"].ToString(),
                    IdBiometria = ulong.Parse(row["idBiometria"].ToString()),
                    Pis = row["idBiometria"].ToString(),
                    Senha = row["senha"].ToString(),
                    Template1 = row["template1"].ToString(),
                    Template2 = row["template2"].ToString(),
                    TipoTemplate = int.Parse(row["TipoTemplate"].ToString()),
                    InfoNivelSeguranca = (int) byte.MaxValue,
                    NivelSeguranca = (int) byte.MaxValue,
                    EmpregadorID = int.Parse(row["EmpregadorId"].ToString())
                  });
              }
            }
            else
              sortableBindingList = tipoTemplate == 12 || tipoTemplate == 17 ? GrupoRepXempregadoBI.PesquisarTemplatesCAMAPorEmpregadorDoGrupoRepPlus(repBase.grupoId, this.objempregador.EmpregadorId) : GrupoRepXempregadoBI.PesquisarTemplatesPorEmpregadorDoGrupoRepPlus(repBase.grupoId, this.objempregador.EmpregadorId);
          }
          else if (tipoTemplate == 12 || tipoTemplate == 17)
          {
            foreach (DataRow row in (InternalDataCollectionBase) templateBio.PesquisarTemplatesCAMAPorEmpregadorRepPlusDataSource(this.objempregador).Rows)
              sortableBindingList.Add(new UsuarioBio()
              {
                Nome = row["nome"].ToString(),
                IdBiometria = ulong.Parse(row["idBiometria"].ToString()),
                Pis = row["idBiometria"].ToString(),
                Senha = row["senha"].ToString(),
                Template1 = row["template1"].ToString(),
                Template2 = row["template2"].ToString(),
                TipoTemplate = int.Parse(row["TipoTemplate"].ToString()),
                InfoNivelSeguranca = (int) byte.MaxValue,
                NivelSeguranca = (int) byte.MaxValue,
                EmpregadorID = int.Parse(row["EmpregadorId"].ToString())
              });
          }
          else
          {
            foreach (DataRow row in (InternalDataCollectionBase) templateBio.PesquisarTemplatesPorEmpregadorRepPlusDataSource(this.objempregador).Rows)
              sortableBindingList.Add(new UsuarioBio()
              {
                Nome = row["nome"].ToString(),
                IdBiometria = ulong.Parse(row["idBiometria"].ToString()),
                Pis = row["idBiometria"].ToString(),
                Senha = row["senha"].ToString(),
                Template1 = row["template1"].ToString(),
                Template2 = row["template2"].ToString(),
                TipoTemplate = int.Parse(row["TipoTemplate"].ToString()),
                InfoNivelSeguranca = (int) byte.MaxValue,
                NivelSeguranca = (int) byte.MaxValue,
                EmpregadorID = int.Parse(row["EmpregadorId"].ToString())
              });
          }
        }
        else
        {
          sortableBindingList.Clear();
          foreach (UsuarioBio usuarioBio in this.ListaTemplatesSDK)
          {
            usuarioBio.Senha = "546f7053656372657400000000000000";
            sortableBindingList.Add(usuarioBio);
          }
        }
        byte[] numArray1 = new byte[944000];
        switch (tipoTemplate)
        {
          case 10:
            numArray1 = new byte[876000];
            break;
          case 12:
            numArray1 = new byte[570000];
            break;
          case 17:
            numArray1 = new byte[1072000];
            break;
        }
        int destinationIndex1 = 0;
        int index1 = 0;
        byte[] numArray2 = new byte[2];
        byte[] numArray3 = new byte[2];
        int num2 = sortableBindingList.Count - index1;
        DateTime now = DateTime.Now;
        UsuarioBio[] usuarioBioArray = new UsuarioBio[sortableBindingList.Count];
        foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) sortableBindingList)
        {
          usuarioBioArray[index1] = usuarioBio;
          ++index1;
          Application.DoEvents();
        }
        int num3 = 1;
        int num4 = 1;
        int num5 = numArray1.Length + 2;
        this.NotificaMaxBytesCripto(num5.ToString());
        bool flag;
        if (!this._chamadaSDK)
        {
          ConfiguracoesGerais configuracoesGerais = new ConfiguracoesGerais();
          flag = new ConfiguracoesGerais().PesquisarConfigGerais().SobrescreverDigitais;
        }
        else
          flag = true;
        for (int index2 = 0; index2 < usuarioBioArray.Length; ++index2)
        {
          byte[] numArray4 = new byte[0];
          byte[] numArray5 = new byte[0];
          byte[] numArray6 = new byte[0];
          byte[] numArray7;
          byte[] numArray8;
          if (tipoTemplate == 12 || tipoTemplate == 17)
          {
            numArray7 = new byte[4]
            {
              (byte) 85,
              (byte) 85,
              (byte) 170,
              (byte) 170
            };
            numArray5 = new byte[498];
            numArray8 = new byte[498];
          }
          else
          {
            numArray7 = new byte[4]
            {
              (byte) 195,
              (byte) 0,
              (byte) 0,
              (byte) 0
            };
            numArray5 = new byte[404];
            numArray8 = new byte[404];
          }
          byte[] numArray9 = new byte[4]
          {
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 1
          };
          byte num6 = 0;
          byte[] numArray10 = new byte[11];
          byte[] numArray11 = new byte[16];
          byte maxValue1 = byte.MaxValue;
          byte maxValue2 = byte.MaxValue;
          byte num7 = flag ? (byte) 1 : (byte) 0;
          byte[] numArray12 = new byte[5];
          byte[] numArray13 = new byte[20];
          Array.Copy((Array) numArray7, 0, (Array) numArray1, destinationIndex1, numArray7.Length);
          int index3 = destinationIndex1 + numArray7.Length;
          numArray1[index3] = num6;
          int destinationIndex2 = index3 + 1;
          string PisUsuario = usuarioBioArray[index2].Pis.PadLeft(16, '0');
          StringBuilder stringBuilder1 = new StringBuilder();
          byte[] numArray14 = new byte[8];
          foreach (int pisUsuarioEmByte in this.AbrirPisUsuarioEmBytes(PisUsuario))
          {
            int num8 = pisUsuarioEmByte;
            ++num8;
            stringBuilder1.Append(num8.ToString("x").PadLeft(2, '0'));
          }
          for (int index4 = 0; index4 < 8; ++index4)
          {
            string s3 = stringBuilder1.ToString().Substring(index4 * 2, 2);
            numArray10[index4] = byte.Parse(s3, NumberStyles.HexNumber);
          }
          Array.Copy((Array) numArray10, 0, (Array) numArray1, destinationIndex2, numArray10.Length);
          int destinationIndex3 = destinationIndex2 + numArray10.Length;
          byte[] numArray15 = this.AbrirSenhaEmBytes(usuarioBioArray[index2].Senha);
          Array.Copy((Array) numArray15, 0, (Array) numArray1, destinationIndex3, numArray15.Length);
          int index5 = destinationIndex3 + numArray15.Length;
          numArray1[index5] = maxValue1;
          int index6 = index5 + 1;
          numArray1[index6] = maxValue2;
          int index7 = index6 + 1;
          numArray1[index7] = num7;
          int destinationIndex4 = index7 + 1;
          Array.Copy((Array) numArray12, 0, (Array) numArray1, destinationIndex4, numArray12.Length);
          int index8 = destinationIndex4 + numArray12.Length;
          int destinationIndex5;
          byte[] template1;
          int num9;
          if (tipoTemplate == 12 || tipoTemplate == 17)
          {
            numArray1[index8] = (byte) 0;
            int index9 = index8 + 1;
            numArray1[index9] = (byte) 0;
            int index10 = index9 + 1;
            numArray1[index10] = (byte) 0;
            int index11 = index10 + 1;
            numArray1[index11] = (byte) 0;
            int index12 = index11 + 1;
            numArray1[index12] = (byte) 0;
            int index13 = index12 + 1;
            numArray1[index13] = (byte) 0;
            int index14 = index13 + 1;
            numArray1[index14] = (byte) 0;
            int index15 = index14 + 1;
            numArray1[index15] = (byte) 0;
            destinationIndex5 = index15 + 1;
            template1 = this.AbrirTemplateEmBytesCama(usuarioBioArray[index2].Template1);
            if (tipoTemplate == 17)
              numArray8 = this.AbrirTemplateEmBytesCama(usuarioBioArray[index2].Template2);
            num9 = 1000;
          }
          else
          {
            numArray1[index8] = (byte) 32;
            int num10 = index8 + 1;
            byte[] numArray16 = numArray1;
            int index16 = num10;
            num5 = DateTime.Now.Year % 100;
            int num11 = (int) (byte) int.Parse(num5.ToString(), NumberStyles.HexNumber);
            numArray16[index16] = (byte) num11;
            int num12 = num10 + 1;
            byte[] numArray17 = numArray1;
            int index17 = num12;
            num5 = now.Month;
            int num13 = (int) (byte) int.Parse(num5.ToString(), NumberStyles.HexNumber);
            numArray17[index17] = (byte) num13;
            int num14 = num12 + 1;
            byte[] numArray18 = numArray1;
            int index18 = num14;
            num5 = now.Day;
            int num15 = (int) (byte) int.Parse(num5.ToString(), NumberStyles.HexNumber);
            numArray18[index18] = (byte) num15;
            int num16 = num14 + 1;
            byte[] numArray19 = numArray1;
            int index19 = num16;
            num5 = now.Hour;
            int num17 = (int) (byte) int.Parse(num5.ToString(), NumberStyles.HexNumber);
            numArray19[index19] = (byte) num17;
            int num18 = num16 + 1;
            byte[] numArray20 = numArray1;
            int index20 = num18;
            num5 = now.Minute;
            int num19 = (int) (byte) int.Parse(num5.ToString(), NumberStyles.HexNumber);
            numArray20[index20] = (byte) num19;
            int num20 = num18 + 1;
            byte[] numArray21 = numArray1;
            int index21 = num20;
            num5 = now.Second;
            int num21 = (int) (byte) int.Parse(num5.ToString(), NumberStyles.HexNumber);
            numArray21[index21] = (byte) num21;
            int index22 = num20 + 1;
            numArray1[index22] = (byte) 0;
            destinationIndex5 = index22 + 1;
            template1 = this.AbrirTemplateEmBytes1_4K(usuarioBioArray[index2].Template1);
            if (!usuarioBioArray[index2].Template1.Equals(usuarioBioArray[index2].Template2))
              numArray8 = this.AbrirTemplateEmBytes1_4K(usuarioBioArray[index2].Template2);
            num9 = 2000;
            if (tipoTemplate == 10)
            {
              if (!usuarioBioArray[index2].Template1.Equals(usuarioBioArray[index2].Template2))
                numArray8 = this.AbrirTemplateEmBytes1_4K(usuarioBioArray[index2].Template2);
              if (!CriptoBI.TemplateZerado(numArray8))
              {
                numArray13[2] = BitConverter.GetBytes((ushort) numArray8.Length)[1];
                numArray13[3] = BitConverter.GetBytes((ushort) numArray8.Length)[0];
              }
              num9 = 1000;
            }
          }
          numArray13[0] = BitConverter.GetBytes((ushort) template1.Length)[1];
          numArray13[1] = BitConverter.GetBytes((ushort) template1.Length)[0];
          if (!CriptoBI.TemplatesIguais(template1, numArray8) && !CriptoBI.TemplateZerado(numArray8))
          {
            numArray13[2] = BitConverter.GetBytes((ushort) numArray8.Length)[1];
            numArray13[3] = BitConverter.GetBytes((ushort) numArray8.Length)[0];
          }
          Array.Copy((Array) numArray13, 0, (Array) numArray1, destinationIndex5, numArray13.Length);
          int destinationIndex6 = destinationIndex5 + numArray13.Length;
          if (tipoTemplate == 12 || tipoTemplate == 17)
          {
            Array.Copy((Array) numArray9, 0, (Array) numArray1, destinationIndex6, numArray9.Length);
            destinationIndex6 += numArray9.Length;
          }
          Array.Copy((Array) template1, 0, (Array) numArray1, destinationIndex6, template1.Length);
          destinationIndex1 = destinationIndex6 + template1.Length;
          if (tipoTemplate == 17)
          {
            Array.Copy((Array) numArray9, 0, (Array) numArray1, destinationIndex1, numArray9.Length);
            destinationIndex1 += numArray9.Length;
          }
          if (tipoTemplate == 10 || tipoTemplate == 17)
          {
            Array.Copy((Array) numArray8, 0, (Array) numArray1, destinationIndex1, numArray8.Length);
            destinationIndex1 += numArray8.Length;
          }
          Application.DoEvents();
          if (num4 == num9 || num4 == num2)
          {
            num1 = (ushort) 0;
            int CheckSumBufferCripto = 0;
            Random random = new Random(DateTime.Now.Millisecond);
            random.NextBytes(buffer1);
            random.NextBytes(buffer2);
            ushort uint16 = BitConverter.ToUInt16(buffer1, 0);
            this.NotificaUsuario(Resources.msgGERANDO_DADOS_TEMPLATES_RB1, 2);
            string path = string.Format(pathCripto, (object) num3.ToString());
            if (tipoTemplate == 10 || tipoTemplate == 12 || tipoTemplate == 17)
              path = string.Format(pathCripto, (object) num3.ToString("00"));
            if (File.Exists(path))
              File.Delete(path);
            File.WriteAllLines(path, new string[0]);
            numArray3[0] = BitConverter.GetBytes((ushort) num4)[0];
            numArray3[1] = BitConverter.GetBytes((ushort) num4)[1];
            destinationIndex1 = 0;
            num4 = 0;
            ++num3;
            numArray2[0] = BitConverter.GetBytes((ushort) num2)[0];
            numArray2[1] = BitConverter.GetBytes((ushort) num2)[1];
            num2 = sortableBindingList.Count - (index2 + 1);
            byte[] registro1 = new byte[944004];
            switch (tipoTemplate)
            {
              case 10:
                registro1 = new byte[876004];
                break;
              case 12:
                registro1 = new byte[570004];
                break;
              case 17:
                registro1 = new byte[1072004];
                break;
            }
            int destinationIndex7 = 0;
            Array.Copy((Array) numArray2, 0, (Array) registro1, destinationIndex7, numArray2.Length);
            int destinationIndex8 = destinationIndex7 + numArray2.Length;
            Array.Copy((Array) numArray3, 0, (Array) registro1, destinationIndex8, numArray3.Length);
            int destinationIndex9 = destinationIndex8 + numArray3.Length;
            Array.Copy((Array) numArray1, 0, (Array) registro1, destinationIndex9, numArray1.Length);
            int num22 = destinationIndex9 + numArray1.Length;
            int destinationIndex10 = 0;
            string hexastring1 = CriptoBI.HashRegistro(registro1);
            byte[] registro2 = new byte[944004];
            if (tipoTemplate == 10)
              registro2 = new byte[876004];
            else if (tipoTemplate == 12)
              registro2 = new byte[570004];
            else if (tipoTemplate == 17)
              registro2 = new byte[1072004];
            int length = registro2.Length;
            int num23 = 0;
            int index23 = 0;
            byte[] buffer3 = new byte[numArray2.Length];
            foreach (byte texto in numArray2)
            {
              buffer3[index23] = (byte) short.Parse(this.CriptoTexto(texto, length, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
              this.NotificaBytesCripto(num23.ToString());
              ++num23;
              ++index23;
              Application.DoEvents();
            }
            Array.Copy((Array) buffer3, 0, (Array) registro2, destinationIndex10, buffer3.Length);
            int destinationIndex11 = destinationIndex10 + buffer3.Length;
            int index24 = 0;
            byte[] buffer4 = new byte[numArray3.Length];
            num23 = 0;
            foreach (byte texto in numArray3)
            {
              buffer4[index24] = (byte) short.Parse(this.CriptoTexto(texto, length, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
              this.NotificaBytesCripto(num23.ToString());
              ++num23;
              ++index24;
              Application.DoEvents();
            }
            Array.Copy((Array) buffer4, 0, (Array) registro2, destinationIndex11, buffer4.Length);
            int destinationIndex12 = destinationIndex11 + buffer4.Length;
            int index25 = 0;
            StringBuilder stringBuilder2 = new StringBuilder();
            byte[] buffer5 = new byte[numArray1.Length];
            num23 = 0;
            foreach (byte texto in numArray1)
            {
              buffer5[index25] = (byte) short.Parse(this.CriptoTexto(texto, length, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
              this.NotificaBytesCripto(num23.ToString());
              ++num23;
              ++index25;
              Application.DoEvents();
            }
            Array.Copy((Array) buffer5, 0, (Array) registro2, destinationIndex12, buffer5.Length);
            num22 = destinationIndex12 + buffer5.Length;
            byte[] numArray22 = new byte[16];
            for (int index26 = 0; index26 < 16; ++index26)
            {
              string s4 = str.Substring(index26 * 2, 2);
              numArray22[index26] = byte.Parse(s4, NumberStyles.HexNumber);
            }
            int index27 = CheckSumBufferCripto % 512;
            buffer2[index27] = buffer1[0];
            buffer2[index27 + 1] = buffer1[1];
            byte[] numArray23 = new byte[512];
            Array.Copy((Array) numArray22, 0, (Array) numArray23, 0, 16);
            byte[] buffer6 = new byte[512];
            for (int index28 = 0; index28 < numArray23.Length; ++index28)
              buffer6[index28] = (byte) short.Parse(this.CriptoTexto(numArray23[index28], length, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
            string hexastring2 = "";
            for (int index29 = 0; index29 < buffer2.Length; ++index29)
              hexastring2 += buffer2[index29].ToString("X").PadLeft(2, '0');
            byte[] buffer7 = new byte[512];
            string hexastring3 = length.ToString("X").PadLeft(8, '0');
            byte[] asCii1 = this.ConverteToASCii(aux1);
            Array.Reverse((Array) asCii1);
            byte num24 = byte.Parse(s1);
            byte[] asCii2 = this.ConverteHexaToASCii(num24.ToString("X").PadLeft(2, '0'));
            byte[] numArray24 = new byte[3]
            {
              this.ConverteToASCii(aux2)[0],
              (byte) 0,
              (byte) 0
            };
            byte[] numArray25 = numArray24;
            num24 = byte.Parse(s2);
            int num25 = (int) this.ConverteHexaToASCii(num24.ToString("X").PadLeft(2, '0'))[0];
            numArray25[1] = (byte) num25;
            numArray24[2] = this.ConverteToASCii(aux3)[0];
            byte[] asCii3 = this.ConverteHexaToASCii(hexastring3);
            Array.Reverse((Array) asCii3);
            byte[] asCii4 = this.ConverteHexaToASCii(CriptoBI.HashRegistro(registro2));
            byte[] asCii5 = this.ConverteHexaToASCii(hexastring1);
            this.ConverteToASCii(aux4);
            int num26 = 0;
            Array.Copy((Array) asCii1, 0, (Array) buffer7, 0, asCii1.Length);
            int destinationIndex13 = num26 + asCii1.Length;
            Array.Copy((Array) asCii2, 0, (Array) buffer7, destinationIndex13, asCii2.Length);
            int destinationIndex14 = destinationIndex13 + asCii2.Length;
            Array.Copy((Array) numArray24, 0, (Array) buffer7, destinationIndex14, numArray24.Length);
            int destinationIndex15 = destinationIndex14 + numArray24.Length;
            Array.Copy((Array) numArray22, 0, (Array) buffer7, destinationIndex15, numArray22.Length);
            int destinationIndex16 = destinationIndex15 + numArray22.Length;
            Array.Copy((Array) asCii3, 0, (Array) buffer7, destinationIndex16, asCii3.Length);
            int destinationIndex17 = destinationIndex16 + asCii3.Length;
            Array.Copy((Array) asCii4, 0, (Array) buffer7, destinationIndex17, asCii4.Length);
            int destinationIndex18 = destinationIndex17 + asCii4.Length;
            Array.Copy((Array) asCii5, 0, (Array) buffer7, destinationIndex18, asCii5.Length);
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(buffer7);
              binaryWriter.Close();
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(this.ConverteHexaToASCii(hexastring2));
              binaryWriter.Close();
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(buffer3);
              binaryWriter.Close();
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(buffer4);
              binaryWriter.Close();
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(buffer5);
              binaryWriter.Close();
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(buffer6);
              binaryWriter.Close();
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(this.ConverteHexaToASCii("FEFFFFFF"));
              binaryWriter.Close();
            }
          }
          ++num4;
        }
        this.NotificaUsuario(Resources.msgDADOS_RB1_GERADOS_SUCESSO, 0);
        return true;
      }
      catch (Exception ex)
      {
        if (!File.Exists(pathCripto))
          this.NotificaUsuario("Atenção: Falha ao gravar o arquivo, diretório não encontrado", 1);
        else
          this.NotificaUsuario("Atenção: " + Environment.NewLine + ex.Message, 1);
        return false;
      }
    }

    private static bool TemplatesIguais(byte[] template1, byte[] template2) => BitConverter.ToString(template1).Equals(BitConverter.ToString(template2));

    private static bool TemplateZerado(byte[] Template2)
    {
      bool flag = true;
      foreach (byte num in Template2)
      {
        if (num != (byte) 0)
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    private bool CriptografaRB1TemplatesSagem(string pathCripto, int tipoTemplate)
    {
      try
      {
        byte[] buffer1 = new byte[2];
        byte[] buffer2 = new byte[512];
        ushort num1 = 0;
        string aux1 = "0001";
        string s1 = "16";
        string aux2 = "0";
        string s2 = tipoTemplate.ToString();
        string aux3 = this.VersaoIR.ToString();
        string str = this.NumeroSerie.Trim().PadLeft(18, '0').PadRight(32, '0');
        string aux4 = "".PadLeft(444, '0');
        TemplateBio templateBio = new TemplateBio();
        SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
        this.NotificaUsuario(Resources.msgPROCESSANDO_DADOS_TEMPLATES_RB1, 2);
        if (!this._chamadaSDK)
        {
          RepBase repBase = (!this.ChamadoPelaAT ? new RepBase() : (RepBase) new RepBaseAT()).PesquisarRepPorID(this.repId);
          if (RegistrySingleton.GetInstance().UTILIZA_GRUPOS && this.repId != 0)
          {
            if (repBase.grupoId == 0)
            {
              foreach (DataRow row in (InternalDataCollectionBase) templateBio.PesquisarTemplatesSagemPorEmpresaRepPlusDataSource(this.objempregador, repBase.grupoId).Rows)
                sortableBindingList.Add(new UsuarioBio()
                {
                  Nome = row["nome"].ToString(),
                  IdBiometria = ulong.Parse(row["idBiometria"].ToString()),
                  Pis = row["idBiometria"].ToString(),
                  Senha = row["senha"].ToString(),
                  Template1 = row["template1"].ToString(),
                  Template2 = row["template2"].ToString(),
                  TipoTemplate = int.Parse(row["TipoTemplate"].ToString()),
                  InfoNivelSeguranca = (int) byte.MaxValue,
                  NivelSeguranca = (int) byte.MaxValue,
                  EmpregadorID = int.Parse(row["EmpregadorId"].ToString())
                });
            }
            else
              sortableBindingList = GrupoRepXempregadoBI.PesquisarTemplatesSAGEMPorEmpregadorDoGrupoRepPlus(repBase.grupoId, this.objempregador.EmpregadorId);
          }
          else
          {
            foreach (DataRow row in (InternalDataCollectionBase) templateBio.PesquisarTemplatesSagemPorEmpresaRepPlusDataSource(this.objempregador, repBase.grupoId).Rows)
              sortableBindingList.Add(new UsuarioBio()
              {
                Nome = row["nome"].ToString(),
                IdBiometria = ulong.Parse(row["idBiometria"].ToString()),
                Pis = row["idBiometria"].ToString(),
                Senha = row["senha"].ToString(),
                Template1 = row["template1"].ToString(),
                Template2 = row["template2"].ToString(),
                TipoTemplate = int.Parse(row["TipoTemplate"].ToString()),
                InfoNivelSeguranca = (int) byte.MaxValue,
                NivelSeguranca = (int) byte.MaxValue,
                EmpregadorID = int.Parse(row["EmpregadorId"].ToString())
              });
          }
        }
        else
        {
          sortableBindingList.Clear();
          foreach (UsuarioBio usuarioBio in this.ListaTemplatesSDK)
          {
            usuarioBio.Senha = "546f7053656372657400000000000000";
            sortableBindingList.Add(usuarioBio);
          }
        }
        byte[] numArray1 = new byte[528000];
        int destinationIndex1 = 0;
        int index1 = 0;
        byte[] numArray2 = new byte[2];
        byte[] numArray3 = new byte[2];
        int num2 = sortableBindingList.Count - index1;
        DateTime now = DateTime.Now;
        UsuarioBio[] usuarioBioArray = new UsuarioBio[sortableBindingList.Count];
        foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) sortableBindingList)
        {
          usuarioBioArray[index1] = usuarioBio;
          ++index1;
          Application.DoEvents();
        }
        int num3 = 1;
        int num4 = 1;
        this.NotificaMaxBytesCripto((numArray1.Length + 2).ToString());
        bool flag;
        if (!this._chamadaSDK)
        {
          ConfiguracoesGerais configuracoesGerais = new ConfiguracoesGerais();
          flag = new ConfiguracoesGerais().PesquisarConfigGerais().SobrescreverDigitais;
        }
        else
          flag = true;
        for (int index2 = 0; index2 < usuarioBioArray.Length; ++index2)
        {
          byte[] numArray4 = new byte[256];
          byte[] numArray5 = new byte[256];
          byte num5 = 0;
          byte[] numArray6 = new byte[11];
          byte num6 = flag ? (byte) 1 : (byte) 0;
          byte[] numArray7 = new byte[3];
          numArray1[destinationIndex1] = num5;
          string PisUsuario = usuarioBioArray[index2].Pis.PadLeft(16, '0');
          StringBuilder stringBuilder1 = new StringBuilder();
          byte[] numArray8 = new byte[8];
          foreach (int pisUsuarioEmByte in this.AbrirPisUsuarioEmBytes(PisUsuario))
          {
            int num7 = pisUsuarioEmByte;
            ++num7;
            stringBuilder1.Append(num7.ToString("x").PadLeft(2, '0'));
          }
          for (int index3 = 0; index3 < 8; ++index3)
          {
            string s3 = stringBuilder1.ToString().Substring(index3 * 2, 2);
            numArray6[index3] = byte.Parse(s3, NumberStyles.HexNumber);
          }
          Array.Copy((Array) numArray6, 0, (Array) numArray1, destinationIndex1, numArray6.Length);
          int index4 = destinationIndex1 + 11;
          byte[] numArray9 = this.AbrirTemplateEmBytesSagem(usuarioBioArray[index2].Template1);
          int num8 = 1000;
          if (!usuarioBioArray[index2].Template1.Equals(usuarioBioArray[index2].Template2) && !usuarioBioArray[index2].Template2.Equals(""))
          {
            numArray1[index4] = (byte) 2;
            numArray5 = this.AbrirTemplateEmBytesSagem(usuarioBioArray[index2].Template2);
          }
          else
            numArray1[index4] = (byte) 1;
          int index5 = index4 + 1;
          numArray1[index5] = num6;
          int destinationIndex2 = index5 + 1;
          Array.Copy((Array) numArray7, 0, (Array) numArray1, destinationIndex2, numArray7.Length);
          int destinationIndex3 = destinationIndex2 + numArray7.Length;
          Array.Copy((Array) numArray9, 0, (Array) numArray1, destinationIndex3, numArray9.Length);
          int destinationIndex4 = destinationIndex3 + numArray9.Length;
          Array.Copy((Array) numArray5, 0, (Array) numArray1, destinationIndex4, numArray5.Length);
          destinationIndex1 = destinationIndex4 + numArray5.Length;
          Application.DoEvents();
          if (num4 == num8 || num4 == num2)
          {
            num1 = (ushort) 0;
            int CheckSumBufferCripto = 0;
            Random random = new Random(DateTime.Now.Millisecond);
            random.NextBytes(buffer1);
            random.NextBytes(buffer2);
            ushort uint16 = BitConverter.ToUInt16(buffer1, 0);
            this.NotificaUsuario(Resources.msgGERANDO_DADOS_TEMPLATES_RB1, 2);
            string.Format(pathCripto, (object) num3.ToString());
            string path = string.Format(pathCripto, (object) num3.ToString("00"));
            if (File.Exists(path))
              File.Delete(path);
            File.WriteAllLines(path, new string[0]);
            numArray3[0] = BitConverter.GetBytes((ushort) num4)[0];
            numArray3[1] = BitConverter.GetBytes((ushort) num4)[1];
            destinationIndex1 = 0;
            num4 = 0;
            ++num3;
            numArray2[0] = BitConverter.GetBytes((ushort) num2)[0];
            numArray2[1] = BitConverter.GetBytes((ushort) num2)[1];
            num2 = sortableBindingList.Count - (index2 + 1);
            byte[] registro1 = new byte[528004];
            int destinationIndex5 = 0;
            Array.Copy((Array) numArray2, 0, (Array) registro1, destinationIndex5, numArray2.Length);
            int destinationIndex6 = destinationIndex5 + numArray2.Length;
            Array.Copy((Array) numArray3, 0, (Array) registro1, destinationIndex6, numArray3.Length);
            int destinationIndex7 = destinationIndex6 + numArray3.Length;
            Array.Copy((Array) numArray1, 0, (Array) registro1, destinationIndex7, numArray1.Length);
            int num9 = destinationIndex7 + numArray1.Length;
            int destinationIndex8 = 0;
            string hexastring1 = CriptoBI.HashRegistro(registro1);
            byte[] registro2 = new byte[528004];
            int length = registro2.Length;
            int num10 = 0;
            int index6 = 0;
            byte[] buffer3 = new byte[numArray2.Length];
            foreach (byte texto in numArray2)
            {
              buffer3[index6] = (byte) short.Parse(this.CriptoTexto(texto, length, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
              this.NotificaBytesCripto(num10.ToString());
              ++num10;
              ++index6;
              Application.DoEvents();
            }
            Array.Copy((Array) buffer3, 0, (Array) registro2, destinationIndex8, buffer3.Length);
            int destinationIndex9 = destinationIndex8 + buffer3.Length;
            int index7 = 0;
            byte[] buffer4 = new byte[numArray3.Length];
            num10 = 0;
            foreach (byte texto in numArray3)
            {
              buffer4[index7] = (byte) short.Parse(this.CriptoTexto(texto, length, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
              this.NotificaBytesCripto(num10.ToString());
              ++num10;
              ++index7;
              Application.DoEvents();
            }
            Array.Copy((Array) buffer4, 0, (Array) registro2, destinationIndex9, buffer4.Length);
            int destinationIndex10 = destinationIndex9 + buffer4.Length;
            int index8 = 0;
            StringBuilder stringBuilder2 = new StringBuilder();
            byte[] buffer5 = new byte[numArray1.Length];
            num10 = 0;
            foreach (byte texto in numArray1)
            {
              buffer5[index8] = (byte) short.Parse(this.CriptoTexto(texto, length, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
              this.NotificaBytesCripto(num10.ToString());
              ++num10;
              ++index8;
              Application.DoEvents();
            }
            Array.Copy((Array) buffer5, 0, (Array) registro2, destinationIndex10, buffer5.Length);
            num9 = destinationIndex10 + buffer5.Length;
            byte[] numArray10 = new byte[16];
            for (int index9 = 0; index9 < 16; ++index9)
            {
              string s4 = str.Substring(index9 * 2, 2);
              numArray10[index9] = byte.Parse(s4, NumberStyles.HexNumber);
            }
            int index10 = CheckSumBufferCripto % 512;
            buffer2[index10] = buffer1[0];
            buffer2[index10 + 1] = buffer1[1];
            byte[] numArray11 = new byte[512];
            Array.Copy((Array) numArray10, 0, (Array) numArray11, 0, 16);
            byte[] buffer6 = new byte[512];
            for (int index11 = 0; index11 < numArray11.Length; ++index11)
              buffer6[index11] = (byte) short.Parse(this.CriptoTexto(numArray11[index11], length, ref uint16, ref CheckSumBufferCripto), NumberStyles.AllowHexSpecifier);
            string hexastring2 = "";
            for (int index12 = 0; index12 < buffer2.Length; ++index12)
              hexastring2 += buffer2[index12].ToString("X").PadLeft(2, '0');
            byte[] buffer7 = new byte[512];
            string hexastring3 = length.ToString("X").PadLeft(8, '0');
            byte[] asCii1 = this.ConverteToASCii(aux1);
            Array.Reverse((Array) asCii1);
            byte num11 = byte.Parse(s1);
            byte[] asCii2 = this.ConverteHexaToASCii(num11.ToString("X").PadLeft(2, '0'));
            byte[] numArray12 = new byte[3]
            {
              this.ConverteToASCii(aux2)[0],
              (byte) 0,
              (byte) 0
            };
            byte[] numArray13 = numArray12;
            num11 = byte.Parse(s2);
            int num12 = (int) this.ConverteHexaToASCii(num11.ToString("X").PadLeft(2, '0'))[0];
            numArray13[1] = (byte) num12;
            numArray12[2] = this.ConverteToASCii(aux3)[0];
            byte[] asCii3 = this.ConverteHexaToASCii(hexastring3);
            Array.Reverse((Array) asCii3);
            byte[] asCii4 = this.ConverteHexaToASCii(CriptoBI.HashRegistro(registro2));
            byte[] asCii5 = this.ConverteHexaToASCii(hexastring1);
            this.ConverteToASCii(aux4);
            int num13 = 0;
            Array.Copy((Array) asCii1, 0, (Array) buffer7, 0, asCii1.Length);
            int destinationIndex11 = num13 + asCii1.Length;
            Array.Copy((Array) asCii2, 0, (Array) buffer7, destinationIndex11, asCii2.Length);
            int destinationIndex12 = destinationIndex11 + asCii2.Length;
            Array.Copy((Array) numArray12, 0, (Array) buffer7, destinationIndex12, numArray12.Length);
            int destinationIndex13 = destinationIndex12 + numArray12.Length;
            Array.Copy((Array) numArray10, 0, (Array) buffer7, destinationIndex13, numArray10.Length);
            int destinationIndex14 = destinationIndex13 + numArray10.Length;
            Array.Copy((Array) asCii3, 0, (Array) buffer7, destinationIndex14, asCii3.Length);
            int destinationIndex15 = destinationIndex14 + asCii3.Length;
            Array.Copy((Array) asCii4, 0, (Array) buffer7, destinationIndex15, asCii4.Length);
            int destinationIndex16 = destinationIndex15 + asCii4.Length;
            Array.Copy((Array) asCii5, 0, (Array) buffer7, destinationIndex16, asCii5.Length);
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(buffer7);
              binaryWriter.Close();
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(this.ConverteHexaToASCii(hexastring2));
              binaryWriter.Close();
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(buffer3);
              binaryWriter.Close();
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(buffer4);
              binaryWriter.Close();
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(buffer5);
              binaryWriter.Close();
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(buffer6);
              binaryWriter.Close();
            }
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Append)))
            {
              binaryWriter.Write(this.ConverteHexaToASCii("FEFFFFFF"));
              binaryWriter.Close();
            }
          }
          ++num4;
        }
        this.NotificaUsuario(Resources.msgDADOS_RB1_GERADOS_SUCESSO, 0);
        return true;
      }
      catch (Exception ex)
      {
        if (!File.Exists(pathCripto))
          this.NotificaUsuario("Atenção: Falha ao gravar o arquivo, diretório não encontrado", 1);
        else
          this.NotificaUsuario("Atenção: " + Environment.NewLine + ex.Message, 1);
        return false;
      }
    }

    private byte[] AbrirPisUsuarioEmBytes(string PisUsuario)
    {
      byte[] numArray = new byte[11];
      int index1 = 0;
      int length = PisUsuario.Length;
      for (int index2 = 0; index2 < length; index2 += 2)
      {
        string s = PisUsuario[index2].ToString() + PisUsuario[index2 + 1].ToString();
        numArray[index1] = (byte) int.Parse(s, NumberStyles.HexNumber);
        ++index1;
      }
      return numArray;
    }

    private byte[] AbrirTemplateEmBytes1_4K(string template)
    {
      byte[] numArray = new byte[404];
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

    private byte[] AbrirTemplateEmBytesCama(string template)
    {
      byte[] numArray = new byte[498];
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

    private byte[] AbrirTemplateEmBytesSagem(string template)
    {
      byte[] numArray = new byte[256];
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

    private byte[] AbrirSenhaEmBytes(string senha)
    {
      byte[] numArray = new byte[16];
      int index1 = 0;
      int length = senha.Length;
      for (int index2 = 0; index2 < length; index2 += 2)
      {
        string s = senha[index2].ToString() + senha[index2 + 1].ToString();
        numArray[index1] = (byte) int.Parse(s, NumberStyles.HexNumber);
        ++index1;
      }
      return numArray;
    }

    public static string HashArquivo(string diretorioArquivo)
    {
      byte[] buffer;
      using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(diretorioArquivo, FileMode.Open)))
      {
        int count = int.Parse(binaryReader.BaseStream.Length.ToString());
        buffer = binaryReader.ReadBytes(count);
      }
      Application.DoEvents();
      SHA1 shA1 = (SHA1) new SHA1CryptoServiceProvider();
      shA1.ComputeHash(buffer);
      return CriptoBI.ImprimeHexadecimal(shA1.Hash).Replace(" ", "");
    }

    public static string HashRegistro(byte[] registro)
    {
      SHA1 shA1 = (SHA1) new SHA1CryptoServiceProvider();
      shA1.ComputeHash(registro);
      return CriptoBI.ImprimeHexadecimal(shA1.Hash).Replace(" ", "");
    }

    public static string ImprimeHexadecimal(byte[] dado)
    {
      string str = "";
      foreach (byte num1 in dado)
      {
        uint num2 = (uint) num1;
        str = str + (num1 <= (byte) 15 ? "0" : "") + string.Format("{0:X}", (object) num2) + " ";
      }
      return str;
    }

    private bool DesCriptografaRB1Empregados(string pathDesCripto)
    {
      byte[] numArray = new byte[2];
      byte[] linhasArq;
      using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(pathDesCripto, FileMode.Open)))
      {
        int count = int.Parse(binaryReader.BaseStream.Length.ToString());
        linhasArq = binaryReader.ReadBytes(count);
      }
      byte[] dado = new byte[20];
      Array.Copy((Array) linhasArq, 28, (Array) dado, 0, 20);
      byte[] registro = new byte[linhasArq.Length - 516 - 1024];
      Array.Copy((Array) linhasArq, 1024, (Array) registro, 0, registro.Length);
      if (CriptoBI.ImprimeHexadecimal(dado).Replace(" ", "") != CriptoBI.HashRegistro(registro) || linhasArq[6] != (byte) 3)
      {
        this.NotificaUsuario("Atenção. O arquivo que está tentando importar é inválido.", 1);
        return false;
      }
      int num = this.CalculaCheckSum(linhasArq);
      if (num < 0)
      {
        this.NotificaUsuario("Atenção. Erro ao realizar o checksum", 1);
        return false;
      }
      int index = num % 512 + 512;
      numArray[0] = linhasArq[index];
      numArray[1] = linhasArq[index + 1];
      ushort uint16 = BitConverter.ToUInt16(numArray, 0);
      byte[] texto = new byte[linhasArq.Length - 516 - 1024];
      Array.Copy((Array) linhasArq, 1024, (Array) texto, 0, texto.Length);
      this.NotificaUsuario(Resources.msgIMPORTANDO_DADOS_RB1, 2);
      return this.DesCriptoTextoEmpregados(texto, ref uint16);
    }

    private bool DesCriptografaRB1Empregador(string pathDesCripto)
    {
      byte[] numArray = new byte[2];
      byte[] linhasArq;
      using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(pathDesCripto, FileMode.Open)))
      {
        int count = int.Parse(binaryReader.BaseStream.Length.ToString());
        linhasArq = binaryReader.ReadBytes(count);
      }
      byte[] dado = new byte[20];
      Array.Copy((Array) linhasArq, 28, (Array) dado, 0, 20);
      byte[] registro = new byte[linhasArq.Length - 516 - 1024];
      Array.Copy((Array) linhasArq, 1024, (Array) registro, 0, registro.Length);
      if (CriptoBI.ImprimeHexadecimal(dado).Replace(" ", "") != CriptoBI.HashRegistro(registro) || linhasArq[6] != (byte) 7)
      {
        this.NotificaUsuario("Atenção. O arquivo que está tentando importar é inválido.", 1);
        return false;
      }
      int num = this.CalculaCheckSum(linhasArq);
      if (num < 0)
      {
        this.NotificaUsuario("Atenção. Erro ao realizar o checksum", 1);
        return false;
      }
      int index = num % 512 + 512;
      numArray[0] = linhasArq[index];
      numArray[1] = linhasArq[index + 1];
      ushort uint16 = BitConverter.ToUInt16(numArray, 0);
      byte[] texto = new byte[linhasArq.Length - 516 - 1024];
      Array.Copy((Array) linhasArq, 1024, (Array) texto, 0, texto.Length);
      this.NotificaUsuario(Resources.msgIMPORTANDO_DADOS_RB1, 2);
      return this.DesCriptoTextoEmpregador(texto, ref uint16);
    }

    public Empregador RecuperarRB1Empregador(string pathDesCripto)
    {
      byte[] numArray = new byte[2];
      byte[] linhasArq;
      using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(pathDesCripto, FileMode.Open)))
      {
        int count = int.Parse(binaryReader.BaseStream.Length.ToString());
        linhasArq = binaryReader.ReadBytes(count);
      }
      byte[] dado = new byte[20];
      Array.Copy((Array) linhasArq, 28, (Array) dado, 0, 20);
      byte[] registro = new byte[linhasArq.Length - 516 - 1024];
      Array.Copy((Array) linhasArq, 1024, (Array) registro, 0, registro.Length);
      if (CriptoBI.ImprimeHexadecimal(dado).Replace(" ", "") != CriptoBI.HashRegistro(registro) || linhasArq[6] != (byte) 7)
      {
        this.NotificaUsuario("Atenção. O arquivo que está tentando importar é inválido.", 1);
        return (Empregador) null;
      }
      int num = this.CalculaCheckSum(linhasArq);
      if (num < 0)
      {
        this.NotificaUsuario("Atenção. Erro ao realizar o checksum", 1);
        return (Empregador) null;
      }
      int index = num % 512 + 512;
      numArray[0] = linhasArq[index];
      numArray[1] = linhasArq[index + 1];
      ushort uint16 = BitConverter.ToUInt16(numArray, 0);
      byte[] texto = new byte[linhasArq.Length - 516 - 1024];
      Array.Copy((Array) linhasArq, 1024, (Array) texto, 0, texto.Length);
      this.NotificaUsuario(Resources.msgIMPORTANDO_DADOS_RB1, 2);
      return this.RecuperarRB1Empregador(texto, ref uint16);
    }

    public int RecuperarTipoArquivoConfiguracoes(string pathDesCripto)
    {
      byte[] numArray;
      using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(pathDesCripto, FileMode.Open)))
      {
        int count = int.Parse(binaryReader.BaseStream.Length.ToString());
        numArray = binaryReader.ReadBytes(count);
      }
      byte[] dado = new byte[20];
      Array.Copy((Array) numArray, 28, (Array) dado, 0, 20);
      byte[] registro = new byte[numArray.Length - 516 - 1024];
      Array.Copy((Array) numArray, 1024, (Array) registro, 0, registro.Length);
      if (!(CriptoBI.ImprimeHexadecimal(dado).Replace(" ", "") != CriptoBI.HashRegistro(registro)) && (numArray[6] == (byte) 16 || numArray[6] == (byte) 9 || numArray[6] == (byte) 22))
        return (int) numArray[6];
      this.NotificaUsuario("Atenção. O arquivo que está tentando importar é inválido.", 1);
      return 0;
    }

    private bool DesCriptografaRB1Configuracoes(string pathDesCripto, int tipoConfiguracoes)
    {
      byte[] numArray = new byte[2];
      byte[] linhasArq;
      using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(pathDesCripto, FileMode.Open)))
      {
        int count = int.Parse(binaryReader.BaseStream.Length.ToString());
        linhasArq = binaryReader.ReadBytes(count);
      }
      byte[] numeroSerieArquivo = new byte[16];
      Array.Copy((Array) linhasArq, 8, (Array) numeroSerieArquivo, 0, 16);
      byte[] dado = new byte[20];
      Array.Copy((Array) linhasArq, 28, (Array) dado, 0, 20);
      byte[] registro = new byte[linhasArq.Length - 516 - 1024];
      Array.Copy((Array) linhasArq, 1024, (Array) registro, 0, registro.Length);
      if (CriptoBI.ImprimeHexadecimal(dado).Replace(" ", "") != CriptoBI.HashRegistro(registro) || linhasArq[6] != (byte) 9 && linhasArq[6] != (byte) 16 && linhasArq[6] != (byte) 22)
      {
        this.NotificaUsuario("Atenção. O arquivo que está tentando importar é inválido.", 1);
        return false;
      }
      int num = this.CalculaCheckSum(linhasArq);
      if (num < 0)
      {
        this.NotificaUsuario("Atenção. Erro ao realizar o checksum", 1);
        return false;
      }
      int index = num % 512 + 512;
      numArray[0] = linhasArq[index];
      numArray[1] = linhasArq[index + 1];
      ushort uint16 = BitConverter.ToUInt16(numArray, 0);
      byte[] texto = new byte[linhasArq.Length - 516 - 1024];
      Array.Copy((Array) linhasArq, 1024, (Array) texto, 0, texto.Length);
      this.NotificaUsuario(Resources.msgIMPORTANDO_DADOS_RB1_CONFIGURACOES_REP, 2);
      return this.DesCriptoTextoConfiguracoes(texto, ref uint16, numeroSerieArquivo, tipoConfiguracoes);
    }

    private bool DesCriptografaRB1Templates(string pathDesCripto)
    {
      byte[] numArray = new byte[2];
      byte[] linhasArq;
      using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(pathDesCripto, FileMode.Open)))
      {
        int count = int.Parse(binaryReader.BaseStream.Length.ToString());
        linhasArq = binaryReader.ReadBytes(count);
      }
      byte[] dado = new byte[20];
      Array.Copy((Array) linhasArq, 28, (Array) dado, 0, 20);
      byte[] registro = new byte[linhasArq.Length - 516 - 1024];
      Array.Copy((Array) linhasArq, 1024, (Array) registro, 0, registro.Length);
      if (CriptoBI.ImprimeHexadecimal(dado).Replace(" ", "") != CriptoBI.HashRegistro(registro) || linhasArq[6] != (byte) 5 && linhasArq[6] != (byte) 13 && linhasArq[6] != (byte) 11 && linhasArq[6] != (byte) 18 && linhasArq[6] != (byte) 20)
      {
        this.NotificaUsuario("Atenção. O arquivo que está tentando importar é inválido.", 1);
        return false;
      }
      int num = this.CalculaCheckSum(linhasArq);
      if (num < 0)
      {
        this.NotificaUsuario("Atenção. Erro ao realizar o checksum", 1);
        return false;
      }
      int index = num % 512 + 512;
      numArray[0] = linhasArq[index];
      numArray[1] = linhasArq[index + 1];
      ushort uint16 = BitConverter.ToUInt16(numArray, 0);
      byte[] texto = new byte[linhasArq.Length - 516 - 1024];
      Array.Copy((Array) linhasArq, 1024, (Array) texto, 0, texto.Length);
      this.NotificaUsuario(Resources.msgIMPORTANDO_DADOS_TEMPLATES_RB1, 2);
      return this.DesCriptoTextoTemplates(texto, ref uint16, linhasArq[6]);
    }

    private ushort busca_cri(ushort semente, int produto) => Convert.ToUInt16((double) semente * 27941.0 % 61357.0);

    private string CriptoTexto(
      byte texto,
      int tamanhoBytes,
      ref ushort sequenciaCripto,
      ref int CheckSumBufferCripto)
    {
      byte num = (byte) ((uint) (byte) ((uint) texto ^ (uint) BitConverter.GetBytes(sequenciaCripto)[1]) ^ (uint) BitConverter.GetBytes(sequenciaCripto)[0]);
      CheckSumBufferCripto += (int) num;
      sequenciaCripto = this.busca_cri(sequenciaCripto, 0);
      return num.ToString("X").PadLeft(2, '0');
    }

    private bool DesCriptoTextoEmpregados(byte[] texto, ref ushort sequenciaCripto)
    {
      try
      {
        int length = texto.Length;
        byte[] DeviceDataResponseArray = new byte[texto.Length];
        bool flag = false;
        Empregado empregado1 = new Empregado();
        List<Empregado> empregadoList = new List<Empregado>();
        if (!this._chamadaSDK)
          empregadoList = empregado1.PesquisarListaEmpregadosPorEmpregador(this.objempregador.EmpregadorId);
        int index1 = 0;
        foreach (uint num1 in texto)
        {
          byte num2 = (byte) ((uint) (byte) (num1 ^ (uint) BitConverter.GetBytes(sequenciaCripto)[1]) ^ (uint) BitConverter.GetBytes(sequenciaCripto)[0]);
          DeviceDataResponseArray[index1] = num2;
          sequenciaCripto = this.busca_cri(sequenciaCripto, 0);
          ++index1;
        }
        int num3 = (int) short.Parse(DeviceDataResponseArray[5].ToString("X").PadLeft(2, '0') + DeviceDataResponseArray[4].ToString("X").PadLeft(2, '0'), NumberStyles.AllowHexSpecifier);
        this.NotificaMaxBytesCripto(num3.ToString());
        CriptoBI.DeslocaBufferResposta(ref DeviceDataResponseArray, 8);
        int sourceIndex1 = 0;
        this.NotificaUsuario(Resources.msgIMPORTANDO_DADOS_RB1, 2);
        for (int index2 = 0; index2 < num3; ++index2)
        {
          Empregado emp = new Empregado();
          if (!this._chamadaSDK)
            emp.EmpregadorId = this.objempregador.EmpregadorId;
          byte[] numArray1 = new byte[6];
          Array.Copy((Array) DeviceDataResponseArray, sourceIndex1, (Array) numArray1, 0, numArray1.Length);
          int sourceIndex2 = sourceIndex1 + numArray1.Length;
          string str = "";
          foreach (byte num4 in numArray1)
            str += num4.ToString("X").PadLeft(2, '0');
          emp.Pis = str.Substring(str.Length - 12, 12);
          byte[] bytes1 = new byte[52];
          Array.Copy((Array) DeviceDataResponseArray, sourceIndex2, (Array) bytes1, 0, bytes1.Length);
          int sourceIndex3 = sourceIndex2 + bytes1.Length;
          emp.Nome = Encoding.Default.GetString(bytes1).Replace("°", "");
          byte[] numArray2 = new byte[2];
          Array.Copy((Array) DeviceDataResponseArray, sourceIndex3, (Array) numArray2, 0, numArray2.Length);
          int sourceIndex4 = sourceIndex3 + numArray2.Length;
          if (numArray2[0] != (byte) 238 && numArray2[1] != (byte) 238 && numArray2[0] != byte.MaxValue && numArray2[1] != byte.MaxValue && numArray2[0] != (byte) 221 && numArray2[1] != (byte) 221)
          {
            emp.Senha = numArray2[0].ToString("X").PadLeft(2, '0') + numArray2[1].ToString("X").PadLeft(2, '0');
          }
          else
          {
            emp.Senha = "";
            if (numArray2[0] == (byte) 238 && numArray2[1] == (byte) 238)
              emp.VerificaBiometria = true;
            if (numArray2[0] == (byte) 221 && numArray2[1] == (byte) 221)
              emp.DuplaVerificacao = true;
          }
          byte[] bytes2 = new byte[16];
          Array.Copy((Array) DeviceDataResponseArray, sourceIndex4, (Array) bytes2, 0, bytes2.Length);
          int sourceIndex5 = sourceIndex4 + bytes2.Length;
          emp.NomeExibicao = Encoding.Default.GetString(bytes2).TrimStart(' ').TrimEnd(' ').Replace("°", "").Replace("\0", "");
          byte[] numArray3 = new byte[8];
          Array.Copy((Array) DeviceDataResponseArray, sourceIndex5, (Array) numArray3, 0, numArray3.Length);
          int sourceIndex6 = sourceIndex5 + numArray3.Length;
          string s1 = "";
          emp.Cartao = 0UL;
          if (numArray3[0] != (byte) 176)
          {
            foreach (byte num5 in numArray3)
              s1 += num5.ToString("X").PadLeft(2, '0');
            emp.CartaoBarras = ulong.Parse(s1);
          }
          else
            emp.CartaoBarras = 0UL;
          byte[] numArray4 = new byte[8];
          Array.Copy((Array) DeviceDataResponseArray, sourceIndex6, (Array) numArray4, 0, numArray4.Length);
          int sourceIndex7 = sourceIndex6 + numArray4.Length;
          string s2 = "";
          if (numArray4[0] != (byte) 176)
          {
            foreach (byte num6 in numArray4)
              s2 += num6.ToString("X").PadLeft(2, '0');
            emp.CartaoProx = ulong.Parse(s2);
          }
          else
            emp.CartaoProx = 0UL;
          if (emp.CartaoProx != 0UL)
            emp.Cartao = emp.CartaoProx;
          else if (emp.CartaoBarras != 0UL)
            emp.Cartao = emp.CartaoBarras;
          if (!this._chamadaSDK && RegistrySingleton.GetInstance().FORMATO_WIEGAND_DEC)
            this.ConvertToWiegandFC(emp);
          byte[] numArray5 = new byte[8];
          Array.Copy((Array) DeviceDataResponseArray, sourceIndex7, (Array) numArray5, 0, numArray5.Length);
          sourceIndex1 = sourceIndex7 + numArray5.Length;
          string s3 = "";
          if (numArray5[0] != (byte) 176)
          {
            foreach (byte num7 in numArray5)
              s3 += num7.ToString("X").PadLeft(2, '0');
            emp.Teclado = ulong.Parse(s3);
          }
          else
            emp.Teclado = 0UL;
          Application.DoEvents();
          if (!this._chamadaSDK)
          {
            flag = true;
            if (empregadoList.Exists((Predicate<Empregado>) (x => x.Pis == emp.Pis)))
            {
              Empregado empregado2 = !this.ChamadoPelaAT ? new Empregado() : (Empregado) new EmpregadoAT();
              Empregado empregado3 = empregadoList.Find((Predicate<Empregado>) (x => x.Pis == emp.Pis));
              emp.EmpregadoId = empregado3.EmpregadoId;
              empregado2.AtualizarEmpregado(emp);
            }
            else if ((!empregadoList.Exists((Predicate<Empregado>) (x => (long) x.CartaoBarras == (long) emp.CartaoBarras)) || emp.CartaoBarras == 0UL) && (!empregadoList.Exists((Predicate<Empregado>) (x => (long) x.CartaoProx == (long) emp.CartaoProx)) || emp.CartaoProx == 0UL) && (!empregadoList.Exists((Predicate<Empregado>) (x => (long) x.Teclado == (long) emp.Teclado)) || emp.Teclado == 0UL))
            {
              new Empregado().InserirEmpregadoPorEmpregador(emp);
              empregadoList.Add(emp);
            }
          }
          else
            this._listaEmpregadosSDK.Add(emp);
          this.NotificaBytesCripto(index2.ToString());
        }
        if (!this._chamadaSDK && flag)
        {
          RepBase repBase = new RepBase();
          foreach (RepBase RepBaseEnt in (Collection<RepBase>) repBase.PesquisarRepsPorEmpregador(this.objempregador.EmpregadorId))
          {
            RepBaseEnt.Sincronizado = false;
            repBase.AtualizarRep(RepBaseEnt);
          }
        }
        this.NotificaUsuario(Resources.msgDADOS_RB1_IMPORTADOS_SUCESSO, 0);
        return true;
      }
      catch
      {
        this.NotificaUsuario("Falha ao processar o arquivo!", 1);
        return false;
      }
    }

    private bool DesCriptoTextoEmpregador(byte[] texto, ref ushort sequenciaCripto)
    {
      try
      {
        int length = texto.Length;
        byte[] numArray1 = new byte[texto.Length];
        int index1 = 0;
        foreach (uint num1 in texto)
        {
          byte num2 = (byte) ((uint) (byte) (num1 ^ (uint) BitConverter.GetBytes(sequenciaCripto)[1]) ^ (uint) BitConverter.GetBytes(sequenciaCripto)[0]);
          numArray1[index1] = num2;
          sequenciaCripto = this.busca_cri(sequenciaCripto, 0);
          ++index1;
        }
        int num3 = 1;
        this.NotificaMaxBytesCripto(num3.ToString());
        int num4 = 0;
        for (int index2 = 0; index2 < num3; ++index2)
        {
          Empregador empregador1 = new Empregador();
          byte[] numArray2 = new byte[7];
          byte[] numArray3 = new byte[6];
          byte[] bytes1 = new byte[150];
          byte[] bytes2 = new byte[100];
          byte num5 = numArray1[0];
          int sourceIndex1 = num4 + 1;
          Array.Copy((Array) numArray1, sourceIndex1, (Array) numArray2, 0, 7);
          string str1 = "";
          foreach (byte num6 in numArray2)
            str1 += num6.ToString("X").PadLeft(2, '0');
          if (num5 == (byte) 49)
          {
            empregador1.Cnpj = Convert.ToUInt64(str1).ToString("00\\.000\\.000\\/0000\\-00");
            empregador1.isCnpj = true;
          }
          else
          {
            empregador1.Cpf = Convert.ToUInt64(str1.Substring(3, 11)).ToString("000\\.000\\.000\\-00");
            empregador1.isCnpj = false;
          }
          int sourceIndex2 = sourceIndex1 + numArray2.Length;
          Array.Copy((Array) numArray1, sourceIndex2, (Array) numArray3, 0, 6);
          string str2 = "";
          foreach (byte num7 in numArray3)
            str2 += num7.ToString("X").PadLeft(2, '0');
          empregador1.Cei = str2;
          int sourceIndex3 = sourceIndex2 + numArray3.Length;
          Array.Copy((Array) numArray1, sourceIndex3, (Array) bytes1, 0, bytes1.Length);
          int sourceIndex4 = sourceIndex3 + bytes1.Length;
          empregador1.RazaoSocial = Encoding.Default.GetString(bytes1).Replace("°", "");
          empregador1.EmpregadorDesc = Encoding.Default.GetString(bytes1).Replace("°", "");
          Array.Copy((Array) numArray1, sourceIndex4, (Array) bytes2, 0, bytes2.Length);
          num4 = sourceIndex4 + bytes2.Length;
          empregador1.Local = Encoding.Default.GetString(bytes2).Replace("°", "");
          Application.DoEvents();
          if (!this._chamadaSDK)
          {
            Empregador empregador2 = !this.ChamadoPelaAT ? new Empregador() : (Empregador) new EmpregadorAT();
            if (this.ExisteCnpj(empregador1) && !empregador1.Cnpj.Equals("") || this.ExisteCpf(empregador1) && !empregador1.Cpf.Equals(""))
            {
              SortableBindingList<Empregador> sortableBindingList = empregador2.PesquisarEmpregadores();
              int num8 = 0;
              foreach (Empregador empregador3 in (Collection<Empregador>) sortableBindingList)
              {
                if ((empregador3.Cnpj.Equals(empregador1.Cnpj) && !empregador3.Cnpj.Equals("") || empregador3.Cpf.Equals(empregador1.Cpf) && !empregador3.Cpf.Equals("")) && empregador3.Cei.Equals(empregador1.Cei))
                {
                  num8 = empregador3.EmpregadorId;
                  break;
                }
              }
              if (num8 != 0)
              {
                empregador1.EmpregadorId = num8;
                empregador2.AtualizarEmpregador(empregador1);
              }
              else
                empregador2.InserirEmpregador(empregador1);
              this.NotificaUsuario(Resources.msgDADOS_RB1_IMPORTADOS_SUCESSO, 0);
              return true;
            }
            empregador2.InserirEmpregador(empregador1);
          }
          else
            this._entEmpregadorSDK = empregador1;
          this.NotificaBytesCripto(index2.ToString());
        }
        this.NotificaUsuario(Resources.msgDADOS_RB1_IMPORTADOS_SUCESSO, 0);
        return true;
      }
      catch
      {
        this.NotificaUsuario("Falha ao processar o arquivo!", 1);
        return false;
      }
    }

    private Empregador RecuperarRB1Empregador(byte[] texto, ref ushort sequenciaCripto)
    {
      try
      {
        int length = texto.Length;
        byte[] numArray1 = new byte[texto.Length];
        int index1 = 0;
        foreach (uint num1 in texto)
        {
          byte num2 = (byte) ((uint) (byte) (num1 ^ (uint) BitConverter.GetBytes(sequenciaCripto)[1]) ^ (uint) BitConverter.GetBytes(sequenciaCripto)[0]);
          numArray1[index1] = num2;
          sequenciaCripto = this.busca_cri(sequenciaCripto, 0);
          ++index1;
        }
        int num3 = 1;
        this.NotificaMaxBytesCripto(num3.ToString());
        int num4 = 0;
        Empregador empregador = new Empregador();
        for (int index2 = 0; index2 < num3; ++index2)
        {
          byte[] numArray2 = new byte[7];
          byte[] numArray3 = new byte[6];
          byte[] bytes1 = new byte[150];
          byte[] bytes2 = new byte[100];
          byte num5 = numArray1[0];
          int sourceIndex1 = num4 + 1;
          Array.Copy((Array) numArray1, sourceIndex1, (Array) numArray2, 0, 7);
          string str1 = "";
          foreach (byte num6 in numArray2)
            str1 += num6.ToString("X").PadLeft(2, '0');
          if (num5 == (byte) 49)
          {
            empregador.Cnpj = Convert.ToUInt64(str1).ToString("00\\.000\\.000\\/0000\\-00");
            empregador.isCnpj = true;
          }
          else
          {
            empregador.Cpf = Convert.ToUInt64(str1.Substring(3, 11)).ToString("000\\.000\\.000\\-00");
            empregador.isCnpj = false;
          }
          int sourceIndex2 = sourceIndex1 + numArray2.Length;
          Array.Copy((Array) numArray1, sourceIndex2, (Array) numArray3, 0, 6);
          string str2 = "";
          foreach (byte num7 in numArray3)
            str2 += num7.ToString("X").PadLeft(2, '0');
          empregador.Cei = str2;
          int sourceIndex3 = sourceIndex2 + numArray3.Length;
          Array.Copy((Array) numArray1, sourceIndex3, (Array) bytes1, 0, bytes1.Length);
          int sourceIndex4 = sourceIndex3 + bytes1.Length;
          empregador.RazaoSocial = Encoding.Default.GetString(bytes1).Replace("°", "");
          empregador.EmpregadorDesc = Encoding.Default.GetString(bytes1).Replace("°", "");
          Array.Copy((Array) numArray1, sourceIndex4, (Array) bytes2, 0, bytes2.Length);
          num4 = sourceIndex4 + bytes2.Length;
          empregador.Local = Encoding.Default.GetString(bytes2).Replace("°", "");
          Application.DoEvents();
          this.NotificaBytesCripto(index2.ToString());
        }
        this.NotificaUsuario(Resources.msgDADOS_RB1_IMPORTADOS_SUCESSO, 0);
        return empregador;
      }
      catch
      {
        this.NotificaUsuario("Falha ao processar o arquivo!", 1);
        return (Empregador) null;
      }
    }

    private bool DesCriptoTextoConfiguracoes(
      byte[] texto,
      ref ushort sequenciaCripto,
      byte[] numeroSerieArquivo,
      int tipoConfiguracoes)
    {
      try
      {
        int length = texto.Length;
        byte[] numArray1 = new byte[texto.Length];
        AjusteBiometrico ajuste1 = new AjusteBiometrico();
        AjusteBiometrico ajusteBiometrico1 = new AjusteBiometrico();
        AjusteBiometrico ajuste2 = new AjusteBiometrico();
        AjusteBiometrico ajusteBiometrico2 = new AjusteBiometrico();
        int index1 = 0;
        foreach (uint num1 in texto)
        {
          byte num2 = (byte) ((uint) (byte) (num1 ^ (uint) BitConverter.GetBytes(sequenciaCripto)[1]) ^ (uint) BitConverter.GetBytes(sequenciaCripto)[0]);
          numArray1[index1] = num2;
          sequenciaCripto = this.busca_cri(sequenciaCripto, 0);
          ++index1;
        }
        this.NotificaMaxBytesCripto(1.ToString());
        int sourceIndex1 = 0;
        RepBase RepBaseEnt = new RepBase();
        FormatoCartao formatoCartao1 = new FormatoCartao();
        FormatoCartao formatoCartao2 = new FormatoCartao();
        Relogio relogioEnt = new Relogio();
        ConfiguracaoBarras20 config = new ConfiguracaoBarras20();
        byte[] numArray2 = new byte[10];
        Array.Copy((Array) numArray1, sourceIndex1, (Array) numArray2, 0, numArray2.Length);
        int sourceIndex2 = sourceIndex1 + numArray2.Length;
        string s1 = numArray2[0].ToString("X").PadLeft(2, '0') + "/" + numArray2[1].ToString("X").PadLeft(2, '0') + "/" + "20" + numArray2[2].ToString("X").PadLeft(2, '0') + " " + numArray2[3].ToString("X").PadLeft(2, '0') + ":" + numArray2[4].ToString("X").PadLeft(2, '0');
        if (s1.Equals("00/00/2000 00:00"))
          s1 = "01/01/1900 01:01:01";
        string s2 = numArray2[5].ToString("X").PadLeft(2, '0') + "/" + numArray2[6].ToString("X").PadLeft(2, '0') + "/" + "20" + numArray2[7].ToString("X").PadLeft(2, '0') + " " + numArray2[8].ToString("X").PadLeft(2, '0') + ":" + (numArray2[9] == byte.MaxValue ? "00" : numArray2[9].ToString("X").PadLeft(2, '0'));
        if (s2.Equals("00/00/2000 00:00"))
          s2 = "01/01/1900 01:01:01";
        DateTime result1 = new DateTime();
        DateTime.TryParse(s1, (IFormatProvider) new CultureInfo("pt-BR", false), DateTimeStyles.None, out result1);
        DateTime result2 = new DateTime();
        DateTime.TryParse(s2, (IFormatProvider) new CultureInfo("pt-BR", false), DateTimeStyles.None, out result2);
        relogioEnt.InicioHorVerao = result1;
        relogioEnt.FimHorVerao = result2;
        byte[] numArray3 = new byte[3];
        byte[] numArray4 = new byte[3];
        byte[] numArray5 = new byte[3];
        Array.Copy((Array) numArray1, sourceIndex2, (Array) numArray3, 0, numArray3.Length);
        int sourceIndex3 = sourceIndex2 + numArray3.Length;
        string str1 = "";
        foreach (byte num in numArray3)
          str1 += num.ToString("X").PadLeft(2, '0');
        RepBaseEnt.SenhaComunic = str1;
        Array.Copy((Array) numArray1, sourceIndex3, (Array) numArray4, 0, numArray4.Length);
        int sourceIndex4 = sourceIndex3 + numArray4.Length;
        string str2 = "";
        foreach (byte num in numArray4)
          str2 += num.ToString("X").PadLeft(2, '0');
        RepBaseEnt.SenhaBio = str2;
        Array.Copy((Array) numArray1, sourceIndex4, (Array) numArray5, 0, numArray5.Length);
        int sourceIndex5 = sourceIndex4 + numArray5.Length;
        string str3 = "";
        foreach (byte num in numArray5)
          str3 += num.ToString("X").PadLeft(2, '0');
        RepBaseEnt.SenhaRelogio = str3;
        byte[] numArray6 = new byte[16];
        byte[] numArray7 = new byte[16];
        Array.Copy((Array) numArray1, sourceIndex5, (Array) numArray6, 0, numArray6.Length);
        int sourceIndex6 = sourceIndex5 + numArray6.Length;
        string str4 = "";
        foreach (byte num in numArray6)
          str4 = str4 + num.ToString().PadLeft(2, '0') + " ";
        formatoCartao1.formatoCartao = str4.Substring(0, str4.Length - 1);
        Array.Copy((Array) numArray1, sourceIndex6, (Array) numArray7, 0, numArray7.Length);
        int index2 = sourceIndex6 + numArray7.Length;
        string str5 = "";
        foreach (byte num in numArray7)
          str5 = str5 + num.ToString().PadLeft(2, '0') + " ";
        formatoCartao2.formatoCartao = str5.Substring(0, str5.Length - 1);
        if (numArray1[index2] != byte.MaxValue)
          formatoCartao1.numDigitosFixos = (int) numArray1[index2];
        int index3 = index2 + 1;
        if (numArray1[index3] != byte.MaxValue)
          formatoCartao1.formatoCartaoID = (int) numArray1[index3];
        int index4 = index3 + 1;
        if (numArray1[index4] != byte.MaxValue)
          formatoCartao2.formatoCartaoID = (int) numArray1[index4];
        int sourceIndex7 = index4 + 1;
        int formatoId1 = 0;
        int formatoId2 = 0;
        switch (formatoCartao1.formatoCartaoID)
        {
          case 0:
            formatoId1 = 0;
            break;
          case 1:
            formatoId1 = 3;
            break;
          case 2:
            formatoId1 = 4;
            break;
          case 3:
            formatoId1 = 6;
            break;
          case 4:
            formatoId1 = 5;
            break;
          case 5:
            formatoId1 = 9;
            break;
        }
        switch (formatoCartao2.formatoCartaoID)
        {
          case 0:
            formatoId2 = 7;
            break;
          case 1:
            formatoId2 = !(formatoCartao2.formatoCartao == "00 00 00 00 00 00 00 00 00 10 11 12 13 14 15 16") ? 1 : 11;
            break;
          case 2:
            formatoId2 = !(formatoCartao2.formatoCartao == "00 00 00 00 00 06 07 08 09 10 11 12 13 14 15 16") ? (!(formatoCartao2.formatoCartao == "00 00 03 04 05 06 07 08 09 10 11 12 13 14 15 16") ? 2 : 13) : 12;
            break;
        }
        RepBaseEnt.FormatoCartaoId = formatoId1;
        RepBaseEnt.FormatoCartaoProxId = formatoId2;
        if (tipoConfiguracoes == 22)
        {
          config.ignorarFormatoPrincipal = numArray1[sourceIndex7] == (byte) 1;
          RepBaseEnt.FormatoCartaoId = config.ignorarFormatoPrincipal ? 10 : formatoId1;
          formatoId1 = config.ignorarFormatoPrincipal ? 10 : formatoId1;
          int index5 = sourceIndex7 + 1;
          config.tab1QtdDigitos = numArray1[index5] == byte.MaxValue ? 0 : (int) numArray1[index5];
          int sourceIndex8 = index5 + 1;
          byte[] numArray8 = new byte[16];
          string str6 = "";
          Array.Copy((Array) numArray1, sourceIndex8, (Array) numArray8, 0, numArray8.Length);
          int index6 = sourceIndex8 + numArray8.Length;
          foreach (byte num in numArray8)
            str6 = str6 + num.ToString().PadLeft(2, '0') + " ";
          config.tab1DigitosLidos = str6.Trim();
          config.tab1TipoCartao = config.tipoPadraoGerenciador((int) numArray1[index6]);
          int index7 = index6 + 1;
          config.tab2QtdDigitos = numArray1[index7] == byte.MaxValue ? 0 : (int) numArray1[index7];
          int sourceIndex9 = index7 + 1;
          byte[] numArray9 = new byte[16];
          string str7 = "";
          Array.Copy((Array) numArray1, sourceIndex9, (Array) numArray9, 0, numArray9.Length);
          int index8 = sourceIndex9 + numArray9.Length;
          foreach (byte num in numArray9)
            str7 = str7 + num.ToString().PadLeft(2, '0') + " ";
          config.tab2DigitosLidos = str7.Trim();
          config.tab2TipoCartao = config.tipoPadraoGerenciador((int) numArray1[index8]);
          int index9 = index8 + 1;
          config.tab3QtdDigitos = numArray1[index9] == byte.MaxValue ? 0 : (int) numArray1[index9];
          int sourceIndex10 = index9 + 1;
          byte[] numArray10 = new byte[16];
          string str8 = "";
          Array.Copy((Array) numArray1, sourceIndex10, (Array) numArray10, 0, numArray10.Length);
          int index10 = sourceIndex10 + numArray10.Length;
          foreach (byte num in numArray10)
            str8 = str8 + num.ToString().PadLeft(2, '0') + " ";
          config.tab3DigitosLidos = str8.Trim();
          config.tab3TipoCartao = config.tipoPadraoGerenciador((int) numArray1[index10]);
          sourceIndex7 = index10 + 1;
        }
        byte[] numArray11 = new byte[16];
        Array.Copy((Array) numArray1, sourceIndex7, (Array) numArray11, 0, numArray11.Length);
        int sourceIndex11 = sourceIndex7 + numArray11.Length;
        RepBaseEnt.repClient = numArray11[0] != (byte) 0;
        byte[] numArray12 = new byte[4];
        Array.Copy((Array) numArray11, 1, (Array) numArray12, 0, numArray12.Length);
        string str9 = "";
        foreach (byte num in numArray12)
          str9 = str9 + (object) num + ".";
        RepBaseEnt.ipServidor = str9.Substring(0, str9.Length - 1);
        byte[] numArray13 = new byte[2];
        Array.Copy((Array) numArray11, 5, (Array) numArray13, 0, numArray13.Length);
        string s3 = "";
        Array.Reverse((Array) numArray13);
        foreach (byte num in numArray13)
          s3 += num.ToString("X").PadLeft(2, '0');
        RepBaseEnt.portaServidor = (int) long.Parse(s3, NumberStyles.HexNumber);
        if (RepBaseEnt.portaServidor > 60000)
          RepBaseEnt.portaServidor = 60000;
        byte[] numArray14 = new byte[4];
        Array.Copy((Array) numArray11, 7, (Array) numArray14, 0, numArray14.Length);
        string str10 = "";
        foreach (byte num in numArray14)
          str10 = str10 + num.ToString() + ".";
        RepBaseEnt.mascaraRede = str10.Substring(0, str10.Length - 1);
        byte[] numArray15 = new byte[4];
        Array.Copy((Array) numArray11, 11, (Array) numArray15, 0, numArray15.Length);
        string str11 = "";
        foreach (byte num in numArray15)
          str11 = str11 + num.ToString() + ".";
        RepBaseEnt.Gateway = str11.Substring(0, str11.Length - 1);
        try
        {
          RepBaseEnt.intervaloConexao = int.Parse(numArray11[15].ToString("X"));
        }
        catch
        {
          RepBaseEnt.intervaloConexao = 1;
        }
        byte[] bytes1 = new byte[16];
        Array.Copy((Array) numArray1, sourceIndex11, (Array) bytes1, 0, bytes1.Length);
        int num3 = sourceIndex11 + bytes1.Length;
        if (bytes1[0] != byte.MaxValue)
          RepBaseEnt.ChaveComunicacao = Encoding.Default.GetString(bytes1).TrimStart(' ').TrimEnd(' ');
        else
          RepBaseEnt.ChaveComunicacao = "                ";
        RepBaseEnt.Porta = 51000;
        int sourceIndex12 = num3 + 12;
        string str12 = "";
        RepBaseEnt.NomeRep = "";
        if (tipoConfiguracoes == 16 || tipoConfiguracoes == 22)
        {
          byte[] numArray16 = new byte[6];
          Array.Copy((Array) numArray1, sourceIndex12, (Array) numArray16, 0, numArray16.Length);
          int sourceIndex13 = sourceIndex12 + numArray16.Length;
          ajuste1.BioIdent = (int) numArray16[0];
          ajuste1.BioVerif = (int) numArray16[1];
          ajuste1.BioFiltro = (int) numArray16[2];
          ajuste1.BioCaptura = (int) numArray16[3];
          ajuste1.BioTimeOut = (int) numArray16[4];
          ajuste1.BioLFD = (int) numArray16[5];
          byte[] numArray17 = new byte[3];
          Array.Copy((Array) numArray1, sourceIndex13, (Array) numArray17, 0, numArray17.Length);
          int sourceIndex14 = sourceIndex13 + numArray17.Length;
          ajusteBiometrico1.BioIdentCama = (int) numArray17[0];
          ajusteBiometrico1.BioTimeOutCama = (int) numArray17[1];
          ajusteBiometrico1.BioDedoDuplicado = numArray17[2] != (byte) 0;
          if (this._chamadaSDK)
          {
            ajusteBiometrico2.BioIdent = (int) numArray16[0];
            ajusteBiometrico2.BioVerif = (int) numArray16[1];
            ajusteBiometrico2.BioFiltro = (int) numArray16[2];
            ajusteBiometrico2.BioCaptura = (int) numArray16[3];
            ajusteBiometrico2.BioTimeOut = (int) numArray16[4];
            ajusteBiometrico2.BioLFD = (int) numArray16[5];
            ajusteBiometrico2.BioIdentCama = (int) numArray17[0];
            ajusteBiometrico2.BioTimeOutCama = (int) numArray17[1];
            ajusteBiometrico2.BioDedoDuplicado = numArray17[2] != (byte) 0;
          }
          if (tipoConfiguracoes == 22)
          {
            ajuste2.IdentSagem = (int) numArray1[sourceIndex14];
            int index11 = sourceIndex14 + 1;
            ajuste2.VerifSagem = (int) numArray1[index11];
            int index12 = index11 + 1;
            ajuste2.BioFiltro = (int) numArray1[index12];
            int index13 = index12 + 1;
            ajuste2.AdvancedMadchine = numArray1[index13];
            int index14 = index13 + 1;
            ajuste2.TimeoutSagem = (int) numArray1[index14];
            int index15 = index14 + 1;
            ajuste2.DedoDuplicadoSagem = (int) numArray1[index15];
            int index16 = index15 + 1;
            ajuste2.FFD = (int) numArray1[index16];
            sourceIndex14 = index16 + 1;
            if (this._chamadaSDK)
            {
              ajusteBiometrico2.IdentSagem = ajuste2.IdentSagem;
              ajusteBiometrico2.VerifSagem = ajuste2.VerifSagem;
              ajusteBiometrico2.BioFiltro = ajuste2.BioFiltro;
              ajusteBiometrico2.AdvancedMadchine = ajuste2.AdvancedMadchine;
              ajusteBiometrico2.TimeoutSagem = ajuste2.TimeoutSagem;
              ajusteBiometrico2.DedoDuplicadoSagem = ajuste2.DedoDuplicadoSagem;
              ajusteBiometrico2.FFD = ajuste2.FFD;
            }
          }
          byte[] bytes2 = new byte[100];
          Array.Copy((Array) numArray1, sourceIndex14, (Array) bytes2, 0, bytes2.Length);
          int index17 = sourceIndex14 + bytes2.Length;
          this.localRep = Encoding.Default.GetString(bytes2).Replace("°", "");
          RepBaseEnt.TipoConexaoDNS = (int) numArray1[index17];
          int sourceIndex15 = index17 + 1;
          if (RepBaseEnt.TipoConexaoDNS == 3)
            RepBaseEnt.habilitaNuvem = true;
          byte[] bytes3 = new byte[512];
          Array.Copy((Array) numArray1, sourceIndex15, (Array) bytes3, 0, bytes3.Length);
          int sourceIndex16 = sourceIndex15 + bytes3.Length;
          RepBaseEnt.nomeServidor = Encoding.Default.GetString(bytes3).Replace("\0", "").Replace("º", "");
          byte[] numArray18 = new byte[4];
          Array.Copy((Array) numArray1, sourceIndex16, (Array) numArray18, 0, numArray18.Length);
          int sourceIndex17 = sourceIndex16 + numArray18.Length;
          string str13 = "";
          foreach (byte num4 in numArray18)
            str13 = str13 + num4.ToString() + ".";
          RepBaseEnt.DNS = str13.Substring(0, str13.Length - 1);
          if (tipoConfiguracoes == 22)
          {
            byte[] numArray19 = new byte[4];
            Array.Copy((Array) numArray1, sourceIndex17, (Array) numArray19, 0, numArray19.Length);
            int sourceIndex18 = sourceIndex17 + numArray19.Length;
            RepBaseEnt.intervaloNuvem = BitConverter.ToInt32(numArray19, 0);
            byte[] numArray20 = new byte[2];
            Array.Copy((Array) numArray1, sourceIndex18, (Array) numArray20, 0, numArray20.Length);
            int num5 = sourceIndex18 + numArray20.Length;
            string s4 = "";
            Array.Reverse((Array) numArray20);
            foreach (byte num6 in numArray20)
              s4 += num6.ToString("X").PadLeft(2, '0');
            RepBaseEnt.portaNuvem = int.Parse(s4, NumberStyles.HexNumber);
          }
        }
        int num7 = 0;
        if (this.repId == 0)
        {
          RepBaseEnt.Ip = "192.168.0.0";
          RepBaseEnt.Desc = "REP";
          RepBaseEnt.Porta = 51000;
          RepBaseEnt.Desc = this.localRep;
          RepBaseEnt.Local = this.localRep;
          string str14 = "";
          foreach (byte num8 in numeroSerieArquivo)
            str14 += num8.ToString("X").PadLeft(2, '0');
          RepBaseEnt.Serial = str14.Substring(1, 17);
          bool flag1 = false;
          bool flag2 = false;
          if (ajusteBiometrico1.BioIdentCama != 0 && ajusteBiometrico1.BioTimeOutCama != 0)
            flag1 = true;
          if (ajuste2.IdentSagem != 0 && ajuste2.VerifSagem != 0)
            flag2 = true;
          RepBaseEnt.TerminalId = RegistrySingleton.GetInstance().EQUIPAMENTO_TOPDATA ? (int.Parse(RepBaseEnt.Serial.Substring(5, 5)) == 364 || int.Parse(RepBaseEnt.Serial.Substring(5, 5)) == 365 || int.Parse(RepBaseEnt.Serial.Substring(5, 5)) == 366 ? (!flag1 ? (!flag2 ? 13 : 26) : 17) : (int.Parse(RepBaseEnt.Serial.Substring(5, 5)) != 99999 ? (int.Parse(RepBaseEnt.Serial.Substring(5, 5)) != 18 ? 13 : (!flag1 ? (!flag2 ? 24 : 32) : 25)) : (!flag1 ? (!flag2 ? 16 : 27) : 18))) : (!flag2 ? (int.Parse(RepBaseEnt.Serial.Substring(5, 5)) != 20 ? (int.Parse(RepBaseEnt.Serial.Substring(5, 5)) == 514 || int.Parse(RepBaseEnt.Serial.Substring(5, 5)) == 515 || int.Parse(RepBaseEnt.Serial.Substring(5, 5)) == 516 || int.Parse(RepBaseEnt.Serial.Substring(5, 5)) == 517 || int.Parse(RepBaseEnt.Serial.Substring(5, 5)) == 518 || tipoConfiguracoes == 22 ? 29 : 21) : 31) : (int.Parse(RepBaseEnt.Serial.Substring(5, 5)) != 20 ? 28 : 30));
          if (!this._chamadaSDK)
          {
            RepBaseEnt.EmpregadorId = this.objempregador.EmpregadorId;
            RepBase repBase = !this.ChamadoPelaAT ? new RepBase() : (RepBase) new RepBaseAT();
            foreach (RepBase pesquisarRep in repBase.PesquisarReps())
            {
              if (pesquisarRep.Serial.Equals(RepBaseEnt.Serial))
              {
                this.NotificaUsuario(Resources.msgRB1_REP_JA_CADASTRADO_NA_BASE, 1);
                return false;
              }
            }
            repBase.InserirRep(RepBaseEnt);
            RepAFD _repAFD = new RepAFD();
            RepAFD repAfd = new RepAFD();
            _repAFD.repID = repBase.PesquisarUltimoIdRep();
            repAfd.AtualizarSerialREP(_repAFD, RepBaseEnt.Serial);
            TipoTerminal tipoTerminal = new TipoTerminal();
            tipoTerminal.InserirConfiguracoesRep(RepBaseEnt);
            num7 = repBase.PesquisarUltimoIdConfiguracaoGeral();
            if (formatoId1 == 0)
              FormatoCartao.AtualizarFormatoPadraoLivreRepPlus(formatoCartao1.formatoCartao, formatoId1, formatoCartao1.numDigitosFixos, num7);
            if (formatoId2 == 7)
              FormatoCartao.AtualizarFormatoPadraoAbatrackRepPlus(formatoCartao2.formatoCartao, formatoId2, formatoCartao2.numDigitosFixos, num7);
            tipoTerminal.InserirConfiguracoesBio(num7, 0, ajusteBiometrico1);
            if (tipoConfiguracoes == 16 || tipoConfiguracoes == 22)
            {
              if (ajuste1.BioIdent == 0 && ajuste1.BioVerif == 0 && ajuste1.BioCaptura == 0 && ajuste1.BioTimeOut == 0 && ajuste1.BioLFD == 0)
              {
                if (ajusteBiometrico1.BioIdentCama != 0 && ajusteBiometrico1.BioTimeOutCama != 0)
                {
                  int num9 = this.EncontrouAjusteBioCompativelDB(ajusteBiometrico1, 1);
                  if (num9 != 0)
                  {
                    RepBio repBio = new RepBio();
                    RepBio RepBioEnt = new RepBio();
                    RepBioEnt.CpfResponsavel = "";
                    RepBioEnt.DataHoraEnvio = DateTime.MaxValue;
                    RepBioEnt.ConfiguracaoId = num7;
                    RepBioEnt.AjusteBiometricoId = num9;
                    repBio.AtualizarConfiguracaoBio(RepBioEnt);
                  }
                }
                else if (ajuste2.IdentSagem != 0 && ajuste2.VerifSagem != 0)
                {
                  int num10 = this.EncontrouAjusteBioCompativelDB(ajuste2, 2);
                  if (num10 != 0)
                  {
                    RepBio repBio = new RepBio();
                    RepBio RepBioEnt = new RepBio();
                    RepBioEnt.CpfResponsavel = "";
                    RepBioEnt.DataHoraEnvio = DateTime.MaxValue;
                    RepBioEnt.ConfiguracaoId = num7;
                    RepBioEnt.AjusteBiometricoId = num10;
                    RepBioEnt.FFD = ajuste2.FFD;
                    repBio.AtualizarConfiguracaoBio(RepBioEnt);
                  }
                }
              }
              else
              {
                int num11 = this.EncontrouAjusteBioCompativelDB(ajuste1, 0);
                if (num11 != 0)
                {
                  RepBio repBio = new RepBio();
                  RepBio RepBioEnt = new RepBio();
                  RepBioEnt.CpfResponsavel = "";
                  RepBioEnt.DataHoraEnvio = DateTime.MaxValue;
                  RepBioEnt.ConfiguracaoId = num7;
                  RepBioEnt.AjusteBiometricoId = num11;
                  RepBioEnt.BioLFD = ajuste1.BioLFD;
                  repBio.AtualizarConfiguracaoBio(RepBioEnt);
                }
              }
            }
          }
          else
          {
            this._RelogioRepSDK = relogioEnt;
            this._FormatoCartaoBarrasSDK = formatoCartao1;
            this._FormatoCartaoProxSDK = formatoCartao2;
            this._entConfigRepSDK = RepBaseEnt;
            this._AjusteBiometricoSDK = ajusteBiometrico2;
            this._Barras20SDK = config;
          }
        }
        else
        {
          RepBaseEnt.RepId = this.repId;
          RepBase repBase1 = !this.ChamadoPelaAT ? new RepBase() : (RepBase) new RepBaseAT();
          RepBase repBase2 = repBase1.PesquisarRepPorID(this.repId);
          RepBaseEnt.ConfiguracaoId = repBase2.ConfiguracaoId;
          RepBaseEnt.Ip = repBase2.Ip;
          RepBaseEnt.Desc = repBase2.Desc;
          RepBaseEnt.Local = repBase2.Local;
          RepBaseEnt.Serial = repBase2.Serial;
          RepBaseEnt.EmpregadorId = repBase2.EmpregadorId;
          RepBaseEnt.TerminalId = repBase2.TerminalId;
          repBase1.AtualizarRep(RepBaseEnt);
          new TipoTerminal().AtualizarConfiguracoesRep(RepBaseEnt);
          num7 = repBase2.ConfiguracaoId;
        }
        if (!this._chamadaSDK)
        {
          if (formatoCartao1.formatoCartaoID == 0 && formatoId1 != 10)
            FormatoCartao.AtualizarFormatoPadraoLivreRepPlus(formatoCartao1.formatoCartao, formatoCartao1.formatoCartaoID, formatoCartao1.numDigitosFixos, num7);
          if (formatoCartao2.formatoCartaoID == 7)
            FormatoCartao.AtualizarFormatoPadraoAbatrackRepPlus(formatoCartao2.formatoCartao, formatoCartao2.formatoCartaoID, formatoCartao2.numDigitosFixos, num7);
          relogioEnt.ConfiguracaoId = num7;
          new Relogio().AtualizarHorVeraoMulti(relogioEnt);
          ArquivoCFGEntity _arquivoCFGEnt = new ArquivoCFGEntity();
          _arquivoCFGEnt.Tlm10Digitos = false;
          _arquivoCFGEnt.CFG = str12;
          _arquivoCFGEnt.NomeEmpresa = string.Empty;
          RepBase repBase = new RepBase();
          _arquivoCFGEnt.RepId = repBase.PesquisarUltimoIdRep();
          new ArquivoCFGBI().InserirArquivoCFG(_arquivoCFGEnt);
          if (!string.IsNullOrEmpty(config.tab1DigitosLidos) && config.tab1DigitosLidos != "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00" || !string.IsNullOrEmpty(config.tab2DigitosLidos) && config.tab2DigitosLidos != "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00" || !string.IsNullOrEmpty(config.tab3DigitosLidos) && config.tab3DigitosLidos != "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00")
          {
            ConfiguracaoBarras20 configuracaoBarras20 = new ConfiguracaoBarras20();
            config.RepId = _arquivoCFGEnt.RepId;
            configuracaoBarras20.Gravar(config);
          }
        }
        Application.DoEvents();
        this.NotificaBytesCripto("1");
        this.NotificaUsuario(Resources.msgDADOS_RB1_IMPORTADOS_SUCESSO, 0);
        return true;
      }
      catch
      {
        this.NotificaUsuario("Falha ao processar o arquivo!", 1);
        return false;
      }
    }

    private int EncontrouAjusteBioCompativelDB(AjusteBiometrico ajuste, int tipoPlacaFim)
    {
      int num = 0;
      foreach (RepBio repBio in new RepBio().RecuperaListaAjusteBiometrico())
      {
        switch (tipoPlacaFim)
        {
          case 0:
            if (ajuste.BioIdent == repBio.BioIdent && ajuste.BioVerif == repBio.BioVerif && ajuste.BioFiltro == repBio.BioFiltro && ajuste.BioCaptura == repBio.BioCaptura && ajuste.BioTimeOut == repBio.BioTimeOut)
              return repBio.AjusteBiometricoId;
            continue;
          case 1:
            if (ajuste.BioIdentCama == repBio.BioIdentCama && ajuste.BioTimeOutCama == repBio.BioTimeOutCama)
              return repBio.AjusteBiometricoId;
            continue;
          case 2:
            if (ajuste.VerifSagem == repBio.VerifSagem && ajuste.IdentSagem == repBio.VerifSagem)
              return repBio.AjusteBiometricoId;
            continue;
          default:
            continue;
        }
      }
      return num;
    }

    private bool DesCriptoTextoTemplates(
      byte[] texto,
      ref ushort sequenciaCripto,
      byte tipoTemplate)
    {
      try
      {
        int length = texto.Length;
        byte[] numArray1 = new byte[texto.Length];
        List<TemplatesBio> templatesBioList = new List<TemplatesBio>();
        int index1 = 0;
        foreach (uint num1 in texto)
        {
          byte num2 = (byte) ((uint) (byte) (num1 ^ (uint) BitConverter.GetBytes(sequenciaCripto)[1]) ^ (uint) BitConverter.GetBytes(sequenciaCripto)[0]);
          numArray1[index1] = num2;
          sequenciaCripto = this.busca_cri(sequenciaCripto, 0);
          ++index1;
        }
        int num3 = (int) short.Parse(numArray1[3].ToString("X").PadLeft(2, '0') + numArray1[2].ToString("X").PadLeft(2, '0'), NumberStyles.AllowHexSpecifier);
        int num4 = (int) short.Parse(numArray1[1].ToString("X").PadLeft(2, '0') + numArray1[0].ToString("X").PadLeft(2, '0'), NumberStyles.AllowHexSpecifier);
        this.NotificaMaxBytesCripto(num3.ToString());
        int sourceIndex1 = 11;
        if (tipoTemplate == (byte) 20)
          sourceIndex1 = 6;
        for (int index2 = 0; index2 < num3; ++index2)
        {
          TemplatesBio templateBio1 = new TemplatesBio();
          byte[] numArray2 = new byte[6];
          byte[] numArray3 = new byte[16];
          byte[] numArray4 = new byte[0];
          byte[] numArray5 = new byte[0];
          byte[] numArray6;
          byte[] numArray7;
          switch (tipoTemplate)
          {
            case 13:
            case 18:
              numArray6 = new byte[498];
              numArray7 = new byte[498];
              break;
            case 20:
              numArray6 = new byte[256];
              numArray7 = new byte[256];
              break;
            default:
              numArray6 = new byte[404];
              numArray7 = new byte[404];
              break;
          }
          Array.Copy((Array) numArray1, sourceIndex1, (Array) numArray2, 0, numArray2.Length);
          int num5 = sourceIndex1 + numArray2.Length;
          StringBuilder stringBuilder1 = new StringBuilder();
          int num6 = 0;
          foreach (int num7 in numArray2)
          {
            if (num6 != 8)
            {
              int num8 = num7 - 1;
              stringBuilder1.Append(num8.ToString("x").PadLeft(2, '0'));
              ++num6;
            }
            else
              break;
          }
          templateBio1.Pis = stringBuilder1.ToString().Substring(0, 12);
          string str1 = "";
          if (tipoTemplate != (byte) 20)
          {
            Array.Copy((Array) numArray1, num5 + 3, (Array) numArray3, 0, numArray3.Length);
            int num9 = num5 + numArray3.Length;
            foreach (byte num10 in numArray3)
              str1 += num10.ToString("X").PadLeft(2, '0');
            int index3 = num9 + 3;
            templateBio1.InfoNivelSeguranca = (int) numArray1[index3];
            int index4 = index3 + 1;
            templateBio1.NivelSeguranca = (int) numArray1[index4];
            num5 = index4 + 1;
          }
          templateBio1.Senha = str1;
          int sourceIndex2 = tipoTemplate == (byte) 13 || tipoTemplate == (byte) 18 ? num5 + 38 : (tipoTemplate != (byte) 20 ? num5 + 34 : num5 + 8);
          Array.Copy((Array) numArray1, sourceIndex2, (Array) numArray6, 0, numArray6.Length);
          int sourceIndex3 = sourceIndex2 + numArray6.Length;
          StringBuilder stringBuilder2 = new StringBuilder();
          foreach (byte num11 in numArray6)
          {
            string str2 = num11.ToString("x");
            if (str2.Length == 1)
              str2 = "0" + str2;
            stringBuilder2.Append(str2);
          }
          templateBio1.Template1 = stringBuilder2.ToString();
          if (tipoTemplate == (byte) 11 || tipoTemplate == (byte) 18 || tipoTemplate == (byte) 20)
          {
            if (tipoTemplate == (byte) 18)
              sourceIndex3 += 4;
            Array.Copy((Array) numArray1, sourceIndex3, (Array) numArray7, 0, numArray7.Length);
            sourceIndex3 += numArray7.Length;
            bool flag = true;
            StringBuilder stringBuilder3 = new StringBuilder();
            foreach (byte num12 in numArray7)
            {
              string str3 = num12.ToString("x");
              if (str3.Length == 1)
                str3 = "0" + str3;
              stringBuilder3.Append(str3);
              if (num12 > (byte) 0)
                flag = false;
            }
            templateBio1.Template2 = stringBuilder3.ToString();
            if (flag)
              templateBio1.Template2 = templateBio1.Template1;
          }
          else
            templateBio1.Template2 = templateBio1.Template1;
          sourceIndex1 = tipoTemplate != (byte) 20 ? sourceIndex3 + 7 : sourceIndex3 + 2;
          Application.DoEvents();
          this.NotificaBytesCripto(index2.ToString());
          if (!this._chamadaSDK)
          {
            Empregado empregado = (!this.ChamadoPelaAT ? new Empregado() : (Empregado) new EmpregadoAT()).PesquisarEmpregadosPorPis(new Empregado()
            {
              Pis = templateBio1.Pis,
              EmpregadorId = this.objempregador.EmpregadorId
            });
            templateBio1.EmpregadoID = empregado.EmpregadoId;
            templateBio1.EmpregadorID = this.objempregador.EmpregadorId;
            TemplateBio templateBio2 = new TemplateBio();
            if (templateBio1.EmpregadoID > 0)
            {
              templateBio2.IniciarTransacao();
              try
              {
                switch (tipoTemplate)
                {
                  case 13:
                  case 18:
                    templateBio1.TipoTemplate = 4;
                    templateBio2.ExcluirTemplatesCAMA(templateBio1, this.objempregador.EmpregadorId);
                    templateBio2.InserirTemplatesCAMA(templateBio1);
                    break;
                  case 20:
                    templateBio1.TipoTemplate = 5;
                    templateBio2.ExcluirTemplates(templateBio1, this.objempregador.EmpregadorId, "TemplatesSagem");
                    templateBio2.InserirTemplates(templateBio1, "TemplatesSagem");
                    break;
                  default:
                    templateBio1.TipoTemplate = 2;
                    templateBio2.ExcluirTemplates(templateBio1, this.objempregador.EmpregadorId, "TemplatesNitgen");
                    templateBio2.InserirTemplates(templateBio1, "TemplatesNitgen");
                    break;
                }
                templateBio2.ComitarTransacao();
              }
              catch
              {
                templateBio2.CancelarTransacao();
              }
            }
          }
          else
            this._listaTemplatesSDK.Add(new UsuarioBio()
            {
              Pis = templateBio1.Pis,
              Template1 = templateBio1.Template1,
              Template2 = templateBio1.Template2
            });
        }
        this.NotificaUsuario(Resources.msgDADOS_RB1_IMPORTADOS_SUCESSO, 0);
        return true;
      }
      catch
      {
        this.NotificaUsuario("Falha ao processar o arquivo!", 1);
        return false;
      }
    }

    public bool ExisteCpf(Empregador _empregadorEnt)
    {
      bool flag = false;
      try
      {
        return new Empregador().ExisteCpf(_empregadorEnt);
      }
      catch
      {
        return flag;
      }
    }

    public bool ExisteCnpj(Empregador _empregadorEntr)
    {
      bool flag = false;
      try
      {
        return new Empregador().ExisteCnpj(_empregadorEntr);
      }
      catch
      {
        return flag;
      }
    }

    public bool ExisteCei(Empregador _empregadorEntr)
    {
      bool flag = false;
      try
      {
        return new Empregador().ExisteCei(_empregadorEntr);
      }
      catch
      {
        return flag;
      }
    }

    private int CalculaCheckSum(byte[] linhasArq)
    {
      int num = 0;
      try
      {
        byte[] DeviceDataResponseArray = linhasArq;
        CriptoBI.DeslocaBufferResposta(ref DeviceDataResponseArray, 1024);
        for (int index = 0; index < DeviceDataResponseArray.Length - 516; ++index)
          num += (int) DeviceDataResponseArray[index];
      }
      catch
      {
        return int.MinValue;
      }
      return num;
    }

    public static void DeslocaBufferResposta(ref byte[] DeviceDataResponseArray, int deslocamento)
    {
      byte[] numArray = new byte[DeviceDataResponseArray.Length - deslocamento];
      Array.Copy((Array) DeviceDataResponseArray, deslocamento, (Array) numArray, 0, DeviceDataResponseArray.Length - deslocamento);
      DeviceDataResponseArray = numArray;
    }

    private byte[] GetSementeCabecalho(string cabecalho, int pos)
    {
      byte[] numArray = new byte[2];
      try
      {
        numArray[0] = byte.Parse(cabecalho.Substring(pos, 2), NumberStyles.HexNumber);
        numArray[1] = byte.Parse(cabecalho.Substring(pos + 2, 2), NumberStyles.HexNumber);
      }
      catch
      {
        this.NotificaUsuario("Atenção. Erro ao recuperar semente da criptografia.", 1);
        return (byte[]) null;
      }
      return numArray;
    }

    private void NotificaUsuario(string msg, int status)
    {
      if (this.OnNotificarCripto == null)
        return;
      this.OnNotificarCripto((object) this, new NotificarEventArgsCripto(msg, status));
    }

    private void NotificaTipoCripto(string msg, int status)
    {
      if (this.OnNotificarTipoCripto == null)
        return;
      this.OnNotificarTipoCripto((object) this, new NotificarEventArgsCripto(msg, status));
    }

    private void NotificaBytesCripto(string msg)
    {
      if (this.OnNotificarBytesProcessadosCripto == null)
        return;
      this.OnNotificarBytesProcessadosCripto((object) this, new NotificarEventArgsCripto(msg, 2));
    }

    private void NotificaMaxBytesCripto(string msg)
    {
      if (this.OnNotificaMaxBytesCripto == null)
        return;
      this.OnNotificaMaxBytesCripto((object) this, new NotificarEventArgsCripto(msg, 2));
    }

    private string CentralizaNomeExibicao(string nome)
    {
      StringBuilder stringBuilder = new StringBuilder(nome);
      if (nome == string.Empty)
      {
        while (stringBuilder.Length < 16)
          stringBuilder.Append(Convert.ToChar(176));
        return stringBuilder.ToString();
      }
      while (stringBuilder.Length < 16)
      {
        stringBuilder.Append(" ");
        if (stringBuilder.Length == 16)
          return stringBuilder.ToString();
        stringBuilder.Insert(0, " ");
      }
      return stringBuilder.ToString();
    }

    public static bool ValidaCaracteresInvalidos(string caracter, bool autenticacao)
    {
      int utf32 = char.ConvertToUtf32(caracter, 0);
      return (utf32 >= 44 && (utf32 <= 57 || utf32 >= 65) && (utf32 <= 90 || utf32 >= 97) && (utf32 <= 122 || utf32 >= 192) && (utf32 <= 207 || utf32 >= 210) && (utf32 <= 132 || utf32 >= 136) && (utf32 <= 136 || utf32 >= 143) && (utf32 <= 161 || utf32 >= 192) && (utf32 <= 151 || utf32 >= 160) && (utf32 <= 220 || utf32 >= 224) && utf32 != 241 && utf32 != 253 && utf32 != (int) byte.MaxValue && utf32 != 248 && utf32 != 246 && utf32 != 247 && utf32 != 230 && utf32 != 198 || utf32 == 32 || utf32 == 8 || utf32 == 180 || utf32 == 170) && utf32 <= 300 || autenticacao && utf32 == 42;
    }

    public static bool ValidaPIS(string vrPIS)
    {
      string str = vrPIS.Replace(".", "").Replace("-", "").Replace("//", "");
      int[] numArray1 = new int[11];
      int[] numArray2 = new int[10]
      {
        3,
        2,
        9,
        8,
        7,
        6,
        5,
        4,
        3,
        2
      };
      int num1 = 0;
      long num2 = 9999;
      if (str.Length != 11)
        return false;
      for (int index = 0; index < 11; ++index)
        numArray1[index] = int.Parse(str[index].ToString());
      if (long.Parse(str.Substring(0, 10)) < num2)
        return false;
      for (int index = 0; index < 10; ++index)
        num1 += numArray1[index] * numArray2[index];
      int num3 = 11 - num1 % 11;
      if (num3 > 9)
        num3 = 0;
      return num3 == numArray1[10];
    }

    public static bool ValidaCPF(string vrCPF)
    {
      string str = vrCPF.Replace(".", "").Replace("-", "");
      if (str.Length != 11)
        return false;
      bool flag = true;
      for (int index = 1; index < 11 && flag; ++index)
      {
        if ((int) str[index] != (int) str[0])
          flag = false;
      }
      if (flag || str == "12345678909")
        return false;
      int[] numArray = new int[11];
      for (int index = 0; index < 11; ++index)
        numArray[index] = int.Parse(str[index].ToString());
      int num1 = 0;
      for (int index = 0; index < 9; ++index)
        num1 += (10 - index) * numArray[index];
      int num2 = num1 % 11;
      switch (num2)
      {
        case 0:
        case 1:
          if (numArray[9] != 0)
            return false;
          break;
        default:
          if (numArray[9] != 11 - num2)
            return false;
          break;
      }
      int num3 = 0;
      for (int index = 0; index < 10; ++index)
        num3 += (11 - index) * numArray[index];
      int num4 = num3 % 11;
      switch (num4)
      {
        case 0:
        case 1:
          if (numArray[10] != 0)
            return false;
          break;
        default:
          if (numArray[10] != 11 - num4)
            return false;
          break;
      }
      return true;
    }

    public static bool ValidaCNPJ(string vrCNPJ)
    {
      string str1 = vrCNPJ.Replace(".", "").Replace("/", "").Replace("-", "");
      string str2 = "6543298765432";
      int[] numArray1 = new int[14];
      int[] numArray2 = new int[2]{ 0, 0 };
      int[] numArray3 = new int[2]{ 0, 0 };
      bool[] flagArray = new bool[2]{ false, false };
      try
      {
        for (int startIndex = 0; startIndex < 14; ++startIndex)
        {
          numArray1[startIndex] = int.Parse(str1.Substring(startIndex, 1));
          if (startIndex <= 11)
            numArray2[0] += numArray1[startIndex] * int.Parse(str2.Substring(startIndex + 1, 1));
          if (startIndex <= 12)
            numArray2[1] += numArray1[startIndex] * int.Parse(str2.Substring(startIndex, 1));
        }
        for (int index = 0; index < 2; ++index)
        {
          numArray3[index] = numArray2[index] % 11;
          flagArray[index] = numArray3[index] == 0 || numArray3[index] == 1 ? numArray1[12 + index] == 0 : numArray1[12 + index] == 11 - numArray3[index];
        }
        return flagArray[0] && flagArray[1];
      }
      catch
      {
        return false;
      }
    }
  }
}
