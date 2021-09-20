// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.TipoDeDados
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System.Runtime.InteropServices;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct TipoDeDados
  {
    public const int CONFIGURACOES_GERAIS = 1;
    public const int CONFIGURACOES_AVANCADAS = 2;
    public const int RELOGIO = 3;
    public const int TEMPLATES = 4;
    public const int COLETAAFD = 5;
    public const int HORARIOVERAO = 6;
    public const int ALTERACOES_EMPREGADOS_REP_PLUS = 7;
    public const int CHAVE_PUBLICA = 8;
    public const int SOLICITAR_RELOGIO = 9;
    public const int SOLICITAR_MODELO_BIOMETRIA = 10;
    public const int SOLICITAR_STATUS_PAPEL = 11;
  }
}
