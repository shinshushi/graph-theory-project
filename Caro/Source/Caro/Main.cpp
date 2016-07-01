#include <stdio.h>
#include <Windows.h>
#include <string.h>
#include <conio.h>
#include <ctype.h>
#include "HamMuon.h"
#include "Play.h"
#include "Skin.h"
#include "User.h"

void main()
{
	char ID1[100],ID2[100];
	char *savefile_name = new char [100];
	chucaro();
	gotoxy(30,20);
	mauchu(14);
	printf("Nhan ENTER de vao game");
	chuchay();
	XoaManHinh();
	HienThiMenuDangnhap_Dangky();
	MenuDangnhap_Dangky(ID1,ID2,savefile_name);
}
