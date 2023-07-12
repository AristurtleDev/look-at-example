using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LookAt;

public class Game1 : Game
{
    //  For example purposes, making the window 1280x270
    const int _windowWidth = 1280;
    const int _windowHeight = 720;

    //  Just so I can calculate the center of the screen to place the player at

    //  Player stuff
    private float _playerRotation = 0.0f;
    private Texture2D _playerTexture;
    private Vector2 _playerPosition;
    private Vector2 _playerCenterOrigin;

    //  Marker stuff that the player will "look at"
    private Texture2D _markerTexture;
    private Vector2 _markerPosition;
    private Vector2 _markerCenterOrigin;


    //  Standard MonoGame boiler plate
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;



    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        //  Just setting resolution
        _graphics.PreferredBackBufferWidth = _windowWidth;
        _graphics.PreferredBackBufferHeight = _windowHeight;
        _graphics.IsFullScreen = false;
        _graphics.ApplyChanges();

        //  Hiding mouse since we'll use the "marker" graphic to show the mouse position.
        IsMouseVisible = false;

        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _playerTexture = Content.Load<Texture2D>("player");

        //  Set player at center of window for demonstration purposes
        _playerPosition = new Vector2(_windowWidth, _windowHeight) * 0.5f;
        _playerCenterOrigin = _playerTexture.Bounds.Size.ToVector2() * 0.5f;


        //  Marker position will follow the mouse, see Update section for that
        _markerTexture = Content.Load<Texture2D>("marker");
        _markerCenterOrigin = _markerTexture.Bounds.Size.ToVector2() * 0.5f;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();


        //  Position of the marker is just the mouse position
        _markerPosition = Mouse.GetState().Position.ToVector2();


        //  Calculate the difference between the player and marker
        Vector2 delta = _playerPosition - _markerPosition;

        //  Atan2 to get the rotation. Since the "player" arrow is pointed "Up" by default,
        //  we have to subtract PiOver2 (essentially 90degs) since Atan2 rotates counter clockwise
        //  from origin (0, 0) pointing right.
        _playerRotation = (float)Math.Atan2(delta.Y, delta.X) - MathHelper.PiOver2;

        base.Update(gameTime);
    }



    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _spriteBatch.Draw(_playerTexture, _playerPosition, null, Color.White, _playerRotation, _playerCenterOrigin, Vector2.One, SpriteEffects.None, 0.0f);
        _spriteBatch.Draw(_markerTexture, _markerPosition, null, Color.White, 0.0f, _markerCenterOrigin, Vector2.One, SpriteEffects.None, 0.0f);


        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
