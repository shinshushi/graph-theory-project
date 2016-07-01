#include <stdio.h>
#include <Windows.h>
#include <string.h>
#include <conio.h>
#include <ctype.h>
#include "HamMuon.h"
#include "Play.h"
#include "Skin.h"
#include "User.h"
void chucaro()
{
	mauchu(10);
	printf("********** _________          ___          ________       _______   ***********\n");
	printf("**********|         |        /   \\        |        \\     /       \\  ***********\n");
	printf("**********|   ______|       /  _  \\       |   ___   |   /   ___   \\ ***********\n");
	printf("**********|  |             /  / \\  \\      |  |___|  |  |   /   \\   |***********\n");
	printf("**********|  |            /  /___\\  \\     |         |  |  |     |  |***********\n");
	printf("**********|  |	         /           \\    |   __   /   |  |     |  |***********\n");
	printf("**********|  |______    /  _________  \\   |  |  \\  \\   |   \\___/   |***********\n");
	printf("**********|         |  /  /         \\  \\  |  |   \\  \\   \\         / ***********\n");
	printf("**********|_________| /__/           \\__\\ |__|    \\__\\   \\_______/  ***********\n");
	printf("\n");
	mauchu(9);
	printf("______________________Copyright of MANH SINH - NHAT SINH________________________");
}
void HienThiMenuDangnhap_Dangky()
{
	sizechu();
	gotoxy(30,15);
	mauchu(10);
	printf("1.Dang ky tai khoan moi");
	gotoxy(30,17);
	mauchu(13);
	printf("2.Dang nhap");
	gotoxy(30,19);
	mauchu(14);
	printf("3.Doi mat khau");
	gotoxy(30,21);
	mauchu(11);
	printf("4.Thoat");
}
void HienThiMenuChinh()
{
	gotoxy(30,15);
	mauchu(10);
	printf("1. New Game");
	gotoxy(30,17);
	mauchu(13);
	printf("2. Load Game");
	gotoxy(30,19);
	mauchu(14);
	printf("3. Luat choi");
	gotoxy(30,21);
	mauchu(11);
	printf("4. Xem diem");
	gotoxy(30,23);
	mauchu(10);
	printf("5. Xem danh sach nhung nguoi co diem cao nhat");
	gotoxy(30,25);
	mauchu(13);
	printf("6. Gioi thieu ve tro choi");
	gotoxy(30,27);
	mauchu(14);
	printf("7. Tro ve menu dang nhap-dangky");
}
void HienThiMenuChonMauSacQuanCo()
{
	mauchu(9);
	gotoxy(35,17);
	printf("1.Xanh duong");
	mauchu(10);
	gotoxy(35,18);
	printf("2.Xanh luc");
	mauchu(11);
	gotoxy(35,19);
	printf("3.Xanh lo");
	mauchu(12);
	gotoxy(35,20);
	printf("4.Do");
	mauchu(13);
	gotoxy(35,21);
	printf("5.Tim");
	mauchu(14);
	gotoxy(35,22);
	printf("6.Vang");
	mauchu(15);
	gotoxy(35,23);
	printf("7.Trang");
}
void InHeToaDo(int m,int n)
{
	int x=(80-(n-1)*3)/2+2;								//Xac dinh vi tri hoanh do de in he toa do ban co ra man hinh
	int y=16+(m-1)*2+3;
	mauchu(12);
	char c=65;
	for(int a=x;a<=x+(n-1)*3;a+=3)
	{
		gotoxy(a,14);
		printf("%c",c);
		c++;
	}
	c=65;
	for(int b=16;b<=16+(m-1)*2;b+=2)
	{
		gotoxy(x-3,b);
		printf("%c",c);
		c++;
	}
	gotoxy(1,14);
	printf("Player 1");
	gotoxy(72,14);
	printf("Player 2");
	gotoxy(x,y);
	printf("Toi luot: ");
}
void HuongDanChoi()
{
	mauchu(10);
	gotoxy(20,17);
	printf("Dung cac phim mui ten de di chuyen tren ban co");
	gotoxy(20,19);
	printf("Nhan phim SPACE de danh co");
	gotoxy(20,21);
	printf("Nhan phim BACKSPACE de undo 1 nuoc khi doi phuong");
	gotoxy(20,22);
	printf("chua bam nut di chuyen");
	gotoxy(20,24);
	printf("Nhan phim TAB de choi lai");
	gotoxy(20,26);
	printf("Nhan phim ENTER de luu lai van co");
	gotoxy(20,28);
	printf("Nhan phim ESC de tro ve menu chinh");
	mauchu(14);
	gotoxy(30,32);
	printf("Nhan phim ESC vao game");
	gotoxy(30,14);
	printf("HUONG DAN CHOI GAME");
}
void LuatChoi()
{
	mauchu(14);
	gotoxy(35,14);
	printf("LUAT CHOI");
	gotoxy(25,35);
	printf("Nhan phim ESC de tro ve menu chinh");
	mauchu(10);
	gotoxy(10,16);
	printf("Hai nguoi choi lan luot danh quan co cua minh vao cac o tren ban co.");
	gotoxy(10,18);
	printf("Nguoi nao danh duoc 5 quan lien tiep tren hang ngang, hang doc hay ");
	gotoxy(10,20);
	printf("duong cheo la thang. Luu y: 6 quan lien tiep khong duoc thang.");
	gotoxy(10,22);
	printf("Khi nguoi choi danh xong nuoc cua minh ma doi phuong chua bam di ");
	gotoxy(10,24);
	printf("chuyen thi duoc phep di lai mot nuoc.");
	gotoxy(10,26);
	printf("Khi ca hai nguoi cung dong y thi co the choi lai van moi hoac thoat");
	gotoxy(10,28);
	printf("ve menu chinh.");
	gotoxy(10,230);
	printf("Moi van co thang nguoi choi se duoc cong 100 diem");
}
void GioiThieuGame()
{
	mauchu(14);
	gotoxy(35,14);
	printf("TAC GIA");
	gotoxy(22,30);
	printf("Nhan phim ESC de tro ve menu chinh");
	mauchu(10);
	gotoxy(10,16);
	printf("Game Caro ho tro 2 nguoi choi nay duoc viet boi 2 sinh vien: ");
	gotoxy(10,18);
	printf("Nguyen Manh Sinh va Vo Nhat Sinh, hien dang la sinh vien khoa ");
	gotoxy(10,20);
	printf("Cong nghe thong tin truong Dai hoc Khoa hoc tu nhien - Dai hoc");
	gotoxy(10,22);
	printf("Quoc gia thanh pho Ho Chi Minh.");
	gotoxy(10,24);
	printf("Game nam trong do an cuoi ky nam I mon Nhap mon lap trinh cua 2 nguoi");
	gotoxy(25,28);
	mauchu(14);
	printf("CAM ON CAC BAN DA QUAN TAM! ^^");
}
void chuchay()
{
	mauchu(12);
	sizechu();
	char s[]="CHAO MUNG BAN DA DEN VOI GAME CARO 2 NGUOI CHOI";
	int x=1;
	while(!kbhit())
	{
		while(!kbhit())
		{
			if(x==29)
				break;
			else
			{
				gotoxy(x,15);
				puts(s);
				Sleep(200);
				x++;
				gotoxy(x-1,15);
				printf(" ");
			}
		}
		while(!kbhit())
		{
			if(x==1)
				break;
			else
			{
				gotoxy(x,15);
				puts(s);
				Sleep(200);
				x--;
				gotoxy(x+47,15);
				printf(" ");
			}
		}
	}
}
