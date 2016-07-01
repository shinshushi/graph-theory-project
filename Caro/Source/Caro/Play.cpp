#include <stdio.h>
#include <Windows.h>
#include <string.h>
#include <conio.h>
#include <ctype.h>
#include "HamMuon.h"
#include "Play.h"
#include "Skin.h"
#include "User.h"

void MenuChinh(char ID1[100],char ID2[100],char *savefile_name)
{
	int m,n;
	int i,j;										//vi tri ghi hien tai trong mang chua danh sach nuoc co
	int mauX,mauO;									//mau quan co
	char A[100][100];
	char PL1[100][2];								//mang luu danh sach nuoc di cuar nguoi choi 1
	char PL2[100][2];								//mang luu danh sach nuoc di cuar nguoi choi 1
	FILE *savefile_main, *accFile;
	int choose;
	choose=NhapLuaChon();
	switch(choose)
	{
	case 1:
		{
			NewGame(ID1,ID2,A,m,n,mauX,mauO);
			Play(ID1,ID2,savefile_name,A,m,n,0,0,PL1,PL2,mauX,mauO);
			XoaManHinh();
			HienThiMenuChinh();
			MenuChinh(ID1,ID2,savefile_name);
			break;
		}
	case 2:
		{
			XoaManHinh();
			savefile_main=fopen(savefile_name,"rt");
			if(savefile_main==NULL)
			{
				gotoxy(20,18);
				mauchu(14);
				printf("Hai ban khong co van co nao dang choi!");
				Sleep(2000);
			}
			else
			{
				LoadGame(savefile_main,A,m,n,i,j,PL1,PL2,mauX,mauO);
				fclose(savefile_main);
				Play(ID1,ID2,savefile_name,A,m,m,i+1,j+1,PL1,PL2,mauX,mauO);
			}
			XoaManHinh();
			HienThiMenuChinh();
			MenuChinh(ID1,ID2,savefile_name);
			break;
		}
	case 3:
		{
			XoaManHinh();
			LuatChoi();
			char c=getch();
			if(c==27)
			{
				XoaManHinh();
				HienThiMenuChinh();
				MenuChinh(ID1,ID2,savefile_name);
			}
			break;
		}
	case 4:
		{
			XoaManHinh();
			accFile=fopen("Account.txt","rt");
			XemDiem2NguoiDangDangNhap(accFile,ID1,17);
			XemDiem2NguoiDangDangNhap(accFile,ID2,21);
			fclose(accFile);
			gotoxy(22,25);
			mauchu(14);
			printf("Nhan esc de quay ve menu chinh");
			char c=getch();
			if(c==27)
			{
				XoaManHinh();
				HienThiMenuChinh();
				MenuChinh(ID1,ID2,savefile_name);
			}
			break;
		}
	case 5:
		{
			XoaManHinh();
			gotoxy(22,16);
			mauchu(14);
			printf("Danh sach 5 nguoi choi co diem cao nhat");
			accFile=fopen("Account.txt","rt");
			XemDanhSachDiemCaoNhat(accFile);
			fclose(accFile);
			gotoxy(22,40);
			mauchu(14);
			printf("Nhan esc de quay ve menu chinh");
			char c=getch();
			if(c==27)
			{
				XoaManHinh();
				HienThiMenuChinh();
				MenuChinh(ID1,ID2,savefile_name);
			}
			break;
		}
	case 6:
		{
			XoaManHinh();
			GioiThieuGame();
			char c=getch();
			if(c==27)
			{
				XoaManHinh();
				HienThiMenuChinh();
				MenuChinh(ID1,ID2,savefile_name);
			}
			break;
		}
	case 7:
		{
			XoaManHinh();
			HienThiMenuDangnhap_Dangky();
			MenuDangnhap_Dangky(ID1,ID2,savefile_name);
		}
	}
}

//Cac ham lua chon
void LuaChonKichThuocBanCo(int &m, int &n)
{
	gotoxy(25,15);
	printf("Moi ban lua chon kich thuoc ban co!");

	gotoxy(35,17);
	mauchu(10);
	printf("1. 10x10");

	gotoxy(35,18);
	mauchu(13);
	printf("2. 16x16");
	
	gotoxy(35,19);
	mauchu(14);
	printf("3. 20x20");
	int choose=NhapLuaChon();
	switch(choose)
	{
		case 1:{m=10;n=10;break;};
		case 2:{m=16;n=16;break;};
		case 3:{m=20;n=20;break;};
	}
}

