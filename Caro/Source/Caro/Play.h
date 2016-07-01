#pragma once
void HienThiMenuChinh();
void InBanCoRaManHinh(char A[100][100],int m,int n,int mauX,int mauO);
int ChangePlayer(int n);
int KiemTraODaDanhChua(char A[100][100],int x,int y);
int KiemTraHangDoc(char A[100][100],int X,int Y);
int KiemTraHangNgang(char A[100][100],int X,int Y);
int KiemTraCheoChinh(char A[100][100],int X,int Y);
int KiemTraCheoPhu(char A[100][100],int X,int Y);
int KiemTraThangThua(char A[100][100],int X,int Y);
void DoiToaDoManHinhSangToaDoMaTran(int x,int y,int &X,int &Y);
void LuuTrangThaiBanCo(FILE *savefile,char A[100][100],int m,int n);
void InTrangThaiBanCoRaManHinh(FILE *savefile,char A[100][100],int m,int n,int mauX,int mauO);
void Play(char ID1[100],char ID2[100],char *savefile_name,char A[100][100],int m,int n,int i,int j,char PL1[100][2],char PL2[100][2],int mauX,int mauO);
void MenuChinh(char ID1[100],char ID2[100],char *savefile_name);
void LuaChonKichThuocBanCo(int &m, int &n);
void TaoBanCo(char A[100][100],int m,int n,int mauX,int mauO);
void HienThiDanhSachNuocCo(char nuoc[100][2],int dong,int vitriin);
void LoadDanhSachNuocCo(FILE *savefile_main,char nuoc[100][2],int dong);
void HienThiMenuChonMauSacQuanCo();
int LuaChonMauChoQuanCo();
void LoadGame(FILE *savefile_main,char A[100][100],int &m,int &n,int &i,int &j,char PL1[100][2],char PL2[100][2],int &mauX,int &mauO);
void SaveGame(FILE *savefile_main,char A[100][100],int m,int n,int i,int j,char PL1[100][2],char PL2[100][2],int mauX,int mauO);
void NewGame(char ID1[100],char ID2[100],char A[100][100],int &m,int &n,int &mauX,int &mauO);
void Restart(char ID1[100],char ID2[100],char *savefile_name,int m,int n,int i,int j,int mauX,int mauO);
void LuuDanhSachNuocCo(FILE *savefile_main,char nuoc[100][2],int dong);
void Thang(FILE *accFile,char ID[100],int moc,int z);

