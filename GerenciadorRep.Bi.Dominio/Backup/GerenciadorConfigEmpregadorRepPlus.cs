// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigEmpregadorRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigEmpregadorRepPlus : TarefaAbstrata
  {
    private bool _versao1_1;
    private bool _liberacaoAt;
    private RepBase _rep;
    private GerenciadorConfigEmpregadorRepPlus.Estados _estadoRep;
    private Empregador _empregadorNoCadastro = new Empregador();
    private bool _chamadaPeloSdk;
    private Empregador _empregadorNoREP = new Empregador();
    private string _localPrestacaoServicoNoREP;
    private bool _empregadorREPDiferenteDeEmpregadorDB;
    public static GerenciadorConfigEmpregadorRepPlus _gerenciadorConfigEmpregadorRepPlus;

    public event EventHandler<NotificarParaUsuarioEmpregadorREPDiferenteEventArgs> OnNotificarParaUsuarioEmpregadorREPDiferente;

    public string localPrestacaoServicoNoREP
    {
      get => this._localPrestacaoServicoNoREP;
      set => this._localPrestacaoServicoNoREP = value;
    }

    public bool empregadorREPDiferenteDeEmpregadorDB
    {
      get => this._empregadorREPDiferenteDeEmpregadorDB;
      set => this._empregadorREPDiferenteDeEmpregadorDB = value;
    }

    public static GerenciadorConfigEmpregadorRepPlus getInstance() => GerenciadorConfigEmpregadorRepPlus._gerenciadorConfigEmpregadorRepPlus != null ? GerenciadorConfigEmpregadorRepPlus._gerenciadorConfigEmpregadorRepPlus : new GerenciadorConfigEmpregadorRepPlus();

    public static GerenciadorConfigEmpregadorRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigEmpregadorRepPlus._gerenciadorConfigEmpregadorRepPlus != null ? GerenciadorConfigEmpregadorRepPlus._gerenciadorConfigEmpregadorRepPlus : new GerenciadorConfigEmpregadorRepPlus(rep);
    }

    public static GerenciadorConfigEmpregadorRepPlus getInstance(
      RepBase rep,
      Empregador empregadorSdk)
    {
      return GerenciadorConfigEmpregadorRepPlus._gerenciadorConfigEmpregadorRepPlus != null ? GerenciadorConfigEmpregadorRepPlus._gerenciadorConfigEmpregadorRepPlus : new GerenciadorConfigEmpregadorRepPlus(rep, empregadorSdk);
    }

    public static GerenciadorConfigEmpregadorRepPlus getInstance(
      RepBase rep,
      bool liberacaoAt)
    {
      return GerenciadorConfigEmpregadorRepPlus._gerenciadorConfigEmpregadorRepPlus != null ? GerenciadorConfigEmpregadorRepPlus._gerenciadorConfigEmpregadorRepPlus : new GerenciadorConfigEmpregadorRepPlus(rep, liberacaoAt);
    }

    public GerenciadorConfigEmpregadorRepPlus()
    {
    }

    public GerenciadorConfigEmpregadorRepPlus(RepBase rep)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
    }

    public GerenciadorConfigEmpregadorRepPlus(RepBase rep, bool liberacaoAt)
    {
      this._rep = rep;
      this._liberacaoAt = liberacaoAt;
      this._chamadaPeloSdk = false;
    }

    public GerenciadorConfigEmpregadorRepPlus(RepBase rep, Empregador empregadorSdk)
    {
      this._rep = rep;
      this._empregadorNoCadastro.RazaoSocial = empregadorSdk.RazaoSocial;
      this._empregadorNoCadastro.Cnpj = empregadorSdk.Cnpj;
      this._empregadorNoCadastro.Cpf = empregadorSdk.Cpf;
      this._empregadorNoCadastro.Cei = empregadorSdk.Cei;
      this._empregadorNoCadastro.isCnpj = empregadorSdk.isCnpj;
      this._chamadaPeloSdk = true;
    }

    private void ProcessarDadosEmpregador()
    {
      this._estadoRep = GerenciadorConfigEmpregadorRepPlus.Estados.estProcessaDadosEmpregador;
      if (!this._chamadaPeloSdk)
      {
        this._empregadorNoCadastro = (!this._liberacaoAt ? new Empregador() : (Empregador) new EmpregadorAt()).PesquisarEmpregadorDeUmREP(this._rep.RepId);
        if (this._rep.Local.Trim().ToString().Equals("") && !this._liberacaoAt)
        {
          this.NotificarParaUsuario(Resources.msgAVISO_OBRIGATORIO_PREENCH_CAMPO_LOCAL, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          return;
        }
        if (this._empregadorNoCadastro.isCnpj)
          this._empregadorNoCadastro.Cnpj = this._empregadorNoCadastro.Cnpj.Replace(",", string.Empty).Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty).PadLeft(14, '0');
        else
          this._empregadorNoCadastro.Cpf = this._empregadorNoCadastro.Cpf.Replace(",", string.Empty).Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty).PadLeft(14, '0');
        this._empregadorNoCadastro.Cei = this._empregadorNoCadastro.Cei.Trim().Length <= 0 ? string.Empty : this._empregadorNoCadastro.Cei.Replace(".", string.Empty).Replace("-", string.Empty).PadLeft(12, '0');
      }
      this._empregadorNoCadastro.Local = this._rep.Local;
      if (!this._liberacaoAt)
        this.ComparaDadosEmpregador();
      else
        this.ComparaDadosEmpregadorAT();
      this.NotificaMensagemParaUsuarioEmpregadorREPDiferente(this.empregadorREPDiferenteDeEmpregadorDB);
      if (this.empregadorREPDiferenteDeEmpregadorDB)
      {
        this.EnviaDadosEmpregador();
      }
      else
      {
        this.EncerrarConexao();
        this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_EMPREGADOR_FINALIZADO_SEM_ALTERACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
      }
    }

    private void ComparaDadosEmpregador()
    {
      if (!this._empregadorNoREP.Cei.Trim().Equals(this._empregadorNoCadastro.Cei.Trim()) || !this._empregadorNoREP.Cpf.Trim().Equals(this._empregadorNoCadastro.Cpf.Trim()) || !this._empregadorNoREP.Cnpj.Trim().Equals(this._empregadorNoCadastro.Cnpj.Trim()) || !this._empregadorNoREP.isCnpj.Equals(this._empregadorNoCadastro.isCnpj) || !this._empregadorNoREP.RazaoSocial.Trim().Equals(this._empregadorNoCadastro.RazaoSocial.Trim()) || !this._empregadorNoREP.Local.Trim().Equals(this._empregadorNoCadastro.Local.Trim()))
        this.empregadorREPDiferenteDeEmpregadorDB = true;
      else
        this.empregadorREPDiferenteDeEmpregadorDB = false;
    }

    private void ComparaDadosEmpregadorAT()
    {
      if (!this._empregadorNoREP.Cei.Trim().Equals(this._empregadorNoCadastro.Cei.Trim()) || !this._empregadorNoREP.Cpf.Trim().Equals(this._empregadorNoCadastro.Cpf.Trim()) || !this._empregadorNoREP.Cnpj.Trim().Equals(this._empregadorNoCadastro.Cnpj.Trim()) || !this._empregadorNoREP.isCnpj.Equals(this._empregadorNoCadastro.isCnpj) || !this._empregadorNoREP.RazaoSocial.ToUpper().Trim().Equals(this._empregadorNoCadastro.RazaoSocial.ToUpper().Trim()) || !this._empregadorNoREP.Local.ToUpper().Trim().Equals(this._empregadorNoCadastro.Local.ToUpper().Trim()))
        this.empregadorREPDiferenteDeEmpregadorDB = true;
      else
        this.empregadorREPDiferenteDeEmpregadorDB = false;
    }

    private void RecuperaEmpregadorDoREP(Envelope envelope) => this._empregadorNoREP = new Empregador(envelope.MsgAplicacaoEmBytes, true);

    private void NotificaMensagemParaUsuarioEmpregadorREPDiferente(bool empregadorREPDiferente)
    {
      NotificarParaUsuarioEmpregadorREPDiferenteEventArgs e = new NotificarParaUsuarioEmpregadorREPDiferenteEventArgs(empregadorREPDiferente, this._rep.RepId, this._rep.Local);
      if (this.OnNotificarParaUsuarioEmpregadorREPDiferente == null)
        return;
      this.OnNotificarParaUsuarioEmpregadorREPDiferente((object) this, e);
    }

    public override void IniciarProcesso()
    {
      this.NotificarParaUsuario(Resources.msgENVIANDO_CONFIGURACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
      this.Conectar(this._rep);
    }

    private void EnviarSolicitacaoDadosEmpregador()
    {
      this._estadoRep = GerenciadorConfigEmpregadorRepPlus.Estados.estSolicitaDadosEmpregador;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaDadosEmpregadorRepPlus()
      }, true);
    }

    public bool ValidaCaracteresInvalidos(string caracter, bool autenticacao)
    {
      int utf32 = char.ConvertToUtf32(caracter, 0);
      return (utf32 >= 44 && (utf32 <= 57 || utf32 >= 65) && (utf32 <= 90 || utf32 >= 97) && (utf32 <= 122 || utf32 >= 192) && (utf32 <= 207 || utf32 >= 210) && (utf32 <= 132 || utf32 >= 136) && (utf32 <= 136 || utf32 >= 143) && (utf32 <= 161 || utf32 >= 192) && (utf32 <= 151 || utf32 >= 160) && (utf32 <= 220 || utf32 >= 224) && utf32 != 241 && utf32 != 253 && utf32 != (int) byte.MaxValue && utf32 != 248 && utf32 != 246 && utf32 != 247 && utf32 != 230 && utf32 != 198 || utf32 == 32 || utf32 == 8) && utf32 <= 300 || autenticacao && utf32 == 42;
    }

    private string ValidaParametrosEmpregador(
      string razaoSocial,
      string cnpj_cpf,
      string cei,
      int tipoIdentificador,
      string local)
    {
      ulong result = 0;
      try
      {
        string str1 = razaoSocial.Trim();
        if (str1 == string.Empty)
          return "Razão social não pode ser vazio.";
        if (str1.Length > 150)
          return "Razão social com mais de 150 caracteres.";
        string s1 = cei.Trim();
        bool flag1 = ulong.TryParse(s1, out result);
        if (s1.Length > 12)
          return "CEI com mais de 12 dígitos";
        if (!flag1)
          return "CEI inválido";
        for (int startIndex = 0; startIndex < local.Length; ++startIndex)
        {
          if (!this.ValidaCaracteresInvalidos(local.Substring(startIndex, 1), false))
            return "Carácter inválido no local do rep!";
        }
        for (int startIndex = 0; startIndex < razaoSocial.Length; ++startIndex)
        {
          if (!this.ValidaCaracteresInvalidos(razaoSocial.Substring(startIndex, 1), false))
            return "Carácter inválido na Razão Social!";
        }
        switch (tipoIdentificador)
        {
          case 1:
            string str2 = cnpj_cpf.Trim();
            bool flag2 = ulong.TryParse(str2, out result);
            if (str2.Length >= 18)
              return "CNPJ não pode ser maior que 17 dígitos";
            try
            {
              if (!GerenciadorConfigEmpregadorRepPlus.ValidaCNPJ(str2))
                return "CNPJ Inválido";
            }
            catch
            {
              return "CNPJ Inválido";
            }
            if (flag2 && !(str2 == "00000000000000"))
            {
              if (!(str2 == ""))
                break;
            }
            return "CNPJ Inválido";
          case 2:
            string s2 = cnpj_cpf.Trim();
            bool flag3 = ulong.TryParse(s2, out result);
            if (s2.Length > 18)
              return "CPF não pode ser maior que 18 dígitos";
            try
            {
              if (!GerenciadorConfigEmpregadorRepPlus.ValidaCPF(s2.Length > 11 ? s2.Substring(3) : s2))
                return "CPF Inválido";
            }
            catch
            {
              return "CPF Inválido";
            }
            if (flag3 && !(s2 == ""))
            {
              if (!(s2 == "00000000000000"))
                break;
            }
            return "CPF Inválido";
          default:
            return "Erro do tipo de identificador";
        }
      }
      catch
      {
        return "Erro de conversão de banco de dados";
      }
      return "";
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

    private void EnviaDadosEmpregador()
    {
      try
      {
        this._estadoRep = GerenciadorConfigEmpregadorRepPlus.Estados.estEnviaEmpregador;
        string message = !this._empregadorNoCadastro.isCnpj ? this.ValidaParametrosEmpregador(this._empregadorNoCadastro.RazaoSocial, this._empregadorNoCadastro.Cpf, this._empregadorNoCadastro.Cei, 2, this._empregadorNoCadastro.Local) : this.ValidaParametrosEmpregador(this._empregadorNoCadastro.RazaoSocial, this._empregadorNoCadastro.Cnpj, this._empregadorNoCadastro.Cei, 1, this._empregadorNoCadastro.Local);
        if (message != "")
          throw new Exception(message);
        MsgTCPAplicacaoRegistroEmpregadorRepPlus empregadorRepPlus1 = new MsgTCPAplicacaoRegistroEmpregadorRepPlus();
        MsgTCPAplicacaoRegistroEmpregadorRepPlus empregadorRepPlus2 = !this._liberacaoAt ? new MsgTCPAplicacaoRegistroEmpregadorRepPlus(this._versao1_1, this._empregadorNoCadastro.isCnpj, this._empregadorNoCadastro.RazaoSocial.ToUpper(), this._empregadorNoCadastro.Cpf, this._empregadorNoCadastro.Cnpj, this._empregadorNoCadastro.Cei, this._empregadorNoCadastro.Local.ToUpper()) : new MsgTCPAplicacaoRegistroEmpregadorRepPlus(this._versao1_1, this._empregadorNoCadastro.isCnpj, this._empregadorNoCadastro.RazaoSocial, this._empregadorNoCadastro.Cpf, this._empregadorNoCadastro.Cnpj, this._empregadorNoCadastro.Cei, this._empregadorNoCadastro.Local);
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) empregadorRepPlus2
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        this.NotificarParaUsuario(ex.Message, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      }
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      switch (this._estadoRep)
      {
        case GerenciadorConfigEmpregadorRepPlus.Estados.estSolicitaDadosEmpregador:
          if (envelope.Grp != (byte) 11 || envelope.Cmd != (byte) 102)
            break;
          this.RecuperaEmpregadorDoREP(envelope);
          this.ProcessarDadosEmpregador();
          break;
        case GerenciadorConfigEmpregadorRepPlus.Estados.estEnviaEmpregador:
          if (envelope.Grp != (byte) 11 || envelope.Cmd != (byte) 101)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0 || envelope.MsgAplicacaoEmBytes[2] == (byte) 2)
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_EMPREGADOR_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_EMPREGADOR_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarSolicitacaoDadosEmpregador();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigEmpregadorRepPlus.Estados.estSolicitaDadosEmpregador:
          menssagem = Resources.msgTIMEOUT_SOLICIT_DADOS_EMPREGADOR;
          break;
        case GerenciadorConfigEmpregadorRepPlus.Estados.estProcessaDadosEmpregador:
          menssagem = Resources.msgTIMEOUT_PROCESSA_DADOS_EMPREGADOR;
          break;
        case GerenciadorConfigEmpregadorRepPlus.Estados.estEnviaEmpregador:
          menssagem = Resources.msgTIMEOUT_ENVIA_DADOS_EMPREGADOR;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigEmpregadorRepPlus.Estados.estSolicitaDadosEmpregador:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_DADOS_EMPREGADOR;
          break;
        case GerenciadorConfigEmpregadorRepPlus.Estados.estProcessaDadosEmpregador:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_PROCESSAMENTO_DE_EMPREGADOR;
          break;
        case GerenciadorConfigEmpregadorRepPlus.Estados.estEnviaEmpregador:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_EMPREGADOR;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estSolicitaDadosEmpregador,
      estProcessaDadosEmpregador,
      estEnviaEmpregador,
    }
  }
}