int LuaChonMauChoQuanCo()
{
	int chon=NhapLuaChon();
	switch(chon)
	{
		case 1:return 9;break;
		case 2:return 10;break;
		case 3:return 11;break;
		case 4:return 12;break;
		case 5:return 13;break;
		case 6:return 14;break;
		case 7:return 15;break;
	}
}

//Ham tao ban co
void TaoBanCo(char A[100][100],int m,int n,int mauX,int mauO)
{
	mauchu(15);
	A[0][0]=' ';
	int maACSII=65;
	for(int i=1;i<m+1;i++)
	{
		A[i][0]=maACSII;
		maACSII++;
	}
	
	maACSII=65;
	for(int j=1;j<n+1;j++)
	{
		A[0][j]=maACSII;
		maACSII++;
	}

	for(int i=1;i<m+1;i++)
		for(int j=1;j<n+1;j++)
		{
			A[i][j]='.';
		}
	InBanCoRaManHinh(A,m,n,mauX,mauO);
	InHeToaDo(m,n);
}

//Ham in ban co
void InBanCoRaManHinh(char A[100][100],int m,int n,int mauX,int mauO)
{
	int x=(80-(n-1)*3)/2-3;							//Xac dinh moc hoanh do tren manh hinh de in ban co ra tuy thuoc vao kich thuoc ban co
	int y=14;
	for(int i=0;i<m+1;i++)
	{
		gotoxy(x,y);
		for(int j=0;j<n+1;j++)	
		{
			if(A[i][j]=='X')
			{
				mauchu(mauX);
				printf("%3c",A[i][j]);
			}
			else
			{
				if(A[i][j]=='O')
				{
					mauchu(mauO);
					printf("%3c",A[i][j]);
				}
				else
				{
					mauchu(15);
					printf("%3c",A[i][j]);
				}
			}
		}
		printf("\n\n");
		y+=2;
	}
}
//Ham doi luot choi
int ChangePlayer(int n)
{
	if(n==1 || n==-2)
		return 2;
	if(n==2 || n==-1)
		return 1;
}

//Ham kiem tra o da danh chua
int KiemTraODaDanhChua(char A[100][100],int X,int Y)
{
	if(A[X][Y]=='X' || A[X][Y]=='O')
		return 1;
	else
		return 0;
}
//Cac ham kiem tra thaang thua
int KiemTraHangDoc(char A[100][100],int X,int Y)
{
	int dem=1;
	int i=X-1;
	
	while(A[X][Y]==A[i][Y])
	{
		dem++;
		i--;
	}

	i=X+1;
	while(A[X][Y]==A[i][Y])
	{
		dem++;
		i++;
	}
	return dem;
}
int KiemTraHangNgang(char A[100][100],int X,int Y)
{
	int dem=1;
	int j=Y-1;
	
	while(A[X][Y]==A[X][j])
	{
		dem++;
		j--;
	}

	j=Y+1;
	while(A[X][Y]==A[X][j])
	{
		dem++;
		j++;
	}
	return dem;
}
int KiemTraCheoChinh(char A[100][100],int X,int Y)
{
	int dem=1;
	int i=X-1;
	int j=Y-1;
	while(A[X][Y]==A[i][j])
	{
		dem++;
		i--;
		j--;
	}
	i=X+1;
	j=Y+1;
	while(A[X][Y]==A[i][j])
	{
		dem++;
		i++;
		j++;
	}
	return dem;
}
int KiemTraCheoPhu(char A[100][100],int X,int Y)
{
	int dem=1;
	int i=X-1;
	int j=Y+1;
	while(A[X][Y]==A[i][j])
	{
		dem++;
		i--;
		j++;
	}
	i=X+1;
	j=Y-1;
	while(A[X][Y]==A[i][j])
	{
		dem++;
		i++;
		j--;
	}
	return dem;
}

int KiemTraThangThua(char A[100][100],int X,int Y)
{
	if(KiemTraHangDoc(A,X,Y)==5 || KiemTraHangNgang(A,X,Y)==5 || KiemTraCheoChinh(A,X,Y)==5 || KiemTraCheoPhu(A,X,Y)==5)
		return 1;
	else 
		return 0;
}

