// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.RepBase
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Net;
using TopData.Framework.Comunicacao;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class RepBase
  {
    private List<TopData.GerenciadorRep.Bi.Dominio.TarefaAbstrata> _tarefaAbstrata;
    private bool _envioConfigAvancadas;
    private long _timeoutExtra;
    private ClienteAssincrono _clienteSocket;
    private string _ipAddress = "";
    private int _port;
    private ushort _numTerminal;
    private string _host = "";
    private int _tipoConexao;
    private int _tempoEsperaConexao = 20000;
    private string _DNS = "";
    private int _tipoConexaoDNS;
    private string _nomeRep = "";
    private Marcacao _marcacao;
    private TipoTerminal _tipoTerminal;
    private CartaoEmpregado _cartaoEmpregado;
    private Relogio _relogio;
    private Empregado _empregado;
    private Empregador _empregador;
    private EnvioREP _EnvioRep;
    private ArquivoBilhete _arquivoBilhete;
    private int _tipoTerminalId;
    private string _chaveComunicacao;
    private string _local;
    private string _desc;
    private string _senhaComunic;
    private string _senhaRelogio;
    private string _senhaBio;
    private int _configuracaoId;
    private int _repId;
    private int _repIdSenior;
    private int _repIdLeitoraSenior;
    private byte _produto = 1;
    private bool _multiRep;
    private string _serial;
    private int _grupoId;
    private string _CpfResponsavel;
    private string _ConfiguracaoLeitorCpf;
    private DateTime _ConfiguracaoLeitorDataHora;
    private bool _sincronizado;
    private int _envioNSR;
    private short _gmt;
    private bool _repClient;
    private bool _redeRemota;
    private string _ipServidor;
    private string _mascaraRede;
    private string _gateway;
    private int _portaServidor;
    private int _tempoEspera;
    private int _intervaloConexao;
    private bool _portaVariavel;
    private string _nomeServidor;
    private byte _status;
    private int _modeloFim;
    private int _versaoFW;
    private string _versaoFWCompleto;

    public override string ToString() => this.Desc + " Local: " + this.Local;

    public int Tentativas { get; set; }

    public List<TopData.GerenciadorRep.Bi.Dominio.TarefaAbstrata> TarefaAbstrata
    {
      get => this._tarefaAbstrata;
      set => this._tarefaAbstrata = value;
    }

    public bool EnvioConfigAvancadas
    {
      get => this._envioConfigAvancadas;
      set => this._envioConfigAvancadas = value;
    }

    public long TimeoutExtra
    {
      get => this._timeoutExtra;
      set => this._timeoutExtra = value;
    }

    public int TempoEsperaConexao
    {
      get => this._tempoEsperaConexao;
      set => this._tempoEsperaConexao = value;
    }

    public bool Sincronizado
    {
      get => this._sincronizado;
      set => this._sincronizado = value;
    }

    public int EnvioNSR
    {
      get => this._envioNSR;
      set => this._envioNSR = value;
    }

    public int TecnologiaProx { get; set; }

    public string ConfiguracaoLeitorCpf
    {
      get => this._ConfiguracaoLeitorCpf;
      set => this._ConfiguracaoLeitorCpf = value;
    }

    public DateTime ConfiguracaoLeitorDataHora
    {
      get => this._ConfiguracaoLeitorDataHora;
      set => this._ConfiguracaoLeitorDataHora = value;
    }

    public short Gmt
    {
      get => this._gmt;
      set => this._gmt = value;
    }

    public byte TecnologiaBio { get; set; }

    public RepBase()
    {
    }

    public RepBase(string ipAddress, int port, ushort numTerminal)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._tipoTerminal = new TipoTerminal();
      this._relogio = new Relogio();
      this._empregado = new Empregado();
      this._empregador = new Empregador();
      this._EnvioRep = new EnvioREP();
      this._cartaoEmpregado = new CartaoEmpregado();
      this._arquivoBilhete = new ArquivoBilhete();
      this._marcacao = new Marcacao(this);
      this.InicializarSocket();
    }

    public RepBase(
      string ipAddress,
      int port,
      ushort numTerminal,
      string chaveComunicacao,
      bool repClient,
      bool portaVariavel,
      string ipServidor,
      int portaServidor,
      string mask,
      string gateway,
      int intervaloConexao,
      int tempoespera)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._chaveComunicacao = chaveComunicacao;
      this._tipoTerminal = new TipoTerminal();
      this._repClient = repClient;
      this._portaVariavel = portaVariavel;
      this._ipServidor = ipServidor;
      this._portaServidor = portaServidor;
      this._mascaraRede = mask;
      this._gateway = gateway;
      this._intervaloConexao = intervaloConexao;
      this._tempoEspera = tempoespera;
      this.InicializarSocket();
    }

    public RepBase(
      string ipAddress,
      int port,
      ushort numTerminal,
      string chaveComunicacao,
      bool repClient,
      bool portaVariavel,
      string ipServidor,
      int portaServidor,
      string mask,
      string gateway,
      int intervaloConexao,
      int tempoespera,
      string cpfResponsavel)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._chaveComunicacao = chaveComunicacao;
      this._tipoTerminal = new TipoTerminal();
      this._repClient = repClient;
      this._portaVariavel = portaVariavel;
      this._ipServidor = ipServidor;
      this._portaServidor = portaServidor;
      this._mascaraRede = mask;
      this._gateway = gateway;
      this._intervaloConexao = intervaloConexao;
      this._tempoEspera = tempoespera;
      this._CpfResponsavel = cpfResponsavel;
      this.InicializarSocket();
    }

    public RepBase(
      string ipAddress,
      int port,
      ushort numTerminal,
      string chaveComunicacao,
      bool repClient,
      bool portaVariavel,
      string ipServidor,
      int portaServidor,
      string mask,
      string gateway,
      int intervaloConexao,
      int tempoespera,
      string cpfResponsavel,
      bool DNSHabilitado,
      string hostDNS,
      string DNS,
      string nomeRep)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._chaveComunicacao = chaveComunicacao;
      this._tipoTerminal = new TipoTerminal();
      this._repClient = repClient;
      this._portaVariavel = portaVariavel;
      this._ipServidor = ipServidor;
      this._portaServidor = portaServidor;
      this._mascaraRede = mask;
      this._gateway = gateway;
      this._intervaloConexao = intervaloConexao;
      this._tempoEspera = tempoespera;
      this._CpfResponsavel = cpfResponsavel;
      this._nomeRep = nomeRep;
      this._nomeServidor = hostDNS;
      this._DNS = DNS;
      this._tipoConexaoDNS = DNSHabilitado ? 1 : 0;
      this.InicializarSocket();
    }

    public RepBase(
      string ipAddress,
      int port,
      ushort numTerminal,
      string chaveComunicacao,
      Empregador empregador,
      string local,
      string senhaRelogio,
      string senhaComunic,
      string senhaBio,
      bool repClient,
      int portaServidor,
      int tempoEspera,
      int tempoEsperaConexao)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._repClient = repClient;
      this._portaServidor = portaServidor;
      this._tempoEspera = tempoEspera;
      this._tempoEsperaConexao = tempoEsperaConexao;
      this._tipoTerminal = new TipoTerminal();
      this._chaveComunicacao = chaveComunicacao;
      this._senhaRelogio = senhaRelogio;
      this._senhaComunic = senhaComunic;
      this._senhaBio = senhaBio;
      this._relogio = new Relogio();
      this._empregado = new Empregado();
      this._empregador = empregador;
      this._EnvioRep = new EnvioREP();
      this.Local = local;
      this._cartaoEmpregado = new CartaoEmpregado();
      this._arquivoBilhete = new ArquivoBilhete();
      this._marcacao = new Marcacao(this);
      this.InicializarSocket();
    }

    public RepBase(
      string ipAddress,
      int port,
      ushort numTerminal,
      string chaveComunicacao,
      Empregador empregador,
      string local,
      string senhaRelogio,
      string senhaComunic,
      string senhaBio,
      bool repClient,
      int portaServidor,
      int tempoEspera,
      string cpf_responsavel)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._repClient = repClient;
      this._portaServidor = portaServidor;
      this._tempoEspera = tempoEspera;
      this._tipoTerminal = new TipoTerminal();
      this._chaveComunicacao = chaveComunicacao;
      this._senhaRelogio = senhaRelogio;
      this._senhaComunic = senhaComunic;
      this._senhaBio = senhaBio;
      this._relogio = new Relogio();
      this._empregado = new Empregado();
      this._empregador = empregador;
      this._EnvioRep = new EnvioREP();
      this.Local = local;
      this._cartaoEmpregado = new CartaoEmpregado();
      this._arquivoBilhete = new ArquivoBilhete();
      this._marcacao = new Marcacao(this);
      this._CpfResponsavel = cpf_responsavel;
      this.InicializarSocket();
    }

    public RepBase(
      string ipAddress,
      int port,
      ushort numTerminal,
      string chaveComunicacao,
      Empregador empregador,
      string local,
      string senhaRelogio,
      string senhaComunic,
      string senhaBio,
      bool repClient,
      int portaServidor,
      int tempoEspera,
      string cpf_responsavel,
      bool DNSHabilitado,
      string hostDNS,
      string DNS,
      string nomeRep,
      string host,
      int tipoConexao,
      int tipoTerminalId,
      int tempoEsperaConexao)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._repClient = repClient;
      this._portaServidor = portaServidor;
      this._tempoEspera = tempoEspera;
      this._tipoTerminal = new TipoTerminal();
      this._chaveComunicacao = chaveComunicacao;
      this._senhaRelogio = senhaRelogio;
      this._senhaComunic = senhaComunic;
      this._senhaBio = senhaBio;
      this._relogio = new Relogio();
      this._empregado = new Empregado();
      this._empregador = empregador;
      this._EnvioRep = new EnvioREP();
      this.Local = local;
      this._cartaoEmpregado = new CartaoEmpregado();
      this._arquivoBilhete = new ArquivoBilhete();
      this._marcacao = new Marcacao(this);
      this._CpfResponsavel = cpf_responsavel;
      this._nomeServidor = hostDNS;
      this._DNS = DNS;
      this._tipoConexaoDNS = DNSHabilitado ? 1 : 0;
      this._nomeRep = nomeRep;
      this._host = host;
      this._tipoConexao = tipoConexao;
      this._tipoTerminalId = tipoTerminalId;
      this._tempoEsperaConexao = tempoEsperaConexao;
      this.InicializarSocket();
    }

    public RepBase(
      string ipAddress,
      int port,
      ushort numTerminal,
      ushort tipoTerminalId,
      string chaveComunicacao,
      Empregador empregador,
      string local,
      string senhaRelogio,
      string senhaComunic,
      string senhaBio)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._tipoTerminal = this.TipoTerminal;
      this._chaveComunicacao = chaveComunicacao;
      this._senhaRelogio = senhaRelogio;
      this._senhaComunic = senhaComunic;
      this._senhaBio = senhaBio;
      this._relogio = new Relogio();
      this._empregado = new Empregado();
      this._empregador = empregador;
      this._EnvioRep = new EnvioREP();
      this.Local = local;
      this._cartaoEmpregado = new CartaoEmpregado();
      this._arquivoBilhete = new ArquivoBilhete();
      this._marcacao = new Marcacao(this);
      this._tipoTerminalId = (int) tipoTerminalId;
      this.InicializarSocket();
    }

    public RepBase(
      string ipAddress,
      int port,
      ushort numTerminal,
      ushort tipoTerminalId,
      string chaveComunicacao,
      Empregador empregador,
      string local,
      string senhaRelogio,
      string senhaComunic,
      string senhaBio,
      string host,
      int tipoConexao,
      string hostDNS,
      string DNS,
      int tipoConexaoDNS,
      string nomeRep)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._tipoTerminal = this.TipoTerminal;
      this._chaveComunicacao = chaveComunicacao;
      this._senhaRelogio = senhaRelogio;
      this._senhaComunic = senhaComunic;
      this._senhaBio = senhaBio;
      this._relogio = new Relogio();
      this._empregado = new Empregado();
      this._empregador = empregador;
      this._EnvioRep = new EnvioREP();
      this.Local = local;
      this._cartaoEmpregado = new CartaoEmpregado();
      this._arquivoBilhete = new ArquivoBilhete();
      this._marcacao = new Marcacao(this);
      this._tipoTerminalId = (int) tipoTerminalId;
      this._host = host;
      this._tipoConexao = tipoConexao;
      this._nomeServidor = hostDNS;
      this._DNS = DNS;
      this._tipoConexaoDNS = tipoConexaoDNS;
      this._nomeRep = nomeRep;
      this.InicializarSocket();
    }

    public RepBase(
      string ipAddress,
      int port,
      ushort numTerminal,
      ushort tipoTerminalId,
      string chaveComunicacao,
      Empregador empregador,
      string local,
      string descricao,
      string senhaRelogio,
      string senhaComunic,
      string senhaBio)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._tipoTerminal = this.TipoTerminal;
      this._chaveComunicacao = chaveComunicacao;
      this._senhaRelogio = senhaRelogio;
      this._senhaComunic = senhaComunic;
      this._senhaBio = senhaBio;
      this._relogio = new Relogio();
      this._empregado = new Empregado();
      this._empregador = empregador;
      this._EnvioRep = new EnvioREP();
      this.Local = local;
      this._desc = descricao;
      this._cartaoEmpregado = new CartaoEmpregado();
      this._arquivoBilhete = new ArquivoBilhete();
      this._marcacao = new Marcacao(this);
      this.InicializarSocket();
    }

    public RepBase(
      string ipAddress,
      int port,
      ushort numTerminal,
      ushort tipoTerminalId,
      string chaveComunicacao,
      Empregador empregador,
      string local,
      string descricao,
      string senhaRelogio,
      string senhaComunic,
      string senhaBio,
      string host,
      int tipoConexao,
      string hostDNS,
      string DNS,
      int tipoConexaoDNS,
      string nomeRep)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._tipoTerminal = this.TipoTerminal;
      this._chaveComunicacao = chaveComunicacao;
      this._senhaRelogio = senhaRelogio;
      this._senhaComunic = senhaComunic;
      this._senhaBio = senhaBio;
      this._relogio = new Relogio();
      this._empregado = new Empregado();
      this._empregador = empregador;
      this._EnvioRep = new EnvioREP();
      this.Local = local;
      this._desc = descricao;
      this._cartaoEmpregado = new CartaoEmpregado();
      this._arquivoBilhete = new ArquivoBilhete();
      this._marcacao = new Marcacao(this);
      this._host = host;
      this._tipoConexao = tipoConexao;
      this._nomeServidor = hostDNS;
      this._DNS = DNS;
      this._tipoConexaoDNS = tipoConexaoDNS;
      this._nomeRep = nomeRep;
      this.InicializarSocket();
    }

    public RepBase(string ipAddress, int port, ushort numTerminal, bool multiRep)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._multiRep = multiRep;
      this._tipoTerminal = new TipoTerminal();
      this._relogio = new Relogio();
      this._empregado = new Empregado();
      this._empregador = new Empregador();
      this._EnvioRep = new EnvioREP();
      this._cartaoEmpregado = new CartaoEmpregado();
      this._arquivoBilhete = new ArquivoBilhete();
      this._marcacao = new Marcacao(this);
      this.InicializarSocket();
    }

    public RepBase(
      string ipAddress,
      int port,
      ushort numTerminal,
      bool multiRep,
      string host,
      int tipoConexao,
      string hostDNS,
      string DNS,
      int tipoConexaoDNS,
      string nomeRep)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._multiRep = multiRep;
      this._tipoTerminal = new TipoTerminal();
      this._relogio = new Relogio();
      this._empregado = new Empregado();
      this._empregador = new Empregador();
      this._EnvioRep = new EnvioREP();
      this._cartaoEmpregado = new CartaoEmpregado();
      this._arquivoBilhete = new ArquivoBilhete();
      this._marcacao = new Marcacao(this);
      this._host = host;
      this._tipoConexao = tipoConexao;
      this._nomeServidor = hostDNS;
      this._DNS = DNS;
      this._tipoConexaoDNS = tipoConexaoDNS;
      this._nomeRep = nomeRep;
      this.InicializarSocket();
    }

    public int RepId
    {
      get => this._repId;
      set => this._repId = value;
    }

    public int RepIdSenior
    {
      get => this._repIdSenior;
      set => this._repIdSenior = value;
    }

    public int RepIdLeitoraSenior
    {
      get => this._repIdLeitoraSenior;
      set => this._repIdLeitoraSenior = value;
    }

    public int ConfiguracaoId
    {
      get => this._configuracaoId;
      set => this._configuracaoId = value;
    }

    public string Desc
    {
      get => this._desc;
      set => this._desc = value;
    }

    public string Local
    {
      get => this._local;
      set => this._local = value;
    }

    public string SenhaComunic
    {
      get => this._senhaComunic;
      set => this._senhaComunic = value;
    }

    public string SenhaRelogio
    {
      get => this._senhaRelogio;
      set => this._senhaRelogio = value;
    }

    public string SenhaBio
    {
      get => this._senhaBio;
      set => this._senhaBio = value;
    }

    public bool repClient
    {
      get => this._repClient;
      set => this._repClient = value;
    }

    public bool redeRemota
    {
      get => this._redeRemota;
      set => this._redeRemota = value;
    }

    public string ipServidor
    {
      get => this._ipServidor;
      set => this._ipServidor = value;
    }

    public string mascaraRede
    {
      get => this._mascaraRede;
      set => this._mascaraRede = value;
    }

    public string Gateway
    {
      get => this._gateway;
      set => this._gateway = value;
    }

    public int portaServidor
    {
      get => this._portaServidor;
      set => this._portaServidor = value;
    }

    public int tempoEspera
    {
      get => this._tempoEspera;
      set => this._tempoEspera = value;
    }

    public int intervaloConexao
    {
      get => this._intervaloConexao;
      set => this._intervaloConexao = value;
    }

    public bool PortaVariavel
    {
      get => this._portaVariavel;
      set => this._portaVariavel = value;
    }

    public string nomeServidor
    {
      get => this._nomeServidor;
      set => this._nomeServidor = value;
    }

    public string CpfResponsavel
    {
      get => this._CpfResponsavel;
      set => this._CpfResponsavel = value;
    }

    public ushort NumTerminal
    {
      get => this._numTerminal;
      set
      {
        this._numTerminal = value;
        this._clienteSocket.NumTerminal = this._numTerminal;
      }
    }

    public int Port
    {
      get => this._port;
      set
      {
        this._port = value;
        this._clienteSocket.Port = this._port;
      }
    }

    public string IpAddress
    {
      get => this._ipAddress;
      set
      {
        this._ipAddress = value;
        this._clienteSocket.Ip = IPAddress.Parse(this._ipAddress);
      }
    }

    public string DNS
    {
      get => this._DNS;
      set => this._DNS = value;
    }

    public int TipoConexaoDNS
    {
      get => this._tipoConexaoDNS;
      set => this._tipoConexaoDNS = value;
    }

    public string NomeRep
    {
      get => this._nomeRep;
      set => this._nomeRep = value;
    }

    public string Host
    {
      get => this._host;
      set
      {
        this._host = value;
        if (this._clienteSocket == null)
          return;
        this._clienteSocket.Host = this._host;
      }
    }

    public int TipoConexao
    {
      get => this._tipoConexao;
      set
      {
        this._tipoConexao = value;
        if (this._clienteSocket == null)
          return;
        this._clienteSocket.TipoConexao = this._tipoConexao;
      }
    }

    public string Serial
    {
      get => this._serial;
      set => this._serial = value;
    }

    public byte Status
    {
      get => this._status;
      set => this._status = value;
    }

    public int ModeloFim
    {
      get => this._modeloFim;
      set => this._modeloFim = value;
    }

    public int VersaoFW
    {
      get => this._versaoFW;
      set => this._versaoFW = value;
    }

    public int VersaoBaixaFW { get; set; }

    public string VersaoFWCompleto
    {
      get => this._versaoFWCompleto;
      set => this._versaoFWCompleto = value;
    }

    public string ChaveComunicacao
    {
      get => this._chaveComunicacao;
      set => this._chaveComunicacao = value;
    }

    public ClienteAssincrono ClienteSocket
    {
      get => this._clienteSocket;
      set => this._clienteSocket = value;
    }

    public Marcacao Marcacao
    {
      get => this._marcacao;
      set => this._marcacao = value;
    }

    public TipoTerminal TipoTerminal
    {
      get => this._tipoTerminal;
      set => this._tipoTerminal = value;
    }

    public ArquivoBilhete ArquivoBilhete
    {
      get => this._arquivoBilhete;
      set => this._arquivoBilhete = value;
    }

    public CartaoEmpregado CartaoEmpregado
    {
      get => this._cartaoEmpregado;
      set => this._cartaoEmpregado = value;
    }

    public Empregado Empregado
    {
      get => this._empregado;
      set => this._empregado = value;
    }

    public Empregador Empregador
    {
      get => this._empregador;
      set => this._empregador = value;
    }

    public EnvioREP EnvioRep
    {
      get => this._EnvioRep;
      set => this._EnvioRep = value;
    }

    public Relogio Relogio
    {
      get => this._relogio;
      set => this._relogio = value;
    }

    public int TipoTerminalId
    {
      get => this._tipoTerminalId;
      set => this._tipoTerminalId = value;
    }

    public bool MultiRep
    {
      get => this._multiRep;
      set => this._multiRep = value;
    }

    public int grupoId
    {
      get => this._grupoId;
      set => this._grupoId = value;
    }

    private void InicializarSocket()
    {
      if (this._tipoConexao == 2)
        this._clienteSocket = ClienteAssincrono.getInstance(this._port, this._host, this._numTerminal, this._produto, this._tipoConexao, RegistrySingleton.GetInstance().PINGINATIVO);
      else
        this._clienteSocket = ClienteAssincrono.getInstance(this._port, IPAddress.Parse(this._ipAddress), this._numTerminal, this._produto, RegistrySingleton.GetInstance().PINGINATIVO);
    }

    public virtual RepBase PesquisarRepPorID(int repId)
    {
      RepBase repBase = (RepBase) null;
      try
      {
        repBase = new RepBase().PesquisarRepPorID(repId);
        this._tipoTerminalId = repBase.TerminalId;
        this._configuracaoId = repBase.ConfiguracaoId;
        this._repId = repBase.RepId;
        this._ipAddress = repBase.Ip;
        this._port = repBase.Porta;
        this._chaveComunicacao = repBase.ChaveComunicacao;
        this.Local = repBase.Local;
        this._senhaComunic = repBase.SenhaComunic;
        this._senhaRelogio = repBase.SenhaRelogio;
        this._senhaBio = repBase.SenhaBio;
        this._serial = repBase.Serial;
        this._repIdSenior = repBase.RepIdSenior;
        this._repIdLeitoraSenior = repBase.RepIdLeitoraSenior;
        this._repClient = repBase.repClient;
        this._ipServidor = repBase.ipServidor;
        this._mascaraRede = repBase.mascaraRede;
        this._gateway = repBase.Gateway;
        this._portaServidor = repBase.portaServidor;
        this._tempoEspera = repBase.tempoEspera;
        this._intervaloConexao = repBase.intervaloConexao;
        this._portaVariavel = repBase.portaVariavel;
        this._desc = repBase.Desc;
        this._nomeServidor = repBase.nomeServidor;
        this._grupoId = repBase.grupoId;
        this._redeRemota = repBase.redeRemota;
        this._sincronizado = repBase.Sincronizado;
        this._ConfiguracaoLeitorCpf = repBase.ConfiguracaoLeitorCpf;
        this._ConfiguracaoLeitorDataHora = repBase.ConfiguracaoLeitorDataHora;
        this._envioNSR = repBase.envioNSR;
        if (this._tarefaAbstrata == null)
          this._tarefaAbstrata = new List<TopData.GerenciadorRep.Bi.Dominio.TarefaAbstrata>();
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
      return repBase;
    }

    public RepBase PesquisarRepPorIDAT(int repId)
    {
      RepBase repBase = (RepBase) null;
      try
      {
        repBase = new RepBaseAT().PesquisarRepPorID(repId);
        this._tipoTerminalId = repBase.TerminalId;
        this._configuracaoId = repBase.ConfiguracaoId;
        this._repId = repBase.RepId;
        this._ipAddress = repBase.Ip;
        this._port = repBase.Porta;
        this._chaveComunicacao = repBase.ChaveComunicacao;
        this.Local = repBase.Local;
        this._senhaComunic = repBase.SenhaComunic;
        this._senhaRelogio = repBase.SenhaRelogio;
        this._senhaBio = repBase.SenhaBio;
        this._serial = repBase.Serial;
        this._repIdSenior = repBase.RepIdSenior;
        this._repIdLeitoraSenior = repBase.RepIdLeitoraSenior;
        this._repClient = repBase.repClient;
        this._ipServidor = repBase.ipServidor;
        this._mascaraRede = repBase.mascaraRede;
        this._gateway = repBase.Gateway;
        this._portaServidor = repBase.portaServidor;
        this._tempoEspera = repBase.tempoEspera;
        this._intervaloConexao = repBase.intervaloConexao;
        this._portaVariavel = repBase.portaVariavel;
        this._desc = repBase.Desc;
        this._nomeServidor = repBase.nomeServidor;
        this._grupoId = repBase.grupoId;
        this._redeRemota = repBase.redeRemota;
        this._sincronizado = repBase.Sincronizado;
        if (this._tarefaAbstrata == null)
          this._tarefaAbstrata = new List<TopData.GerenciadorRep.Bi.Dominio.TarefaAbstrata>();
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
      return repBase;
    }

    public RepBase PesquisarRepPorIDRepSenior(int repId)
    {
      RepBase repBase = (RepBase) null;
      try
      {
        repBase = new RepBase().PesquisarRepPorIDRepSenior(repId);
        this._tipoTerminalId = repBase.TerminalId;
        this._configuracaoId = repBase.ConfiguracaoId;
        this._repId = repBase.RepId;
        this._ipAddress = repBase.Ip;
        this._port = repBase.Porta;
        this._chaveComunicacao = repBase.ChaveComunicacao;
        this.Local = repBase.Local;
        this._senhaComunic = repBase.SenhaComunic;
        this._senhaRelogio = repBase.SenhaRelogio;
        this._senhaBio = repBase.SenhaBio;
        this._serial = repBase.Serial;
        this._repIdSenior = repBase.RepIdSenior;
        this._repIdLeitoraSenior = repBase.RepIdLeitoraSenior;
        this._repClient = repBase.repClient;
        this._ipServidor = repBase.ipServidor;
        this._mascaraRede = repBase.mascaraRede;
        this._gateway = repBase.Gateway;
        this._portaServidor = repBase.portaServidor;
        this._tempoEspera = repBase.tempoEspera;
        this._intervaloConexao = repBase.intervaloConexao;
        this._portaVariavel = repBase.portaVariavel;
        this._desc = repBase.Desc;
        this._nomeServidor = repBase.nomeServidor;
        this._grupoId = repBase.grupoId;
        this._redeRemota = repBase.redeRemota;
        this._sincronizado = repBase.Sincronizado;
        this._modeloFim = repBase.ModeloFim;
        this._DNS = repBase.DNS;
        this._host = repBase.Host;
        this._nomeRep = repBase.NomeRep;
        this._tipoConexaoDNS = repBase.TipoConexaoDNS;
        this.TecnologiaProx = repBase.TecnologiaCartaoProx;
        this.TecnologiaBio = Convert.ToByte(repBase.TecnologiaBiometrica);
        if (this._tarefaAbstrata == null)
          this._tarefaAbstrata = new List<TopData.GerenciadorRep.Bi.Dominio.TarefaAbstrata>();
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
      return repBase;
    }

    public RepBase PesquisarRepPorIDSenior(int repIdSenior)
    {
      RepBase repBase = (RepBase) null;
      try
      {
        repBase = new RepBase().PesquisarRepPorIDSenior(repIdSenior);
        this._ipAddress = repBase.Ip;
        this._tipoTerminalId = repBase.TerminalId;
        this._configuracaoId = repBase.ConfiguracaoId;
        this._repId = repBase.RepId;
        this._chaveComunicacao = repBase.ChaveComunicacao;
        this.Local = repBase.Local;
        this._senhaComunic = repBase.SenhaComunic;
        this._senhaRelogio = repBase.SenhaRelogio;
        this._senhaBio = repBase.SenhaBio;
        this._serial = repBase.Serial;
        this._repIdSenior = repBase.RepIdSenior;
        this._repIdLeitoraSenior = repBase.RepIdLeitoraSenior;
        this._repClient = repBase.repClient;
        this._ipServidor = repBase.ipServidor;
        this._mascaraRede = repBase.mascaraRede;
        this._gateway = repBase.Gateway;
        this._portaServidor = repBase.portaServidor;
        this._tempoEspera = repBase.tempoEspera;
        this._intervaloConexao = repBase.intervaloConexao;
        this._portaVariavel = repBase.portaVariavel;
        this._nomeServidor = repBase.nomeServidor;
        this._grupoId = repBase.grupoId;
        this.ConfiguracaoLeitorCpf = repBase.ConfiguracaoLeitorCpf;
        this.ConfiguracaoLeitorDataHora = repBase.ConfiguracaoLeitorDataHora;
        this._redeRemota = repBase.redeRemota;
        this.TecnologiaProx = repBase.TecnologiaCartaoProx;
        this._DNS = repBase.DNS;
        this._host = repBase.Host;
        this._nomeRep = repBase.NomeRep;
        this._tipoConexaoDNS = repBase.TipoConexaoDNS;
        this.Sincronizado = repBase.Sincronizado;
        if (this._tarefaAbstrata == null)
          this._tarefaAbstrata = new List<TopData.GerenciadorRep.Bi.Dominio.TarefaAbstrata>();
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
      return repBase;
    }

    public int AtualizarConfiguracoes(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().AtualizarConfiguracoes(RepBaseEnt);
        this.Local = RepBaseEnt.Local;
        this._senhaComunic = RepBaseEnt.SenhaComunic;
        this._senhaRelogio = RepBaseEnt.SenhaRelogio;
        this._senhaBio = RepBaseEnt.SenhaBio;
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

    public int AtualizarConfiguracoesLeitorCPF(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().AtualizarConfiguracoesLeitorCPF(RepBaseEnt);
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

    public int ConfirmarConfiguracoesLeitorCPFSenior(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().ConfirmarConfiguracoesLeitorCPFSenior(RepBaseEnt);
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

    public int AtualizarChaveComunicacao(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().AtualizarChaveComunicacao(RepBaseEnt);
        this._chaveComunicacao = RepBaseEnt.ChaveComunicacao;
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

    public int AtualizarModeloBio(int repId, int modelo)
    {
      int num = 0;
      try
      {
        num = new RepBase().AtualizarModeloBio(repId, modelo);
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

    public int AtualizarIP(RepBase RepEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().AtualizarIP(RepEnt);
        this.IpAddress = RepEnt.Ip;
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

    public int AtualizarRep(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().AtualizarRep(RepBaseEnt);
        this.Local = RepBaseEnt.Local;
        this._senhaComunic = RepBaseEnt.SenhaComunic;
        this._senhaRelogio = RepBaseEnt.SenhaRelogio;
        this._senhaBio = RepBaseEnt.SenhaBio;
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

    public int AtualizarRepSenior(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().AtualizarRepSenior(RepBaseEnt);
        this.Local = RepBaseEnt.Local;
        this._senhaComunic = RepBaseEnt.SenhaComunic;
        this._senhaRelogio = RepBaseEnt.SenhaRelogio;
        this._senhaBio = RepBaseEnt.SenhaBio;
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

    public int AtualizarPosMemRep(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().AtualizarPosMemRep(RepBaseEnt);
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

    public int AtualizarNSRInicialEnvio(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().AtualizarNSREnvio(RepBaseEnt);
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

    public SortableBindingList<RepBase> PesquisarRepsPorEmpregador(
      int _empregadorId)
    {
      SortableBindingList<RepBase> sortableBindingList = new SortableBindingList<RepBase>();
      try
      {
        sortableBindingList = new RepBase().PesquisarRepPorEmpregador(_empregadorId);
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

    public SortableBindingList<RepBase> PesquisarRepsNaoPlusPorEmpregador(
      int _empregadorId)
    {
      SortableBindingList<RepBase> sortableBindingList = new SortableBindingList<RepBase>();
      try
      {
        sortableBindingList = new RepBase().PesquisarRepNaoPlusPorEmpregador(_empregadorId);
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

    public SortableBindingList<RepBase> PesquisarRepsPlusPorEmpregador(
      int _empregadorId)
    {
      SortableBindingList<RepBase> sortableBindingList = new SortableBindingList<RepBase>();
      try
      {
        sortableBindingList = new RepBase().PesquisarRepPlusPorEmpregador(_empregadorId);
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

    public static bool EmpregadorPossuiRepBIO(int _empregadorId)
    {
      bool flag = false;
      try
      {
        flag = new RepBase().EmpregadorPossuiRepBIO(_empregadorId);
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

    public virtual List<RepBase> PesquisarReps()
    {
      List<RepBase> repBaseList = new List<RepBase>();
      try
      {
        repBaseList = new RepBase().PesquisarReps();
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
      return repBaseList;
    }

    public List<RepBase> PesquisarRepsSenior(bool ativo) => new RepBase().PesquisarRepsSENIOR(ativo);

    public virtual List<RepBase> PesquisarRepsComunicacaoNuvem()
    {
      List<RepBase> repBaseList = new List<RepBase>();
      try
      {
        repBaseList = new RepBase().PesquisarRepComunicaNuvem();
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
      return repBaseList;
    }

    public SortableBindingList<RepBase> PesquisarRepsPorGrupo(int _grupoId)
    {
      SortableBindingList<RepBase> sortableBindingList = new SortableBindingList<RepBase>();
      try
      {
        sortableBindingList = new RepBase().PesquisarRepPorGrupo(_grupoId);
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

    public void DesativarRepSenior(int repIdSenior)
    {
      try
      {
        new RepBase().DesativarRepSenior(repIdSenior);
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
    }

    public void DesativarRepsNaoRecebidosSenior(string repsRecebidosIdSenior)
    {
      try
      {
        new RepBase().DesativarRepsNaoRecebidosSenior(repsRecebidosIdSenior);
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
    }

    public void ExcluirRepPorId(int _repId, int empregadorID)
    {
      try
      {
        new ArquivoBilhete().ExcluirArquivoAFDPorRepId(_repId);
        new RepBase().ExcluirRepPorId(new RepBase()
        {
          RepId = _repId
        });
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
    }

    public void ExcluirRepConfiguracaoGeral(int _repId)
    {
      int num = 0;
      try
      {
        RepBase repBase = new RepBase();
        RepBase RepBaseEnt = new RepBase();
        RepBaseEnt.RepId = _repId;
        num = repBase.ExcluirConfiguracalGeral(RepBaseEnt) + repBase.ExcluirRepConfiguracalGeral(RepBaseEnt);
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
    }

    public int PesquisarUltimoIdConfiguracaoGeral()
    {
      int num = 0;
      try
      {
        num = new RepBase().PesquisarUltimoIdConfiguracaoGeral();
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

    public int PesquisarRepConfigIdSenior(int repId)
    {
      int num = 0;
      try
      {
        num = new RepBase().PesquisarRepConfigIdSenior(repId);
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

    public int RemoverRepConfigIdSenior(int repId)
    {
      int num = 0;
      try
      {
        num = new RepBase().RemoverRepConfigIdSenior(repId);
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

    public int PesquisarUltimoIdConfiguracaoGeralSenior()
    {
      int num = 0;
      try
      {
        num = new RepBase().PesquisarUltimoIdConfiguracaoGeralSenior();
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

    public RepBase(
      string ipAddress,
      int port,
      ushort numTerminal,
      ushort tipoTerminalId,
      string chaveComunicacao,
      Empregador empregador,
      string local,
      string descricao,
      string senhaRelogio,
      string senhaComunic,
      string senhaBio,
      string host,
      int tipoConexao)
    {
      this._ipAddress = ipAddress;
      this._port = port;
      this._numTerminal = numTerminal;
      this._tipoTerminal = this.TipoTerminal;
      this._chaveComunicacao = chaveComunicacao;
      this._senhaRelogio = senhaRelogio;
      this._senhaComunic = senhaComunic;
      this._senhaBio = senhaBio;
      this._relogio = new Relogio();
      this._empregado = new Empregado();
      this._empregador = empregador;
      this._EnvioRep = new EnvioREP();
      this.Local = local;
      this._desc = descricao;
      this._cartaoEmpregado = new CartaoEmpregado();
      this._arquivoBilhete = new ArquivoBilhete();
      this._marcacao = new Marcacao(this);
      this._host = host;
      this._tipoConexao = tipoConexao;
      this.InicializarSocket();
    }
  }
}
