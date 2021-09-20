// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.EstadosEnvioTemplate
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System.Runtime.InteropServices;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct EstadosEnvioTemplate
  {
    public const int Nenhum = 0;
    public const int AGUARDA_VALIDACOES = 1;
    public const int AGUARDA_LISTA = 2;
    public const int AGUARDA_TEMPLATES = 3;
    public const int AGUARDA_PROXIMO_ESTADO = 4;
  }
}
