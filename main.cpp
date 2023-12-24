#include "cppgraphics.hpp"
#include <iostream>
#include<fstream>
#include <windows.h>
using namespace std;

void new_matrix(int tiles[][4]){
    
    for(int i=0;i<4;i++){
       for(int j=0;j<4;j++){
         tiles[i][j]=0;
       }
    }
   

}   
void new_number(int tiles[][4]){
 while(true){
    int x=cg::random_int(3);
    int y=cg::random_int(3);
    if(tiles[x][y]==0){
        tiles[x][y]=2;
        break;
    }
 }

}
void create_rectangle(int x, int y, int width,int height){
    cg::rectangle(x, y,width,height);

}
int disp_highscore(){
    int highscore;
   ifstream pscores;
    pscores.open("C:\\Users\\Public\\Documents\\2048 Resources\\scores.txt");
    pscores>>highscore;
     return highscore;

}
void create_tile(int x, int y, int value,int scr){
   int tileSize = 150;
      cg::set_color(cg::Yellow);
      cg::text_centered("Your Score:",175,40,50);
      cg::text_centered(to_string(scr),135,80,40);
      cg::text_centered("High Score!:",600,40,50);
      cg::text_centered(std::to_string(disp_highscore()),600,80,40);
      cg::text_centered("Press R to reset",150,750,30);
      cg::text_centered("Press S to save game",600,750,30);
 
    if (value == 2) {
        cg::set_color(cg::Brown);  
        cg::set_fill_color(cg::Yellow);  
    } 
    else if (value == 4) {
        cg::set_color(cg::Brown);  
        cg::set_fill_color(cg::Green);  
    } 
    else if (value == 8) {
        cg::set_color(cg::White);  
        cg::set_fill_color(cg::DarkGray);  
    }
     else if (value == 16) {
        cg::set_color(cg::White);  
        cg::set_fill_color(cg::DarkGreen);  
    }
    else if (value == 32) {
        cg::set_color(cg::White);  
        cg::set_fill_color(cg::DarkBlue);  
    }
    else if (value == 64) {
        cg::set_color(cg::White);  
        cg::set_fill_color(cg::DarkRed);  
    }
     else {
        cg::set_color(cg::White);
        cg::set_fill_color(cg::Orange);
        
    }
    create_rectangle(100+x*tileSize,100+y*tileSize,tileSize,tileSize);
    if(value!=0){
        cg::text_centered(to_string(value),175+x*tileSize,175+y*tileSize,50);
    }

} 
void create(const int tiles[][4],int scr){
   cg::set_color(cg::Yellow);
   cg::set_fill_color(cg::Yellow);
   cg::set_background_color(cg::Brown);
   cg::clear();
   for(int i=0;i<4;i++){
      for(int j=0;j<4;j++){
       create_tile(i,j,tiles[i][j],scr);
      }
    }  

}
void swap_vertically(int tiles[][4])
{
    for (int x = 0; x < 4; x++) {
        swap(tiles[0][x],tiles[1][x]);
        swap(tiles[2][x], tiles[3][x]);
    }
}
void swap_horizontally(int tiles[][4])
{
    for(int i=0;i<4;i++){
        swap(tiles[i][0],tiles[i][1]);
        swap(tiles[i][2],tiles[i][3]);
    }
}
void merge_columns(int tiles[][4], int index,int key, int &scr) {
   if(key==cg::KeyUp){
    for(int dest=0;dest<=3;dest++){
        
      int src=dest+1;
     while(src<=3&&tiles[index][src]==0){
        src++;
     }
     if(src>3){
        break;
     }
    if(tiles[index][src]==tiles[index][dest]){
            tiles[index][dest]=tiles[index][dest]*2;
            tiles[index][src]=0;
            scr=scr+tiles[index][dest];
          
     }
    if(tiles[index][dest]==0){
            swap(tiles[index][dest],tiles[index][src]);
          dest--;
    }
  }
 }
  if(key==cg::KeyDown){
   for(int dest=3;dest>=0;dest--){
        
      int src=dest-1;
     while(src>=0&&tiles[index][src]==0){
        src--;
     }
     if(src<0){
        break;
     }
         if(tiles[index][src]==tiles[index][dest]){
            tiles[index][dest]=tiles[index][dest]*2;
            tiles[index][src]=0;
            scr=scr+tiles[index][dest];
          
         }
        if(tiles[index][dest]==0){
            swap(tiles[index][dest],tiles[index][src]);
          dest++;
        }
    }
 }
}
void merge_rows(int tiles[][4],int index,int key ,int &scr){
    if(key==cg::KeyLeft){
     for(int dest=0;dest<=3;dest++){
        
      int src=dest+1;
     while(src<=3&&tiles[src][index]==0){
        src++;
     }
     if(src>3){
        break;
     }
         if(tiles[src][index]==tiles[dest][index]){
            tiles[dest][index]=tiles[dest][index]*2;
            tiles[src][index]=0;
            scr=scr+tiles[dest][index];
          
         }
        if(tiles[dest][index]==0){
            swap(tiles[dest][index],tiles[src][index]);
          dest--;
        }
        
    }
    }
    if(key==cg::KeyRight){
   for(int dest=3;dest>=0;dest--){
        
      int src=dest-1;
     while(src>=0&&tiles[src][index]==0){
        src--;
     }
     if(src<0){
        break;
     }
         if(tiles[src][index]==tiles[dest][index]){
            tiles[dest][index]=tiles[dest][index]*2;
            tiles[src][index]=0;
            scr=scr+tiles[dest][index];
          
         }
        if(tiles[dest][index]==0){
            swap(tiles[dest][index],tiles[src][index]);
          dest++;
        }
        
    }
    }
}

