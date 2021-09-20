// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigSenhasREPRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigSenhasREPRepPlus : TarefaAbstrata
  {
    private Relogio _entRelogio;
    private FormatoCartao _formatoEntBarras;
    private FormatoCartao _formatoEntProx;
    private RepBase _rep;
    private bool _chamadaPeloSdk;
    private GerenciadorConfigSenhasREPRepPlus.Estados _estadoRep;
    public static GerenciadorConfigSenhasREPRepPlus _gerenciadorConfigSenhasREPRepPlus;

    public static GerenciadorConfigSenhasREPRepPlus getInstance() => GerenciadorConfigSenhasREPRepPlus._gerenciadorConfigSenhasREPRepPlus != null ? GerenciadorConfigSenhasREPRepPlus._gerenciadorConfigSenhasREPRepPlus : new GerenciadorConfigSenhasREPRepPlus();

    public static GerenciadorConfigSenhasREPRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigSenhasREPRepPlus._gerenciadorConfigSenhasREPRepPlus != null ? GerenciadorConfigSenhasREPRepPlus._gerenciadorConfigSenhasREPRepPlus : new GerenciadorConfigSenhasREPRepPlus(rep);
    }

    public static GerenciadorConfigSenhasREPRepPlus getInstance(
      RepBase rep,
      bool chamadaSDK)
    {
      return GerenciadorConfigSenhasREPRepPlus._gerenciadorConfigSenhasREPRepPlus != null ? GerenciadorConfigSenhasREPRepPlus._gerenciadorConfigSenhasREPRepPlus : new GerenciadorConfigSenhasREPRepPlus(rep, chamadaSDK);
    }

    public GerenciadorConfigSenhasREPRepPlus()
    {
    }

    public GerenciadorConfigSenhasREPRepPlus(RepBase rep)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
    }

    public GerenciadorConfigSenhasREPRepPlus(RepBase rep, bool chamadaSDK)
    {
      this._rep = rep;
      this._chamadaPeloSdk = chamadaSDK;
    }

    public override void IniciarProcesso()
    {
      this.NotificarParaUsuario(Resources.msgENVIANDO_CONFIGURACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
      this.Conectar(this._rep);
    }

    private void EnviarConfiguracoesSenhas()
    {
      this._estadoRep = GerenciadorConfigSenhasREPRepPlus.Estados.estEnvioConfiguracoesSenhas;
      try
      {
        string menssagem = this.ValidaParametrosInnerRep(this._rep.SenhaComunic, this._rep.SenhaBio, this._rep.SenhaRelogio, this._rep.TipoTerminalId);
        if (menssagem != "")
          this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        try
        {
          this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
          {
            MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoConfigSenhasREP(this._rep.SenhaComunic, this._rep.SenhaBio, this._rep.SenhaRelogio)
          }, true);
        }
        catch
        {
        }
      }
      catch (Exception ex)
      {
        this.NotificarParaUsuario(ex.Message.ToString(), EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      }
    }

    private string ValidaParametrosInnerRep(
      string senhaConfig,
      string senhaBio,
      string senhaRelogio,
      int TerminalId)
    {
      ulong result = 0;
      try
      {
        string s1 = senhaConfig.Trim();
        bool flag1 = ulong.TryParse(s1, out result);
        if (s1.Length != 6 && s1 != string.Empty)
          return "Campo senha da comunicação requer 6 dígitos obrigatoriamente";
        if (!flag1 && s1 != string.Empty)
          return "Campo senha da comunicação somente pode ser numérico";
        if (s1 == string.Empty)
          return "Campo senha da configuração não pode ser vazio";
        string s2 = senhaRelogio.Trim();
        bool flag2 = ulong.TryParse(s2, out result);
        if (s2.Length != 6 && s2 != string.Empty)
          return "Campo senha do pendrive requer 6 dígitos obrigatoriamente";
        if (!flag2 && s2 != string.Empty)
          return "Campo senha do pendrive somente pode ser numérico";
        if (s2 == string.Empty)
          return "Campo senha do pendrive não pode ser vazio";
        if (TerminalId == 13 || TerminalId == 16)
        {
          string s3 = senhaBio.Trim();
          if (!ulong.TryParse(s3, out result) && s3 != string.Empty)
            return "Campo senha da biometria somente pode ser numérico";
          if (s3.Length != 6 && s3 != string.Empty)
            return "Campo senha da biometria requer 6 dígitos obrigatoriamente";
          if (s3 == string.Empty)
            return "Campo senha da biometria não pode ser vazio";
        }
        return "";
      }
      catch
      {
        return "Erro de conversão no banco de dados";
      }
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorConfigSenhasREPRepPlus.Estados.estEnvioConfiguracoesSenhas || envelope.Grp != (byte) 10 || envelope.Cmd != (byte) 107)
        return;
      if (!this._chamadaPeloSdk)
      {
        if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
        }
        else
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        }
      }
      else
      {
        this.EncerrarConexao();
        this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarConfiguracoesSenhas();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigSenhasREPRepPlus.Estados.estEnvioConfiguracoesSenhas)
        menssagem = Resources.msgTIMEOUT_ENVIO_CONFIG_GERAL;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigSenhasREPRepPlus.Estados.estEnvioConfiguracoesSenhas)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioConfiguracoesSenhas,
    }
  }
}
