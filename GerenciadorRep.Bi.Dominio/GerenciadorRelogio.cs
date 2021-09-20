// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorRelogio
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.GerenciadorRep.Bi.Dominio.Properties;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorRelogio : TarefaAbstrata
  {
    private DateTime dataHoraSenior;
    private bool chamadaSenior;
    private RepBase _rep;
    private int _incrementoHora;
    private GerenciadorRelogio.Estados _estadoRep;
    public static GerenciadorRelogio _gerenciadorRelogio;

    public bool ChamadaSenior
    {
      get => this.chamadaSenior;
      set => this.chamadaSenior = value;
    }

    public static GerenciadorRelogio getInstance() => GerenciadorRelogio._gerenciadorRelogio != null ? GerenciadorRelogio._gerenciadorRelogio : new GerenciadorRelogio();

    public static GerenciadorRelogio getInstance(RepBase rep) => GerenciadorRelogio._gerenciadorRelogio != null ? GerenciadorRelogio._gerenciadorRelogio : new GerenciadorRelogio(rep);

    public static GerenciadorRelogio getInstance(
      RepBase rep,
      DateTime dataHoraSenior,
      bool chamadaSenior)
    {
      return GerenciadorRelogio._gerenciadorRelogio != null ? GerenciadorRelogio._gerenciadorRelogio : new GerenciadorRelogio(rep, dataHoraSenior, chamadaSenior);
    }

    public static GerenciadorRelogio getInstance(RepBase rep, int incrementoHora) => GerenciadorRelogio._gerenciadorRelogio != null ? GerenciadorRelogio._gerenciadorRelogio : new GerenciadorRelogio(rep, incrementoHora);

    public GerenciadorRelogio() => this._incrementoHora = 0;

    public GerenciadorRelogio(RepBase rep, DateTime dataHoraSenior, bool chamadaSenior)
    {
      this._rep = rep;
      this.dataHoraSenior = dataHoraSenior;
      this.chamadaSenior = chamadaSenior;
    }

    public GerenciadorRelogio(RepBase rep)
    {
      this._rep = rep;
      this._incrementoHora = 0;
    }

    public GerenciadorRelogio(RepBase rep, int incrementoHora)
    {
      this._rep = rep;
      this._incrementoHora = incrementoHora;
    }

    ~GerenciadorRelogio()
    {
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarRelogio()
    {
      try
      {
        this._estadoRep = GerenciadorRelogio.Estados.estEnvioRelogio;
        DateTime.Now.AddHours((double) this._incrementoHora);
        DateTime dateTime = !this.chamadaSenior ? DateTime.Now.AddHours((double) this._incrementoHora) : this.dataHoraSenior;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        MsgTcpAplicacaoRelogio aplicacaoRelogio = new MsgTcpAplicacaoRelogio();
        aplicacaoRelogio.Dia = Convert.ToByte(dateTime.Day);
        aplicacaoRelogio.Mes = Convert.ToByte(dateTime.Month);
        dateTime.Year.ToString();
        aplicacaoRelogio.Ano = (byte) (DateTime.Now.Year % 100);
        aplicacaoRelogio.Hora = Convert.ToByte(dateTime.Hour);
        aplicacaoRelogio.Minuto = Convert.ToByte(dateTime.Minute);
        aplicacaoRelogio.Segundo = Convert.ToByte(dateTime.Second);
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) aplicacaoRelogio;
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorRelogio.Estados.estEnvioRelogio || envelope.Grp != (byte) 3 || envelope.Cmd != (byte) 0 || envelope.MsgAplicacaoEmBytes[2] != (byte) 2)
        return;
      this.NotificarParaUsuario(Resources.msgRELOGIO_ATUALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
      this.EncerrarConexao();
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorRelogio.Estados.estEnvioRelogio)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_RELOGIO;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorRelogio.Estados.estEnvioRelogio)
        menssagem = Resources.msgTIMEOUT_ENVIO_RELOGIO;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarRelogio();
      else
        this.EncerrarConexao();
    }

    private new enum Estados
    {
      estEnvioRelogio,
    }
  }
}