void merge(int tiles[][4], int key,int &scr)
{
    if (key == cg::KeyLeft || key == cg::KeyRight){
        swap_horizontally(tiles);
    for(int x=0;x<4;x++){
        merge_rows(tiles,x,key,scr);

     }
    
    }
    if (key == cg::KeyUp || key == cg::KeyDown){
        swap_vertically(tiles);

   
     for(int y=0;y<4;y++){
        merge_columns(tiles,y,key,scr);

     }
    }

    if (key == cg::KeyUp || key == cg::KeyDown)
        swap_vertically(tiles);
    if (key == cg::KeyLeft || key == cg::KeyRight)
        swap_horizontally(tiles);
    
}
void reset(int tiles[][4],int &scr){
 for (int x = 0; x < 4; x++){
        for (int y = 0; y < 4; y++){
             tiles[x][y]=0;
        }
    }   
    new_number(tiles);

    create(tiles,scr=0);

}
void savegame(int tiles[][4],int scr){
   ofstream lastsave;
   ofstream lastscore;
   lastsave.open("C:\\Users\\Public\\Documents\\2048 Resources\\saves.txt");
  lastscore.open("C:\\Users\\Public\\Documents\\2048 Resources\\save score.txt");
   for(int i=0;i<4;i++){
    for(int j=0;j<4;j++){
        lastsave<<tiles[i][j]<<endl;
    }
   }  
  lastscore<<scr;

}
void loadgame(int tiles[][4],int &scr){
    ifstream loadsave;
    ifstream loadscore;
    loadsave.open("C:\\Users\\Public\\Documents\\2048 Resources\\saves.txt");
    loadscore.open("C:\\Users\\Public\\Documents\\2048 Resources\\save score.txt");
    for(int i=0;i<4;i++){
        for(int j=0;j<4;j++){
            
            while(!(loadsave>>tiles[i][j]));
           
        }
    }
     loadscore>>scr;
} 
bool is_2048(int tiles[][4]){
for (int x = 0; x < 4; x++){
        for (int y = 0; y < 4; y++){
            if (tiles[x][y] == 2048)
                return true;
        }
    }
    return false;
}
bool is_full(int tiles[][4])
{
    for (int x = 0; x < 4; x++)
        for (int y = 0; y < 4; y++)
            if (tiles[x][y] == 0)
                return false;
    return true;
}
bool has_moves(int tiles[][4]) {
    
    for (int x = 0; x < 3; x++) {
        for (int y = 0; y < 3; y++) {
            if (tiles[x][y] == tiles[x + 1][y] || tiles[x][y] == tiles[x][y + 1]) {
                return false;
            }
        }
    }
    return true;
}
bool if_highscore(int scr) {
    int lastscore;
    ifstream previousscores;
    previousscores.open("C:\\Users\\Public\\Documents\\2048 Resources\\scores.txt");
    previousscores >> lastscore;
     previousscores.close();
    if (lastscore<scr) {
        ofstream newscores;
        newscores.open("C:\\Users\\Public\\Documents\\2048 Resources\\scores.txt");
        newscores << scr;
        newscores.close();
        return true;
    }
   return false;
}
int main()

