#include <stdio.h>
#include <Windows.h>
#include <string.h>
#include <conio.h>
#include <ctype.h>
#include "HamMuon.h"
#include "Play.h"
#include "Skin.h"
#include "User.h"
#define max 2048

//Ham lay mot chuoi tu file va cat bo ki tu trong
void getPassword(FILE *fp,char pass[])
{
	fgets(pass,100,fp);
	int i=0;
	while(1)
	{
		if(pass[i]==' ')
			break;
		i++;
	}
	pass[i]='\0';
}
//Ham nhap mat khau
void NhapMatKhau(char PASSWORD[100],int vtx,int vty )                  //vtx,vty: toan do de in chuoi mat khau ra man hinh cho dep mat
{
	char c;
	int i=0;										//vi tri ghi ki tu tren mang PASSWORD
	while(1)
	{
		c = getch();
		if(c==13)
			break;
		if(c==8)									//dung de xoa mat khau vua nhap va nhap lai
		{
			gotoxy(vtx-1,vty);
			printf(" ");							//lam mat dau * tren man hinh
			vtx--;
			i--;
		}
		else
		{
			c=(c+1)*2-3;							//ma hoa ki tu theo quy tac rieng
			PASSWORD[i]=c;
			gotoxy(vtx,vty);
			printf("*");							//in dau * ra mang hinh thay cho ki tu vua moi nhap
			vtx++;
			i++;
		}
	}
	PASSWORD[i]='\0';
}
//Ham dang ky
void DangKy()
{
	char ID[100]; 
	char PASSWORD[100];
	char PASS_confirm[100];
	int vitri;													//vi tri doc ghi tren file
	gotoxy(25,20);
	printf("Ten tai khoan: ");
	fflush(stdin);
	gotoxy(45,20);
	gets(ID);
	if(ID[0]==' '||ID[0]=='\t'||ID[0]==NULL)                      //Kiem tra tinh hop le cua ID nhap vao
	{
		gotoxy(22,25);
		mauchu(14);
		printf("Ten tai khoan chua ki tu khong hop le!");
		Sleep(1000);
		XoaManHinh();
		gotoxy(30,16);
		printf("Moi ban dang ki lai!");
		mauchu(15);
		DangKy();
	}
	else
	{
		FILE*accFile = fopen("Account.txt", "a+t");				//file chua danh sach nguoi choi
		if(accFile !=NULL)
		{
			int KTten = findchar(accFile,ID,vitri);             //Kiem tra tai khoan da ton tai chua
			if(KTten == 1)
			{
				gotoxy(25,25);
				mauchu(14);
				printf("Tai khoan da ton tai!");
				Sleep(1000);
				XoaManHinh();
				gotoxy(30,16);
				printf("Moi ban dang ki lai!");
				mauchu(15);
				DangKy();
			}
			else
			{
				gotoxy(25,21);
				printf("Mat khau: ");
				NhapMatKhau(PASSWORD,45,21);
				if(PASSWORD[0]==' '||PASSWORD[0]=='\t'||PASSWORD[0]==NULL)				//Kiem tra tinh hop le cua mat khau nhap vao
				{
					gotoxy(22,25);
					mauchu(14);
					printf("Mat khau chua ki tu khong hop le!");
					Sleep(1000);
					XoaManHinh();
					gotoxy(30,16);
					printf("Moi ban dang ki lai!");
					mauchu(15);
					DangKy();
				}
				else
				{
					gotoxy(25,22);
					printf("Nhap lai mat khau: ");
					NhapMatKhau(PASS_confirm,45,22);
					if(strcmp(PASSWORD,PASS_confirm)==0)		//so sanh hai mat khau vua nhap
					{
						fputs(ID, accFile);						//ghi thong tin nguoi choi vao file chua danh sach nguoi choi
						fprintf(accFile,"\n");
						fputs(PASSWORD, accFile);
						fprintf(accFile,"                                                                                  \n");
						fprintf(accFile,"%d      \n",0);
						fclose(accFile);
						gotoxy(25,25);
						mauchu(14);
						printf("Dang ky thanh cong!");
						Sleep(1000);
					}
					else
					{
						gotoxy(20,25);
						mauchu(14);
						printf("Hai lan nhap mat khau khong giong nhau.");
						Sleep(1000);
						XoaManHinh();
						gotoxy(30,16);
						printf("Moi ban dang ki lai!");
						mauchu(15);
						DangKy();
					}
				}
			}
		}
	}			
}	
//Ham dang nhap
void DangNhap(char ID[100],char *savefile_name)
{
	char PASSWORD[100];
	char str_temp[100];													//chuoi phu
	int vitri;															//vi tri con tro doc ghi du lieu tren file
	gotoxy(25,20);
	printf("Ten tai khoan: ");
	fflush(stdin);
	gets(ID);
	gotoxy(25,21);
	printf("Mat khau: ");
	NhapMatKhau(PASSWORD,40,21);
	FILE *accFile = fopen("Account.txt", "rt");
	if(accFile != NULL)
	{
		int KTten = findchar(accFile,ID,vitri);
		if(KTten==1)
		{
			if(strcmp(ID,savefile_name)==0)
			{
				gotoxy(25,25);
				mauchu(14);
				printf("Tai khoan da co nguoi dang nhap!");
				Sleep(1000);
				XoaManHinh();
				gotoxy(30,16);
				mauchu(14);
				printf("Moi ban dang nhap lai!");
				mauchu(15);
				DangNhap(ID,savefile_name);
			}
			else
			{
				getPassword(accFile,str_temp);
				if(strcmp(str_temp,PASSWORD)==0)
				{
					gotoxy(25,25);
					mauchu(14);
					printf("Dang nhap thanh cong!");
					Sleep(1000);
				}
				else
				{
					gotoxy(25,25);
					mauchu(14);
					printf("Mat khau khong chinh xac!");
					Sleep(1000);
					XoaManHinh();
					gotoxy(30,16);
					mauchu(14);
					printf("Moi ban dang nhap lai!");
					mauchu(15);
					DangNhap(ID,savefile_name);
				}
			}
		}
		else
		{
			gotoxy(25,25);
			mauchu(14);
			printf("Tai khoan khong ton tai!");
			Sleep(1000);
			XoaManHinh();
			gotoxy(30,16);
			mauchu(14);
			printf("Moi ban dang nhap lai!");
			mauchu(15);
			DangNhap(ID,savefile_name);
		}
	}
	fclose(accFile);
}
//Ham doi mat khau
void DoiMatKhau()
{
	char ID[100];
	char PASSWORD[100];					//mat khau cu
	char PASSWORD_NEW[100];				//mat khau moi
	char str_temp[100];
	int vitri;
	gotoxy(25,20);
	printf("Nhap ten tai khoan: ");
	fflush(stdin);
	gotoxy(50,20);
	gets(ID);
	gotoxy(25,21);
	printf("Nhap mat khau hien tai: ");
	NhapMatKhau(PASSWORD,50,21);
	FILE *accFile = fopen("Account.txt", "r+");
	if(accFile != NULL)
	{
		int KiemtraID = findchar(accFile,ID,vitri);
		if(KiemtraID==1)
		{
			getPassword(accFile,str_temp);
			if(strcmp(str_temp,PASSWORD)==0)
			{
				gotoxy(25,22);
				printf("Nhap mat khau moi: ");
				NhapMatKhau(PASSWORD_NEW,50,22);
				fseek(accFile,vitri,0);
				fputs(PASSWORD_NEW,accFile);
				fprintf(accFile,"                                        ");
				gotoxy(25,25);
				mauchu(14);
				printf("Doi mat khau thanh cong!");
				Sleep(1000);
			}
			else
			{
				gotoxy(25,25);
				mauchu(14);
				printf("Mat khau khong chinh xac!");
				Sleep(1000);
				XoaManHinh();
				gotoxy(30,16);
				mauchu(14);
				printf("Moi ban nhap lai!");
				mauchu(15);
				DoiMatKhau();
			}
		}
		else
		{
			gotoxy(25,25);
			mauchu(14);
			printf("Tai khoan khong ton tai!");
			Sleep(1000);
			XoaManHinh();
			gotoxy(30,16);
			mauchu(14);
			printf("Moi ban nhap lai!");
			mauchu(15);
			DoiMatKhau();
		}
	}
	fclose(accFile);
}
//Ham lay thong tin cua nguoi choi
int getPlayer(FILE *accFile,PLAYER &pl)								
{
	char scor[20];
	if(fgets(pl.ID,100,accFile)==NULL)								//lay ten nguoi choi
		return 0;
	pl.ID[strlen(pl.ID)-1]='\0';									
	getPassword(accFile,pl.PASSWORD);								//Lay password
	getPassword(accFile,scor);										//lay diem
	pl.SCORE=atoi(scor);
	return 1;
}
//Ham lay thong tin tat ca nguoi choi trong danh sach nguoi choi
void getAllPlayer(FILE *accFile,PLAYER plList[200],int &n)        
{
	n=0;
	while(!feof(accFile))
	{
		if(getPlayer(accFile,plList[n])==1)
			n++;
	}
}
//Ham sap xep thu tu nguoi choi theo diem giam dan		
void sortPlayerbyScore(PLAYER plList[200],int n)								
{
	PLAYER pl;
	for(int i=0;i<n-1;i++)
		for(int j=i+1;j<n;j++)
			if(plList[i].SCORE<plList[j].SCORE)
			{
				pl=plList[i];
				plList[i]=plList[j];
				plList[j]=pl;
			}
}
//Ham in danh sach nguoi choi ra man hinh
void printPLAYER(PLAYER plList[200],int n)
{
	mauchu(15);
	int y=18;
	for(int i=0;i<5;i++)
	{
		gotoxy(26,y);
		printf("%d.",i+1);
		gotoxy(28,y);
		puts(plList[i].ID);
		gotoxy(48,y);
		printf("%d",plList[i].SCORE);
		y++;
	}
}
//Ham xem danh sach diem cao nhat
void XemDanhSachDiemCaoNhat(FILE *accFile)
{
	PLAYER plList[200];
	int n;
	getAllPlayer(accFile,plList,n);
	sortPlayerbyScore(plList,n);
	printPLAYER(plList,n);
}
//Ham xem diem cua 2 nguoi dang dang nhap
void XemDiem2NguoiDangDangNhap(FILE *accFile,char ID[100],int moc)
{
	int vitri;
	char pass[100];
	char scor[20];
	int score;
	if(findchar(accFile,ID,vitri))
	{
		fgets(pass,100,accFile);
		getPassword(accFile,scor);
		score=atoi(scor);
	}
	mauchu(14);
	gotoxy(28,moc);
	printf("Nguoi choi: ");
	gotoxy(28,moc+2);
	printf("Diem: ");
	mauchu(15);
	gotoxy(40,moc);
	fputs(ID,stdout);
	gotoxy(40,moc+2);
	printf("%d",score);
}
//Ham cap nhap diem nguoi choi
void GhiDiem(FILE *accFile,char ID[100])
{
	int vitridocghi;
	char pass[100];									//chuoi pass de get mat khau
	char scor[20];									//chuoi scor de get diem
	int score;										//bien score de luu diem
	if(findchar(accFile,ID,vitridocghi))			//tim toi dong chua ID
	{
		fgets(pass,100,accFile);					//get dong chua password
		vitridocghi=ftell(accFile);					//ghi nhan lai vi tri doc ghi file hien tai
		getPassword(accFile,scor);					//get diem vao chuoi scor
		score=atoi(scor);							//bien diem tu dang chuoi sang sang so
		score+=100;									//cap nhat lai diem moi
		fseek(accFile,vitridocghi,0);				//di chuyen vi tri doc ghi file lai ngay dong luu diem
		fprintf(accFile,"%d",score);				//in diem moi vao file
	}
}
//Menu dang nhap-dang ky-doi mat khau
void MenuDangnhap_Dangky(char ID1[100],char ID2[100],char *savefile_name)
{
	int choose;
	do
	{
		choose=NhapLuaChon();
		switch(choose)
		{
		case 1:
			{
				XoaManHinh();
				gotoxy(30,16);
				mauchu(14);
				printf("Moi ban dang ki!");
				mauchu(15);
				DangKy();
				XoaManHinh();
				HienThiMenuDangnhap_Dangky();
				MenuDangnhap_Dangky(ID1,ID2,savefile_name);
				break;
			}
		case 2:
			{
				XoaManHinh();
				gotoxy(25,16);
				mauchu(14);
				printf("Moi nguoi choi thu nhat dang nhap!");
				mauchu(15);
				DangNhap(ID1,savefile_name);
				strcpy(savefile_name,ID1);
				XoaManHinh();
				gotoxy(25,16);
				mauchu(14);
				printf("Moi nguoi choi thu hai dang nhap!");
				mauchu(15);
				DangNhap(ID2,savefile_name);
				strcat(savefile_name,ID2);
				strcat(savefile_name,".txt");
				XoaManHinh();
				HienThiMenuChinh();
				MenuChinh(ID1,ID2,savefile_name);
				break;
			}
		case 3:
			{
				XoaManHinh();
				gotoxy(30,16);
				mauchu(14);
				printf("Moi ban doi mat khau");
				mauchu(15);
				DoiMatKhau();
				XoaManHinh();
				HienThiMenuDangnhap_Dangky();
				MenuDangnhap_Dangky(ID1,ID2,savefile_name);
				break;
			}
		}
	}
	while(choose!=4);
}
