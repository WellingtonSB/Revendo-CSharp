// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigGeraisREP
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
  public class GerenciadorConfigGeraisREP : TarefaAbstrata
  {
    private ArquivoCFGEntity _arquivoCFGEntSDK = new ArquivoCFGEntity();
    private bool _enviarSomenteCFGTLM;
    private Relogio _entRelogio;
    private FormatoCartao _formatoEnt;
    private RepBase _rep;
    private bool chamadaPeloSdk;
    private bool chamadaPelaSenior;
    private GerenciadorConfigGeraisREP.Estados _estadoRep;
    public static GerenciadorConfigGeraisREP _gerenciadorConfigGeraisREP;

    public bool ChamadaPelaSenior
    {
      get => this.chamadaPelaSenior;
      set => this.chamadaPelaSenior = value;
    }

    public static GerenciadorConfigGeraisREP getInstance() => GerenciadorConfigGeraisREP._gerenciadorConfigGeraisREP != null ? GerenciadorConfigGeraisREP._gerenciadorConfigGeraisREP : new GerenciadorConfigGeraisREP();

    public static GerenciadorConfigGeraisREP getInstance(RepBase rep) => GerenciadorConfigGeraisREP._gerenciadorConfigGeraisREP != null ? GerenciadorConfigGeraisREP._gerenciadorConfigGeraisREP : new GerenciadorConfigGeraisREP(rep);

    public static GerenciadorConfigGeraisREP getInstance(
      RepBase rep,
      bool senior)
    {
      return GerenciadorConfigGeraisREP._gerenciadorConfigGeraisREP != null ? GerenciadorConfigGeraisREP._gerenciadorConfigGeraisREP : new GerenciadorConfigGeraisREP(rep, senior);
    }

    public static GerenciadorConfigGeraisREP getInstance(
      RepBase rep,
      Relogio entRelogio,
      FormatoCartao formatoCartao)
    {
      return GerenciadorConfigGeraisREP._gerenciadorConfigGeraisREP != null ? GerenciadorConfigGeraisREP._gerenciadorConfigGeraisREP : new GerenciadorConfigGeraisREP(rep, entRelogio, formatoCartao);
    }

    public static GerenciadorConfigGeraisREP getInstance(
      RepBase rep,
      Relogio entRelogio,
      FormatoCartao formatoCartao,
      ArquivoCFGEntity arquivoCFG)
    {
      return GerenciadorConfigGeraisREP._gerenciadorConfigGeraisREP != null ? GerenciadorConfigGeraisREP._gerenciadorConfigGeraisREP : new GerenciadorConfigGeraisREP(rep, entRelogio, formatoCartao, arquivoCFG);
    }

    public static GerenciadorConfigGeraisREP getInstance(
      RepBase rep,
      Relogio entRelogio,
      FormatoCartao formatoCartao,
      ArquivoCFGEntity arquivoCFG,
      bool enviarSomenteCFGTLM)
    {
      return GerenciadorConfigGeraisREP._gerenciadorConfigGeraisREP != null ? GerenciadorConfigGeraisREP._gerenciadorConfigGeraisREP : new GerenciadorConfigGeraisREP(rep, entRelogio, formatoCartao, arquivoCFG, enviarSomenteCFGTLM);
    }

    public GerenciadorConfigGeraisREP() => this._enviarSomenteCFGTLM = false;

    public GerenciadorConfigGeraisREP(RepBase rep)
    {
      this._rep = rep;
      this.chamadaPeloSdk = false;
      this._enviarSomenteCFGTLM = false;
    }

    public GerenciadorConfigGeraisREP(RepBase rep, bool Senior)
    {
      this._rep = rep;
      this.chamadaPeloSdk = false;
      this.chamadaPelaSenior = Senior;
      this._enviarSomenteCFGTLM = false;
    }

    public GerenciadorConfigGeraisREP(RepBase rep, Relogio entRelogio, FormatoCartao formatoCartao)
    {
      this._rep = rep;
      this._entRelogio = entRelogio;
      this._formatoEnt = formatoCartao;
      this.chamadaPeloSdk = true;
      this._enviarSomenteCFGTLM = false;
    }

    public GerenciadorConfigGeraisREP(
      RepBase rep,
      Relogio entRelogio,
      FormatoCartao formatoCartao,
      ArquivoCFGEntity arquivoCFG)
    {
      this._rep = rep;
      this._entRelogio = entRelogio;
      this._formatoEnt = formatoCartao;
      this.chamadaPeloSdk = true;
      this._arquivoCFGEntSDK = arquivoCFG;
      this._enviarSomenteCFGTLM = false;
    }

    public GerenciadorConfigGeraisREP(
      RepBase rep,
      Relogio entRelogio,
      FormatoCartao formatoCartao,
      ArquivoCFGEntity arquivoCFG,
      bool enviarSomenteCFGTLM)
    {
      this._rep = rep;
      this._entRelogio = entRelogio;
      this._formatoEnt = formatoCartao;
      this.chamadaPeloSdk = true;
      this._arquivoCFGEntSDK = arquivoCFG;
      this._enviarSomenteCFGTLM = enviarSomenteCFGTLM;
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarConfiguracoesGerais()
    {
      this._estadoRep = GerenciadorConfigGeraisREP.Estados.estEnvioConfiguracoes;
      if (!this.chamadaPeloSdk)
      {
        Relogio relogio = new Relogio();
        this._entRelogio = !this.chamadaPelaSenior ? relogio.PesquisarHorVeraoMulti() : relogio.PesquisarHorVerao(this._rep.RepId);
      }
      try
      {
        string message = this.ValidaParametrosInnerRep(this._rep.Local, this._rep.SenhaComunic, this._rep.SenhaRelogio, this._rep.SenhaBio, this._rep.TipoTerminalId);
        if (message != "")
          throw new Exception(message);
        try
        {
          Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
          if (!this.chamadaPeloSdk)
            this._formatoEnt = FormatoCartao.PesquisarFormatoCartaoByRepId(this._rep.RepId);
          MsgTCPAplicacaoConfigGeralREP aplicacaoConfigGeralRep = new MsgTCPAplicacaoConfigGeralREP(this._entRelogio.InicioHorVerao, this._entRelogio.FimHorVerao, this._rep.SenhaRelogio, this._rep.SenhaBio, this._rep.SenhaComunic, this._formatoEnt.formatoCartao);
          envelope.MsgAplicacao = (MsgTcpAplicacaoBase) aplicacaoConfigGeralRep;
          this.ClienteSocket.Enviar(envelope, true);
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
      string local,
      string senhaComunic,
      string senhaRelogio,
      string senhaBio,
      int TerminalId)
    {
      ulong result = 0;
      try
      {
        string str = local.Trim();
        if (str.Length > 100)
          return "Campo local maior que 100 dígitos";
        if (str == string.Empty)
          return "Campo local não pode ser vazio.";
        string s1 = senhaComunic.Trim();
        bool flag1 = ulong.TryParse(s1, out result);
        if (s1.Length != 6 && s1 != string.Empty)
          return "Campo senha da comunicação requer 6 dígitos obrigatoriamente";
        if (!flag1 && s1 != string.Empty)
          return "Campo senha da comunicação somente pode ser numérico";
        if (s1 == string.Empty)
          return "Campo senha da comunicação não pode ser vazio";
        string s2 = senhaRelogio.Trim();
        bool flag2 = ulong.TryParse(s2, out result);
        if (s2.Length != 6 && s2 != string.Empty)
          return "Campo senha do relógio requer 6 dígitos obrigatoriamente";
        if (!flag2 && s2 != string.Empty)
          return "Campo senha do relógio somente pode ser numérico";
        if (s2 == string.Empty)
          return "Campo senha do relógio não pode ser vazio";
        if (TerminalId == 1 || TerminalId == 6 || TerminalId == 7)
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

    private void EnviarBufferCFGTlm()
    {
      this._estadoRep = GerenciadorConfigGeraisREP.Estados.estEnvioBufferCFGTlm;
      ArquivoCFGBI arquivoCfgbi = new ArquivoCFGBI();
      ArquivoCFGEntity arquivoCfgEntity = this.chamadaPeloSdk ? this._arquivoCFGEntSDK : arquivoCfgbi.PesquisarArquivoCFGPorRepID(this._rep.RepId);
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoBufferCFGTlm(PadraoTLM.GetByteArray(arquivoCfgEntity.CFG))
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      switch (this._estadoRep)
      {
        case GerenciadorConfigGeraisREP.Estados.estEnvioConfiguracoes:
          if (envelope.Grp != (byte) 3 || envelope.Cmd != (byte) 0)
            break;
          if ((this.chamadaPeloSdk ? this._formatoEnt : FormatoCartao.PesquisarFormatoCartaoByRepId(this._rep.RepId)).formatoCartaoID == 6)
          {
            if (!this.chamadaPeloSdk)
            {
              this.EnviarBufferCFGTlm();
              break;
            }
            if (this._arquivoCFGEntSDK.CFG.Equals(""))
            {
              this.EncerrarConexao();
              this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            this.EnviarBufferCFGTlm();
            break;
          }
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
          break;
        case GerenciadorConfigGeraisREP.Estados.estEnvioBufferCFGTlm:
          if (envelope.Grp != (byte) 3 || envelope.MsgAplicacaoEmBytes[2] != (byte) 3)
            break;
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
          break;
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        this.AtualizarSerial();
        if (!this._enviarSomenteCFGTLM)
          this.EnviarConfiguracoesGerais();
        else
          this.EnviarBufferCFGTlm();
      }
      else
        this.EncerrarConexao();
    }

    private void AtualizarSerial()
    {
      if (!RegistrySingleton.GetInstance().VALIDAR_SERIAL)
        return;
      new RepAFD().AtualizarSerialREP(new RepAFD()
      {
        repID = this._rep.RepId
      }, this._rep.Serial);
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigGeraisREP.Estados.estEnvioConfiguracoes)
        menssagem = Resources.msgTIMEOUT_ENVIO_CONFIG_GERAL;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigGeraisREP.Estados.estEnvioConfiguracoes:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
          break;
        case GerenciadorConfigGeraisREP.Estados.estEnvioBufferCFGTlm:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioConfiguracoes,
      estEnvioBufferCFGTlm,
      estFinal,
    }
  }
}