//Ham doi toa do man hinh sang toa do ma tran
void DoiToaDoManHinhSangToaDoMaTran(int x,int y,int moc,int &X,int &Y)
{
	X=(y-14)/2;						//doi tung do man hinh thanh hoanh do ma tran theo quy uoc rieng
	Y=(x-moc+3)/3;					//doi hoanh do man hinh thanh tung do ma tran theo quy uoc rieng
}

//Cac ham nhan chuc nang save
void LuuTrangThaiBanCo(FILE *savefile,char A[100][100],int m,int n)							//Ham luu trang thai ban co
{
	char ch;
	for(int i=0;i<m+1;i++)
	{
		for(int j=0;j<n+1;j++)
		{
			ch=A[i][j];
			fputc(ch,savefile);
		}
	}
}
void SaveGame(FILE *savefile_main,char A[100][100],int m,int n,int i,int j,char PL1[100][2],char PL2[100][2],int mauX,int mauO)
{
	fprintf(savefile_main,"%d ",mauX);				//luu mau quan X vao file
	fprintf(savefile_main,"%d ",mauO);				//luu mau quan O vao file
	fprintf(savefile_main,"%d",m);					//luu kich thuoc ban co vao file
	LuuTrangThaiBanCo(savefile_main,A,m,n);			//Luu trang thai ban co vao file
	LuuDanhSachNuocCo(savefile_main,PL1,i-1);		//Luu danh sach nuoc co cua nguoi choi 1
	LuuDanhSachNuocCo(savefile_main,PL2,j-1);		//Luu danh sach nuoc co cua nguoi choi 2
}
//Cac ham nhan chuc nang load
void LoadGame(FILE *savefile_main,char A[100][100],int &m,int &n,int &i,int &j,char PL1[100][2],char PL2[100][2],int &mauX,int &mauO)
{
	fscanf(savefile_main,"%d",&mauX);				
	fscanf(savefile_main,"%d",&mauO);
	fscanf(savefile_main,"%d",&m);
	InTrangThaiBanCoRaManHinh(savefile_main,A,m,m,mauX,mauO);
	fscanf(savefile_main,"%d",&i);
	LoadDanhSachNuocCo(savefile_main,PL1,i);
	HienThiDanhSachNuocCo(PL1,i,2);
	fscanf(savefile_main,"%d",&j);
	LoadDanhSachNuocCo(savefile_main,PL2,j);
	HienThiDanhSachNuocCo(PL2,j,74);
}
void InTrangThaiBanCoRaManHinh(FILE *savefile,char A[100][100],int m,int n,int mauX,int mauO)
{
	char ch;
	for(int i=0;i<m+1;i++)
	{
		for(int j=0;j<n+1;j++)
		{
			ch=fgetc(savefile);
			A[i][j]=ch;
		}
	}
	InBanCoRaManHinh(A,m,n,mauX,mauO);
	InHeToaDo(m,n);
}
//Cac ham lien quan toi nuoc co
void HienThiDanhSachNuocCo(char nuoc[100][2],int sodong,int vitriin)			//vitriin: moc hoanh do in danh sach nuoc di ung voi tung nguoi choi
{
	int y=16;																	//y: moc hoanh do in danh sach nuoc di
	for(int i=0;i<=sodong;i++)
	{
		gotoxy(vitriin,y);
		printf(" %c,%c \n",nuoc[i][0],nuoc[i][1]);
		y++;
	}
}
void LuuDanhSachNuocCo(FILE *savefile_main,char nuoc[100][2],int dong)
{
	fprintf(savefile_main,"%d",dong);									//in so dong hien tai cua manh chua danh sach nuoc co vao file
	for(int i=0;i<=dong;i++)
	{
		fputc(nuoc[i][0],savefile_main);
		fputc(nuoc[i][1],savefile_main);
	}
}
void LoadDanhSachNuocCo(FILE *savefile_main,char nuoc[100][2],int dong)
{
	char ch;
	for(int i=0;i<=dong;i++)
	{
		ch=fgetc(savefile_main);
		nuoc[i][0]=ch;
		ch=fgetc(savefile_main);
		nuoc[i][1]=ch;
	}
}

