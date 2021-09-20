// Decompiled with JetBrains decompiler
// Type: ASO15_DEF
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Runtime.InteropServices;

public class ASO15_DEF
{
  public const int RES_OK = 0;
  public const int IS_EMPTY_ID = 1;
  public const int IS_EMPTY_FINGER = 1;
  public const int IS_MANAGER = 1;
  public const int IS_USER = 0;
  public const int ERR_MEMORY = -1;
  public const int ERR_INVALID_ID = -2;
  public const int ERR_INVALID_TEMPLATE = -3;
  public const int ERR_NOT_EMPTY = -4;
  public const int ERR_DISK_SIZE = -5;
  public const int ERR_NOT_USBDEV = -6;
  public const int ERR_INVALID_PARAMETER = -7;
  public const int ERR_FAIL_INIT_ENGINE = -8;
  public const int ERR_FAIL_MATCH_PROC = -9;
  public const int ERR_FP_TIMEOUT = -10;
  public const int ERR_BAD_QUALITY = -11;
  public const int ERR_FAIL_GEN = -12;
  public const int ERR_FAIL_FILE_IO = -13;
  public const int ERR_ENROLLED_ID = -14;
  public const int ERR_ENROLLED_FINGER = -15;
  public const int ERR_INVALID_FINGERNUM = -16;
  public const int ERR_FAIL_MATCH = -17;
  public const int ERR_NOT_ENROLLED = -18;
  public const int ERR_FAIL_SET = -19;
  public const int ERR_DUPLICATED = -20;
  public const int ERR_CANCELED = -21;
  public const int ERR_UNKNOWN = -30;
  public const int SFEPDB_REG_MAX = 100000;
  public const int MAX_IDNUMBER = 100000;
  public const int MAX_FPNUMBER = 10;
  public const int SFEP_UFPDATA_SIZE = 498;
  public const int IMAGE_WIDTH = 600;
  public const int IMAGE_HEIGHT = 400;
  public const int IMAGE_FULL_WIDTH = 636;
  public const int IMAGE_FULL_HEIGHT = 478;

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_Initialize();

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_Uninitialize();

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_SetDatabasePath(string szPath);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_SetConfig(IntPtr hWnd);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_SetBrightness(byte bBVal);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_GetBrightness(ref byte pbBVal);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_GetLiveImage();

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_CalcBrightness();

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_CalcBrightnessInFullImage();

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_CurrentSaveBMP(string szFileName, int nWidth, int nHeight);

  [DllImport("SPL_ASO15.dll")]
  public static extern bool SFEP_IsFingerPress();

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_CaptureFingerImage(uint dwTimeOut);

  [DllImport("SPL_ASO15.dll")]
  public static extern void SFEP_FpCancel();

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_CreateTemplate(ref byte pTemplate);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_GetTemplateForRegister(ref byte pTemplates, ref byte pRegTem);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_Enroll(
    ref byte pTemplate,
    ref uint pdwID,
    ref byte pbFingerNum,
    ref byte pbManager);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_Identify(
    ref byte pTemplate,
    ref uint pdwID,
    ref byte pbFingerNum,
    ref byte pbManager,
    byte bSecLevel);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_Verify(
    ref byte pTemplate,
    uint dwID,
    byte bFingerNum,
    byte bSecLevel);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_Match2Template(
    ref byte pTemplate1,
    ref byte pTemplate2,
    byte bSecLevel);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_RemoveTemplate(uint dwID, byte bFingerNum);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_RemoveAll();

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_ReadTemplate(uint dwID, byte bFingerNum, string szFileName);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_WriteTemplate(
    uint dwID,
    byte bFingerNum,
    byte bManager,
    string szFileName);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_GetEnrollCount();

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_CheckID(uint dwID);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_CheckFingerNum(uint dwID, byte bFingerNum);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_SearchID(ref uint pdwID);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_SearchFingerNumber(uint pdwID, ref byte pbFingerNum);

  [DllImport("SPL_ASO15.dll")]
  public static extern int SFEP_SetCameraType(byte pbType);

  public struct SFEP_USER_FPDATA
  {
    public byte[] rbData;
  }
}
