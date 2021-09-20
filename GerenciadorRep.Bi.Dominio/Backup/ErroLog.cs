// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.ErroLog
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public enum ErroLog
  {
    ErrConectarSocketException = 5,
    ErrConectarException = 6,
    ErrEnviarAutenticacaoException = 7,
    NotificarNenhumaMsg = 8,
    NotificarTimeOutAck = 9,
    ErrTimerColetaTravado = 10, // 0x0000000A
    ErrDesabilitarTimers = 11, // 0x0000000B
    ErrHabilitarTimers = 12, // 0x0000000C
    ErrTimerImpressorasTravado = 13, // 0x0000000D
  }
}
