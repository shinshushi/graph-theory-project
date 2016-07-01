#pragma once
struct  PLAYER
{
	char ID[100];
	char PASSWORD[100];
	int SCORE;
};
void HienThiMenuDangnhap_Dangky();
void DangKy(PLAYER PL);
void DangNhap(char ID[100],char *savefile_name);
void MenuDangnhap_Dangky(char ID1[100],char ID2[100],char *savefilename);
int getPlayer(FILE *accFile,PLAYER &pl);
void XemDiem2NguoiDangDangNhap(FILE *accFile,char ID[100],int moc);
void GhiDiem(FILE *accFile,char ID[100]);
void NhapMatKhau(char PASSWORD[100],int vtx,int vty );
void XemDanhSachDiemCaoNhat(FILE *accFile);