//Ham tao van choi moi
void NewGame(char ID1[100],char ID2[100],char A[100][100],int &m,int &n,int &mauX,int &mauO)
{
	XoaManHinh();
	LuaChonKichThuocBanCo(m,n);
	XoaManHinh();
	HienThiMenuChonMauSacQuanCo();
	gotoxy(25,15);
	mauchu(14);
	printf("Moi ");
	puts(ID1);
	gotoxy(29+strlen(ID1),15);
	printf(" lua chon mau quan co!");
	mauX=LuaChonMauChoQuanCo();
	gotoxy(25,15);
	mauchu(14);
	printf("Moi ");
	puts(ID2);
	gotoxy(29+strlen(ID2),15);
	printf(" lua chon mau quan co!");
	mauO=LuaChonMauChoQuanCo();
	XoaManHinh();
	HuongDanChoi();
	char c=getch();
	if(c==27)
	{
		XoaManHinh();
		TaoBanCo(A,m,n,mauX,mauO);
	}
}
//Ham choi lai
void Restart(char ID1[100],char ID2[100],char *savefile_name,int m,int n,int i,int j,int mauX,int mauO)
{
	char A[100][100];
	char PL1[100][2],PL2[100][2];
	TaoBanCo(A,m,n,mauX,mauO);
	Play(ID1,ID2,savefile_name,A,m,n,0,0,PL1,PL2,mauX,mauO);
}
//Ham thuc hien viec choi game
void Play(char ID1[100],char ID2[100],char *savefile_name,char A[100][100],int m,int n,int i,int j,char PL1[100][2],char PL2[100][2],int mauX,int mauO)
{
	char c;												//bien nhan su kien tu ban phim
	int X,Y;											//Toa do cua ma tran chua ban co
	FILE *savefile_temp;								//file luu tam thoi trang thai ban co sau moi nuoc di
	FILE *savefile_main;								//file luu ban co khi nguoi choi savegame
	FILE *accFile;										//file chua danh sach nguoi choi
	int vitridocghi;									//vi tri doc ghi du lieu tren file Account.txt
	int x=(80-(n-1)*3)/2+2;								//moc hoanh do man hinh tuy thuoc vao kich thuoc ban co
	int y=16;											//moc tung do man hinh
	int z=y+(m-1)*2+3;									//moc tung do ghi thong bao tren man hinh
	int moc=x;											//moc hoanh do ghi thong bao tren man hinh
	int player=1;										//bien xac dinh luot danh hien tai mac dinh ban dau bang 1
	gotoxy(moc+11,z);
	mauchu(mauX);
	puts(ID1);											//thong bao luot di cua nguoi choi 1
	gotoxy(x,y);
	while(1)
	{
		c=getch();										//nhan cac phim dieu khien
		if(c==9)										//phim TAB nhan chuc nang choi lai van co
		{
			XoaManHinh();
			Restart(ID1,ID2,savefile_name,m,n,0,0,mauX,mauO);
		}
		if(c==27)										//phim ESC nhan chuc nang thoat ve  MENU chinh
			break;
		if(c==13)										//phim ENTER nhan chuc nang savegame
		{
			savefile_main=fopen(savefile_name,"wt");
			SaveGame(savefile_main,A,m,n,i,j,PL1,PL2,mauX,mauO);
			fclose(savefile_main);
			gotoxy(moc,z+2);
			printf("Game da duoc save!");
			Sleep(2000);
		}
		if(c==75)										//phim qua trai
		{
			if(x-3>=moc)								//moc: moc hoanh do trai
				gotoxy(x-=3,y);
			else
				gotoxy(x,y);
		}
		if(c==77)										//phim qua phai
		{
			if(x+3<=(moc+(n-1)*3))						//moc+(n-1)*3): moc hoanh do phai
				gotoxy(x+=3,y);
			else;
				gotoxy(x,y);
				
		}
		if(c==72)										//phim di len
		{
			if(y-2>=16)									//16: moc tung do tren
				gotoxy(x,y-=2);
			else
				gotoxy(x,y);
		}
		if(c==80)										//phim di xuong
		{
			if(y+2<=(16+(m-1)*2))						//16+(m-1)*2): moc hoanh do duoi
				gotoxy(x,y+=2);
			else
				gotoxy(x,y);
		}
		if(c==32)										//phim SPACE nhan chuc nang danh co
		{
			printf("\a");								//tao tieng bip
			DoiToaDoManHinhSangToaDoMaTran(x,y,moc,X,Y);				//doi toa do man hinh sang toa do ma tran
			if(KiemTraODaDanhChua(A,X,Y)==0)
			{
				if(player==1)
				{
					mauchu(mauX);
					printf("X");
					gotoxy(x,y);
					c=getch();																//Xac nhan chuyen luot di cho doi phuong hay undo
					if(c==8)																//Undo 1 nuoc
					{
						savefile_temp=fopen("savefile.txt","rt");
						InTrangThaiBanCoRaManHinh(savefile_temp,A,m,n,mauX,mauO);			
						fclose(savefile_temp);
						gotoxy(x,y);
						player=-1;
					}
					else																	//Chap nhan luot danh cua minh
					{
						A[X][Y]='X';													//gan ki tu X vao ma tran chua ban co
						savefile_temp=fopen("savefile.txt","wt");						//Luu trang thai ban co hien tai vao file luu tam thoi
						LuuTrangThaiBanCo(savefile_temp,A,m,n);						
						fclose(savefile_temp);
						PL1[i][0]=Y+64;													//ghi toa do nuoc vua danh vao mang danh sach nuoc di
						PL1[i][1]=X+64;
						gotoxy(1,16);
						HienThiDanhSachNuocCo(PL1,i,2);									//in toa do nuoc vua danh ra man hinh
						i++;															//tang vi tri dong trong mang danh sach nuoc di
						gotoxy(x,y);
						if(KiemTraThangThua(A,X,Y)==1)
						{
							accFile=fopen("Account.txt","r+");
							Thang(accFile,ID1,moc,z);								
							fclose(accFile);
						}
						gotoxy(moc+11,z);
						printf("                                             ");
						gotoxy(moc+11,z);
						mauchu(mauO);
						puts(ID2);
						gotoxy(x,y);
					}
				}
				if(player==2)
				{
					mauchu(mauO);
					printf("O");
					gotoxy(x,y);
					c=getch();
					if(c==8)
					{
						savefile_temp=fopen("savefile.txt","rt");
						InTrangThaiBanCoRaManHinh(savefile_temp,A,m,n,mauX,mauO);
						fclose(savefile_temp);
						gotoxy(x,y);
						player=-2;
					}
					else
					{
						A[X][Y]='O';
						savefile_temp=fopen("savefile.txt","wt");
						LuuTrangThaiBanCo(savefile_temp,A,m,n);
						fclose(savefile_temp);
						PL2[j][0]=Y+64;
						PL2[j][1]=X+64;
						gotoxy(1,40);
						HienThiDanhSachNuocCo(PL2,j,74);
						j++;
						gotoxy(x,y);
						if(KiemTraThangThua(A,X,Y)==1)
						{
							accFile=fopen("Account.txt","r+");
							Thang(accFile,ID2,moc,z);
							fclose(accFile);
						}
						gotoxy(moc+11,z);
						printf("                                             ");
						gotoxy(moc+11,z);
						mauchu(mauX);
						puts(ID1);
						gotoxy(x,y);
					}
				}
				player=ChangePlayer(player);
			}
			else
			{
				gotoxy(moc-10,z+2);
				printf("O nay da danh roi, moi ban danh o khac!");
				Sleep(1000);
				gotoxy(moc-10,z+2);
				printf("                                        ");
				gotoxy(x,y);
			}
		}
	}
}

/*void TinhThoiGian(int &time,int moc,int z,int x,int y)
{
	time=5;
	while(!kbhit())
	{
		if(time==-1)
			break;
		Sleep(1000);
		time--;
	}
}*/
void Thang(FILE *accFile,char ID[100],int moc,int z)
{
	gotoxy(moc,z);
	puts(ID);
	gotoxy(moc+strlen(ID),z);
	printf(" WINNNNNN!!!");
	Sleep(3000);
	XoaManHinh();
	gotoxy(20,25);
	mauchu(14);
	printf("Nhan TAB de choi lai, nhan ESC tro ve menu chinh");
	GhiDiem(accFile,ID);
}
