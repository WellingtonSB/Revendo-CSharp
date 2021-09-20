// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigCPFResponsavel
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp;
using TopData.GerenciadorRep.Bi.Dominio.Properties;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigCPFResponsavel : TarefaAbstrata
  {
    private RepBase _rep;
    private GerenciadorConfigCPFResponsavel.Estados _estadoRep;
    public static GerenciadorConfigCPFResponsavel _gerenciadorConfigCPFResponsavel;

    public static GerenciadorConfigCPFResponsavel getInstance() => GerenciadorConfigCPFResponsavel._gerenciadorConfigCPFResponsavel != null ? GerenciadorConfigCPFResponsavel._gerenciadorConfigCPFResponsavel : new GerenciadorConfigCPFResponsavel();

    public static GerenciadorConfigCPFResponsavel getInstance(
      RepBase rep)
    {
      return GerenciadorConfigCPFResponsavel._gerenciadorConfigCPFResponsavel != null ? GerenciadorConfigCPFResponsavel._gerenciadorConfigCPFResponsavel : new GerenciadorConfigCPFResponsavel(rep);
    }

    public GerenciadorConfigCPFResponsavel()
    {
    }

    public GerenciadorConfigCPFResponsavel(RepBase rep) => this._rep = rep;

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarCPFResponsavel()
    {
      this._estadoRep = GerenciadorConfigCPFResponsavel.Estados.estEnviaCPFResponsavel;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoCPFResponsavel(this._rep.CpfResponsavel)
      }, true);
    }

    private string ValidaParametrosCPFResponsavel(string cpfResponsavel)
    {
      ulong result = 0;
      try
      {
        string s = cpfResponsavel.Trim();
        bool flag = ulong.TryParse(s, out result);
        if (s.Length > 18)
          return "CPF não pode ser maior que 18 dígitos";
        try
        {
          if (!GerenciadorConfigCPFResponsavel.ValidaCPF(s.Substring(3)))
            return "CPF Inválido";
        }
        catch
        {
          return "CPF Inválido";
        }
        if (flag && !(s == ""))
        {
          if (!(s == "00000000000000"))
            goto label_10;
        }
        return "CPF Inválido";
      }
      catch
      {
        return "Erro de conversão de banco de dados";
      }
label_10:
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

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorConfigCPFResponsavel.Estados.estEnviaCPFResponsavel || envelope.Grp != (byte) 10 || envelope.Cmd != (byte) 105)
        return;
      this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CPF_RESPONSAVEL_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarCPFResponsavel();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigCPFResponsavel.Estados.estEnviaCPFResponsavel)
        menssagem = Resources.msgTIMEOUT_ENVIO_CPF_RESPONSAVEL;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigCPFResponsavel.Estados.estEnviaCPFResponsavel)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_CPF_RESPONSAVEL;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnviaCPFResponsavel,
    }
  }
}
