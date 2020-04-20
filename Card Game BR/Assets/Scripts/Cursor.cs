﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private static Cursor _instance;
    public static Cursor Instance{
        get{
            
            
            return _instance;
        }
    }

    

     void Awake(){
        if(_instance == null)
        _instance = this;
        else{
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
     }

     public UICard CardInfo;

     float MoveAgainTimer;
     public float MaxMAT;

     private void Update() {
         MoveAgainTimer+= Time.deltaTime;
         
         if(MoveAgainTimer >= MaxMAT){
            
         if(Input.GetAxis("Horizontal")>0.3){
             MovetoTile(xpos+1,ypos);
              MoveAgainTimer = 0;
         }
         else if(Input.GetAxis("Horizontal")<-0.3){
             MovetoTile(xpos-1,ypos);
              MoveAgainTimer = 0;
         }
         else if(Input.GetAxis("Vertical")>0.3){
             MovetoTile(xpos,ypos+1);
              MoveAgainTimer = 0;
         }
         else if(Input.GetAxis("Vertical")<-0.3){
             MovetoTile(xpos,ypos-1);
              MoveAgainTimer = 0;
         }
         }

         if(Input.GetButtonDown("Confirm")){
             if(Board.Instance.GetTiles(xpos,ypos).isCard !=null){

             
             CancelAllHighlights = false;
             
             HighlightTerrain(xpos+1,ypos);
             HighlightTerrain(xpos,ypos+1);
             HighlightTerrain(xpos-1,ypos);
             HighlightTerrain(xpos,ypos-1);
             }

         }
         if(Input.GetButtonDown("Cancel")){
             CancelAllHighlights = true;
         }

         if(Input.GetButtonDown("Change Pos")){
             if(Board.Instance.GetTiles(xpos,ypos).isCard != null)
             Board.Instance.GetTiles(xpos,ypos).ChangePos(!Board.Instance.GetTiles(xpos,ypos).FacingUp);
         }

         
     }

     public bool CancelAllHighlights;

     int xpos = 0;
    int ypos=0;

     public void MovetoTile(int x, int y){
        

         if( x<Board.Instance.xLength&& y<Board.Instance.yLength && x>=0 && y>=0){
         xpos = x;
         ypos = y;
         transform.position = Board.Instance.GetTiles(x,y).tilePosition;
         }
          if(Board.Instance.GetTiles(xpos,ypos).isCard!=null){
             CardInfo.UpdateCard(Board.Instance.GetTiles(xpos,ypos).isCard, Board.Instance.GetTiles(xpos,ypos).currentCardHP);
         }
         else{
             
         }
     }

     public Tiles GetCurrentTile(){
         return Board.Instance.GetTiles(xpos,ypos);
     }

     public void HighlightTerrain(int x, int y){
        if(Board.Instance.GetTiles(x,y).isCard == null && Board.Instance.GetTiles(x,y).isPlayer == null){
            Board.Instance.GetTiles(x,y).highlight.SetActive(true);
            Board.Instance.GetTiles(x,y).Attack.SetActive(false);
        }
        else{
            Board.Instance.GetTiles(x,y).highlight.SetActive(false);
            Board.Instance.GetTiles(x,y).Attack.SetActive(true);
        }
    }
    

}
