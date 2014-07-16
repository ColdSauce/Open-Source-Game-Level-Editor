#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace OSGLE.cs {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class OSGLE : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static Texture2D defaultCellTexture;
        static Texture2D currentCellTexture;
        static Texture2D wallCellTexture;
        static Texture2D grassCellTexture;
        public static readonly int SCREEN_WIDTH = 768;
        public static readonly int SCREEN_HEIGHT = 768;
        public static readonly int BLOCK_SIZE = 32;
        Cell[,] cells = new Cell[1000,1000];
        private Cell currentCell;
        


        public OSGLE()
            : base() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }


        protected override void Initialize() {

            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.ApplyChanges();

            base.Initialize();
        }

        public static Texture2D GetWallTexture() {
            return wallCellTexture;
        }

        public static Texture2D GetGrassTexture() {
            return grassCellTexture;
        }

        public static Texture2D GetDefualtTexture() {
            return defaultCellTexture;
        }

        public static Texture2D GetCurrentCellTexture() {
            return currentCellTexture;
        }


        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            defaultCellTexture = Content.Load<Texture2D>("defaultCellTexture.png");
            grassCellTexture = Content.Load<Texture2D>("grassCellTexture.png");
            currentCellTexture = Content.Load<Texture2D>("currentCellTexture.png");




            currentCell = new Cell(Point.Zero);
            for (int i = 0; i < 1000; i++) {
                for (int j = 0; j < 1000; j++) {
                    Cell cell = new Cell(new Point(i * 32, j * 32));
                    cells[i, j] = cell;
                }
            }



        }


        protected override void UnloadContent() {

        }
        KeyboardState OldKeyState;

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState NewKeyState = Keyboard.GetState();

            if (NewKeyState.IsKeyDown(Keys.Enter) && OldKeyState.IsKeyUp(Keys.Enter)) {
                cells[currentCell.realPoint.X / 32,currentCell.realPoint.Y / 32].lt = LandType.GRASS;
            }

            if (NewKeyState.IsKeyDown(Keys.Left) && OldKeyState.IsKeyUp(Keys.Left)) {
                currentCell = new Cell(new Point(currentCell.realPoint.X - 32, currentCell.realPoint.Y));
                
            }

            if (NewKeyState.IsKeyDown(Keys.Up) && OldKeyState.IsKeyUp(Keys.Up)) {
                currentCell = new Cell(new Point(currentCell.realPoint.X, currentCell.realPoint.Y-32));
            }


            if (NewKeyState.IsKeyDown(Keys.Right) && OldKeyState.IsKeyUp(Keys.Right)) {
                currentCell = new Cell(new Point(currentCell.realPoint.X + 32, currentCell.realPoint.Y));
            }


            if (NewKeyState.IsKeyDown(Keys.Down) && OldKeyState.IsKeyUp(Keys.Down)) {
                currentCell = new Cell(new Point(currentCell.realPoint.X, currentCell.realPoint.Y+32));
            }



            OldKeyState = NewKeyState;

        


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            foreach (Cell c in cells) {
                if (c.realPoint.X >= 0 && c.realPoint.X <= SCREEN_WIDTH) {
                    if (c.realPoint.Y >= 0 && c.realPoint.Y <= SCREEN_HEIGHT) {
                        c.Draw(spriteBatch);
                    }
                }
            }
            currentCell.DrawCurrentCell(spriteBatch);
                base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}
