﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ContentPooling;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.ObjectPooling.ObjectTypes;
using Todesesser.ObjectPooling;
using Microsoft.Xna.Framework.Input;
using Todesesser.Core;
using Todesesser.Screens;

namespace Todesesser
{
    public class GameCore
    {
        private int Width;
        private int Height;
        private ContentPool Content;
        private ObjectPool Objects;
        private GraphicsDeviceManager Graphics;
        private Game Game;

        //Screens:
        GameScreen screenGame;
        MenuScreen screenMenu;
        PauseScreen screenPause;

        public GameCore(int width, int height, ContentPool content, ObjectPool objects, GraphicsDeviceManager graphics, Game game)
        {
            Game = game;
            Graphics = graphics;
            Width = width;
            Height = height;
            Content = content;
            Objects = objects;
        }

        public void Initialize()
        {
            screenGame.Initialize();
            screenMenu.Initialize();
            screenPause.Initialize();
        }

        public void LoadContent()
        {
            //GameData.GameState = GameData.GameStates.Menu;
            GameData.GameState = GameData.GameStates.Menu;

            //Create Screens:
            screenGame = new GameScreen(Graphics.GraphicsDevice, Objects, Content);
            screenMenu = new MenuScreen(Graphics.GraphicsDevice, Objects, Content);
            screenPause = new PauseScreen(Graphics.GraphicsDevice, Objects, Content);
            
            //Load Screens:
            screenGame.LoadContent();
            screenMenu.LoadContent();
            screenPause.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            //Update Screen:
            switch (GameData.GameState)
            {
                case GameData.GameStates.Playing:
                    screenGame.Update(gameTime);
                    break;
                case GameData.GameStates.Menu:
                    screenMenu.Update(gameTime);
                    break;
                case GameData.GameStates.Exiting:
                    Game.Exit();
                    break;
                case GameData.GameStates.Paused:
                    screenPause.Update(gameTime);
                    break;
            }
        }

        public void Draw(GameTime gameTime)
        {
            //Draw Screen:
            switch (GameData.GameState)
            {
                case GameData.GameStates.Playing:
                    screenGame.Draw(gameTime);
                    break;
                case GameData.GameStates.Menu:
                    screenMenu.Draw(gameTime);
                    break;
                case GameData.GameStates.Paused:
                    screenGame.Draw(gameTime);
                    screenPause.Draw(gameTime);
                    break;
            }
        }
    }
}