{   
 bool start=true;
 
 while(start){
    
    PlaySound("C:\\Users\\Public\\Documents\\2048 Resources\\music\\menu.wav", NULL, SND_FILENAME | SND_ASYNC);
    int score=0;
    bool gameover=false;
    cg::create_window("", 799, 799);
    cg::image("C:\\Users\\Public\\Documents\\2048 Resources\\20481.jpg",0,0);
    cg::set_font("C:\\Users\\Public\\Documents\\2048 Resources\\River Adventurer\\River Adventurer.ttf");
    cg::set_color(cg::Yellow);
    cg::text_centered("2048 Game",220,100,80);
    cg::set_color(cg::Cyan);
     cg::text_centered("Join the numbers and get to the 2048 tile!",350,160,30);
     cg::set_color(cg::Orange);
    cg::text_centered("Press an Arrow key to start the game", 400,220, 30);
    cg::text_centered("Press L to load the saved game", 400,260, 30);
    cg::text_centered("Press ESC key to exit the game", 400,300, 30);
     cg::set_color(cg::Yellow);
    cg::text_centered("HOW TO PLAY:Use your arrow keys to move the tiles",400,350,30);
     cg::text_centered("When two tiles with the same number touch they,",400,400,30);
    cg::text_centered("merge into one!",400,450,40);
    cg::set_color(cg::Brown);
    cg::set_fill_color(cg::Yellow);
    int tileSize = 100;
    int radius=tileSize/2;
    cg::rectangle(500,550,tileSize, tileSize);
    cg::text_centered("2",500+radius,600,50);
    cg::set_color(cg::White);
    cg::set_fill_color(cg::Orange);
    cg::rectangle(600,550,tileSize, tileSize-1.2);
    cg::text_centered("0",600+radius,600,50);
    cg::set_color(cg::Brown);
    cg::set_fill_color(cg::Green);
    cg::rectangle(500,650,tileSize, tileSize);
    cg::text_centered("4",500+radius,700,50);
    cg::set_color(cg::White);
    cg::set_fill_color(cg::Gray);
    cg::rectangle(600,650,tileSize, tileSize-1);
    cg::text_centered("8",600+radius,700,50);
    cg::set_color(cg::Cyan);
    cg::text_centered("Try to reach !",250,530,60);
    cg::set_color(cg::Magenta);
    cg::text_centered("2048 Tile",230,620,60);
    
    int checker;
    int tiles[4][4];
     new_matrix(tiles);
    checker=cg::wait_until_keypressed();
    if(checker==cg::KeyEscape){
     return 0;
    }
    
    else{

     PlaySound(nullptr, nullptr, 0);
     PlaySound("C:\\Users\\Public\\Documents\\2048 Resources\\music\\8bit.wav", NULL, SND_FILENAME | SND_ASYNC);
    if(checker==cg::KeyL){
        cg::clear();
            cg::set_color(cg::Yellow);
            cg::image("C:\\Users\\Public\\Documents\\2048 Resources\\20481.jpg",0,0,0,0);
            cg::text_centered("Loading Saved Game", 400, 400, 50);
            cg::wait(1);
            loadgame(tiles,score);
            create(tiles,score);
           }
    else
     new_number(tiles);

      while (cg::is_window_open()) {
      
        int key = cg::wait_until_keypressed();
        
        if(key==cg::KeyUp||key==cg::KeyDown||key==cg::KeyLeft||key==cg::KeyRight){
         new_number(tiles);
         create(tiles,score);
         merge(tiles, key,score);
        }
        if(key==cg::KeyR){
           reset(tiles,score);
        }
        if (key == cg::KeyEscape) {
            cg::close_window();
            break;
        }
        if(key==cg::KeyS){
            savegame(tiles,score);
            cg::clear();
            cg::set_color(cg::Yellow);
            cg::image("C:\\Users\\Public\\Documents\\2048 Resources\\20481.jpg",0,0,0,0);
            cg::text_centered("Saving Game", 400, 400, 50);
            cg::wait(1);
           break;
        }
        if(is_2048(tiles)){
            gameover=true;
             
        }
        if (is_full(tiles)){
            if(!has_moves(tiles)){
               merge(tiles, key=cg::wait_until_keypressed(),score);
               merge(tiles, key=cg::wait_until_keypressed(),score);
               merge(tiles, key=cg::wait_until_keypressed(),score);
               merge(tiles, key=cg::wait_until_keypressed(),score);
               if(is_full(tiles)){
                gameover=true;
               }
            }
            else
          gameover=true;
        }
        
        if(gameover){
            cg::wait(1);
            cg::clear();
            cg::set_color(cg::Yellow);
            cg::image("C:\\Users\\Public\\Documents\\2048 Resources\\20481.jpg",0,0,0,0);
              if(if_highscore(score)){
           PlaySound(nullptr, nullptr, 0);
           PlaySound("C:\\Users\\Public\\Documents\\2048 Resources\\music\\highscore.wav", NULL, SND_FILENAME | SND_ASYNC);
                cg::text_centered("Congratulations! New High Score", 400, 400, 40);

           }
            else{
                
            PlaySound(nullptr, nullptr, 0);
            PlaySound("C:\\Users\\Public\\Documents\\2048 Resources\\music\\ending.wav", NULL, SND_FILENAME | SND_ASYNC);
           
             cg::text_centered("Game Over", 400, 400, 50);
         }
   
          cg::wait(2);
          cg::clear(); 
          cg::text_centered("Play Again Y/N:",400,400,50);
          int key1=cg::wait_until_keypressed();
    
          if(key1==cg::KeyN){
             start=false;
           }
        
         cg::close_window();
        }
           
      }
     
    }
 }
 return 0;
}