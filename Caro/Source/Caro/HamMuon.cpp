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
//Cac ham tim hieu tren internet
void gotoxy(int x, int y) 
{
  COORD c;

  c.X = x - 1;
  c.Y = y - 1;
  SetConsoleCursorPosition (GetStdHandle(STD_OUTPUT_HANDLE), c);
}
void mauchu(WORD color)
{
    HANDLE hConsoleOutput;
    hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE);
    CONSOLE_SCREEN_BUFFER_INFO screen_buffer_info;
    GetConsoleScreenBufferInfo(hConsoleOutput, &screen_buffer_info);
    WORD wAttributes = screen_buffer_info.wAttributes;
    color &= 0x000f;
    wAttributes &= 0xfff0;
    wAttributes |= color;
    SetConsoleTextAttribute(hConsoleOutput, wAttributes);
}

void sizechu()
{
	HANDLE cons = GetStdHandle(STD_OUTPUT_HANDLE);
    PCONSOLE_FONT_INFOEX font = new CONSOLE_FONT_INFOEX() ;
    font->cbSize = sizeof(CONSOLE_FONT_INFOEX);
    GetCurrentConsoleFontEx(cons,0,font);
    font->dwFontSize.X = 8;
    font->dwFontSize.Y = 16;
    SetCurrentConsoleFontEx(cons,0,font);
}
//Cac ham ho tro trong qua trinh thuc hien
int findchar(FILE*infile,char str[],int &vitri)
{
	int found = 0;
	char line[max];
	while(fgets(line, max-1, infile)!=NULL)
	{
		line[strlen(line)-1]='\0';						//bo ki tu xuong dong trong chuoi line.
		if(strcmp(str,line)==0)
		{
			found = 1;
			vitri=ftell(infile);						//xac dinh vi tri con tro doc ghi du lieu hien tai
			break;
		}
	}
	return found;
}

void XoaManHinh()
{
	for(int i=1;i<=80;i++)
		for(int j=12;j<=50;j++)
		{
			gotoxy(i,j);
			printf(" ");
		}
}
int NhapLuaChon()
{
	int ch;
	scanf("%d",&ch);
	return ch;
}
